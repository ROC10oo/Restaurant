using Application.Interfaces.ICategory;
using Application.Interfaces.IDeliveryType;
using Application.Interfaces.IDish;
using Application.Interfaces.IOrder;
using Application.Interfaces.IOrderItem;
using Application.Interfaces.IStatus;
using Application.Services.ServiceCategory;
using Application.Services.ServiceDeliveryType;
using Application.Services.ServiceOrder;
using Application.Services.ServicesDish;
using Application.Services.ServiceStatus;
using Application.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Command;
using Infrastructure.Data;
using Infrastructure.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Restaurant.Middlewares;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // Esto reemplaza el comportamiento automático de 400 Bad Request
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Restaurant API",
        Version = "1.0",
        Description = "API para la gestión de platos en un restaurante",
        Contact = new OpenApiContact { 
            Name = "Restaurant API Support",
            Email = "",    
        }
    });


    c.UseInlineDefinitionsForEnums(); //Enums

    // XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);



    c.MapType<Microsoft.AspNetCore.Mvc.ProblemDetails>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "object",
        AdditionalPropertiesAllowed = true
    });



    c.EnableAnnotations(); //Annotations for swagger
    c.ExampleFilters(); //Examples for swagger
});

// Configurar DbContext
builder.Services.AddDbContext<RestauranteDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Registrar servicios de dominio (arquitectura hexagonal)
builder.Services.AddScoped<IDishCommand, DishCommand>();
builder.Services.AddScoped<IDishQuery, DishQuery>();

builder.Services.AddScoped<IUpdateDishService, UpdateDishService>();
builder.Services.AddScoped<ICreateDishService, CreateDishService>();
builder.Services.AddScoped<IGetDishesService, GetDishesService>();
builder.Services.AddScoped<IGetDishService, GetDishService>();
builder.Services.AddScoped<IDeleteDishService, DeleteDishService>();

builder.Services.AddScoped<ICategoryCommand, CategoryCommand>();
builder.Services.AddScoped<ICategoryQuery, CategoryQuery>();
builder.Services.AddScoped<IGetCategoriesService, GetCategoriesService>();



builder.Services.AddScoped<IStatusQuery, StatusQuery>();
builder.Services.AddScoped<IGetStatusService, GetStatusService>();



builder.Services.AddScoped<IDeliveryTypeQuery, DeliveryTypeQuery>();
builder.Services.AddScoped<IGetDeliverysTypesService, GetDeliverysTypesService>();

builder.Services.AddScoped<IOrderQuery, OrderQuery>();
builder.Services.AddScoped<IOrderCommand, OrderCommand>();
builder.Services.AddScoped<ICreateOrderService, CreateOrderService>();
builder.Services.AddScoped<IGetOrdersService, GetOrdersService>();
builder.Services.AddScoped<IGetOrderService, GetOrderService>();
builder.Services.AddScoped<IUpdateOrderService, UpdateOrderService>();
builder.Services.AddScoped<IUpdateOrderItemForStatusService, UpdateOrderItemForStatusService>();


builder.Services.AddScoped<IOrderItemCommand, OrderItemCommand>();
builder.Services.AddScoped<IOrderItemQuery, OrderItemQuery>();


builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

builder.Services.AddScoped<IValidatorCreatedDish, DishValidatorCreated>();
builder.Services.AddScoped<IValidatorUpdateDish, DishValidatorUpdate>();



builder.Services.AddScoped<DishValidatorUpdate>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>(); //Middleware for errors

// Configurar Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API v1");
        c.RoutePrefix = "swagger"; 

    });
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<RestauranteDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error in Migration.");
    }
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
