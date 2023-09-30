global using ConcentraTest.Models;
using ConcentraTest.Initializer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ConcentraTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = new ConfigurationBuilder()
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json")
            .Build();

            // Configurar la autenticación basada en tokens Bearer
            var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:SecretKey"]); // Reemplaza con tu clave secreta
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddDbContext<ConcentraContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("connection"))
            );

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var myContext = scope.ServiceProvider.GetRequiredService<ConcentraContext>();
                bool canConnectToDatabase = myContext.Database.CanConnect();

                if (!canConnectToDatabase)
                {
                    myContext.Database.Migrate();
                    myContext.SaveChanges();

                    AppDBInitializer.Seed(app);
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}