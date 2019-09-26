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
using Repository;

namespace FileSort
{
    /// <summary>
    /// Interaction logic for MessageBoxWithProgressBar.xaml
    /// </summary>
    public partial class MessageBoxWithProgressBar : Window
    {
        #region GLOBAL 
        CancellationTokenSource cts = new CancellationTokenSource();        // CancellationTokenSource
        CancellationToken ct;                                               // CancellationToken

        string destinationPathFolder;                                       // A placeholder for the users chosen destination folder path
        string[] FoundFielsFromSearch;                                      // A placeholder for a string array of paths to files that match the users search

        int sortingMethodOfChoice;
        #endregion

        public MessageBoxWithProgressBar()
        {
            // default constructor
        }

        public MessageBoxWithProgressBar(int sortingMethod, string msgText, string msgHeader, string destPathFolder, string[] filesFoundInSearch)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;     // Starts this window in the center of the screen.
            ct = cts.Token;                                                 // CancellationToken is set
            

            Title = msgHeader;                              // Window Titel = message header
            textMessage.Text = msgText;                     // TextBox text = message text
            destinationPathFolder = destPathFolder;         // destPathFolder = users chosen destination folder path
            FoundFielsFromSearch = filesFoundInSearch;      // filesFoundInSearch = A string array of paths to files that match the users search
            sortingMethodOfChoice = sortingMethod;

            DataImport(new SortingMethods());               // Starter En progressbar sammen med den valgte metode
        }

        #region Buttons 
        /// <summary>
        /// A cansel button that closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cansel_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();   // Cansels all CancellationTokens on this thread
            this.Close();   // Closes this window
        }

        #endregion

        #region Progress 
        /// <summary>
        /// Checks what method the user have chousen and call the chousen method
        /// </summary>
        /// <param name="importFiles">A new instance of the SortingMethods class</param>
        public void DataImport(SortingMethods importFiles)
        {
            // TODO: 
            //  • Få progressbaren til at lukke automatisk når progress rammer 100%
            //

            try
            {
                switch (sortingMethodOfChoice)
                {
                    case 0:
                        importFiles.Move(new Progress<ProgressReportModel>(DisplayProgress), destinationPathFolder, FoundFielsFromSearch, ct);
                        break;

                    case 1:
                        importFiles.Copy(new Progress<ProgressReportModel>(DisplayProgress), destinationPathFolder, FoundFielsFromSearch, ct);
                        break;

                    case 2:
                        importFiles.LastModefiedDate(new Progress<ProgressReportModel>(DisplayProgress), destinationPathFolder, FoundFielsFromSearch, ct);
                        break;

                    case 3:
                        importFiles.CreatedDate(new Progress<ProgressReportModel>(DisplayProgress), destinationPathFolder, FoundFielsFromSearch, ct);
                        break;

                    case 4:
                        importFiles.Alfabetic(new Progress<ProgressReportModel>(DisplayProgress), destinationPathFolder, FoundFielsFromSearch, ct);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            {
                // Hånter Exception her
            }

        }

        /// <summary>
        /// Takes an int value and uses it to display the progresbar value
        /// </summary>
        /// <param name="progress">An int value for the progressbar Value</param>
        private void DisplayProgress(ProgressReportModel progress)
        {
            // Udskriver Progressbar value (hvor langt progressbaren den er)
            progressBar.Value = progress.PercentageCompleted;
        }

        #endregion

        
    }
}
