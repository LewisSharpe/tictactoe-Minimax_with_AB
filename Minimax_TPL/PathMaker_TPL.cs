using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax_TPL
{
    class PathMaker_TPL
    {
        // GET RELATIVE PATH
        public static string GetRelativePath(string filespec, string folder)
        {
            folder = Environment.CurrentDirectory;
            filespec = "TPLTST_Report.csv";
            Uri pathUri = new Uri(filespec, UriKind.Relative);
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folder += Path.DirectorySeparatorChar;
            }
            Uri folderUri = new Uri(folder);
            return folder + "/" + filespec;
        }
    }
}
