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
using Repository;

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
        LanguageSettings LS = new LanguageSettings();

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ComboBox_Languages.SelectedIndex = 1; // Default language is English 

        }

        #region Language
        private void ComboBox_Languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexedSelection = ComboBox_Languages.SelectedIndex;

            var LanguageList = LS.LanguageList(indexedSelection);
            
            if (LanguageList[indexedSelection] != null)
            {
                LanguageList[indexedSelection].TextBox_ProgramHeader = ProgramHeader.Text;
                LanguageList[indexedSelection].TextBox_ContentTextBox = ContentTextBox.Text;
                LanguageList[indexedSelection].TextBox_ErrorMsgBox = ErrorMsgBox.Text;

                LanguageList[indexedSelection].TextBox_SortingMethod = SortingMethodsTextBox.Text;
                LanguageList[indexedSelection].TextBox_FileTypes = FileTypesTextBox.Text;
                LanguageList[indexedSelection].TextBox_Language = LanguageesTextBox.Text;

                //TODO 
                // First clear the existing 'ConboboxItem' list and then insert the new list from the 'Languagelist'

                //ComboBox_Languages.
                foreach (var IndexedItem in LanguageList[indexedSelection].ComboBox_SortingMethods)
                {
                    //ComboBox_SortingMethods.inde
                }
                
            }
            
            //TODO 
            // Set LanguageModel = MainView element name ".text/content"

        }
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
