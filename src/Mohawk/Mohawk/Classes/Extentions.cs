using System;

namespace Mohawk.Classes {

    internal static class Extentions {

        public static bool ContainsStringCount(this string input, string pattern, int count) {
            var a = 0;
            var b = 0;
            while ((b = input.IndexOf(pattern, b, StringComparison.Ordinal)) != -1) {
                b += pattern.Length;
                a++;
            }
            return a == count;
        }

    }

}