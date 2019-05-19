using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingPlatform.SysModule;
using GalaSoft.MvvmLight.Command;
using Trading.Logic;
using Trading.Common;
using Trading.Common.Common;

namespace TradingPlatform.ViewModel.Login
{
    public class FindPwdViewModel: BaseViewModel
    {
        #region 字段

        private string _userName = string.Empty;
        private string _passWord = string.Empty;
        private string _checkCode = string.Empty;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _passWord; }
            set { _passWord = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 短信验证码
        /// </summary>
        public string CheckCode
        {
            get { return _checkCode; }
            set { _checkCode = value; RaisePropertyChanged(); }
        }

        #endregion

        #region 命令(Binding Command)

        private RelayCommand _sureCommand;

        public RelayCommand SureCommand
        {
            get
            {
                if (_sureCommand == null)
                {
                    _sureCommand = new RelayCommand(() => Sure());
                }
                return _sureCommand;
            }
        }

        #endregion

        public void Sure()
        {
            try
            {
                if (!PhoneHelper.ValidateMobile(UserName))
                {
                    SendMsg("resetPassWord", "userNameError");
                    return;
                }
                if (CheckCode == "")
                {
                    SendMsg("resetPassWord", "checkCodeError");
                    return;
                }
                if (Password == "")
                {
                    SendMsg("resetPassWord", "passWordError");
                    return;
                }

                var result = new LoginLogic().ResetPassWord(CheckCode, UserName, Password);
                SendMsg("resetPassWord", result.code.ToString());
                Log4Helper.Info(this.GetType(), $"手机号:{UserName},找回密码：{result.msg}");
            }
            catch (Exception ex)
            {
                SendMsg("resetPassWord", "Error");
                Log4Helper.Error(this.GetType(), ex);
            }
        }
    }
}
