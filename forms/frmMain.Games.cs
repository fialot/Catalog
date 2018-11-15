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
        void UpdateGameOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Games> gam;

            if (chbShowExcludedGames.Checked)
                gam = db.Games.ToList();
            else
                gam = db.Games.Where(p => (p.Excluded ?? false) == false).ToList();

            gmTags.Renderer = new ImageRenderer();
            gmTags.AspectGetter = delegate (object x) {
                if (x == null) return "";
                List<int> ret = new List<int>();
                FastFlags flag = (FastFlags)((Games)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) ret.Add(0);
                if (flag.HasFlag(FastFlags.FLAG2)) ret.Add(1);
                if (flag.HasFlag(FastFlags.FLAG3)) ret.Add(2);
                if (flag.HasFlag(FastFlags.FLAG4)) ret.Add(3);
                if (flag.HasFlag(FastFlags.FLAG5)) ret.Add(4);
                if (flag.HasFlag(FastFlags.FLAG6)) ret.Add(5);

                return ret;
            };
            gmTagsNum.AspectGetter = delegate (object x) {
                if (x == null) return "";
                string res = "";
                FastFlags flag = (FastFlags)((Games)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) res += "1";
                if (flag.HasFlag(FastFlags.FLAG2)) res += "2";
                if (flag.HasFlag(FastFlags.FLAG3)) res += "3";
                if (flag.HasFlag(FastFlags.FLAG4)) res += "4";
                if (flag.HasFlag(FastFlags.FLAG5)) res += "5";
                if (flag.HasFlag(FastFlags.FLAG6)) res += "6";
                return res;
            };
            gmName.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Games)x).Name;
            };
            gmCategory.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Games)x).Category;
            };
            gmKeywords.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Games)x).Keywords;
            };
            gmEnviroment.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Games)x).Environment;
            };
            gmMinPlayers.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Games)x).MinPlayers;
            };
            gmMaxPlayers.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Games)x).MaxPlayers;
            };
            gmDuration.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Games)x).Duration;
            };
            gmPlayerAge.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Games)x).PlayerAge;
            };

            gmExcluded.Renderer = new ImageRenderer();
            gmExcluded.AspectGetter = delegate (object x) {
                if (((Games)x).Excluded ?? false)
                    return 7;
                else return 6;
            };
            olvGames.SetObjects(gam);
        }

        /// <summary>
        /// OLV Select Index changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvGames_SelectionChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }

        /// <summary>
        /// OLV Forma Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvGames_FormatRow(object sender, FormatRowEventArgs e)
        {
            if (e.Model == null) return;

            Games itm = (Games)e.Model;
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
        private void chbShowExcludedGames_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGameOLV();
        }

        #endregion

        #region EditItems

        /// <summary>
        /// New Item
        /// </summary>
        private void NewItemGames()
        {
            frmEditGames form = new frmEditGames();
            var res = form.ShowDialog();                    // Show Edit form
            while (res == DialogResult.Yes)                 // If New item request
            {
                form.Dispose();
                form = new frmEditGames();             // New Form
                res = form.ShowDialog();                    // Show new Edit form
            }
            UpdateGameOLV();                               // Update Items OLV
        }

        /// <summary>
        /// Edit Item
        /// </summary>
        private void EditItemGames()
        {
            if (olvGames.SelectedIndex >= 0)                     // If selected Item
            {
                frmEditGames form = new frmEditGames();           // Show Edit form
                var res = form.ShowDialog(((Games)olvGames.SelectedObject).ID);
                while (res == DialogResult.Yes)                 // If New item request
                {
                    form.Dispose();
                    form = new frmEditGames();                   // New Form
                    res = form.ShowDialog();                    // Show new Edit form
                }
                UpdateGameOLV();                               // Update Items OLV
            }
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItemGames()
        {
            databaseEntities db = new databaseEntities();
            int count = olvGames.SelectedObjects.Count;
            if (count == 1)                 // If selected Item
            {                                                   // Find Object
                Games itm = db.Games.Find(((Games)olvGames.SelectedObject).ID);

                if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + itm.Name.Trim() + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    db.Games.Remove(itm);                         // Delete Item
                    db.SaveChanges();                               // Save to DB
                    UpdateGameOLV();                                 // Update Recipes OLV 
                }
            }
            else if (count > 1)                 // If selected Item
            {
                
                if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items") + " (" + count.ToString() + ")?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    foreach (var item in olvGames.SelectedObjects) // Find Object
                    {
                        Games itm = db.Games.Find(((Games)item).ID);
                        db.Games.Remove(itm);                 // Delete Item
                    }
                    db.SaveChanges();                           // Save to DB
                    UpdateGameOLV();                             // Update Contacts OLV 
                }
            }
        }

        /// <summary>
        /// Set Fast Tags
        /// </summary>
        /// <param name="tag">Tags Mask</param>
        private void SetTagItemGames(short tag)
        {
            if (olvGames.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvGames.SelectedObjects) // Find Object
                {
                    Games itm = db.Games.Find(((Games)item).ID);
                    itm.FastTags |= tag;
                }
                db.SaveChanges();                           // Save to DB
                UpdateGameOLV();                             // Update Contacts OLV 
            }
        }

        /// <summary>
        /// Set active (excluded)
        /// </summary>
        /// <param name="active"></param>
        private void SetActiveGames(bool active)
        {
            if (olvGames.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvGames.SelectedObjects) // Find Object
                {
                    Games itm = db.Games.Find(((Games)item).ID);
                    itm.Excluded = !active;
                }
                db.SaveChanges();                           // Save to DB
                UpdateGameOLV();                             // Update Contacts OLV 
            }
        }

        #endregion

        #region Filter

        /// <summary>
        /// Update Filter ComboBox
        /// </summary>
        private void UpdateCBFilterGames()
        {
            cbFilterCol.Items.Add(Lng.Get("All"));
            cbFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
            cbFilterCol.Items.Add(Lng.Get("Category"));
            cbFilterCol.Items.Add(Lng.Get("Keywords"));
            cbFilterCol.Items.Add(Lng.Get("Enviroment"));
            cbFilterCol.Items.Add(Lng.Get("MinPlayers", "Min. players"));
            cbFilterCol.Items.Add(Lng.Get("MaxPlayers", "Max. players"));
            cbFilterCol.Items.Add(Lng.Get("Age"));
            cbFilterCol.Items.Add(Lng.Get("Duration"));
            cbFilterCol.Items.Add(Lng.Get("Excluded"));
            cbFilterCol.SelectedIndex = 0;

            cbFastFilterCol.Items.Add(Lng.Get("All"));
            cbFastFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
            cbFastFilterCol.Items.Add(Lng.Get("Category"));
            cbFastFilterCol.Items.Add(Lng.Get("Enviroment"));
            cbFastFilterCol.Items.Add(Lng.Get("Excluded"));
            cbFastFilterCol.SelectedIndex = 0;
        }

        /// <summary>
        /// Use Filters
        /// </summary>
        private void UseFiltersGames()
        {
            olvGames.UseFiltering = true;
            olvGames.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
        }

        /// <summary>
        /// Use Fast Filter
        /// </summary>
        private void UseFastFilterGames()
        {
            if (FastFilterList.Count == 0)
                FastFilter = TextMatchFilter.Contains(olvGames, "");
            else
            {
                string[] filterArray = FastFilterList.ToArray();
                FastFilter = TextMatchFilter.Prefix(olvGames, filterArray);
            }
            if (cbFastFilterCol.SelectedIndex == 0)
                FastFilter.Columns = new OLVColumn[] { gmName, gmCategory, gmKeywords, gmEnviroment, gmExcluded };
            else if (cbFastFilterCol.SelectedIndex == 1)
                FastFilter.Columns = new OLVColumn[] { gmName };
            else if (cbFastFilterCol.SelectedIndex == 2)
                FastFilter.Columns = new OLVColumn[] { gmCategory };
            else if (cbFastFilterCol.SelectedIndex == 3)
                FastFilter.Columns = new OLVColumn[] { gmEnviroment };
            else if (cbFastFilterCol.SelectedIndex == 4)
                FastFilter.Columns = new OLVColumn[] { gmExcluded };
        }

        /// <summary>
        /// Use Fast Tag Filter
        /// </summary>
        private void UseFastTagFilterGames()
        {
            if (FastTagFilterList.Count == 0)
                FastFilterTags = TextMatchFilter.Contains(olvGames, "");
            else
            {
                string[] filterArray = FastTagFilterList.ToArray();
                FastFilterTags = TextMatchFilter.Contains(olvGames, filterArray);
                FastFilterTags.Columns = new OLVColumn[] { gmTagsNum };
            }
        }

        /// <summary>
        /// Use Standard Filter
        /// </summary>
        private void UseStandardFilterGames()
        {
            StandardFilter = TextMatchFilter.Contains(olvGames, txtFilter.Text);

            if (cbFilterCol.SelectedIndex == 0)
                StandardFilter.Columns = new OLVColumn[] { gmName, gmCategory, gmKeywords, gmEnviroment, gmMinPlayers, gmMaxPlayers, gmPlayerAge, gmDuration, gmExcluded };
            else if (cbFilterCol.SelectedIndex == 1)
                StandardFilter.Columns = new OLVColumn[] { gmName };
            else if (cbFilterCol.SelectedIndex == 2)
                StandardFilter.Columns = new OLVColumn[] { gmCategory };
            else if (cbFilterCol.SelectedIndex == 3)
                StandardFilter.Columns = new OLVColumn[] { gmKeywords };
            else if (cbFilterCol.SelectedIndex == 4)
                StandardFilter.Columns = new OLVColumn[] { gmEnviroment };
            else if (cbFilterCol.SelectedIndex == 5)
                StandardFilter.Columns = new OLVColumn[] { gmMinPlayers };
            else if (cbFilterCol.SelectedIndex == 6)
                StandardFilter.Columns = new OLVColumn[] { gmMaxPlayers };
            else if (cbFilterCol.SelectedIndex == 7)
                StandardFilter.Columns = new OLVColumn[] { gmPlayerAge };
            else if (cbFilterCol.SelectedIndex == 8)
                StandardFilter.Columns = new OLVColumn[] { gmDuration };
            else if (cbFilterCol.SelectedIndex == 9)
                StandardFilter.Columns = new OLVColumn[] { gmExcluded };
        }

        #endregion


        #region Import / Export

        private void ImportGames(string fileName)
        {
            List<Games> con = global.ImportGamesCSV(fileName);
            if (con == null)
            {
                Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                return;
            }

            databaseEntities db = new databaseEntities();

            foreach (var item in con)
            {
                Games itm;
                // ----- ID -----
                if (item.ID != Guid.Empty)
                {
                    itm = db.Games.Find(item.ID);
                    if (itm != null)
                        Conv.CopyClassPropetries(itm, item);
                    else
                    {
                        db.Games.Add(item);
                    }

                }
                else
                {
                    item.ID = Guid.NewGuid();
                    db.Games.Add(item);
                }
            }
            db.SaveChanges();
            UpdateGameOLV();
            Dialogs.ShowInfo(Lng.Get("SuccesfullyImport", "Import was succesfully done") + ".", Lng.Get("Import"));
        }

        private void ExportGames(string fileName)
        {
            List<Games> itm = new List<Games>();

            foreach (var item in olvGames.FilteredObjects)
            {
                itm.Add((Games)item);
            }
            global.ExportGamesCSV(fileName, itm);
        }

        #endregion

    }
}
