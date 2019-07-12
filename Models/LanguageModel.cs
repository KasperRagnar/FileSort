using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class LanguageModel
    {
        public string TextBox_ProgramHeader { get; set; }

        public string TextBox_ContentTextBox { get; set; }

        public string TextBox_ErrorMsgBox { get; set; }

        public string TextBox_SortingMethod { get; set; }
        public string TextBox_FileTypes { get; set; }
        public string TextBox_Language { get; set; }

        public string[] ComboBox_SortingMethods { get; set; }
        public string[] ComboBox_FileTypes { get; set; }
        public string[] ComboBox_Languages { get; set; }

        public string TextBox_SourchPathLabel { get; set; }
        public string TextBox_DestinationPathLabel { get; set; }
        public string btn_SourchPathButton { get; set; }
        public string btn_DestinationPathButton { get; set; }

        public string radio_SearchFolderOption { get; set; }
        public string radio_SearchAllSubFoldersOption { get; set; }

        public string btn_StartButton { get; set; }
    }
}
