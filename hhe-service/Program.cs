using Confluent.Kafka;
using hhe_service.Data;
using hhe_service.Services;

namespace hhe_service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConsumerConfig config = new ConsumerConfig()
            {
                BootstrapServers = "<KAFKA SERVER IP:PORT>",
                GroupId = "foo"
            };
            builder.Services.AddSingleton(config);
            builder.Services.AddDbContext<HHEDbContext>();
            builder.Services.AddScoped<IHHEService, HHEService>();
            builder.Services.AddHostedService<ActivationConsumer>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}