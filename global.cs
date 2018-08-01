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
        public string InventoryNumber { get; set; }
        public long Barcode { get; set; }
        public short Count { get; set; }
        public short Available { get; set; }

        public Guid ItemID { get; set; }
        public string ItemType { get; set; }
        public int ItemNum { get; set; }
                
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

            switch (type.Trim())
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

        public static List<string> DeleteDuplicates(List<string> list)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                bool find = false;
                for (int j = 0; j < res.Count; j++)
                {
                    if (list[i] == res[j])
                    {
                        find = true;
                        break;
                    }
                }
                if (!find && list[i] != "")
                    res.Add(list[i]);
            }
            return res;
        }

        public static string GetInvNumList(Guid id)
        {
            databaseEntities db = new databaseEntities();
            var list = db.Copies.Where(x => x.ItemID == id).Select(x => x.InventoryNumber).ToList();
            list = DeleteDuplicates(list);

            string NumList = "";
            foreach(var item in list)
            {
                if (NumList != "") NumList += ", ";
                NumList += item;
            }
            return NumList;
        }
        
        public static string GetLocationList(Guid id)
        {
            databaseEntities db = new databaseEntities();
            var list = db.Copies.Where(x => x.ItemID == id).Select(x => x.Location).ToList();
            list = DeleteDuplicates(list);
            string NumList = "";
            foreach (var item in list)
            {
                if (NumList != "") NumList += ", ";
                NumList += item;
            }
            return NumList;
        }



        /// <summary>
        /// Check if item available
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool IsAvailable(short? status)
        {
            short stat = status ?? (short)LendStatus.Returned;
            if (stat == (short)LendStatus.Canceled || stat == (short)LendStatus.Returned)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Check Duplicate Inventory number
        /// </summary>
        /// <param name="InventoryNumber">Ger duplicate Inventory number</param>
        /// <returns>Returns true if duplicate exist</returns>
        public static bool IsDuplicate(string InventoryNumber, Guid ID)
        {
            databaseEntities db = new databaseEntities();

            var list = db.Copies.Where(x => x.ID != ID).Select(x => x.InventoryNumber).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                if (InventoryNumber.Trim() == list[i].Trim())
                    return true;
            }
            return false;
        }

        #region Copies

        /// <summary>
        /// Create new Copy item
        /// </summary>
        public static Copies CreateCopy(Guid ItemID, ItemTypes ItemType)
        {
            Copies itm = new Copies();

            // ----- Init values -----
            itm.ID = Guid.NewGuid();                // ID
            itm.ItemID = ItemID;                    // Item ID
            itm.ItemType = ItemType.ToString();     // Item Type - Item
            itm.ItemNum = 0;                        // ItemNum
            itm.Note = "";                          // Note
            itm.Status = (short)LendStatus.Returned;// Status

            //itm.Price = 0;                        // Price
            itm.AcquisitionDate = DateTime.Now;     // Acqusition date
            itm.Excluded = false;                   // Excluded
            //itm.Condition = "";                   // Condition
            //itm.InventoryNumber = "";             // Inventory Number
            //itm.Barcode = "";                     // Barcode
            itm.Location = "";                      // Location

            // ----- Return Copy -----
            return itm;
        }

        /// <summary>
        /// Create new Copy item and copy data from another structure
        /// </summary>
        /// <param name="input">Input</param>
        public static Copies CopyCopies(Copies input)
        {
            // ----- Try parse ItemType -----
            ItemTypes type = ItemTypes.item;
            try
            {
                Enum.TryParse(input.ItemType, out type);
            } catch { }
            
            // ----- Create new Copy -----
            Copies itm = CreateCopy(input.ItemID ?? Guid.Empty, type);

            // ----- Fill Copy -----
            itm.Price = input.Price;                        // Price
            itm.AcquisitionDate = input.AcquisitionDate;    // Acqusition date
            itm.Excluded = input.Excluded;                  // Excluded
            itm.Condition = input.Condition;                // Condition
            itm.InventoryNumber = input.InventoryNumber;    // Inventory Number
            itm.Barcode = input.Barcode;                    // Barcode
            itm.Location = input.Location;                  // Location
            itm.Note = input.Note;

            // ----- Return Copy -----
            return itm;
        }

        /// <summary>
        /// Get available items
        /// </summary>
        /// <param name="list">Copies list</param>
        /// <returns>Number of available items</returns>
        public static short GetAvailableCopies(List<Copies> list)
        {
            short available = 0;
            foreach (var item in list)
            {
                if (!(item.Excluded ?? false))
                    if (item.Status == (short)LendStatus.Canceled || item.Status == (short)LendStatus.Returned) available++;
            }
            return available;
        }
        
        /// <summary>
        /// Get copies count
        /// </summary>
        /// <returns></returns>
        public static short GetCopiesCount(List<Copies> list)
        {
            short count = 0;
            foreach (var item in list)
            {
                if (!(item.Excluded ?? false)) count++;
            }
            return count;
        }

        /// <summary>
        /// Refresh Copies status
        /// </summary>
        /// <param name="list">Copies list to refresf</param>
        /// <param name="status">Status</param>
        public static void RefreshCopiesStatus(List<Copies> list, LendStatus status)
        {
            RefreshCopiesStatus(list, (short)status);
        }

        /// <summary>
        /// Refresh Copies status
        /// </summary>
        /// <param name="list">Copies list to refresf</param>
        /// <param name="status">Status</param>
        public static void RefreshCopiesStatus(List<Copies> list, short status)
        {
            databaseEntities db = new databaseEntities();

            foreach (var itm in list)
            {
                var copy = db.Copies.Find(itm.ID);
                copy.Status = status;
            }
            db.SaveChanges();
        }
        
        /// <summary>
        /// Refresh Copies status
        /// </summary>
        /// <param name="list">Copies list to refresf</param>
        /// <param name="status">Status</param>
        public static void RefreshCopiesStatus(List<Lending> list)
        {
            databaseEntities db = new databaseEntities();

            foreach (var itm in list)
            {
                var copy = db.Copies.Find(itm.CopyID);
                copy.Status = itm.Status;
            }
            db.SaveChanges();
        }


        #endregion


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
        public static ItemTypes GetItemType(string type)
        {
            type = type.Trim();
            switch (type)
            {
                case "item":                    // Item
                    return ItemTypes.item;
                case "book":                    // Book
                    return ItemTypes.book;
                case "boardgame":               // Board game
                    return ItemTypes.boardgame;
                default:                        // Unknown
                    return ItemTypes.item;
            }
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
            databaseEntities db = new databaseEntities();

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
                var copy = db.Copies.Find(lendList[i].CopyID);
                tab[i + 2, 0] = (i + 1).ToString();
                tab[i + 2, 1] = global.GetItemTypeName(lendList[i].CopyType);
                tab[i + 2, 2] = global.GetLendingItemName(copy.ItemType, copy.ItemID ?? Guid.Empty);
                tab[i + 2, 3] = (lendList[i].From ?? DateTime.Now).ToShortDateString();
                tab[i + 2, 4] = (lendList[i].To ?? DateTime.Now).ToShortDateString();
                tab[i + 2, 5] = global.GetStatusName(lendList[i].Status ?? 1);
            }

            // ----- Return table -----
            return tab;
        }
        
        /// <summary>
        /// Create Borrowing table for PDF document
        /// </summary>
        /// <param name="borrList">Borowing list</param>
        /// <returns>PDF table</returns>
        public static string[,] GetTable(List<Borrowing> borrList)
        {
            databaseEntities db = new databaseEntities();

            // ----- Return if no data -----
            if (borrList == null) return null;

            // ----- Compute Tabsize -----
            string[,] tab = new string[borrList.Count + 2, 5];  // 5 Columns

            // ----- 1. ROW - columns size -----
            tab[0, 0] = "1cm";
            tab[0, 1] = "8.5cm";
            tab[0, 2] = "2cm";
            tab[0, 3] = "2cm";
            tab[0, 4] = "2.5cm";

            // ----- 2. ROW - columns names -----
            tab[1, 0] = Lng.Get("Number");
            tab[1, 1] = Lng.Get("ItemName", "Name");
            tab[1, 2] = Lng.Get("From", "From");
            tab[1, 3] = Lng.Get("To", "To");
            tab[1, 4] = Lng.Get("Status");

            // ----- 3+. ROW - fill data-----
            for (int i = 0; i < borrList.Count; i++)
            {
                tab[i + 2, 0] = (i + 1).ToString();
                tab[i + 2, 1] = borrList[i].Item.Trim();
                tab[i + 2, 2] = (borrList[i].From ?? DateTime.Now).ToShortDateString();
                tab[i + 2, 3] = (borrList[i].To ?? DateTime.Now).ToShortDateString();
                tab[i + 2, 4] = global.GetStatusName(borrList[i].Status ?? 1);
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
        /// <returns>Return True if saved succesfully</returns>
        public static bool ExportContactsCSV(string path, List<Contacts> con)
        {
            // ----- Head -----
            string lines = "FialotCatalog:Contacts v1" + Environment.NewLine;

            // ----- Names -----
            lines += "name;surname;nick;sex;birth;phone;email;www;im;company;position;street;city;region;country;postcode;personcode;note;groups;tags;updated;googleID;active;avatar;GUID" + Environment.NewLine;

            // ----- Data -----
            int imgNum = 0;
            foreach (var item in con)
            {
                // ----- Images -----
                string imgFileName = "";
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

            return Files.SaveFile(path, lines);
        }

        /// <summary>
        /// Export Lending to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="bor">Lending list</param>
        /// <returns>Return True if saved succesfully</returns>
        public static bool ExportLendedCSV(string path, List<Lending> bor)
        {
            databaseEntities db = new databaseEntities();

            // ----- Head -----
            string lines = "FialotCatalog:Lending v1" + Environment.NewLine;

            // ----- Names -----
            lines += "copyName;copyInvNum;copyType;copyID;personName;personID;from;to;status;note;fastTags;updated;GUID" + Environment.NewLine;

            // ----- Data -----
            foreach (var item in bor)
            {
                var person = db.Contacts.Find(item.PersonID);           // Get person
                var copy = db.Copies.Find(item.CopyID);                 // Get copy

                lines += global.GetLendingItemName(copy.ItemType, copy.ItemID ?? Guid.Empty) + ";" + copy.InventoryNumber + ";";
                lines += item.CopyType.Trim() + ";" + item.CopyID.ToString() + ";" + person.Name.Trim() + " " + person.Surname.Trim() + ";";
                lines += item.PersonID.ToString() + ";" + item.From.ToString() + ";" + item.To.ToString() + ";" + item.Status.ToString() + ";";
                lines += item.Note.Trim() + ";" + item.FastTags.ToString() + ";" + item.Updated.ToString() + ";" + item.ID.ToString() + Environment.NewLine;
            }

            // ----- Save to file ------
            return Files.SaveFile(path, lines);
        }
        
        /// <summary>
        /// Export Borrowing to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="bor">Borrowing list</param>
        /// <returns>Return True if saved succesfully</returns>
        public static bool ExportBorrowingCSV(string path, List<Borrowing> bor)
        {
            databaseEntities db = new databaseEntities();

            // ----- Head -----
            string lines = "FialotCatalog:Borrowing v1" + Environment.NewLine;

            // ----- Names -----
            lines += "item;itemInvNum;person;personID;from;to;status;note;fastTags;updated;GUID" + Environment.NewLine;

            // ----- Data -----
            foreach (var item in bor)
            {
                var person = db.Contacts.Find(item.PersonID);           // Get person

                lines += item.Item + ";" + item.ItemInvNum + ";" + person.Name.Trim() + " " + person.Surname.Trim() + ";";
                lines += item.PersonID.ToString() + ";" + item.From.ToString() + ";" + item.To.ToString() + ";" + item.Status.ToString() + ";";
                lines += item.Note + ";" + item.FastTags.ToString() + ";" + item.Updated.ToString() + ";" + item.ID.ToString() + Environment.NewLine;
            }

            // ----- Save to file ------
            return Files.SaveFile(path, lines);
        }

        /// <summary>
        /// Export Copies to CSV file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="itm"></param>
        public static void ExportCopiesCSV(string path, List<Copies> itm)
        {
            // ----- Head -----
            string lines = "FialotCatalog:Copies v1" + Environment.NewLine;

            // ----- Names -----
            lines += "itemName;itemType;itemID;itemNum;invNumber;condition;location;note;acqDate;price;excluded;status;GUID" + Environment.NewLine;

            // ----- Data -----
            foreach (var item in itm)
            {
                // ----- Other data -----
                lines += global.GetLendingItemName(item.ItemType, item.ItemID ?? Guid.Empty) + ";" + item.ItemType.Trim() + ";" + item.ItemID.ToString() + ";" + item.ItemNum.ToString() + ";";
                lines += item.InventoryNumber.Trim() + ";" + item.Condition.Trim() + ";" + item.Location.Trim() + ";" + item.Note.Trim() + ";";
                lines += item.AcquisitionDate.ToString() + ";" + item.Price.ToString() + ";" + item.Excluded.ToString() + ";" + item.Status + ";";
                lines += item.ID + Environment.NewLine;
            }

            // ----- Save to file ------
            Files.SaveFile(path, lines);
        }

        /// <summary>
            /// Export Items to CSV file
            /// </summary>
            /// <param name="path">File path</param>
            /// <param name="itm">Item list</param>
        public static void ExportItemsCSV(string path, List<Items> itm)
        {
            databaseEntities db = new databaseEntities();

            // ----- Head -----
            string lines = "FialotCatalog:Items v1" + Environment.NewLine;

            // ----- Names -----
            lines += "name;category;subcategory;subcategory2;keywords;manufacturer;note;excluded;count;fasttags;image;updated;GUID" + Environment.NewLine;

            // ----- Data -----
            int imgNum = 0;
            string filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
            Directory.CreateDirectory(filePath);        // create files path

            // ----- Copies -----
            var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.item.ToString())).ToList();
            string copiesPath = filePath + Path.DirectorySeparatorChar + "copies.csv";
            ExportCopiesCSV(copiesPath, copies);

            // ----- Data -----
            foreach (var item in itm)
            {
                // ----- Images -----
                string imgFileName = "";
                if (item.Image != null && item.Image.Length > 0)
                {
                    try
                    {
                        imgFileName = "img" + imgNum.ToString("D4") + ".jpg";

                        string imgPath = filePath + Path.DirectorySeparatorChar + imgFileName;
                        try
                        {
                            File.WriteAllBytes(imgPath, item.Image);
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
                lines += item.Name.Trim() + ";" + item.Category.Trim() + ";" + item.Subcategory.Trim() + ";" + item.Subcategory2 + ";" + item.Keywords.Trim().Replace(";", ",") + ";";
                lines += item.Manufacturer + ";" + item.Note.Trim().Replace(Environment.NewLine, "\n") +";" + item.Excluded.ToString() + ";" + item.Count.ToString() + ";" ;
                lines += item.FastTags.ToString() + ";" + imgFileName + ";" + item.Updated.ToString() + ";" + item.ID + Environment.NewLine;
            }

            // ----- Save to file ------
            Files.SaveFile(path, lines);
        }

        /// <summary>
        /// Export Books to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="itm">Book list</param>
        public static void ExportBooksCSV(string path, List<Books> book)
        {
            databaseEntities db = new databaseEntities();

            // ----- Head -----
            string lines = "FialotCatalog:Books v1" + Environment.NewLine;

            // ----- Names -----
            lines += "Title;AuthorName;AuthorSurname;ISBN;Illustrator;Translator;Language;Publisher;Edition;Year;Pages;MainCharacter;URL;Note;Note1;Note2;Content;OrigName;OrigLanguage;OrigYear;Genre;SubGenre;Series;SeriesNum;Keywords;Rating;MyRating;Readed;Type;Bookbinding;EbookPath;EbookType;Publication;Excluded;Cover;Updated;FastTags;GUID" + Environment.NewLine;

            // ----- Data -----
            int imgNum = 0;
            string filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
            Directory.CreateDirectory(filePath);        // create files path

            // ----- Copies -----
            var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.book.ToString())).ToList();
            string copiesPath = filePath + Path.DirectorySeparatorChar + "copies.csv";
            ExportCopiesCSV(copiesPath, copies);
                        
            // ----- Data -----
            foreach (var item in book)
            {
                // ----- Cover -----
                string imgFileName = "";
                if (item.Cover != null && item.Cover.Length > 0)
                {
                    try
                    {
                        imgFileName = "img" + imgNum.ToString("D4") + ".jpg";

                        string imgPath = filePath + Path.DirectorySeparatorChar + imgFileName;
                        try
                        {
                            File.WriteAllBytes(imgPath, item.Cover);
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
                lines += item.Title.Trim() + ";" + item.AuthorName.Trim() + ";" + item.AuthorSurname.Trim() + ";" + item.ISBN + ";" + item.Illustrator + ";" + item.Translator + ";";
                lines += item.Language + ";" + item.Publisher + ";" + item.Edition + ";" + item.Year + ";" + item.Pages + ";" + item.MainCharacter + ";";
                lines += item.URL + ";" + item.Note.Replace(Environment.NewLine, "\n") + ";" + item.Note1.Replace(Environment.NewLine, "\n") + ";" + item.Note2.Replace(Environment.NewLine, "\n") + ";" + item.Content.Replace(Environment.NewLine, "\n") + ";";
                lines += item.OrigName + ";" + item.OrigLanguage + ";" + item.OrigYear + ";" + item.Genre + ";" + item.SubGenre + ";" + item.Series + ";" + item.SeriesNum + ";";
                lines += item.Keywords.Replace(";", ",") + ";" + item.Rating + ";" + item.MyRating + ";" + item.Readed + ";" + item.Type + ";" + item.Bookbinding + ";";
                lines += item.EbookPath + ";" + item.EbookType + ";" + item.Publication + ";" + item.Excluded + ";" + imgFileName + ";" + item.Updated + ";";
                lines += item.FastTags.ToString() + ";" + item.ID + Environment.NewLine;
            }
            // ----- Save to file ------
            Files.SaveFile(path, lines);
        }

        /// <summary>
        /// Export Boardgames to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="itm">Item list</param>
        public static void ExportBoardCSV(string path, List<Boardgames> itm)
        {
            databaseEntities db = new databaseEntities();

            // ----- Head -----
            string lines = "FialotCatalog:Boardgames v1" + Environment.NewLine;

            // ----- Names -----
            lines += "name;category;subcategory;subcategory2;keywords;manufacturer;note;excluded;count;fasttags;image;updated;GUID" + Environment.NewLine;

            // ----- Data -----
            int imgNum = 0;
            string filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
            Directory.CreateDirectory(filePath);        // create files path

            // ----- Copies -----
            var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.item.ToString())).ToList();
            string copiesPath = filePath + Path.DirectorySeparatorChar + "copies.csv";
            ExportCopiesCSV(copiesPath, copies);

            // ----- Data -----
            foreach (var item in itm)
            {
                // ----- Images -----
                string imgFileName = "";
                if (item.Image != null && item.Image.Length > 0)
                {
                    try
                    {
                        imgFileName = "img" + imgNum.ToString("D4") + ".jpg";

                        string imgPath = filePath + Path.DirectorySeparatorChar + imgFileName;
                        try
                        {
                            File.WriteAllBytes(imgPath, item.Image);
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
                lines += item.Name.Trim() + ";" + item.Category.Trim() + ";" + item.Subcategory.Trim() + ";" + item.Subcategory2 + ";" + item.Keywords.Trim().Replace(";", ",") + ";";
                lines += item.Manufacturer + ";" + item.Note.Trim().Replace(Environment.NewLine, "\n") + ";" + item.Excluded.ToString() + ";" + item.Count.ToString() + ";";
                lines += item.FastTags.ToString() + ";" + imgFileName + ";" + item.Updated.ToString() + ";" + item.ID + Environment.NewLine;
            }

            // ----- Save to file ------
            Files.SaveFile(path, lines);
        }

        #endregion

        #region Import

        /// <summary>
        /// Import Contacts from CSV
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Contact list</returns>
        public static List<Contacts> ImportContactsCSV(string path)
        {
            List<Contacts> con = new List<Contacts>();

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Check File Head -----
            if (!Str.GetFirstLine(ref text, true).Contains("FialotCatalog:Contacts"))
                return null;

            // ----- Parse CSV File -----
            CSVfile file = Files.ParseCSV(text);

            // ----- Check table size -----
            if (file.head.Length != 25)
                return null;

            // ----- Parse data -----
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

        /// <summary>
        /// Import Lended from CSV
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Lending list</returns>
        public static List<Lending> ImportLendedCSV(string path)
        {
            List<Lending> con = new List<Lending>();

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Check File Head -----
            if (!Str.GetFirstLine(ref text, true).Contains("FialotCatalog:Lending"))
                return null;

            // ----- Parse CSV File -----
            CSVfile file = Files.ParseCSV(text);

            // ----- Check table size -----
            if (file.head.Length != 13)
                return null;

            // ----- Parse data -----
            foreach (var item in file.data)
            {
                Lending itm = new Lending();
                //itm.CopyName = item[0];
                //itm.CopyInvNum = item[1];
                itm.CopyType = item[2];
                itm.CopyID = Conv.ToGuid(item[3]);
                //itm.PersonName = Conv.ToGuid(item[4]);
                itm.PersonID = Conv.ToGuid(item[5]);
                itm.From = Conv.ToDateTimeNull(item[6]);
                itm.To = Conv.ToDateTimeNull(item[7]);
                itm.Status = Conv.ToShortNull(item[8]);
                itm.Note = item[9];
                itm.FastTags = Conv.ToShortNull(item[10]);
                itm.Updated = Conv.ToDateTimeNull(item[11]);
                itm.ID = Conv.ToGuid(item[12]);
                con.Add(itm);
            }

            return con;
        }

        /// <summary>
        /// Import Borrowing from CSV
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Borrowing list</returns>
        public static List<Borrowing> ImportBorowingCSV(string path)
        {
            List<Borrowing> con = new List<Borrowing>();

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Check File Head -----
            if (!Str.GetFirstLine(ref text, true).Contains("FialotCatalog:Borrowing"))
                return null;

            // ----- Parse CSV File -----
            CSVfile file = Files.ParseCSV(text);

            // ----- Check table size -----
            if (file.head.Length != 11)
                return null;

            // ----- Parse data -----
            foreach (var item in file.data)
            {
                Borrowing itm = new Borrowing();
                itm.Item = item[0];
                itm.ItemInvNum = item[1];
                // PersonName = item[2];
                itm.PersonID = Conv.ToGuid(item[3]);
                itm.From = Conv.ToDateTimeNull(item[4]);
                itm.To = Conv.ToDateTimeNull(item[5]);
                itm.Status = Conv.ToShortNull(item[6]);
                itm.Note = item[7];
                itm.FastTags = Conv.ToShortNull(item[8]);
                itm.Updated = Conv.ToDateTimeNull(item[9]);
                itm.ID = Conv.ToGuid(item[10]);
                con.Add(itm);
            }

            return con;
        }

        /// <summary>
        /// Import Copies from CSV
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Item list</returns>
        public static List<Copies> ImportCopiesCSV(string path)
        {
            List<Copies> con = new List<Copies>();

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Check File Head -----
            if (!Str.GetFirstLine(ref text, true).Contains("FialotCatalog:Copies"))
                return null;

            // ----- Parse CSV File -----
            CSVfile file = Files.ParseCSV(text);

            // ----- Check table size -----
            if (file.head.Length != 13)
                return null;

            foreach (var item in file.data)
            {
                Copies itm = new Copies();
                //itm.ItemName = item[0];
                itm.ItemType = item[1];
                itm.ItemID = Conv.ToGuid(item[2]);
                itm.ItemNum = Conv.ToShortNull(item[3]);
                itm.InventoryNumber = item[4];
                itm.Condition = item[5];
                itm.Location = item[6];
                itm.Note = item[7];
                itm.AcquisitionDate = Conv.ToDateTimeNull(item[8]);
                itm.Price = Conv.ToFloatDef(item[9], 0);
                itm.Excluded = Conv.ToBoolNull(item[11]);
                itm.Status = Conv.ToShortNull(item[11]);
                itm.ID = Conv.ToGuid(item[12]);
                con.Add(itm);
            }

            return con;
        }

        /// <summary>
        /// Import Items from CSV
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Item list</returns>
        public static List<Items> ImportItemsCSV(string path, out List<Copies> copies)
        {
            List<Items> con = new List<Items>();
            copies = null;

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Check File Head -----
            if (!Str.GetFirstLine(ref text, true).Contains("FialotCatalog:Items"))
                return null;

            // ----- Parse CSV File -----
            CSVfile file = Files.ParseCSV(text);

            // ----- Check table size -----
            if (file.head.Length != 13)
                return null;

            // ----- Import Copies -----
            string filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
            copies = ImportCopiesCSV(filePath + Path.DirectorySeparatorChar + "copies.csv");

            foreach (var item in file.data)
            {
                string imgPath = filePath + Path.DirectorySeparatorChar + item[10];

                Items itm = new Items();
                itm.Name = item[0];
                itm.Category = item[1];
                itm.Subcategory = item[2];
                itm.Subcategory2 = item[3];
                itm.Keywords = item[4];
                itm.Manufacturer = item[5];
                itm.Note = item[6].Replace("\n", Environment.NewLine);
                itm.Excluded = Conv.ToBoolNull(item[7]);
                itm.Count = Conv.ToShortNull(item[8]);
                itm.FastTags = Conv.ToShortDef(item[9], 0);
                if (item[10] != "")
                    itm.Image = Files.LoadBinFile(imgPath);
                itm.Updated = Conv.ToDateTimeNull(item[11]);
                itm.ID = Conv.ToGuid(item[12]);
                con.Add(itm);
            }

            return con;
        }

        /// <summary>
        /// Import Books from CSV
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Book list</returns>
        public static List<Books> ImportBooksCSV(string path, out List<Copies> copies)
        {
            List<Books> con = new List<Books>();
            copies = null;

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Check File Head -----
            if (!Str.GetFirstLine(ref text, true).Contains("FialotCatalog:Books"))
                return null;

            // ----- Parse CSV File -----
            CSVfile file = Files.ParseCSV(text);

            // ----- Check table size -----
            if (file.head.Length != 38)
                return null;

            // ----- Import Copies -----
            string filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
            copies = ImportCopiesCSV(filePath + Path.DirectorySeparatorChar + "copies.csv");
            
            foreach (var item in file.data)
            {
                string imgPath = filePath + Path.DirectorySeparatorChar + item[34];

                Books itm = new Books();
                itm.Title = item[0];
                itm.AuthorName = item[1];
                itm.AuthorSurname = item[2];
                itm.ISBN = item[3];
                itm.Illustrator = item[4];
                itm.Translator = item[5];
                itm.Language = item[6];
                itm.Publisher = item[7];
                itm.Edition = item[8];
                itm.Year = Conv.ToShortNull(item[9]);
                itm.Pages = Conv.ToShortNull(item[10]);
                itm.MainCharacter = item[11];
                itm.URL = item[12];
                itm.Note = item[13].Replace("\n",Environment.NewLine);
                itm.Note1 = item[14].Replace("\n", Environment.NewLine);
                itm.Note2 = item[15].Replace("\n", Environment.NewLine);
                itm.Content = item[16];
                itm.OrigName = item[17];
                itm.OrigLanguage = item[18];
                itm.OrigYear = Conv.ToShortNull(item[19]);
                itm.Genre = item[20];
                itm.SubGenre = item[21];
                itm.Series = item[22];
                itm.SeriesNum = Conv.ToShortNull(item[23]);
                itm.Keywords = item[24].Replace(",", ";");
                itm.Rating = Conv.ToShortNull(item[25]);
                itm.MyRating = Conv.ToShortNull(item[26]);
                itm.Readed = Conv.ToBoolNull(item[27]);
                itm.Type = item[28];
                itm.Bookbinding = item[29];
                itm.EbookPath = item[30];
                itm.EbookType = item[31];
                itm.Publication = Conv.ToShortNull(item[32]);
                itm.Excluded = Conv.ToBoolNull(item[33]);
                if (item[34] != "")
                    itm.Cover = Files.LoadBinFile(imgPath);
                itm.Updated = Conv.ToDateTimeNull(item[35]);
                itm.FastTags = Conv.ToShortNull(item[36]);
                itm.ID = Conv.ToGuid(item[37]);
                con.Add(itm);
            }

            return con;
        }

        #endregion
    }


}
