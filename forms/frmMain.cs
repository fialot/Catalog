﻿using BrightIdeasSoftware;
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
                switch (((Lending)x).ItemType.Trim())
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
                return global.GetLendingItemName(((Lending)x).ItemType.Trim(), ((Lending)x).ItemID ?? Guid.Empty);
            };
            ldItemNum.AspectGetter = delegate (object x) {
                return ((Lending)x).ItemNum;
            };
            ldItemInvNum.AspectGetter = delegate (object x) {
                if (((Lending)x).ItemInvNum != null)
                    return ((Lending)x).ItemInvNum.Trim();
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
                return ((Items)x).InventoryNumber.Trim();
            };
            itLocation.AspectGetter = delegate (object x) {
                return ((Items)x).Location.Trim();
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
                if (((Books)x).InventoryNumber == null) return "";
                return ((Books)x).InventoryNumber.Trim();
            };
            bkLocation.AspectGetter = delegate (object x) {
                if (((Books)x).Location == null) return "";
                return ((Books)x).Location.Trim();
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
                return ((Boardgames)x).InventoryNumber.Trim();
            };
            bgLocation.AspectGetter = delegate (object x) {
                return ((Boardgames)x).Location.Trim();
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
            int index = -1;

            if (tabCatalog.SelectedTab == tabContacts)
                index = olvContacts.SelectedIndex;
            else if (tabCatalog.SelectedTab == tabLending)
                index = olvLending.SelectedIndex;
            else if (tabCatalog.SelectedTab == tabBorrowing)
                index = olvBorrowing.SelectedIndex;
            else if (tabCatalog.SelectedTab == tabItems)
                index = olvItem.SelectedIndex;
            else if (tabCatalog.SelectedTab == tabBooks)
                index = olvBooks.SelectedIndex;
            else if (tabCatalog.SelectedTab == tabBoardGames)
                index = olvBoard.SelectedIndex;

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
                UpdateItemsOLV();                               // Update Items OLV
                UpdateBooksOLV();                               // Update Books OLV
                UpdateBoardOLV();                               // Update Board OLV
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
                    UpdateItemsOLV();                               // Update Items OLV
                    UpdateBooksOLV();                               // Update Books OLV
                    UpdateBoardOLV();                               // Update Board OLV
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
                }
            }
            // ----- Lending -----
            else if (tabCatalog.SelectedTab == tabLending)
            {
                if (olvLending.SelectedIndex >= 0)                  // If selected Item
                {                                                   // Find Object
                    Lending borr = db.Lending.Find(((Lending)olvLending.SelectedObject).ID);

                    if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + global.GetLendingItemName(borr.ItemType.Trim(), borr.ItemID ?? Guid.Empty) + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        db.Lending.Remove(borr);                    // Delete Item
                        db.SaveChanges();                           // Save to DB
                        UpdateLendingOLV();                         // Update Lending OLV 
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
                    StandardFilter.Columns = new OLVColumn[] { bgName, itCategory, itInvNum, itLocation, itKeywords, itCounts, itAvailable, itExcluded };
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
        
        private void ExportConCSV(string path, List<Contacts> con)
        {
            string lines;

            lines = "name;surname;nick;sex;birth;phone;email;www;im;company;position;street;city;region;country;postcode;code;note;groups;tags;GUID" + Environment.NewLine;

            foreach(var item in con)
            {
                lines += item.Name.Trim() + ";" + item.Surname.Trim() + ";" + item.Nick.Trim() + ";" + item.Sex.Trim() + ";" + item.Birth.ToString() + ";" + item.Phone.Trim() + ";" + item.Email.Trim() + ";" + item.WWW.Trim() + ";" + item.IM.Trim() + ";";
                lines += item.Company.Trim() + ";" + item.Position.Trim() + ";" + item.Street.Trim() + ";" + item.City.Trim() + ";" + item.Region.Trim() + ";" + item.Country.Trim() + ";" + item.PostCode.Trim() + ";";
                lines += item.PersonCode.Trim() + ";" + item.Note.Trim() + ";" + item.Tags.Trim() + ";" + item.FastTags.ToString() + ";" + item.ID + Environment.NewLine;
            }

            Files.SaveFile(path, lines);
        }

        private void ExportBorCSV(string path, List<Lending> bor)
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

        private void ExportItmCSV(string path, List<Items> itm)
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
        
        private void ExportBooksCSV(string path, List<Books> book)
        {
            string lines;

            lines = "name;authorName;authorSurname;fasttags;GUID" + Environment.NewLine;

            foreach (var item in book)
            {
                lines += item.Title.Trim() + ";" + item.AuthorName.Trim() + ";" + item.AuthorSurname.Trim() + ";" + item.FastTags.ToString() + ";" + item.ID + Environment.NewLine;
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
                    ExportConCSV(dialog.FileName, con);
                }
                else if (tabCatalog.SelectedTab == tabLending)
                {
                    List<Lending> itm = new List<Lending>();

                    foreach (var item in olvLending.FilteredObjects)
                    {
                        itm.Add((Lending)item);
                    }
                    ExportBorCSV(dialog.FileName, itm);
                }
                else if (tabCatalog.SelectedTab == tabItems)
                {
                    List<Items> itm = new List<Items>();

                    foreach (var item in olvItem.FilteredObjects)
                    {
                        itm.Add((Items)item);
                    }
                    ExportItmCSV(dialog.FileName, itm);
                }
                else if (tabCatalog.SelectedTab == tabBooks)
                {
                    List<Books> itm = new List<Books>();

                    foreach (var item in olvBooks.FilteredObjects)
                    {
                        itm.Add((Books)item);
                    }
                    ExportBooksCSV(dialog.FileName, itm);
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
                contact.ID = Conv.ToGuid(item[20]);
                con.Add(contact);
            }

            return con;
        }

        private List<Lending> ImportBorCSV(string path)
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

        private List<Items> ImportItmCSV(string path)
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

        private List<Books> ImportBookCSV(string path)
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


        private void FillContact(ref Contacts contact, Contacts newItem)
        {
            // ----- Avatar -----
            //contact.Avatar = ImageToByteArray(imgAvatar.Image);

            // ----- Name -----
            contact.Name = newItem.Name;
            contact.Surname = newItem.Surname;
            contact.Nick = newItem.Nick;
            contact.Sex = newItem.Sex;

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
            

            contact.PersonCode = newItem.PersonCode;
            contact.Updated = DateTime.Now;

            contact.Company = newItem.Company;
            contact.Position = newItem.Position;


            // ----- Unused now -----
            /*contact.Partner = "";
            contact.Childs = "";
            contact.Parrents = "";
            contact.GoogleID = "";*/
        }

        private void FillLending(ref Lending itm, Lending newItem)
        {
            itm.ItemType = newItem.ItemType;
            itm.ItemID = newItem.ItemID;
            itm.ItemNum = newItem.ItemNum;
            itm.PersonID = newItem.PersonID;
            itm.From = newItem.From;
            itm.To = newItem.To;
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

            itm.AcquisitionDate = newItem.AcquisitionDate;
            itm.Price = newItem.Price;

            itm.Excluded = newItem.Excluded;
            itm.InventoryNumber = newItem.InventoryNumber;
            itm.Location = newItem.Location;
            itm.FastTags = newItem.FastTags;
        }
        
        private void FillBook(ref Books itm, Books newItem)
        {
            // ----- Avatar -----
            itm.Title = newItem.Title;

            itm.AuthorName = newItem.AuthorName;
            itm.AuthorSurname = newItem.AuthorSurname;

            itm.FastTags = newItem.FastTags;

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
                }
                else if (tabCatalog.SelectedTab == tabLending)
                {
                    List<Lending> con = ImportBorCSV(dialog.FileName);
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
                }
                else if (tabCatalog.SelectedTab == tabItems)
                {
                    List<Items> con = ImportItmCSV(dialog.FileName);
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
                    db.SaveChanges();
                    UpdateItemsOLV();
                }
                else if (tabCatalog.SelectedTab == tabBooks)
                {
                    List<Books> con = ImportBookCSV(dialog.FileName);
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
                    db.SaveChanges();
                    UpdateBooksOLV();
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
            list = db.Items.Select(u => u.InventoryNumber).ToList();
            MaxInvNumbers.Item = GetMaxNum(list);
            list = db.Books.Select(u => u.InventoryNumber).ToList();
            MaxInvNumbers.Book = GetMaxNum(list);
            list = db.Boardgames.Select(u => u.InventoryNumber).ToList();
            MaxInvNumbers.Boardgame = GetMaxNum(list);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
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
            UpdateConOLV();
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
    }
}
