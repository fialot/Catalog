using BrightIdeasSoftware;
using myFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Katalog
{
    public partial class frmMain
    {
        #region OLV

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
        private void olvBorrowing_SelectionChanged(object sender, EventArgs e)
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

        #region EditItems

        /// <summary>
        /// New Item
        /// </summary>
        private void NewItemBorrowing()
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

        /// <summary>
        /// Edit Item
        /// </summary>
        private void EditItemBorrowing()
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

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItemBorrowing()
        {
            databaseEntities db = new databaseEntities();
            int count = olvBorrowing.SelectedObjects.Count;
            if (count == 1)                // If selected Item
            {                                                   // Find Object
                Borrowing borr = db.Borrowing.Find(((Borrowing)olvBorrowing.SelectedObject).ID);

                if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + borr.Item + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    db.Borrowing.Remove(borr);                  // Delete Item
                    db.SaveChanges();                           // Save to DB
                    UpdateBorrowingOLV();                       // Update Borrowing OLV 
                }
            }
            else if (count > 1)                 // If selected Item
            {
                
                if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items") + " (" + count.ToString() + ")?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    foreach (var item in olvBorrowing.SelectedObjects) // Find Object
                    {
                        Borrowing itm = db.Borrowing.Find(((Borrowing)item).ID);
                        db.Borrowing.Remove(itm);                   // Delete Item
                    }
                    db.SaveChanges();                           // Save to DB
                    UpdateBorrowingOLV();                       // Update Borrowing OLV 
                }
            }
        }

        /// <summary>
        /// Set Fast Tags
        /// </summary>
        /// <param name="tag">Tags Mask</param>
        private void SetTagItemBorrowing(short tag)
        {
            if (olvBorrowing.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvBorrowing.SelectedObjects) // Find Object
                {
                    Borrowing itm = db.Borrowing.Find(((Borrowing)item).ID);
                    itm.FastTags |= tag;
                }
                db.SaveChanges();                           // Save to DB
                UpdateBorrowingOLV();                             // Update Contacts OLV 
            }
        }

        /// <summary>
        /// Set active (excluded)
        /// </summary>
        /// <param name="active"></param>
        private void SetActiveBorrowing(bool active)
        {
            /*if (olvLending.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvLending.SelectedObjects) // Find Object
                {
                    Lending itm = db.Lending.Find(((Lending)item).ID);
                    itm. = !active;
                }
                db.SaveChanges();                           // Save to DB
                UpdateLendingOLV();                             // Update Contacts OLV 
            }*/
        }

        #endregion

        #region Filter

        /// <summary>
        /// Update Filter ComboBox
        /// </summary>
        private void UpdateCBFilterBorrowing()
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

        /// <summary>
        /// Use Filters
        /// </summary>
        private void UseFiltersBorrowing()
        {
            olvBorrowing.UseFiltering = true;
            olvBorrowing.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
        }

        /// <summary>
        /// Use Fast Filter
        /// </summary>
        private void UseFastFilterBorrowing()
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

        /// <summary>
        /// Use Fast Tag Filter
        /// </summary>
        private void UseFastTagFilterBorrowing()
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

        /// <summary>
        /// Use Standard Filter
        /// </summary>
        private void UseStandardFilterBorrowing()
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

        #endregion

        #region Import / Export

        private void ImportBorrowing(string fileName)
        {
            List<Borrowing> con;
            if (Path.GetExtension(fileName) == "csv")
                con = global.ImportBorowingCSV(fileName);
            else
                con = global.ImportBorrowingXML(fileName);
            if (con == null)
            {
                Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                return;
            }

            databaseEntities db = new databaseEntities();

            foreach (var item in con)
            {
                Borrowing itm;
                // ----- ID -----
                if (item.ID != Guid.Empty)
                {
                    itm = db.Borrowing.Find(item.ID);
                    if (itm != null)
                        Conv.CopyClassPropetries(itm, item);
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

        private void ExportBorrowing(string fileName)
        {
            List<Borrowing> itm = new List<Borrowing>();

            foreach (var item in olvBorrowing.FilteredObjects)
            {
                itm.Add((Borrowing)item);
            }
            if (Path.GetExtension(fileName) == "csv")
                global.ExportBorrowingCSV(fileName, itm);
            else
                global.ExportBorrowingXML(fileName, itm);
            
        }

        #endregion
    }
}
