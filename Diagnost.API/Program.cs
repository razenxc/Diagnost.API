using Diagnost.Domain;
using Diagnost.Domain.Interfaces;
using Diagnost.Infrastructure;
using Diagnost.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Diagnost.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (builder.Configuration["DatabaseProvider"] == "MSSQL")
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
                else if (builder.Configuration["DatabaseProvider"] == "PostgreSQL")
                {
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
            });

            builder.Services.AddAuthorization();

            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = false;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
            });

            builder.Services.AddCors();
            
            builder.Services.AddScoped<IAccessCodeService, AccessCodeService>();
            builder.Services.AddScoped<IResultService, ResultService>();

            WebApplication? app = builder.Build();

            // Apply database migrations automatically with retry while DB becomes ready
            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    IServiceProvider services = scope.ServiceProvider;
                    ApplicationDbContext? db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    const int maxAttempts = 30;
                    const int delayMs = 2000;
                    int attempt = 0;
                    bool connected = false;

                    while (attempt < maxAttempts && !connected)
                    {
                        attempt++;
                        try
                        {
                            if (db.Database.CanConnect())
                            {
                                connected = true;
                                db.Database.Migrate();
                                Console.WriteLine("Database connected and migrations applied.");
                                await DbInit.InitializeAsync(services);
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Database connect attempt {attempt} failed: {ex.Message}");
                        }

                        Console.WriteLine($"Waiting for database to become available... Attempt {attempt}/{maxAttempts}");
                        Thread.Sleep(delayMs);
                    }

                    if (!connected)
                    {
                        Console.WriteLine("Could not connect to the database after multiple attempts. Continuing without applying migrations.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database migration failed: {ex.Message}");
                throw;
            }

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{}
            app.UseSwagger();
            app.UseSwaggerUI();

            // Do not force HTTPS in Development container where certs are not configured
            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseCors(x => x
                .WithOrigins("http://localhost:7169", "https://localhost:7169")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
