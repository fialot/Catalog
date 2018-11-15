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
        /// Update Contacts ObjectListView
        /// </summary>
        void UpdateRecOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Recipes> rec;

            if (chbShowExcludedRecipes.Checked)
                rec = db.Recipes.ToList();
            else
                rec = db.Recipes.Where(p => (p.Excluded ?? false) == false).ToList();

            recTags.Renderer = new ImageRenderer();
            recTags.AspectGetter = delegate (object x) {
                if (x == null) return "";
                List<int> ret = new List<int>();
                FastFlags flag = (FastFlags)((Recipes)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) ret.Add(0);
                if (flag.HasFlag(FastFlags.FLAG2)) ret.Add(1);
                if (flag.HasFlag(FastFlags.FLAG3)) ret.Add(2);
                if (flag.HasFlag(FastFlags.FLAG4)) ret.Add(3);
                if (flag.HasFlag(FastFlags.FLAG5)) ret.Add(4);
                if (flag.HasFlag(FastFlags.FLAG6)) ret.Add(5);

                return ret;
            };
            recTagsNum.AspectGetter = delegate (object x) {
                if (x == null) return "";
                string res = "";
                FastFlags flag = (FastFlags)((Recipes)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) res += "1";
                if (flag.HasFlag(FastFlags.FLAG2)) res += "2";
                if (flag.HasFlag(FastFlags.FLAG3)) res += "3";
                if (flag.HasFlag(FastFlags.FLAG4)) res += "4";
                if (flag.HasFlag(FastFlags.FLAG5)) res += "5";
                if (flag.HasFlag(FastFlags.FLAG6)) res += "6";
                return res;
            };
            recName.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Recipes)x).Name;
            };
            recCategory.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Recipes)x).Category;
            };
            recKeywords.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Recipes)x).Keywords;
            };
            recExcluded.Renderer = new ImageRenderer();
            recExcluded.AspectGetter = delegate (object x) {
                if (((Recipes)x).Excluded ?? false)
                    return 7;
                else return 6;
            };
            olvRecipes.SetObjects(rec);
        }

        /// <summary>
        /// OLV Select Index changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvRecipes_SelectionChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }

        /// <summary>
        /// OLV Forma Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvRecipes_FormatRow(object sender, FormatRowEventArgs e)
        {
            if (e.Model == null) return;

            Recipes itm = (Recipes)e.Model;
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
        private void chbShowExcludedRecipes_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRecOLV();
        }

        #endregion

        #region EditItems
    
        /// <summary>
        /// New Item
        /// </summary>
        private void NewItemRecipes()
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
        private void EditItemRecipes()
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
        private void DeleteItemRecipes()
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
                        Recipes itm = db.Recipes.Find(((Recipes)olvRecipes.SelectedObject).ID);
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
        private void SetTagItemRecipes(short tag)
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
        private void SetActiveRecipes(bool active)
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
        private void UpdateCBFilterRecipes()
        {
            cbFilterCol.Items.Add(Lng.Get("All"));
            cbFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
            cbFilterCol.Items.Add(Lng.Get("Category"));
            cbFilterCol.Items.Add(Lng.Get("Keywords"));
            cbFilterCol.Items.Add(Lng.Get("Excluded"));
            cbFilterCol.SelectedIndex = 0;

            cbFastFilterCol.Items.Add(Lng.Get("All"));
            cbFastFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
            cbFastFilterCol.Items.Add(Lng.Get("Category"));
            cbFastFilterCol.Items.Add(Lng.Get("Excluded"));
            cbFastFilterCol.SelectedIndex = 0;
        }

        /// <summary>
        /// Use Filters
        /// </summary>
        private void UseFiltersRecipes()
        {
            olvRecipes.UseFiltering = true;
            olvRecipes.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
        }

        /// <summary>
        /// Use Fast Filter
        /// </summary>
        private void UseFastFilterRecipes()
        {
            if (FastFilterList.Count == 0)
                FastFilter = TextMatchFilter.Contains(olvRecipes, "");
            else
            {
                string[] filterArray = FastFilterList.ToArray();
                FastFilter = TextMatchFilter.Prefix(olvRecipes, filterArray);
            }
            if (cbFastFilterCol.SelectedIndex == 0)
                FastFilter.Columns = new OLVColumn[] { recName, recCategory, recExcluded };
            else if (cbFastFilterCol.SelectedIndex == 1)
                FastFilter.Columns = new OLVColumn[] { recName };
            else if (cbFastFilterCol.SelectedIndex == 2)
                FastFilter.Columns = new OLVColumn[] { recCategory };
            else if (cbFastFilterCol.SelectedIndex == 3)
                FastFilter.Columns = new OLVColumn[] { recExcluded };
        }

        /// <summary>
        /// Use Fast Tag Filter
        /// </summary>
        private void UseFastTagFilterRecipes()
        {
            if (FastTagFilterList.Count == 0)
                FastFilterTags = TextMatchFilter.Contains(olvRecipes, "");
            else
            {
                string[] filterArray = FastTagFilterList.ToArray();
                FastFilterTags = TextMatchFilter.Contains(olvRecipes, filterArray);
                FastFilterTags.Columns = new OLVColumn[] { recTagsNum };
            }
        }

        /// <summary>
        /// Use Standard Filter
        /// </summary>
        private void UseStandardFilterRecipes()
        {
            StandardFilter = TextMatchFilter.Contains(olvRecipes, txtFilter.Text);

            if (cbFilterCol.SelectedIndex == 0)
                StandardFilter.Columns = new OLVColumn[] { recName, recCategory, recKeywords, recExcluded };
            else if (cbFilterCol.SelectedIndex == 1)
                StandardFilter.Columns = new OLVColumn[] { recName };
            else if (cbFilterCol.SelectedIndex == 2)
                StandardFilter.Columns = new OLVColumn[] { recCategory };
            else if (cbFilterCol.SelectedIndex == 3)
                StandardFilter.Columns = new OLVColumn[] { recKeywords };
            else if (cbFilterCol.SelectedIndex == 4)
                StandardFilter.Columns = new OLVColumn[] { recExcluded };
        }

        #endregion
    }
}
