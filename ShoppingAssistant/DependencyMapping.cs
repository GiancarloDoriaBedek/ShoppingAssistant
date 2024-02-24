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
    public class DependencyMapping
    {
        private readonly WebApplicationBuilder _builder;

        public DependencyMapping(string[] args) 
        {
            _builder = WebApplication.CreateBuilder(args);
        }

        public WebApplication BuildMappedWebApplication()
        {
            MapControllers();

            MapAutoMapper();
            MapHttpClient();

            MapServices();
            MapRepositories();

            MapSwagger();
            MapAuthentication();
            MapDatabaseConnection();

            return _builder.Build();
        }

        private void MapControllers()
        {
            _builder.Services.AddControllers();
        }

        private void MapAutoMapper()
        {
            _builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
        }

        private void MapHttpClient()
        {
            _builder.Services.AddHttpClient();
        }

        private void MapServices()
        {
            _builder.Services.AddScoped<IPasswordService, PasswordService>();
            _builder.Services.AddScoped<ITokenService, TokenService>();
            _builder.Services.AddScoped<IWebshopScraperService, WebshopScraperService>();
            _builder.Services.AddScoped<IStringDistanceService, StringDistanceService>();
            _builder.Services.AddScoped<IProductMappingService, ProductMappingService>();
            _builder.Services.AddScoped<IProductService, ProductService>();
        }

        private void MapRepositories()
        {
            _builder.Services.AddScoped<IProductRepository, ProductRepository>();
            _builder.Services.AddScoped<IUserRepository, UserRepository>();
            _builder.Services.AddScoped<IPackageRepository, PackageRepository>();
            _builder.Services.AddScoped<IStoreRepository, StoreRepository>();
            _builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            _builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        }

        private void MapSwagger()
        {
            _builder.Services.AddEndpointsApiExplorer();
            _builder.Services.AddSwaggerGen(options =>
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
        }

        private void MapAuthentication()
        {
            _builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(_builder.Configuration
                                .GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }


        private void MapDatabaseConnection()
        {
            var connectionString = _builder.Configuration["ConnectionStrings:DefaultConnection"];

            _builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
