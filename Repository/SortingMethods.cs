using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Repository
{
    public class SortingMethods
    {
        #region GLOBAL 
        float renameCounter = 0;                                                // Used for giving a number to filenames if there are more then one file with the same name

        FileInfo[] filesInfoArr;                                                // An empty array used for temporarily hold information about individual files

        DoFileExistCheck DFEC = new DoFileExistCheck();
        FileMovers FM = new FileMovers();
        #endregion

        // TODO: 
        //  • Få progressbaren til at lukke automatisk når progress rammer 100%
        //
        //  • Find ud af hvorfor dette regnestykke ikke vil slutte på 100% i progressbaren. 

        // Udregner hvor langet programmet er i processen, og omdanner det til procenter (%) 
        // progressObserver.Report(new ProgressReportModel { PercentageCompleted = (i* 100) / filesInfoArr.Length});


        /// <summary>
        /// Moves found files from one folder to another
        /// </summary>
        /// <param name="destPathFolder">Where files are going</param>
        /// <param name="filesFoundInSearch">Found files in form of a array of strings</param>
        public Task Move(IProgress<ProgressReportModel> progressObserver, string destPathFolder, string[] filesFoundInSearch, CancellationToken ct) 
        {
            try
            {
                return Task.Run(() =>
                {
                    // adds every files fileinfo to a 'FileInfo' array
                    foreach (var filePath in filesFoundInSearch)
                    {
                        ct.ThrowIfCancellationRequested();                                         // CanselationToken 
                        var dir = new DirectoryInfo(Path.GetDirectoryName(filePath));
                        filesInfoArr = dir.GetFiles();
                    }

                    // sorting all files in the global 'movedFilesArr' array.
                    for (int i = 0; i < filesInfoArr.Length; i++)
                    {
                        FileInfo file = filesInfoArr[i];                                           // The current file element in the array

                        ct.ThrowIfCancellationRequested();                                         // CanselationToken til at stoppe flytning af filer

                        FM.MovingFiles(destPathFolder, file);                                      // Moves files from one place to another, checks if files already exists, makes the 'fullDestination' path if it does not already exists

                        // Udregner hvor langet programmet er i processen, og omdanner det til procenter (%) 
                        progressObserver.Report(new ProgressReportModel { PercentageCompleted = (i * 100) / filesInfoArr.Length });
                    }

                    filesInfoArr = null;                                                           // clears the 'FileInfo' array
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Copies found files from one folder to another
        /// </summary>
        /// <param name="destPathFolder">Where files are going</param>
        /// <param name="filesFoundInSearch">Found files in form of a array of strings</param>
        public Task Copy(IProgress<ProgressReportModel> progressObserver, string destPathFolder, string[] filesFoundInSearch, CancellationToken ct)
        {
            try
            {
                return Task.Run(() =>
                {
                    // adds every files fileinfo to a 'FileInfo' array
                    foreach (var filePath in filesFoundInSearch)
                    {
                        ct.ThrowIfCancellationRequested();                                          // CanselationToken 
                        var dir = new DirectoryInfo(Path.GetDirectoryName(filePath));
                        filesInfoArr = dir.GetFiles();
                    }

                    // sorting all files in the global 'movedFilesArr' array.
                    for (int i = 0; i < filesInfoArr.Length; i++)
                    {
                        FileInfo file = filesInfoArr[i];                                            // The current file element in the array

                        ct.ThrowIfCancellationRequested();                                          // CanselationToken til at stoppe kopiering af filer

                        // checks if the file allready exists in the destination folder
                        bool check1 = DFEC.CheckIfFileAlreadyExist(destPathFolder, file.Name);

                        if (check1 == true)                                                         // if the file already exists
                        {
                            string[] fileNameArr = file.Name.Split('.');                            // Seperate filename and it's file type

                            string NewfileName;
                            bool check2 = true;

                            do
                            {
                                NewfileName = fileNameArr[0] + "(" + ++renameCounter + ")" + "." + fileNameArr[1];  // A new complete filename with a filetype
                                check2 = DFEC.CheckIfFileAlreadyExist(destPathFolder, NewfileName);                 // Checks if the new filename exists in destination folder

                            } while (check2);

                            ct.ThrowIfCancellationRequested();                                      // CanselationToken til at stoppe kopiering af filer
                            File.Copy(file.FullName, destPathFolder + "\\" + NewfileName);
                        }
                        else                                                                        // if the file does not already exists
                        {
                            ct.ThrowIfCancellationRequested();                                      // CanselationToken til at stoppe kopiering af filer
                            File.Copy(file.FullName, destPathFolder + "\\" + file.Name);
                        }

                        renameCounter = 0;                                                          // resets the counter for future use

                        // Udregner hvor langet programmet er i processen, og omdanner det til procenter (%) 
                        progressObserver.Report(new ProgressReportModel { PercentageCompleted = (i * 100) / filesInfoArr.Length });
                    }

                    filesInfoArr = null;                                                            // clears the 'FileInfo' array
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sorts files baced on when they where last modefied
        /// </summary>
        /// <param name="destPathFolder">Where files are going</param>
        /// <param name="filesFoundInSearch">Found files in form of a array of strings</param>
        public Task LastModefiedDate(IProgress<ProgressReportModel> progressObserver, string destPathFolder, string[] filesFoundInSearch, CancellationToken ct)
        {
            try
            {
                return Task.Run(() =>
                {
                    // adds every files fileinfo to a 'FileInfo' array
                    foreach (var filePath in filesFoundInSearch)
                    {
                        ct.ThrowIfCancellationRequested();                                                  // CanselationToken 
                        var dir = new DirectoryInfo(Path.GetDirectoryName(filePath));
                        filesInfoArr = dir.GetFiles();
                    }

                    // sorting all files in the global 'movedFilesArr' array.
                    for (int i = 0; i < filesInfoArr.Length; i++)
                    {
                        FileInfo file = filesInfoArr[i];                                                    // The current file element in the array

                        // fullDestination = The crrent files last Modefied date (YYY,MM,DD) 
                        string fullDestination = Path.Combine(destPathFolder, file.LastWriteTime.Year.ToString(), file.LastWriteTime.Month.ToString(), file.LastWriteTime.Day.ToString());

                        ct.ThrowIfCancellationRequested();                                                  // CanselationToken 

                        FM.MovingFiles(fullDestination, file);                                              // Moves files from one place to another, checks if files already exists, makes the 'fullDestination' path if it does not already exists

                        // Udregner hvor langet programmet er i processen, og omdanner det til procenter (%) 
                        progressObserver.Report(new ProgressReportModel { PercentageCompleted = (i * 100) / filesInfoArr.Length });
                    }

                    filesInfoArr = null;                                                                    // clears the 'FileInfo' array
                });
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        /// Sorts files baced on when they where created
        /// </summary>
        /// <param name="destPathFolder">Where files are going</param>
        /// <param name="filesFoundInSearch">Found files in form of a array of strings</param>
        public Task CreatedDate(IProgress<ProgressReportModel> progressObserver, string destPathFolder, string[] filesFoundInSearch, CancellationToken ct)
        {
            try
            {
                return Task.Run(() =>
                {
                    // adds every files fileinfo to a 'FileInfo' array
                    foreach (var filePath in filesFoundInSearch)
                    {
                        ct.ThrowIfCancellationRequested();                                                  // CanselationToken 
                        var dir = new DirectoryInfo(Path.GetDirectoryName(filePath));
                        filesInfoArr = dir.GetFiles();
                    }

                    // sorting all files in the global 'movedFilesArr' array.
                    for (int i = 0; i < filesInfoArr.Length; i++)
                    {
                        FileInfo file = filesInfoArr[i];                                                    // The current file element in the array

                        // fullDestination = The crrent files Creation date
                        string fullDestination = Path.Combine(destPathFolder, file.CreationTime.Year.ToString(), file.CreationTime.Month.ToString(), file.CreationTime.Day.ToString());

                        ct.ThrowIfCancellationRequested();                                                  // CanselationToken 

                        FM.MovingFiles(fullDestination, file);                                              // Moves files from one place to another, checks if files already exists, makes the 'fullDestination' path if it does not already exists

                        // Udregner hvor langet programmet er i processen, og omdanner det til procenter (%) 
                        progressObserver.Report(new ProgressReportModel { PercentageCompleted = (i * 100) / filesInfoArr.Length });
                    }

                    filesInfoArr = null;                                                                    // clears the 'FileInfo' array
                });
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        /// <summary>
        /// Sorts files by letters, numbers and symbols
        /// </summary>
        /// <param name="destPathFolder">Where files are going</param>
        /// <param name="filesFoundInSearch">Found files in form of a array of strings</param>
        public Task Alfabetic(IProgress<ProgressReportModel> progressObserver, string destPathFolder, string[] filesFoundInSearch, CancellationToken ct)
        {

            #region Arrays of (numbers, letters, symbols)
            string[] numbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "æ", "ø", "å", "ä", "ö" };
            string[] symbols = { "!", "'", "@", "#", "£", "¤", "$", "%", "€", "&", "{", "}", "[", "]", "(", ")", "=", "+", "-", "_", ";", ".", ",", "¨", "^", "~", "§", "½", "∞", "≈", "≡", "µ" };
            #endregion 

            try
            {
                return Task.Run(() =>
                {
                    // adds every files fileinfo to a 'FileInfo' array
                    foreach (var filePath in filesFoundInSearch)
                    {
                        ct.ThrowIfCancellationRequested();                                                  // CanselationToken 
                        var dir = new DirectoryInfo(Path.GetDirectoryName(filePath));
                        filesInfoArr = dir.GetFiles();
                    }

                    // sorting all files in the global 'movedFilesArr' array.
                    for (int i = 0; i < filesInfoArr.Length; i++)
                    {
                        FileInfo file = filesInfoArr[i];                                                    // The current file element in the array

                        ct.ThrowIfCancellationRequested();                                                  // CanselationToken 

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

                            else if (letter == firstInName.ToLower())
                            {
                                fullDestination = Path.Combine(destPathFolder, firstInName.ToUpper());      // Full directory path for letters

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

                            else if (number == firstInName.ToLower())
                            {
                                fullDestination = Path.Combine(destPathFolder, "[Numbers]");                  // Full directory path for numbers

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

                            else if (symbol == firstInName.ToLower())
                            {
                                fullDestination = Path.Combine(destPathFolder, "[Symbols]");                  // Full directory path for symbols 

                                FM.MovingFiles(fullDestination, file);                                      // Moves files from one place to another, checks if files already exists, makes the 'fullDestination' path if it does not already exists

                                runChecker = true;                                                          // Sets 'runChecker' to true so it does not run the other foreach loop checks
                                break;                                                                      // Breaks out of the Foreach loop after the first match 
                            }
                        }
                        #endregion

                        // Udregner hvor langet programmet er i processen, og omdanner det til procenter (%) 
                        progressObserver.Report(new ProgressReportModel { PercentageCompleted = (i * 100) / filesInfoArr.Length });
                    }

                    filesInfoArr = null;                                                                    // clears the 'FileInfo' array
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
