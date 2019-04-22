using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

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


        private T StreamToObj(Stream stream)
        {
            string str = string.Empty;
            using (StreamReader sr = new StreamReader(stream))
            {
                str = sr.ReadToEnd();
            }
            var obj = JsonConvert.DeserializeObject<T>(str);
            return obj;
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

            var response = request.GetResponse();

            var stream = response.GetResponseStream();

            return StreamToObj(stream);
        }

        public T Post(string methods= "post")
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = methods;

            request.ContentType = "application/json";

            string _params = JsonConvert.SerializeObject(this.data);

            byte[] data = Encoding.UTF8.GetBytes(_params);

            request.ContentLength = data.Length;

            Stream requestStream = request.GetRequestStream();

            requestStream.Write(data, 0, data.Length);

            WebResponse response = request.GetResponse();

            Stream stream = response.GetResponseStream();

            return StreamToObj(stream);
        }

        public T Put()
        {
            return Post("put");
        }
    }
}
