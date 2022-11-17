

using Confluent.Kafka;
using hhe_service.Models;
using Newtonsoft.Json;
using shared_lib.kafka;

namespace hhe_service.Services
{
    public class ActivationConsumer : BackgroundService
    {
        IServiceScopeFactory _serviceScopeFactory;
        private readonly Consumer<string,string> _consumer;
        public ActivationConsumer(IServiceScopeFactory serviceScopeFactory, ConsumerConfig config)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _consumer = new Consumer<string, string>(config, "activations");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Not ideal for production code. What if thread stops, for example
            //Better solution would be to have a 3rd micro service for consuming the queue
            var thread = new Thread(() => WaitForAndSaveMessage(stoppingToken));
            thread.Start();
        }

        private void WaitForAndSaveMessage(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var hheService = scope.ServiceProvider.GetRequiredService<IHHEService>();
                    var message = _consumer.read();
                    try
                    {
                        Activation activation = JsonConvert.DeserializeObject<Activation>(message);
                        var save = hheService.AddActivation(activation);
                        save.Wait();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
