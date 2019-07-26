using RabbitMQClient = RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageReceive
{
    public class MessageSyncTypeMapInfo
    {
        public static MessageSyncTypeMapInfo PurchaseRequisitions = new MessageSyncTypeMapInfo("ex.erp.inventory.stockCheckQuue", "ex.erp.inventory.stockCheck", RabbitMQClient.ExchangeType.Fanout, "");
        public MessageSyncTypeMapInfo() { }
        public MessageSyncTypeMapInfo(string queueName, string exchangeName, string exchangeType, params string[] rountingKeys)
        {
            this.QueueName = queueName;
            this.ExchangeName = exchangeName;
            this.ExchangeType = exchangeType;
            this.RountingKeys = rountingKeys;
        }
        public string QueueName { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }
        public string[] RountingKeys { get; set; }
    }
    public enum MessageType
    {
        RabbitMQ_PurchaseRequisitions,
    }
}
