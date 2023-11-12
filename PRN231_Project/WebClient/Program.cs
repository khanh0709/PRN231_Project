using WebAPI.Business.IRepository;
using WebAPI.Business.Repository;
using WebAPI.DataAccess.Models;
using WebClient.Helper;

namespace WebClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors();
            builder.Services.AddRazorPages();
            builder.Services.AddSession();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5184/")

            });

            builder.Services.AddScoped<APIHelper>();
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}