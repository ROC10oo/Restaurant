using Application.Interfaces.ICategory;
using Application.Interfaces.IDish;
using Application.Services.ServicesDish;
using Application.Validator;
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

// Habilita controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());  //Enums 
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

builder.Services.AddScoped<ICategoryCommand, CategoryCommand>();
builder.Services.AddScoped<ICategoryQuery, CategoryQuery>();

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

builder.Services.AddScoped<DishValidatorCreated>();
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

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
