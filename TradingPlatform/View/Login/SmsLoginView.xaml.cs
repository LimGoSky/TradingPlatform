using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TradingPlatform.View.Login
{
    /// <summary>
    /// SmsLoginView.xaml 的交互逻辑
    /// </summary>
    public partial class SmsLoginView : Page
    {
        public SmsLoginView()
        {
            InitializeComponent();
        }

        private void tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Regex re = new Regex("[^0-9.-]+");

            e.Handled = re.IsMatch(e.Text);

        }

        /// <summary>
        /// 祖册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterView v = new RegisterView();
            NavigationService.Navigate(v);
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSure_Click(object sender, RoutedEventArgs e)
        {
            var windowCollection = Application.Current.Windows;
            foreach (var item in windowCollection)
            {
                if (item.GetType() == typeof(LoginMainView))
                {
                    Window w = (Window)item;
                    MainWindow m = new MainWindow();
                    m.Show();
                    w.Close();
                }
            }
        }
    }
}
