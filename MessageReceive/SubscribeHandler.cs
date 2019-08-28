using GD.CoursePreparationSystem.Common;
using GD.CoursePreparationSystem.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageReceive
{
    public class SubscribeHandler
    {
        private static readonly RabbitMQConsumerClientFactory clientFactory = IocManager.Resolve<RabbitMQConsumerClientFactory>();
        private static Dictionary<string, MessageSyncTypeMapInfo> queueTypeMap = new Dictionary<string, MessageSyncTypeMapInfo>();
        private static CancellationTokenSource _cts;
        private static Task _compositeTask;
        private static bool running;
        private static readonly TimeSpan _pollingDelay = TimeSpan.FromMilliseconds(10);
        static SubscribeHandler()
        {
            running = true;
            InitMapData();
        }
        /// <summary>
        /// 映射 新消息队列名称 与 旧消息类型
        /// </summary>
        private static void InitMapData()
        {
            queueTypeMap.Add(MessageType.RabbitMQ_PurchaseRequisitions.ToString(), MessageSyncTypeMapInfo.PurchaseRequisitions);
        }
        public string Listening()
        {
            string strMessage = "";
            _cts = new CancellationTokenSource();
            running = true;
            foreach (var type in queueTypeMap.Keys)
            {
                Task.Factory.StartNew(() =>
                {
                    MessageSyncTypeMapInfo mapInfo;
                    queueTypeMap.TryGetValue(type, out mapInfo);
                    using (var client = clientFactory.Create(mapInfo.QueueName, mapInfo.ExchangeName, mapInfo.ExchangeType))
                    {
                        client.OnMessageReceived += (sender, e) =>
                        {
                            HandleResponse response = null;
                            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                            watch.Start();
                            string message = System.Text.Encoding.UTF8.GetString(e.Body);
                            try
                            {
                                IRabbitMessageHandler handler = IocManager.Resolve<IRabbitMessageHandler>(type);
                                response = handler.HandleRabbitMessage(message);
                                //strMessage += message;
                                Loger.Write(message);
                            }

                            catch (Exception ex)
                            {
                                response = new HandleResponse(HandleResult.失败, ex.Message);
                                response.Exception = ex;
                            }

                            switch (response.Result)
                            {
                                case HandleResult.成功:
                                case HandleResult.不处理:
                                    break;
                                default:
                                    //包装为原消息同步接口格式，可以通过老工具进行二次处理

                                    break;
                            }
                            //不管成功还是失败，都发送ack， 异常消息手动处理。
                            client.Commit();

                        };
                        client.OnError += (sender, e) =>
                        {
                        };
                        client.Subscribe(mapInfo.RountingKeys);
                        client.Listening(_pollingDelay, _cts.Token);
                    }
                }, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default).ContinueWith(task =>
                {
                    if (task.Exception != null && task.Exception.InnerExceptions != null & task.Exception.InnerExceptions.Count > 0)
                    {
                        var innerException = task.Exception.InnerExceptions[0];
                    }
                },
                TaskContinuationOptions.OnlyOnFaulted);
            }
            _compositeTask = Task.WhenAll();
            return strMessage;
        }

        public void Stop()
        {
            if (!running)
                return;
            running = false;
            _cts.Cancel();
            try
            {
                _compositeTask.Wait(TimeSpan.FromSeconds(10));
            }
            catch (AggregateException ex)
            {
                var innerEx = ex.InnerExceptions[0];
            }
        }
    }
}
