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
        /// Update Contacts ObjectListView
        /// </summary>
        void UpdateCopOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Copies> rec;

            if (chbShowExcludedCopies.Checked)
                rec = db.Copies.ToList();
            else
                rec = db.Copies.Where(p => (p.Excluded ?? false) == false).ToList();

            cpName.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return global.GetLendingItemName(((Copies)x).ItemType, ((Copies)x).ItemID ?? Guid.Empty);
            };
            cpType.ImageGetter = delegate (object x) {
                switch (((Copies)x).ItemType.Trim())
                {
                    case "item":
                        return 11;
                    case "book":
                        return 12;
                    case "boardgame":
                        return 13;
                }
                return 14;
            };
            cpType.AspectGetter = delegate (object x) {
                switch (((Copies)x).ItemType.Trim())
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
            cpNum.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Copies)x).ItemNum.ToString();
            };
            cpInvNum.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Copies)x).InventoryNumber;
            };

            cpBarcode.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Copies)x).Barcode.ToString();
            };
            cpCondition.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Copies)x).Condition;
            };
            cpLocation.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Copies)x).Location;
            };
            cpPrice.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Copies)x).Price.ToString();
            };
            cpAcqDate.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Copies)x).AcquisitionDate.ToString();
            };
            cpStatus.ImageGetter = delegate (object x) {
                int status = ((Copies)x).Status ?? 1;
                if (status == 2)        // Returned
                    return 6;
                else if (status == 0)   // Reserved
                    return 9;
                else if (status == 3)   // Canceled
                    return 7;
                else return 10;         // Borrowed
            };
            cpStatus.AspectGetter = delegate (object x) {
                int status = ((Copies)x).Status ?? 1;
                if (status == 2)        // Returned
                    return Lng.Get("Returned");
                else if (status == 0)   // Reserved
                    return Lng.Get("Reserved");
                else if (status == 3)   // Canceled
                    return Lng.Get("Canceled");
                else return Lng.Get("Borrowed"); // Borrowed
            };
            cpExcluded.Renderer = new ImageRenderer();
            cpExcluded.AspectGetter = delegate (object x) {
                if (((Copies)x).Excluded ?? false)
                    return 7;
                else return 6;
            };
            olvCopies.SetObjects(rec);
        }

        /// <summary>
        /// OLV Select Index changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvCopies_SelectionChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }

        /// <summary>
        /// OLV Forma Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvCopies_FormatRow(object sender, FormatRowEventArgs e)
        {
            if (e.Model == null) return;

            Copies itm = (Copies)e.Model;
            if ((itm.Excluded ?? false) == true)
                e.Item.ForeColor = Color.Gray;
            else
                e.Item.ForeColor = Color.Black;
        }

        /// <summary>
        /// Change Excluded CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbShowExcludedCopies_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCopOLV();
        }

        #endregion

        #region EditItems
    
        /// <summary>
        /// New Item
        /// </summary>
        private void NewItemCopies()
        {
            frmEditRecipes form = new frmEditRecipes();
            var res = form.ShowDialog();                    // Show Edit form
            while (res == DialogResult.Yes)                 // If New item request
            {
                form.Dispose();
                form = new frmEditRecipes();             // New Form
                res = form.ShowDialog();                    // Show new Edit form
            }
            UpdateRecOLV();                               // Update Items OLV
        }

        /// <summary>
        /// Edit Item
        /// </summary>
        private void EditItemCopies()
        {
            if (olvRecipes.SelectedIndex >= 0)                     // If selected Item
            {
                frmEditRecipes form = new frmEditRecipes();           // Show Edit form
                var res = form.ShowDialog(((Recipes)olvRecipes.SelectedObject).ID);
                while (res == DialogResult.Yes)                 // If New item request
                {
                    form.Dispose();
                    form = new frmEditRecipes();                   // New Form
                    res = form.ShowDialog();                    // Show new Edit form
                }
                UpdateRecOLV();                               // Update Items OLV
            }
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItemCopies()
        {
            databaseEntities db = new databaseEntities();
            int count = olvRecipes.SelectedObjects.Count;
            if (count == 1)                 // If selected Item
            {                                                   // Find Object
                Recipes itm = db.Recipes.Find(((Recipes)olvRecipes.SelectedObject).ID);

                if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + itm.Name.Trim() + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    db.Recipes.Remove(itm);                         // Delete Item
                    db.SaveChanges();                               // Save to DB
                    UpdateRecOLV();                                 // Update Recipes OLV 
                }
            }
            else if (count > 1)                 // If selected Item
            {
                
                if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items") + " (" + count.ToString() + ")?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    foreach (var item in olvRecipes.SelectedObjects) // Find Object
                    {
                        Recipes itm = db.Recipes.Find(((Recipes)item).ID);
                        db.Recipes.Remove(itm);                 // Delete Item
                    }
                    db.SaveChanges();                           // Save to DB
                    UpdateRecOLV();                             // Update Contacts OLV 
                }
            }
        }

        /// <summary>
        /// Set Fast Tags
        /// </summary>
        /// <param name="tag">Tags Mask</param>
        private void SetTagItemCopies(short tag)
        {
            if (olvRecipes.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvRecipes.SelectedObjects) // Find Object
                {
                    Recipes itm = db.Recipes.Find(((Recipes)item).ID);
                    itm.FastTags |= tag;
                }
                db.SaveChanges();                           // Save to DB
                UpdateRecOLV();                             // Update Contacts OLV 
            }
        }

        /// <summary>
        /// Set active (excluded)
        /// </summary>
        /// <param name="active"></param>
        private void SetActiveCopies(bool active)
        {
            if (olvRecipes.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvRecipes.SelectedObjects) // Find Object
                {
                    Recipes itm = db.Recipes.Find(((Recipes)item).ID);
                    itm.Excluded = !active;
                }
                db.SaveChanges();                           // Save to DB
                UpdateRecOLV();                             // Update Contacts OLV 
            }
        }

        #endregion

        #region Filter

        /// <summary>
        /// Update Filter ComboBox
        /// </summary>
        private void UpdateCBFilterCopies()
        {
            cbFilterCol.Items.Add(Lng.Get("All"));
            cbFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
            cbFilterCol.Items.Add(Lng.Get("Type"));
            cbFilterCol.Items.Add(Lng.Get("InventoryNumber", "Inventory Number"));
            cbFilterCol.Items.Add(Lng.Get("Barcode"));
            cbFilterCol.Items.Add(Lng.Get("Condition"));
            cbFilterCol.Items.Add(Lng.Get("Location"));
            cbFilterCol.Items.Add(Lng.Get("Excluded"));
            cbFilterCol.Items.Add(Lng.Get("Status"));
            cbFilterCol.SelectedIndex = 0;

            cbFastFilterCol.Items.Add(Lng.Get("All"));
            cbFastFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
            cbFastFilterCol.Items.Add(Lng.Get("Type"));
            cbFastFilterCol.Items.Add(Lng.Get("InventoryNumber", "Inventory Number"));
            cbFastFilterCol.Items.Add(Lng.Get("Barcode"));
            cbFastFilterCol.Items.Add(Lng.Get("Condition"));
            cbFastFilterCol.Items.Add(Lng.Get("Location"));
            cbFastFilterCol.Items.Add(Lng.Get("Excluded"));
            cbFastFilterCol.Items.Add(Lng.Get("Status"));
            cbFastFilterCol.SelectedIndex = 0;
        }

        /// <summary>
        /// Use Filters
        /// </summary>
        private void UseFiltersCopies()
        {
            olvCopies.UseFiltering = true;
            olvCopies.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, StandardFilter });
        }

        /// <summary>
        /// Use Fast Filter
        /// </summary>
        private void UseFastFilterCopies()
        {
            if (FastFilterList.Count == 0)
                FastFilter = TextMatchFilter.Contains(olvCopies, "");
            else
            {
                string[] filterArray = FastFilterList.ToArray();
                FastFilter = TextMatchFilter.Prefix(olvCopies, filterArray);
            }
            if (cbFastFilterCol.SelectedIndex == 0)
                FastFilter.Columns = new OLVColumn[] { cpName, cpType, cpInvNum, cpBarcode, cpCondition, cpLocation, cpExcluded, cpStatus };
            else if (cbFastFilterCol.SelectedIndex == 1)
                FastFilter.Columns = new OLVColumn[] { cpName };
            else if (cbFastFilterCol.SelectedIndex == 2)
                FastFilter.Columns = new OLVColumn[] { cpType };
            else if (cbFastFilterCol.SelectedIndex == 3)
                FastFilter.Columns = new OLVColumn[] { cpInvNum };
            else if (cbFastFilterCol.SelectedIndex == 4)
                FastFilter.Columns = new OLVColumn[] { cpBarcode };
            else if (cbFastFilterCol.SelectedIndex == 5)
                FastFilter.Columns = new OLVColumn[] { cpCondition };
            else if (cbFastFilterCol.SelectedIndex == 6)
                FastFilter.Columns = new OLVColumn[] { cpLocation };
            else if (cbFastFilterCol.SelectedIndex == 7)
                FastFilter.Columns = new OLVColumn[] { cpExcluded };
            else if (cbFastFilterCol.SelectedIndex == 8)
                FastFilter.Columns = new OLVColumn[] { cpStatus };

        }


        /// <summary>
        /// Use Standard Filter
        /// </summary>
        private void UseStandardFilterCopies()
        {
            StandardFilter = TextMatchFilter.Contains(olvCopies, txtFilter.Text);

            if (cbFilterCol.SelectedIndex == 0)
                StandardFilter.Columns = new OLVColumn[] { cpName, cpType, cpInvNum, cpBarcode, cpCondition, cpLocation, cpExcluded, cpStatus };
            else if (cbFilterCol.SelectedIndex == 1)
                StandardFilter.Columns = new OLVColumn[] { cpName };
            else if (cbFilterCol.SelectedIndex == 2)
                StandardFilter.Columns = new OLVColumn[] { cpType };
            else if (cbFilterCol.SelectedIndex == 3)
                StandardFilter.Columns = new OLVColumn[] { cpInvNum };
            else if (cbFilterCol.SelectedIndex == 4)
                StandardFilter.Columns = new OLVColumn[] { cpBarcode };
            else if (cbFilterCol.SelectedIndex == 5)
                StandardFilter.Columns = new OLVColumn[] { cpCondition };
            else if (cbFilterCol.SelectedIndex == 6)
                StandardFilter.Columns = new OLVColumn[] { cpLocation };
            else if (cbFilterCol.SelectedIndex == 7)
                StandardFilter.Columns = new OLVColumn[] { cpExcluded };
            else if (cbFilterCol.SelectedIndex == 8)
                StandardFilter.Columns = new OLVColumn[] { cpStatus };
        }

        #endregion

        #region Import / Export

        private void ImportCopies(string fileName)
        {
            List<Recipes> con;

            if (Path.GetExtension(fileName) == "csv")
                con = global.ImportRecipesCSV(fileName);
            else
                con = global.ImportRecipesXML(fileName);
            if (con == null)
            {
                Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                return;
            }

            databaseEntities db = new databaseEntities();

            foreach (var item in con)
            {
                Recipes itm;
                // ----- ID -----
                if (item.ID != Guid.Empty)
                {
                    itm = db.Recipes.Find(item.ID);
                    if (itm != null)
                        Conv.CopyClassPropetries(itm, item);
                    else
                    {
                        db.Recipes.Add(item);
                    }

                }
                else
                {
                    item.ID = Guid.NewGuid();
                    db.Recipes.Add(item);
                }
            }
            db.SaveChanges();
            UpdateRecOLV();
            Dialogs.ShowInfo(Lng.Get("SuccesfullyImport", "Import was succesfully done") + ".", Lng.Get("Import"));
        }

        private void ExportCopies(string fileName)
        {
            List<Recipes> itm = new List<Recipes>();

            foreach (var item in olvRecipes.FilteredObjects)
            {
                itm.Add((Recipes)item);
            }
            if (Path.GetExtension(fileName) == "csv")
                global.ExportRecipesCSV(fileName, itm);
            else
                global.ExportRecipesXML(fileName, itm);
        }

        #endregion
    }
}
