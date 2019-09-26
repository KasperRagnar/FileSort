using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FileSort
{
    /// <summary>
    /// Interaction logic for MessageBoxWithProgressBar.xaml
    /// </summary>
    public partial class MessageBoxWithProgressBar : Window
    {
        #region GLOBAL 
        CancellationTokenSource cts;        // A placeholder for a CancellationTokenSource from MainWindow

        #endregion 

        public MessageBoxWithProgressBar()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;     // Starts this window in the center of the screen.
        }

        #region Message Text
        public void MessageBox(string msgText, string msgHeader, CancellationTokenSource cancellationTokenSource)
        {
            Title = msgHeader;              // window Titel = message header
            textMessage.Text = msgText;     // textBox text = message text
            cts = cancellationTokenSource;  // Setter global cts = lig med local cancellationTokenSource 

            // ProgressBar

        }

        #endregion

        private void Btn_Cansel_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();   // Cansels all CancellationTokens on this thread
            this.Close();   // Closes this window
        }
    }
}
