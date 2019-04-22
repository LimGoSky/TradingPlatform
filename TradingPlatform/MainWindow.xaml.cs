﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trading.Common;
using Trading.Logic;
using Trading.Model.Common;

namespace TradingPlatform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoginLogic model = new LoginLogic();
            ResultModel result = model.SendMessage("15620938880", CheckCodeTypeEnum.REGISTER.ToString());
            //Log4Helper.Info(this.GetType(), "abc");
            InitializeComponent();
        }
    }
}
