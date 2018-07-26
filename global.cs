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

    public enum ItemTypes { item = 0, book = 1, boardgame = 2 }
    public enum LendStatus { Reserved = 0, Lended = 1, Returned = 2, Canceled = 3 }

    public class CInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalNum { get; set; }
    }

    public class IInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string InvNum { get; set; }
        public short Count { get; set; }
        public short Available { get; set; }
        public int ItemNum { get; set; }
        public ItemTypes ItemType { get; set; }
        public string Note { get; set; }
    }

    public class LInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string InvNum { get; set; }
        public ItemTypes ItemType { get; set; }
        public string Note { get; set; }
        public DateTime LendFrom { get; set; }
        public DateTime LendTo { get; set; }
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

        public static string GetLendingItemName(string type, Guid id)
        {
            databaseEntities db = new databaseEntities();

            switch (type)
            {
                case "item":
                    Items itm = db.Items.Find(id);
                    if (itm != null) return itm.Name.Trim();
                    break;
                case "book":
                    Books book = db.Books.Find(id);
                    if (book != null) return book.Title.Trim();
                    break;
                case "boardgame":
                    Boardgames board = db.Boardgames.Find(id);
                    if (board != null) return board.Name.Trim();
                    break;
            }
            return "";
        }

        public static string GetItemTypeName(ItemTypes type)
        {
            switch (type)
            {
                case ItemTypes.item:
                    return Lng.Get("Item");
                case ItemTypes.book:
                    return Lng.Get("Book");
                case ItemTypes.boardgame:
                    return Lng.Get("Boardgame", "Board game");
                default:
                    return Lng.Get("Unknown");
            }
        }

        public static string GetItemTypeName(string type)
        {
            type = type.Trim();
            switch (type)
            {
                case "item":
                    return Lng.Get("Item");
                case "book":
                    return Lng.Get("Book");
                case "boardgame":
                    return Lng.Get("Boardgame", "Board game");
                default:
                    return Lng.Get("Unknown");
            }
        }

        public static string GetStatusName(short status)
        {
            if (status == (short)LendStatus.Returned)       // Returned
                return Lng.Get("Returned");
            else if (status == (short)LendStatus.Reserved)  // Reserved
                return Lng.Get("Reserved");
            else if (status == (short)LendStatus.Canceled)  // Canceled
                return Lng.Get("Canceled");
            else if(status == (short)LendStatus.Lended)     // Lended
                return Lng.Get("Lended");
            else return Lng.Get("Unknown");                 // Unknown
        }

        public static string[,] GetTable(List<Lending> lendList)
        {
            if (lendList == null) return null;

            string[,] tab = new string[lendList.Count + 2, 6];

            tab[0, 0] = "1cm";
            tab[0, 1] = "2cm";
            tab[0, 2] = "6.5cm";
            tab[0, 3] = "2cm";
            tab[0, 4] = "2cm";
            tab[0, 5] = "2.5cm";

            tab[1, 0] = Lng.Get("Number");
            tab[1, 1] = Lng.Get("Type");
            tab[1, 2] = Lng.Get("ItemName", "Name");
            tab[1, 3] = Lng.Get("From", "From");
            tab[1, 4] = Lng.Get("To", "To");
            tab[1, 5] = Lng.Get("Status");

            for (int i = 0; i < lendList.Count; i++)
            {
                tab[i + 2, 0] = (i + 1).ToString();
                tab[i + 2, 1] = global.GetItemTypeName(lendList[i].ItemType);
                tab[i + 2, 2] = global.GetLendingItemName(lendList[i].ItemType.Trim(), lendList[i].ItemID ?? Guid.Empty);
                tab[i + 2, 3] = (lendList[i].From ?? DateTime.Now).ToShortDateString();
                tab[i + 2, 4] = (lendList[i].To ?? DateTime.Now).ToShortDateString();
                tab[i + 2, 5] = global.GetStatusName(lendList[i].Status ?? 1);
            }

            return tab;
            
        }
    }

    
}
