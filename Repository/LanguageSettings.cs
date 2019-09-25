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

        public readonly string[] FileTypesArr = new String[] { ".jpg", ".jpeg", ".png", ".tif", ".tiff", ".gif", ".3fr", ".raw", ".dcr", ".cr3", ".cr2", ".erf", ".mef", ".mos", ".nef", ".orf", ".pef", ".rw2", ".arw", ".crw", ".srf", ".sr2", ".doc", ".docx", ".pdf", ".rtf", ".tex", ".txt", ".wks", ".wps", ".wpd", ".dll", ".exe" }; // 'File Type' list to the UI. og Back-end.
        public readonly string[] LanguagesArr = new String[] { "Dansk", "English" }; // 'Language' list to the UI. og Back-end.
        #endregion

        public LanguageModel LanguageList(int indexNumber)
        {
            List<LanguageModel> LM = new List<LanguageModel>();

            // Danish = Index 0
            LM.Add(new LanguageModel
            {
                TextBox_ContentTextBox = $" FileSort er et lille program der hjælper dig med at organisere dine filer. {Environment.NewLine}{Environment.NewLine}Sådan virker der:{Environment.NewLine}   • [Sprog]: Vægl et sprog.{Environment.NewLine}   • [Fil Typer]: Vælg hvad for fil typer du vil sortere.{Environment.NewLine}   • [Sorterings Metoder]: Vælg hvordan du vil sortere dine filer.{Environment.NewLine}   • [Hvor Fra ?]: Vælg hvor FileSort skal kigge efter filer. {Environment.NewLine}   • [Hvor til ?]: Vælg hvor FileSort skal lægge de sorterede filer. {Environment.NewLine}   • Husk at vælge hvor grundigt FileSort skal søger efter filer. {Environment.NewLine}       ○ Søg kun i den valgte folder! {Environment.NewLine}       ○ Søg i den valgte folder og alle dens undermapper. {Environment.NewLine}   • Klik på START knappen når alt er udfyldt. ",
                TextBox_ErrorMsgBox = new String[] { "", "• Fejl: Ikke alle felter er udfyldt!", "• Fejl: Ingen billeder filer blev fundet!", "• Fejl: Prøv en anden sti", "• Fejl: Noget gik galt. Genstart programmet og prøv igen!", $"• Fejl: Der gik noget galt. {Environment.NewLine}Prøv en anden vej! {Environment.NewLine}eller se om alle felter er udfyldt!" },

                TextBox_SortingMethod = "Sorterings Metoder",
                ComboBox_SortingMethods = new String[] { "Flyt", "Kopiere", "Sidste redigeret dato", "Skaber Dato", "Alfabetisk (abc)" },

                TextBox_FileTypes = "Fil Typer",
                btn_AddFileTypeToList = "Tilføj til listen",
                btn_RemoveFileTypeToList = "Fjern fra listen",

                TextBox_Language = "Sprog",

                TextBox_SourchPathLabel = "Hvor fra ?",
                btn_SourchPathButton = "Gennemse",

                TextBox_DestinationPathLabel = "Hvor til ?",
                btn_DestinationPathButton = "Gennemse",

                radio_SearchFolderOption = "Søg kun i den valgte folder!",
                radio_SearchAllSubFoldersOption = "Søg i den valgte folder og alle dens undermapper.",

                btn_StartButton = "START",

                MsgBoxText = new String[] { "Flytter filer Fra:", "Til:"},
                MsgBoxHeader = "Arbejder...",
            });

            // English = Index 1
            LM.Add(new LanguageModel 
            {
                TextBox_ContentTextBox = $"FileSort is a small program that helps you organiz your files. {Environment.NewLine}{Environment.NewLine}How it works:{Environment.NewLine}   • [Language]: Select a language of your choice.{Environment.NewLine}   • [File Types]: Select What file types you want to sort.{Environment.NewLine}   • [Sorting Method]: Select how you want to sort your files.{Environment.NewLine}   • [Source folder]: Select witch folder FileSort should look for files. {Environment.NewLine}   • [Destination folder]: Select where FileSort should put {Environment.NewLine}     the sorted files. {Environment.NewLine}   • Remember to choose how thoroughly FileSerch can search for files. {Environment.NewLine}       ○ search in the selected folder only! {Environment.NewLine}       ○ search in the selected folder and all of its sub-folders! {Environment.NewLine}   • Click the START button when everything is filed out. ",
                TextBox_ErrorMsgBox = new String[] { "", "• Error: Not all fields are filled out!", "• Error: NO image files found!", "• Error: Try anothe path", "• Error: Something went wrong. Restart the program and try again!", $"• Error: Something went wrong. {Environment.NewLine}Try another path! {Environment.NewLine}or see if all fields are filled out!" },

                TextBox_SortingMethod = "Sorting Methods",
                ComboBox_SortingMethods = new String[] { "Move", "Copy", "Last Modefied Date", "Created Date", "Alfabetic (abc)" },

                TextBox_FileTypes = "File Types",
                btn_AddFileTypeToList = "Add to list",
                btn_RemoveFileTypeToList = "Remove from list",

                TextBox_Language = "Language",

                TextBox_SourchPathLabel = "Sourch Path",
                btn_SourchPathButton = "Browse",

                TextBox_DestinationPathLabel = "Destination Path",
                btn_DestinationPathButton = "Browse",

                radio_SearchFolderOption = "Search in the selected folder only!",
                radio_SearchAllSubFoldersOption = "Search in the selected folder and all of its sub-folders.",

                btn_StartButton = "START",

                MsgBoxText = new String[] { "Moving Files from:", "To:"},
                MsgBoxHeader = "Loading...",
            });


            return LM[indexNumber];
        }
    }
}
