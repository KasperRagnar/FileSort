using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository
{
    public class SearchAndFindFiles
    {
        /// <summary>
        /// Puts together search terms and finds a Array of images
        /// </summary>
        /// <param name="searchFolder">a folder path</param>
        /// <param name="filters">type/format of images you want to find</param>
        /// <param name="isRecursive">search terms</param>
        /// <returns>A Array af strings</returns>
        public String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();


            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly; // true | false

            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format($"*{filter}"), searchOption)); // sammensetning af søgelogig
            }

            return filesFound.ToArray();
        }
    }
}
