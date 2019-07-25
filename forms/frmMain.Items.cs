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
        private void olvItem_SelectionChanged(object sender, EventArgs e)
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

        #region EditItems

        /// <summary>
        /// New Item
        /// </summary>
        private void NewItemItems()
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
            UpdateCopOLV();                                 // Update Copies OLV
        }

        /// <summary>
        /// Edit Item
        /// </summary>
        private void EditItemItems()
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
                UpdateCopOLV();                                 // Update Copies OLV
            }
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItemItems()
        {
            databaseEntities db = new databaseEntities();
            int count = olvItem.SelectedObjects.Count;
            if (count == 1)                 // If selected Item
            {                                                   // Find Object
                Items itm = db.Items.Find(((Items)olvItem.SelectedObject).ID); // Find Object

                if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + itm.Name.Trim() + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    db.Items.Remove(itm);                       // Delete Item

                    // ----- Remove copies -----
                    var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.item.ToString()) && (x.ItemID == ((Items)olvItem.SelectedObject).ID)).ToList();
                    foreach (var copy in copies)
                    {
                        db.Copies.Remove(copy);                 // Remove copy
                    }
                    db.SaveChanges();                           // Save to DB
                    UpdateItemsOLV();                           // Update Items OLV 
                    UpdateCopOLV();                                 // Update Copies OLV
                }
            }
            else if (count > 1)                 // If selected Item
            {
                
                if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items") + " (" + count.ToString() + ")?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    foreach (var item in olvItem.SelectedObjects) // Find Object
                    {
                        Items itm = db.Items.Find(((Items)item).ID);
                        db.Items.Remove(itm);                   // Delete Item

                        // ----- Remove copies -----
                        var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.item.ToString()) && (x.ItemID == ((Items)item).ID)).ToList();
                        foreach (var copy in copies)
                        {
                            db.Copies.Remove(copy);                 // Remove copy
                        }
                    }
                    db.SaveChanges();                           // Save to DB
                    UpdateItemsOLV();                           // Update Items OLV 
                    UpdateCopOLV();                                 // Update Copies OLV
                }
            }
        }

        /// <summary>
        /// Set Fast Tags
        /// </summary>
        /// <param name="tag">Tags Mask</param>
        private void SetTagItemItems(short tag)
        {
            if (olvItem.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvItem.SelectedObjects) // Find Object
                {
                    Items itm = db.Items.Find(((Items)item).ID);
                    itm.FastTags |= tag;
                }
                db.SaveChanges();                           // Save to DB
                UpdateItemsOLV();                             // Update Contacts OLV 
            }
        }

        /// <summary>
        /// Set active (excluded)
        /// </summary>
        /// <param name="active"></param>
        private void SetActiveItems(bool active)
        {
            if (olvItem.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvItem.SelectedObjects) // Find Object
                {
                    Items itm = db.Items.Find(((Items)item).ID);
                    itm.Excluded = !active;
                }
                db.SaveChanges();                           // Save to DB
                UpdateItemsOLV();                             // Update Contacts OLV 
            }
        }

        #endregion

        #region Filter

        /// <summary>
        /// Update Filter ComboBox
        /// </summary>
        private void UpdateCBFilterItems()
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

        /// <summary>
        /// Use Filters
        /// </summary>
        private void UseFiltersItems()
        {
            olvItem.UseFiltering = true;
            olvItem.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
        }

        /// <summary>
        /// Use Fast Filter
        /// </summary>
        private void UseFastFilterItems()
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

        /// <summary>
        /// Use Fast Tag Filter
        /// </summary>
        private void UseFastTagFilterItems()
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

        /// <summary>
        /// Use Standard Filter
        /// </summary>
        private void UseStandardFilterItems()
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

        #endregion

        #region Import

        private void ImportItems(string fileName)
        {
            List<Items> con = global.ImportItemsCSV(fileName, out List<Copies> copies);
            if (con == null)
            {
                Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                return;
            }

            databaseEntities db = new databaseEntities();

            foreach (var item in con)
            {
                Items itm;
                // ----- ID -----
                if (item.ID != Guid.Empty)
                {
                    itm = db.Items.Find(item.ID);
                    if (itm != null)
                        Conv.CopyClassPropetries(itm, item);
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

        private void ExportItems(string fileName)
        {
            List<Items> itm = new List<Items>();

            foreach (var item in olvItem.FilteredObjects)
            {
                itm.Add((Items)item);
            }
            global.ExportItemsCSV(fileName, itm);
        }

        #endregion
    }
}
