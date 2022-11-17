using shared_lib;
using data_collector.Models;
using Newtonsoft.Json;
using Confluent.Kafka;
using shared_lib.kafka;

namespace data_collector.Services
{
    public class ActivationProducer : Producer<string,string>
    {
        public ActivationProducer(ProducerConfig config): base(config){}

        public async Task Submit(Guid accountId, Activation activation)
        {
            var msg = new Confluent.Kafka.Message<string,string>()
            {
                Key = accountId.ToString(),
                Value = JsonConvert.SerializeObject(activation)
            };
            await ProduceAsync("activations", msg);
        }
    }
}
