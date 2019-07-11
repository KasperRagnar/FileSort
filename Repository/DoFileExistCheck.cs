using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository
{
    public class DoFileExistCheck
    {
        /// <summary>
        /// Check If File Already Exist 
        /// </summary>
        /// <param name="destinationFolder">Destination folder path</param>
        /// <param name="filename">individual files</param>
        /// <returns></returns>
        public bool CheckIfFileAlreadyExist(string destinationFolder, string filename)
        {
            string[] files = Directory.GetFiles(destinationFolder, filename);
            if (files.Length == 0)
            {
                // the file does not exist
                return false;
            }
            else
            {
                // the file does exist
                return true;
            }
        }
    }
}
