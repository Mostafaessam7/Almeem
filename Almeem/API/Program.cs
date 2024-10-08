using Core.Context;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Services.Services.CategoryService;
using Services.Services.CategoryService.Dto;
using Services.Services.ColorService;
using Services.Services.ColorService.Dto;
using Services.Services.ProductService;
using Services.Services.ProductService.Dto;
using Services.Services.ProductSizeColorService.Dto;
using Services.Services.SizeService;
using Services.Services.SizeService.Dto;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            options.AddDefaultPolicy(
                policy=>policy.AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowAnyOrigin()
                )

                );


            #region Self Registered Services
            builder.Services.AddDbContext<AlmeemContext>(opt =>
                    {
                        opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                    });
                builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                builder.Services.AddScoped<IProductRepository, ProductRepository>();
                builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
                builder.Services.AddScoped<IColorRepository, ColorRepository>();
                builder.Services.AddScoped<ISizeRepository, SizeRepository>();
                builder.Services.AddScoped<IProductService, ProductService>();
                builder.Services.AddScoped<ICategoryService, CategoryService>();
                builder.Services.AddScoped<ISizeService, SizeService>();
                builder.Services.AddScoped<IColorService, ColorService>();
                builder.Services.AddAutoMapper(m => m.AddProfile(new ProductProfile()));
                builder.Services.AddAutoMapper(m => m.AddProfile(new ProductSizeColorProfile())); 
                builder.Services.AddAutoMapper(m => m.AddProfile(new CategoryProfile())); 
                builder.Services.AddAutoMapper(m => m.AddProfile(new SizeProfile())); 
                builder.Services.AddAutoMapper(m => m.AddProfile(new ColorProfile())); 
            #endregion

            var app = builder.Build();

            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<AlmeemContext>();
                        await context.Database.MigrateAsync();
                        await AlmeemContextSeed.SeedAsync(context);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
