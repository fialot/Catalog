using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using myFunctions;

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

        #region Contacts

        /// <summary>
        /// Update Contacts ObjectListView
        /// </summary>
        void UpdateConOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Contacts> con = db.Contacts.ToList();
            
            conName.AspectGetter = delegate (object x) {
                return ((Contacts)x).Name.Trim();
            };
            conSurname.AspectGetter = delegate (object x) {
                return ((Contacts)x).Surname.Trim();
            };
            conPhone.AspectGetter = delegate (object x) {
                return ((Contacts)x).Phone.Trim();
            };
            conEmail.AspectGetter = delegate (object x) {
                return ((Contacts)x).Email.Trim();
            };
            conAddress.AspectGetter = delegate (object x) {
                string address = ((Contacts)x).Street.Trim();
                if (address != "") address += ", ";
                address += ((Contacts)x).City.Trim();
                if (address != "") address += ", ";
                address += ((Contacts)x).Country.Trim();
                return address;
            };

            olvContacts.SetObjects(con);
        }

        /// <summary>
        /// OLV Contacts selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }

        /// <summary>
        /// Edit Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvContacts_DoubleClick(object sender, EventArgs e)
        {
            if (olvContacts.SelectedIndex >= 0)
            {
                frmEditContacts form = new frmEditContacts();
                form.ShowDialog(((Contacts)olvContacts.SelectedObject).Id);
                UpdateConOLV();
            }
        }

        #endregion

        #region Borrowing

        string GetBorrItemName(string type, Guid id)
        {
            databaseEntities db = new databaseEntities();

            switch (type)
            {
                case "book":
                    Books book = db.Books.Find(id);
                    if (book != null) return book.Name.Trim();
                    break;
            }
            return "";
        }

        /// <summary>
        /// Update Borrowing ObjectListView
        /// </summary>
        void UpdateBorrOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Borrowing> borr = db.Borrowing.ToList();

            brType.AspectGetter = delegate (object x) {
                switch (((Borrowing)x).type.Trim())
                {
                    case "book":
                        return Lng.Get("Book");
                }
                return "Unknown";
            };
            brName.AspectGetter = delegate (object x) {
                return GetBorrItemName(((Borrowing)x).type.Trim(), ((Borrowing)x).item ?? Guid.Empty);
            };
            brPerson.AspectGetter = delegate (object x) {
                Contacts contact = db.Contacts.Find(((Borrowing)x).person);
                if (contact != null)
                    return contact.Name.Trim() + " " + contact.Surname.Trim();
                else return "";
            };
            brFrom.AspectGetter = delegate (object x) {
                if (((Borrowing)x).from == null) return "";
                DateTime t = ((Borrowing)x).from ?? DateTime.Now;
                return t.ToShortDateString();
            };
            brTo.AspectGetter = delegate (object x) {
                if (((Borrowing)x).to == null) return "";
                DateTime t = ((Borrowing)x).to ?? DateTime.Now;
                return t.ToShortDateString();
            };
            brReturned.AspectGetter = delegate (object x) {
                return ((Borrowing)x).returned.ToString();
            };

            olvBorrowing.SetObjects(borr);
        }

        /// <summary>
        /// OLV Borrowing selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvBorrowing_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }

        /// <summary>
        /// Edit Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvBorrowing_DoubleClick(object sender, EventArgs e)
        {
            if (olvBorrowing.SelectedIndex >= 0)
            {
                frmEditBorrowing form = new frmEditBorrowing();
                form.ShowDialog(((Borrowing)olvBorrowing.SelectedObject).Id);
                UpdateBorrOLV();
            }
        }

        #endregion

        #region Books

        /// <summary>
        /// Update Books ObjectListView
        /// </summary>
        void UpdateBooksOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Books> book = db.Books.ToList();

            bkName.AspectGetter = delegate (object x) {
                return ((Books)x).Name.Trim();
            };
            bkAuthor.AspectGetter = delegate (object x) {
                return ((Books)x).AuthorSurname.Trim() + ", " + ((Books)x).AuthorName.Trim();
            };
            bkType.AspectGetter = delegate (object x) {
                return ((Books)x).Type.Trim();
            };
            bkYear.AspectGetter = delegate (object x) {
                return ((Books)x).Year.ToString();
            };
            bkGenre.AspectGetter = delegate (object x) {
                return ((Books)x).Genre.Trim();
            };
            bkSubgenre.AspectGetter = delegate (object x) {
                return ((Books)x).SubGenre.Trim();
            };
            bkInvNum.AspectGetter = delegate (object x) {
                return ((Books)x).InventoryNumber.Trim();
            };
            bkLocation.AspectGetter = delegate (object x) {
                return ((Books)x).Location.Trim();
            };
            bkKeywords.AspectGetter = delegate (object x) {
                return ((Books)x).Keywords.Trim();
            };
            bkSeries.AspectGetter = delegate (object x) {
                return ((Books)x).Series.Trim() + " " + ((Books)x).SeriesNum.ToString();
            };
            
            olvBooks.SetObjects(book);
        }

        /// <summary>
        /// OLV Books selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }

        /// <summary>
        /// Edit Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvBooks_DoubleClick(object sender, EventArgs e)
        {
            if (olvBooks.SelectedIndex >= 0)
            {
                frmEditBooks form = new frmEditBooks();
                form.ShowDialog(((Books)olvBooks.SelectedObject).Id);
                UpdateBooksOLV();
            }
        }
        
        #endregion


        #endregion

        #region Edit Items

        private void EnableEditItems()
        {
            int index = -1;

            if (tabCatalog.SelectedTab == tabContacts)
                index = olvContacts.SelectedIndex;
            else if (tabCatalog.SelectedTab == tabBooks)
                index = olvBooks.SelectedIndex;

            if (index >= 0)
            {
                btnEditItem.Enabled = true;
                btnDeleteItem.Enabled = true;
                mnuEditItem.Enabled = true;
                mnuDelItem.Enabled = true;
            }
            else
            {
                btnEditItem.Enabled = false;
                btnDeleteItem.Enabled = false;
                mnuEditItem.Enabled = false;
                mnuDelItem.Enabled = false;
            }
        }

        /// <summary>
        /// Button New Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewItem_Click(object sender, EventArgs e)
        {
            if (tabCatalog.SelectedTab == tabContacts)
            {
                frmEditContacts form = new frmEditContacts();
                form.ShowDialog();
                UpdateConOLV();
            }
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                frmEditBorrowing form = new frmEditBorrowing();
                form.ShowDialog();
                UpdateBorrOLV();
            }
            else if (tabCatalog.SelectedTab == tabBooks)
            {
                frmEditBooks form = new frmEditBooks();
                form.ShowDialog();
                UpdateBooksOLV();
            } 
        }

        /// <summary>
        /// Button Edit Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (tabCatalog.SelectedTab == tabContacts)
            {
                if (olvContacts.SelectedIndex >= 0)
                {
                    frmEditContacts form = new frmEditContacts();
                    form.ShowDialog(((Contacts)olvContacts.SelectedObject).Id);
                    UpdateConOLV();
                }
                
            }
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                if (olvBorrowing.SelectedIndex >= 0)
                {
                    frmEditBorrowing form = new frmEditBorrowing();
                    form.ShowDialog(((Borrowing)olvBorrowing.SelectedObject).Id);
                    UpdateBorrOLV();
                }
            }
            else if (tabCatalog.SelectedTab == tabBooks)
            {
                if (olvBooks.SelectedIndex >= 0)
                {
                    frmEditBooks form = new frmEditBooks();
                    form.ShowDialog(((Books)olvBooks.SelectedObject).Id);
                    UpdateBooksOLV();
                }
            }
        }

        /// <summary>
        /// Button Delete Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();

            if (tabCatalog.SelectedTab == tabContacts)
            {
                if (olvContacts.SelectedIndex >= 0)
                {
                    Contacts contact = db.Contacts.Find(((Contacts)olvContacts.SelectedObject).Id);

                    if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + contact.Name.Trim() + " " + contact.Surname.Trim() + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        db.Contacts.Remove(contact);
                        db.SaveChanges();
                        UpdateConOLV();
                    }

                }
            }
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                if (olvBorrowing.SelectedIndex >= 0)
                {
                    Borrowing borr = db.Borrowing.Find(((Borrowing)olvBorrowing.SelectedObject).Id);

                    if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + GetBorrItemName(borr.type.Trim(), borr.Id) + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        db.Borrowing.Remove(borr);
                        db.SaveChanges();
                        UpdateBorrOLV();
                    }

                }
            }
            else if (tabCatalog.SelectedTab == tabBooks)
            {
                if (olvBooks.SelectedIndex >= 0)
                {
                    Books book = db.Books.Find(((Books)olvBooks.SelectedObject).Id);

                    if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + book.Name + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        db.Books.Remove(book);
                        db.SaveChanges();
                        UpdateBooksOLV();
                    }

                }
            }
        }

        #endregion

        #region Filter

        TextMatchFilter FastFilter;                         // Fast filter
        TextMatchFilter StandardFilter;                     // Standard filter

        List<string> FastFilterList = new List<string>();   // Fast Filter list

        /// <summary>
        /// Update Filter Combobox
        /// </summary>
        void UpdateFilterComboBox()
        {
            cbFilterCol.Items.Clear();
            cbFastFilterCol.Items.Clear();
            if (tabCatalog.SelectedTab == tabContacts)
            {
                cbFilterCol.Items.Add(Lng.Get("All"));
                cbFilterCol.Items.Add(Lng.Get("Name"));
                cbFilterCol.Items.Add(Lng.Get("Surname"));
                cbFilterCol.Items.Add(Lng.Get("Phone"));
                cbFilterCol.Items.Add(Lng.Get("Email"));
                cbFilterCol.Items.Add(Lng.Get("Address"));
                cbFilterCol.SelectedIndex = 0;

                cbFastFilterCol.Items.Add(Lng.Get("All"));
                cbFastFilterCol.Items.Add(Lng.Get("Name"));
                cbFastFilterCol.Items.Add(Lng.Get("Surname"));
                cbFastFilterCol.Items.Add(Lng.Get("Phone"));
                cbFastFilterCol.Items.Add(Lng.Get("Email"));
                cbFastFilterCol.Items.Add(Lng.Get("Address"));
                cbFastFilterCol.SelectedIndex = 0;
            }
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                cbFilterCol.Items.Add(Lng.Get("All"));
                cbFilterCol.Items.Add(Lng.Get("Type"));
                cbFilterCol.Items.Add(Lng.Get("ItemName"));
                cbFilterCol.Items.Add(Lng.Get("Person"));
                cbFilterCol.Items.Add(Lng.Get("From"));
                cbFilterCol.Items.Add(Lng.Get("To"));
                cbFilterCol.Items.Add(Lng.Get("Returned"));
                cbFilterCol.SelectedIndex = 0;

                cbFastFilterCol.Items.Add(Lng.Get("All"));
                cbFastFilterCol.Items.Add(Lng.Get("Type"));
                cbFastFilterCol.Items.Add(Lng.Get("ItemName"));
                cbFastFilterCol.Items.Add(Lng.Get("Person"));
                cbFastFilterCol.SelectedIndex = 0;
            }
            else if (tabCatalog.SelectedTab == tabBooks)
            {
                cbFilterCol.Items.Add(Lng.Get("All"));
                cbFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
                cbFilterCol.Items.Add(Lng.Get("Author"));
                cbFilterCol.Items.Add(Lng.Get("Type"));
                cbFilterCol.Items.Add(Lng.Get("Year"));
                cbFilterCol.Items.Add(Lng.Get("Genre"));
                cbFilterCol.Items.Add(Lng.Get("Subgenre"));
                cbFilterCol.Items.Add(Lng.Get("InvNum", "Inv. Num."));
                cbFilterCol.Items.Add(Lng.Get("Location"));
                cbFilterCol.Items.Add(Lng.Get("Keywords"));
                cbFilterCol.Items.Add(Lng.Get("Series"));
                cbFilterCol.SelectedIndex = 0;

                cbFastFilterCol.Items.Add(Lng.Get("All"));
                cbFastFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
                cbFastFilterCol.Items.Add(Lng.Get("Author"));
                cbFastFilterCol.Items.Add(Lng.Get("Type"));
                cbFastFilterCol.Items.Add(Lng.Get("Genre"));
                cbFastFilterCol.Items.Add(Lng.Get("Subgenre"));
                cbFastFilterCol.Items.Add(Lng.Get("Location"));
                cbFastFilterCol.Items.Add(Lng.Get("Series"));
                cbFastFilterCol.SelectedIndex = 0;
            }

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
                        FastFilterList.Add("Ý");
                    FastFilterList.Remove(letter);
                }
                
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
            UseStandardFilter();

            if (tabCatalog.SelectedTab == tabContacts)
            {
                olvContacts.UseFiltering = true;
                olvContacts.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, StandardFilter });
            }
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                olvBorrowing.UseFiltering = true;
                olvBorrowing.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, StandardFilter });
            }
            else if (tabCatalog.SelectedTab == tabBooks)
            {
                olvBooks.UseFiltering = true;
                olvBooks.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, StandardFilter });
            }
        }

        /// <summary>
        /// Use Fast Filter
        /// </summary>
        private void UseFastFilter()
        {
            if (tabCatalog.SelectedTab == tabContacts)
            {
                if (FastFilterList.Count == 0)
                    FastFilter = TextMatchFilter.Contains(olvContacts, "");
                else
                {
                    string[] filterArray = FastFilterList.ToArray();
                    FastFilter = TextMatchFilter.Prefix(olvContacts, filterArray);
                }
                if (cbFastFilterCol.SelectedIndex == 0)
                    FastFilter.Columns = new OLVColumn[] { conName, conSurname, conPhone, conEmail, conAddress };
                else if (cbFastFilterCol.SelectedIndex == 1)
                    FastFilter.Columns = new OLVColumn[] { conName };
                else if (cbFastFilterCol.SelectedIndex == 2)
                    FastFilter.Columns = new OLVColumn[] { conSurname };
                else if (cbFastFilterCol.SelectedIndex == 3)
                    FastFilter.Columns = new OLVColumn[] { conPhone };
                else if (cbFastFilterCol.SelectedIndex == 4)
                    FastFilter.Columns = new OLVColumn[] { conEmail };
                else if (cbFastFilterCol.SelectedIndex == 5)
                    FastFilter.Columns = new OLVColumn[] { conAddress };
            }
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                if (FastFilterList.Count == 0)
                    FastFilter = TextMatchFilter.Contains(olvBorrowing, "");
                else
                {
                    string[] filterArray = FastFilterList.ToArray();
                    FastFilter = TextMatchFilter.Prefix(olvBorrowing, filterArray);
                }
                if (cbFastFilterCol.SelectedIndex == 0)
                    FastFilter.Columns = new OLVColumn[] { brType, brName, brPerson};
                else if (cbFastFilterCol.SelectedIndex == 1)
                    FastFilter.Columns = new OLVColumn[] { brType };
                else if (cbFastFilterCol.SelectedIndex == 2)
                    FastFilter.Columns = new OLVColumn[] { brName };
                else if (cbFastFilterCol.SelectedIndex == 3)
                    FastFilter.Columns = new OLVColumn[] { brPerson };
            }
            else if (tabCatalog.SelectedTab == tabBooks)
            {
                if (FastFilterList.Count == 0)
                    FastFilter = TextMatchFilter.Contains(olvBooks, "");
                else
                {
                    string[] filterArray = FastFilterList.ToArray();
                    FastFilter = TextMatchFilter.Prefix(olvBooks, filterArray);
                }
                if (cbFastFilterCol.SelectedIndex == 0)
                    FastFilter.Columns = new OLVColumn[] { bkAuthor, bkGenre, bkInvNum, bkKeywords, bkLocation, bkName, bkSeries, bkSubgenre, bkType, bkYear };
                else if (cbFastFilterCol.SelectedIndex == 1)
                    FastFilter.Columns = new OLVColumn[] { bkName };
                else if (cbFastFilterCol.SelectedIndex == 2)
                    FastFilter.Columns = new OLVColumn[] { bkAuthor };
                else if (cbFastFilterCol.SelectedIndex == 3)
                    FastFilter.Columns = new OLVColumn[] { bkType };
                else if (cbFastFilterCol.SelectedIndex == 4)
                    FastFilter.Columns = new OLVColumn[] { bkGenre };
                else if (cbFastFilterCol.SelectedIndex == 5)
                    FastFilter.Columns = new OLVColumn[] { bkSubgenre };
                else if (cbFastFilterCol.SelectedIndex == 6)
                    FastFilter.Columns = new OLVColumn[] { bkLocation };
                else if (cbFastFilterCol.SelectedIndex == 7)
                    FastFilter.Columns = new OLVColumn[] { bkSeries };
            }

        }
        
        /// <summary>
        /// Use Standard Filter
        /// </summary>
        private void UseStandardFilter()
        {
            if (tabCatalog.SelectedTab == tabContacts)
            {
                StandardFilter = TextMatchFilter.Contains(olvContacts, txtFilter.Text);

                if (cbFilterCol.SelectedIndex == 0)
                    StandardFilter.Columns = new OLVColumn[] { conName, conSurname, conPhone, conEmail, conAddress };
                else if (cbFilterCol.SelectedIndex == 1)
                    StandardFilter.Columns = new OLVColumn[] { conName };
                else if (cbFilterCol.SelectedIndex == 2)
                    StandardFilter.Columns = new OLVColumn[] { conSurname };
                else if (cbFilterCol.SelectedIndex == 3)
                    StandardFilter.Columns = new OLVColumn[] { conPhone };
                else if (cbFilterCol.SelectedIndex == 4)
                    StandardFilter.Columns = new OLVColumn[] { conEmail };
                else if (cbFilterCol.SelectedIndex == 5)
                    StandardFilter.Columns = new OLVColumn[] { conAddress };
            } 
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                StandardFilter = TextMatchFilter.Contains(olvBorrowing, txtFilter.Text);

                if (cbFilterCol.SelectedIndex == 0)
                    StandardFilter.Columns = new OLVColumn[] { brType, brName, brPerson, brFrom, brTo, brReturned };
                else if (cbFilterCol.SelectedIndex == 1)
                    StandardFilter.Columns = new OLVColumn[] { brType };
                else if (cbFilterCol.SelectedIndex == 2)
                    StandardFilter.Columns = new OLVColumn[] { brName };
                else if (cbFilterCol.SelectedIndex == 3)
                    StandardFilter.Columns = new OLVColumn[] { brPerson };
                else if (cbFilterCol.SelectedIndex == 4)
                    StandardFilter.Columns = new OLVColumn[] { brFrom };
                else if (cbFilterCol.SelectedIndex == 5)
                    StandardFilter.Columns = new OLVColumn[] { brTo };
                else if (cbFilterCol.SelectedIndex == 6)
                    StandardFilter.Columns = new OLVColumn[] { brReturned };
            }
            else if (tabCatalog.SelectedTab == tabBooks)
            {
                StandardFilter = TextMatchFilter.Contains(olvBooks, txtFilter.Text);

                if (cbFilterCol.SelectedIndex == 0)
                    StandardFilter.Columns = new OLVColumn[] { bkAuthor, bkGenre, bkInvNum, bkKeywords, bkLocation, bkName, bkSeries, bkSubgenre, bkType, bkYear };
                else if (cbFilterCol.SelectedIndex == 1)
                    StandardFilter.Columns = new OLVColumn[] { bkName };
                else if (cbFilterCol.SelectedIndex == 2)
                    StandardFilter.Columns = new OLVColumn[] { bkAuthor };
                else if (cbFilterCol.SelectedIndex == 3)
                    StandardFilter.Columns = new OLVColumn[] { bkType };
                else if (cbFilterCol.SelectedIndex == 4)
                    StandardFilter.Columns = new OLVColumn[] { bkYear };
                else if (cbFilterCol.SelectedIndex == 5)
                    StandardFilter.Columns = new OLVColumn[] { bkGenre };
                else if (cbFilterCol.SelectedIndex == 6)
                    StandardFilter.Columns = new OLVColumn[] { bkSubgenre };
                else if (cbFilterCol.SelectedIndex == 7)
                    StandardFilter.Columns = new OLVColumn[] { bkInvNum };
                else if (cbFilterCol.SelectedIndex == 8)
                    StandardFilter.Columns = new OLVColumn[] { bkLocation };
                else if (cbFilterCol.SelectedIndex == 9)
                    StandardFilter.Columns = new OLVColumn[] { bkKeywords };
                else if (cbFilterCol.SelectedIndex == 10)
                    StandardFilter.Columns = new OLVColumn[] { bkSeries };
            }
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
        
        private void ExportCSV(string path, List<Contacts> con)
        {
            string lines;

            lines = "name;surname;nick;sex;birth;phone;email;www;im;company;position;street;city;region;country;postcode;code;note;groups;tags;GUID" + Environment.NewLine;

            foreach(var item in con)
            {
                lines += item.Name.Trim() + ";" + item.Surname.Trim() + ";" + item.Nick.Trim() + ";" + item.sex.Trim() + ";" + item.Birth.ToString() + ";" + item.Phone.Trim() + ";" + item.Email.Trim() + ";" + item.WWW.Trim() + ";" + item.IM.Trim() + ";";
                lines += item.Company.Trim() + ";" + item.Position.Trim() + ";" + item.Street.Trim() + ";" + item.City.Trim() + ";" + item.Region.Trim() + ";" + item.Country.Trim() + ";" + item.PostCode.Trim() + ";";
                lines += item.code.Trim() + ";" + item.Note.Trim() + ";" + item.Tags.Trim() + ";" + item.FastTags.ToString() + ";" + item.Id + Environment.NewLine;
            }

            Files.SaveFile(path, lines);
        }

        

        private void mnuExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV file|*.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (tabCatalog.SelectedTab == tabContacts)
                {
                    List<Contacts> con = new List<Contacts>();

                    foreach (var item in olvContacts.FilteredObjects)
                    {
                        con.Add((Contacts)item);
                    }
                    ExportCSV(dialog.FileName, con);
                }
            }
        }

        #endregion

        #region Import

        private List<Contacts> ImportConCSV(string path)
        {
            List<Contacts> con = new List<Contacts>();
            string text = Files.LoadFile(path);
            CSVfile file = Files.ParseCSV(text);

            foreach (var item in file.data)
            {
                Contacts contact = new Contacts();
                contact.Name = item[0];
                contact.Surname = item[1];
                contact.Nick = item[2];
                contact.sex = item[3];
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
                contact.code = item[16];
                contact.Note = item[17];
                contact.Tags = item[18];
                contact.FastTags = Conv.ToUIntDef(item[19], 0);
                contact.Id = Conv.ToGuid(item[20]);
                con.Add(contact);
            }

            return con;
        }

        private void FillContact(ref Contacts contact, Contacts newItem)
        {
            // ----- Avatar -----
            //contact.Avatar = ImageToByteArray(imgAvatar.Image);

            // ----- Name -----
            contact.Name = newItem.Name;
            contact.Surname = newItem.Surname;
            contact.Nick = newItem.Nick;
            contact.sex = newItem.sex;

            // ----- Contacts -----
            //for (int i = 0; i < )
            contact.Phone = newItem.Phone;

            contact.Email = newItem.Email;
            contact.WWW = newItem.WWW;
            contact.IM = newItem.IM;


            // ----- Address -----
            contact.Street = newItem.Street;
            contact.City = newItem.City;
            contact.Region = newItem.Region;
            contact.Country = newItem.Country;
            contact.PostCode = newItem.PostCode;

            contact.Note = newItem.Note;
            contact.Birth = newItem.Birth;
            contact.Tags = newItem.Tags;
            //contact.FastTags = 0;
            

            contact.code = newItem.code;
            contact.update = DateTime.Now;

            contact.Company = newItem.Company;
            contact.Position = newItem.Position;


            // ----- Unused now -----
            /*contact.Partner = "";
            contact.Childs = "";
            contact.Parrents = "";
            contact.GoogleID = "";*/
        }

        private void mnuImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV file|*.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                databaseEntities db = new databaseEntities();

                if (tabCatalog.SelectedTab == tabContacts)
                {
                    List<Contacts> con = ImportConCSV(dialog.FileName);
                    foreach (var item in con)
                    {
                        

                        Contacts contact;
                        // ----- ID -----
                        if (item.Id != Guid.Empty)
                        {
                            contact = db.Contacts.Find(item.Id);
                            if (contact != null)
                                FillContact(ref contact, item);
                            else
                            {
                                db.Contacts.Add(item);
                            }
                            
                        }
                        else
                        {
                            item.Id = Guid.NewGuid();
                            db.Contacts.Add(item);
                        }
                    }
                    db.SaveChanges();
                    UpdateConOLV();
                }
            }


        }

        #endregion

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            frmAbout formAbout = new frmAbout();
            formAbout.ShowDialog();
        }

        
        

        private void frmMain_Load(object sender, EventArgs e)
        {
            UpdateConOLV();
            UpdateBorrOLV();
            UpdateBooksOLV();
            EnableEditItems();
            UpdateFilterComboBox();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            UpdateConOLV();
        }

        private void tabCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilterComboBox();
        }

    }
}
