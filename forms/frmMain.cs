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

        #region Contacts

        /// <summary>
        /// Update Contacts ObjectListView
        /// </summary>
        void UpdateConOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Contacts> con = db.Contacts.ToList();

            conFastTags.Renderer = new ImageRenderer();
            conFastTags.AspectGetter = delegate (object x) {
                List<int> ret = new List<int>();
                FastFlags flag = (FastFlags)((Contacts)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) ret.Add(0);
                if (flag.HasFlag(FastFlags.FLAG2)) ret.Add(1);
                if (flag.HasFlag(FastFlags.FLAG3)) ret.Add(2);
                if (flag.HasFlag(FastFlags.FLAG4)) ret.Add(3);
                if (flag.HasFlag(FastFlags.FLAG5)) ret.Add(4);
                if (flag.HasFlag(FastFlags.FLAG6)) ret.Add(5);

                return ret;
            };
            conFastTagsNum.AspectGetter = delegate (object x) {
                string res = "";
                FastFlags flag = (FastFlags)((Contacts)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) res += "1";
                if (flag.HasFlag(FastFlags.FLAG2)) res += "2";
                if (flag.HasFlag(FastFlags.FLAG3)) res += "3";
                if (flag.HasFlag(FastFlags.FLAG4)) res += "4";
                if (flag.HasFlag(FastFlags.FLAG5)) res += "5";
                if (flag.HasFlag(FastFlags.FLAG6)) res += "6";
                return res;
            };
            conName.AspectGetter = delegate (object x) {
                return ((Contacts)x).Name;
            };
            conSurname.AspectGetter = delegate (object x) {
                return ((Contacts)x).Surname;
            };
            conPhone.AspectGetter = delegate (object x) {
                return ((Contacts)x).Phone;
            };
            conEmail.AspectGetter = delegate (object x) {
                return ((Contacts)x).Email;
            };
            conAddress.AspectGetter = delegate (object x) {
                string address = ((Contacts)x).Street;
                string city = ((Contacts)x).City;
                string country = ((Contacts)x).Country;

                if ((city != null && city != "") && (address != null && address != ""))
                    address += ", ";
                address += city;

                if ((country != null && country != "") && (address != null && address != ""))
                    address += ", ";
                address += country;
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
        
        #endregion

        #region Lending

        /// <summary>
        /// Update Lending ObjectListView
        /// </summary>
        void UpdateLendingOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Lending> lend;
            // ----- Show Expired -----
            if (cbLendingShow.SelectedIndex == 1)
            {
                DateTime now = DateTime.Now;
                lend = db.Lending.Where(p => ((p.To ?? now) < DateTime.Now) && ((p.Status ?? 1) == 0 || (p.Status ?? 1) == 1)).ToList();
            }
            // ----- Show Borrowed -----
            else if (cbLendingShow.SelectedIndex == 2)
            {
                if (chbShowReturned.Checked)
                    lend = db.Lending.Where(p => ((p.Status ?? 1) == 1 || (p.Status ?? 1) == 2)).ToList();
                else
                    lend = db.Lending.Where(p => (p.Status ?? 1) == 1).ToList();
            }
            // ----- Show Reserved -----
            else if (cbLendingShow.SelectedIndex == 3)
            {
                if (chbShowReturned.Checked)
                    lend = db.Lending.Where(p => ((p.Status ?? 1) == 0 || (p.Status ?? 1) == 3)).ToList();
                else
                    lend = db.Lending.Where(p => (p.Status ?? 1) == 0).ToList();
            }
            // ----- Show All -----
            else
            {
                if (chbShowReturned.Checked)
                    lend = db.Lending.ToList();
                else
                    lend = db.Lending.Where(p => (p.Status ?? 1) == 0 || (p.Status ?? 1) == 1).ToList();
            }

            ldFastTags.Renderer = new ImageRenderer();
            ldFastTags.AspectGetter = delegate (object x) {
                List<int> ret = new List<int>();
                if (((Lending)x).FastTags != null)
                {
                    FastFlags flag = (FastFlags)((Lending)x).FastTags;
                    if (flag.HasFlag(FastFlags.FLAG1)) ret.Add(0);
                    if (flag.HasFlag(FastFlags.FLAG2)) ret.Add(1);
                    if (flag.HasFlag(FastFlags.FLAG3)) ret.Add(2);
                    if (flag.HasFlag(FastFlags.FLAG4)) ret.Add(3);
                    if (flag.HasFlag(FastFlags.FLAG5)) ret.Add(4);
                    if (flag.HasFlag(FastFlags.FLAG6)) ret.Add(5);
                }
                return ret;
            };
            ldFastTagsNum.AspectGetter = delegate (object x) {
                string res = "";
                if (((Lending)x).FastTags != null)
                {
                    FastFlags flag = (FastFlags)((Lending)x).FastTags;
                    if (flag.HasFlag(FastFlags.FLAG1)) res += "1";
                    if (flag.HasFlag(FastFlags.FLAG2)) res += "2";
                    if (flag.HasFlag(FastFlags.FLAG3)) res += "3";
                    if (flag.HasFlag(FastFlags.FLAG4)) res += "4";
                    if (flag.HasFlag(FastFlags.FLAG5)) res += "5";
                    if (flag.HasFlag(FastFlags.FLAG6)) res += "6";
                }
                return res;
            };
            ldPerson.AspectGetter = delegate (object x) {
                Contacts contact = db.Contacts.Find(((Lending)x).PersonID);
                if (contact != null)
                    return contact.Name.Trim() + " " + contact.Surname.Trim();
                else return "";
            };
            ldItemType.AspectGetter = delegate (object x) {
                switch (((Lending)x).CopyType.Trim())
                {
                    case "item":
                        return Lng.Get("Item");
                    case "book":
                        return Lng.Get("Book");
                    case "boardgame":
                        return Lng.Get("Boardgame", "Board game");
                }
                return Lng.Get("Unknown");
            };
            ldItemName.AspectGetter = delegate (object x) {
                var copy = db.Copies.Find(((Lending)x).CopyID);
                return global.GetLendingItemName(copy.ItemType, copy.ItemID ?? Guid.Empty);
            };
            ldItemNum.AspectGetter = delegate (object x) {
                return 0;
            };
            ldItemInvNum.AspectGetter = delegate (object x) {
                if (((Lending)x).CopyID != null)
                    return db.Copies.Find(((Lending)x).CopyID).InventoryNumber;
                return "";
            };
            ldFrom.AspectGetter = delegate (object x) {
                if (((Lending)x).From == null) return "";
                DateTime t = ((Lending)x).From ?? DateTime.Now;
                return t.ToShortDateString();
            };
            ldTo.AspectGetter = delegate (object x) {
                if (((Lending)x).To == null) return "";
                DateTime t = ((Lending)x).To ?? DateTime.Now;
                return t.ToShortDateString();
            };
            //brStatus.Renderer = new ImageRenderer();
            ldStatus.ImageGetter = delegate (object x) {
                int status = ((Lending)x).Status ?? 1;
                if (status == 2)        // Returned
                    return 6;
                else if (status == 0)   // Reserved
                    return 9;
                else if (status == 3)   // Canceled
                    return 7;
                else return 10;         // Borrowed
            };
            ldStatus.AspectGetter = delegate (object x) {
                int status = ((Lending)x).Status ?? 1;
                if (status == 2)        // Returned
                    return Lng.Get("Returned");
                else if (status == 0)   // Reserved
                    return Lng.Get("Reserved");
                else if (status == 3)   // Canceled
                    return Lng.Get("Canceled");
                else return Lng.Get("Borrowed"); // Borrowed
            };
            ldNote.AspectGetter = delegate (object x) {
                return ((Lending)x).Note;
            };
            olvLending.SetObjects(lend);
        }

        /// <summary>
        /// OLV Lending selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvLending_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }

        /// <summary>
        /// Color Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvLending_FormatRow(object sender, FormatRowEventArgs e)
        {
            Lending itm = (Lending)e.Model;
            DateTime now = DateTime.Now;
            if (itm.Status == 2 || itm.Status == 3)
                e.Item.ForeColor = Color.Gray;
            else if ((itm.To ?? now) < now )
                e.Item.ForeColor = Color.Red;
            else
                e.Item.ForeColor = Color.Black;
        }

        /// <summary>
        /// Show returned Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbShowReturned_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLendingOLV();
        }
        
        private void cbLendingShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLendingOLV();
        }

        private void olvLending_DoubleClick(object sender, EventArgs e)
        {
            EditPersonalLending();
        }

        #endregion

        #region Borrowing

        /// <summary>
        /// Update Lending ObjectListView
        /// </summary>
        void UpdateBorrowingOLV()
        {
            databaseEntities db = new databaseEntities();


            List<Borrowing> borr;
            // ----- Show Expired -----
            if (cbBorrowingShow.SelectedIndex == 1)
            {
                DateTime now = DateTime.Now;
                borr = db.Borrowing.Where(p => ((p.To ?? now) < DateTime.Now) && ((p.Status ?? 1) == 0 || (p.Status ?? 1) == 1)).ToList();
            }
            // ----- Show Borrowed -----
            else if (cbBorrowingShow.SelectedIndex == 2)
            {
                if (chbBorrowingReturned.Checked)
                    borr = db.Borrowing.Where(p => ((p.Status ?? 1) == 1 || (p.Status ?? 1) == 2)).ToList();
                else
                    borr = db.Borrowing.Where(p => (p.Status ?? 1) == 1).ToList();
            }
            // ----- Show Reserved -----
            else if (cbBorrowingShow.SelectedIndex == 3)
            {
                if (chbBorrowingReturned.Checked)
                    borr = db.Borrowing.Where(p => ((p.Status ?? 1) == 0 || (p.Status ?? 1) == 3)).ToList();
                else
                    borr = db.Borrowing.Where(p => (p.Status ?? 1) == 0).ToList();
            }
            // ----- Show All -----
            else
            {
                if (chbBorrowingReturned.Checked)
                    borr = db.Borrowing.ToList();
                else
                    borr = db.Borrowing.Where(p => (p.Status ?? 1) == 0 || (p.Status ?? 1) == 1).ToList();
            }

            brFastTags.Renderer = new ImageRenderer();
            brFastTags.AspectGetter = delegate (object x) {
                List<int> ret = new List<int>();
                if (((Borrowing)x).FastTags != null)
                {
                    FastFlags flag = (FastFlags)((Borrowing)x).FastTags;
                    if (flag.HasFlag(FastFlags.FLAG1)) ret.Add(0);
                    if (flag.HasFlag(FastFlags.FLAG2)) ret.Add(1);
                    if (flag.HasFlag(FastFlags.FLAG3)) ret.Add(2);
                    if (flag.HasFlag(FastFlags.FLAG4)) ret.Add(3);
                    if (flag.HasFlag(FastFlags.FLAG5)) ret.Add(4);
                    if (flag.HasFlag(FastFlags.FLAG6)) ret.Add(5);
                }
                return ret;
            };
            brFastTagsNum.AspectGetter = delegate (object x) {
                string res = "";
                if (((Borrowing)x).FastTags != null)
                {
                    FastFlags flag = (FastFlags)((Borrowing)x).FastTags;
                    if (flag.HasFlag(FastFlags.FLAG1)) res += "1";
                    if (flag.HasFlag(FastFlags.FLAG2)) res += "2";
                    if (flag.HasFlag(FastFlags.FLAG3)) res += "3";
                    if (flag.HasFlag(FastFlags.FLAG4)) res += "4";
                    if (flag.HasFlag(FastFlags.FLAG5)) res += "5";
                    if (flag.HasFlag(FastFlags.FLAG6)) res += "6";
                }
                return res;
            };
            brPerson.AspectGetter = delegate (object x) {
                Contacts contact = db.Contacts.Find(((Borrowing)x).PersonID);
                if (contact != null)
                    return contact.Name.Trim() + " " + contact.Surname.Trim();
                else return "";
            };
            brItemName.AspectGetter = delegate (object x) {
                return ((Borrowing)x).Item.Trim();
            };
            brItemInvNum.AspectGetter = delegate (object x) {
                if (((Borrowing)x).ItemInvNum != null)
                    return ((Borrowing)x).ItemInvNum.Trim();
                return "";
            };
            brFrom.AspectGetter = delegate (object x) {
                if (((Borrowing)x).From == null) return "";
                DateTime t = ((Borrowing)x).From ?? DateTime.Now;
                return t.ToShortDateString();
            };
            brTo.AspectGetter = delegate (object x) {
                if (((Borrowing)x).To == null) return "";
                DateTime t = ((Borrowing)x).To ?? DateTime.Now;
                return t.ToShortDateString();
            };
            //brStatus.Renderer = new ImageRenderer();
            brStatus.ImageGetter = delegate (object x) {
                int status = ((Borrowing)x).Status ?? 1;
                if (status == 2)        // Returned
                    return 6;
                else if (status == 0)   // Reserved
                    return 9;
                else if (status == 3)   // Canceled
                    return 7;
                else return 10;         // Borrowed
            };
            brStatus.AspectGetter = delegate (object x) {
                int status = ((Borrowing)x).Status ?? 1;
                if (status == 2)        // Returned
                    return Lng.Get("Returned");
                else if (status == 0)   // Reserved
                    return Lng.Get("Reserved");
                else if (status == 3)   // Canceled
                    return Lng.Get("Canceled");
                else return Lng.Get("Borrowed"); // Borrowed
            };
            brNote.AspectGetter = delegate (object x) {
                if (((Borrowing)x).Note != null)
                    return ((Borrowing)x).Note.Trim();
                else
                    return "";
            };

            olvBorrowing.SetObjects(borr);
        }

        /// <summary>
        /// OLV Lending selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvBorrowing_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }

        /// <summary>
        /// Color Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvBorrowing_FormatRow(object sender, FormatRowEventArgs e)
        {
            Borrowing itm = (Borrowing)e.Model;
            DateTime now = DateTime.Now;
            if (itm.Status == 2 || itm.Status == 3)     // Returned, Canceled
                e.Item.ForeColor = Color.Gray;
            else if ((itm.To ?? now) < now)
                e.Item.ForeColor = Color.Red;
            else
                e.Item.ForeColor = Color.Black;
        }

        /// <summary>
        /// Show returned Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbBorrowingReturned_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBorrowingOLV();
        }

        private void cbBorrowingShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBorrowingOLV();
        }


        private void olvBorrowing_DoubleClick(object sender, EventArgs e)
        {
            EditPersonalLending();
        }

        #endregion

        #region Items

        /// <summary>
        /// Update Items ObjectListView
        /// </summary>
        void UpdateItemsOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Items> itm;
            if (chbShowExcluded.Checked)
                itm = db.Items.ToList();
            else
                itm = db.Items.Where(p => !(p.Excluded ?? false)).ToList();

            itFastTags.Renderer = new ImageRenderer();
            itFastTags.AspectGetter = delegate (object x) {
                List<int> ret = new List<int>();
                FastFlags flag = (FastFlags)((Items)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) ret.Add(0);
                if (flag.HasFlag(FastFlags.FLAG2)) ret.Add(1);
                if (flag.HasFlag(FastFlags.FLAG3)) ret.Add(2);
                if (flag.HasFlag(FastFlags.FLAG4)) ret.Add(3);
                if (flag.HasFlag(FastFlags.FLAG5)) ret.Add(4);
                if (flag.HasFlag(FastFlags.FLAG6)) ret.Add(5);

                return ret;
            };
            itFastTagsNum.AspectGetter = delegate (object x) {
                string res = "";
                FastFlags flag = (FastFlags)((Items)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) res += "1";
                if (flag.HasFlag(FastFlags.FLAG2)) res += "2";
                if (flag.HasFlag(FastFlags.FLAG3)) res += "3";
                if (flag.HasFlag(FastFlags.FLAG4)) res += "4";
                if (flag.HasFlag(FastFlags.FLAG5)) res += "5";
                if (flag.HasFlag(FastFlags.FLAG6)) res += "6";
                return res;
            };

            itName.AspectGetter = delegate (object x) {
                return ((Items)x).Name.Trim();
            };
            itCategory.AspectGetter = delegate (object x) {
                return ((Items)x).Category.Trim();
            };
            itSubcategory.AspectGetter = delegate (object x) {
                return ((Items)x).Subcategory.Trim();
            };
            itInvNum.AspectGetter = delegate (object x) {
                return global.GetInvNumList(((Items)x).ID);
            };
            itLocation.AspectGetter = delegate (object x) {
                return global.GetLocationList(((Items)x).ID);
            };
            itKeywords.AspectGetter = delegate (object x) {
                return ((Items)x).Keywords.Trim();
            };
            itCounts.AspectGetter = delegate (object x) {
                return ((Items)x).Count.ToString();
            };
            itAvailable.AspectGetter = delegate (object x) {
                /*var borr = db.Lending.Where(p => p.ItemID == ((Items)x).Id && p.ItemType.Contains("item") && !(p.Returned ?? false)).ToList();
                int count = ((Items)x).Count ?? 1 - borr.Count;*/
                return ((Items)x).Available ?? (((Items)x).Count ?? 1);
            };
            itExcluded.Renderer = new ImageRenderer();
            itExcluded.AspectGetter = delegate (object x) {
                if (((Items)x).Excluded ?? false)
                    return 7;
                else return 6;
            };

            olvItem.SetObjects(itm);
        }

        /// <summary>
        /// OLV Items selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }


        /// <summary>
        /// Color Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvItem_FormatRow(object sender, FormatRowEventArgs e)
        {
            Items itm = (Items)e.Model;
            if (itm.Available == 0)
                e.Item.ForeColor = Color.Red;
            else
                e.Item.ForeColor = Color.Black;
        }

        /// <summary>
        /// Show Excluded items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbShowExcluded_CheckedChanged(object sender, EventArgs e)
        {
            UpdateItemsOLV();
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

            bkFastTags.Renderer = new ImageRenderer();
            bkFastTags.AspectGetter = delegate (object x) {
                List<int> ret = new List<int>();
                if (((Books)x).FastTags == null) return ret;

                FastFlags flag = (FastFlags)((Books)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) ret.Add(0);
                if (flag.HasFlag(FastFlags.FLAG2)) ret.Add(1);
                if (flag.HasFlag(FastFlags.FLAG3)) ret.Add(2);
                if (flag.HasFlag(FastFlags.FLAG4)) ret.Add(3);
                if (flag.HasFlag(FastFlags.FLAG5)) ret.Add(4);
                if (flag.HasFlag(FastFlags.FLAG6)) ret.Add(5);

                return ret;
            };
            bkFastTagsNum.AspectGetter = delegate (object x) {
                string res = "";
                FastFlags flag = (FastFlags)((Books)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) res += "1";
                if (flag.HasFlag(FastFlags.FLAG2)) res += "2";
                if (flag.HasFlag(FastFlags.FLAG3)) res += "3";
                if (flag.HasFlag(FastFlags.FLAG4)) res += "4";
                if (flag.HasFlag(FastFlags.FLAG5)) res += "5";
                if (flag.HasFlag(FastFlags.FLAG6)) res += "6";
                return res;
            };
            bkName.AspectGetter = delegate (object x) {
                if (((Books)x).Title == null) return "";
                return ((Books)x).Title.Trim();
            };
            bkAuthor.AspectGetter = delegate (object x) {
                if (((Books)x).AuthorSurname == null) return "";
                return ((Books)x).AuthorSurname.Trim() + ", " + ((Books)x).AuthorName.Trim();
            };
            bkCount.AspectGetter = delegate (object x) {
                return ((Books)x).Count.ToString();
            };
            bkAvailable.AspectGetter = delegate (object x) {
                /*var borr = db.Lending.Where(p => p.ItemID == ((Items)x).Id && p.ItemType.Contains("item") && !(p.Returned ?? false)).ToList();
                int count = ((Items)x).Count ?? 1 - borr.Count;*/
                return ((Books)x).Available ?? (((Books)x).Count ?? 1);
            };
            bkType.AspectGetter = delegate (object x) {
                if (((Books)x).Type == null) return "";
                return ((Books)x).Type.Trim();
            };
            bkYear.AspectGetter = delegate (object x) {
                return ((Books)x).Year.ToString();
            };
            bkGenre.AspectGetter = delegate (object x) {
                if (((Books)x).Genre == null) return "";
                return ((Books)x).Genre.Trim();
            };
            bkSubgenre.AspectGetter = delegate (object x) {
                if (((Books)x).SubGenre == null) return "";
                return ((Books)x).SubGenre.Trim();
            };
            bkInvNum.AspectGetter = delegate (object x) {
                return global.GetInvNumList(((Books)x).ID);
            };
            bkLocation.AspectGetter = delegate (object x) {
                return global.GetLocationList(((Books)x).ID);
            };
            bkKeywords.AspectGetter = delegate (object x) {
                if (((Books)x).Keywords == null) return "";
                return ((Books)x).Keywords.Trim();
            };
            bkSeries.AspectGetter = delegate (object x) {
                if (((Books)x).Series == null) return "";
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
        /// Color Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvBooks_FormatRow(object sender, FormatRowEventArgs e)
        {
            Books itm = (Books)e.Model;
            if (itm.Available == 0)
                e.Item.ForeColor = Color.Red;
            else
                e.Item.ForeColor = Color.Black;
        }


        #endregion

        #region Board Games

        /// <summary>
        /// Update Items ObjectListView
        /// </summary>
        void UpdateBoardOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Boardgames> itm;
            if (chbShowExcludedBoard.Checked)
                itm = db.Boardgames.ToList();
            else
                itm = db.Boardgames.Where(p => !(p.Excluded ?? false)).ToList();

            bgTags.Renderer = new ImageRenderer();
            bgTags.AspectGetter = delegate (object x) {
                List<int> ret = new List<int>();
                FastFlags flag = (FastFlags)((Boardgames)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) ret.Add(0);
                if (flag.HasFlag(FastFlags.FLAG2)) ret.Add(1);
                if (flag.HasFlag(FastFlags.FLAG3)) ret.Add(2);
                if (flag.HasFlag(FastFlags.FLAG4)) ret.Add(3);
                if (flag.HasFlag(FastFlags.FLAG5)) ret.Add(4);
                if (flag.HasFlag(FastFlags.FLAG6)) ret.Add(5);

                return ret;
            };
            bgTagsNum.AspectGetter = delegate (object x) {
                string res = "";
                FastFlags flag = (FastFlags)((Boardgames)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) res += "1";
                if (flag.HasFlag(FastFlags.FLAG2)) res += "2";
                if (flag.HasFlag(FastFlags.FLAG3)) res += "3";
                if (flag.HasFlag(FastFlags.FLAG4)) res += "4";
                if (flag.HasFlag(FastFlags.FLAG5)) res += "5";
                if (flag.HasFlag(FastFlags.FLAG6)) res += "6";
                return res;
            };

            bgName.AspectGetter = delegate (object x) {
                return ((Boardgames)x).Name.Trim();
            };
            bgCategory.AspectGetter = delegate (object x) {
                return ((Boardgames)x).Category.Trim();
            };
            bgInvNum.AspectGetter = delegate (object x) {
                return global.GetInvNumList(((Boardgames)x).ID);
            };
            bgLocation.AspectGetter = delegate (object x) {
                return global.GetLocationList(((Boardgames)x).ID);
            };
            bgKeywords.AspectGetter = delegate (object x) {
                return ((Boardgames)x).Keywords.Trim();
            };
            bgCounts.AspectGetter = delegate (object x) {
                return ((Boardgames)x).Count.ToString();
            };
            bgAvailable.AspectGetter = delegate (object x) {
                /*var borr = db.Lending.Where(p => p.ItemID == ((Items)x).Id && p.ItemType.Contains("item") && !(p.Returned ?? false)).ToList();
                int count = ((Items)x).Count ?? 1 - borr.Count;*/
                return ((Boardgames)x).Available ?? (((Boardgames)x).Count ?? 1);
            };
            bgExcluded.Renderer = new ImageRenderer();
            bgExcluded.AspectGetter = delegate (object x) {
                if (((Boardgames)x).Excluded ?? false)
                    return 7;
                else return 6;
            };

            olvBoard.SetObjects(itm);
        }

        /// <summary>
        /// OLV Items selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvBoard_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }


        /// <summary>
        /// Color Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvBoard_FormatRow(object sender, FormatRowEventArgs e)
        {
            Boardgames itm = (Boardgames)e.Model;
            if (itm.Available == 0)
                e.Item.ForeColor = Color.Red;
            else
                e.Item.ForeColor = Color.Black;
        }

        /// <summary>
        /// Show Excluded items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbShowExcludedBoard_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBoardOLV();
        }

        #endregion

        #endregion

        #region Edit Items

        private void EnableEditItems()
        {
            int count = 0;

            if (tabCatalog.SelectedTab == tabContacts)
                count = olvContacts.SelectedObjects.Count;
            else if (tabCatalog.SelectedTab == tabLending)
                count = olvLending.SelectedObjects.Count;
            else if (tabCatalog.SelectedTab == tabBorrowing)
                count = olvBorrowing.SelectedObjects.Count;
            else if (tabCatalog.SelectedTab == tabItems)
                count = olvItem.SelectedObjects.Count;
            else if (tabCatalog.SelectedTab == tabBooks)
                count = olvBooks.SelectedObjects.Count;
            else if (tabCatalog.SelectedTab == tabBoardGames)
                count = olvBoard.SelectedObjects.Count;

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
            if (tabCatalog.SelectedTab == tabContacts)
            {
                frmEditContacts form = new frmEditContacts();
                var res = form.ShowDialog();                    // Show Edit form
                while (res == DialogResult.Yes)                 // If New item request
                {
                    form.Dispose();
                    form = new frmEditContacts();               // New Form
                    res = form.ShowDialog();                    // Show new Edit form
                }
                UpdateConOLV();                                 // Update Contact OLV
            }
            // ----- Lending -----
            else if (tabCatalog.SelectedTab == tabLending)
            {
                frmEditLending form = new frmEditLending();
                var res = form.ShowDialog();                    // Show Edit form
                while (res == DialogResult.Yes)                 // If New item request
                {
                    form.Dispose();
                    form = new frmEditLending();                // New Form
                    res = form.ShowDialog();                    // Show new Edit form
                } 
                UpdateLendingOLV();                             // Update Lending OLV
                UpdateConOLV();                                 // Update Contact OLV
                UpdateAllItemsOLV();
            }
            // ----- Borrowing -----
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                frmEditBorrowing form = new frmEditBorrowing();
                var res = form.ShowDialog();                    // Show Edit form
                while (res == DialogResult.Yes)                 // If New item request
                {
                    form.Dispose();
                    form = new frmEditBorrowing();              // New Form
                    res = form.ShowDialog();                    // Show new Edit form
                }
                UpdateBorrowingOLV();                           // Update Borrowing OLV
                UpdateConOLV();                                 // Update Contact OLV
            }
            // ----- Item -----
            else if (tabCatalog.SelectedTab == tabItems)
            {
                frmEditItem form = new frmEditItem();
                var res = form.ShowDialog();                    // Show Edit form
                while (res == DialogResult.Yes)                 // If New item request
                {
                    form.Dispose();
                    form = new frmEditItem();                   // New Form
                    res = form.ShowDialog();                    // Show new Edit form
                }
                UpdateItemsOLV();                               // Update Items OLV
            }
            // ----- Book -----
            else if (tabCatalog.SelectedTab == tabBooks)
            {
                frmEditBooks form = new frmEditBooks();
                var res = form.ShowDialog();                    // Show Edit form
                while (res == DialogResult.Yes)                 // If New item request
                {
                    form.Dispose();
                    form = new frmEditBooks();                  // New Form
                    res = form.ShowDialog();                    // Show new Edit form
                }
                UpdateBooksOLV();                               // Update Books OLV
            }
            // ----- Boardgames -----
            else if (tabCatalog.SelectedTab == tabBoardGames)
            {
                frmEditBoardGames form = new frmEditBoardGames();
                var res = form.ShowDialog();                    // Show Edit form
                while (res == DialogResult.Yes)                 // If New item request
                {
                    form.Dispose();
                    form = new frmEditBoardGames();             // New Form
                    res = form.ShowDialog();                    // Show new Edit form
                }
                UpdateBoardOLV();                               // Update Items OLV
            }
        }

        /// <summary>
        /// Show Edit Item Form
        /// </summary>
        private void EditItem()
        {
            // ----- Contact -----
            if (tabCatalog.SelectedTab == tabContacts)
            {
                if (olvContacts.SelectedIndex >= 0)                 // If selected Item
                {
                    frmEditContacts form = new frmEditContacts();   // Show Edit form
                    var res = form.ShowDialog(((Contacts)olvContacts.SelectedObject).ID);
                    while (res == DialogResult.Yes)                 // If New item request
                    {
                        form.Dispose();
                        form = new frmEditContacts();               // New Form
                        res = form.ShowDialog();                    // Show new Edit form
                    }
                    UpdateConOLV();                                 // Update Contact OLV
                }
            }
            // ----- Lending -----
            else if (tabCatalog.SelectedTab == tabLending)
            {
                if (olvLending.SelectedIndex >= 0)                // If selected Item
                {
                    frmEditLending form = new frmEditLending(); // Show Edit form
                    List<Guid> gList = new List<Guid>();
                    gList.Add(((Lending)olvLending.SelectedObject).ID);
                    var res = form.ShowDialog(gList);
                    while (res == DialogResult.Yes)                 // If New item request
                    {
                        form.Dispose();
                        form = new frmEditLending();              // New Form
                        res = form.ShowDialog();                    // Show new Edit form
                    }
                    UpdateLendingOLV();                                // Update Lending OLV
                    UpdateConOLV();                                 // Update Contact OLV
                    UpdateAllItemsOLV();
                }
            }
            // ----- Borrowing -----
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                if (olvBorrowing.SelectedIndex >= 0)                // If selected Item
                {
                    frmEditBorrowing form = new frmEditBorrowing(); // Show Edit form
                    List<Guid> gList = new List<Guid>();
                    gList.Add(((Borrowing)olvBorrowing.SelectedObject).ID);
                    var res = form.ShowDialog(gList);
                    while (res == DialogResult.Yes)                 // If New item request
                    {
                        form.Dispose();
                        form = new frmEditBorrowing();              // New Form
                        res = form.ShowDialog();                    // Show new Edit form
                    }
                    UpdateBorrowingOLV();                           // Update Borrowing OLV
                    UpdateConOLV();                                 // Update Contact OLV
                }
            }
            // ----- Item -----
            else if (tabCatalog.SelectedTab == tabItems)
            {
                if (olvItem.SelectedIndex >= 0)                     // If selected Item
                {
                    frmEditItem form = new frmEditItem();           // Show Edit form
                    var res = form.ShowDialog(((Items)olvItem.SelectedObject).ID);
                    while (res == DialogResult.Yes)                 // If New item request
                    {
                        form.Dispose();
                        form = new frmEditItem();                   // New Form
                        res = form.ShowDialog();                    // Show new Edit form
                    }
                    UpdateItemsOLV();                               // Update Items OLV
                }
            }
            // ----- Book -----
            else if (tabCatalog.SelectedTab == tabBooks)
            {
                if (olvBooks.SelectedIndex >= 0)                    // If selected Item
                {
                    frmEditBooks form = new frmEditBooks();         // Show Edit form
                    var res = form.ShowDialog(((Books)olvBooks.SelectedObject).ID);
                    while (res == DialogResult.Yes)                 // If New item request
                    {
                        form.Dispose();
                        form = new frmEditBooks();                  // New Form
                        res = form.ShowDialog();                    // Show new Edit form
                    }
                    UpdateBooksOLV();                               // Update Books OLV
                }
            }
            // ----- Boardgames -----
            else if (tabCatalog.SelectedTab == tabBoardGames)
            {
                if (olvBoard.SelectedIndex >= 0)                     // If selected Item
                {
                    frmEditBoardGames form = new frmEditBoardGames();           // Show Edit form
                    var res = form.ShowDialog(((Boardgames)olvBoard.SelectedObject).ID);
                    while (res == DialogResult.Yes)                 // If New item request
                    {
                        form.Dispose();
                        form = new frmEditBoardGames();                   // New Form
                        res = form.ShowDialog();                    // Show new Edit form
                    }
                    UpdateBoardOLV();                               // Update Items OLV
                }
            }
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItem()
        {
            databaseEntities db = new databaseEntities();

            // ----- Contact -----
            if (tabCatalog.SelectedTab == tabContacts)
            {
                if (olvContacts.SelectedIndex >= 0)                 // If selected Item
                {                                                   // Find Object
                    Contacts contact = db.Contacts.Find(((Contacts)olvContacts.SelectedObject).ID);

                    if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + contact.Name.Trim() + " " + contact.Surname.Trim() + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        db.Contacts.Remove(contact);                // Delete Item
                        db.SaveChanges();                           // Save to DB
                        UpdateConOLV();                             // Update Contacts OLV 
                    }
                } else if (olvContacts.SelectedObjects != null)                 // If selected Item
                {
                    if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items?"), Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        foreach (var item in olvContacts.SelectedObjects) // Find Object
                        {
                            Contacts contact = db.Contacts.Find(((Contacts)item).ID);
                            db.Contacts.Remove(contact);                // Delete Item
                        }
                        db.SaveChanges();                           // Save to DB
                        UpdateConOLV();                             // Update Contacts OLV 
                    }

                }
            }
            // ----- Lending -----
            else if (tabCatalog.SelectedTab == tabLending)
            {
                if (olvLending.SelectedIndex >= 0)                  // If selected Item
                {                                                   // Find Object
                    Lending borr = db.Lending.Find(((Lending)olvLending.SelectedObject).ID);

                    if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + global.GetLendingItemName(borr.CopyType.Trim(), borr.CopyID ?? Guid.Empty) + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        db.Lending.Remove(borr);                    // Delete Item
                        db.SaveChanges();                           // Save to DB
                        UpdateLendingOLV();                         // Update Lending OLV 
                        UpdateAllItemsOLV();
                    }
                }
            }
            // ----- Borrowing -----
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                if (olvBorrowing.SelectedIndex >= 0)                // If selected Item
                {                                                   // Find Object
                    Borrowing borr = db.Borrowing.Find(((Borrowing)olvBorrowing.SelectedObject).ID);

                    if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + borr.Item + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        db.Borrowing.Remove(borr);                  // Delete Item
                        db.SaveChanges();                           // Save to DB
                        UpdateBorrowingOLV();                       // Update Borrowing OLV 
                    }
                }
            }
            // ----- Item -----
            else if (tabCatalog.SelectedTab == tabItems)
            {
                if (olvItem.SelectedIndex >= 0)                     // If selected Item
                {                                                   // Find Object
                    Items itm = db.Items.Find(((Items)olvItem.SelectedObject).ID);

                    if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + itm.Name.Trim() + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        db.Items.Remove(itm);                       // Delete Item

                        // ----- Remove copies -----
                        var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.item.ToString()) && (x.ItemID == ((Items)olvItem.SelectedObject).ID)).ToList();
                        foreach(var copy in copies)
                        {
                            db.Copies.Remove(copy);                 // Remove copy
                        }
                        db.SaveChanges();                           // Save to DB
                        UpdateItemsOLV();                           // Update Items OLV 
                    }
                }
            }
            // ----- Book -----
            else if (tabCatalog.SelectedTab == tabBooks)
            {
                if (olvBooks.SelectedIndex >= 0)                    // If selected Item
                {                                                   // Find Object
                    Books book = db.Books.Find(((Books)olvBooks.SelectedObject).ID);

                    if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + book.Title.Trim() + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        db.Books.Remove(book);                      // Delete Item

                        // ----- Remove copies -----
                        var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.book.ToString()) && (x.ItemID == ((Books)olvBooks.SelectedObject).ID)).ToList();
                        foreach (var copy in copies)
                        {
                            db.Copies.Remove(copy);                 // Remove copy
                        }

                        db.SaveChanges();                           // Save to DB
                        UpdateBooksOLV();                           // Update Books OLV                   
                    }
                }
            }
            // ----- Boardgames -----
            else if (tabCatalog.SelectedTab == tabBoardGames)
            {
                if (olvBoard.SelectedIndex >= 0)                     // If selected Item
                {                                                   // Find Object
                    Boardgames itm = db.Boardgames.Find(((Boardgames)olvBoard.SelectedObject).ID);

                    if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + itm.Name.Trim() + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        db.Boardgames.Remove(itm);                       // Delete Item

                        // ----- Remove copies -----
                        var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.boardgame.ToString()) && (x.ItemID == ((Boardgames)olvBoard.SelectedObject).ID)).ToList();
                        foreach (var copy in copies)
                        {
                            db.Copies.Remove(copy);                 // Remove copy
                        }

                        db.SaveChanges();                           // Save to DB
                        UpdateBoardOLV();                           // Update Items OLV 
                    }
                }
            }
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

        private void EditPersonalLending()
        {
            // ----- Contact -----
            if (tabCatalog.SelectedTab == tabContacts)
            {
                if (olvContacts.SelectedIndex >= 0)                 // If selected Item
                {
                    frmEditPersonLending form = new frmEditPersonLending();   // Show Edit form
                    var res = form.ShowDialog(((Contacts)olvContacts.SelectedObject).ID);
                    UpdateLendingOLV();
                    UpdateAllItemsOLV();
                }
            }
            // ----- Lending -----
            else if (tabCatalog.SelectedTab == tabLending)
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
            else if (tabCatalog.SelectedTab == tabBorrowing)
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
            else if (tabCatalog.SelectedTab == tabLending)
            {
                cbFilterCol.Items.Add(Lng.Get("All"));
                cbFilterCol.Items.Add(Lng.Get("Type"));
                cbFilterCol.Items.Add(Lng.Get("ItemName"));
                cbFilterCol.Items.Add(Lng.Get("Person"));
                cbFilterCol.Items.Add(Lng.Get("From"));
                cbFilterCol.Items.Add(Lng.Get("To"));
                cbFilterCol.Items.Add(Lng.Get("Status"));
                cbFilterCol.Items.Add(Lng.Get("Note"));
                cbFilterCol.SelectedIndex = 0;

                cbFastFilterCol.Items.Add(Lng.Get("All"));
                cbFastFilterCol.Items.Add(Lng.Get("Type"));
                cbFastFilterCol.Items.Add(Lng.Get("ItemName"));
                cbFastFilterCol.Items.Add(Lng.Get("Person"));
                cbFastFilterCol.SelectedIndex = 0;
            }
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                cbFilterCol.Items.Add(Lng.Get("All"));
                cbFilterCol.Items.Add(Lng.Get("ItemName"));
                cbFilterCol.Items.Add(Lng.Get("Person"));
                cbFilterCol.Items.Add(Lng.Get("From"));
                cbFilterCol.Items.Add(Lng.Get("To"));
                cbFilterCol.Items.Add(Lng.Get("Status"));
                cbFilterCol.Items.Add(Lng.Get("Note"));
                cbFilterCol.SelectedIndex = 0;

                cbFastFilterCol.Items.Add(Lng.Get("All"));
                cbFastFilterCol.Items.Add(Lng.Get("ItemName"));
                cbFastFilterCol.Items.Add(Lng.Get("Person"));
                cbFastFilterCol.SelectedIndex = 0;
            }
            else if (tabCatalog.SelectedTab == tabItems)
            {
                cbFilterCol.Items.Add(Lng.Get("All"));
                cbFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
                cbFilterCol.Items.Add(Lng.Get("Category"));
                cbFilterCol.Items.Add(Lng.Get("Subcategory"));
                cbFilterCol.Items.Add(Lng.Get("InvNum", "Inv. Num."));
                cbFilterCol.Items.Add(Lng.Get("Location"));
                cbFilterCol.Items.Add(Lng.Get("Keywords"));
                cbFilterCol.Items.Add(Lng.Get("Counts"));
                cbFilterCol.Items.Add(Lng.Get("Available"));
                cbFilterCol.Items.Add(Lng.Get("Excluded"));
                cbFilterCol.SelectedIndex = 0;

                cbFastFilterCol.Items.Add(Lng.Get("All"));
                cbFastFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
                cbFastFilterCol.Items.Add(Lng.Get("Category"));
                cbFastFilterCol.Items.Add(Lng.Get("Subcategory"));
                cbFastFilterCol.Items.Add(Lng.Get("InvNum", "Inv. Num."));
                cbFastFilterCol.Items.Add(Lng.Get("Location"));
                cbFastFilterCol.Items.Add(Lng.Get("Excluded"));
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
            else if (tabCatalog.SelectedTab == tabBoardGames)
            {
                cbFilterCol.Items.Add(Lng.Get("All"));
                cbFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
                cbFilterCol.Items.Add(Lng.Get("Category"));
                cbFilterCol.Items.Add(Lng.Get("InvNum", "Inv. Num."));
                cbFilterCol.Items.Add(Lng.Get("Location"));
                cbFilterCol.Items.Add(Lng.Get("Keywords"));
                cbFilterCol.Items.Add(Lng.Get("Counts"));
                cbFilterCol.Items.Add(Lng.Get("Available"));
                cbFilterCol.Items.Add(Lng.Get("Excluded"));
                cbFilterCol.SelectedIndex = 0;

                cbFastFilterCol.Items.Add(Lng.Get("All"));
                cbFastFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
                cbFastFilterCol.Items.Add(Lng.Get("Category"));
                cbFastFilterCol.Items.Add(Lng.Get("InvNum", "Inv. Num."));
                cbFastFilterCol.Items.Add(Lng.Get("Location"));
                cbFastFilterCol.Items.Add(Lng.Get("Excluded"));
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

            if (tabCatalog.SelectedTab == tabContacts)
            {
                olvContacts.UseFiltering = true;
                olvContacts.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
            }
            else if (tabCatalog.SelectedTab == tabLending)
            {
                olvLending.UseFiltering = true;
                olvLending.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
            }
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                olvBorrowing.UseFiltering = true;
                olvBorrowing.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
            }
            else if (tabCatalog.SelectedTab == tabItems)
            {
                olvItem.UseFiltering = true;
                olvItem.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
            }
            else if (tabCatalog.SelectedTab == tabBooks)
            {
                olvBooks.UseFiltering = true;
                olvBooks.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
            }
            else if (tabCatalog.SelectedTab == tabBoardGames)
            {
                olvBoard.UseFiltering = true;
                olvBoard.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
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
            else if (tabCatalog.SelectedTab == tabLending)
            {
                if (FastFilterList.Count == 0)
                    FastFilter = TextMatchFilter.Contains(olvLending, "");
                else
                {
                    string[] filterArray = FastFilterList.ToArray();
                    FastFilter = TextMatchFilter.Prefix(olvLending, filterArray);
                }
                if (cbFastFilterCol.SelectedIndex == 0)
                    FastFilter.Columns = new OLVColumn[] { ldItemType, ldItemName, ldPerson};
                else if (cbFastFilterCol.SelectedIndex == 1)
                    FastFilter.Columns = new OLVColumn[] { ldItemType };
                else if (cbFastFilterCol.SelectedIndex == 2)
                    FastFilter.Columns = new OLVColumn[] { ldItemName };
                else if (cbFastFilterCol.SelectedIndex == 3)
                    FastFilter.Columns = new OLVColumn[] { ldPerson };
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
                    FastFilter.Columns = new OLVColumn[] { brItemName, brPerson };
                else if (cbFastFilterCol.SelectedIndex == 1)
                    FastFilter.Columns = new OLVColumn[] { brItemName };
                else if (cbFastFilterCol.SelectedIndex == 2)
                    FastFilter.Columns = new OLVColumn[] { brPerson };
            }
            else if (tabCatalog.SelectedTab == tabItems)
            {
                if (FastFilterList.Count == 0)
                    FastFilter = TextMatchFilter.Contains(olvItem, "");
                else
                {
                    string[] filterArray = FastFilterList.ToArray();
                    FastFilter = TextMatchFilter.Prefix(olvItem, filterArray);
                }
                if (cbFastFilterCol.SelectedIndex == 0)
                    FastFilter.Columns = new OLVColumn[] { itName, itCategory, itSubcategory, itInvNum, itLocation, itExcluded };
                else if (cbFastFilterCol.SelectedIndex == 1)
                    FastFilter.Columns = new OLVColumn[] { itName };
                else if (cbFastFilterCol.SelectedIndex == 2)
                    FastFilter.Columns = new OLVColumn[] { itCategory };
                else if (cbFastFilterCol.SelectedIndex == 3)
                    FastFilter.Columns = new OLVColumn[] { itSubcategory };
                else if (cbFastFilterCol.SelectedIndex == 4)
                    FastFilter.Columns = new OLVColumn[] { itInvNum };
                else if (cbFastFilterCol.SelectedIndex == 5)
                    FastFilter.Columns = new OLVColumn[] { itLocation };
                else if (cbFastFilterCol.SelectedIndex == 6)
                    FastFilter.Columns = new OLVColumn[] { itExcluded };
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
            else if (tabCatalog.SelectedTab == tabBoardGames)
            {
                if (FastFilterList.Count == 0)
                    FastFilter = TextMatchFilter.Contains(olvBoard, "");
                else
                {
                    string[] filterArray = FastFilterList.ToArray();
                    FastFilter = TextMatchFilter.Prefix(olvBoard, filterArray);
                }
                if (cbFastFilterCol.SelectedIndex == 0)
                    FastFilter.Columns = new OLVColumn[] { bgName, bgCategory, bgInvNum, bgLocation, bgExcluded };
                else if (cbFastFilterCol.SelectedIndex == 1)
                    FastFilter.Columns = new OLVColumn[] { bgName };
                else if (cbFastFilterCol.SelectedIndex == 2)
                    FastFilter.Columns = new OLVColumn[] { bgCategory };
                else if (cbFastFilterCol.SelectedIndex == 3)
                    FastFilter.Columns = new OLVColumn[] { bgInvNum };
                else if (cbFastFilterCol.SelectedIndex == 4)
                    FastFilter.Columns = new OLVColumn[] { bgLocation };
                else if (cbFastFilterCol.SelectedIndex == 5)
                    FastFilter.Columns = new OLVColumn[] { bgExcluded };
            }
        }

        /// <summary>
        /// Use Fast Tag Filter
        /// </summary>
        private void UseFastTagFilter()
        {
            if (FastFilterTags != null) FastFilterTags.Columns = null;
            if (tabCatalog.SelectedTab == tabContacts)
            {
                if (FastTagFilterList.Count == 0)
                    FastFilterTags = TextMatchFilter.Contains(olvContacts, "");
                else
                {
                    string[] filterArray = FastTagFilterList.ToArray();
                    FastFilterTags = TextMatchFilter.Contains(olvContacts, filterArray);
                    FastFilterTags.Columns = new OLVColumn[] { conFastTagsNum };
                }
            }
            else if (tabCatalog.SelectedTab == tabLending)
            {
                if (FastTagFilterList.Count == 0)
                    FastFilterTags = TextMatchFilter.Contains(olvLending, "");
                else
                {
                    string[] filterArray = FastTagFilterList.ToArray();
                    FastFilterTags = TextMatchFilter.Contains(olvLending, filterArray);
                    FastFilterTags.Columns = new OLVColumn[] { ldFastTagsNum };
                }
            }
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                if (FastTagFilterList.Count == 0)
                    FastFilterTags = TextMatchFilter.Contains(olvBorrowing, "");
                else
                {
                    string[] filterArray = FastTagFilterList.ToArray();
                    FastFilterTags = TextMatchFilter.Contains(olvBorrowing, filterArray);
                    FastFilterTags.Columns = new OLVColumn[] { brFastTagsNum };
                }
            }
            else if (tabCatalog.SelectedTab == tabItems)
            {
                if (FastTagFilterList.Count == 0)
                    FastFilterTags = TextMatchFilter.Contains(olvItem, "");
                else
                {
                    string[] filterArray = FastTagFilterList.ToArray();
                    FastFilterTags = TextMatchFilter.Contains(olvItem, filterArray);
                    FastFilterTags.Columns = new OLVColumn[] { itFastTagsNum };
                }
                
            }
            else if (tabCatalog.SelectedTab == tabBooks)
            {
                if (FastTagFilterList.Count == 0)
                    FastFilterTags = TextMatchFilter.Contains(olvBooks, "");
                else
                {
                    string[] filterArray = FastTagFilterList.ToArray();
                    FastFilterTags = TextMatchFilter.Contains(olvBooks, filterArray);
                    FastFilterTags.Columns = new OLVColumn[] { bkFastTagsNum };
                }
                
            }
            else if (tabCatalog.SelectedTab == tabBoardGames)
            {
                if (FastTagFilterList.Count == 0)
                    FastFilterTags = TextMatchFilter.Contains(olvBoard, "");
                else
                {
                    string[] filterArray = FastTagFilterList.ToArray();
                    FastFilterTags = TextMatchFilter.Contains(olvBoard, filterArray);
                    FastFilterTags.Columns = new OLVColumn[] { bgTagsNum };
                }

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
            else if (tabCatalog.SelectedTab == tabLending)
            {
                StandardFilter = TextMatchFilter.Contains(olvLending, txtFilter.Text);

                if (cbFilterCol.SelectedIndex == 0)
                    StandardFilter.Columns = new OLVColumn[] { ldItemType, ldItemName, ldPerson, ldFrom, ldTo, ldStatus, ldNote };
                else if (cbFilterCol.SelectedIndex == 1)
                    StandardFilter.Columns = new OLVColumn[] { ldItemType };
                else if (cbFilterCol.SelectedIndex == 2)
                    StandardFilter.Columns = new OLVColumn[] { ldItemName };
                else if (cbFilterCol.SelectedIndex == 3)
                    StandardFilter.Columns = new OLVColumn[] { ldPerson };
                else if (cbFilterCol.SelectedIndex == 4)
                    StandardFilter.Columns = new OLVColumn[] { ldFrom };
                else if (cbFilterCol.SelectedIndex == 5)
                    StandardFilter.Columns = new OLVColumn[] { ldTo };
                else if (cbFilterCol.SelectedIndex == 6)
                    StandardFilter.Columns = new OLVColumn[] { ldStatus };
                else if (cbFilterCol.SelectedIndex == 7)
                    StandardFilter.Columns = new OLVColumn[] { ldNote };
            }
            else if (tabCatalog.SelectedTab == tabBorrowing)
            {
                StandardFilter = TextMatchFilter.Contains(olvBorrowing, txtFilter.Text);

                if (cbFilterCol.SelectedIndex == 0)
                    StandardFilter.Columns = new OLVColumn[] { brItemName, brPerson, brFrom, brTo, brStatus, brNote };
                else if (cbFilterCol.SelectedIndex == 1)
                    StandardFilter.Columns = new OLVColumn[] { brItemName };
                else if (cbFilterCol.SelectedIndex == 2)
                    StandardFilter.Columns = new OLVColumn[] { brPerson };
                else if (cbFilterCol.SelectedIndex == 3)
                    StandardFilter.Columns = new OLVColumn[] { brFrom };
                else if (cbFilterCol.SelectedIndex == 4)
                    StandardFilter.Columns = new OLVColumn[] { brTo };
                else if (cbFilterCol.SelectedIndex == 5)
                    StandardFilter.Columns = new OLVColumn[] { brStatus };
                else if (cbFilterCol.SelectedIndex == 6)
                    StandardFilter.Columns = new OLVColumn[] { brNote };
            }
            else if (tabCatalog.SelectedTab == tabItems)
            {
                StandardFilter = TextMatchFilter.Contains(olvItem, txtFilter.Text);

                if (cbFilterCol.SelectedIndex == 0)
                    StandardFilter.Columns = new OLVColumn[] { itName, itCategory, itSubcategory, itInvNum, itLocation, itKeywords, itCounts, itAvailable, itExcluded };
                else if (cbFilterCol.SelectedIndex == 1)
                    StandardFilter.Columns = new OLVColumn[] { itName };
                else if (cbFilterCol.SelectedIndex == 2)
                    StandardFilter.Columns = new OLVColumn[] { itCategory };
                else if (cbFilterCol.SelectedIndex == 3)
                    StandardFilter.Columns = new OLVColumn[] { itSubcategory };
                else if (cbFilterCol.SelectedIndex == 4)
                    StandardFilter.Columns = new OLVColumn[] { itInvNum };
                else if (cbFilterCol.SelectedIndex == 5)
                    StandardFilter.Columns = new OLVColumn[] { itLocation };
                else if (cbFilterCol.SelectedIndex == 6)
                    StandardFilter.Columns = new OLVColumn[] { itKeywords };
                else if (cbFilterCol.SelectedIndex == 7)
                    StandardFilter.Columns = new OLVColumn[] { itCounts };
                else if (cbFilterCol.SelectedIndex == 8)
                    StandardFilter.Columns = new OLVColumn[] { itAvailable };
                else if (cbFilterCol.SelectedIndex == 9)
                    StandardFilter.Columns = new OLVColumn[] { itExcluded };
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
            else if (tabCatalog.SelectedTab == tabBoardGames)
            {
                StandardFilter = TextMatchFilter.Contains(olvBoard, txtFilter.Text);

                if (cbFilterCol.SelectedIndex == 0)
                    StandardFilter.Columns = new OLVColumn[] { bgName, bgCategory, bgInvNum, bgLocation, bgKeywords, bgCounts, bgAvailable, bgExcluded };
                else if (cbFilterCol.SelectedIndex == 1)
                    StandardFilter.Columns = new OLVColumn[] { bgName };
                else if (cbFilterCol.SelectedIndex == 2)
                    StandardFilter.Columns = new OLVColumn[] { bgCategory };
                else if (cbFilterCol.SelectedIndex == 3)
                    StandardFilter.Columns = new OLVColumn[] { bgInvNum };
                else if (cbFilterCol.SelectedIndex == 4)
                    StandardFilter.Columns = new OLVColumn[] { bgLocation };
                else if (cbFilterCol.SelectedIndex == 5)
                    StandardFilter.Columns = new OLVColumn[] { bgKeywords };
                else if (cbFilterCol.SelectedIndex == 6)
                    StandardFilter.Columns = new OLVColumn[] { bgCounts };
                else if (cbFilterCol.SelectedIndex == 7)
                    StandardFilter.Columns = new OLVColumn[] { bgAvailable };
                else if (cbFilterCol.SelectedIndex == 8)
                    StandardFilter.Columns = new OLVColumn[] { bgExcluded };
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
             
        private void mnuExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV file|*.csv";
            dialog.FileName = "data.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (tabCatalog.SelectedTab == tabContacts)
                {
                    List<Contacts> con = new List<Contacts>();

                    foreach (var item in olvContacts.FilteredObjects)
                    {
                        con.Add((Contacts)item);
                    }
                    global.ExportContactsCSV(dialog.FileName, con);
                }
                else if (tabCatalog.SelectedTab == tabLending)
                {
                    List<Lending> itm = new List<Lending>();

                    foreach (var item in olvLending.FilteredObjects)
                    {
                        itm.Add((Lending)item);
                    }
                    global.ExportLendedCSV(dialog.FileName, itm);
                }
                else if (tabCatalog.SelectedTab == tabBorrowing)
                {
                    List<Borrowing> itm = new List<Borrowing>();

                    foreach (var item in olvBorrowing.FilteredObjects)
                    {
                        itm.Add((Borrowing)item);
                    }
                    global.ExportBorrowingCSV(dialog.FileName, itm);
                }
                else if (tabCatalog.SelectedTab == tabItems)
                {
                    List<Items> itm = new List<Items>();

                    foreach (var item in olvItem.FilteredObjects)
                    {
                        itm.Add((Items)item);
                    }
                    global.ExportItemsCSV(dialog.FileName, itm);
                }
                else if (tabCatalog.SelectedTab == tabBooks)
                {
                    List<Books> itm = new List<Books>();

                    foreach (var item in olvBooks.FilteredObjects)
                    {
                        itm.Add((Books)item);
                    }
                    global.ExportBooksCSV(dialog.FileName, itm);
                }
                else if (tabCatalog.SelectedTab == tabBoardGames)
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

                if (tabCatalog.SelectedTab == tabContacts)
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
                else if (tabCatalog.SelectedTab == tabLending)
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
                else if (tabCatalog.SelectedTab == tabBorrowing)
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
                else if (tabCatalog.SelectedTab == tabItems)
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
                else if (tabCatalog.SelectedTab == tabBooks)
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
                else if (tabCatalog.SelectedTab == tabBoardGames)
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
            tabCatalog.TabPages.Remove(tabAudio);
            tabCatalog.TabPages.Remove(tabVideo);
            tabCatalog.TabPages.Remove(tabFoto);

            PrepareForm();

            UpdateConOLV();
            UpdateLendingOLV();
            UpdateBorrowingOLV();
            UpdateItemsOLV();
            UpdateBooksOLV();
            UpdateBoardOLV();
            EnableEditItems();
            UpdateFilterComboBox();
            CheckMaxInvNums();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();

            if (tabCatalog.SelectedTab == tabContacts)
            {
                List<Contacts> con = global.ImportContactsGoogle();
                if (con == null)
                {
                    Dialogs.ShowErr(Lng.Get("GoogleImportError", "Google import Error") + ".", Lng.Get("Error"));
                    return;
                }

                int x = 0;
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
                        x++;
                        item.ID = Guid.NewGuid();
                        db.Contacts.Add(item);
                        db.SaveChanges();
                    }
                }
                db.SaveChanges();
                UpdateConOLV();
                Dialogs.ShowInfo(Lng.Get("SuccesfullyImport", "Import was succesfully done") + ".", Lng.Get("Import"));
            }
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

        private void olvContacts_FormatRow(object sender, FormatRowEventArgs e)
        {

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
                    tabCatalog.TabPages.Remove(tabContacts);
                else
                    tabCatalog.TabPages.Insert(0, tabContacts);
            }
            // ----- Tab Lending -----
            else if (((ToolStripMenuItem)sender).Tag == "Lending")
            {
                if (((ToolStripMenuItem)sender).Checked == false)
                    tabCatalog.TabPages.Remove(tabLending);
                else
                    tabCatalog.TabPages.Insert(FindTabPosition(tabLending), tabLending);
            }
            // ----- Tab Borrowing -----
            else if (((ToolStripMenuItem)sender).Tag == "Borrowing")
            {
                if (((ToolStripMenuItem)sender).Checked == false)
                    tabCatalog.TabPages.Remove(tabBorrowing);
                else
                    tabCatalog.TabPages.Insert(FindTabPosition(tabBorrowing), tabBorrowing);
            }
            // ----- Tab Items -----
            else if (((ToolStripMenuItem)sender).Tag == "Items")
            {
                if (((ToolStripMenuItem)sender).Checked == false)
                    tabCatalog.TabPages.Remove(tabItems);
                else
                    tabCatalog.TabPages.Insert(FindTabPosition(tabItems), tabItems);
            }
            // ----- Tab Books -----
            else if (((ToolStripMenuItem)sender).Tag == "Books")
            {
                if (((ToolStripMenuItem)sender).Checked == false)
                    tabCatalog.TabPages.Remove(tabBooks);
                else
                    tabCatalog.TabPages.Insert(FindTabPosition(tabBooks), tabBooks);
            }
            // ----- Tab Boardgames -----
            else if (((ToolStripMenuItem)sender).Tag == "Boardgames")
            {
                if (((ToolStripMenuItem)sender).Checked == false)
                    tabCatalog.TabPages.Remove(tabBoardGames);
                else
                    tabCatalog.TabPages.Insert(FindTabPosition(tabBoardGames), tabBoardGames);
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

    }
}
