using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using RabbitMQClient = RabbitMQ.Client;

namespace GD.CoursePreparationSystem.RabbitMQ
{
    public class RabbitMQOptions
    {
        public const int DefaultConnectionTimeout = 30 * 1000;

        public RabbitMQOptions()
        {
            this.Uri = ConfigurationManager.AppSettings["RabbitMQ_Uri"] ?? "amqp://admin:123456@197.168.12.195/";
            this.VirtualHost = ConfigurationManager.AppSettings["RabbitMQ_VirtualHost"] ?? "/production";
            this.Uris = Uri.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            string requestedConnectionTimeoutValue = ConfigurationManager.AppSettings["RabbitMQ_RequestedConnectionTimeout"];
            int requestedConnectionTimeout;
            if (int.TryParse(requestedConnectionTimeoutValue, out requestedConnectionTimeout))
                this.RequestedConnectionTimeout = requestedConnectionTimeout;
            else
                this.RequestedConnectionTimeout = DefaultConnectionTimeout;
        }

        public string[] Uris { get; set; }
        internal string Uri { get; set; }

        public IList<AmqpTcpEndpoint> Endpoints
        {
            get
            {
                return this.Uris.Select(uri => new AmqpTcpEndpoint(new Uri(uri))).ToList();
            }
        }

        public string VirtualHost { get; set; }
        public int RequestedConnectionTimeout { get; set; }
    }
}
