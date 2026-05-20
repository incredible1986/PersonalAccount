using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalAccount.Data;
using PersonalAccount.Data.Entities;
using PersonalAccount.Models;
using PersonalAccount.Models.Students;
using PersonalAccount.Repositories;
using PersonalAccount.Repositories.Mappers;
using PersonalAccount.Services;
using PersonalAccount.Services.Auth;
using PersonalAccount.Services.Confirmation;
using PersonalAccount.Services.Db;

namespace PersonalAccount
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    // options.AccessDeniedPath = "/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromDays(int.Parse(builder.Configuration["Auth:ExpireTimeInDays"]!));
                    options.SlidingExpiration = true;
                });
            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDefaultConnection")));

            // Services
            builder.Services.AddScoped<IStudentAuthService, StudentAuthService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IConfirmationTokenService, ConfirmationTokenService>();
            if (builder.Environment.IsDevelopment())
                builder.Services.AddScoped<DbSeeder>();
            
            // Repositories
            builder.Services.AddScoped<IStudentRepo<StudentAuthModel>, StudentRepo<StudentAuthModel>>();
            builder.Services.AddScoped<IStudentRepo<StudentModel>, StudentRepo<StudentModel>>();
            builder.Services.AddScoped<IConfirmationTokenRepo, ConfirmationTokenRepo>();
            
            // Mappers
            builder.Services.AddSingleton<IMapper<StudentEntity, StudentAuthModel>, StudentAuthMapper>();
            builder.Services.AddSingleton<IMapper<StudentEntity, StudentModel>, StudentMapper>();
            builder.Services.AddSingleton<IMapper<ConfirmationTokenEntity, ConfirmationTokenModel>, ConfirmationTokenMapper>();
            
            // Others
            builder.Services.AddSingleton<IPasswordHasher<StudentAuthModel>, PasswordHasher<StudentAuthModel>>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
                await seeder.SeedAsync();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            await app.RunAsync();
        }
    }
}
// Auth
// Personal Account


// Client -> Server -> Page -> PageModel -> Client


// Client -> Server ->  Controller -> Model
//                      Controller -> View -> Client


// MVC 
// MVVM


// Client -> Backend -> Controller -> Service -> Repository
//                      Controller -> JSON -> Client

// Client -> Frontend -> View -> Client