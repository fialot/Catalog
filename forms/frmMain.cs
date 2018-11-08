using BrightIdeasSoftware;
using myFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;



namespace Katalog
{
    public partial class frmMain : Form
    {
        #region Constructor

        public frmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region ObjectListView

        void UpdateAllItemsOLV()
        {
            UpdateItemsOLV();
            UpdateBooksOLV();
            UpdateBoardOLV();
        }

        
        #endregion

        #region Edit Items

        private void EnableEditItems()
        {
            int count = 0;

            if (TabBars.SelectedTab == tabContacts)
                count = olvContacts.SelectedObjects.Count;
            else if (TabBars.SelectedTab == tabLending)
                count = olvLending.SelectedObjects.Count;
            else if (TabBars.SelectedTab == tabBorrowing)
                count = olvBorrowing.SelectedObjects.Count;
            else if (TabBars.SelectedTab == tabItems)
                count = olvItem.SelectedObjects.Count;
            else if (TabBars.SelectedTab == tabBooks)
                count = olvBooks.SelectedObjects.Count;
            else if (TabBars.SelectedTab == tabBoardGames)
                count = olvBoard.SelectedObjects.Count;
            else if (TabBars.SelectedTab == tabGames)
                count = olvGames.SelectedObjects.Count;
            else if (TabBars.SelectedTab == tabRecipes)
                count = olvRecipes.SelectedObjects.Count;

            if (count == 1)
            {
                btnEditItem.Enabled = true;
                mnuEditItem.Enabled = true;
            }
            else
            {
                btnEditItem.Enabled = false;
                mnuEditItem.Enabled = false;
            }

            if (count >= 0)
            {
                btnDeleteItem.Enabled = true;
                mnuDelItem.Enabled = true;
            }
            else
            {
                btnDeleteItem.Enabled = false;
                mnuDelItem.Enabled = false;
            }
        }

        /// <summary>
        /// Show Add New Item Form
        /// </summary>
        private void NewItem()
        {
            // ----- Contact -----
            if (TabBars.SelectedTab == tabContacts) NewItemContacts();
            // ----- Lending -----
            else if (TabBars.SelectedTab == tabLending) NewItemLending();
            // ----- Borrowing -----
            else if (TabBars.SelectedTab == tabBorrowing) NewItemBorrowing();
            // ----- Item -----
            else if (TabBars.SelectedTab == tabItems) NewItemItems();
            // ----- Book -----
            else if (TabBars.SelectedTab == tabBooks) NewItemBooks();
            // ----- Boardgames -----
            else if (TabBars.SelectedTab == tabBoardGames) NewItemBoard();
            // ----- Games -----
            else if (TabBars.SelectedTab == tabGames) NewItemGames();
            // ----- Recipes -----
            else if (TabBars.SelectedTab == tabRecipes) NewItemRecipes();
        }

        /// <summary>
        /// Show Edit Item Form
        /// </summary>
        private void EditItem()
        {
            // ----- Contact -----
            if (TabBars.SelectedTab == tabContacts) EditItemContacts();
            // ----- Lending -----
            else if (TabBars.SelectedTab == tabLending) EditItemLending();
            // ----- Borrowing -----
            else if (TabBars.SelectedTab == tabBorrowing) EditItemBorrowing();
            // ----- Item -----
            else if (TabBars.SelectedTab == tabItems) EditItemItems();
            // ----- Book -----
            else if (TabBars.SelectedTab == tabBooks) EditItemBooks();
            // ----- Boardgames -----
            else if (TabBars.SelectedTab == tabBoardGames) EditItemBoard();
            // ----- Gamse -----
            else if (TabBars.SelectedTab == tabGames) EditItemGames();
            // ----- Recipes -----
            else if (TabBars.SelectedTab == tabRecipes) EditItemRecipes();
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItem()
        {
            databaseEntities db = new databaseEntities();

            // ----- Contact -----
            if (TabBars.SelectedTab == tabContacts) DeleteItemContacts();
            // ----- Lending -----
            else if (TabBars.SelectedTab == tabLending) DeleteItemLending();
            // ----- Borrowing -----
            else if (TabBars.SelectedTab == tabBorrowing) DeleteItemBorrowing();
            // ----- Item -----
            else if (TabBars.SelectedTab == tabItems) DeleteItemItems();
            // ----- Book -----
            else if (TabBars.SelectedTab == tabBooks) DeleteItemBooks();
            // ----- Boardgames -----
            else if (TabBars.SelectedTab == tabBoardGames) DeleteItemBoard();
            // ----- Gamse -----
            else if (TabBars.SelectedTab == tabGames) DeleteItemGames();
            // ----- Recipes -----
            else if (TabBars.SelectedTab == tabRecipes) DeleteItemRecipes();
        }

        /// <summary>
        /// Set fast tag to Item
        /// </summary>
        private void SetTagItem(short tag)
        {
            databaseEntities db = new databaseEntities();

            // ----- Contact -----
            if (TabBars.SelectedTab == tabContacts) SetTagItemContacts(tag);
            // ----- Lending -----
            else if (TabBars.SelectedTab == tabLending) { }
            // ----- Borrowing -----
            else if (TabBars.SelectedTab == tabBorrowing) { }
            // ----- Item -----
            else if (TabBars.SelectedTab == tabItems) SetTagItemItems(tag);
            // ----- Book -----
            else if (TabBars.SelectedTab == tabBooks) SetTagItemBooks(tag);
            // ----- Boardgames -----
            else if (TabBars.SelectedTab == tabBoardGames) SetTagItemBoard(tag);
            // ----- Gamse -----
            else if (TabBars.SelectedTab == tabGames) SetTagItemGames(tag);
            // ----- Recipes -----
            else if (TabBars.SelectedTab == tabRecipes) SetTagItemRecipes(tag);
        }

        /// <summary>
        /// Button New Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewItem_Click(object sender, EventArgs e)
        {
            NewItem();
        }

        /// <summary>
        /// Button Edit Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditItem_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        /// <summary>
        /// Button Delete Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void EditPersonalLending(bool borrowing = false)
        {
            // ----- Contact -----
            if (TabBars.SelectedTab == tabContacts)
            {
                if (olvContacts.SelectedIndex >= 0)                 // If selected Item
                {
                    if(borrowing)
                    {
                        frmEditPersonBorrowing form = new frmEditPersonBorrowing();   // Show Edit form
                        var res = form.ShowDialog(((Contacts)olvContacts.SelectedObject).ID);
                        UpdateBorrowingOLV();
                    }
                    else
                    {
                        frmEditPersonLending form = new frmEditPersonLending();   // Show Edit form
                        var res = form.ShowDialog(((Contacts)olvContacts.SelectedObject).ID);
                        UpdateLendingOLV();
                        UpdateAllItemsOLV();
                    }
                    
                }
            }
            // ----- Lending -----
            else if (TabBars.SelectedTab == tabLending)
            {
                if (olvLending.SelectedIndex >= 0)                 // If selected Item
                {
                    frmEditPersonLending form = new frmEditPersonLending();   // Show Edit form
                    var res = form.ShowDialog(((Lending)olvLending.SelectedObject).PersonID ?? Guid.Empty);
                    UpdateLendingOLV();
                    UpdateAllItemsOLV();
                }
            }
            // ----- Borrowing -----
            else if (TabBars.SelectedTab == tabBorrowing)
            {
                if (olvBorrowing.SelectedIndex >= 0)                 // If selected Item
                {
                    frmEditPersonBorrowing form = new frmEditPersonBorrowing();   // Show Edit form
                    var res = form.ShowDialog(((Borrowing)olvBorrowing.SelectedObject).PersonID ?? Guid.Empty);
                    UpdateBorrowingOLV();
                }
            }
        }

        #endregion

        #region Filter

        TextMatchFilter FastFilter;                         // Fast filter
        TextMatchFilter FastFilterTags;                     // Fast Tags filter
        TextMatchFilter StandardFilter;                     // Standard filter

        List<string> FastFilterList = new List<string>();   // Fast Filter list
        List<string> FastTagFilterList = new List<string>();   // Fast Tag Filter list

        /// <summary>
        /// Update Filter Combobox
        /// </summary>
        void UpdateFilterComboBox()
        {
            cbFilterCol.Items.Clear();
            cbFastFilterCol.Items.Clear();
            if (TabBars.SelectedTab == tabContacts) UpdateCBFilterContacts();
            else if (TabBars.SelectedTab == tabLending) UpdateCBFilterLending();
            else if (TabBars.SelectedTab == tabBorrowing) UpdateCBFilterBorrowing();
            else if (TabBars.SelectedTab == tabItems) UpdateCBFilterItems();
            else if (TabBars.SelectedTab == tabBooks) UpdateCBFilterBooks();
            else if (TabBars.SelectedTab == tabBoardGames) UpdateCBFilterBoard();
            else if (TabBars.SelectedTab == tabGames) UpdateCBFilterGames();
            else if (TabBars.SelectedTab == tabRecipes) UpdateCBFilterRecipes();
        }

        /// <summary>
        /// Set Fast Filter Change
        /// </summary>
        /// <param name="set">Set/remove Filter</param>
        /// <param name="letter">Filter letter</param>
        void SetFastFilter(bool set, string letter)
        {
            if (set)
            {
                // ----- Add Fast Filter -----
                if (letter == "0-9")
                {
                    FastFilterList.Add("0");
                    FastFilterList.Add("1");
                    FastFilterList.Add("2");
                    FastFilterList.Add("3");
                    FastFilterList.Add("4");
                    FastFilterList.Add("5");
                    FastFilterList.Add("6");
                    FastFilterList.Add("7");
                    FastFilterList.Add("8");
                    FastFilterList.Add("9");
                }
                else
                {
                    if (letter == "A")
                        FastFilterList.Add("Á");
                    else if (letter == "E")
                    {
                        FastFilterList.Add("É");
                        FastFilterList.Add("Ě");
                    }
                    else if (letter == "I")
                        FastFilterList.Add("Í");
                    else if (letter == "O")
                        FastFilterList.Add("Ó");
                    else if (letter == "U")
                    {
                        FastFilterList.Add("Ú");
                        FastFilterList.Add("Ů");
                    }
                    else if (letter == "Y")
                        FastFilterList.Add("Ý");
                    FastFilterList.Add(letter);
                }
            }
                
            else
            {
                // ----- Remove Fast Filter -----
                if (letter == "0-9")
                {
                    FastFilterList.Remove("0");
                    FastFilterList.Remove("1");
                    FastFilterList.Remove("2");
                    FastFilterList.Remove("3");
                    FastFilterList.Remove("4");
                    FastFilterList.Remove("5");
                    FastFilterList.Remove("6");
                    FastFilterList.Remove("7");
                    FastFilterList.Remove("8");
                    FastFilterList.Remove("9");
                }
                else
                {
                    if (letter == "A")
                        FastFilterList.Remove("Á");
                    else if (letter == "E")
                    {
                        FastFilterList.Remove("É");
                        FastFilterList.Remove("Ě");
                    }
                    else if (letter == "I")
                        FastFilterList.Remove("Í");
                    else if (letter == "O")
                        FastFilterList.Remove("Ó");
                    else if (letter == "U")
                    {
                        FastFilterList.Remove("Ú");
                        FastFilterList.Remove("Ů");
                    }
                    else if (letter == "Y")
                        FastFilterList.Remove("Ý");
                    FastFilterList.Remove(letter);
                }
                
            }

            // ----- Use Filter -----
            UseFilters();
        }

        /// <summary>
        /// Set Fast Tag Filter Change
        /// </summary>
        /// <param name="set">Set/remove Filter</param>
        /// <param name="letter">Filter letter</param>
        void SetFastTagFilter(bool set, string letter)
        {
            if (set)
            {
                // ----- Add Fast Filter -----
                FastTagFilterList.Add(letter);
            }
            else
            {
                // ----- Remove Fast Filter -----
                FastTagFilterList.Remove(letter);
            }

            // ----- Use Filter -----
            UseFilters();
        }

        /// <summary>
        /// Use filters
        /// </summary>
        private void UseFilters()
        {
            UseFastFilter();
            UseFastTagFilter();
            UseStandardFilter();

            if (TabBars.SelectedTab == tabContacts) UseFiltersContacts();
            else if (TabBars.SelectedTab == tabLending) UseFiltersLending();
            else if (TabBars.SelectedTab == tabBorrowing) UseFiltersBorrowing();
            else if (TabBars.SelectedTab == tabItems) UseFiltersItems();
            else if (TabBars.SelectedTab == tabBooks) UseFiltersBooks();
            else if (TabBars.SelectedTab == tabBoardGames) UseFiltersBoard();
            else if (TabBars.SelectedTab == tabGames) UseFiltersGames();
            else if (TabBars.SelectedTab == tabRecipes) UseFiltersRecipes();
        }

        /// <summary>
        /// Use Fast Filter
        /// </summary>
        private void UseFastFilter()
        {
            if (TabBars.SelectedTab == tabContacts) UseFastFilterContacts();
            else if (TabBars.SelectedTab == tabLending) UseFastFilterLending();
            else if (TabBars.SelectedTab == tabBorrowing) UseFastFilterBorrowing();
            else if (TabBars.SelectedTab == tabItems) UseFastFilterItems();
            else if (TabBars.SelectedTab == tabBooks) UseFastFilterBooks();
            else if (TabBars.SelectedTab == tabBoardGames) UseFastFilterBoard();
            else if (TabBars.SelectedTab == tabGames) UseFastFilterGames();
            else if (TabBars.SelectedTab == tabRecipes) UseFastFilterRecipes();
        }

        /// <summary>
        /// Use Fast Tag Filter
        /// </summary>
        private void UseFastTagFilter()
        {
            if (FastFilterTags != null) FastFilterTags.Columns = null;
            if (TabBars.SelectedTab == tabContacts) UseFastTagFilterContacts();
            else if (TabBars.SelectedTab == tabLending) UseFastTagFilterLending();
            else if (TabBars.SelectedTab == tabBorrowing) UseFastTagFilterBorrowing();
            else if (TabBars.SelectedTab == tabItems) UseFastTagFilterItems();
            else if (TabBars.SelectedTab == tabBooks) UseFastTagFilterBooks();
            else if (TabBars.SelectedTab == tabBoardGames) UseFastTagFilterBoard();
            else if (TabBars.SelectedTab == tabGames) UseFastTagFilterGames();
            else if (TabBars.SelectedTab == tabRecipes) UseFastTagFilterRecipes();
        }

        /// <summary>
        /// Use Standard Filter
        /// </summary>
        private void UseStandardFilter()
        {
            if (TabBars.SelectedTab == tabContacts) UseStandardFilterContacts();
            else if (TabBars.SelectedTab == tabLending) UseStandardFilterLending();
            else if (TabBars.SelectedTab == tabBorrowing) UseStandardFilterBorrowing();
            else if (TabBars.SelectedTab == tabItems) UseStandardFilterItems();
            else if (TabBars.SelectedTab == tabBooks) UseStandardFilterBooks();
            else if (TabBars.SelectedTab == tabBoardGames) UseStandardFilterBoard();
            else if (TabBars.SelectedTab == tabGames) UseStandardFilterGames();
            else if (TabBars.SelectedTab == tabRecipes) UseStandardFilterRecipes();
        }

        /// <summary>
        /// Filter Letter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterA_Click(object sender, EventArgs e)
        {
            string letter = ((ToolStripButton)sender).Text;
            bool set = ((ToolStripButton)sender).Checked;
            SetFastFilter(set, letter);
        }

        /// <summary>
        /// Filter Fast Tags
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterPin1_Click(object sender, EventArgs e)
        {
            string letter = Conv.ToNumber(((ToolStripButton)sender).Name).ToString();
            bool set = ((ToolStripButton)sender).Checked;
            SetFastTagFilter(set, letter);
        }

        /// <summary>
        /// Fast filter Combo box changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbFastFilterCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            UseFilters();
        }

        /// <summary>
        /// Standard filter Text box changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            UseFilters();
        }

        /// <summary>
        /// Standard filter Combo box changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbFilterCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            UseFilters();
        }

        #endregion

        #region Export


        private void ExportGoogleContact()
        {
            databaseEntities db = new databaseEntities();

            if (TabBars.SelectedTab == tabContacts)
            {
                if (olvContacts.SelectedIndex >= 0)                 // If selected Item
                {
                    if (global.ExportContactGoogle((Contacts)olvContacts.SelectedObject))
                    {
                        Dialogs.ShowInfo(Lng.Get("SuccesfullyExport", "Export was succesfully done") + ".", "");
                    }
                    else
                    {
                        Dialogs.ShowErr(Lng.Get("ErrorExport", "Export Error") + "!", "");
                    }
                }
            }
        }


        private void mnuExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV file|*.csv";
            dialog.FileName = "data.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (TabBars.SelectedTab == tabContacts)
                {
                    List<Contacts> con = new List<Contacts>();

                    foreach (var item in olvContacts.FilteredObjects)
                    {
                        con.Add((Contacts)item);
                    }
                    global.ExportContactsCSV(dialog.FileName, con);
                }
                else if (TabBars.SelectedTab == tabLending)
                {
                    List<Lending> itm = new List<Lending>();

                    foreach (var item in olvLending.FilteredObjects)
                    {
                        itm.Add((Lending)item);
                    }
                    global.ExportLendedCSV(dialog.FileName, itm);
                }
                else if (TabBars.SelectedTab == tabBorrowing)
                {
                    List<Borrowing> itm = new List<Borrowing>();

                    foreach (var item in olvBorrowing.FilteredObjects)
                    {
                        itm.Add((Borrowing)item);
                    }
                    global.ExportBorrowingCSV(dialog.FileName, itm);
                }
                else if (TabBars.SelectedTab == tabItems)
                {
                    List<Items> itm = new List<Items>();

                    foreach (var item in olvItem.FilteredObjects)
                    {
                        itm.Add((Items)item);
                    }
                    global.ExportItemsCSV(dialog.FileName, itm);
                }
                else if (TabBars.SelectedTab == tabBooks)
                {
                    List<Books> itm = new List<Books>();

                    foreach (var item in olvBooks.FilteredObjects)
                    {
                        itm.Add((Books)item);
                    }
                    global.ExportBooksCSV(dialog.FileName, itm);
                }
                else if (TabBars.SelectedTab == tabBoardGames)
                {
                    List<Boardgames> itm = new List<Boardgames>();

                    foreach (var item in olvBoard.FilteredObjects)
                    {
                        itm.Add((Boardgames)item);
                    }
                    global.ExportBoardCSV(dialog.FileName, itm);
                }
            }
        }

        #endregion

        #region Import


        private void ImportGoogleContact()
        {
            databaseEntities db = new databaseEntities();

            if (TabBars.SelectedTab == tabContacts)
            {
                if (olvContacts.SelectedIndex >= 0)                 // If selected Item
                {
                    Contacts item = global.ImportContactGoogle(((Contacts)olvContacts.SelectedObject).GoogleID);
                    if (item != null)
                    {
                        var contacts = db.Contacts.Where(w => w.GoogleID == item.GoogleID).ToList();
                        if (contacts.Count > 0)
                        {
                            Contacts contact = contacts[0];
                            FillContact(ref contact, item);
                        }
                        else
                        {
                            item.ID = Guid.NewGuid();
                            db.Contacts.Add(item);
                        }
                        db.SaveChanges();
                        UpdateConOLV();                         // Update Contact OLV
                        Dialogs.ShowInfo(Lng.Get("SuccesfullyImport", "Import was succesfully done") + ".", "");

                    }
                }
            }
        }

        private void ImportGoogleContacts()
        {
            databaseEntities db = new databaseEntities();

            if (TabBars.SelectedTab == tabContacts)
            {
                List<Contacts> con = global.ImportContactsGoogle();
                if (con == null)
                {
                    Dialogs.ShowErr(Lng.Get("GoogleImportError", "Google import Error") + ".", Lng.Get("Error"));
                    return;
                }

                int added = 0;
                int updated = 0;
                
                foreach (var item in con)
                {
                    var contacts = db.Contacts.Where(w => w.GoogleID == item.GoogleID).ToList();
                    if (contacts.Count > 0)
                    {
                        updated++;
                        Contacts contact = contacts[0];
                        FillContact(ref contact, item);
                    }
                    else
                    {
                        added++;
                        item.ID = Guid.NewGuid();
                        db.Contacts.Add(item);
                        db.SaveChanges();
                    }
                }
                db.SaveChanges();
                UpdateConOLV();
                Dialogs.ShowInfo(Lng.Get("SuccesfullyImport", "Import was succesfully done") + ". (" + Lng.Get("Added") + " " + added.ToString() + ", " + Lng.Get("updated") + " " + updated.ToString() + " " + Lng.Get("contacts") + ")", Lng.Get("Import"));
            }
        }
             
        private void FillContact(ref Contacts contact, Contacts newItem)
        {
            // ----- Avatar -----
            contact.Avatar = newItem.Avatar;

            // ----- Name -----
            contact.Name = newItem.Name;
            contact.Surname = newItem.Surname;
            contact.Nick = newItem.Nick;
            contact.Sex = newItem.Sex;

            contact.Birth = newItem.Birth;

            // ----- Contacts -----
            //for (int i = 0; i < )
            contact.Phone = newItem.Phone;

            contact.Email = newItem.Email;
            contact.WWW = newItem.WWW;
            contact.IM = newItem.IM;

            // ----- Company -----
            contact.Company = newItem.Company;
            contact.Position = newItem.Position;

            // ----- Address -----
            contact.Street = newItem.Street;
            contact.City = newItem.City;
            contact.Region = newItem.Region;
            contact.Country = newItem.Country;
            contact.PostCode = newItem.PostCode;

            contact.Note = newItem.Note;
            
            contact.Tags = newItem.Tags;
            contact.FastTags = newItem.FastTags;
            

            contact.PersonCode = newItem.PersonCode;
            contact.Updated = DateTime.Now;
            contact.Active = newItem.Active;
            contact.GoogleID = newItem.GoogleID;
            


            // ----- Unused now -----
            /*contact.Partner = "";
            contact.Childs = "";
            contact.Parrents = "";*/
        }
        
        private void FillLending(ref Lending itm, Lending newItem)
        {
            itm.CopyType = newItem.CopyType;
            itm.CopyType = newItem.CopyType;
            itm.PersonID = newItem.PersonID;
            itm.From = newItem.From;
            itm.To = newItem.To;
            itm.Status = newItem.Status;
            itm.Note = newItem.Note;
            itm.FastTags = newItem.FastTags;
            itm.Updated = newItem.Updated;
        }

        private void FillBorrowing(ref Borrowing itm, Borrowing newItem)
        {
            itm.Item = newItem.Item;
            itm.ItemInvNum = newItem.ItemInvNum;
            itm.PersonID = newItem.PersonID;
            itm.From = newItem.From;
            itm.To = newItem.To;
            itm.Status = newItem.Status;
            itm.Note = newItem.Note;
            itm.FastTags = newItem.FastTags;
            itm.Updated = newItem.Updated;
        }
        
        private void FillCopy(ref Copies itm, Copies newItem)
        {
            
            itm.ItemID = newItem.ItemID;
            itm.ItemType = newItem.ItemType;
            itm.ItemNum = newItem.ItemNum;
            itm.InventoryNumber = newItem.InventoryNumber;
            itm.Barcode = newItem.Barcode;
            itm.Location = newItem.Location;

            itm.AcquisitionDate = newItem.AcquisitionDate;
            itm.Price = newItem.Price;
            itm.Condition = newItem.Condition;
            itm.Excluded = newItem.Excluded;
            itm.Note = newItem.Note;

            itm.Status = newItem.Status;
        }

        private void FillItem(ref Items itm, Items newItem)
        {
            // ----- Avatar -----
            itm.Image = newItem.Image;

            itm.Name = newItem.Name;
            itm.Category = newItem.Category;
            itm.Subcategory = newItem.Subcategory;
            itm.Subcategory2 = newItem.Subcategory2;

            itm.Keywords = newItem.Keywords;
            itm.Note = newItem.Note;

            itm.Manufacturer = newItem.Manufacturer;

            itm.Excluded = newItem.Excluded;
            itm.FastTags = newItem.FastTags;
        }
        
        private void FillBook(ref Books itm, Books newItem)
        {
            itm.Title = itm.Title;
            itm.AuthorName = itm.AuthorName;
            itm.AuthorSurname = itm.AuthorSurname;
            itm.ISBN = itm.ISBN;
            itm.Illustrator = itm.Illustrator;
            itm.Translator = itm.Translator;
            itm.Language = itm.Language;
            itm.Publisher = itm.Publisher;
            itm.Edition = itm.Edition;
            itm.Year = itm.Year;
            itm.Pages = itm.Pages;
            itm.MainCharacter = itm.MainCharacter;
            itm.URL = itm.URL;
            itm.Note = itm.Note;
            itm.Note1 = itm.Note1;
            itm.Note2 = itm.Note2;
            itm.Content = itm.Content;
            itm.OrigName = itm.OrigName;
            itm.OrigLanguage = itm.OrigLanguage;
            itm.OrigYear = itm.OrigYear;
            itm.Genre = itm.Genre;
            itm.SubGenre = itm.SubGenre;
            itm.Series = itm.Series;
            itm.SeriesNum = itm.SeriesNum;
            itm.Keywords = itm.Keywords;
            itm.Rating = itm.Rating;
            itm.MyRating = itm.MyRating;
            itm.Readed = itm.Readed;
            itm.Type = itm.Type;
            itm.Bookbinding = itm.Bookbinding;
            itm.EbookPath = itm.EbookPath;
            itm.EbookType = itm.EbookType;
            itm.Publication = itm.Publication;
            itm.Excluded = itm.Excluded;
            itm.Cover = itm.Cover;
            itm.Updated = itm.Updated;
            itm.FastTags = itm.FastTags;

        }

        private void FillBoard(ref Boardgames itm, Boardgames newItem)
        {
            itm.Cover = newItem.Cover;
            itm.Img1 = newItem.Img1;
            itm.Img2 = newItem.Img2;
            itm.Img3 = newItem.Img3;
            
            itm.Name = newItem.Name;
            itm.Category = newItem.Category;
            itm.MinPlayers = newItem.MinPlayers;
            itm.MaxPlayers = newItem.MaxPlayers;
            itm.MinAge = newItem.MinAge;
            itm.GameTime = newItem.GameTime;
            itm.GameWorld = newItem.GameWorld;
            itm.Language = newItem.Language;
            itm.Publisher = newItem.Publisher;
            itm.Author = newItem.Author;
            itm.Year = newItem.Year;
            itm.Description = newItem.Description;
            itm.Keywords = newItem.Keywords;
            itm.Note = newItem.Note;
            itm.Family = newItem.Family;
            itm.Extension = newItem.Extension;
            itm.ExtensionNumber = newItem.ExtensionNumber;
            itm.Rules = newItem.Rules;
            itm.MaterialPath = newItem.MaterialPath;
            itm.Rating = newItem.Rating;
            itm.MyRating = newItem.MyRating;
            itm.URL = newItem.URL;

            itm.Excluded = newItem.Excluded;
            itm.FastTags = newItem.FastTags;
            itm.Updated = newItem.Updated;
        }
        
        private void mnuImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV file|*.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                databaseEntities db = new databaseEntities();

                if (TabBars.SelectedTab == tabContacts)
                {
                    List<Contacts> con = global.ImportContactsCSV(dialog.FileName);
                    if (con == null)
                    {
                        Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                        return;
                    }

                    foreach (var item in con)
                    {
                        

                        Contacts contact;
                        // ----- ID -----
                        if (item.ID != Guid.Empty)
                        {
                            contact = db.Contacts.Find(item.ID);
                            if (contact != null)
                                FillContact(ref contact, item);
                            else
                            {
                                db.Contacts.Add(item);
                            }
                            
                        }
                        else
                        {
                            item.ID = Guid.NewGuid();
                            db.Contacts.Add(item);
                        }
                    }
                    db.SaveChanges();
                    UpdateConOLV();
                    Dialogs.ShowInfo(Lng.Get("SuccesfullyImport","Import was succesfully done") + ".", Lng.Get("Import"));
                }
                else if (TabBars.SelectedTab == tabLending)
                {
                    List<Lending> con = global.ImportLendedCSV(dialog.FileName);
                    if (con == null)
                    {
                        Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                        return;
                    }

                    foreach (var item in con)
                    {
                        Lending itm;
                        // ----- ID -----
                        if (item.ID != Guid.Empty)
                        {
                            itm = db.Lending.Find(item.ID);
                            if (itm != null)
                                FillLending(ref itm, item);
                            else
                            {
                                db.Lending.Add(item);
                            }

                        }
                        else
                        {
                            item.ID = Guid.NewGuid();
                            db.Lending.Add(item);
                        }
                    }

                    db.SaveChanges();
                    UpdateLendingOLV();
                    Dialogs.ShowInfo(Lng.Get("SuccesfullyImport", "Import was succesfully done") + ".", Lng.Get("Import"));
                }
                else if (TabBars.SelectedTab == tabBorrowing)
                {
                    List<Borrowing> con = global.ImportBorowingCSV(dialog.FileName);
                    if (con == null)
                    {
                        Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                        return;
                    }

                    foreach (var item in con)
                    {
                        Borrowing itm;
                        // ----- ID -----
                        if (item.ID != Guid.Empty)
                        {
                            itm = db.Borrowing.Find(item.ID);
                            if (itm != null)
                                FillBorrowing(ref itm, item);
                            else
                            {
                                db.Borrowing.Add(item);
                            }

                        }
                        else
                        {
                            item.ID = Guid.NewGuid();
                            db.Borrowing.Add(item);
                        }
                    }
                    db.SaveChanges();
                    UpdateBorrowingOLV();
                    Dialogs.ShowInfo(Lng.Get("SuccesfullyImport", "Import was succesfully done") + ".", Lng.Get("Import"));
                }
                else if (TabBars.SelectedTab == tabItems)
                {
                    List<Items> con = global.ImportItemsCSV(dialog.FileName, out List<Copies> copies);
                    if (con == null)
                    {
                        Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                        return;
                    }

                    foreach (var item in con)
                    {
                        Items itm;
                        // ----- ID -----
                        if (item.ID != Guid.Empty)
                        {
                            itm = db.Items.Find(item.ID);
                            if (itm != null)
                                FillItem(ref itm, item);
                            else
                            {
                                db.Items.Add(item);
                            }
                        }
                        else
                        {
                            item.ID = Guid.NewGuid();
                            db.Items.Add(item);
                        }
                    }

                    foreach (var item in copies)
                    {
                        Copies itm;
                        // ----- ID -----
                        if (item.ID != Guid.Empty)
                        {
                            itm = db.Copies.Find(item.ID);
                            if (itm != null)
                                FillCopy(ref itm, item);
                            else
                            {
                                db.Copies.Add(item);
                            }
                        }
                        else
                        {
                            item.ID = Guid.NewGuid();
                            db.Copies.Add(item);
                        }
                    }

                    db.SaveChanges();
                    UpdateItemsOLV();
                    Dialogs.ShowInfo(Lng.Get("SuccesfullyImport", "Import was succesfully done") + ".", Lng.Get("Import"));
                }
                else if (TabBars.SelectedTab == tabBooks)
                {
                    List<Books> con = global.ImportBooksCSV(dialog.FileName, out List<Copies> copies);
                    if (con == null)
                    {
                        Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                        return;
                    }

                    foreach (var item in con)
                    {


                        Books itm;
                        // ----- ID -----
                        if (item.ID != Guid.Empty)
                        {
                            itm = db.Books.Find(item.ID);
                            if (itm != null)
                                FillBook(ref itm, item);
                            else
                            {
                                db.Books.Add(item);
                            }

                        }
                        else
                        {
                            item.ID = Guid.NewGuid();
                            db.Books.Add(item);
                        }
                    }

                    foreach (var item in copies)
                    {
                        Copies itm;
                        // ----- ID -----
                        if (item.ID != Guid.Empty)
                        {
                            itm = db.Copies.Find(item.ID);
                            if (itm != null)
                                FillCopy(ref itm, item);
                            else
                            {
                                db.Copies.Add(item);
                            }
                        }
                        else
                        {
                            item.ID = Guid.NewGuid();
                            db.Copies.Add(item);
                        }
                    }

                    db.SaveChanges();
                    UpdateBooksOLV();
                    Dialogs.ShowInfo(Lng.Get("SuccesfullyImport", "Import was succesfully done") + ".", Lng.Get("Import"));
                }
                else if (TabBars.SelectedTab == tabBoardGames)
                {
                    List<Boardgames> con = global.ImportBoardgamesCSV(dialog.FileName, out List<Copies> copies);
                    if (con == null)
                    {
                        Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                        return;
                    }

                    foreach (var item in con)
                    {


                        Boardgames itm;
                        // ----- ID -----
                        if (item.ID != Guid.Empty)
                        {
                            itm = db.Boardgames.Find(item.ID);
                            if (itm != null)
                                FillBoard(ref itm, item);
                            else
                            {
                                db.Boardgames.Add(item);
                            }

                        }
                        else
                        {
                            item.ID = Guid.NewGuid();
                            db.Boardgames.Add(item);
                        }
                    }

                    foreach (var item in copies)
                    {
                        Copies itm;
                        // ----- ID -----
                        if (item.ID != Guid.Empty)
                        {
                            itm = db.Copies.Find(item.ID);
                            if (itm != null)
                                FillCopy(ref itm, item);
                            else
                            {
                                db.Copies.Add(item);
                            }
                        }
                        else
                        {
                            item.ID = Guid.NewGuid();
                            db.Copies.Add(item);
                        }
                    }

                    db.SaveChanges();
                    UpdateBoardOLV();
                    Dialogs.ShowInfo(Lng.Get("SuccesfullyImport", "Import was succesfully done") + ".", Lng.Get("Import"));
                }
            }


        }

        #endregion

        private void PrepareForm()
        {
            cbLendingShow.Items.Add(Lng.Get("All"));
            cbLendingShow.Items.Add(Lng.Get("Expired"));
            cbLendingShow.Items.Add(Lng.Get("Borrowed"));
            cbLendingShow.Items.Add(Lng.Get("Reserved"));
            cbLendingShow.SelectedIndex = 0;

            cbBorrowingShow.Items.Add(Lng.Get("All"));
            cbBorrowingShow.Items.Add(Lng.Get("Expired"));
            cbBorrowingShow.Items.Add(Lng.Get("Borrowed"));
            cbBorrowingShow.Items.Add(Lng.Get("Reserved"));
            cbBorrowingShow.SelectedIndex = 0;
        }


        private void mnuAbout_Click(object sender, EventArgs e)
        {
            frmAbout formAbout = new frmAbout();
            formAbout.ShowDialog();
        }


        private long GetMaxNum(List<string> list)
        {
            long num = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null)
                {
                    string[] separate = list[i].Trim().Split(new string[] { ";" }, StringSplitOptions.None);
                    for (int j = 0; j < separate.Length; j++)
                    {
                        long value = Conv.ToNumber(separate[j]);
                        if (num < value) num = value;
                    }
                }
                
            }
            return num;
        }

        private void CheckMaxInvNums()
        {
            databaseEntities db = new databaseEntities();

            var list = db.Contacts.Select(u => u.PersonCode).ToList();
            MaxInvNumbers.Contact = GetMaxNum(list);
            list = db.Copies.Where(x => x.ItemType.Trim() == "item").Select(u => u.InventoryNumber).ToList();
            MaxInvNumbers.Item = GetMaxNum(list);
            list = db.Copies.Where(x => x.ItemType.Trim() == "book").Select(u => u.InventoryNumber).ToList();
            MaxInvNumbers.Book = GetMaxNum(list);
            list = db.Copies.Where(x => x.ItemType.Trim() == "boardgame").Select(u => u.InventoryNumber).ToList();
            MaxInvNumbers.Boardgame = GetMaxNum(list);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            TabBars.TabPages.Remove(tabAudio);
            TabBars.TabPages.Remove(tabVideo);
            TabBars.TabPages.Remove(tabFoto);

            PrepareForm();

            UpdateConOLV();
            UpdateLendingOLV();
            UpdateBorrowingOLV();
            UpdateItemsOLV();
            UpdateBooksOLV();
            UpdateBoardOLV();
            UpdateGameOLV();
            UpdateRecOLV();
            EnableEditItems();
            UpdateFilterComboBox();
            CheckMaxInvNums();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
           
        }

        private void tabCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilterComboBox();
        }

        private void mnuOptions_Click(object sender, EventArgs e)
        {
            frmSettings form = new frmSettings();
            form.ShowDialog();
        }


        public const int MaximumNumberOfResults = 100;


        private void button1_Click(object sender, EventArgs e)
        {
            


        }

        private void btnPersonalLending_Click(object sender, EventArgs e)
        {
            EditPersonalLending();
        }

        private void RecalculateAvailableItems()
        {

        }

        private void mnuRecalculateAvailableItems_Click(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();
            string log = "";
            
            // ----- Recalculate Items -----
            var list = db.Items.Select(x => new IInfo { ID = x.ID, Name = x.Name, Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1 }).ToList();

            foreach (var itm in list)
            {
                var lend = db.Lending.Where(p => (p.CopyID == itm.ID) && p.CopyType.Contains(ItemTypes.item.ToString()) && ((p.Status ?? 1) == 0 || (p.Status ?? 1) == 1)).Select(c => c.ID).ToList();
                int available = itm.Count - lend.Count;
                if (available < 0)
                {
                    log += Lng.Get("Error") + ": " + itm.Name + " - " + Lng.Get("AvailableNegative", "available is negative number!") + " " + available.ToString() + " -> 0" + Environment.NewLine;
                    available = 0;
                }

                if (available != itm.Available)
                {
                    log += Lng.Get("Fixed") + ": " + itm.Name + " - " + Lng.Get("Available") + " " + itm.Available.ToString() + " -> " + available.ToString() + Environment.NewLine;
                   
                    Items item = db.Items.Find(itm.ID);
                    if (item != null) item.Available = (short) available; 
                }
            }

            // ----- Recalculate Books -----
            list = db.Books.Select(x => new IInfo { ID = x.ID, Name = x.Title, Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1 }).ToList();

            foreach (var itm in list)
            {
                var lend = db.Lending.Where(p => (p.CopyID == itm.ID) && p.CopyType.Contains(ItemTypes.book.ToString()) && ((p.Status ?? 1) == 0 || (p.Status ?? 1) == 1)).Select(c => c.ID).ToList();
                int available = itm.Count - lend.Count;
                if (available < 0)
                {
                    log += Lng.Get("Error") + ": " + itm.Name + " - " + Lng.Get("AvailableNegative", "available is negative number!") + " " + available.ToString() + " -> 0" + Environment.NewLine;
                    available = 0;
                }

                if (available != itm.Available)
                {
                    log += Lng.Get("Fixed") + ": " + itm.Name + " - " + Lng.Get("Available") + " " + itm.Available.ToString() + " -> " + available.ToString() + Environment.NewLine;

                    Books book = db.Books.Find(itm.ID);
                    if (book != null) book.Available = (short)available;
                }
            }

            // ----- Recalculate Board games -----
            list = db.Boardgames.Select(x => new IInfo { ID = x.ID, Name = x.Name, Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1 }).ToList();

            foreach (var itm in list)
            {
                var lend = db.Lending.Where(p => (p.CopyID == itm.ID) && p.CopyType.Contains(ItemTypes.boardgame.ToString()) && ((p.Status ?? 1) == 0 || (p.Status ?? 1) == 1)).Select(c => c.ID).ToList();
                int available = itm.Count - lend.Count;
                if (available < 0)
                {
                    log += Lng.Get("Error") + ": " + itm.Name + " - " + Lng.Get("AvailableNegative", "available is negative number!") + " " + available.ToString() + " -> 0" + Environment.NewLine;
                    available = 0;
                }

                if (available != itm.Available)
                {
                    log += Lng.Get("Fixed") + ": " + itm.Name + " - " + Lng.Get("Available") + " " + itm.Available.ToString() + " -> " + available.ToString() + Environment.NewLine;

                    Boardgames board = db.Boardgames.Find(itm.ID);
                    if (board != null) board.Available = (short)available;
                }
            }

            // ----- Save DB -----
            db.SaveChanges();

            UpdateItemsOLV();
            UpdateBooksOLV();
            UpdateBoardOLV();

            if (log == "")
                Dialogs.ShowInfo(Lng.Get("Done") + " - " + Lng.Get("NoChanges", "no changes") + ".", Lng.Get("Done"));
            else
                Dialogs.ShowInfo(log, Lng.Get("Done"));
        }

        private void btnPrintTest_Click(object sender, EventArgs e)
        {
            List<LInfo> list = new List<LInfo>();
            LInfo info = new LInfo();
            info.Name = "XXX";
            info.LendTo = DateTime.Now;
            list.Add(info);
            list.Add(info);
            PrintPDF.CreateTemplate(list);
        }

        private int FindTabPosition(TabPage page)
        {
            int pos = 0;
            if (page == tabContacts) return pos;
            if (mnuShowContacts.Checked) pos++;
            if (page == tabLending) return pos;
            if (mnuShowLending.Checked) pos++;
            if (page == tabBorrowing) return pos;
            if (mnuShowBorrowing.Checked) pos++;
            if (page == tabItems) return pos;
            if (mnuShowItems.Checked) pos++;
            if (page == tabBooks) return pos;
            if (mnuShowBooks.Checked) pos++;
            if (page == tabBoardGames) return pos;
           


            return pos;
        }

        private void mnuShowTabs_Click(object sender, EventArgs e)
        {
            // ----- Tab Contacts -----
            if (((ToolStripMenuItem)sender).Tag == "Contacts")
            {
                if (((ToolStripMenuItem)sender).Checked == false)
                    TabBars.TabPages.Remove(tabContacts);
                else
                    TabBars.TabPages.Insert(0, tabContacts);
            }
            // ----- Tab Lending -----
            else if (((ToolStripMenuItem)sender).Tag == "Lending")
            {
                if (((ToolStripMenuItem)sender).Checked == false)
                    TabBars.TabPages.Remove(tabLending);
                else
                    TabBars.TabPages.Insert(FindTabPosition(tabLending), tabLending);
            }
            // ----- Tab Borrowing -----
            else if (((ToolStripMenuItem)sender).Tag == "Borrowing")
            {
                if (((ToolStripMenuItem)sender).Checked == false)
                    TabBars.TabPages.Remove(tabBorrowing);
                else
                    TabBars.TabPages.Insert(FindTabPosition(tabBorrowing), tabBorrowing);
            }
            // ----- Tab Items -----
            else if (((ToolStripMenuItem)sender).Tag == "Items")
            {
                if (((ToolStripMenuItem)sender).Checked == false)
                    TabBars.TabPages.Remove(tabItems);
                else
                    TabBars.TabPages.Insert(FindTabPosition(tabItems), tabItems);
            }
            // ----- Tab Books -----
            else if (((ToolStripMenuItem)sender).Tag == "Books")
            {
                if (((ToolStripMenuItem)sender).Checked == false)
                    TabBars.TabPages.Remove(tabBooks);
                else
                    TabBars.TabPages.Insert(FindTabPosition(tabBooks), tabBooks);
            }
            // ----- Tab Boardgames -----
            else if (((ToolStripMenuItem)sender).Tag == "Boardgames")
            {
                if (((ToolStripMenuItem)sender).Checked == false)
                    TabBars.TabPages.Remove(tabBoardGames);
                else
                    TabBars.TabPages.Insert(FindTabPosition(tabBoardGames), tabBoardGames);
            }
        }

        private void mnuShowToolbars_Click(object sender, EventArgs e)
        {
            // ----- Toolbar Filter -----
            if (((ToolStripMenuItem)sender).Tag == "Filter")
            {
                toolFilter.Visible = ((ToolStripMenuItem)sender).Checked;
            }
            // ----- Toolbar Fast Filter -----
            else if (((ToolStripMenuItem)sender).Tag == "FastFilter")
            {
                toolFastFilter.Visible = ((ToolStripMenuItem)sender).Checked;
            }
        }

        private void mnuCLending_Click(object sender, EventArgs e)
        {
            EditPersonalLending();
        }

        private void mnuCBorrowing_Click(object sender, EventArgs e)
        {
            EditPersonalLending(true);
        }

        private void mnuCImportGoogle_Click(object sender, EventArgs e)
        {
            ImportGoogleContact();
        }

        private void mnuCExportGoogle_Click(object sender, EventArgs e)
        {
            ExportGoogleContact();
        }

        private void mnuImportGoogleContacts_Click(object sender, EventArgs e)
        {
            ImportGoogleContacts();
        }

        
    }
}
