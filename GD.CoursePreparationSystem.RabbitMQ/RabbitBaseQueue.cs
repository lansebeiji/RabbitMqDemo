using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace GD.CoursePreparationSystem.RabbitMQ
{
    public class RabbitBaseQueue
    {
        private readonly ConnectionPool _connectionPool;
        public RabbitBaseQueue(ConnectionPool connectionPool)
        {
            this._connectionPool = connectionPool;
        }
        public void SendTextMessage(RabbitMQInfo info, string message)
        {
            var connection = _connectionPool.GetConnection();
            if (connection == null)
                throw new RabbitMQExcepiton("获取connection失败！");
            using (var channel = connection.CreateModel())
            {
                var properties = channel.CreateBasicProperties();
                properties.ContentType = "application/json";
                properties.MessageId = Guid.NewGuid().ToString();
                var body = Encoding.UTF8.GetBytes(message);
                //发送消息
                channel.BasicPublish(info.ExchangeName, ""/*info.RoutingKey*/, properties, body); //暂时不取routingkey
            }
            if (!_connectionPool.ReturnConnection(connection))
                connection.Dispose();
        }
    }
}
