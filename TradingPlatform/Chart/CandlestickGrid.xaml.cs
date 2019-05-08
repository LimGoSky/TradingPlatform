using System;
using System.Collections.Generic;
using System.IO;
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
using Trading.Demo;

namespace TradingPlatform.Chart
{
    /// <summary>
    /// Candlestick.xaml 的交互逻辑
    /// </summary>
    public partial class CandlestickGrid : Grid
    {
        public CandlestickGrid()
        {
            InitializeComponent();
        }

        public List<StockInfo> Data { get; set; }


        String stockName;
        private List<StockInfo> LoadStockInfo(string fileName)
        {
            using (Stream resourceStream = new FileStream(fileName, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(resourceStream, Encoding.GetEncoding("GB2312")))
                {
                    //读每一行
                    var strings = reader.ReadToEnd().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    //获取股票名称
                    stockName = strings[0].Replace("\r", "");

                    var res = new List<StockInfo>(strings.Length - 2);

                    //第一行是股票名称, 第二行是类型名称, 第3行才是股票数据
                    for (int i = 2; i < strings.Length; i++)
                    {
                        string line = strings[i];
                        string[] subLines = line.Split('\t');

                        DateTime date = DateTime.Parse(subLines[0]);
                        Double open = Double.Parse(subLines[1]);
                        Double high = Double.Parse(subLines[2]);
                        Double low = Double.Parse(subLines[3]);
                        Double close = Double.Parse(subLines[4]);
                        Double volumn = Double.Parse(subLines[5]);

                        res.Add(
                            new StockInfo
                            {
                                date = date,
                                open = open,
                                high = high,
                                low = low,
                                close = close,
                                volume = volumn
                            });
                    }
                    return res;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            LoadData("./SH600747.TXT");
        }

        private void LoadData(string path)
        {

            Data = LoadStockInfo(path);
            //stockChart.Charts[0].Graphs[0].Title = stockName;
            //stockChart.Charts[1].Graphs[0].Title = stockName;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            var obj = e.GetPosition((IInputElement)sender);

            var x1 = 0;
            var x2 = this.Grid.ActualWidth;

            var y1 = obj.Y;
            var y2 = obj.Y;

            this.xLine.X1 = 0;
            this.xLine.X2 = x2;
            this.xLine.Y1 = y1;
            this.xLine.Y2 = y2;

            this.yLine.X1 = obj.X;
            this.yLine.Y1 = 0;

            this.yLine.X2 = obj.X;
            this.yLine.Y2 = this.Grid.ActualHeight;
        }
    }
}
