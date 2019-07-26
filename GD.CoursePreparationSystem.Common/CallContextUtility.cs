using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GD.CoursePreparationSystem.Common
{
    public class CallContextUtility
    {
        public static T GetData<T>() where T : class
        {
            T obj = CallContext.GetData(typeof(T).Name) as T;
            return obj;
        }
        public static T GetData<T>(string name) where T : class
        {
            T obj = CallContext.GetData(name) as T;
            return obj;
        }

        public static void SetData<T>(string name, T data) where T : class
        {
            CallContext.SetData(name, data);
        }

        public static void SetData<T>(T data) where T : class
        {
            CallContext.SetData(typeof(T).Name, data);
        }

        public static void ClearData(string name)
        {
            CallContext.FreeNamedDataSlot(name);
        }
        public static void ClearData<T>()
        {
            CallContext.FreeNamedDataSlot(typeof(T).Name);
        }

    }
}
