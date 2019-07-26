using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace GD.CoursePreparationSystem.RabbitMQ
{
    public class ConnectionPool : IDisposable
    {
        private const int DefaultPoolSize = 10;

        private readonly Func<IConnection> createConnectionFunc;

        private readonly ConcurrentQueue<IConnection> _pool = new ConcurrentQueue<IConnection>();
        private int _count;

        private int _maxSize;

        public ConnectionPool(RabbitMQOptions options)
        {
            _maxSize = DefaultPoolSize;

            createConnectionFunc = CreateConnection(options);
        }

        public void Dispose()
        {
            _maxSize = 0;

            IConnection context;
            while (_pool.TryDequeue(out context))
                context.Dispose();
        }

        private static Func<IConnection> CreateConnection(RabbitMQOptions options)
        {
            var factory = new ConnectionFactory
            {
                RequestedConnectionTimeout = options.RequestedConnectionTimeout,
                AutomaticRecoveryEnabled = true,
            };
            return () =>
                {
                    int length = options.Uris.Length;
                    for (int i = 0; i < length; i++)
                    {
                        factory.Uri = new Uri(options.Uris[i]);
                       // factory.VirtualHost = options.VirtualHost;
                        try
                        {
                            return factory.CreateConnection();
                        }
                        catch(Exception ex)
                        {
                            if (i == length)
                                throw new RabbitMQExcepiton("CreateConnection异常！");
                        }
                    }
                    return null;
                };
        }

        public virtual IConnection GetConnection()
        {
            IConnection connection;
            if (_pool.TryDequeue(out  connection))
            {
                Interlocked.Decrement(ref _count);

                Debug.Assert(_count >= 0);

                return connection;
            }

            connection = createConnectionFunc();

            return connection;
        }

        /// <summary>
        /// 归还mq连接 ，返回False，则释放该连接
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public virtual bool ReturnConnection(IConnection connection)
        {
            if (Interlocked.Increment(ref _count) <= _maxSize)
            {
                _pool.Enqueue(connection);

                return true;
            }

            Interlocked.Decrement(ref _count);

            Debug.Assert(_maxSize == 0 || _pool.Count <= _maxSize);

            return false;
        }
    }
}
