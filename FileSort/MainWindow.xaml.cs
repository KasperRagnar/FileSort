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
using Models;

namespace FileSort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region GLOBAL 
        bool allOrOneFolderBool; // Used for walidation under Radio buttons
        bool runConditions;
        LanguageModel ChousenLanguageList; // A model/list of the current selectet language varibels
        string appInfoMessageBox;

        List<string> ListBoxFileTypesFilter = new List<string>(); // A list of alle the selected items in the "ListBox_FileTypes"

        FolderBrowserDialog fbd = new FolderBrowserDialog();
        LanguageSettings LS = new LanguageSettings();
        DoFileExistCheck DFEC = new DoFileExistCheck();
        SearchAndFindFiles SAFF = new SearchAndFindFiles();
        SortingMethods SM = new SortingMethods();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            ComboBox_FileTypes.ItemsSource = LS.FileTypesArr; // Add's a 'File Type'list to the UI.

            ComboBox_Languages.ItemsSource = LS.LanguagesArr; // Add's a 'language' list to the UI.
            ComboBox_Languages.SelectedIndex = 1; // Sets the default language in the UI to: English 
            ComboBox_SortingMethods.SelectedIndex = 0;
            ComboBox_FileTypes.SelectedIndex = 0;
        }

        #region Message box & Error Messages clean-up
        private void InfoBox_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(appInfoMessageBox, "Info");
        }

        public void ErrorMessagesCleanUp() 
        {
            ErrorMsgBox.Text = ChousenLanguageList.TextBox_ErrorMsgBox[0]; // sets the error messagebox to "";
        }

        public void ClearUI()
        {
            ErrorMsgBox.Text = ChousenLanguageList.TextBox_ErrorMsgBox[0]; // sets the error messagebox to "";
            ComboBox_FileTypes.SelectedIndex = 0;
            SourchPathBox.Text = "";
            DestinationPathBox.Text = "";
            SearchFolderOption.IsChecked = false;
            SearchAllSubFoldersOption.IsChecked = false;
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

                ChousenLanguageList = LS.LanguageList(indexedSelection);
                

                if (ChousenLanguageList != null)
                {
                    appInfoMessageBox = ChousenLanguageList.TextBox_ContentTextBox; //for the application info messagebox

                    ErrorMsgBox.Text = ChousenLanguageList.TextBox_ErrorMsgBox[0];

                    SortingMethodsTextBox.Text = ChousenLanguageList.TextBox_SortingMethod;
                    ComboBox_SortingMethods.ItemsSource = ChousenLanguageList.ComboBox_SortingMethods;

                    FileTypesTextBox.Text = ChousenLanguageList.TextBox_FileTypes;
                    AddFileTypeToList.Content = ChousenLanguageList.btn_AddFileTypeToList;
                    RemoveFileTypeToList.Content = ChousenLanguageList.btn_RemoveFileTypeToList;

                    LanguageesTextBox.Text = ChousenLanguageList.TextBox_Language;

                    SourchPathLabel.Content = ChousenLanguageList.TextBox_SourchPathLabel;
                    SourchPathButton.Content = ChousenLanguageList.btn_SourchPathButton;

                    DestinationPathLabel.Content = ChousenLanguageList.TextBox_DestinationPathLabel;
                    DestinationPathButton.Content = ChousenLanguageList.btn_DestinationPathButton;

                    SearchFolderOption.Content = ChousenLanguageList.radio_SearchFolderOption;
                    SearchAllSubFoldersOption.Content = ChousenLanguageList.radio_SearchAllSubFoldersOption;

                    StartButton.Content = ChousenLanguageList.btn_StartButton;
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
            try
            {
                #region Check for errors
                if (ComboBox_SortingMethods.SelectedItem == null || ComboBox_Languages.SelectedItem == null || ListBox_FileTypes.Items.IsEmpty || SourchPathBox.Text == "" || DestinationPathBox.Text == "" || (SearchFolderOption.IsChecked == false && SearchAllSubFoldersOption.IsChecked == false))
                {
                    runConditions = false;

                    #region Error Messages 
                    ErrorMsgBox.Text = ChousenLanguageList.TextBox_ErrorMsgBox[1]; // A error message taken from an 'array' from the ChousenLanguageList
                    #endregion
                }
                else
                {
                    runConditions = true;
                }
                #endregion

                if (runConditions == true)
                {
                    #region Local Variabler
                    string selectedPath = SourchPathBox.Text;
                    string destPath = DestinationPathBox.Text;
                    string destPathFolder = destPath + "\\";
                    #endregion

                    ErrorMessagesCleanUp(); // sets the error messagebox to "";

                    #region Making a filter out os the items from ListBox_FileTypes called 'ListBoxFileTypesFilter'
                    foreach (var item in ListBox_FileTypes.Items)
                    {
                        ListBoxFileTypesFilter.Add(Convert.ToString(item));
                    } // Adds all the items in the listbox to a new global list called "ListBoxFileTypes"
                    ListBox_FileTypes.Items.Clear(); // Clears the 'ListBox_FileTypes' list after giving alle values to 'ListBoxFileTypes'
                    #endregion

                    #region Looking for files that mach the 'ListBoxFileTypesFilter' list

                    //-- Calling the search Method and gets back a Array of Strings (every string is a full file path)
                    String[] searchResult = SAFF.GetFilesFrom(selectedPath, ListBoxFileTypesFilter, allOrOneFolderBool);

                    //-- Checks if the list of files is emty or null.
                    if (searchResult.Length <= 0 || searchResult == null)
                    {
                        ErrorMsgBox.Text = ChousenLanguageList.TextBox_ErrorMsgBox[2]; // sets the error messagebox to error message number 3
                    }
                    #endregion

                    #region Sorting Meteds
                    else
                    {
                        switch (ComboBox_SortingMethods.SelectedIndex)
                        {
                            case 0:     // Move
                                SM.Move(selectedPath, destPathFolder, searchResult);
                                break;

                            case 1:     // Copy
                                SM.Copy(selectedPath, destPathFolder, searchResult);
                                break;

                            case 2:     // Last Modefied Date
                                SM.LastModefiedDate(selectedPath, destPath, destPathFolder, searchResult);
                                break;

                            case 3:     // Created Date
                                SM.CreatedDate();
                                break;

                            case 4:     // Alfabetic (abc)
                                SM.Alfabetic();
                                break;

                            default:
                                ErrorMsgBox.Text = ChousenLanguageList.TextBox_ErrorMsgBox[4]; //-- Unexpected error message
                                break;
                        }
                    }
                    #endregion

                    ClearUI(); // Clears all the textboxes to prepare for the next job
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }
}
