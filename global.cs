using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katalog
{
    [Flags]
    public enum FastFlags : ushort
    {
        FLAG1 = 0x00000001,
        FLAG2 = 0x00000002,
        FLAG3 = 0x00000004,
        FLAG4 = 0x00000008,
        FLAG5 = 0x00000010,
        FLAG6 = 0x00000020,
    }



    static class MaxInvNumbers
    {
        public static long Contact = Properties.Settings.Default.ContactStart - 1;
        public static long Item = Properties.Settings.Default.ItemStart - 1;
        public static long Book = Properties.Settings.Default.BookStart - 1;
        public static long Boardgame = Properties.Settings.Default.BoardStart - 1;
    }

    static class global
    {
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }

    
}
