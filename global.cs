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
using System.Xml.Linq;

namespace Katalog
{
    #region Enum Defines 

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

    #endregion

    #region Structure defines

    public class PInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }

    public class PInfoComparer : IComparer<PInfo>
    {
        public int Compare(PInfo x, PInfo y)
        {
            if (x.Name == null || y.Name == null)
            {
                return 0;
            }

            // CompareTo() method 
            return x.Name.CompareTo(y.Name);

        }
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

    #endregion


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

        public static List<Objects> GetObjectsFromText(string text)
        {
            List<Objects> list = new List<Objects>();
            databaseEntities db = new databaseEntities();       // Database

            string[] strList = text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in strList)
            {
                try
                {
                    Guid ID = Guid.Parse(item);
                    Objects obj = db.Objects.Find(ID);
                    if (obj != null)
                        list.Add(obj);

                } catch { }
                
            }
            return list;
        }

        public static string GetTextFromObjects(List<Objects> list)
        {
            string res = "";

            foreach (var item in list)
            {
                if (res != "") res += ";";
                res += item.ID.ToString();
            }

            return res;
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
                if (copy != null)
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
        /// Export XML Copies
        /// </summary>
        /// <param name="copies">Copies list</param>
        /// <returns>Copies elements</returns>
        public static XElement ExportCopiesXML(List<Copies> copies)
        {
            var xmlCopies = new XElement("Copies");

            foreach (var copy in copies)
            {
                var xmlCopy = new XElement("Copy");

                xmlCopy.Add(
                    new XElement("ItemName", global.GetLendingItemName(copy.ItemType, copy.ItemID ?? Guid.Empty)),
                    new XElement("ItemType", copy.ItemType.Trim()),
                    new XElement("ItemID", copy.ItemID.ToString()),
                    new XElement("ItemNum", copy.ItemNum.ToString()),
                    new XElement("InventoryNumber", copy.InventoryNumber.Trim()),
                    new XElement("Barcode", copy.Barcode.ToString()),
                    new XElement("Condition", copy.Condition.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("Location", copy.Location.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("Note", copy.Note.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("AcquisitionDate", copy.AcquisitionDate.ToString()),
                    new XElement("Price", copy.Price.ToString()),
                    new XElement("Excluded", copy.Excluded.ToString()),
                    new XElement("Status", copy.Status.ToString()),
                    new XElement("ID", copy.ID.ToString())
                );

                xmlCopies.Add(xmlCopy);
            }

            return xmlCopies;
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
                lines += item.Manufacturer + ";" + item.Note.Trim().Replace(Environment.NewLine, "\\n") + ";" + item.Excluded.ToString() + ";" + item.Count.ToString() + ";" ;
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
                lines += item.URL + ";" + item.Note.Replace(Environment.NewLine, "\\n") + ";" + item.Note1.Replace(Environment.NewLine, "\\n") + ";" + item.Note2.Replace(Environment.NewLine, "\\n") + ";" + item.Content.Replace(Environment.NewLine, "\\n") + ";";
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
        /// Export Books to XML file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="itm">Books list</param>
        /// <returns>Return True if saved succesfully</returns>
        public static bool ExportBooksXML(string path, List<Books> itm)
        {

            databaseEntities db = new databaseEntities();

            // ----- Create files path -----
            string filePath = "";
            try
            {
                filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
                Directory.CreateDirectory(filePath);
            }
            catch { }

            // ----- Create XML document -----
            XDocument doc = new XDocument(
              new XDeclaration("1.0", "utf-8", null)
            );

            var data = new XElement("Data",
              new XElement("Info",
                new XElement("Type", "FialotCatalog:Books"),
                new XElement("Version", "1")
              )
            );

            var items = new XElement("Items");


            int imgNum = 0;
            foreach (var item in itm)
            {
                string imgFileName = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + ".jpg";
                ExportImage(ref imgFileName, item.Cover);
                try
                {
                    imgFileName = Path.GetFileName(imgFileName);
                }
                catch { }

                var objItem = new XElement("Book");

                var xmlGeneral = new XElement("General");

                if (item.Title != "") xmlGeneral.Add(new XElement("Title", item.Title.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.AuthorName != "") xmlGeneral.Add(new XElement("AuthorName", item.AuthorName.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.AuthorSurname != "") xmlGeneral.Add(new XElement("AuthorSurname", item.AuthorSurname.ToString()));
                if (item.Note != "") xmlGeneral.Add(new XElement("Note", item.Note.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.Note1 != "") xmlGeneral.Add(new XElement("Note1", item.Note1.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.Note2 != "") xmlGeneral.Add(new XElement("Note2", item.Note2.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.ISBN != "") xmlGeneral.Add(new XElement("ISBN", item.ISBN.ToString()));
                if (item.Illustrator != "") xmlGeneral.Add(new XElement("Illustrator", item.Illustrator.ToString().Replace(Environment.NewLine, "\\n")));
                if (item.Translator != "") xmlGeneral.Add(new XElement("Translator", item.Translator.ToString().Replace(Environment.NewLine, "\\n")));
                if (item.Language != "") xmlGeneral.Add(new XElement("Language", item.Language.Replace(Environment.NewLine, "\\n")));
                if (item.Publisher != "") xmlGeneral.Add(new XElement("Publisher", item.Publisher.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.Edition != "") xmlGeneral.Add(new XElement("Edition", item.Edition.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.Year != null) xmlGeneral.Add(new XElement("Year", item.Year.ToString()));
                if (item.Pages != null) xmlGeneral.Add(new XElement("Pages", item.Pages.ToString()));
                if (item.URL != "") xmlGeneral.Add(new XElement("URL", item.URL.ToString()));
                if (item.MainCharacter != "") xmlGeneral.Add(new XElement("MainCharacter", item.MainCharacter.ToString().Replace(Environment.NewLine, "\\n")));
                if (item.Content != "") xmlGeneral.Add(new XElement("Content", item.Content.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.OrigName != "") xmlGeneral.Add(new XElement("OrigName", item.OrigName.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.OrigLanguage != "") xmlGeneral.Add(new XElement("OrigLanguage", item.OrigLanguage.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.OrigYear != null) xmlGeneral.Add(new XElement("OrigYear", item.OrigYear.ToString()));
                if (item.Series != "") xmlGeneral.Add(new XElement("Series", item.Series.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.SeriesNum != null) xmlGeneral.Add(new XElement("SeriesNum", item.SeriesNum.ToString()));
                if (item.Type != "") xmlGeneral.Add(new XElement("Type", item.Type.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.Bookbinding != "") xmlGeneral.Add(new XElement("Bookbinding", item.Bookbinding.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.EbookType != "") xmlGeneral.Add(new XElement("EbookType", item.EbookType.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.Publication != null) xmlGeneral.Add(new XElement("Publication", item.Publication.ToString()));

                objItem.Add(xmlGeneral);

                var xmlClassification = new XElement("Classification");

                if (item.Genre != "") xmlClassification.Add(new XElement("Genre", item.Genre.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.SubGenre != "") xmlClassification.Add(new XElement("SubGenre", item.SubGenre.Trim().Replace(Environment.NewLine, "\\n")));
                if (item.Keywords != "") xmlClassification.Add(new XElement("Keywords", item.Keywords.Trim()));
                if (item.FastTags != null) xmlClassification.Add(new XElement("FastTags", item.FastTags.ToString()));
                                
                objItem.Add(xmlClassification);


                var xmlRating = new XElement("Rating");

                if (item.Rating != null) xmlRating.Add(new XElement("Rating", item.Rating.ToString()));
                if (item.MyRating != null) xmlRating.Add(new XElement("MyRating", item.MyRating.ToString()));

                objItem.Add(xmlRating);


                var xmlSystem = new XElement("System");

                if (item.ID != Guid.Empty) xmlSystem.Add(new XElement("ID", item.ID));
                if (item.Updated != null) xmlSystem.Add(new XElement("Updated", item.Updated.ToString()));
                if (item.Excluded != null) xmlSystem.Add(new XElement("Excluded", Conv.ToString(item.Excluded ?? false)));
                if (imgFileName != "") xmlSystem.Add(new XElement("Cover", imgFileName));
                if (item.Readed != null) xmlSystem.Add(new XElement("Readed", Conv.ToString(item.Readed ?? false)));

                objItem.Add(xmlSystem);
                
                items.Add(objItem);
                imgNum++;
            }

            // ----- Copies -----
            var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.book.ToString())).ToList();

            var xmlCopies = ExportCopiesXML(copies);


            data.Add(items);
            data.Add(xmlCopies);
            doc.Add(data);


            var wr = new Utf8StringWriter();
            doc.Save(wr);
            return Files.SaveFile(path, wr.ToString());
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
                lines += item.Language + ";" + item.Publisher + ";" + item.Author + ";" + item.Year + ";" + item.Description.Replace(Environment.NewLine, "\\n") + ";" + item.Keywords.Trim().Replace(";", ",") + ";";
                lines += item.Note.Replace(Environment.NewLine, "\\n") + ";" + item.Family + ";" + item.Extension + ";" + item.ExtensionNumber + ";" + item.Rules.Replace(Environment.NewLine, "\\n") + ";";
                lines += imgCover + ";" + img1 + ";" + img2 + ";" + img3 + ";" + item.MaterialPath + ";" + item.Rating + ";" + item.MyRating + ";" + item.URL + ";";
                lines += item.Excluded + ";" + item.FastTags + ";" + item.Updated + ";" + item.ID + Environment.NewLine;

                imgNum++;
            }

            // ----- Save to file ------
            Files.SaveFile(path, lines);
        }


        /// <summary>
        /// Export Board games to XML file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="itm">Recipes list</param>
        /// <returns>Return True if saved succesfully</returns>
        public static bool ExportBoardXML(string path, List<Boardgames> itm)
        {

            databaseEntities db = new databaseEntities();

            // ----- Create files path -----
            string filePath = "";
            try
            {
                filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
                Directory.CreateDirectory(filePath);
            }
            catch { }

            // ----- Create XML document -----
            XDocument doc = new XDocument(
              new XDeclaration("1.0", "utf-8", null)
            );

            var data = new XElement("Data",
              new XElement("Info",
                new XElement("Type", "FialotCatalog:Boardgames"),
                new XElement("Version", "1")
              )
            );

            var items = new XElement("Items");


            int imgNum = 0;
            foreach (var item in itm)
            {
                string imgFileName = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + ".jpg";
                string imgFileNameA = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + "A.jpg";
                string imgFileNameB = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + "B.jpg";
                string imgFileNameC = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + "C.jpg";
                ExportImage(ref imgFileName, item.Cover);
                ExportImage(ref imgFileNameA, item.Img1);
                ExportImage(ref imgFileNameB, item.Img2);
                ExportImage(ref imgFileNameC, item.Img3);
                try
                {
                    imgFileName = Path.GetFileName(imgFileName);
                }
                catch { }
                try
                {
                    imgFileNameA = Path.GetFileName(imgFileNameA);
                }
                catch { }
                try
                {
                    imgFileNameB = Path.GetFileName(imgFileNameB);
                }
                catch { }
                try
                {
                    imgFileNameC = Path.GetFileName(imgFileNameC);
                }
                catch { }

                var objItem = new XElement("BoardGame");

                objItem.Add(new XElement("General",
                    new XElement("Name", item.Name.Trim()),
                    new XElement("Description", item.Description.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("Note", item.Note.Trim().Replace(Environment.NewLine, "\\n")),

                    new XElement("MinAge", item.MinAge.ToString()),
                    new XElement("MinPlayers", item.MinPlayers.ToString()),
                    new XElement("MaxPlayers", item.MaxPlayers.ToString()),
                    new XElement("GameTime", item.GameTime.ToString()),
                    new XElement("GameWorld", item.GameWorld.Replace(Environment.NewLine, "\\n")),
                    new XElement("Language", item.Language.Trim()),
                    new XElement("Publisher", item.Publisher.Trim()),
                    new XElement("Author", item.Author.Trim()),
                    new XElement("Year", item.Year.ToString()),
                    new XElement("Extension", item.Extension.ToString()),
                    new XElement("ExtensionNumber", item.ExtensionNumber.ToString()),
                    new XElement("Rules", item.Rules.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("MaterialPath", item.MaterialPath.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("Family", item.Family.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("URL", item.URL.Trim())

                ));

                objItem.Add(new XElement("Classification",
                    new XElement("Category", item.Category.Trim()),
                    //new XElement("Subcategory", item.Subcategory.Trim()),
                    new XElement("Keywords", item.Keywords.Trim()),
                    new XElement("FastTags", item.FastTags.ToString())
                ));
                objItem.Add(new XElement("Rating",
                    new XElement("Rating", item.Rating.ToString()),
                    new XElement("MyRating", item.MyRating.ToString())
                ));


                objItem.Add(new XElement("System",
                    new XElement("ID", item.ID),
                    new XElement("Updated", item.Updated.ToString()),
                    new XElement("Excluded", (item.Excluded ?? false).ToString()),
                    new XElement("Cover", imgFileName),
                    new XElement("Img1", imgFileNameA),
                    new XElement("Img2", imgFileNameB),
                    new XElement("Img3", imgFileNameC)
                ));

                items.Add(objItem);
                imgNum++;
            }

            // ----- Copies -----
            var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.boardgame.ToString())).ToList();

            var xmlCopies = ExportCopiesXML(copies);


            data.Add(items);
            data.Add(xmlCopies);
            doc.Add(data);
            

            var wr = new Utf8StringWriter();
            doc.Save(wr);
            return Files.SaveFile(path, wr.ToString());
        }

        /// <summary>
        /// Export Games to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="itm">Contact list</param>
        /// <returns>Return True if saved succesfully</returns>
        public static bool ExportGamesCSV(string path, List<Games> itm)
        {
            // ----- Head -----
            string lines = "FialotCatalog:Games v1" + Environment.NewLine;

            // ----- Names -----
            lines += "name;category;subcategory;keywords;note;description;image;playerage;minplayers;maxplayers;duration;durationpreparation;things;url;rules;preparation;enviroment;files;rating;myrating;fasttags;updated;excluded;GUID" + Environment.NewLine;

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
                lines += item.Name.Trim().Replace(";", "//") + ";" + item.Category.Trim().Replace(";", "//") + ";" + item.Subcategory.Trim().Replace(";", "//") + ";" +
                    item.Keywords.Trim().Replace(";", "//") + ";" + item.Note.Replace(";", "//").Replace(Environment.NewLine, "\\n") + ";" + item.Description.Trim().Replace(";", "//").Replace(Environment.NewLine, "\\n") + ";" +
                    imgFileName + ";" + item.PlayerAge.Trim().Replace(";", "//") + ";" + item.MinPlayers.ToString() + ";" + item.MaxPlayers.ToString() + ";" + item.Duration.ToString() + ";" +
                    item.DurationPreparation.ToString() + ";" + item.Things.Trim().Replace(";", "//").Replace(Environment.NewLine, "\\n") + ";" + item.URL.Trim().Replace(";", "//") + ";" + item.Rules.Trim().Replace(";", "//").Replace(Environment.NewLine, "\\n") + ";" +
                    item.Preparation.Trim().Replace(";", "//").Replace(Environment.NewLine, "\\n") + ";" + item.Environment.Trim().Replace(";", "//").Replace(Environment.NewLine, "\\n") + ";" + item.Files.Trim().Replace(";", "//") + ";" +
                    item.Rating.ToString() + ";" + item.MyRating.ToString() + ";" + item.FastTags.ToString() + ";" + item.Updated.ToString() + ";" +
                    (item.Excluded ?? false).ToString() + ";" + item.ID + Environment.NewLine;

                imgNum++;
            }

            return Files.SaveFile(path, lines);
        }


        /// <summary>
        /// Export Games to XML file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="itm">Recipes list</param>
        /// <returns>Return True if saved succesfully</returns>
        public static bool ExportGamesXML(string path, List<Games> itm)
        {

            // ----- Create files path -----
            string filePath = "";
            try
            {
                filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
                Directory.CreateDirectory(filePath);
            }
            catch { }

            // ----- Create XML document -----
            XDocument doc = new XDocument(
              new XDeclaration("1.0", "utf-8", null)
            );

            var data = new XElement("Data",
              new XElement("Info",
                new XElement("Type", "FialotCatalog:Games"),
                new XElement("Version", "1")
              )
            );

            var items = new XElement("Items");


            int imgNum = 0;
            foreach (var item in itm)
            {
                string imgFileName = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + ".jpg";
                ExportImage(ref imgFileName, item.Image);
                try
                {
                    imgFileName = Path.GetFileName(imgFileName);
                }
                catch { }

                var objItem = new XElement("Game");

                objItem.Add(new XElement("General",
                    new XElement("Name", item.Name.Trim()),
                    new XElement("Description", item.Description.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("Note", item.Note.Trim().Replace(Environment.NewLine, "\\n")),

                    new XElement("PlayerAge", item.PlayerAge.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("MinPlayers", item.MinPlayers.ToString()),
                    new XElement("MaxPlayers", item.MaxPlayers.ToString()),
                    new XElement("Duration", item.Duration.ToString()),
                    new XElement("DurationPreparation", item.DurationPreparation.ToString()),
                    new XElement("Things", item.Things.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("Rules", item.Rules.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("Preparation", item.Preparation.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("Environment", item.Environment.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("Files", item.Files.Trim()),
                    new XElement("URL", item.URL.Trim())

                ));

                objItem.Add(new XElement("Classification",
                    new XElement("Category", item.Category.Trim()),
                    new XElement("Subcategory", item.Subcategory.Trim()),
                    new XElement("Keywords", item.Keywords.Trim()),
                    new XElement("FastTags", item.FastTags.ToString())
                ));
                objItem.Add(new XElement("Rating",
                    new XElement("Rating", item.Rating.ToString()),
                    new XElement("MyRating", item.MyRating.ToString())
                ));


                objItem.Add(new XElement("System",
                    new XElement("ID", item.ID),
                    new XElement("Updated", item.Updated.ToString()),
                    new XElement("Excluded", (item.Excluded ?? false).ToString()),
                    new XElement("Image", imgFileName)
                ));

                items.Add(objItem);
                imgNum++;
            }


            data.Add(items);
            doc.Add(data);

            var wr = new Utf8StringWriter();
            doc.Save(wr);
            return Files.SaveFile(path, wr.ToString());
        }


        /// <summary>
        /// Export Recipes to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="itm">Contact list</param>
        /// <returns>Return True if saved succesfully</returns>
        public static bool ExportRecipesCSV(string path, List<Recipes> itm)
        {
            // ----- Head -----
            string lines = "FialotCatalog:Recipes v1" + Environment.NewLine;

            // ----- Names -----
            lines += "name;category;subcategory;keywords;note;description;image;procedure;resources;rating;myrating;fasttags;updated;excluded;GUID" + Environment.NewLine;

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
                lines += item.Name.Trim().Replace(";", "//") + ";" + item.Category.Trim().Replace(";", "//") + ";" + item.Subcategory.Trim().Replace(";", "//") + ";" +
                    item.Keywords.Trim().Replace(";", "//") + ";" + item.Note.Replace(";", "//").Replace(Environment.NewLine, "\\n") + ";" + item.Description.Trim().Replace(";", "//").Replace(Environment.NewLine, "\\n") + ";" +
                    imgFileName + ";" + item.Procedure.Trim().Replace(";", "//").Replace(Environment.NewLine, "\\n") + ";" + item.Resources.Trim().Replace(";", "//").Replace(Environment.NewLine, "\\n") + ";" +
                    item.Rating.ToString() + ";" + item.MyRating.ToString() + ";" + item.FastTags.ToString() + ";" + item.Updated.ToString() + ";" +
                    (item.Excluded ?? false).ToString() + ";" + item.ID + Environment.NewLine;

                imgNum++;
            }

            return Files.SaveFile(path, lines);
        }


        /// <summary>
        /// Export Recipes to XML file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="itm">Recipes list</param>
        /// <returns>Return True if saved succesfully</returns>
        public static bool ExportRecipesXML(string path, List<Recipes> itm)
        {

            // ----- Create files path -----
            string filePath = "";
            try
            {
                filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
                Directory.CreateDirectory(filePath);
            }
            catch { }

            // ----- Create XML document -----
            XDocument doc = new XDocument(
              new XDeclaration("1.0", "utf-8", null)
            );

            var data = new XElement("Data",
              new XElement("Info",
                new XElement("Type", "FialotCatalog:Recipes"),
                new XElement("Version", "1")
              )
            );

            var items = new XElement("Items");


            int imgNum = 0;
            foreach (var item in itm)
            {
                string imgFileName = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + ".jpg";
                ExportImage(ref imgFileName, item.Image);
                try
                {
                    imgFileName = Path.GetFileName(imgFileName);
                }
                catch { }

                var objItem = new XElement("Recipe");

                objItem.Add(new XElement("General",
                    new XElement("Name", item.Name.Trim()),
                    new XElement("Description", item.Description.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("Note", item.Note.Trim().Replace(Environment.NewLine, "\\n")),

                    new XElement("Procedure", item.Procedure.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("Resources", item.Resources.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("URL", item.URL.Trim())

                ));

                objItem.Add(new XElement("Classification",
                    new XElement("Category", item.Category.Trim()),
                    new XElement("Subcategory", item.Subcategory.Trim()),
                    new XElement("Keywords", item.Keywords.Trim()),
                    new XElement("FastTags", item.FastTags.ToString())
                ));
                objItem.Add(new XElement("Rating",
                    new XElement("Rating", item.Rating.ToString()),
                    new XElement("MyRating", item.MyRating.ToString())
                ));


                objItem.Add(new XElement("System",
                    new XElement("ID", item.ID),
                    new XElement("Updated", item.Updated.ToString()),
                    new XElement("Excluded", (item.Excluded ?? false).ToString()),
                    new XElement("Image", imgFileName)
                ));

                items.Add(objItem);
                imgNum++;
            }


            data.Add(items);
            doc.Add(data);

            var wr = new Utf8StringWriter();
            doc.Save(wr);
            return Files.SaveFile(path, wr.ToString());
        }


        /// <summary>
        /// Export Objects to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="itm">Contact list</param>
        /// <returns>Return True if saved succesfully</returns>
        public static bool ExportObjectCSV(string path, List<Objects> itm)
        {
            // ----- Head -----
            string lines = "FialotCatalog:Objects v1" + Environment.NewLine;

            // ----- Names -----
            lines += "name;category;subcategory;keywords;note;description;URL;image;rating;myrating;fasttags;updated;active;version;files;folder;type;objectnum;language;parent;customer;development;isparent;usedobject;GUID" + Environment.NewLine;

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
                lines += item.Name.Trim().Replace(";", "//") + ";" + item.Category.Trim().Replace(";", "//") + ";" + item.Subcategory.Trim().Replace(";", "//") + ";" + 
                    item.Keywords.Trim().Replace(";", "//") + ";" + item.Note.Replace(";", "//").Replace(Environment.NewLine, "\\n") + ";" + item.Description.Trim().Replace(";", "//").Replace(Environment.NewLine, "\\n") + ";" +
                    item.URL.Trim() + ";" +
                    imgFileName + ";" + item.Rating.ToString() + ";" + item.MyRating.ToString() + ";" + item.FastTags.ToString() + ";" + item.Updated.ToString() + ";" + 
                    (item.Active ?? true).ToString() + ";" + item.Version.Trim().Replace(";", "//") + ";" + item.Files.Trim().Replace(";", "//") + ";" + item.Folder.Trim().Replace(";", "//") + ";" +
                    item.Type.Trim().Replace(";", "//") + ";" + item.ObjectNum.Trim().Replace(";", "//") + ";" + item.Language.Trim().Replace(";", "//") + ";" + item.Parent.ToString() + ";" + 
                    item.Customer.Trim().Replace(";", "//") + ";" + item.Development.Trim().Replace(";", "//") + ";" + (item.IsParent ?? true).ToString() + ";" +
                    item.UsedObjects.Trim().Replace(";", "//") + ";" + item.ID + Environment.NewLine;

                imgNum++;
            }

            return Files.SaveFile(path, lines);
        }

        /// <summary>
        /// Export Objects to CSV file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="itm">Contact list</param>
        /// <returns>Return True if saved succesfully</returns>
        public static bool ExportObjectXML(string path, List<Objects> itm)
        {

            // ----- Create files path -----
            string filePath = "";
            try
            {
                filePath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files";
                Directory.CreateDirectory(filePath);
            }
            catch { }

            // ----- Create XML document -----
            XDocument doc = new XDocument(
              new XDeclaration("1.0", "utf-8", null)
            );

            var data = new XElement("Data",
              new XElement("Info",
                new XElement("Type", "FialotCatalog:Objects"),
                new XElement("Version", "1")
              )
            );

            var items = new XElement("Items");

            int imgNum = 0;
            foreach (var item in itm)
            {
                string imgFileName = filePath + Path.DirectorySeparatorChar + "img" + imgNum.ToString("D4") + ".jpg";
                ExportImage(ref imgFileName, item.Image);
                try
                {
                    imgFileName = Path.GetFileName(imgFileName);
                }
                catch { }

                var objItem = new XElement("Object");
                
                objItem.Add(new XElement("General",
                    new XElement("Name", item.Name.Trim()),
                    new XElement("ObjectNumber", item.ObjectNum.Trim()),
                    new XElement("Description", item.Description.Trim().Replace(Environment.NewLine, "\\n")),
                    new XElement("Note", item.Note.Trim().Replace(Environment.NewLine, "\\n")),

                    new XElement("Version", item.Version.Trim()),
                    new XElement("Files", item.Files.Trim()),
                    new XElement("Folder", item.Folder.Trim()),
                    new XElement("URL", item.URL.Trim()),

                    new XElement("Customer", item.Customer.Trim()),
                    new XElement("Development", item.Development.Trim()),

                    new XElement("Language", item.Language.Trim())
                    
                ));

                objItem.Add(new XElement("Classification",
                    new XElement("Type", item.Type.Trim()),
                    new XElement("Category", item.Category.Trim()),
                    new XElement("Subcategory", item.Subcategory.Trim()),
                    new XElement("Keywords", item.Keywords.Trim()),
                    new XElement("FastTags", item.FastTags.ToString())
                ));
                objItem.Add(new XElement("Rating",
                    new XElement("Rating", item.Rating.ToString()),
                    new XElement("MyRating", item.MyRating.ToString())
                ));


                objItem.Add(new XElement("System",
                    new XElement("ID", item.ID),
                    new XElement("Updated", item.Updated.ToString()),
                    new XElement("Active", (item.Active ?? true).ToString()),
                    new XElement("Parent", item.Parent.ToString()),
                    new XElement("IsParent", (item.IsParent ?? true).ToString()),
                    new XElement("UsedObjects", item.UsedObjects.Trim()),
                    new XElement("Image", imgFileName)
                ));

                items.Add(objItem);
                imgNum++;
            }


            data.Add(items);
            doc.Add(data);

            var wr = new Utf8StringWriter();
            doc.Save(wr);
            return Files.SaveFile(path, wr.ToString());
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
                contact.Name = item[0].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.Surname = item[1].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.Nick = item[2].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.Sex = item[3].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.Birth = Conv.ToDateTimeNull(item[4]);
                contact.Phone = item[5].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.Email = item[6].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.WWW = item[7].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.IM = item[8].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.Company = item[9].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.Position = item[10].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.Street = item[11].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.City = item[12].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.Region = item[13].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.Country = item[14].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.PostCode = item[15].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.PersonCode = item[16].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.Note = item[17].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.Tags = item[18].Replace("//", ";").Replace("\\n", Environment.NewLine);
                contact.FastTags = Conv.ToShortDef(item[19], 0);
                contact.Updated = Conv.ToDateTimeNull(item[20]);
                contact.GoogleID = item[21].Replace("//", ";").Replace("\\n", Environment.NewLine);
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
                itm.Note = item[9].Replace("//", ";").Replace("\\n", Environment.NewLine);
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
                itm.Note = item[7].Replace("//", ";").Replace("\\n", Environment.NewLine);
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
                itm.ItemType = item[1].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.ItemID = Conv.ToGuid(item[2]);
                itm.ItemNum = Conv.ToShortNull(item[3]);
                itm.InventoryNumber = item[4].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Condition = item[5].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Location = item[6].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Note = item[7].Replace("//", ";").Replace("\\n", Environment.NewLine);
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
        /// Import Copies from CSV
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Item list</returns>
        public static List<Copies> ImportCopiesXML(IEnumerable<XElement> xmlCopies)
        {

            List<Copies> copies = new List<Copies>();
            
            foreach (var item in xmlCopies)
            {
                Copies obj = new Copies();

                /*if (item.Element("ItemName") != null)
                    obj.ItemName = item.Element("ItemName").Value;
                else obj.ItemName = "";*/

                if (item.Element("ItemType") != null)
                    obj.ItemType = item.Element("ItemType").Value.Replace("\\n", Environment.NewLine);
                else obj.ItemType = "";

                if (item.Element("ItemID") != null)
                    obj.ItemID = Conv.ToGuidNull(item.Element("ItemID").Value);

                if (item.Element("ItemNum") != null)
                    obj.ItemNum = Conv.ToShortNull(item.Element("ItemNum").Value);

                if (item.Element("InventoryNumber") != null)
                    obj.InventoryNumber = item.Element("InventoryNumber").Value.Replace("\\n", Environment.NewLine);
                else obj.InventoryNumber = "";

                if (item.Element("Barcode") != null)
                    obj.Barcode = Conv.ToLongNull(item.Element("Barcode").Value);

                if (item.Element("Condition") != null)
                    obj.Condition = item.Element("Condition").Value.Replace("\\n", Environment.NewLine);
                else obj.Condition = "";

                if (item.Element("Location") != null)
                    obj.Location = item.Element("Location").Value.Replace("\\n", Environment.NewLine);
                else obj.Location = "";

                if (item.Element("Note") != null)
                    obj.Note = item.Element("Note").Value.Replace("\\n", Environment.NewLine);
                else obj.Note = "";

                if (item.Element("AcquisitionDate") != null)
                    obj.AcquisitionDate = Conv.ToDateTimeNull(item.Element("AcquisitionDate").Value);

                if (item.Element("Price") != null)
                    obj.Price = Conv.ToDoubleNull(item.Element("Price").Value);

                if (item.Element("Excluded") != null)
                    obj.Excluded = Conv.ToBoolDef(item.Element("Excluded").Value, false);
                else obj.Excluded = false;

                if (item.Element("Status") != null)
                    obj.Status = Conv.ToShortNull(item.Element("Status").Value);

                if (item.Element("ID") != null)
                    obj.ID = Conv.ToGuid(item.Element("ID").Value);


                copies.Add(obj);
            }

            return copies;
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
                itm.Name = item[0].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Category = item[1].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Subcategory = item[2].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Subcategory2 = item[3].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Keywords = item[4].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Manufacturer = item[5].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Note = item[6].Replace("//", ";").Replace("\\n", Environment.NewLine);
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
                itm.Title = item[0].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.AuthorName = item[1].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.AuthorSurname = item[2].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.ISBN = item[3].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Illustrator = item[4].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Translator = item[5].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Language = item[6].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Publisher = item[7].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Edition = item[8].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Year = Conv.ToShortNull(item[9]);
                itm.Pages = Conv.ToShortNull(item[10]);
                itm.MainCharacter = item[11].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.URL = item[12].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Note = item[13].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Note1 = item[14].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Note2 = item[15].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Content = item[16].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.OrigName = item[17].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.OrigLanguage = item[18].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.OrigYear = Conv.ToShortNull(item[19]);
                itm.Genre = item[20].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.SubGenre = item[21].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Series = item[22].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.SeriesNum = Conv.ToShortNull(item[23]);
                itm.Keywords = item[24].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Rating = Conv.ToShortNull(item[25]);
                itm.MyRating = Conv.ToShortNull(item[26]);
                itm.Readed = Conv.ToBoolNull(item[27]);
                itm.Type = item[28].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Bookbinding = item[29].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.EbookPath = item[30].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.EbookType = item[31].Replace("//", ";").Replace("\\n", Environment.NewLine);
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
        /// Import Books from XML
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Recipes list</returns>
        public static List<Books> ImportBooksXML(string path, out List<Copies> copies)
        {
            List<Books> objList = new List<Books>();
            copies = null;

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Parse XML to Structure -----
            var xml = XDocument.Parse(text);
            var xmlType = xml.Elements().Elements("Info").Elements("Type");

            // ----- Check File Head -----
            if (xmlType.First().Value != "FialotCatalog:Books")
                return null;

            var xmlItems = xml.Elements().Elements("Items").Elements("Book");


            // ----- Parse data -----
            foreach (var item in xmlItems)
            {
                Books obj = new Books();

                var xmlGeneral = item.Element("General");

                if (xmlGeneral.Element("Title") != null)
                    obj.Title = xmlGeneral.Element("Title").Value.Replace("\\n", Environment.NewLine);
                else obj.Title = "";

                if (xmlGeneral.Element("AuthorName") != null)
                    obj.AuthorName = xmlGeneral.Element("AuthorName").Value.Replace("\\n", Environment.NewLine);
                else obj.AuthorName = "";

                if (xmlGeneral.Element("AuthorSurname") != null)
                    obj.AuthorSurname = xmlGeneral.Element("AuthorSurname").Value.Replace("\\n", Environment.NewLine);
                else obj.AuthorSurname = "";

                if (xmlGeneral.Element("Note") != null)
                    obj.Note = xmlGeneral.Element("Note").Value.Replace("\\n", Environment.NewLine);
                else obj.Note = "";

                if (xmlGeneral.Element("Note1") != null)
                    obj.Note1 = xmlGeneral.Element("Note1").Value.Replace("\\n", Environment.NewLine);
                else obj.Note1 = "";

                if (xmlGeneral.Element("Note2") != null)
                    obj.Note2 = xmlGeneral.Element("Note2").Value.Replace("\\n", Environment.NewLine);
                else obj.Note2 = "";

                if (xmlGeneral.Element("ISBN") != null)
                    obj.ISBN = xmlGeneral.Element("ISBN").Value.Replace("\\n", Environment.NewLine);
                else obj.ISBN = "";

                if (xmlGeneral.Element("Illustrator") != null)
                    obj.Illustrator = xmlGeneral.Element("Illustrator").Value.Replace("\\n", Environment.NewLine);
                else obj.Illustrator = "";

                if (xmlGeneral.Element("Translator") != null)
                    obj.Translator = xmlGeneral.Element("Translator").Value.Replace("\\n", Environment.NewLine);
                else obj.Translator = "";

                if (xmlGeneral.Element("Language") != null)
                    obj.Language = xmlGeneral.Element("Language").Value.Replace("\\n", Environment.NewLine);
                else obj.Language = "";

                if (xmlGeneral.Element("Publisher") != null)
                    obj.Publisher = xmlGeneral.Element("Publisher").Value.Replace("\\n", Environment.NewLine);
                else obj.Publisher = "";

                if (xmlGeneral.Element("Edition") != null)
                    obj.Edition = xmlGeneral.Element("Edition").Value.Replace("\\n", Environment.NewLine);
                else obj.Edition = "";

                if (xmlGeneral.Element("Year") != null)
                    obj.Year = Conv.ToShortNull(xmlGeneral.Element("Year").Value);

                if (xmlGeneral.Element("Pages") != null)
                    obj.Pages = Conv.ToShortNull(xmlGeneral.Element("Pages").Value);

                if (xmlGeneral.Element("URL") != null)
                    obj.URL = xmlGeneral.Element("URL").Value.Replace("\\n", Environment.NewLine);
                else obj.URL = "";

                if (xmlGeneral.Element("MainCharacter") != null)
                    obj.MainCharacter = xmlGeneral.Element("MainCharacter").Value.Replace("\\n", Environment.NewLine);
                else obj.MainCharacter = "";

                if (xmlGeneral.Element("Content") != null)
                    obj.Content = xmlGeneral.Element("Content").Value.Replace("\\n", Environment.NewLine);
                else obj.Content = "";

                if (xmlGeneral.Element("OrigName") != null)
                    obj.OrigName = xmlGeneral.Element("OrigName").Value.Replace("\\n", Environment.NewLine);
                else obj.OrigName = "";

                if (xmlGeneral.Element("OrigLanguage") != null)
                    obj.OrigLanguage = xmlGeneral.Element("OrigLanguage").Value.Replace("\\n", Environment.NewLine);
                else obj.OrigLanguage = "";

                if (xmlGeneral.Element("OrigYear") != null)
                    obj.OrigYear = Conv.ToShortNull(xmlGeneral.Element("OrigYear").Value);

                if (xmlGeneral.Element("Series") != null)
                    obj.Series = xmlGeneral.Element("Series").Value.Replace("\\n", Environment.NewLine);
                else obj.Series = "";

                if (xmlGeneral.Element("SeriesNum") != null)
                    obj.SeriesNum = Conv.ToLongNull(xmlGeneral.Element("SeriesNum").Value);

                if (xmlGeneral.Element("Type") != null)
                    obj.Type = xmlGeneral.Element("Type").Value.Replace("\\n", Environment.NewLine);
                else obj.Type = "";

                if (xmlGeneral.Element("Bookbinding") != null)
                    obj.Bookbinding = xmlGeneral.Element("Bookbinding").Value.Replace("\\n", Environment.NewLine);
                else obj.Bookbinding = "";

                if (xmlGeneral.Element("EbookPath") != null)
                    obj.EbookPath = xmlGeneral.Element("EbookPath").Value.Replace("\\n", Environment.NewLine);
                else obj.EbookPath = "";

                if (xmlGeneral.Element("EbookType") != null)
                    obj.EbookType = xmlGeneral.Element("EbookType").Value.Replace("\\n", Environment.NewLine);
                else obj.EbookType = "";

                if (xmlGeneral.Element("Publication") != null)
                    obj.Publication = Conv.ToShortNull(xmlGeneral.Element("Publication").Value);



                var xmlClass = item.Element("Classification");

                if (xmlClass.Element("Genre") != null)
                    obj.Genre = xmlClass.Element("Genre").Value;
                else obj.Genre = "";

                if (xmlClass.Element("SubGenre") != null)
                    obj.SubGenre = xmlClass.Element("SubGenre").Value;
                else obj.SubGenre = "";

                if (xmlClass.Element("Keywords") != null)
                    obj.Keywords = xmlClass.Element("Keywords").Value;
                else obj.Keywords = "";

                if (xmlClass.Element("FastTags") != null)
                    obj.FastTags = Conv.ToShortDef(xmlClass.Element("FastTags").Value, 0);
                else obj.FastTags = 0;


                var xmlRating = item.Element("Rating");

                if (xmlRating.Element("Rating") != null)
                    obj.Rating = Conv.ToShortNull(xmlRating.Element("Rating").Value);

                if (xmlRating.Element("MyRating") != null)
                    obj.MyRating = Conv.ToShortNull(xmlRating.Element("MyRating").Value);


                var xmlSystem = item.Element("System");

                if (xmlSystem.Element("ID") != null)
                    obj.ID = Conv.ToGuid(xmlSystem.Element("ID").Value);

                if (xmlSystem.Element("Updated") != null)
                    obj.Updated = Conv.ToDateTimeNull(xmlSystem.Element("Updated").Value);

                if (xmlSystem.Element("Excluded") != null)
                    obj.Excluded = Conv.ToBoolDef(xmlSystem.Element("Excluded").Value, false);
                else obj.Excluded = false;

                if (xmlSystem.Element("Readed") != null)
                    obj.Readed = Conv.ToBoolDef(xmlSystem.Element("Readed").Value, false);
                else obj.Readed = false;

                if (xmlSystem.Element("Cover") != null)
                {
                    string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files" + Path.DirectorySeparatorChar + xmlSystem.Element("Cover").Value;

                    obj.Cover = Files.LoadBinFile(imgPath);
                }

               

                objList.Add(obj);
            }

            // ----- Parse copies -----
            IEnumerable<XElement> xmlCopies = xml.Elements().Elements("Copies").Elements("Copy");

            copies = ImportCopiesXML(xmlCopies);

            return objList;
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
                itm.Name = item[0].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Category = item[1].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.MinPlayers = Conv.ToShortNull(item[2]);
                itm.MaxPlayers = Conv.ToShortNull(item[3]);
                itm.MinAge = Conv.ToShortNull(item[4]);
                itm.GameTime = Conv.ToShortNull(item[5]);
                itm.GameWorld = item[6].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Language = item[7].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Publisher = item[8].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Author = item[9].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Year = Conv.ToShortNull(item[10]);
                itm.Description = item[11].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Keywords = item[12].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Note = item[13].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Family = item[14].Replace("//", ";").Replace("\\n", Environment.NewLine);
                itm.Extension = Conv.ToBoolNull(item[15]);
                itm.ExtensionNumber = Conv.ToShortNull(item[16]);
                itm.Rules = item[17].Replace("//", ";").Replace("\\n", Environment.NewLine);
                if (item[18] != "")
                    itm.Cover = Files.LoadBinFile(CoverPath);
                if (item[19] != "")
                    itm.Img1 = Files.LoadBinFile(Img1Path);
                if (item[20] != "")
                    itm.Img2 = Files.LoadBinFile(Img2Path);
                if (item[21] != "")
                    itm.Img3 = Files.LoadBinFile(Img3Path);
                itm.MaterialPath = item[22].Replace("//", ";").Replace("\\n", Environment.NewLine);
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
        /// Import Board games from XML
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Recipes list</returns>
        public static List<Boardgames> ImportBoardgamesXML(string path, out List<Copies> copies)
        {
            List<Boardgames> objList = new List<Boardgames>();
            copies = null;

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Parse XML to Structure -----
            var xml = XDocument.Parse(text);
            var xmlType = xml.Elements().Elements("Info").Elements("Type");

            // ----- Check File Head -----
            if (xmlType.First().Value != "FialotCatalog:Boardgames")
                return null;

            var xmlItems = xml.Elements().Elements("Items").Elements("BoardGame");


            // ----- Parse data -----
            foreach (var item in xmlItems)
            {
                Boardgames obj = new Boardgames();

                var xmlGeneral = item.Element("General");

                if (xmlGeneral.Element("Name") != null)
                    obj.Name = xmlGeneral.Element("Name").Value;
                else obj.Name = "";

                if (xmlGeneral.Element("Description") != null)
                    obj.Description = xmlGeneral.Element("Description").Value.Replace("\\n", Environment.NewLine);
                else obj.Description = "";

                if (xmlGeneral.Element("Note") != null)
                    obj.Note = xmlGeneral.Element("Note").Value.Replace("\\n", Environment.NewLine);
                else obj.Note = "";

                if (xmlGeneral.Element("MinAge") != null)
                    obj.MinAge = Conv.ToShortNull(xmlGeneral.Element("MinAge").Value);

                if (xmlGeneral.Element("MinPlayers") != null)
                    obj.MinPlayers = Conv.ToShortNull(xmlGeneral.Element("MinPlayers").Value);

                if (xmlGeneral.Element("MaxPlayers") != null)
                    obj.MaxPlayers = Conv.ToShortNull(xmlGeneral.Element("MaxPlayers").Value);

                if (xmlGeneral.Element("GameTime") != null)
                    obj.GameTime = Conv.ToShortNull(xmlGeneral.Element("GameTime").Value);

                if (xmlGeneral.Element("GameWorld") != null)
                    obj.GameWorld = xmlGeneral.Element("GameWorld").Value.Replace("\\n", Environment.NewLine);
                else obj.GameWorld = "";

                if (xmlGeneral.Element("Language") != null)
                    obj.Language = xmlGeneral.Element("Language").Value.Replace("\\n", Environment.NewLine);
                else obj.Language = "";

                if (xmlGeneral.Element("Publisher") != null)
                    obj.Publisher = xmlGeneral.Element("Publisher").Value.Replace("\\n", Environment.NewLine);
                else obj.Publisher = "";

                if (xmlGeneral.Element("Author") != null)
                    obj.Author = xmlGeneral.Element("Author").Value.Replace("\\n", Environment.NewLine);
                else obj.Author = "";

                if (xmlGeneral.Element("Year") != null)
                    obj.Year = Conv.ToShortNull(xmlGeneral.Element("Year").Value);

                if (xmlGeneral.Element("Extension") != null)
                    obj.Extension = Conv.ToBoolNull(xmlGeneral.Element("Extension").Value);

                if (xmlGeneral.Element("ExtensionNumber") != null)
                    obj.ExtensionNumber = Conv.ToShortNull(xmlGeneral.Element("ExtensionNumber").Value);

                if (xmlGeneral.Element("Rules") != null)
                    obj.Rules = xmlGeneral.Element("Rules").Value.Replace("\\n", Environment.NewLine);
                else obj.Rules = "";

                if (xmlGeneral.Element("MaterialPath") != null)
                    obj.MaterialPath = xmlGeneral.Element("MaterialPath").Value.Replace("\\n", Environment.NewLine);
                else obj.MaterialPath = "";

                if (xmlGeneral.Element("Family") != null)
                    obj.Family = xmlGeneral.Element("Family").Value.Replace("\\n", Environment.NewLine);
                else obj.Family = "";

                if (xmlGeneral.Element("URL") != null)
                    obj.URL = xmlGeneral.Element("URL").Value;
                else obj.URL = "";



                var xmlClass = item.Element("Classification");

                if (xmlClass.Element("Category") != null)
                    obj.Category = xmlClass.Element("Category").Value;
                else obj.Category = "";

                /*if (xmlClass.Element("Subcategory") != null)
                    obj.Subcategory = xmlClass.Element("Subcategory").Value;
                else obj.Subcategory = "";*/

                if (xmlClass.Element("Keywords") != null)
                    obj.Keywords = xmlClass.Element("Keywords").Value;
                else obj.Keywords = "";

                if (xmlClass.Element("FastTags") != null)
                    obj.FastTags = Conv.ToShortDef(xmlClass.Element("FastTags").Value, 0);
                else obj.FastTags = 0;


                var xmlRating = item.Element("Rating");

                if (xmlRating.Element("Rating") != null)
                    obj.Rating = Conv.ToShortNull(xmlRating.Element("Rating").Value);

                if (xmlRating.Element("MyRating") != null)
                    obj.MyRating = Conv.ToShortNull(xmlRating.Element("MyRating").Value);


                var xmlSystem = item.Element("System");

                if (xmlSystem.Element("ID") != null)
                    obj.ID = Conv.ToGuid(xmlSystem.Element("ID").Value);

                if (xmlSystem.Element("Updated") != null)
                    obj.Updated = Conv.ToDateTimeNull(xmlSystem.Element("Updated").Value);

                if (xmlSystem.Element("Excluded") != null)
                    obj.Excluded = Conv.ToBoolDef(xmlSystem.Element("Excluded").Value, false);
                else obj.Excluded = false;


                if (xmlSystem.Element("Cover") != null)
                {
                    string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files" + Path.DirectorySeparatorChar + xmlSystem.Element("Cover").Value;

                    obj.Cover = Files.LoadBinFile(imgPath);
                }

                if (xmlSystem.Element("Img1") != null)
                {
                    string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files" + Path.DirectorySeparatorChar + xmlSystem.Element("Img1").Value;

                    obj.Img1 = Files.LoadBinFile(imgPath);
                }

                if (xmlSystem.Element("Img2") != null)
                {
                    string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files" + Path.DirectorySeparatorChar + xmlSystem.Element("Img2").Value;

                    obj.Img2 = Files.LoadBinFile(imgPath);
                }

                if (xmlSystem.Element("Img3") != null)
                {
                    string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files" + Path.DirectorySeparatorChar + xmlSystem.Element("Img3").Value;

                    obj.Img3 = Files.LoadBinFile(imgPath);
                }


                objList.Add(obj);
            }

            // ----- Parse copies -----
            IEnumerable<XElement> xmlCopies = xml.Elements().Elements("Copies").Elements("Copy");

            copies = ImportCopiesXML(xmlCopies);

            return objList;
        }


        /// <summary>
        /// Import Games from CSV
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Contact list</returns>
        public static List<Games> ImportGamesCSV(string path)
        {
            List<Games> objList = new List<Games>();

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Check File Head -----
            if (!Str.GetFirstLine(ref text, true).Contains("FialotCatalog:Games"))
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

                Games obj = new Games();
                obj.Name = item[0].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Category = item[1].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Subcategory = item[2].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Keywords = item[3].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Note = item[4].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Description = item[5].Replace("//", ";").Replace("\\n", Environment.NewLine);
                if (item[6] != "")
                    obj.Image = Files.LoadBinFile(imgPath);

                obj.PlayerAge = item[7].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.MinPlayers = Conv.ToShortNull(item[8]);
                obj.MaxPlayers = Conv.ToShortNull(item[9]);
                obj.Duration = Conv.ToShortNull(item[10]);
                obj.DurationPreparation = Conv.ToShortNull(item[11]);
                obj.Things = item[12].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.URL = item[13].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Rules = item[14].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Preparation = item[15].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Environment = item[16].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Files = item[17].Replace("//", ";").Replace("\\n", Environment.NewLine);

                obj.Rating = Conv.ToShortNull(item[18]);
                obj.MyRating = Conv.ToShortNull(item[19]);
                obj.FastTags = Conv.ToShortDef(item[20], 0);
                obj.Updated = Conv.ToDateTimeNull(item[21]);
                obj.Excluded = Conv.ToBoolDef(item[22], true);
                
                obj.ID = Conv.ToGuid(item[23]);
                objList.Add(obj);
            }

            return objList;
        }



        /// <summary>
        /// Import Games from XML
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Recipes list</returns>
        public static List<Games> ImportGamesXML(string path)
        {
            List<Games> objList = new List<Games>();

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Parse XML to Structure -----
            var xml = XDocument.Parse(text);
            var xmlType = xml.Elements().Elements("Info").Elements("Type");

            // ----- Check File Head -----
            if (xmlType.First().Value != "FialotCatalog:Games")
                return null;

            var xmlItems = xml.Elements().Elements("Items").Elements("Game");


            // ----- Parse data -----
            foreach (var item in xmlItems)
            {
                Games obj = new Games();

                var xmlGeneral = item.Element("General");

                if (xmlGeneral.Element("Name") != null)
                    obj.Name = xmlGeneral.Element("Name").Value;
                else obj.Name = "";

                if (xmlGeneral.Element("Description") != null)
                    obj.Description = xmlGeneral.Element("Description").Value.Replace("\\n", Environment.NewLine);
                else obj.Description = "";

                if (xmlGeneral.Element("Note") != null)
                    obj.Note = xmlGeneral.Element("Note").Value.Replace("\\n", Environment.NewLine);
                else obj.Note = "";

                if (xmlGeneral.Element("PlayerAge") != null)
                    obj.PlayerAge = xmlGeneral.Element("PlayerAge").Value.Replace("\\n", Environment.NewLine);
                else obj.PlayerAge = "";

                if (xmlGeneral.Element("MinPlayers") != null)
                    obj.MinPlayers = Conv.ToShortNull(xmlGeneral.Element("MinPlayers").Value);

                if (xmlGeneral.Element("MaxPlayers") != null)
                    obj.MaxPlayers = Conv.ToShortNull(xmlGeneral.Element("MaxPlayers").Value);

                if (xmlGeneral.Element("Duration") != null)
                    obj.Duration = Conv.ToShortNull(xmlGeneral.Element("Duration").Value);

                if (xmlGeneral.Element("DurationPreparation") != null)
                    obj.DurationPreparation = Conv.ToShortNull(xmlGeneral.Element("DurationPreparation").Value);

                if (xmlGeneral.Element("Things") != null)
                    obj.Things = xmlGeneral.Element("Things").Value.Replace("\\n", Environment.NewLine);
                else obj.Things = "";

                if (xmlGeneral.Element("Rules") != null)
                    obj.Rules = xmlGeneral.Element("Rules").Value.Replace("\\n", Environment.NewLine);
                else obj.Rules = "";

                if (xmlGeneral.Element("Preparation") != null)
                    obj.Preparation = xmlGeneral.Element("Preparation").Value.Replace("\\n", Environment.NewLine);
                else obj.Preparation = "";

                if (xmlGeneral.Element("Environment") != null)
                    obj.Environment = xmlGeneral.Element("Environment").Value.Replace("\\n", Environment.NewLine);
                else obj.Environment = "";

                if (xmlGeneral.Element("Files") != null)
                    obj.Files = xmlGeneral.Element("Files").Value.Replace("\\n", Environment.NewLine);
                else obj.Files = "";
                
                if (xmlGeneral.Element("URL") != null)
                    obj.URL = xmlGeneral.Element("URL").Value;
                else obj.URL = "";



                var xmlClass = item.Element("Classification");

                if (xmlClass.Element("Category") != null)
                    obj.Category = xmlClass.Element("Category").Value;
                else obj.Category = "";

                if (xmlClass.Element("Subcategory") != null)
                    obj.Subcategory = xmlClass.Element("Subcategory").Value;
                else obj.Subcategory = "";

                if (xmlClass.Element("Keywords") != null)
                    obj.Keywords = xmlClass.Element("Keywords").Value;
                else obj.Keywords = "";

                if (xmlClass.Element("FastTags") != null)
                    obj.FastTags = Conv.ToShortDef(xmlClass.Element("FastTags").Value, 0);
                else obj.FastTags = 0;


                var xmlRating = item.Element("Rating");

                if (xmlRating.Element("Rating") != null)
                    obj.Rating = Conv.ToShortNull(xmlRating.Element("Rating").Value);

                if (xmlRating.Element("MyRating") != null)
                    obj.MyRating = Conv.ToShortNull(xmlRating.Element("MyRating").Value);


                var xmlSystem = item.Element("System");

                if (xmlSystem.Element("ID") != null)
                    obj.ID = Conv.ToGuid(xmlSystem.Element("ID").Value);

                if (xmlSystem.Element("Updated") != null)
                    obj.Updated = Conv.ToDateTimeNull(xmlSystem.Element("Updated").Value);

                if (xmlSystem.Element("Excluded") != null)
                    obj.Excluded = Conv.ToBoolDef(xmlSystem.Element("Excluded").Value, false);
                else obj.Excluded = false;


                if (xmlSystem.Element("Image") != null)
                {
                    string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files" + Path.DirectorySeparatorChar + xmlSystem.Element("Image").Value;

                    obj.Image = Files.LoadBinFile(imgPath);
                }


                objList.Add(obj);
            }

            return objList;
        }


        /// <summary>
        /// Import Recipes from CSV
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Contact list</returns>
        public static List<Recipes> ImportRecipesCSV(string path)
        {
            List<Recipes> objList = new List<Recipes>();

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Check File Head -----
            if (!Str.GetFirstLine(ref text, true).Contains("FialotCatalog:Recipes"))
                return null;

            // ----- Parse CSV File -----
            CSVfile file = Files.ParseCSV(text);

            // ----- Check table size -----
            if (file.head.Length != 15)
                return null;

            // ----- Parse data -----
            foreach (var item in file.data)
            {
                string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files" + Path.DirectorySeparatorChar + item[6];

                Recipes obj = new Recipes();
                obj.Name = item[0].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Category = item[1].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Subcategory = item[2].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Keywords = item[3].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Note = item[4].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Description = item[5].Replace("//", ";").Replace("\\n", Environment.NewLine);
                if (item[6] != "")
                    obj.Image = Files.LoadBinFile(imgPath);

                obj.Procedure = item[7].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Resources = item[8].Replace("//", ";").Replace("\\n", Environment.NewLine);

                obj.Rating = Conv.ToShortNull(item[9]);
                obj.MyRating = Conv.ToShortNull(item[10]);
                obj.FastTags = Conv.ToShortDef(item[11], 0);
                obj.Updated = Conv.ToDateTimeNull(item[12]);
                obj.Excluded = Conv.ToBoolDef(item[13], true);

                obj.ID = Conv.ToGuid(item[14]);
                objList.Add(obj);
            }

            return objList;
        }


        /// <summary>
        /// Import Recipes from XML
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Recipes list</returns>
        public static List<Recipes> ImportRecipesXML(string path)
        {
            List<Recipes> objList = new List<Recipes>();

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Parse XML to Structure -----
            var xml = XDocument.Parse(text);
            var xmlType = xml.Elements().Elements("Info").Elements("Type");

            // ----- Check File Head -----
            if (xmlType.First().Value != "FialotCatalog:Recipes")
                return null;

            var xmlItems = xml.Elements().Elements("Items").Elements("Recipe");


            // ----- Parse data -----
            foreach (var item in xmlItems)
            {
                Recipes obj = new Recipes();

                var xmlGeneral = item.Element("General");

                if (xmlGeneral.Element("Name") != null)
                    obj.Name = xmlGeneral.Element("Name").Value;
                else obj.Name = "";
                
                if (xmlGeneral.Element("Description") != null)
                    obj.Description = xmlGeneral.Element("Description").Value.Replace("\\n", Environment.NewLine);
                else obj.Description = "";

                if (xmlGeneral.Element("Note") != null)
                    obj.Note = xmlGeneral.Element("Note").Value.Replace("\\n", Environment.NewLine);
                else obj.Note = "";

                if (xmlGeneral.Element("Procedure") != null)
                    obj.Procedure = xmlGeneral.Element("Procedure").Value.Replace("\\n", Environment.NewLine);
                else obj.Procedure = "";

                if (xmlGeneral.Element("Resources") != null)
                    obj.Resources = xmlGeneral.Element("Resources").Value.Replace("\\n", Environment.NewLine);
                else obj.Resources = "";


                if (xmlGeneral.Element("URL") != null)
                    obj.URL = xmlGeneral.Element("URL").Value;
                else obj.URL = "";



                var xmlClass = item.Element("Classification");
                
                if (xmlClass.Element("Category") != null)
                    obj.Category = xmlClass.Element("Category").Value;
                else obj.Category = "";

                if (xmlClass.Element("Subcategory") != null)
                    obj.Subcategory = xmlClass.Element("Subcategory").Value;
                else obj.Subcategory = "";

                if (xmlClass.Element("Keywords") != null)
                    obj.Keywords = xmlClass.Element("Keywords").Value;
                else obj.Keywords = "";

                if (xmlClass.Element("FastTags") != null)
                    obj.FastTags = Conv.ToShortDef(xmlClass.Element("FastTags").Value, 0);
                else obj.FastTags = 0;


                var xmlRating = item.Element("Rating");

                if (xmlRating.Element("Rating") != null)
                    obj.Rating = Conv.ToShortNull(xmlRating.Element("Rating").Value);

                if (xmlRating.Element("MyRating") != null)
                    obj.MyRating = Conv.ToShortNull(xmlRating.Element("MyRating").Value);


                var xmlSystem = item.Element("System");

                if (xmlSystem.Element("ID") != null)
                    obj.ID = Conv.ToGuid(xmlSystem.Element("ID").Value);

                if (xmlSystem.Element("Updated") != null)
                    obj.Updated = Conv.ToDateTimeNull(xmlSystem.Element("Updated").Value);

                if (xmlSystem.Element("Excluded") != null)
                    obj.Excluded = Conv.ToBoolDef(xmlSystem.Element("Excluded").Value, false);
                else obj.Excluded = false;


                if (xmlSystem.Element("Image") != null)
                {
                    string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files" + Path.DirectorySeparatorChar + xmlSystem.Element("Image").Value;

                    obj.Image = Files.LoadBinFile(imgPath);
                }


                objList.Add(obj);
            }

            return objList;
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
            if (file.head.Length != 25)
                return null;

            // ----- Parse data -----
            foreach (var item in file.data)
            {
                string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files" + Path.DirectorySeparatorChar + item[7];

                Objects obj = new Objects();
                obj.Name = item[0].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Category = item[1].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Subcategory = item[2].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Keywords = item[3].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Note = item[4].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Description = item[5].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.URL = item[6].Replace("//", ";");
                if (item[7] != "")
                    obj.Image = Files.LoadBinFile(imgPath);
                obj.Rating = Conv.ToShortNull(item[8]);
                obj.MyRating = Conv.ToShortNull(item[9]);
                obj.FastTags = Conv.ToShortDef(item[10], 0);
                obj.Updated = Conv.ToDateTimeNull(item[11]);
                obj.Active = Conv.ToBoolDef(item[12], true);
                obj.Version = item[13].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Files = item[14].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Folder = item[15].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Type = item[16].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.ObjectNum = item[17].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Language = item[18].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Parent = Conv.ToGuidNull(item[19]);
                obj.Customer = item[20].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.Development = item[21].Replace("//", ";").Replace("\\n", Environment.NewLine);
                obj.IsParent = Conv.ToBoolDef(item[22], true);
                obj.UsedObjects = item[23].Replace("//", ";").Replace("\\n", Environment.NewLine);


                obj.ID = Conv.ToGuid(item[24]);
                objList.Add(obj);
            }

            return objList;
        }


        /// <summary>
        /// Import Objects from XML
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>Contact list</returns>
        public static List<Objects> ImportObjectsXML(string path)
        {
            List<Objects> objList = new List<Objects>();

            // ----- Load file -----
            string text = Files.LoadFile(path);

            // ----- Parse XML to Structure -----
            var xml = XDocument.Parse(text);
            var xmlType = xml.Elements().Elements("Info").Elements("Type");

            // ----- Check File Head -----
            if (xmlType.First().Value != "FialotCatalog:Objects")
                return null;

            var xmlItems = xml.Elements().Elements("Items").Elements("Object");


            // ----- Parse data -----
            foreach (var item in xmlItems)
            {
                Objects obj = new Objects();

                var xmlGeneral = item.Element("General");

                if (xmlGeneral.Element("Name") != null)
                    obj.Name = xmlGeneral.Element("Name").Value;
                else obj.Name = "";

                if (xmlGeneral.Element("ObjectNumber") != null)
                    obj.ObjectNum = xmlGeneral.Element("ObjectNumber").Value;
                else obj.ObjectNum = "";

                if (xmlGeneral.Element("Description") != null)
                    obj.Description = xmlGeneral.Element("Description").Value.Replace("\\n", Environment.NewLine);
                else obj.Description = "";

                if (xmlGeneral.Element("Note") != null)
                    obj.Note = xmlGeneral.Element("Note").Value.Replace("\\n", Environment.NewLine);
                else obj.Note = "";

                if (xmlGeneral.Element("Version") != null)
                    obj.Version = xmlGeneral.Element("Version").Value;
                else obj.Version = "";

                if (xmlGeneral.Element("Files") != null)
                    obj.Files = xmlGeneral.Element("Files").Value;
                else obj.Files = "";

                if (xmlGeneral.Element("Folder") != null)
                    obj.Folder = xmlGeneral.Element("Folder").Value;
                else obj.Folder = "";

                if (xmlGeneral.Element("URL") != null)
                  obj.URL = xmlGeneral.Element("URL").Value;
                else obj.URL = "";

                if (xmlGeneral.Element("Customer") != null)
                    obj.Customer = xmlGeneral.Element("Customer").Value;
                else obj.Customer = "";

                if (xmlGeneral.Element("Development") != null)
                    obj.Development = xmlGeneral.Element("Development").Value;
                else obj.Development = "";

                if (xmlGeneral.Element("Language") != null)
                    obj.Language = xmlGeneral.Element("Language").Value;
                else obj.Language = "";


                var xmlClass = item.Element("Classification");

                if (xmlClass.Element("Type") != null)
                    obj.Type = xmlClass.Element("Type").Value;
                else obj.Type = "";

                if (xmlClass.Element("Category") != null)
                    obj.Category = xmlClass.Element("Category").Value;
                else obj.Category = "";

                if (xmlClass.Element("Subcategory") != null)
                    obj.Subcategory = xmlClass.Element("Subcategory").Value;
                else obj.Subcategory = "";

                if (xmlClass.Element("Keywords") != null)
                    obj.Keywords = xmlClass.Element("Keywords").Value;
                else obj.Keywords = "";

                if (xmlClass.Element("FastTags") != null)
                    obj.FastTags = Conv.ToShortDef(xmlClass.Element("FastTags").Value, 0);
                else obj.FastTags = 0;


                var xmlRating = item.Element("Rating");

                if (xmlRating.Element("Rating") != null)
                    obj.Rating = Conv.ToShortNull(xmlRating.Element("Rating").Value);

                if (xmlRating.Element("MyRating") != null)
                    obj.MyRating = Conv.ToShortNull(xmlRating.Element("MyRating").Value);


                var xmlSystem = item.Element("System");

                if (xmlSystem.Element("ID") != null)
                    obj.ID = Conv.ToGuid(xmlSystem.Element("ID").Value);

                if (xmlSystem.Element("Updated") != null)
                    obj.Updated = Conv.ToDateTimeNull(xmlSystem.Element("Updated").Value);

                if (xmlSystem.Element("Active") != null)
                    obj.Active = Conv.ToBoolDef(xmlSystem.Element("Active").Value, true);
                else obj.Active = true;

                if (xmlSystem.Element("Parent") != null)
                    obj.Parent = Conv.ToGuidNull(xmlSystem.Element("Parent").Value);

                if (xmlSystem.Element("IsParent") != null)
                    obj.IsParent = Conv.ToBoolDef(xmlSystem.Element("IsParent").Value, true);
                else obj.IsParent = true;

                if (xmlSystem.Element("UsedObjects") != null)
                    obj.UsedObjects = xmlSystem.Element("UsedObjects").Value;
                else obj.UsedObjects = "";

                if (xmlSystem.Element("Image") != null)
                {
                    string imgPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(path) + "_files" + Path.DirectorySeparatorChar + xmlSystem.Element("Image").Value;

                    obj.Image = Files.LoadBinFile(imgPath);
                }
                    

                objList.Add(obj);
            }

            return objList;
        }


        #endregion

    }


}
