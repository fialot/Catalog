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
        private void olvBooks_SelectionChanged(object sender, EventArgs e)
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

        #region EditItems

        /// <summary>
        /// New Item
        /// </summary>
        private void NewItemBooks()
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
            UpdateCopOLV();                                 // Update Copies OLV
        }

        /// <summary>
        /// Edit Item
        /// </summary>
        private void EditItemBooks()
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
                UpdateCopOLV();                                 // Update Copies OLV
            }
        }

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItemBooks()
        {
            databaseEntities db = new databaseEntities();
            int count = olvBooks.SelectedObjects.Count;
            if (count == 1)                    // If selected Item
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
                    UpdateCopOLV();                                 // Update Copies OLV
                }
            }
            else if (count > 1)                 // If selected Item
            {
                
                if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items") + " (" + count.ToString() + ")?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    foreach (var item in olvBooks.SelectedObjects) // Find Object
                    {
                        Books itm = db.Books.Find(((Books)item).ID);
                        db.Books.Remove(itm);                   // Delete Item

                        // ----- Remove copies -----
                        var copies = db.Copies.Where(x => (x.ItemType.Trim() == ItemTypes.book.ToString()) && (x.ItemID == ((Books)item).ID)).ToList();
                        foreach (var copy in copies)
                        {
                            db.Copies.Remove(copy);                 // Remove copy
                        }
                    }
                    db.SaveChanges();                           // Save to DB
                    UpdateBooksOLV();                           // Update Items OLV 
                    UpdateCopOLV();                                 // Update Copies OLV
                }
            }
        }

        /// <summary>
        /// Set Fast Tags
        /// </summary>
        /// <param name="tag">Tags Mask</param>
        private void SetTagItemBooks(short tag)
        {
            if (olvBooks.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvBooks.SelectedObjects) // Find Object
                {
                    Books itm = db.Books.Find(((Books)item).ID);
                    itm.FastTags |= tag;
                }
                db.SaveChanges();                           // Save to DB
                UpdateBooksOLV();                             // Update Contacts OLV 
            }
        }

        /// <summary>
        /// Set active (excluded)
        /// </summary>
        /// <param name="active"></param>
        private void SetActiveBooks(bool active)
        {
            if (olvBooks.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvBooks.SelectedObjects) // Find Object
                {
                    Books itm = db.Books.Find(((Books)item).ID);
                    itm.Excluded = !active;
                }
                db.SaveChanges();                           // Save to DB
                UpdateBooksOLV();                             // Update Contacts OLV 
            }
        }

        #endregion

        #region Filter

        /// <summary>
        /// Update Filter ComboBox
        /// </summary>
        private void UpdateCBFilterBooks()
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

        /// <summary>
        /// Use Filters
        /// </summary>
        private void UseFiltersBooks()
        {
            olvBooks.UseFiltering = true;
            olvBooks.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
        }

        /// <summary>
        /// Use Fast Filter
        /// </summary>
        private void UseFastFilterBooks()
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

        /// <summary>
        /// Use Fast Tag Filter
        /// </summary>
        private void UseFastTagFilterBooks()
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

        /// <summary>
        /// Use Standard Filter
        /// </summary>
        private void UseStandardFilterBooks()
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

        #endregion

        #region Import

        private void ImportBooks(string fileName)
        {
            List<Books> con;
            List<Copies> copies;
            if (Path.GetExtension(fileName) == "csv")
                con = global.ImportBooksCSV(fileName, out copies);
            else
                con = global.ImportBooksXML(fileName, out copies);
            if (con == null)
            {
                Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                return;
            }

            databaseEntities db = new databaseEntities();

            foreach (var item in con)
            {


                Books itm;
                // ----- ID -----
                if (item.ID != Guid.Empty)
                {
                    itm = db.Books.Find(item.ID);
                    if (itm != null)
                        Conv.CopyClassPropetries(itm, item);
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

        private void ExportBooks(string fileName)
        {
            List<Books> itm = new List<Books>();

            foreach (var item in olvBooks.FilteredObjects)
            {
                itm.Add((Books)item);
            }
            if (Path.GetExtension(fileName) == "csv")
                global.ExportBooksCSV(fileName, itm);
            else
                global.ExportBooksXML(fileName, itm);
        }

        #endregion
    }
}
