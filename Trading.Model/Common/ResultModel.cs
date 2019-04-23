using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Model.Common
{
    /// <summary>
    /// 返回结果实体
    /// </summary>
    public class ResultModel<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回对象
        /// </summary>
        public T data { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
    }

    /// <summary>
    /// 返回结果实体
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回对象
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
    }

    public class Token
    {
        public string token { get; set; }
    }
}
