using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Common
{
    public class JsonHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ToJson<T>(T obj)
        {
           return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="strJosn">json字符串</param>
        /// <returns></returns>
        public static T JsonToObj<T>(string strJosn)
        {
            return JsonConvert.DeserializeObject<T>(strJosn);
        }
    }

}
