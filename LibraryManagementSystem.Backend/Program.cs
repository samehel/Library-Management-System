using LibraryManagementSystem.Backend.Contexts;
using LibraryManagementSystem.Backend.Services;
using LibraryManagementSystem.Backend.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization; // Import this for JsonSerializerOptions

namespace LibraryManagementSystem.Backend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<LibraryContext>(opt =>
                opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuditService, AuditService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IBackendService, BackendService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICartBookService, CartBookService>();

            // Configure JSON serialization to handle circular references
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                });

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            ConfigureAuthentication(builder);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
                await DatabaseInitializerUtil.InitializeAsync(context);
            }

            ConfigurePipeline(app, builder.Environment);

            app.Run();
        }

        private static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {

                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                    };

                    opt.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var claimsIdentity = context.Principal!.Identity as ClaimsIdentity;
                            if (claimsIdentity != null)
                            {
                                var roles = claimsIdentity.FindAll(ClaimTypes.Role).Select(c => c.Value);
                                var roleClaims = new List<Claim>();
                                foreach (var role in roles)
                                {
                                    roleClaims.Add(new Claim(ClaimTypes.Role, role));
                                }
                                var appIdentity = new ClaimsIdentity(roleClaims);
                                context.Principal.AddIdentity(appIdentity);
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }

        private static void ConfigurePipeline(WebApplication app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management System API V1");
                    c.RoutePrefix = string.Empty;

                    c.OAuthClientId("swagger-client");
                    c.OAuthClientSecret(null);
                    c.OAuthRealm(null);
                    c.OAuthAppName("Swagger UI");
                    c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
