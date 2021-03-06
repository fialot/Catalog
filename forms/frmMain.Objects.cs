﻿using BrightIdeasSoftware;
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
        void UpdateObjOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Objects> obj;

            if (chbShowExcludedObjects.Checked)
            {
                if (cbObjectShow.SelectedIndex == 1) obj = db.Objects.Where(p => p.Parent == null).ToList();
                else if (cbObjectShow.SelectedIndex == 2) obj = db.Objects.Where(p => !(p.IsParent ?? false)).ToList();
                else obj = db.Objects.ToList();
            }
            else
            { 
                if (cbObjectShow.SelectedIndex == 1) obj = db.Objects.Where(p => (p.Active ?? true) && (p.Parent == null)).ToList();
                else if (cbObjectShow.SelectedIndex == 2) obj = db.Objects.Where(p => (p.Active ?? true) && !(p.IsParent ?? false)).ToList();
                else obj = db.Objects.Where(p => p.Active ?? true).ToList();
            }
             
            objTags.Renderer = new ImageRenderer();
            objTags.AspectGetter = delegate (object x) {
                if (x == null) return "";
                List<int> ret = new List<int>();
                FastFlags flag = (FastFlags)((Objects)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) ret.Add(0);
                if (flag.HasFlag(FastFlags.FLAG2)) ret.Add(1);
                if (flag.HasFlag(FastFlags.FLAG3)) ret.Add(2);
                if (flag.HasFlag(FastFlags.FLAG4)) ret.Add(3);
                if (flag.HasFlag(FastFlags.FLAG5)) ret.Add(4);
                if (flag.HasFlag(FastFlags.FLAG6)) ret.Add(5);

                return ret;
            };
            objTagsNum.AspectGetter = delegate (object x) {
                if (x == null) return "";
                string res = "";
                FastFlags flag = (FastFlags)((Objects)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) res += "1";
                if (flag.HasFlag(FastFlags.FLAG2)) res += "2";
                if (flag.HasFlag(FastFlags.FLAG3)) res += "3";
                if (flag.HasFlag(FastFlags.FLAG4)) res += "4";
                if (flag.HasFlag(FastFlags.FLAG5)) res += "5";
                if (flag.HasFlag(FastFlags.FLAG6)) res += "6";
                return res;
            };
            objName.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Objects)x).Name;
            };
            objType.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Objects)x).Type;
            };
            objCategory.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Objects)x).Category;
            };
            objNumber.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Objects)x).ObjectNum;
            };
            objParent.AspectGetter = delegate (object x) {
                if (x == null) return "";
                var parentName = db.Objects.Where(p => p.ID == ((Objects)x).Parent).Select(p => p.Name).ToList();
                if (parentName.Count > 0) return parentName[0];
                return "";
            };
            objKeywords.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Objects)x).Keywords;
            };
            objCustomer.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Objects)x).Customer;
            };
            objDevelopment.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Objects)x).Development;
            };
            objFolder.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Objects)x).Folder;
            };
            objVersion.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Objects)x).Version;
            };
            
            objActive.Renderer = new ImageRenderer();
            objActive.AspectGetter = delegate (object x) {
                if (((Objects)x).Active ?? true)
                    return 6;
                else return 7;
            };
            olvObjects.SetObjects(obj);
        }

        /// <summary>
        /// OLV Select Index changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvObjects_SelectionChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }

        /// <summary>
        /// OLV Forma Row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvObjects_FormatRow(object sender, FormatRowEventArgs e)
        {
            if (e.Model == null) return;

            Objects itm = (Objects)e.Model;
            if ((itm.Active ?? true) == false)
                e.Item.ForeColor = Color.Gray;
            else
                e.Item.ForeColor = Color.Black;
        }

        /// <summary>
        /// Change Excluded CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbShowExcludedObjects_CheckedChanged(object sender, EventArgs e)
        {
            UpdateObjOLV();
        }

        private void cbObjectShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateObjOLV();
        }

        #endregion

        #region EditItems

        /// <summary>
        /// New Item
        /// </summary>
        private void NewItemObjects()
        {
            frmEditObjects form = new frmEditObjects();
            var res = form.ShowDialog();                    // Show Edit form
            while (res == DialogResult.Yes)                 // If New item request
            {
                form.Dispose();
                form = new frmEditObjects();             // New Form
                res = form.ShowDialog();                    // Show new Edit form
            }
            UpdateObjOLV();                               // Update Items OLV
        }

        /// <summary>
        /// Edit Item
        /// </summary>
        private void EditItemObjects()
        {
            if (olvObjects.SelectedIndex >= 0)                     // If selected Item
            {
                frmEditObjects form = new frmEditObjects();           // Show Edit form
                var res = form.ShowDialog(((Objects)olvObjects.SelectedObject).ID);
                while (res == DialogResult.Yes)                 // If New item request
                {
                    form.Dispose();
                    form = new frmEditObjects();                   // New Form
                    res = form.ShowDialog();                    // Show new Edit form
                }
                UpdateObjOLV();                               // Update Items OLV
            }
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItemObjects()
        {
            int count = olvObjects.SelectedObjects.Count;
            databaseEntities db = new databaseEntities();
            if (count == 1)                 // If selected Item
            {                                                   // Find Object
                Objects itm = db.Objects.Find(((Objects)olvObjects.SelectedObject).ID);

                if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + itm.Name.Trim() + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    db.Objects.Remove(itm);                         // Delete Item
                    db.SaveChanges();                               // Save to DB
                    UpdateObjOLV();                                 // Update Recipes OLV 
                }
            }
            else if (count > 1)                 // If selected Item
            {
                
                if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items") + " (" + count.ToString() + ")?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    foreach (var item in olvObjects.SelectedObjects) // Find Object
                    {
                        Objects itm = db.Objects.Find(((Objects)item).ID);
                        db.Objects.Remove(itm);                 // Delete Item
                    }
                    db.SaveChanges();                           // Save to DB
                    UpdateObjOLV();                             // Update Contacts OLV 
                }
            }
        }

        /// <summary>
        /// Set Fast Tags
        /// </summary>
        /// <param name="tag">Tags Mask</param>
        private void SetTagItemObjects(short tag)
        {
            if (olvObjects.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvObjects.SelectedObjects) // Find Object
                {
                    Objects itm = db.Objects.Find(((Objects)item).ID);
                    itm.FastTags |= tag;
                }
                db.SaveChanges();                           // Save to DB
                UpdateObjOLV();                             // Update Contacts OLV 
            }
        }

        /// <summary>
        /// Set active (excluded)
        /// </summary>
        /// <param name="active"></param>
        private void SetActiveObjects(bool active)
        {
            if (olvObjects.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvObjects.SelectedObjects) // Find Object
                {
                    Objects itm = db.Objects.Find(((Objects)item).ID);
                    itm.Active = active;
                }
                db.SaveChanges();                           // Save to DB
                UpdateObjOLV();                             // Update Contacts OLV 
            }
        }

        #endregion

        #region Filter

        /// <summary>
        /// Update Filter ComboBox
        /// </summary>
        private void UpdateCBFilterObjects()
        {
            cbFilterCol.Items.Add(Lng.Get("All"));
            cbFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
            cbFilterCol.Items.Add(Lng.Get("Type"));
            cbFilterCol.Items.Add(Lng.Get("Category"));
            cbFilterCol.Items.Add(Lng.Get("Number"));
            cbFilterCol.Items.Add(Lng.Get("Parrent"));
            cbFilterCol.Items.Add(Lng.Get("Keywords"));
            cbFilterCol.Items.Add(Lng.Get("Customer"));
            cbFilterCol.Items.Add(Lng.Get("Development"));
            cbFilterCol.Items.Add(Lng.Get("Folder"));
            cbFilterCol.Items.Add(Lng.Get("Version"));
            cbFilterCol.Items.Add(Lng.Get("Active"));
            cbFilterCol.SelectedIndex = 0;

            cbFastFilterCol.Items.Add(Lng.Get("All"));
            cbFastFilterCol.Items.Add(Lng.Get("ItemName", "Name"));
            cbFastFilterCol.Items.Add(Lng.Get("Type"));
            cbFastFilterCol.Items.Add(Lng.Get("Category"));
            cbFastFilterCol.Items.Add(Lng.Get("Number"));
            cbFastFilterCol.Items.Add(Lng.Get("Parrent"));
            cbFastFilterCol.Items.Add(Lng.Get("Customer"));
            cbFastFilterCol.Items.Add(Lng.Get("Development"));
            cbFastFilterCol.Items.Add(Lng.Get("Active"));
            cbFastFilterCol.SelectedIndex = 0;
        }

        /// <summary>
        /// Use Filters
        /// </summary>
        private void UseFiltersObjects()
        {
            olvObjects.UseFiltering = true;
            olvObjects.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
        }

        /// <summary>
        /// Use Fast Filter
        /// </summary>
        private void UseFastFilterObjects()
        {
            if (FastFilterList.Count == 0)
                FastFilter = TextMatchFilter.Contains(olvObjects, "");
            else
            {
                string[] filterArray = FastFilterList.ToArray();
                FastFilter = TextMatchFilter.Prefix(olvObjects, filterArray);
            }
            if (cbFastFilterCol.SelectedIndex == 0)
                FastFilter.Columns = new OLVColumn[] { objName, objType, objCategory, objNumber, objParent, objCustomer, objDevelopment, objActive };
            else if (cbFastFilterCol.SelectedIndex == 1)
                FastFilter.Columns = new OLVColumn[] { objName };
            else if (cbFastFilterCol.SelectedIndex == 2)
                FastFilter.Columns = new OLVColumn[] { objType };
            else if (cbFastFilterCol.SelectedIndex == 3)
                FastFilter.Columns = new OLVColumn[] { objCategory };
            else if (cbFastFilterCol.SelectedIndex == 4)
                FastFilter.Columns = new OLVColumn[] { objNumber };
            else if (cbFastFilterCol.SelectedIndex == 5)
                FastFilter.Columns = new OLVColumn[] { objParent };
            else if (cbFastFilterCol.SelectedIndex == 6)
                FastFilter.Columns = new OLVColumn[] { objCustomer };
            else if (cbFastFilterCol.SelectedIndex == 7)
                FastFilter.Columns = new OLVColumn[] { objDevelopment };
            else if (cbFastFilterCol.SelectedIndex == 8)
                FastFilter.Columns = new OLVColumn[] { objActive };
        }

        /// <summary>
        /// Use Fast Tag Filter
        /// </summary>
        private void UseFastTagFilterObjects()
        {
            if (FastTagFilterList.Count == 0)
                FastFilterTags = TextMatchFilter.Contains(olvObjects, "");
            else
            {
                string[] filterArray = FastTagFilterList.ToArray();
                FastFilterTags = TextMatchFilter.Contains(olvObjects, filterArray);
                FastFilterTags.Columns = new OLVColumn[] { objTagsNum };
            }
        }

        /// <summary>
        /// Use Standard Filter
        /// </summary>
        private void UseStandardFilterObjects()
        {
            StandardFilter = TextMatchFilter.Contains(olvObjects, txtFilter.Text);

            if (cbFilterCol.SelectedIndex == 0)
                StandardFilter.Columns = new OLVColumn[] { objName, objType, objCategory, objNumber, objParent, objKeywords, objCustomer, objDevelopment, objFolder, objVersion, objActive };
            else if (cbFilterCol.SelectedIndex == 1)
                StandardFilter.Columns = new OLVColumn[] { objName };
            else if (cbFilterCol.SelectedIndex == 2)
                StandardFilter.Columns = new OLVColumn[] { objType };
            else if (cbFilterCol.SelectedIndex == 3)
                StandardFilter.Columns = new OLVColumn[] { objCategory };
            else if (cbFilterCol.SelectedIndex == 4)
                StandardFilter.Columns = new OLVColumn[] { objNumber };
            else if (cbFilterCol.SelectedIndex == 5)
                StandardFilter.Columns = new OLVColumn[] { objParent };
            else if (cbFilterCol.SelectedIndex == 6)
                StandardFilter.Columns = new OLVColumn[] { objKeywords };
            else if (cbFilterCol.SelectedIndex == 7)
                StandardFilter.Columns = new OLVColumn[] { objCustomer };
            else if (cbFilterCol.SelectedIndex == 8)
                StandardFilter.Columns = new OLVColumn[] { objDevelopment };
            else if (cbFilterCol.SelectedIndex == 9)
                StandardFilter.Columns = new OLVColumn[] { objFolder };
            else if (cbFilterCol.SelectedIndex == 10)
                StandardFilter.Columns = new OLVColumn[] { objVersion };
            else if (cbFilterCol.SelectedIndex == 11)
                StandardFilter.Columns = new OLVColumn[] { objActive };
        }

        #endregion


        #region Import / Export

        private void ImportObjects(string fileName)
        {
            List<Objects> con;

            if (Path.GetExtension(fileName) == "csv")
                con = global.ImportObjectsCSV(fileName);
            else
                con = global.ImportObjectsXML(fileName);

            if (con == null)
            {
                Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                return;
            }

            databaseEntities db = new databaseEntities();

            foreach (var item in con)
            {
                Objects itm;
                // ----- ID -----
                if (item.ID != Guid.Empty)
                {
                    itm = db.Objects.Find(item.ID);
                    if (itm != null)
                        Conv.CopyClassPropetries(itm, item);
                    else
                    {
                        db.Objects.Add(item);
                    }

                }
                else
                {
                    item.ID = Guid.NewGuid();
                    db.Objects.Add(item);
                }
            }
            db.SaveChanges();
            UpdateObjOLV();
            Dialogs.ShowInfo(Lng.Get("SuccesfullyImport", "Import was succesfully done") + ".", Lng.Get("Import"));
        }

        private void ExportObjects(string fileName)
        {
            List<Objects> itm = new List<Objects>();

            foreach (var item in olvObjects.FilteredObjects)
            {
                itm.Add((Objects)item);
            }

            if (Path.GetExtension(fileName) == "csv")
                global.ExportObjectCSV(fileName, itm);
            else
                global.ExportObjectXML(fileName, itm);
        }

        #endregion

    }
}
