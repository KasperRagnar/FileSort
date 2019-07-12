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

        public string Label_SortingMethod { get; set; }
        public string Label_FileTypes { get; set; }
        public string Label_Language { get; set; }
        public string ComboBox_SortingMethods { get; set; }
        public string ComboBox_FileTypes { get; set; }
        public string ComboBox_Languages { get; set; }

        public string Label_SourchPathLabel { get; set; }
        public string Label_DestinationPathLabel { get; set; }
        public string btn_SourchPathButton { get; set; }
        public string btn_DestinationPathButton { get; set; }

        public string radio_SearchFolderOption { get; set; }
        public string radio_SearchAllSubFoldersOption { get; set; }

        public string btn_StartButton { get; set; }
    }
}
