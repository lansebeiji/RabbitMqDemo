using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GD.CoursePreparationSystem.RabbitMQ
{
    /// <summary>
    /// MQ消息信息
    /// </summary>
    public class RabbitMQInfo
    {
        public RabbitMQInfo(string exchangeName, string exchangeType, string queueName, string routingKey)
        {
            ExchangeName = exchangeName;
            ExchangeType = exchangeType;
            QueueName = queueName;
            RoutingKey = routingKey;
        }

        public RabbitMQInfo(string exchangeName, string routingKey)
            : this()
        {
            ExchangeName = exchangeName;
            RoutingKey = routingKey;
        }
        public RabbitMQInfo()
        {
            ExchangeType = "fanout";
        }

        public string ExchangeType { get; set; }

        public string ExchangeName { get; set; }

        public string RoutingKey { get; set; }

        public string QueueName { get; set; }
    }
}
