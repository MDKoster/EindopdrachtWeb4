
using RestaurantOpdracht_BL.Interfaces;
using RestaurantOpdracht_BL.Managers;
using RestaurantOpdracht_EF.Repositories;

namespace REST_API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IKlantRepository, KlantRepository>();
            builder.Services.AddSingleton<IReservatieRepository, ReservatieRepository>();
            builder.Services.AddSingleton<IRestaurantRepository, RestaurantRepository>();
            builder.Services.AddSingleton<KlantManager>();
            builder.Services.AddSingleton<RestaurantManager>();
            builder.Services.AddSingleton<ReservatieManager>();
            builder.Services.AddControllers();

            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}