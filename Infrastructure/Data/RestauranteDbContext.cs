using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Data
{
    public class RestauranteDbContext : DbContext
    {
        public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options) : base(options)
        {
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Dish
            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("Dish");
                entity.HasKey(d => d.DishId);
                entity.Property(d => d.Name).IsRequired().HasColumnType("varchar(255)");
                entity.Property(d => d.Description).HasColumnType("varchar(max)");
                entity.Property(d => d.Price).IsRequired().HasColumnType("decimal(10,2)");
                entity.Property(d => d.Available).IsRequired();
                entity.Property(d => d.ImageUrl).HasColumnType("varchar(max)");
                entity.Property(d => d.CreateDate).IsRequired();
                entity.Property(d => d.UpdateDate).IsRequired();

                entity.Property(o => o.CategoryId)
                      .HasColumnName("Category");
                // Relación con Category
                entity.HasOne(e => e.Category)
                      .WithMany(c => c.Dishes)
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relation with OrderItems
                entity.HasMany(e => e.OrderItems)
                      .WithOne(oi => oi.Dish)
                      .HasForeignKey(oi => oi.DishId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            //Category 
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasColumnType("varchar(25)");
                entity.Property(c => c.Description).HasColumnType("varchar(255)");
                entity.Property(c => c.Order).IsRequired();
                entity.HasData(
                    new Category { Id = 1, Name = "Entradas", Description = "Pequeñas porciones para abrir el apetito antes del plato principal.", Order = 1 },
                    new Category { Id = 2, Name = "Ensaladas", Description = "Opciones frescas y livianas, ideales como acompañamiento o plato principal.", Order = 2 },
                    new Category { Id = 3, Name = "Minutas", Description = "Platos rápidos y clásicos de bodegón: milanesas, tortillas, revueltos.", Order = 3 },
                    new Category { Id = 4, Name = "Pastas", Description = "Variedad de pastas caseras y salsas tradicionales.", Order = 5 },
                    new Category { Id = 5, Name = "Parrilla", Description = "Cortes de carne asados a la parrilla, servidos con guarniciones.", Order = 4 },
                    new Category { Id = 6, Name = "Pizzas", Description = "Pizzas artesanales con masa casera y variedad de ingredientes.", Order = 7 },
                    new Category { Id = 7, Name = "Sandwiches", Description = "Sandwiches y lomitos completos preparados al momento.", Order = 6 },
                    new Category { Id = 8, Name = "Bebidas", Description = "Gaseosas, jugos, aguas y opciones sin alcohol.", Order = 8 },
                    new Category { Id = 9, Name = "Cerveza Artesanal", Description = "Cervezas de producción artesanal, rubias, rojas y negras.", Order = 9 },
                    new Category { Id = 10, Name = "Postres", Description = "Clásicos dulces caseros para cerrar la comida.", Order = 10 }
                );
            });

            //Status
            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasColumnType("varchar(25)");
                entity.HasData(
                    new Status { Id = 1, Name = "Pending" },
                    new Status { Id = 2, Name = "In Progress" },
                    new Status { Id = 3, Name = "Ready" },
                    new Status { Id = 4, Name = "Delivery" },
                    new Status { Id = 5, Name = "Closed" }
                );
            });

            //DeliveryType
            modelBuilder.Entity<DeliveryType>(entity =>
            {
                entity.ToTable("DeliveryType");
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired().HasColumnType("nvarchar(25)");
                entity.HasData(
                    new DeliveryType { Id = 1, Name = "Delivery" },
                    new DeliveryType { Id = 2, Name = "Takeaway" },
                    new DeliveryType { Id = 3, Name = "Dine-In" }
                );
            });

            //Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");
                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.DeliveryTo).IsRequired().HasColumnType("varchar(255)");
                entity.Property(o => o.Notes).HasColumnType("varchar(max)");
                entity.Property(o => o.Price).IsRequired().HasColumnType("decimal(10,2)");
                entity.Property(o => o.CreateDate).IsRequired();
                entity.Property(o => o.UpdateDate).IsRequired();

                // Relations 
                entity.Property(o => o.StatusId)
                      .HasColumnName("OverallStatus");
                entity.HasOne(o => o.OverallStatus)
                      .WithMany(s => s.Orders)
                      .HasForeignKey(o => o.StatusId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(o => o.DeliveryTypeId)
                      .HasColumnName("DeliveryType");
                entity.HasOne(o => o.DeliveryType)
                      .WithMany(d => d.Orders)
                      .HasForeignKey(o => o.DeliveryTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(o => o.OrderItems)
                      .WithOne(oi => oi.Order)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            //OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");
                entity.HasKey(o => o.OrderItemId);

                entity.Property(o => o.Quantity).IsRequired();
                entity.Property(o => o.Notes).HasColumnType("varchar(max)");
                entity.Property(o => o.CreateDate).IsRequired();

                entity.Property(o => o.OrderId)
                      .HasColumnName("Order");
                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relation with Status
                entity.Property(o => o.StatusId)
                      .HasColumnName("Status");
                entity.HasOne(oi => oi.Status)
                      .WithMany(s => s.OrderItems)
                      .HasForeignKey("StatusId")
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(o => o.DishId)
                      .HasColumnName("Dish");
                entity.HasOne(oi => oi.Dish)
                      .WithMany(d => d.OrderItems)
                      .HasForeignKey(oi => oi.DishId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
