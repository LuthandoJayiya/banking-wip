using log4net;
using MELLBankRestAPI.AuthModels;
using MELLBankRestAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace MELLBankRestAPI
{
    public class Program
    {
        private static readonly ILog logger = LogManager.GetLogger("Program.main method");

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Configure the EF Database Context to connect to SQL Server
            builder.Services.AddDbContext<MELLBank_EFDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CRUDConnection"));
            });

            //Inject Application Settings            
            builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));
            builder.Services.AddDbContext<AuthenticationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
            builder.Services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationContext>();

            //Register Cross Origin Resource Sharing (CORS)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.WithOrigins("http://localhost:60923")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });


            // Generate and Validate JWT for user authentication
            string tmpKeyIssuer = builder.Configuration.GetSection("ApplicationSettings:JWT_Site_URL").Value;
            string tmpKeySign = builder.Configuration.GetSection("ApplicationSettings:SigningKey").Value;
            var key = Encoding.UTF8.GetBytes(tmpKeySign);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = tmpKeyIssuer,
                    ValidAudience = tmpKeyIssuer,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            //Configure Password Requirements
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6; //12 for production
            });

            // Add services to the container.
            //builder.Services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            //});

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Configure swagger to allow adding token to the request
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization using the Bearer scheme."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new string[] {}
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowOrigin");

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}