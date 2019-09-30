using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Repository;
using Models;
using System.IO;
using System.Threading;

namespace FileSort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region GLOBAL 
        bool allOrOneFolderBool;            // Used for walidation under Radio buttons
        bool runConditions;                 // This bool is used for error handeling in START
        LanguageModel ChousenLanguageList;  // A model/list of the current selectet language varibels
        string appInfoMessageBox;           // this string is the info message in 'InfoBox_Click' method
        int languageIndexedSelection;       // A int index-number for the current language.
        int StyleIndexedSelection;          // A int index-number for the current Theam/styleing.
        string[] MsgBoxTextIndput;          // A plaseholder for the Message box "From: To:" text
        string MsgBoxHeaderIndput;          // A placeholder for the Message box "Header" text

        List<string> ListBoxFileTypesFilter = new List<string>();       // A list of alle the selected items in the "ListBox_FileTypes"

        FolderBrowserDialog fbd = new FolderBrowserDialog();
        LanguageSettings LS = new LanguageSettings();
        SearchAndFindFiles SAFF = new SearchAndFindFiles();
        SortingMethods SM = new SortingMethods();
        SystemMessageBoxes SMB = new SystemMessageBoxes();
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            #region UI / GUI
            WindowStartupLocation = WindowStartupLocation.CenterScreen;     // Starts the application up in the middle of the sceen

            ComboBox_FileTypes.ItemsSource = LS.FileTypesArr;               // Add's a 'File Type'list to the UI.
            languageIndexedSelection = ComboBox_Languages.SelectedIndex;    // Getting and setting data from the LanguageSettings.cs 
            StyleIndexedSelection = ComboBox_Styles.SelectedIndex;          // Getting and setting data from the StylingSettings.cs 

            ComboBox_Languages.ItemsSource = LS.LanguagesArr;               // Add's a 'language' list to the UI.
            ComboBox_Languages.SelectedIndex = 1;                           // Sets the default language in the UI to: English 
            ComboBox_Styles.SelectedIndex = 0;                              // Sets the default Theam in the UI to: Light Mode 
            ComboBox_SortingMethods.SelectedIndex = 0;                      // Sets the default Sorting method in the UI to: Move 
            ComboBox_FileTypes.SelectedIndex = 0;                           // Sets the default file type in the UI to: .jpg 
            #endregion
            
        }

        #region Message box & Error Messages clean-up
        private void InfoBox_Click(object sender, RoutedEventArgs e)
        {
            // Makes a new pop-up message box with info about the program.
            System.Windows.Forms.MessageBox.Show(appInfoMessageBox, "Info");
        }

        public void UiErrorMessages(int msgIndex) 
        {
            // sets the error message (textbox) to the index (int) you have sent with the method. 
            ErrorMsgBox.Text = ChousenLanguageList.TextBox_ErrorMsgBox[msgIndex];

            // Error Message index list
            // [0] = "",
            // [1] = "• Error: Not all fields are selected!",
            // [2] = "• Error: NO image files found!",
            // [3] = "• Error: Try anothe path",
            // [4] = "• Error: Something went wrong. Restart the program and try again!"
            // [5] = "• Error: Something went wrong.Try another path! or see if all fields are filled out!"
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
                languageIndexedSelection = ComboBox_Languages.SelectedIndex;

                ChousenLanguageList = LS.LanguageList(languageIndexedSelection);
                

                if (ChousenLanguageList != null)
                {
                    appInfoMessageBox = ChousenLanguageList.TextBox_ContentTextBox;                             //for the application info messagebox

                    ErrorMsgBox.Text = ChousenLanguageList.TextBox_ErrorMsgBox[0];

                    SortingMethodsTextBox.Text = ChousenLanguageList.TextBox_SortingMethod;
                    ComboBox_SortingMethods.ItemsSource = ChousenLanguageList.ComboBox_SortingMethods;

                    FileTypesTextBox.Text = ChousenLanguageList.TextBox_FileTypes;
                    AddFileTypeToList.Content = ChousenLanguageList.btn_AddFileTypeToList;
                    RemoveFileTypeToList.Content = ChousenLanguageList.btn_RemoveFileTypeToList;

                    LanguageesTextBox.Text = ChousenLanguageList.TextBox_Language;

                    TextBox_StylesTextBox.Text = ChousenLanguageList.TextBox_StylesTextBox;
                    ComboBox_Styles.ItemsSource = ChousenLanguageList.ComboBox_Styles;

                    TextBox_AddCostumFileType.Text = ChousenLanguageList.TextBox_AddCostumFileType;
                    btn_AddCostumFileType.Content = ChousenLanguageList.btn_AddCostumFileType;

                    SourchPathLabel.Content = ChousenLanguageList.TextBox_SourchPathLabel;
                    SourchPathButton.Content = ChousenLanguageList.btn_SourchPathButton;

                    DestinationPathLabel.Content = ChousenLanguageList.TextBox_DestinationPathLabel;
                    DestinationPathButton.Content = ChousenLanguageList.btn_DestinationPathButton;

                    SearchFolderOption.Content = ChousenLanguageList.radio_SearchFolderOption;
                    SearchAllSubFoldersOption.Content = ChousenLanguageList.radio_SearchAllSubFoldersOption;

                    StartButton.Content = ChousenLanguageList.btn_StartButton;

                    MsgBoxTextIndput = ChousenLanguageList.MsgBoxText;                                          // A string[] for dynamid text indput on the message box text fild
                    MsgBoxHeaderIndput = ChousenLanguageList.MsgBoxHeader;                                      // A string for dynamid text indput on the message box header
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Styleing
        private void ComboBox_Styles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                StyleIndexedSelection = ComboBox_Styles.SelectedIndex;

                ChousenLanguageList = LS.LanguageList(languageIndexedSelection);


                if (ChousenLanguageList != null)
                {
                    appInfoMessageBox = ChousenLanguageList.TextBox_ContentTextBox;                             //for the application info messagebox

                    ErrorMsgBox.Text = ChousenLanguageList.TextBox_ErrorMsgBox[0];

                    SortingMethodsTextBox.Text = ChousenLanguageList.TextBox_SortingMethod;
                    ComboBox_SortingMethods.ItemsSource = ChousenLanguageList.ComboBox_SortingMethods;

                    FileTypesTextBox.Text = ChousenLanguageList.TextBox_FileTypes;
                    AddFileTypeToList.Content = ChousenLanguageList.btn_AddFileTypeToList;
                    RemoveFileTypeToList.Content = ChousenLanguageList.btn_RemoveFileTypeToList;

                    LanguageesTextBox.Text = ChousenLanguageList.TextBox_Language;

                    TextBox_StylesTextBox.Text = ChousenLanguageList.TextBox_StylesTextBox;
                    ComboBox_Styles.ItemsSource = ChousenLanguageList.ComboBox_Styles;

                    SourchPathLabel.Content = ChousenLanguageList.TextBox_SourchPathLabel;
                    SourchPathButton.Content = ChousenLanguageList.btn_SourchPathButton;

                    DestinationPathLabel.Content = ChousenLanguageList.TextBox_DestinationPathLabel;
                    DestinationPathButton.Content = ChousenLanguageList.btn_DestinationPathButton;

                    SearchFolderOption.Content = ChousenLanguageList.radio_SearchFolderOption;
                    SearchAllSubFoldersOption.Content = ChousenLanguageList.radio_SearchAllSubFoldersOption;

                    StartButton.Content = ChousenLanguageList.btn_StartButton;

                    MsgBoxTextIndput = ChousenLanguageList.MsgBoxText;                                          // A string[] for dynamid text indput on the message box text fild
                    MsgBoxHeaderIndput = ChousenLanguageList.MsgBoxHeader;                                      // A string for dynamid text indput on the message box header
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
                    UiErrorMessages(1); // sets the error messagebox to error message number 1 using the 'UiErrorMessages' Method
                    #endregion
                }
                else
                {
                    SMB.LanguageDefinition(languageIndexedSelection); // Sendt the index-number of the current language in use to the 'languageDefinition' method
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

                    UiErrorMessages(0); // sets the error messagebox to error message number 0 using the 'UiErrorMessages' Method

                    #region Making a filter out os the items from ListBox_FileTypes called 'ListBoxFileTypesFilter'
                    foreach (var item in ListBox_FileTypes.Items)
                    {
                        ListBoxFileTypesFilter.Add(Convert.ToString(item));
                    } // Adds all the items in the listbox to a new global list called "ListBoxFileTypes"
                    ListBox_FileTypes.Items.Clear(); // Clears the 'ListBox_FileTypes' list after giving alle values to 'ListBoxFileTypes'
                    #endregion

                    #region Looking for files that mach the 'ListBoxFileTypesFilter' list
                    //-- Calling the search Method and gets back a Array of Strings (every string is a full file path)
                    String[] filesFoundInSearch = SAFF.GetFilesFrom(selectedPath, ListBoxFileTypesFilter, allOrOneFolderBool);

                    //-- Checks if the list of files is emty or null.
                    if (filesFoundInSearch.Length <= 0 || filesFoundInSearch == null)
                    {
                        UiErrorMessages(2); // sets the error messagebox to error message number 3 using the 'UiErrorMessages' Method
                    }
                    #endregion

                    #region Sorting Meteds
                    else
                    {
                        switch (ComboBox_SortingMethods.SelectedIndex)
                        {
                            case 0:     // Move
                                #region case 0
                                try
                                {
                                    #region Display names for the "From: and To:" paths
                                    // TO: path
                                    string[] destPathSplitterArr = destPathFolder.Split('\\');
                                    List<string> destPathEndFolder = new List<string>();
                                    foreach (var split in destPathSplitterArr)
                                    {
                                        if (split != "")
                                        {
                                            destPathEndFolder.Add(split);
                                        }
                                    }

                                    //From: path
                                    string[] sourcePathSplitterArr = selectedPath.Split('\\');
                                    List<string> sourcePathEndFolder = new List<string>();
                                    foreach (var split in sourcePathSplitterArr)
                                    {
                                        if (split != "")
                                        {
                                            sourcePathEndFolder.Add(split);
                                        }
                                    }

                                    // MsgBoxTextIndput[ ] = dynamisk string[] indput from LanguageSettings
                                    string msgText = $"{MsgBoxTextIndput[0]} {sourcePathEndFolder[sourcePathEndFolder.Count - 1]}. {MsgBoxTextIndput[1]} {destPathEndFolder[destPathEndFolder.Count - 1]}.";
                                    string msgHeader = $"{MsgBoxHeaderIndput}";

                                    #endregion

                                    // Makes a new window everytime it opens
                                    MessageBoxWithProgressBar MBWPB = new MessageBoxWithProgressBar(ComboBox_SortingMethods.SelectedIndex, msgText, msgHeader, destPathFolder, filesFoundInSearch);          
                                    MBWPB.Show();                                       // Opens a new 'MessageBoxWithProgressBar' window
                                }
                                catch (Exception)
                                {
                                    // Fang de exeptions der bobler op
                                }
                                #endregion
                                break;

                            case 1:     // Copy
                                #region case 1
                                try
                                {
                                    #region Display names for the "From: and To:" paths
                                    // TO: path
                                    string[] destPathSplitterArr = destPathFolder.Split('\\');
                                    List<string> destPathEndFolder = new List<string>();
                                    foreach (var split in destPathSplitterArr)
                                    {
                                        if (split != "")
                                        {
                                            destPathEndFolder.Add(split);
                                        }
                                    }

                                    //From: path
                                    string[] sourcePathSplitterArr = selectedPath.Split('\\');
                                    List<string> sourcePathEndFolder = new List<string>();
                                    foreach (var split in sourcePathSplitterArr)
                                    {
                                        if (split != "")
                                        {
                                            sourcePathEndFolder.Add(split);
                                        }
                                    }

                                    // MsgBoxTextIndput[ ] = dynamisk string[] indput from LanguageSettings
                                    string msgText = $"{MsgBoxTextIndput[0]} {sourcePathEndFolder[sourcePathEndFolder.Count - 1]}. {MsgBoxTextIndput[1]} {destPathEndFolder[destPathEndFolder.Count - 1]}.";
                                    string msgHeader = $"{MsgBoxHeaderIndput}";

                                    #endregion

                                    // Makes a new window everytime it opens
                                    MessageBoxWithProgressBar MBWPB = new MessageBoxWithProgressBar(ComboBox_SortingMethods.SelectedIndex, msgText, msgHeader, destPathFolder, filesFoundInSearch);
                                    MBWPB.Show();                                       // Opens a new 'MessageBoxWithProgressBar' window
                                }
                                catch (Exception)
                                {
                                    // Fang de exeptions der bobler op
                                }
                                #endregion
                                break;

                            case 2:     // Last Modefied Date
                                #region case 2
                                try
                                {
                                    #region Display names for the "From: and To:" paths
                                    // TO: path
                                    string[] destPathSplitterArr = destPathFolder.Split('\\');
                                    List<string> destPathEndFolder = new List<string>();
                                    foreach (var split in destPathSplitterArr)
                                    {
                                        if (split != "")
                                        {
                                            destPathEndFolder.Add(split);
                                        }
                                    }

                                    //From: path
                                    string[] sourcePathSplitterArr = selectedPath.Split('\\');
                                    List<string> sourcePathEndFolder = new List<string>();
                                    foreach (var split in sourcePathSplitterArr)
                                    {
                                        if (split != "")
                                        {
                                            sourcePathEndFolder.Add(split);
                                        }
                                    }

                                    // MsgBoxTextIndput[ ] = dynamisk string[] indput from LanguageSettings
                                    string msgText = $"{MsgBoxTextIndput[0]} {sourcePathEndFolder[sourcePathEndFolder.Count - 1]}. {MsgBoxTextIndput[1]} {destPathEndFolder[destPathEndFolder.Count - 1]}.";
                                    string msgHeader = $"{MsgBoxHeaderIndput}";

                                    #endregion

                                    // Makes a new window everytime it opens
                                    MessageBoxWithProgressBar MBWPB = new MessageBoxWithProgressBar(ComboBox_SortingMethods.SelectedIndex, msgText, msgHeader, destPathFolder, filesFoundInSearch);
                                    MBWPB.Show();                                       // Opens a new 'MessageBoxWithProgressBar' window
                                }
                                catch (Exception)
                                {
                                    // Fang de exeptions der bobler op
                                }
                                #endregion
                                break;

                            case 3:     // Created Date
                                #region case 3
                                try
                                {
                                    #region Display names for the "From: and To:" paths
                                    // TO: path
                                    string[] destPathSplitterArr = destPathFolder.Split('\\');
                                    List<string> destPathEndFolder = new List<string>();
                                    foreach (var split in destPathSplitterArr)
                                    {
                                        if (split != "")
                                        {
                                            destPathEndFolder.Add(split);
                                        }
                                    }

                                    //From: path
                                    string[] sourcePathSplitterArr = selectedPath.Split('\\');
                                    List<string> sourcePathEndFolder = new List<string>();
                                    foreach (var split in sourcePathSplitterArr)
                                    {
                                        if (split != "")
                                        {
                                            sourcePathEndFolder.Add(split);
                                        }
                                    }

                                    // MsgBoxTextIndput[ ] = dynamisk string[] indput from LanguageSettings
                                    string msgText = $"{MsgBoxTextIndput[0]} {sourcePathEndFolder[sourcePathEndFolder.Count - 1]}. {MsgBoxTextIndput[1]} {destPathEndFolder[destPathEndFolder.Count - 1]}.";
                                    string msgHeader = $"{MsgBoxHeaderIndput}";

                                    #endregion

                                    // Makes a new window everytime it opens
                                    MessageBoxWithProgressBar MBWPB = new MessageBoxWithProgressBar(ComboBox_SortingMethods.SelectedIndex, msgText, msgHeader, destPathFolder, filesFoundInSearch);
                                    MBWPB.Show();                                       // Opens a new 'MessageBoxWithProgressBar' window
                                }
                                catch (Exception)
                                {
                                    // Fang de exeptions der bobler op
                                }
                                #endregion
                                break;

                            case 4:     // Alfabetic (abc)
                                #region case 4
                                try
                                {
                                    #region Display names for the "From: and To:" paths
                                    // TO: path
                                    string[] destPathSplitterArr = destPathFolder.Split('\\');
                                    List<string> destPathEndFolder = new List<string>();
                                    foreach (var split in destPathSplitterArr)
                                    {
                                        if (split != "")
                                        {
                                            destPathEndFolder.Add(split);
                                        }
                                    }

                                    //From: path
                                    string[] sourcePathSplitterArr = selectedPath.Split('\\');
                                    List<string> sourcePathEndFolder = new List<string>();
                                    foreach (var split in sourcePathSplitterArr)
                                    {
                                        if (split != "")
                                        {
                                            sourcePathEndFolder.Add(split);
                                        }
                                    }

                                    // MsgBoxTextIndput[ ] = dynamisk string[] indput from LanguageSettings
                                    string msgText = $"{MsgBoxTextIndput[0]} {sourcePathEndFolder[sourcePathEndFolder.Count - 1]}. {MsgBoxTextIndput[1]} {destPathEndFolder[destPathEndFolder.Count - 1]}.";
                                    string msgHeader = $"{MsgBoxHeaderIndput}";

                                    #endregion

                                    // Makes a new window everytime it opens
                                    MessageBoxWithProgressBar MBWPB = new MessageBoxWithProgressBar(ComboBox_SortingMethods.SelectedIndex, msgText, msgHeader, destPathFolder, filesFoundInSearch);
                                    MBWPB.Show();                                       // Opens a new 'MessageBoxWithProgressBar' window
                                }
                                catch (Exception)
                                {
                                    // Fang de exeptions der bobler op
                                }
                                #endregion
                                break;

                            default:
                                UiErrorMessages(4); //-- Unexpected error message. please restart the program
                                break;
                        }
                    }
                    #endregion

                    ClearUI(); // Clears all the textboxes to prepare for the next job
                }
            }
            catch (Exception)
            {
                // Højeste lag af Exception
            }
        }
        #endregion


    }
}
