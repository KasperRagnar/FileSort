using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Repository
{
    public class LanguageSettings
    {
        LanguageModel LM = new LanguageModel();
        
        public List<LanguageModel> LanguageList(int indexNumber)
        {
            List<LanguageModel> LM = new List<LanguageModel>();

            LM.Add(new LanguageModel // Danish = Index 0
            {
                TextBox_ProgramHeader = "FileSort", 
                TextBox_ContentTextBox = "",
                TextBox_ErrorMsgBox = "",

                Label_SortingMethod = "",
                ComboBox_SortingMethods = "",

                Label_FileTypes = "",
                ComboBox_FileTypes = "",

                Label_Language = "",
                ComboBox_Languages = "",

                Label_SourchPathLabel = "",
                btn_SourchPathButton = "", 

                Label_DestinationPathLabel = "",
                btn_DestinationPathButton = "",

                radio_SearchFolderOption = "", 
                radio_SearchAllSubFoldersOption = "",

                btn_StartButton = "",
            });

            LM.Add(new LanguageModel // English = Index 1
            {
                TextBox_ProgramHeader = "FileSort",
                TextBox_ContentTextBox = "",
                TextBox_ErrorMsgBox = "",

                Label_SortingMethod = "",
                ComboBox_SortingMethods = "",

                Label_FileTypes = "",
                ComboBox_FileTypes = "",

                Label_Language = "",
                ComboBox_Languages = "",

                Label_SourchPathLabel = "",
                btn_SourchPathButton = "",

                Label_DestinationPathLabel = "",
                btn_DestinationPathButton = "",

                radio_SearchFolderOption = "",
                radio_SearchAllSubFoldersOption = "",

                btn_StartButton = "",
            });

            return LM;
        }
    }
}
