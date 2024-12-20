
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyWardrobe.Contracts.WardrobeItem;
using MyWardrobe.Data;
using MyWardrobe.Services.WardrobeItems;
using MyWardrobe.Services.WardrobeItemsUsage;
using MyWardrobe.Validations;

namespace MyWardrobe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IWardrobeItemService, WardrobeItemService>();
            builder.Services.AddScoped<IWardrobeItemUsageService, WardrobeItemUsageService>();
            builder.Services.AddScoped<IValidator<CreateWardrobeItemRequest>, CreateWardrobeItemRequestValidator>();
            builder.Services.AddScoped<IValidator<UpdateWardrobeItem>, UpdateWardrobeItemValidator>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MyWardrobeDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
