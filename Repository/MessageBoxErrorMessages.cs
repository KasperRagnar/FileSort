using Models;
using System.Windows.Forms;


namespace Repository
{
    public class SystemMessageBoxes
    {
        LanguageModel ChousenLanguageList;
        LanguageSettings LS = new LanguageSettings();

        public void languageDefinition(int languageIndex)
        {
            ChousenLanguageList = LS.LanguageList(languageIndex);
        }

        public void messageBoxErrorMsg(int msgIndex)
        {
            // Makes error message (MessageBox) to the index (int) you have sent with the method. 
            MessageBox.Show(ChousenLanguageList.TextBox_ErrorMsgBox[msgIndex]);

            // Error Message index list
            // [0] = "",
            // [1] = "• Error: Not all fields are selected!",
            // [2] = "• Error: NO image files found!",
            // [3] = "• Error: Try anothe path",
            // [4] = "• Error: Something went wrong. Restart the program and try again!" 
            // [5] = "• Error: Something went wrong.Try another path! or see if all fields are filled out!"
        }
    }
}
