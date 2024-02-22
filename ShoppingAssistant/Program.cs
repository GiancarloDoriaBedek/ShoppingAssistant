using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShoppingAssistant.Data;
using ShoppingAssistant.Repository.Implementations;
using ShoppingAssistant.Repository.Interfaces;
using ShoppingAssistant.Services.Implementations;
using ShoppingAssistant.Services.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace ShoppingAssistant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
            builder.Services.AddHttpClient();

            // Services
            builder.Services.AddScoped<IPasswordService, PasswordService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IWebshopScraperService, WebshopScraperService>();
            builder.Services.AddScoped<IStringDistanceService, StringDistanceService>();
            builder.Services.AddScoped<IProductMappingService, ProductMappingService>();
            builder.Services.AddScoped<IProductService, ProductService>();

            // Repositories
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPackageRepository, PackageRepository>();
            builder.Services.AddScoped<IStoreRepository, StoreRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(builder.Configuration
                                .GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

            builder.Services.AddDbContext<DataContext>(options =>
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

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}