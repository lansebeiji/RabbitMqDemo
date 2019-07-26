using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageReceive
{
    public class HandleResponse
    {
        public static readonly HandleResponse Success = new HandleResponse(HandleResult.成功);
        public static readonly HandleResponse UnHandle = new HandleResponse(HandleResult.不处理);
        public static readonly HandleResponse Failure = new HandleResponse(HandleResult.失败);

        public HandleResponse() { }
        public HandleResponse(HandleResult result, string message = null, object data = null)
        {
            this.Result = result;
            this.Message = message;
            this.Data = data;
        }
        public HandleResult Result { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public enum HandleResult
    {
        失败 = 0,
        成功 = 1,
        不处理 = 2
    }
}

