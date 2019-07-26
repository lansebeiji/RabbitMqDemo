using GD.CoursePreparationSystem.Common;
using GD.CoursePreparationSystem.RabbitMQ;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GD.CoursePreparationSystem.Domain.Model
{
    public class MessageUtility
    {
        private static IDbConnection GetSqlConnection()
        {
            var conn = new SqlConnection(DBConfig.ConnString);
            conn.Open();
            return conn;
        }

 

      

        #region 重发消息（基于事务）

        private static readonly RabbitBaseQueue rabbitMQ;

        static MessageUtility()
        {
            rabbitMQ = IocManager.Resolve<RabbitBaseQueue>();
        }

       
        private static void ResendMessage(EJPMessage message, IDbConnection conn, IDbTransaction tran)
        {
            rabbitMQ.SendTextMessage(MessageInfos.Get(message.MessageType), message.Body);
        }

        #endregion
    }
}
