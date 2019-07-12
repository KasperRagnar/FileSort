using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Repository
{
    public class LanguageSettings
    {
        #region GLOBAL
        LanguageModel LM = new LanguageModel();

        string[] FileTypesArr = new String[] { "jpg", "jpeg", "png", "tif", "tiff", "gif", "3fr", "raw", "dcr", "cr3", "cr2", "erf", "mef", "mos", "nef", "orf", "pef", "rw2", "arw", "crw", "srf", "sr2", ".doc", ".docx", ".pdf", ".rtf", ".tex", ".txt", ".wks", ".wps", ".wpd", ".dll", ".exe", "", "", "", "", "", "", "", "", "", "", "", "", };
        string[] LanguagesArr = new String[] { "Dansk", "English" };
        #endregion

        public List<LanguageModel> LanguageList(int indexNumber)
        {
            List<LanguageModel> LM = new List<LanguageModel>();

            LM.Add(new LanguageModel // Danish = Index 0
            {
                TextBox_ProgramHeader = "FileSort", 
                TextBox_ContentTextBox = "FileSort er et lille program der hjælper dig med at organisere dine filer. &#xA;&#xA;Sådan virker der:&#xA;   • [Sprog]: Vægl et sprog.&#xA;   • [Fil Typer]: Vælg hvad for fil typer du vil sortere.&#xA;   • [Sorterings Metoder]: Vælg hvordan du vil sortere dine filer.&#xA;   • [Hvor Fra ?]: Vælg hvor FileSort skal kigge efter filer. &#xA;   • [Hvor til ?]: Vælg hvor FileSort skal lægge de sorterede filer. &#xA;   • Husk at vælge hvor grundigt FileSort skal søger efter filer. &#xA;       ○ Søg kun i den valgte folder! &#xA;       ○ Søg i den valgte folder og alle dens undermapper. &#xA;   • Klik på START knappen når alt er udfyldt. ",
                TextBox_ErrorMsgBox = "",

                TextBox_SortingMethod = "Sorterings Metoder",
                ComboBox_SortingMethods = new String[] { "Sidste redigeret dato", "Skaber Dato", "Alfabetisk (abc)" },

                TextBox_FileTypes = "Fil Typer",
                ComboBox_FileTypes = FileTypesArr,

                TextBox_Language = "Sprog",
                ComboBox_Languages = LanguagesArr,

                TextBox_SourchPathLabel = "Hvor fra ?",
                btn_SourchPathButton = "Gennemse",

                TextBox_DestinationPathLabel = "Hvor til ?",
                btn_DestinationPathButton = "Gennemse",

                radio_SearchFolderOption = "Søg kun i den valgte folder!", 
                radio_SearchAllSubFoldersOption = "Søg i den valgte folder og alle dens undermapper.",

                btn_StartButton = "START",
            });

            LM.Add(new LanguageModel // English = Index 1
            {
                TextBox_ProgramHeader = "FileSort",
                TextBox_ContentTextBox = "FileSort is a small program that helps you organiz your files. &#xA;&#xA;How it works:&#xA;   • [Language]: Select a language of your choice.&#xA;   • [File Types]: Select What file types you want to sort.&#xA;   • [Sorting Method]: Select how you want to sort your files.&#xA;   • [Source folder]: Select witch folder FileSort should look for files. &#xA;   • [Destination folder]: Select where FileSort should put the sorted files. &#xA;   • Remember to choose how thoroughly FileSerch can search for files. &#xA;       ○ search in the selected folder only! &#xA;       ○ search in the selected folder and all of its sub-folders! &#xA;   • Click the START button when everything is filed out. ",
                TextBox_ErrorMsgBox = "",

                TextBox_SortingMethod = "Sorting Methods",
                ComboBox_SortingMethods = new String[] { "Last Modefied Date", "Created Date", "Alfabetic (abc)" },

                TextBox_FileTypes = "File Types",
                ComboBox_FileTypes = LanguagesArr,

                TextBox_Language = "Language",
                ComboBox_Languages = LanguagesArr,

                TextBox_SourchPathLabel = "Sourch Path",
                btn_SourchPathButton = "Browse",

                TextBox_DestinationPathLabel = "Destination Path",
                btn_DestinationPathButton = "Browse",

                radio_SearchFolderOption = "Search in the selected folder only!",
                radio_SearchAllSubFoldersOption = "Search in the selected folder and all of its sub-folders.",

                btn_StartButton = "START",
            });

            return LM;
        }
    }
}
