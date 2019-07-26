using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageReceive
{
    public abstract class BaseMessageHandler<T>
    {
        public virtual T ParseData(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch (Newtonsoft.Json.JsonSerializationException ex)
            {
                throw new Exception("反序列化异常！", ex);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                throw new Exception("反序列化异常！", ex);
            }
        }
    }
}
