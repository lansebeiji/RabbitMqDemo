using GD.CoursePreparationSystem.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GD.CoursePreparationSystem.Domain.Model
{
    public class MessageInfos
    {
        public static readonly RabbitMQInfo ERP_StockCheck_MQ = new RabbitMQInfo("ex.erp.inventory.stockCheck", "", "ex.erp.inventory.stockCheckQuue","");
        public static RabbitMQInfo Get(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.ERP_BaseOrg:
                    return ERP_StockCheck_MQ;
            }
            return null;
        }


    }
}
