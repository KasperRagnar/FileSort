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
        int renameCounter = 0;
        FileInfo[] movedFilesArr;
        List<string> pathFileNameList;

        FolderBrowserDialog fbd = new FolderBrowserDialog();
        DoFileExistCheck DFEC = new DoFileExistCheck();
        #endregion

        public void Move(string selectedPath, string destPath, string destPathFolder, String[] searchResult)
        {
            try
            {
                foreach (var filePath in searchResult)
                {
                    // Gets the Directory Name from 'filePath' and adds the filename to 'movedFilesArr'
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

                        System.IO.Directory.Move(filePath, destPathFolder + "\\" + NewfileName);
                    }
                    else  // if the file does not already exists
                    {
                        // Moves the Found file to the Destination Path
                        //file.MoveTo(destPathFolder + "\\" + file);
                        System.IO.Directory.Move(filePath, destPathFolder + "\\" + file);
                    }
                    
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void Copy()
        {

        }

        public void LastModefiedDate()
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
