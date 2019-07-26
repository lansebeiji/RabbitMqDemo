using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GD.CoursePreparationSystem.RabbitMQ
{
    public class RabbitMQConsumerClient : IDisposable
    {
        private readonly ConnectionPool _connectionPool;
        private readonly string _exchangeName;
        private readonly string _queueName;
        private readonly string _exchangeType;
        private readonly RabbitMQOptions _rabbitMQOptions;

        private IModel _channel;
        private ulong _deliveryTag;

        public RabbitMQConsumerClient(string queueName,
            string exchangeName,
            string exchangeType,
            ConnectionPool connectionPool,
            RabbitMQOptions options)
        {
            _queueName = queueName;
            _connectionPool = connectionPool;
            _rabbitMQOptions = options;
            _exchangeName = exchangeName;
            _exchangeType = exchangeType;

            InitClient();
        }

        public event EventHandler<BasicDeliverEventArgs> OnMessageReceived;

        public event EventHandler<ShutdownEventArgs> OnError;

        public void Subscribe(params string[] topics)
        {
            //if (topics == null) throw new ArgumentNullException("topics");

            if (!string.IsNullOrWhiteSpace(this._exchangeName))
            {
                if (topics != null)
                {
                    foreach (var topic in topics)
                        _channel.QueueBind(_queueName, _exchangeName, topic);
                }
            }
        }

        public void Listening(TimeSpan timeout, CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += OnConsumerReceived;
            consumer.Shutdown += OnConsumerShutdown;
            _channel.BasicConsume(_queueName, false, consumer);
            while (true)
                Task.Delay(timeout, cancellationToken).GetAwaiter().GetResult();
        }

        /// <summary>
        /// ack
        /// </summary>
        public void Commit()
        {
            _channel.BasicAck(_deliveryTag, false);
        }

        public void Reject()
        {
            _channel.BasicReject(_deliveryTag, true);
        }

        public void Dispose()
        {
            _channel.Dispose();
        }

        private void InitClient()
        {
            var connection = _connectionPool.GetConnection();

            if (connection == null)
                throw new RabbitMQExcepiton("获取connection失败！");

            _channel = connection.CreateModel();

            _channel.BasicQos(0, 1, false);

            //if (!string.IsNullOrWhiteSpace(_exchangeName))
            //    _channel.ExchangeDeclare(
            //        _exchangeName,
            //        _exchangeType,
            //        true);

            //_channel.QueueDeclare(_queueName,
            //    true,
            //    false,
            //    false,
            //    null);

            _connectionPool.ReturnConnection(connection);
        }

        private void OnConsumerReceived(object sender, BasicDeliverEventArgs e)
        {
            _deliveryTag = e.DeliveryTag;
            if (OnMessageReceived != null)
                OnMessageReceived.Invoke(sender, e);
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            if (OnError != null)
                OnError.Invoke(sender, e);
        }
    }
}
