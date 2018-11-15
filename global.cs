using myFunctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GContacts;

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

    public class PInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }

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

    public class FInfo
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public string Path { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
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
            foreach (var item in list)
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

        public static List<PInfo> GetParentlist(Guid RemoveGuid)
        {
            databaseEntities db = new databaseEntities();

            List<PInfo> list = db.Objects.Where(x => (x.Active ?? true) && (x.IsParent ?? true) && (x.ID != RemoveGuid)).Select(x => new PInfo { ID = x.ID, Name = x.Name.Trim() }).ToList();

            return list;
        }

        public static FInfo GetFInfo(string text)
        {
            string[] info = text.Split(new string[] { ">" }, StringSplitOptions.None);
            if (info.Length == 5)
            {
                FInfo itm = new FInfo();
                itm.Name = info[0];
                itm.Path = info[1];
                itm.Version = info[2];
                itm.Group = info[3];
                itm.Description = info[4];
                return itm;
            }
            return null;
        }

        public static List<FInfo> GetFInfoList(string text)
        {
            List<FInfo> list = new List<FInfo>();

            string[] file = text.Split(new string[] { ";" }, StringSplitOptions.None);

            foreach (var item in file)
            {
                string[] info = item.Split(new string[] { ">" }, StringSplitOptions.None);
                if (info.Length == 5)
                {
                    FInfo itm = new FInfo();
                    itm.Name = info[0];
                    itm.Path = info[1];
                    itm.Version = info[2];
                    itm.Group = info[3];
                    itm.Description = info[4];
                    list.Add(itm);
                }
            }

            return list;
        }

        public static string FInfoToText(FInfo item)
        {
            return item.Name + ">" + item.Path + ">" + item.Version + ">" + item.Group + ">" + item.Description;
        }

        public static string FInfoToText(List<FInfo> list)
        {
            if (list == null) return "";
            string text = "";

            foreach (var item in list)
            {
                string line = FInfoToText(item);
                if (text != "") text += ";";
                text += line;
            }

            return text;
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
        /// Export Image
        /// </summary>
        /// <param name="path">Image path</param>
        /// <param name="image">Image</param>
        /// <returns></returns>
        public static bool ExportImage(ref string path, byte[] image)
        {
            // ----- Images -----
            if (image != null && image.Length > 0)
            {
                try
                {
                    File.WriteAllBytes(path, image);
                    path = Path.GetFileName(path);
                }
                catch
                {
                    path = "";
                    return false;
                }
            }
            else
            {
                path = "";
                return false;
            }
            return true;
        }

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

            // ----- Create files path -----
            string filePath = "";
            try
            {
                filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
                Directory.CreateDirectory(filePath);
            }
            catch { }
            
            // ----- Data -----
            int imgNum = 0;
            foreach (var item in con)
            {
                // ----- Images -----
                string imgFileName = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + ".jpg";
                ExportImage(ref imgFileName, item.Avatar);
                try
                {
                    imgFileName = Path.GetFileName(imgFileName);
                }
                catch { }
                

                // ----- Other data -----
                lines += item.Name.Trim().Replace(";", "//") + ";" + item.Surname.Trim().Replace(";", "//") + ";" + item.Nick.Trim().Replace(";", "//") + ";" + item.Sex.Trim().Replace(";", "//") + ";" + item.Birth.ToString() + ";" + item.Phone.Trim().Replace(";", "//") + ";" + item.Email.Trim().Replace(";", "//") + ";" + item.WWW.Trim().Replace(";", "//") + ";" + item.IM.Trim().Replace(";", "//") + ";";
                lines += item.Company.Trim().Replace(";", "//") + ";" + item.Position.Trim().Replace(";", "//") + ";" + item.Street.Trim().Replace(";", "//") + ";" + item.City.Trim().Replace(";", "//") + ";" + item.Region.Trim().Replace(";", "//") + ";" + item.Country.Trim().Replace(";", "//") + ";" + item.PostCode.Trim().Replace(";", "//") + ";";
                lines += item.PersonCode.Trim().Replace(";", "//") + ";" + item.Note.Trim().Replace(";", "//") + ";" + item.Tags.Trim().Replace(";", "//") + ";" + item.FastTags.ToString() + ";" + item.Updated.ToString() + ";" + item.GoogleID.Trim() + ";" + (item.Active ?? true).ToString() + ";";
                lines += imgFileName + ";" +  item.ID + Environment.NewLine;

                imgNum++;
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

            // ----- Create files path -----
            string filePath = "";
            try
            {
                filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
                Directory.CreateDirectory(filePath);
            }
            catch { }
            
            // ----- Copies -----
            var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.item.ToString())).ToList();
            string copiesPath = filePath + Path.DirectorySeparatorChar + "copies.csv";
            ExportCopiesCSV(copiesPath, copies);

            // ----- Data -----
            int imgNum = 0;
            foreach (var item in itm)
            {
                // ----- Images -----
                string imgFileName = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + ".jpg";
                ExportImage(ref imgFileName, item.Image);
                try
                {
                    imgFileName = Path.GetFileName(imgFileName);
                }
                catch { }

                // ----- Other data -----
                lines += item.Name.Trim() + ";" + item.Category.Trim() + ";" + item.Subcategory.Trim() + ";" + item.Subcategory2 + ";" + item.Keywords.Trim().Replace(";", ",") + ";";
                lines += item.Manufacturer + ";" + item.Note.Trim().Replace(Environment.NewLine, "\n") +";" + item.Excluded.ToString() + ";" + item.Count.ToString() + ";" ;
                lines += item.FastTags.ToString() + ";" + imgFileName + ";" + item.Updated.ToString() + ";" + item.ID + Environment.NewLine;

                imgNum++;
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

            // ----- Create files path -----
            string filePath = "";
            try
            {
                filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
                Directory.CreateDirectory(filePath);
            }
            catch { }

            // ----- Copies -----
            var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.book.ToString())).ToList();
            string copiesPath = filePath + Path.DirectorySeparatorChar + "copies.csv";
            ExportCopiesCSV(copiesPath, copies);

            // ----- Data -----
            int imgNum = 0;
            foreach (var item in book)
            {
                // ----- Cover -----
                string imgFileName = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + ".jpg";
                ExportImage(ref imgFileName, item.Cover);
                try
                {
                    imgFileName = Path.GetFileName(imgFileName);
                }
                catch { }

                // ----- Other data -----
                lines += item.Title.Trim() + ";" + item.AuthorName.Trim() + ";" + item.AuthorSurname.Trim() + ";" + item.ISBN + ";" + item.Illustrator + ";" + item.Translator + ";";
                lines += item.Language + ";" + item.Publisher + ";" + item.Edition + ";" + item.Year + ";" + item.Pages + ";" + item.MainCharacter + ";";
                lines += item.URL + ";" + item.Note.Replace(Environment.NewLine, "\n") + ";" + item.Note1.Replace(Environment.NewLine, "\n") + ";" + item.Note2.Replace(Environment.NewLine, "\n") + ";" + item.Content.Replace(Environment.NewLine, "\n") + ";";
                lines += item.OrigName + ";" + item.OrigLanguage + ";" + item.OrigYear + ";" + item.Genre + ";" + item.SubGenre + ";" + item.Series + ";" + item.SeriesNum + ";";
                lines += item.Keywords.Replace(";", ",") + ";" + item.Rating + ";" + item.MyRating + ";" + item.Readed + ";" + item.Type + ";" + item.Bookbinding + ";";
                lines += item.EbookPath + ";" + item.EbookType + ";" + item.Publication + ";" + item.Excluded + ";" + imgFileName + ";" + item.Updated + ";";
                lines += item.FastTags.ToString() + ";" + item.ID + Environment.NewLine;

                imgNum++;
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
            lines += "Name;Category;MinPlayers;MaxPlayers;MinAge;GameTime;GameWorld;Language;Publisher;Author;Year;Description;Keywords;Note;Family;Extension;ExtensionNumber;Rules;Cover;Img1;Img2;Img3;MaterialPath;Rating;MyRating;URL;Excluded;FastTags;Updated;GUID" + Environment.NewLine;

            // ----- Create files path -----
            string filePath = "";
            try
            {
                filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
                Directory.CreateDirectory(filePath);
            }
            catch { }

            // ----- Copies -----
            var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.boardgame.ToString())).ToList();
            string copiesPath = filePath + Path.DirectorySeparatorChar + "copies.csv";
            ExportCopiesCSV(copiesPath, copies);

            // ----- Data -----
            int imgNum = 0;
            foreach (var item in itm)
            {
                // ----- Images -----
                string imgCover = filePath + Path.DirectorySeparatorChar + "imgC" + imgNum.ToString("D4") + ".jpg";
                ExportImage(ref imgCover, item.Cover);
                imgCover = Path.GetFileName(imgCover);
                string img1 = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + "A.jpg";
                ExportImage(ref img1, item.Img1);
                img1 = Path.GetFileName(img1);
                string img2 = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + "B.jpg";
                ExportImage(ref img2, item.Img2);
                img2 = Path.GetFileName(img2);
                string img3 = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + "C.jpg";
                ExportImage(ref img3, item.Img3);
                img3 = Path.GetFileName(img3);

                // ----- Other data -----
                lines += item.Name + ";" + item.Category + ";" + item.MinPlayers + ";" + item.MaxPlayers + ";" + item.MinAge + ";" + item.GameTime + ";" + item.GameWorld + ";";
                lines += item.Language + ";" + item.Publisher + ";" + item.Author + ";" + item.Year + ";" + item.Description.Replace(Environment.NewLine, "\n") + ";" + item.Keywords.Trim().Replace(";", ",") + ";";
                lines += item.Note.Replace(Environment.NewLine, "\n") + ";" + item.Family + ";" + item.Extension + ";" + item.ExtensionNumber + ";" + item.Rules.Replace(Environment.NewLine, "\n") + ";";
                lines += imgCover + ";" + img1 + ";" + img2 + ";" + img3 + ";" + item.MaterialPath + ";" + item.Rating + ";" + item.MyRating + ";" + item.URL + ";";
                lines += item.Excluded + ";" + item.FastTags + ";" + item.Updated + ";" + item.ID + Environment.NewLine;

                imgNum++;
            }

            // ----- Save to file ------
            Files.SaveFile(path, lines);
        }


        /// <summary>
        /// Export Objects to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="con">Contact list</param>
        /// <returns>Return True if saved succesfully</returns>
        public static bool ExportObjectCSV(string path, List<Objects> con)
        {
            // ----- Head -----
            string lines = "FialotCatalog:Objects v1" + Environment.NewLine;

            // ----- Names -----
            lines += "name;category;subcategory;keywords;note;description;image;rating;myrating;fasttags;updated;active;version;files;folder;type;objectnum;language;parent;customer;development;isparent;usedobject;GUID" + Environment.NewLine;

            // ----- Create files path -----
            string filePath = "";
            try
            {
                filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
                Directory.CreateDirectory(filePath);
            }
            catch { }

            // ----- Data -----
            int imgNum = 0;
            foreach (var item in con)
            {
                // ----- Images -----
                string imgFileName = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + ".jpg";
                ExportImage(ref imgFileName, item.Image);
                try
                {
                    imgFileName = Path.GetFileName(imgFileName);
                }
                catch { }

                // ----- Other data -----
                lines += item.Name.Trim().Replace(";", "//") + ";" + item.Category.Trim().Replace(";", "//") + ";" + item.Subcategory.Trim().Replace(";", "//") + ";" + 
                    item.Keywords.Trim().Replace(";", "//") + ";" + item.Note.ToString() + ";" + item.Description.Trim().Replace(";", "//") + ";" +
                    imgFileName + ";" + item.Rating.ToString() + ";" + item.MyRating.ToString() + ";" + item.FastTags.ToString() + ";" + item.Updated.ToString() + ";" + 
                    (item.Active ?? true).ToString() + ";" + item.Version.Trim().Replace(";", "//") + ";" + item.Files.Trim().Replace(";", "//") + ";" + item.Folder.Trim().Replace(";", "//") + ";" +
                    item.Type.Trim().Replace(";", "//") + ";" + item.ObjectNum.Trim().Replace(";", "//") + ";" + item.Language.Trim().Replace(";", "//") + ";" + item.Parent.ToString() + ";" + 
                    item.Customer.Trim().Replace(";", "//") + ";" + item.Development.Trim().Replace(";", "//") + ";" + (item.IsParent ?? true).ToString() + ";" +
                    item.UsedObjects.Trim().Replace(";", "//") + ";" + item.ID + Environment.NewLine;

                imgNum++;
            }

            return Files.SaveFile(path, lines);
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
                string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files" + Path.DirectorySeparatorChar + item[23];

                Contacts contact = new Contacts();
                contact.Name = item[0].Replace("//", ";");
                contact.Surname = item[1].Replace("//", ";");
                contact.Nick = item[2].Replace("//", ";");
                contact.Sex = item[3].Replace("//", ";");
                contact.Birth = Conv.ToDateTimeNull(item[4]);
                contact.Phone = item[5].Replace("//", ";");
                contact.Email = item[6].Replace("//", ";");
                contact.WWW = item[7].Replace("//", ";");
                contact.IM = item[8].Replace("//", ";");
                contact.Company = item[9].Replace("//", ";");
                contact.Position = item[10].Replace("//", ";");
                contact.Street = item[11].Replace("//", ";");
                contact.City = item[12].Replace("//", ";");
                contact.Region = item[13].Replace("//", ";");
                contact.Country = item[14].Replace("//", ";");
                contact.PostCode = item[15].Replace("//", ";");
                contact.PersonCode = item[16].Replace("//", ";");
                contact.Note = item[17].Replace("//", ";");
                contact.Tags = item[18].Replace("//", ";");
                contact.FastTags = Conv.ToShortDef(item[19], 0);
                contact.Updated = Conv.ToDateTimeNull(item[20]);
                contact.GoogleID = item[21].Replace("//", ";");
                contact.Active = Conv.ToBoolDef(item[22], true);
                if (item[23] != "")
                    contact.Avatar = Files.LoadBinFile(imgPath);
                contact.ID = Conv.ToGuid(item[24]);
                con.Add(contact);
            }

            return con;
        }

        private static string CValueToString(List<cValue> val)
        {
            string res = "";

            if (val != null)
            {
                foreach(var item in val)
                {
                    if (res != "") res += ";";
                    res += item.Value + "," + item.Desc;
                }
            }

            return res;
        }

        private static List<cValue> StringToCValue(string text)
        {
            List<cValue> list = new List<cValue>();

            if (text == "") return null;

            string[] items = text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < items.Length; i++)
            {
                cValue val = new cValue() { Desc = "", Value = "", Primary = false};
                string[] split = items[i].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                val.Primary = true;
                if (split.Length > 0)
                    val.Value = split[0];
                if (split.Length > 1)
                    val.Desc = split[1];

                list.Add(val);
            }

            return list;
        }

        private static string GetCompany(List<cCompany> val)
        {
            string res = "";

            if (val != null)
            {
                foreach (var item in val)
                {
                    if (res != "") res += ";";
                    res += item.Name;
                }
            }
            return res;
        }

        private static string GetPosition(List<cCompany> val)
        {
            string res = "";

            if (val != null)
            {
                foreach (var item in val)
                {
                    if (res != "") res += ";";
                    res += item.Position;
                }
            }
            return res;
        }

        private static string GetStreet(List<cAddress> val)
        {

            if (val != null && val.Count > 0)
            {
                foreach (var item in val)
                {
                    if (item.Primary)
                    {
                        return item.Street;
                    }
                }
                return val[0].Street;
            }
            return "";
        }
        
        private static string GetCity(List<cAddress> val)
        {
            if (val != null && val.Count > 0)
            {
                foreach (var item in val)
                {
                    if (item.Primary)
                    {
                        return item.City;
                    }
                }
                return val[0].City;
            }
            return "";
        }

        private static string GetRegion(List<cAddress> val)
        {
            if (val != null && val.Count > 0)
            {
                foreach (var item in val)
                {
                    if (item.Primary)
                    {
                        return item.Region;
                    }
                }
                return val[0].Region;
            }
            return "";
        }
        
        private static string GetCountry(List<cAddress> val)
        {
            if (val != null && val.Count > 0)
            {
                foreach (var item in val)
                {
                    if (item.Primary)
                    {
                        return item.Country;
                    }
                }
                return val[0].Country;
            }
            return "";
        }

        private static string GetPostCode(List<cAddress> val)
        {
            if (val != null && val.Count > 0)
            {
                foreach (var item in val)
                {
                    if (item.Primary)
                    {
                        return item.ZipCode;
                    }
                }
                return val[0].ZipCode;
            }
            return "";
        }

        private static Contacts ConvertToContacts(contacts item)
        {
            Contacts contact = new Contacts();
            contact.Name = item.Name.Firstname;
            if (item.Name.AdditionalName != null && item.Name.AdditionalName != "")
                contact.Name += " " + item.Name.AdditionalName;
            contact.Surname = item.Name.Surname;
            contact.Nick = item.Name.Nick;
            contact.Sex = item.Genre;
            if (item.BirthDate != DateTime.MinValue)
                contact.Birth = item.BirthDate;
            contact.Phone = CValueToString(item.Phone);
            contact.Email = CValueToString(item.Email);
            contact.WWW = CValueToString(item.Web);
            contact.IM = CValueToString(item.IM);
            contact.Company = GetCompany(item.Company);
            contact.Position = GetPosition(item.Company);
            contact.Street = GetStreet(item.Address);
            contact.City = GetCity(item.Address);
            contact.Region = GetRegion(item.Address);
            contact.Country = GetCountry(item.Address);
            contact.PostCode = GetPostCode(item.Address);
            contact.PersonCode = "";
            contact.Note = item.Note;
            contact.Tags = item.Group;
            contact.FastTags = 0;
            contact.Updated = DateTime.Now;
            contact.GoogleID = item.gID;
            contact.Active = true;
            
            CheckNullContacts(ref contact);
            return contact;
        }

        private static void FillContacts(ref contacts item, Contacts contact)
        {
            item.Name.Firstname = "";
            item.Name.AdditionalName = "";
            string[] split = contact.Name.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length > 0)
                item.Name.Firstname = split[0];
            for (int i = 1; i < split.Length; i++)
            {
                if (item.Name.AdditionalName != "") item.Name.AdditionalName += " ";
                item.Name.AdditionalName += split[i];
            }
                
            item.Name.Surname = contact.Surname;
            item.Name.Nick = contact.Nick;
            item.Name.FullName = item.Name.Firstname;
            if (item.Name.AdditionalName != "") item.Name.FullName += " " + item.Name.AdditionalName;
            if (item.Name.Surname != "") item.Name.FullName += " " + item.Name.Surname;
            item.Genre = contact.Sex.Trim();

            if (contact.Birth != null)
                item.BirthDate = contact.Birth ?? DateTime.MinValue;

            item.Phone = StringToCValue(contact.Phone);
            item.Email = StringToCValue(contact.Email);
            item.Web = StringToCValue(contact.WWW);
            item.IM = StringToCValue(contact.IM);

            item.Company = new List<cCompany>();
            item.Company.Add(new cCompany()
            {
                Name = contact.Company,
                Position = contact.Position
            });

            /*
            contact.Street = GetStreet(item.Address);
            contact.City = GetCity(item.Address);
            contact.Region = GetRegion(item.Address);
            contact.Country = GetCountry(item.Address);
            contact.PostCode = GetPostCode(item.Address);*/
            //contact.PersonCode = "";
            item.Note = contact.Note;
            item.Group = contact.Tags;
            //contact.FastTags = 0;
            //contact.Updated = DateTime.Now;
            //contact.GoogleID = item.gID;
            //contact.Active = true;

        }

        /// <summary>
        /// Import Contacts from Google
        /// </summary>
        /// <returns>Contact list</returns>
        public static List<Contacts> ImportContactsGoogle()
        {
            List<Contacts> con = new List<Contacts>();

            GoogleContacts GC = new GoogleContacts();
            string user = Properties.Settings.Default.LastGUserCred;
            if (GC.Login(ref user))
            {
                GC.ImportGmail();
            }
            else
                return null;
            Properties.Settings.Default.LastGUserCred = user;
            Properties.Settings.Default.Save();

            // ----- Parse data -----
            foreach (var item in GC.Contact)
            {
                //string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_images" + Path.DirectorySeparatorChar + item[23];

                Contacts contact = ConvertToContacts(item);
                contact.Avatar = Conv.StreamToByteArray(GC.GetAvatar(item.AvatarUri));
                con.Add(contact);
            }

            return con;
        }

        public static Contacts ImportContactGoogle(string GoogleID)
        {
            Contacts con = new Contacts();

            GoogleContacts GC = new GoogleContacts();
            string user = Properties.Settings.Default.LastGUserCred;
            if (GC.Login(ref user))
            {
                contacts item = GC.GetContact(GoogleID);
                con = ConvertToContacts(item);
                con.Avatar = Conv.StreamToByteArray(GC.GetAvatar(item.AvatarUri));
            }
            else
                return null;
            Properties.Settings.Default.LastGUserCred = user;
            Properties.Settings.Default.Save();
            return con;
        }

        public static bool ExportContactGoogle(Contacts contacts)
        {
            
            GoogleContacts GC = new GoogleContacts();
            string user = Properties.Settings.Default.LastGUserCred;
            if (GC.Login(ref user))
            {
                contacts item = GC.GetContact(contacts.GoogleID);
                FillContacts(ref item, contacts);

                GC.UpdateContact(item);
                //GC.UpdateContactPhoto(item.AvatarUri, con.Avatar)
                // = Conv.StreamToByteArray(GC.GetAvatar(item.AvatarUri));
            }
            else
                return false;
            Properties.Settings.Default.LastGUserCred = user;
            Properties.Settings.Default.Save();
            return true;
        }



        public static void CheckNullContacts(ref Contacts contact)
        {
            if (contact.Name == null) contact.Name = "";
            if (contact.Surname == null) contact.Surname = "";
            if (contact.Nick == null) contact.Nick = "";
            if (contact.Sex == null) contact.Sex = "";
            if (contact.Phone == null) contact.Phone = "";
            if (contact.Email == null) contact.Email = "";
            if (contact.WWW == null) contact.WWW = "";
            if (contact.IM == null) contact.IM = "";
            if (contact.Company == null) contact.Company = "";
            if (contact.Position == null) contact.Position = "";
            if (contact.Street == null) contact.Street = "";
            if (contact.City == null) contact.City = "";
            if (contact.Region == null) contact.Region = "";
            if (contact.Country == null) contact.Country = "";
            if (contact.PostCode == null) contact.PostCode = "";
            if (contact.PersonCode == null) contact.PersonCode = "";
            if (contact.Note == null) contact.Note = "";
            if (contact.Tags == null) contact.Tags = "";
            if (contact.GoogleID == null) contact.GoogleID = "";
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
        
        /// <summary>
        /// Import Boardgames from CSV
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Boardgame list</returns>
        public static List<Boardgames> ImportBoardgamesCSV(string path, out List<Copies> copies)
        {
            List<Boardgames> con = new List<Boardgames>();
            copies = null;

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Check File Head -----
            if (!Str.GetFirstLine(ref text, true).Contains("FialotCatalog:Boardgames"))
                return null;

            // ----- Parse CSV File -----
            CSVfile file = Files.ParseCSV(text);

            // ----- Check table size -----
            if (file.head.Length != 30)
                return null;

            // ----- Import Copies -----
            string filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
            copies = ImportCopiesCSV(filePath + Path.DirectorySeparatorChar + "copies.csv");

            foreach (var item in file.data)
            {
                //MaterialPath;Rating;MyRating;URL;Excluded;FastTags;Updated;GUID" + Environment.NewLine;

                string CoverPath = filePath + Path.DirectorySeparatorChar + item[18];
                string Img1Path = filePath + Path.DirectorySeparatorChar + item[19];
                string Img2Path = filePath + Path.DirectorySeparatorChar + item[20];
                string Img3Path = filePath + Path.DirectorySeparatorChar + item[21];

                Boardgames itm = new Boardgames();
                itm.Name = item[0];
                itm.Category = item[1];
                itm.MinPlayers = Conv.ToShortNull(item[2]);
                itm.MaxPlayers = Conv.ToShortNull(item[3]);
                itm.MinAge = Conv.ToShortNull(item[4]);
                itm.GameTime = Conv.ToShortNull(item[5]);
                itm.GameWorld = item[6];
                itm.Language = item[7];
                itm.Publisher = item[8];
                itm.Author = item[9];
                itm.Year = Conv.ToShortNull(item[10]);
                itm.Description = item[11];
                itm.Keywords = item[12];
                itm.Note = item[13];
                itm.Family = item[14];
                itm.Extension = Conv.ToBoolNull(item[15]);
                itm.ExtensionNumber = Conv.ToShortNull(item[16]);
                itm.Rules = item[17];
                if (item[18] != "")
                    itm.Cover = Files.LoadBinFile(CoverPath);
                if (item[19] != "")
                    itm.Img1 = Files.LoadBinFile(Img1Path);
                if (item[20] != "")
                    itm.Img2 = Files.LoadBinFile(Img2Path);
                if (item[21] != "")
                    itm.Img3 = Files.LoadBinFile(Img3Path);
                itm.MaterialPath = item[22];
                itm.Rating = Conv.ToShortNull(item[23]);
                itm.MyRating = Conv.ToShortNull(item[24]);
                itm.URL = item[25];
                itm.Excluded = Conv.ToBoolNull(item[26]);
                itm.FastTags = Conv.ToShortNull(item[27]);
                itm.Updated = Conv.ToDateTimeNull(item[28]);
                itm.ID = Conv.ToGuid(item[29]);
                con.Add(itm);
            }

            return con;
        }


        /// <summary>
        /// Import Objects from CSV
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Contact list</returns>
        public static List<Objects> ImportObjectsCSV(string path)
        {
            List<Objects> objList = new List<Objects>();

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Check File Head -----
            if (!Str.GetFirstLine(ref text, true).Contains("FialotCatalog:Objects"))
                return null;

            // ----- Parse CSV File -----
            CSVfile file = Files.ParseCSV(text);

            // ----- Check table size -----
            if (file.head.Length != 24)
                return null;

            // ----- Parse data -----
            foreach (var item in file.data)
            {
                string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files" + Path.DirectorySeparatorChar + item[6];

                Objects obj = new Objects();
                obj.Name = item[0].Replace("//", ";");
                obj.Category = item[1].Replace("//", ";");
                obj.Subcategory = item[2].Replace("//", ";");
                obj.Keywords = item[3].Replace("//", ";");
                obj.Note = item[4].Replace("//", ";").Replace("\n", Environment.NewLine);
                obj.Description = item[5].Replace("//", ";").Replace("\n", Environment.NewLine);
                if (item[6] != "")
                    obj.Image = Files.LoadBinFile(imgPath);
                obj.Rating = Conv.ToShortNull(item[7]);
                obj.MyRating = Conv.ToShortNull(item[8]);
                obj.FastTags = Conv.ToShortDef(item[9], 0);
                obj.Updated = Conv.ToDateTimeNull(item[10]);
                obj.Active = Conv.ToBoolDef(item[11], true);
                obj.Version = item[12].Replace("//", ";");
                obj.Files = item[13].Replace("//", ";");
                obj.Folder = item[14].Replace("//", ";");
                obj.Type = item[15].Replace("//", ";");
                obj.ObjectNum = item[16].Replace("//", ";");
                obj.Language = item[17].Replace("//", ";");
                obj.Parent = Conv.ToGuidNull(item[18]);
                obj.Customer = item[19].Replace("//", ";");
                obj.Development = item[20].Replace("//", ";");
                obj.IsParent = Conv.ToBoolDef(item[21], true);
                obj.UsedObjects = item[22].Replace("//", ";");


                obj.ID = Conv.ToGuid(item[23]);
                objList.Add(obj);
            }

            return objList;
        }


        #endregion
    }


}
