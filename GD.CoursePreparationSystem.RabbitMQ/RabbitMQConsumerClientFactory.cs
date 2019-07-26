using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.CoursePreparationSystem.RabbitMQ
{
    public class RabbitMQConsumerClientFactory
    {
        private readonly ConnectionPool _connectionPool;
        private readonly RabbitMQOptions _rabbitMQOptions;

        public RabbitMQConsumerClientFactory(RabbitMQOptions rabbitMQOptions, ConnectionPool pool)
        {
            _rabbitMQOptions = rabbitMQOptions;
            _connectionPool = pool;
        }

        public RabbitMQConsumerClient Create(string queueName, string exchangeName, string exchangeType)
        {
            return new RabbitMQConsumerClient(queueName, exchangeName, exchangeType, _connectionPool, _rabbitMQOptions);
        }
    }
}
