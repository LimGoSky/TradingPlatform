using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;

namespace Trading.Common
{
    public sealed class HttpUtility<T>
    {
        /// <summary>
        /// 超时
        /// </summary>
        private int timeout = 0;

        private string url;

        private object data;

        private HttpUtility(string url, object data, int timeout = 5000)
        {
            this.url = url;
            this.data = data;
            this.timeout = timeout;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="timeout">请求超时</param>
        public static HttpUtility<T> Factory(string url, object data, int timeout = 5000)
        {
            return new HttpUtility<T>(url, data, timeout);
        }


        public T Get()
        {
            PropertyInfo[] ps = data.GetType().GetProperties();

            if (ps.Length > 0)
            {
                url += "?";
                foreach (var item in ps)
                {
                    url += item.Name + "=" + item.GetValue(data) + "&";
                }
            }


            WebRequest request = WebRequest.Create(url);
            request.Method = "get";

            return default(T);

        }
    }
}
