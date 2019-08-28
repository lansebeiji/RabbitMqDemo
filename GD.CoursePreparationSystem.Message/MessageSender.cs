using GD.CoursePreparationSystem.Common;
using GD.CoursePreparationSystem.Domain.Model;
using GD.CoursePreparationSystem.Message.CourseModule;
using GD.CoursePreparationSystem.RabbitMQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.CoursePreparationSystem.Message
{
    public class MessageSender
    {
        private static readonly RabbitBaseQueue rabbitMQ = IocManager.Resolve<RabbitBaseQueue>();

        public static bool SendTestMessage(MessageContent dto)
        {
            var message = JsonConvert.SerializeObject(dto);
            try
            {
              
                rabbitMQ.SendTextMessage(MessageInfos.ERP_StockCheck_MQ, message);
                return true;
            }
            catch (Exception ex)
            {
                //MessageUtility.AddMessage(new EJPMessage()
                //{
                //    Body = message,
                //    MessageType = MessageType.ERP_StockCheck_MQ,
                //    FailedMessage = ex.Message,
                //});
                return false;
            }
        }


    }
    public class MessageContent
    {
        // 0 修改 1 删除 2 新增
        public int state { get; set; }

        public object data { get; set; }

        public string type { get; set; }
    }
}


