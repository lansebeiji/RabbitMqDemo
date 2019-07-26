using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace GD.CoursePreparationSystem.RabbitMQ
{
    public class RabbitMQAutofacModule : Autofac.Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<RabbitMQOptions>().AsSelf();
            builder.RegisterType<ConnectionPool>().AsSelf().SingleInstance();
            builder.RegisterType<RabbitBaseQueue>().AsSelf().SingleInstance();
            builder.RegisterType<RabbitMQConsumerClientFactory>().AsSelf().SingleInstance();
        }
    }
}
