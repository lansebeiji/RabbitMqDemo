using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageReceive
{
    public class MessageSyncAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PurchaseRequisitionsHandler>().Named<IRabbitMessageHandler>(MessageType.RabbitMQ_PurchaseRequisitions.ToString());
        }
    }
}
