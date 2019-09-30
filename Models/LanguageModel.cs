using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class LanguageModel
    {
        // Content text in the "info" messageBox
        public string TextBox_ContentTextBox { get; set; }

        // Error message text
        public string[] TextBox_ErrorMsgBox { get; set; }

        // Labels over the ConboBoxes "SortingMethod, FileTypes, Language"
        public string TextBox_SortingMethod { get; set; }
        public string TextBox_FileTypes { get; set; }
        public string TextBox_Language { get; set; }
        public string TextBox_StylesTextBox { get; set; }
        public string TextBox_AddCostumFileType { get; set; }

        // Text inside the ConboBoxes "SortingMethod, FileTypes, Language"
        public string[] ComboBox_SortingMethods { get; set; }
        public string[] ComboBox_FileTypes { get; set; }
        public string[] ComboBox_Languages { get; set; }
        public string[] ComboBox_Styles { get; set; }
       

        // Labels over the path TextBoxes
        public string TextBox_SourchPathLabel { get; set; }
        public string TextBox_DestinationPathLabel { get; set; }

        // A mix of buttons on the main window
        public string btn_SourchPathButton { get; set; }
        public string btn_DestinationPathButton { get; set; }
        public string btn_RemoveFileTypeToList { get; set; }
        public string btn_AddFileTypeToList { get; set; }
        public string btn_AddCostumFileType { get; set; }

        // Radio buttons text
        public string radio_SearchFolderOption { get; set; }
        public string radio_SearchAllSubFoldersOption { get; set; }

        // Start Button
        public string btn_StartButton { get; set; }

        // MessageBox text
        public string[] MsgBoxText { get; set; }
        public string MsgBoxHeader { get; set; }
    }
}
