using ApiDogsCrud.BusinessLogic;
using ApiDogsCrud.BusinessLogic.MappingProfile;
using ApiDogsCrud.BusinessLogic.Validators;
using ApiDogsCrud.Contracts;
using ApiDogsCrud.DataLayer;
using ApiDogsCrud.DataLayer.Entity;
using ApiDogsCrud.DataLayer.Repository;
using ApiDogsCrud.Models;
using AspNetCoreRateLimit;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ApiDogsCrud.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<DogDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDogService, DogService>();
            builder.Services.AddScoped<IRepository<Dog>, RepositoryBase<Dog>>();
            builder.Services.AddScoped<IValidator<Dog>, DogValidator>();
            builder.Services.AddScoped<IValidator<DogsFilter>, DogsFilterValidator>();
            builder.Services.AddAutoMapper(typeof(DogService));
            builder.Services.AddAutoMapper(typeof(DogMappingProfile));

            builder.Services.AddMemoryCache();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            builder.Services.AddOptions();
            builder.Services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            builder.Services.AddInMemoryRateLimiting();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // Enable rate limiting
            app.UseIpRateLimiting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}