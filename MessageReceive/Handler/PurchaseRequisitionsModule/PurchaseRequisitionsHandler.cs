using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageReceive
{
    public class PurchaseRequisitionsHandler : BaseMessageHandler<MessageContent>, IRabbitMessageHandler
    {
        public HandleResponse HandleRabbitMessage(string data)
        {
            var dto = ParseData(data);
            ///此处接收到消息后 根据消息的类型 判断是不同的模块 然后调用不同的类来处理不同的业务逻辑。
            var dj = JsonConvert.DeserializeObject(dto.data.ToString(), dto.type);
           //  根据 dto.type.Name 来判断所属不同模块
            return HandleResponse.Success;
        }
    }

    public class MessageContent
    {
        // 0 修改 1 删除 2 新增
        public int state { get; set; }

        public object data { get; set; }

        public Type type { get; set; }
    }
}

