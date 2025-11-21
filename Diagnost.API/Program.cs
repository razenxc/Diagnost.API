using Diagnost.Infrastructure;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Diagnost.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddAuthorization();

            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.Configure<BearerTokenOptions>(IdentityConstants.BearerScheme, options =>
            {
                options.BearerTokenExpiration = TimeSpan.FromDays(30);
            });

            WebApplication? app = builder.Build();

            // Apply database migrations automatically with retry while DB becomes ready
            try
            {
                using (var scope = app.Services.CreateScope())
                {
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
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Do not force HTTPS in Development container where certs are not configured
            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.MapIdentityApi<IdentityUser>();

            app.Run();
        }
    }
}
