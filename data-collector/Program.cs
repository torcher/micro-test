using Confluent.Kafka;
using data_collector.Services;

namespace data_collector
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ProducerConfig kafkaProducerConfig = new ProducerConfig() { BootstrapServers = "<<KAFKA SERVER IP:PORT>" };
            builder.Services.AddSingleton(new ActivationProducer(kafkaProducerConfig));

            builder.Services.AddControllers();
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