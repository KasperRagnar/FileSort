using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Repository
{
    public class FileMovers
    {
        #region GLOBAL 
        float renameCounter = 0;                                                // Used for giving a number to filenames if there are more then one file with the same name
        FileInfo[] filesInfoArr;                                                // An empty array used for temporarily hold information about individual files 

        DoFileExistCheck DFEC = new DoFileExistCheck(); 
        #endregion

        public void MovingFiles(string fullDestination, FileInfo file)
        {
            try
            {
                #region Directory
                // Creates a new directory if the given directory does not yet exist
                if (!Directory.Exists(fullDestination))
                {
                    Directory.CreateDirectory(fullDestination);
                }
                #endregion

                #region Moving file 
                // Moves file to new folder
                if (Directory.Exists(fullDestination))
                {
                    bool check1 = DFEC.CheckIfFileAlreadyExist(fullDestination, file.Name);                     // checks if the file allready exists in the destination folder

                    // Change the name of the file and then moves it. if the file allready exists in the destination folder
                    if (check1 == true)
                    {
                        string[] fileNameArr = file.Name.Split('.');                                            // Seperate filename and it's file type

                        string NewfileName;
                        bool check2 = true;

                        do // If the filename already exists in this directory. then add or increment a number in front of the filename to get a unique filename
                        {
                            NewfileName = fileNameArr[0] + "(" + ++renameCounter + ")" + "." + fileNameArr[1];  // A new complete filename with a filetype
                            check2 = DFEC.CheckIfFileAlreadyExist(fullDestination, NewfileName);

                        } while (check2);

                        Directory.Move(file.FullName, fullDestination + "\\" + NewfileName);               // Moves a file from one dir to another
                    }

                    // Moves the file. if the file does not exists in the destination folder
                    else
                    {
                        Directory.Move(file.FullName, fullDestination + "\\" + file.Name);                 // Moves a file from one dir to another
                    }

                    renameCounter = 0;                                                                      // resets the counter for future use
                }
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
            
        }

    }
}
