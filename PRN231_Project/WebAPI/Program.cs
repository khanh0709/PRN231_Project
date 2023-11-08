using WebAPI.Business.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI.Business.IRepository;
using WebAPI.Business.Repository;
using WebAPI.DataAccess.Models;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //DI
            builder.Services.AddTransient<IAttempRepository, AttempRepository>()
               .AddDbContext<CoFABContext>(opt => builder.Configuration.GetConnectionString("DB"));
            builder.Services.AddTransient<IUserRepository, UserRepository>()
                .AddDbContext<CoFABContext>(opt => builder.Configuration.GetConnectionString("DB"));
            builder.Services.AddTransient<IMatchRepository, MatchRepository>()
                .AddDbContext<CoFABContext>(opt => builder.Configuration.GetConnectionString("DB"));
            builder.Services.AddTransient<IRoundRepository, RoundRepository>()
                .AddDbContext<CoFABContext>(opt => builder.Configuration.GetConnectionString("DB"));
            builder.Services.AddTransient<ITournamentRepository, TournamentRepository>()
                .AddDbContext<CoFABContext>(opt => builder.Configuration.GetConnectionString("DB"));
            builder.Services.AddTransient<ITypeRepository, TypeRepository>()
                .AddDbContext<CoFABContext>(opt => builder.Configuration.GetConnectionString("DB"));
            builder.Services.AddTransient<IFormatRepository, FormatRepository>()
                .AddDbContext<CoFABContext>(opt => builder.Configuration.GetConnectionString("DB"));
            //add cor
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });


            builder.Services.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
               {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
               });
            });

            //config jwt
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
            var secretKey = builder.Configuration["AppSettings:SecretKey"];
            var secretKeyByte = Encoding.UTF8.GetBytes(secretKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),
                    ClockSkew = TimeSpan.Zero
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}