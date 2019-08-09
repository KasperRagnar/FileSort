using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Repository
{
    public class SortingMethods
    {
        #region GLOBAL 
        float renameCounter = 0;

        FileInfo movedFilesArr;

        FolderBrowserDialog fbd = new FolderBrowserDialog();
        DoFileExistCheck DFEC = new DoFileExistCheck();
        MessageBoxErrorMessages MBEM = new MessageBoxErrorMessages();
        #endregion

        public void Move(string selectedPath, string destPathFolder, String[] searchResult)
        {
            try
            {
                foreach (var filePath in searchResult)
                {
                    // Gets the Directory Name from 'filePath' and split it up
                    string[] dir = Path.GetDirectoryName(filePath + "\\").Split(Path.DirectorySeparatorChar);
                    Array.Reverse(dir);

                    string file = dir[0]; // filens originale navn

                    // checks if the file allready exists in the destination folder
                    bool check1 = DFEC.CheckIfFileAlreadyExist(destPathFolder, file);

                    if (check1 == true) // if the file already exists
                    {
                        string[] fileNameArr = file.Split('.'); // Seperate filename and it's file type

                        bool check2;
                        string NewfileName;
                        do
                        {
                            NewfileName = fileNameArr[0] + "(" + ++renameCounter + ")" + "." + fileNameArr[1]; // A new complete filename with a filetype
                            check2 = DFEC.CheckIfFileAlreadyExist(destPathFolder, NewfileName);

                        } while (check2 == true);

                        Directory.Move(filePath, destPathFolder + "\\" + NewfileName);
                        renameCounter = 0;
                    }
                    else  // if the file does not already exists
                    {
                        Directory.Move(filePath, destPathFolder + "\\" + file);
                    }

                }
            }
            catch (Exception)
            {
                MBEM.messageBoxErrorMsg(5); // A predefined error message
                throw;
            }
        }

        public void Copy(string selectedPath, string destPathFolder, String[] searchResult)
        {
            try
            {
                foreach (var filePath in searchResult)
                {
                    // Gets the Directory Name from 'filePath' and split it up
                    string[] dir = Path.GetDirectoryName(filePath + "\\").Split(Path.DirectorySeparatorChar);
                    Array.Reverse(dir);

                    string file = dir[0]; // filens originale navn

                    // checks if the file allready exists in the destination folder
                    bool check1 = DFEC.CheckIfFileAlreadyExist(destPathFolder, file);

                    if (check1 == true) // if the file already exists
                    {
                        string[] fileNameArr = file.Split('.'); // Seperate filename and it's file type

                        bool check2;
                        string NewfileName;
                        do
                        {
                            NewfileName = fileNameArr[0] + "(" + ++renameCounter + ")" + "." + fileNameArr[1]; // A new complete filename with a filetype
                            check2 = DFEC.CheckIfFileAlreadyExist(destPathFolder, NewfileName);

                        } while (check2 == true);

                        File.Copy(filePath, destPathFolder + "\\" + NewfileName);
                        renameCounter = 0;
                    }
                    else  // if the file does not already exists
                    {
                        File.Copy(filePath, destPathFolder + "\\" + file);
                    }
                }
            }
            catch (Exception)
            {
                MBEM.messageBoxErrorMsg(5); // A predefined error message
                throw;
            }
        }

        public void LastModefiedDate(string selectedPath, string destPath, string destPathFolder, String[] searchResult)
        {
        
        }

        public void CreatedDate()
        {

        }

        public void Alfabetic()
        {

        }
    }
}
