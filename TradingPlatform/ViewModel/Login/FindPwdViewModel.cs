using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingPlatform.SysModule;
using GalaSoft.MvvmLight.Command;
using Trading.Logic;
using Trading.Common;

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

        public async void Sure()
        {
            try
            {
                if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(CheckCode))
                {
                    var registerTask = new LoginLogic().ResetPassWord1(CheckCode, UserName, Password);
                    var timeouttask = Task.Delay(3000);
                    var completedTask = await Task.WhenAny(registerTask, timeouttask);
                    if (completedTask == timeouttask)
                    {
                        SendMsg("resetPassWord", "timeout");
                        Log4Helper.Info(this.GetType(), $"账号：{UserName}找回密码超时！时间：{DateTime.Now.ToString()}");
                    }
                    else
                    {
                        var task = await registerTask;
                        if (task.code == 200)
                        {
                            //登陆成功发送消息
                            SendMsg("resetPassWord", "OK");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                SendMsg("resetPassWord", "Error");
                Log4Helper.Error(this.GetType(), ex);
            }
        }
    }
}
