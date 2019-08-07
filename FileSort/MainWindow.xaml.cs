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
        bool allOrOneFolderBool; // Used for walidation under Radio buttons
        string appInfoMessageBox;

        List<string> ListBoxFileTypes = new List<string>(); // A list of alle the selected items in the "ListBox_FileTypes"

        FolderBrowserDialog fbd = new FolderBrowserDialog();
        LanguageSettings LS = new LanguageSettings();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ComboBox_FileTypes.ItemsSource = LS.FileTypesArr; // Add's a 'File Type'list to the UI.

            ComboBox_Languages.ItemsSource = LS.LanguagesArr; // Add's a 'language' list to the UI.
            ComboBox_Languages.SelectedIndex = 1; // Sets the default language in the UI to: English 
        }

        #region Message box
        private void InfoBox_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(appInfoMessageBox);
        }
        #endregion

        #region Add / Remove filetypes fron Listbox
        private void AddFileTypeToList_Click(object sender, RoutedEventArgs e)
        {
            if (!ListBox_FileTypes.Items.Contains(ComboBox_FileTypes.SelectedItem))
            {
                ListBox_FileTypes.Items.Add(ComboBox_FileTypes.SelectedItem);
            }
        }

        private void RemoveFileTypeToList_Click(object sender, RoutedEventArgs e)
        {
            ListBox_FileTypes.Items.Remove(ListBox_FileTypes.SelectedItem);
        }
        #endregion

        #region Language
        private void ComboBox_Languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int indexedSelection = ComboBox_Languages.SelectedIndex;

                var LanguageList = LS.LanguageList(indexedSelection);

                if (LanguageList != null)
                {
                    appInfoMessageBox = LanguageList.TextBox_ContentTextBox; //for the application info messagebox

                    ErrorMsgBox.Text = LanguageList.TextBox_ErrorMsgBox[0];

                    SortingMethodsTextBox.Text = LanguageList.TextBox_SortingMethod;
                    ComboBox_SortingMethods.ItemsSource = LanguageList.ComboBox_SortingMethods;

                    FileTypesTextBox.Text = LanguageList.TextBox_FileTypes;
                    AddFileTypeToList.Content = LanguageList.btn_AddFileTypeToList;
                    RemoveFileTypeToList.Content = LanguageList.btn_RemoveFileTypeToList;

                    LanguageesTextBox.Text = LanguageList.TextBox_Language;

                    SourchPathLabel.Content = LanguageList.TextBox_SourchPathLabel;
                    SourchPathButton.Content = LanguageList.btn_SourchPathButton;

                    DestinationPathLabel.Content = LanguageList.TextBox_DestinationPathLabel;
                    DestinationPathButton.Content = LanguageList.btn_DestinationPathButton;

                    SearchFolderOption.Content = LanguageList.radio_SearchFolderOption;
                    SearchAllSubFoldersOption.Content = LanguageList.radio_SearchAllSubFoldersOption;

                    StartButton.Content = LanguageList.btn_StartButton;
                }
            }
            catch (Exception)
            {

                throw;
            }
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

        #region START and run the program
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ListBox_FileTypes.Items) // Adds all the items in the listbox to a new global list called "ListBoxFileTypes"
            {
                ListBoxFileTypes.Add(Convert.ToString(item)); 
            }
            


        }
        #endregion


    }
}
