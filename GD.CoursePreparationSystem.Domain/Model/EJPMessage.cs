using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GD.CoursePreparationSystem.Domain.Model
{
    /// <summary>
    /// 消息
    /// </summary>
    public class EJPMessage
    {
        public EJPMessage()
        {
            this.CreateTime = DateTime.Now;
        }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 消息主体
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// 失败消息
        /// </summary>
        public string FailedMessage { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }


    }
}
