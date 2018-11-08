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
        
        #region EditItems

        /// <summary>
        /// New Item
        /// </summary>
        private void NewItemBoard()
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

        /// <summary>
        /// Edit Item
        /// </summary>
        private void EditItemBoard()
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

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItemBoard()
        {
            databaseEntities db = new databaseEntities();
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
            else if (olvBoard.SelectedObjects != null)                 // If selected Item
            {
                int count = olvBoard.SelectedObjects.Count;
                if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items") + " (" + count.ToString() + ")?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    foreach (var item in olvBoard.SelectedObjects) // Find Object
                    {
                        Boardgames itm = db.Boardgames.Find(((Boardgames)item).ID);
                        db.Boardgames.Remove(itm);                   // Delete Item

                        // ----- Remove copies -----
                        var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.boardgame.ToString()) && (x.ItemID == ((Boardgames)item).ID)).ToList();
                        foreach (var copy in copies)
                        {
                            db.Copies.Remove(copy);                 // Remove copy
                        }
                    }
                    db.SaveChanges();                           // Save to DB
                    UpdateBoardOLV();                           // Update Items OLV 
                }
            }
        }

        /// <summary>
        /// Set Fast Tags
        /// </summary>
        /// <param name="tag">Tags Mask</param>
        private void SetTagItemBoard(short tag)
        {
            if (olvBoard.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();
                foreach (var item in olvBoard.SelectedObjects) // Find Object
                {
                    Boardgames itm = db.Boardgames.Find(((Boardgames)item).ID);
                    itm.FastTags |= tag;
                }
                db.SaveChanges();                           // Save to DB
                UpdateBoardOLV();                             // Update Contacts OLV 

            }
        }

        /// <summary>
        /// Set active (excluded)
        /// </summary>
        /// <param name="active"></param>
        private void SetActiveBoard(bool active)
        {
            if (olvBoard.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvBoard.SelectedObjects) // Find Object
                {
                    Boardgames itm = db.Boardgames.Find(((Boardgames)item).ID);
                    itm.Excluded = !active;
                }
                db.SaveChanges();                           // Save to DB
                UpdateBoardOLV();                             // Update Contacts OLV 
            }
        }

        #endregion

        #region Filter

        /// <summary>
        /// Update Filter ComboBox
        /// </summary>
        private void UpdateCBFilterBoard()
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

        /// <summary>
        /// Use Filters
        /// </summary>
        private void UseFiltersBoard()
        {
            olvBoard.UseFiltering = true;
            olvBoard.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
        }

        /// <summary>
        /// Use Fast Filter
        /// </summary>
        private void UseFastFilterBoard()
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

        /// <summary>
        /// Use Fast Tag Filter
        /// </summary>
        private void UseFastTagFilterBoard()
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

        /// <summary>
        /// Use Standard Filter
        /// </summary>
        private void UseStandardFilterBoard()
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

        #endregion
    }
}
