using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyDirectory.Copier
{
    public delegate void CopyingProcess(string message);
    /// <summary>
    /// The interface for CopyDirectory
    /// </summary>
    public interface ICopy
    {
        /// <summary>
        /// Event to display each file being copied during the copying process.
        /// </summary>
        event CopyingProcess Process;
        /// <summary>
        /// Copy all files and folders and their contents from the source to the target.
        /// </summary>
        /// <param name="source">Required</param>
        /// <param name="target">Required</param>
        /// <returns>Return ResponseMessage enum</returns>
        ResponseMessage CopyDirectory(string source, string target);
    }
}
