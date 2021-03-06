﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Common;
using Trading.Common.Common;
using Trading.Logic;
using Trading.Model.Common;
using TradingPlatform.SysModule;

namespace TradingPlatform.ViewModel.Login
{
    public class RegisterViewModel: BaseViewModel
    {

        #region 字段

        private string _userName = string.Empty;
        private string _passWord = string.Empty;
        private string _checkCode = string.Empty;
        private string _nickName = string.Empty;

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

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get { return _nickName; }
            set { _nickName = value; RaisePropertyChanged(); }
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

        /// <summary>
        /// 确定
        /// </summary>
        public void Sure()
        {
            try
            {
                if (!PhoneHelper.ValidateMobile(UserName))
                {
                    SendMsg("register", "userNameError");
                    return;
                }
                if (CheckCode == "")
                {
                    SendMsg("register", "checkCodeError");
                    return;
                }
                if (Password == "")
                {
                    SendMsg("register", "passWordError");
                    return;
                }

                LoginLogic loginLogic = new LoginLogic();
                ResultModel<Token> result = loginLogic.Register(InterfacePath.Default.Register, CheckCode, UserName, NickName, Password, "", "");
                SendMsg("register", result.code.ToString());
                Log4Helper.Info(this.GetType(), $"手机号:{UserName},注册：{result.msg}");
            }
            catch (Exception ex)
            {
                SendMsg("register", "Error");
                Log4Helper.Error(this.GetType(), ex);
            }
        }
    }
}
