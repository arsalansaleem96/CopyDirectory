using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyDirectory.Copier
{
    /// <summary>
    /// The class to implement ICopy
    /// </summary>
    public class Copy : ICopy
    {
        public event CopyingProcess Process;

        public ResponseMessage CopyDirectory(string source, string target)
        {
            try
            {
                if (Equals(source.ToLower(),target.ToLower()))
                    return ResponseMessage.SAME;

                if (!Directory.Exists(target))
                    Directory.CreateDirectory(target);

                DirectoryInfo dirSource = new DirectoryInfo(source);
                DirectoryInfo dirTarget = new DirectoryInfo(target);

                CopyFiles(ref dirSource, ref dirTarget);

                CopySubDirectories(ref dirSource, ref dirTarget);

                return ResponseMessage.SUCCESS;
            }
            catch (Exception ex)
            {
                Process?.Invoke(ex.Message);
                return ResponseMessage.ERROR;
            }
        }
        /// <summary>
        /// Local method to Copy files. 
        /// </summary>
        /// <param name="dirSource">Pass by refrence.</param>
        /// <param name="dirTarget">Pass by refrence.</param>
        private void CopyFiles(ref DirectoryInfo dirSource, ref DirectoryInfo dirTarget)
        {
            foreach (FileInfo fileInfo in dirSource.GetFiles())
            {
                Process?.Invoke(string.Format(@"Copying {0}\{1}.", dirTarget.FullName, fileInfo.Name));
                fileInfo.CopyTo(Path.Combine(dirTarget.FullName, fileInfo.Name), true);
                Process?.Invoke(string.Format(@"Copied {0}\{1} successfully.", dirTarget.FullName, fileInfo.Name));
            }
        }
        /// <summary>
        /// Local method to Copy sub directories and makes recursive calls to main method. 
        /// </summary>
        /// <param name="dirSource">Pass by refrence.</param>
        /// <param name="dirTarget">Pass by refrence.</param>
        private void CopySubDirectories(ref DirectoryInfo dirSource, ref DirectoryInfo dirTarget)
        {
            foreach (DirectoryInfo subDirectory in dirSource.GetDirectories())
            {
                DirectoryInfo newSubDirectory = dirTarget.CreateSubdirectory(subDirectory.Name);
                CopyDirectory(subDirectory.FullName, newSubDirectory.FullName); 
            }
        }
    }
}
