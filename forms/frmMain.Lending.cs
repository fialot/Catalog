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
    public partial class frmMain
    {
        #region OLV
        
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

        #region EditItems

        /// <summary>
        /// New Item
        /// </summary>
        private void NewItemLending()
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

        /// <summary>
        /// Edit Item
        /// </summary>
        private void EditItemLending()
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

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItemLending()
        {
            databaseEntities db = new databaseEntities();
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
            else if (olvLending.SelectedObjects != null)                 // If selected Item
            {
                int count = olvLending.SelectedObjects.Count;
                if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items") + " (" + count.ToString() + ")?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    foreach (var item in olvLending.SelectedObjects) // Find Object
                    {
                        Lending itm = db.Lending.Find(((Lending)item).ID);
                        db.Lending.Remove(itm);                   // Delete Item
                    }
                    db.SaveChanges();                           // Save to DB
                    UpdateLendingOLV();                         // Update Lending OLV 
                    UpdateAllItemsOLV();
                }
            }
        }

        /// <summary>
        /// Set Fast Tags
        /// </summary>
        /// <param name="tag">Tags Mask</param>
        private void SetTagItemLending(short tag)
        {
            if (olvLending.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvLending.SelectedObjects) // Find Object
                {
                    Lending itm = db.Lending.Find(((Lending)item).ID);
                    itm.FastTags |= tag;
                }
                db.SaveChanges();                           // Save to DB
                UpdateLendingOLV();                             // Update Contacts OLV 
            }
        }

        /// <summary>
        /// Set active (excluded)
        /// </summary>
        /// <param name="active"></param>
        private void SetActiveLending(bool active)
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
        private void UpdateCBFilterLending()
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

        /// <summary>
        /// Use Filters
        /// </summary>
        private void UseFiltersLending()
        {
            olvLending.UseFiltering = true;
            olvLending.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
        }

        /// <summary>
        /// Use Fast Filter
        /// </summary>
        private void UseFastFilterLending()
        {
            if (FastFilterList.Count == 0)
                FastFilter = TextMatchFilter.Contains(olvLending, "");
            else
            {
                string[] filterArray = FastFilterList.ToArray();
                FastFilter = TextMatchFilter.Prefix(olvLending, filterArray);
            }
            if (cbFastFilterCol.SelectedIndex == 0)
                FastFilter.Columns = new OLVColumn[] { ldItemType, ldItemName, ldPerson };
            else if (cbFastFilterCol.SelectedIndex == 1)
                FastFilter.Columns = new OLVColumn[] { ldItemType };
            else if (cbFastFilterCol.SelectedIndex == 2)
                FastFilter.Columns = new OLVColumn[] { ldItemName };
            else if (cbFastFilterCol.SelectedIndex == 3)
                FastFilter.Columns = new OLVColumn[] { ldPerson };
        }

        /// <summary>
        /// Use Fast Tag Filter
        /// </summary>
        private void UseFastTagFilterLending()
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

        /// <summary>
        /// Use Standard Filter
        /// </summary>
        private void UseStandardFilterLending()
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

        #endregion
    }
}
