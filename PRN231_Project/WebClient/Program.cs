using CoFAB.Business.IRepository;
using CoFAB.Business.Repository;
using CoFAB.DataAccess.Models;

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
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}