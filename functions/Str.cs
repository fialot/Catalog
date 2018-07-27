using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFunctions
{
    /// <summary>
    /// String functions
    /// Version:    1.0
    /// Date:       2018-07-27
    /// </summary>
    static class Str
    {
        /// <summary>
        /// Get first line in text (and remove it in input string)
        /// </summary>
        /// <param name="text">Input text</param>
        /// <param name="remove">Remove line from Input text</param>
        /// <returns></returns>
        public static string GetFirstLine(ref string text, bool remove)
        {
            string firstline = "";

            // ----- Get endline position -----
            int position = text.IndexOf(Environment.NewLine);

            // ----- If more lines -----
            if (position >= 0)
            {
                // ----- Get Firstline -----
                firstline = text.Substring(0, position);

                // ----- Remove this line in text -----
                if (remove)
                    text = text.Remove(0, position + Environment.NewLine.Length);
            }
            // ----- If 1 line -----
            else
            {
                firstline = text;
                text = "";
            }
            
            // ----- Return firstline -----
            return firstline;
        }

    }
}
