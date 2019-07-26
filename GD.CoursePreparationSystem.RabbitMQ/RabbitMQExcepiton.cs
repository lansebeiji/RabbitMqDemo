using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GD.CoursePreparationSystem.RabbitMQ
{
    public class RabbitMQExcepiton : Exception
    {
        public RabbitMQExcepiton()
            : base()
        {
        }

        public RabbitMQExcepiton(string message)
            : base(message)
        {
        }

        public RabbitMQExcepiton(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
