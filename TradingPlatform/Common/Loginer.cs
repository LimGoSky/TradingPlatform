using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingPlatform.Common
{
    /// <summary>
    /// 登录用户信息
    /// </summary>
    public class Loginer: ViewModelBase
    {
        private Loginer() { }
        private static Loginer _Loginer = new Loginer(); //饿汉式单例

        /// <summary>
        /// 当前用户
        /// </summary>
        public static Loginer LoginerUser
        {
            get { return _Loginer; }
        }

        private string _UserId = string.Empty;
        private string _NickName = string.Empty;
        private string _ProFilePhoto = string.Empty;
        private string _CreateTime = string.Empty;
        private string _Token = string.Empty;
        private string _GeneralParam = string.Empty;

        /// <summary>
        /// 登录名
        /// </summary>
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string NickName
        {
            get
            {
                return _NickName;
            }
            set { _NickName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 照片
        /// </summary>
        public string ProFilePhoto
        {
            get { return _ProFilePhoto; }
            set { _ProFilePhoto = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 照片
        /// </summary>
        public string CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Token
        /// </summary>
        public string Token
        {
            get { return _Token; }
            set { _Token = value; RaisePropertyChanged(); }
        }

        public string GeneralParam
        {
            get { return _GeneralParam; }
            set { _GeneralParam = value;RaisePropertyChanged(); }
        }
    }
}
