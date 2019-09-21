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
            try
            {
                foreach (var filePath in searchResult)
                {
                    #region Splitting up the filepath and file name for better forting
                    // Gets the Directory Name from 'filePath' and split it up
                    string[] dir = Path.GetDirectoryName(filePath + "\\").Split(Path.DirectorySeparatorChar);
                    Array.Reverse(dir);

                    string file = dir[0]; // filens originale navn
                     
                    // Splits the name up in a char array to sortis by the first char
                    char[] nameSplatter = file.ToCharArray();
                    string firstInName = nameSplatter[0].ToString();
                    #endregion 

                    #region Arrays of (numbers, letters, symbols)
                    string[] numbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v" , "w", "x", "y", "z", "æ", "ø", "å", "ä", "ö" };
                    string[] symbols = { "!", "'", "@", "#", "£", "¤", "$", "%", "€", "&", "{", "}", "[", "]", "(", ")", "=", "+", "-", "_", ";", ".", ",", "¨", "^", "~", "§", "½", "∞", "≈", "≡", "µ" };
                    #endregion

                    #region Sorting What comes first in 'firstInName'
                    bool runChecker = false;                    // Used for checking if the 'firstInName' value already has been found in a previus foreach loop

                    // if 'firstInName' starts with a letter
                    foreach (var letter in letters)
                    {
                        if (runChecker == true)                 // prevents the program for running through all of the foreach loop
                        {
                            break;
                        }
                        else if (letter == firstInName)
                        {
                            // Check if a folder with the 'firstInName' character allready exists 
                            // if not make one
                            // then move the file to the correct folder

                            runChecker = true;
                            break;
                        }
                    }

                    // if 'firstInName' starts with a nummer
                    foreach (var number in numbers)
                    {
                        if (runChecker == true)                 // prevents the program for running through all of the foreach loop
                        {
                            break;
                        }
                        else if (number == firstInName)
                        {
                            // Check if a folder with the 'firstInName' character allready exists 
                            // if not make one
                            // then move the file to the correct folder

                            runChecker = true;
                            break;
                        }
                    }

                    // if 'firstInName' starts with a symbol
                    foreach (var symbol in symbols)
                    {
                        if (runChecker == true)                 // prevents the program for running through all of the foreach loop
                        {
                            break;
                        }
                        else if (symbol == firstInName)
                        {
                            // Check if a folder with the 'firstInName' character allready exists 
                            // if not make one
                            // then move the file to the correct folder

                            runChecker = true;
                            break;
                        }
                    }
                    #endregion

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
