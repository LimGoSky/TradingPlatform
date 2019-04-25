using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingPlatform.SysModule;

namespace TradingPlatform.ViewModel.Login
{
    public class LoginViewModel: BaseHostDialogOperation
    {
        #region 用户名/密码

        private string _Report;
        private string userName = string.Empty;
        private string passWord = string.Empty;
        private bool _IsCancel = true;
        private bool _UserChecked;
        private string _SkinName;

        /// <summary>
        /// 皮肤样式
        /// </summary>
        public string SkinName
        {
            get { return _SkinName; }
        }

        /// <summary>
        /// 进度报告
        /// </summary>
        public string Report
        {
            get { return _Report; }
            set { _Report = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 记住密码
        /// </summary>
        public bool UserChecked
        {
            get { return _UserChecked; }
            set { _UserChecked = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 禁用按钮
        /// </summary>
        public bool IsCancel
        {
            get { return _IsCancel; }
            set { _IsCancel = value; RaisePropertyChanged(); }
        }

        #endregion
    }
}
