using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Mohawk.Classes.Components {

    internal class BitmapComparer : IComparer<string> {

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string x, string y);

        public int Compare(string x, string y) {
            return StrCmpLogicalW(x, y);
        }

    }

}