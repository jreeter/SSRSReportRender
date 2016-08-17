using SSRSReporting;
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

namespace ReportTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
           var result = await Task.Run(() => GetReport());
           using (Stream pdf = new MemoryStream(result))
           {
               ReportViewer.LoadDocument(pdf);
           }
        }

        private byte[] GetReport()
        {
            SSRSReportClient rc = new SSRSReportClient("http://phxssrs/reportserver/reportexecution2005.asmx", "http://phxssrs/reportserver/reportservice2010.asmx", System.Net.CredentialCache.DefaultCredentials);
            return rc.ExecuteReport("/Helios Reports/ActiveDus", "PDF");
        }
    }
}
