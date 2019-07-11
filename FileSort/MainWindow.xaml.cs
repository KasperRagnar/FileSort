using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileSort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region GLOBAL 
        bool allOrOneFolderBool;

        FolderBrowserDialog fbd = new FolderBrowserDialog();

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        #region Language
        
        #endregion

        #region Select Paths
        private void SourchPathButton_Click(object sender, RoutedEventArgs e)
        {
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SourchPathBox.Text = fbd.SelectedPath; // selected folder path
            }
        }

        private void DestinationPathButton_Click(object sender, RoutedEventArgs e)
        {
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DestinationPathBox.Text = fbd.SelectedPath; // selected folder path
            }
        }
        #endregion

        #region Radio buttons
        private void SearchFolderOption_Checked(object sender, RoutedEventArgs e)
        {
            if (SearchAllSubFoldersOption.IsChecked == true)
            {
                SearchFolderOption.IsChecked = false;
            }
            allOrOneFolderBool = false;
        }

        private void SearchAllSubFoldersOption_Checked(object sender, RoutedEventArgs e)
        {
            if (SearchFolderOption.IsChecked == true)
            {
                SearchAllSubFoldersOption.IsChecked = false;
            }
            allOrOneFolderBool = true;
        }
        #endregion

        
    }
}
