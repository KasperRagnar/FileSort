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
        float renameCounter = 0;                                                // Used for giving a number to filenames if there are more then one file with the same name

        FileInfo[] filesInfoArr;                                                // An empty array used for temporarily hold information about individual files

        FolderBrowserDialog fbd = new FolderBrowserDialog();
        DoFileExistCheck DFEC = new DoFileExistCheck();
        MessageBoxErrorMessages MBEM = new MessageBoxErrorMessages();
        FileMovers FM = new FileMovers();
        #endregion

        /// <summary>
        /// Moves found files from one folder to another
        /// </summary>
        /// <param name="selectedPath">Where files are comming from</param>
        /// <param name="destPathFolder">Where files are going</param>
        /// <param name="searchResult">Found files in form of a array of strings</param>
        public void Move(string selectedPath, string destPathFolder, String[] searchResult) 
        {
            try
            {
                // adds every files fileinfo to a 'FileInfo' array
                foreach (var filePath in searchResult)
                {
                    var dir = new DirectoryInfo(Path.GetDirectoryName(filePath));
                    filesInfoArr = dir.GetFiles();
                }

                // sorting all files in the global 'movedFilesArr' array.
                for (int i = 0; i < filesInfoArr.Length; i++)
                {
                    FileInfo file = filesInfoArr[i];                                           // The current file element in the array

                    FM.MovingFiles(destPathFolder, file);                                      // Moves files from one place to another, checks if files already exists, makes the 'fullDestination' path if it does not already exists
                }

                filesInfoArr = null;                                                           // clears the 'FileInfo' array
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Copies found files from one folder to another
        /// </summary>
        /// <param name="selectedPath">Where files are comming from</param>
        /// <param name="destPathFolder">Where files are going</param>
        /// <param name="searchResult">Found files in form of a array of strings</param>
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
                filesInfoArr = null; // clears the 'FileInfo' array
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LastModefiedDate(string selectedPath, string destPath, string destPathFolder, String[] searchResult)
        {
        
        }

        public void CreatedDate()
        {

        }

        public void Alfabetic(string selectedPath, string destPathFolder, String[] searchResult)
        {

            #region Arrays of (numbers, letters, symbols)
            string[] numbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "æ", "ø", "å", "ä", "ö" };
            string[] symbols = { "!", "'", "@", "#", "£", "¤", "$", "%", "€", "&", "{", "}", "[", "]", "(", ")", "=", "+", "-", "_", ";", ".", ",", "¨", "^", "~", "§", "½", "∞", "≈", "≡", "µ" };
            #endregion 

            try
            {
                // adds every files fileinfo to a 'FileInfo' array
                foreach (var filePath in searchResult)
                {
                    var dir = new DirectoryInfo(Path.GetDirectoryName(filePath));
                    filesInfoArr = dir.GetFiles();
                }

                // sorting all files in the global 'movedFilesArr' array.
                for (int i = 0; i < filesInfoArr.Length; i++)
                {
                    FileInfo file = filesInfoArr[i];                                                    // The current file element in the array

                    #region Splits the name up in a char array to sortis by the first char
                    char[] nameSplatter = file.Name.ToCharArray();                                      // Splits the name up in a char array
                    string firstInName = nameSplatter[0].ToString();                                    // Sets 'firstInName' equal to the first letter/number/sumbol in the 'nameSplatter'
                    #endregion

                    string fullDestination;                                                             // A placeholder for the file destination path, for the individual scenarios
                    bool runChecker = false;                                                            // Used for checking if the 'firstInName' value already has been found in a previus foreach loop

                    #region Check if 'firstInName' starts with a letter, number or a symbol
                    // if 'firstInName' starts with a letter
                    foreach (var letter in letters)
                    {
                        if (runChecker == true)                                                         // prevents the program for running through all of the foreach loop
                        {
                            break;
                        } 

                        else if (letter == firstInName)
                        {
                            fullDestination = destPathFolder + "\\" + firstInName.ToUpper();            // Full directory path for letters

                            FM.MovingFiles(fullDestination, file);                                      // Moves files from one place to another, checks if files already exists, makes the 'fullDestination' path if it does not already exists

                            runChecker = true;                                                          // Sets 'runChecker' to true so it does not run the other foreach loop checks
                            break;                                                                      // Breaks out of the Foreach loop after the first match 
                        }
                    }

                    // if 'firstInName' starts with a nummer
                    foreach (var number in numbers)
                    {
                        if (runChecker == true)                                                         // prevents the program for running through all of the foreach loop
                        {
                            break;
                        } 

                        else if (number == firstInName)
                        {
                            fullDestination = destPathFolder + "\\" + "Numbers";                        // Full directory path for numbers

                            FM.MovingFiles(fullDestination, file);                                      // Moves files from one place to another, checks if files already exists, makes the 'fullDestination' path if it does not already exists

                            runChecker = true;                                                          // Sets 'runChecker' to true so it does not run the other foreach loop checks
                            break;                                                                      // Breaks out of the Foreach loop after the first match 
                        }
                    }

                    // if 'firstInName' starts with a symbol
                    foreach (var symbol in symbols)
                    {
                        if (runChecker == true)                                                         // prevents the program for running through all of the foreach loop
                        {
                            break;
                        } 

                        else if (symbol == firstInName)
                        {
                            fullDestination = destPathFolder + "\\" + "Symbols";                        // Full directory path for symbols 

                            FM.MovingFiles(fullDestination, file);                                      // Moves files from one place to another, checks if files already exists, makes the 'fullDestination' path if it does not already exists

                            runChecker = true;                                                          // Sets 'runChecker' to true so it does not run the other foreach loop checks
                            break;                                                                      // Breaks out of the Foreach loop after the first match 
                        }
                    }
                    #endregion
                }

                filesInfoArr = null;                                                                    // clears the 'FileInfo' array
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
