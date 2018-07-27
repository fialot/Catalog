using myFunctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
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

        #region Translate Type Names

        /// <summary>
        /// Get Item Type Name from ItemType
        /// </summary>
        /// <param name="type">Item Type</param>
        /// <returns>Item Type Name</returns>
        public static string GetItemTypeName(ItemTypes type)
        {
            string strType = type.ToString();
            return GetItemTypeName(strType);
        }
        
        /// <summary>
        /// Get Item Type Name from ItemType
        /// </summary>
        /// <param name="type">Item Type</param>
        /// <returns>Item Type Name</returns>
        public static string GetItemTypeName(string type)
        {
            type = type.Trim();
            switch (type)
            {
                case "item":                    // Item
                    return Lng.Get("Item");
                case "book":                    // Book
                    return Lng.Get("Book");
                case "boardgame":               // Board game
                    return Lng.Get("Boardgame", "Board game");
                default:                        // Unknown
                    return Lng.Get("Unknown");
            }
        }

        /// <summary>
        /// Get Status name from Status number
        /// </summary>
        /// <param name="status">Status number</param>
        /// <returns>Status name</returns>
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

        #endregion

        #region Tables for PDF

        /// <summary>
        /// Create Lending table for PDF document
        /// </summary>
        /// <param name="lendList">Lending list</param>
        /// <returns>PDF table</returns>
        public static string[,] GetTable(List<Lending> lendList)
        {
            // ----- Return if no data -----
            if (lendList == null) return null;

            // ----- Compute Tabsize -----
            string[,] tab = new string[lendList.Count + 2, 6];  // 6 Columns

            // ----- 1. ROW - columns size -----
            tab[0, 0] = "1cm";
            tab[0, 1] = "2cm";
            tab[0, 2] = "6.5cm";
            tab[0, 3] = "2cm";
            tab[0, 4] = "2cm";
            tab[0, 5] = "2.5cm";

            // ----- 2. ROW - columns names -----
            tab[1, 0] = Lng.Get("Number");
            tab[1, 1] = Lng.Get("Type");
            tab[1, 2] = Lng.Get("ItemName", "Name");
            tab[1, 3] = Lng.Get("From", "From");
            tab[1, 4] = Lng.Get("To", "To");
            tab[1, 5] = Lng.Get("Status");

            // ----- 3+. ROW - fill data-----
            for (int i = 0; i < lendList.Count; i++)
            {
                tab[i + 2, 0] = (i + 1).ToString();
                tab[i + 2, 1] = global.GetItemTypeName(lendList[i].ItemType);
                tab[i + 2, 2] = global.GetLendingItemName(lendList[i].ItemType.Trim(), lendList[i].ItemID ?? Guid.Empty);
                tab[i + 2, 3] = (lendList[i].From ?? DateTime.Now).ToShortDateString();
                tab[i + 2, 4] = (lendList[i].To ?? DateTime.Now).ToShortDateString();
                tab[i + 2, 5] = global.GetStatusName(lendList[i].Status ?? 1);
            }

            // ----- Return table -----
            return tab;
        }

        #endregion

        #region Export

        /// <summary>
        /// Export Contacts to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="con">Contact list</param>
        public static void ExportContactsCSV(string path, List<Contacts> con)
        {
            string lines;

            lines = "name;surname;nick;sex;birth;phone;email;www;im;company;position;street;city;region;country;postcode;personcode;note;groups;tags;updated;googleID;active;avatar;GUID" + Environment.NewLine;

            int imgNum = 0;
            foreach (var item in con)
            {
                string imgFileName = "";
                // ----- Images -----
                if (item.Avatar != null && item.Avatar.Length > 0)
                {
                    try
                    {
                        imgFileName = "img" + imgNum.ToString("D4") + ".jpg";
                        string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_images";
                        Directory.CreateDirectory(imgPath);
                        imgPath += Path.DirectorySeparatorChar + imgFileName;
                        try
                        {
                            File.WriteAllBytes(imgPath, item.Avatar);
                            imgNum++;
                        }
                        catch
                        {
                            imgFileName = "";
                        }
                    }
                    catch { }
                    
                }

                // ----- Other data -----
                lines += item.Name.Trim() + ";" + item.Surname.Trim() + ";" + item.Nick.Trim() + ";" + item.Sex.Trim() + ";" + item.Birth.ToString() + ";" + item.Phone.Trim() + ";" + item.Email.Trim() + ";" + item.WWW.Trim() + ";" + item.IM.Trim() + ";";
                lines += item.Company.Trim() + ";" + item.Position.Trim() + ";" + item.Street.Trim() + ";" + item.City.Trim() + ";" + item.Region.Trim() + ";" + item.Country.Trim() + ";" + item.PostCode.Trim() + ";";
                lines += item.PersonCode.Trim() + ";" + item.Note.Trim() + ";" + item.Tags.Trim() + ";" + item.FastTags.ToString() + ";" + item.Updated.ToString() + ";" + item.GoogleID.Trim() + ";" + (item.Active ?? true).ToString() + ";";
                lines += imgFileName + ";" +  item.ID + Environment.NewLine;

                
            }

            Files.SaveFile(path, lines);
        }

        /// <summary>
        /// Export Lending to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="bor">Lending list</param>
        public static void ExportLendedCSV(string path, List<Lending> bor)
        {
            string lines;

            lines = "itemType;ItemID;personID;from;to;status;GUID" + Environment.NewLine;

            foreach (var item in bor)
            {
                lines += item.ItemType.Trim() + ";" + item.ItemID.ToString() + ";" + item.PersonID.ToString() + ";" + item.From.ToString() + ";" + item.To.ToString() + ";";
                lines += item.Status.ToString() + ";" + item.ID.ToString() + Environment.NewLine;
            }

            Files.SaveFile(path, lines);
        }

        /// <summary>
        /// Export Lending to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="bor">Lending list</param>
        public static void ExportBorrowedCSV(string path, List<Lending> bor)
        {
            string lines;

            lines = "itemType;ItemID;personID;from;to;status;GUID" + Environment.NewLine;

            foreach (var item in bor)
            {
                lines += item.ItemType.Trim() + ";" + item.ItemID.ToString() + ";" + item.PersonID.ToString() + ";" + item.From.ToString() + ";" + item.To.ToString() + ";";
                lines += item.Status.ToString() + ";" + item.ID.ToString() + Environment.NewLine;
            }

            Files.SaveFile(path, lines);
        }

        /// <summary>
        /// Export Items to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="itm">Item list</param>
        public static void ExportItemsCSV(string path, List<Items> itm)
        {
            string lines;

            lines = "name;category;subcategory;keywords;note;acqdate;price;excluded;count;invnum;location;fasttags;GUID" + Environment.NewLine;

            foreach (var item in itm)
            {
                lines += item.Name.Trim() + ";" + item.Category.Trim() + ";" + item.Subcategory.Trim() + ";" + item.Keywords.Trim().Replace(";", ",") + ";" + item.Note.Trim().Replace(Environment.NewLine, "\n") + ";";
                lines += item.AcquisitionDate.ToString() + ";" + item.Price.ToString() + ";" + item.Excluded.ToString() + ";" + item.Count.ToString() + ";" + item.InventoryNumber.Trim().Replace(";", ",") + ";" + item.Location.Trim().Replace(";", ",") + ";" + item.FastTags.ToString() + ";" + item.ID + Environment.NewLine;
            }

            Files.SaveFile(path, lines);
        }

        /// <summary>
        /// Export Books to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="itm">Book list</param>
        public static void ExportBooksCSV(string path, List<Books> book)
        {
            string lines;

            lines = "name;authorName;authorSurname;fasttags;GUID" + Environment.NewLine;

            foreach (var item in book)
            {
                lines += item.Title.Trim() + ";" + item.AuthorName.Trim() + ";" + item.AuthorSurname.Trim() + ";" + item.FastTags.ToString() + ";" + item.ID + Environment.NewLine;
            }

            Files.SaveFile(path, lines);
        }

        #endregion

        #region Import

        public static List<Contacts> ImportContactsCSV(string path)
        {
            List<Contacts> con = new List<Contacts>();
            string text = Files.LoadFile(path);
            CSVfile file = Files.ParseCSV(text);

            foreach (var item in file.data)
            {
                string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_images" + Path.DirectorySeparatorChar + item[23];

                Contacts contact = new Contacts();
                contact.Name = item[0];
                contact.Surname = item[1];
                contact.Nick = item[2];
                contact.Sex = item[3];
                contact.Birth = Conv.ToDateTimeNull(item[4]);
                contact.Phone = item[5];
                contact.Email = item[6];
                contact.WWW = item[7];
                contact.IM = item[8];
                contact.Company = item[9];
                contact.Position = item[10];
                contact.Street = item[11];
                contact.City = item[12];
                contact.Region = item[13];
                contact.Country = item[14];
                contact.PostCode = item[15];
                contact.PersonCode = item[16];
                contact.Note = item[17];
                contact.Tags = item[18];
                contact.FastTags = Conv.ToShortDef(item[19], 0);
                contact.Updated = Conv.ToDateTimeNull(item[20]);
                contact.GoogleID = item[21];
                contact.Active = Conv.ToBoolDef(item[22], true);
                if (item[23] != "")
                    contact.Avatar = Files.LoadBinFile(imgPath);
                contact.ID = Conv.ToGuid(item[24]);
                con.Add(contact);
            }

            return con;
        }

        public static List<Lending> ImportLendedCSV(string path)
        {
            List<Lending> con = new List<Lending>();
            string text = Files.LoadFile(path);
            CSVfile file = Files.ParseCSV(text);

            foreach (var item in file.data)
            {
                Lending itm = new Lending();
                itm.ItemType = item[0];
                itm.ItemID = Conv.ToGuid(item[1]);
                itm.PersonID = Conv.ToGuid(item[2]);
                itm.From = Conv.ToDateTimeNull(item[3]);
                itm.To = Conv.ToDateTimeNull(item[4]);
                itm.Status = Conv.ToShortNull(item[5]);
                itm.ID = Conv.ToGuid(item[6]);
                con.Add(itm);
            }

            return con;
        }

        public static List<Items> ImportItemsCSV(string path)
        {
            List<Items> con = new List<Items>();
            string text = Files.LoadFile(path);
            CSVfile file = Files.ParseCSV(text);

            foreach (var item in file.data)
            {
                Items itm = new Items();
                itm.Name = item[0];
                itm.Category = item[1];
                itm.Subcategory = item[2];
                itm.Keywords = item[3];
                itm.Note = item[4].Replace("\n", Environment.NewLine);
                itm.AcquisitionDate = Conv.ToDateTimeNull(item[5]);
                itm.Price = Conv.ToDoubleNull(item[6]);
                itm.Excluded = Conv.ToBoolNull(item[7]);
                itm.Count = Conv.ToShortNull(item[8]);
                itm.InventoryNumber = item[9].Replace(",", ";");
                itm.Location = item[10].Replace(",", ";");
                itm.FastTags = Conv.ToShortDef(item[11], 0);
                itm.ID = Conv.ToGuid(item[12]);
                con.Add(itm);
            }

            return con;
        }

        public static List<Books> ImportBooksCSV(string path)
        {
            List<Books> con = new List<Books>();
            string text = Files.LoadFile(path);
            CSVfile file = Files.ParseCSV(text);

            foreach (var item in file.data)
            {
                Books itm = new Books();
                itm.Title = item[0];
                itm.AuthorName = item[1];
                itm.AuthorSurname = item[2];
                itm.FastTags = Conv.ToShortNull(item[3]);
                itm.ID = Conv.ToGuid(item[4]);
                con.Add(itm);
            }

            return con;
        }

        #endregion
    }


}
