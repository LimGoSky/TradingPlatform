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

namespace Trading.Demo
{
    using System.Threading;
    using Trading.Common;
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.btn1.IsEnabled = false;

            //耗时长或有阻塞情况的不用线程池中的线程
            //ThreadPool.QueueUserWorkItem(fn => {
            //    Connect();
            //});
            try
            {
                Task task = Task.Factory.StartNew(() => {
                    var dic = new Dictionary<string, string>();
                    //dic.Add("sub-1", "/topic/latest_quotation_detail_CL1906");
                    //dic.Add("sub-2", "/topic/ask_bid_CL1906");
                    dic.Add("sub-1", "/topic/test");

                    WebSocketUtility ws = WebSocketUtility.Create("ws://market.future.alibaba.com/webSocket/zd/market", dic);
                    ws.Connect(onMessage);
                }, TaskCreationOptions.LongRunning);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        void onMessage(string data)
        {
            this.txtBox.Dispatcher.Invoke(new Action(() => { this.txtBox.Text = data; }));
        }
    }
}
