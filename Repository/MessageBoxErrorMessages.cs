using Models;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Repository
{
    public class SystemMessageBoxes
    {
        #region GLOBAL
        LanguageModel ChousenLanguageList;
        LanguageSettings LS = new LanguageSettings();
        #endregion


        /// <summary>
        /// Sets the language index equal to 'ChousenLanguageList'
        /// </summary>
        /// <param name="languageIndex">the chousen index of the language list</param>
        public void LanguageDefinition(int languageIndex)
        {
            ChousenLanguageList = LS.LanguageList(languageIndex);
        }


        /// <summary>
        /// Overview and handeling of all error messages
        /// </summary>
        /// <param name="msgIndex">Index of the found error message</param>
        public void MessageBoxErrorMsg(int msgIndex)
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


        public void MessageBoxProgressBar(string msgText, string msgHeader, CancellationTokenSource cts)
        {
            DialogResult dialogResult = MessageBox.Show(msgText, msgHeader, MessageBoxButtons.OKCancel, MessageBoxIcon.None);
            if (dialogResult == DialogResult.Cancel)
            {
                cts.Cancel();                           // Cansels all CancellationTokens on this thread


                // 'MessageBoxButtons.OKCancel' should be changed 
                // to a costum Message Box Button 'MessageBoxButtons.Cansel'
            }
            else
            {

            }
        }

    }
}
