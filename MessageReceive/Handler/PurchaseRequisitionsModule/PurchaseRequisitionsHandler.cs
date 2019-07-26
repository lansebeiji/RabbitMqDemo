using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageReceive
{
    public class PurchaseRequisitionsHandler : BaseMessageHandler<CourseDto>, IRabbitMessageHandler
    {
        public HandleResponse HandleRabbitMessage(string data)
        {
            var dto = ParseData(data);
            
            return HandleResponse.Success;
        }
    }
}

