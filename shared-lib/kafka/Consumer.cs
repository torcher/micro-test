using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared_lib.kafka
{
    public class Consumer<K,V>
    {
        private readonly IConsumer<K,V> _consumer;
        private readonly string _topic;
        public Consumer(ConsumerConfig config, string topic)
        {
            _consumer = new ConsumerBuilder<K,V>(config).Build();
            _topic = topic;
            _consumer.Subscribe(_topic);
        }

        public V read()
        {
            var result = _consumer.Consume();
            return result.Message.Value;
        }
    }
}
