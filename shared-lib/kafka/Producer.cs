using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared_lib.kafka
{
    public abstract class Producer<K,V>
    {
        private readonly IProducer<K,V> _producer;
        public Producer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<K,V>(config).Build();
        }

        /// <summary>
        ///     Asychronously produce a message and expose delivery information
        ///     via the returned Task. Use this method of producing if you would
        ///     like to await the result before flow of execution continues.
        /// <summary>
        protected Task ProduceAsync(string topic, Message<K, V> message)
            => _producer.ProduceAsync(topic, message);

        /// <summary>
        ///     Asynchronously produce a message and expose delivery information
        ///     via the provided callback function. Use this method of producing
        ///     if you would like flow of execution to continue immediately, and
        ///     handle delivery information out-of-band.
        /// </summary>
        protected void Produce(string topic, Message<K, V> message, Action<DeliveryReport<K, V>> deliveryHandler = null)
            => _producer.Produce(topic, message, deliveryHandler);

        protected void Flush(TimeSpan timeout)
            => _producer.Flush(timeout);

    }
}
