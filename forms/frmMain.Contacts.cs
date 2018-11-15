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

        string GetOnlyValue(string text)
        {
            string res = "";
            string[] items = text.Split(new string[] { ";" }, StringSplitOptions.None);
            for (int i = 0; i < items.Length; i++)
            {
                string[] vals = items[i].Split(new string[] { "," }, StringSplitOptions.None);
                if (res != "") res += ", ";
                res += vals[0];
            }
            return res;
        }

        /// <summary>
        /// Update Contacts ObjectListView
        /// </summary>
        void UpdateConOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Contacts> con;

            if (chbShowUnactivCon.Checked)
                con = db.Contacts.ToList();
            else
                con = db.Contacts.Where(p => (p.Active ?? true) == true).ToList();

            conFastTags.Renderer = new ImageRenderer();
            conFastTags.AspectGetter = delegate (object x) {
                if (x == null) return "";
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
                if (x == null) return "";
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
                if (x == null) return "";
                return ((Contacts)x).Name;
            };
            conSurname.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Contacts)x).Surname;
            };
            conNick.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Contacts)x).Nick;
            };
            conPhone.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return GetOnlyValue(((Contacts)x).Phone);
            };
            conEmail.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return GetOnlyValue(((Contacts)x).Email);
            };
            conAddress.AspectGetter = delegate (object x) {
                if (x == null) return "";
                string address = ((Contacts)x).Street;
                string city = ((Contacts)x).City;
                string country = ((Contacts)x).Country;

                if ((city != null && city != "") && (address != null && address != ""))
                    address += ", ";
                address += city;

                if ((country != null && country != "") && (address != null && address != ""))
                    address += ", ";
                address += country;
                return address;
            };
            conCompany.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Contacts)x).Company;
            };
            olvContacts.SetObjects(con);
        }

        /// <summary>
        /// OLV Contacts selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvContacts_SelectionChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }


        private void olvContacts_FormatRow(object sender, FormatRowEventArgs e)
        {
            if (e.Model == null) return;

            Contacts itm = (Contacts)e.Model;
            DateTime now = DateTime.Now;
            if ((itm.Active ?? true) == false)
                e.Item.ForeColor = Color.Gray;
            else
                e.Item.ForeColor = Color.Black;
        }

        private void chbUnactivateContacts_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConOLV();
        }

        #endregion
        
        #region EditItems

        /// <summary>
        /// New Item
        /// </summary>
        private void NewItemContacts()
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

        /// <summary>
        /// Edit Item
        /// </summary>
        private void EditItemContacts()
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

        /// <summary>
        /// Delete Item
        /// </summary>
        private void DeleteItemContacts()
        {
            databaseEntities db = new databaseEntities();
            int count = olvContacts.SelectedObjects.Count;
            if (count == 1)                 // If selected Item
            {                                                   // Find Object
                Contacts contact = db.Contacts.Find(((Contacts)olvContacts.SelectedObject).ID);

                if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + contact.Name.Trim() + " " + contact.Surname.Trim() + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    db.Contacts.Remove(contact);                // Delete Item
                    db.SaveChanges();                           // Save to DB
                    UpdateConOLV();                             // Update Contacts OLV 
                }
            }
            else if (count > 1)                 // If selected Item
            {
                
                if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items") + " (" + count.ToString() +  ")?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    foreach (var item in olvContacts.SelectedObjects) // Find Object
                    {
                        Contacts contact = db.Contacts.Find(((Contacts)item).ID);
                        db.Contacts.Remove(contact);                // Delete Item
                    }
                    db.SaveChanges();                           // Save to DB
                    UpdateConOLV();                             // Update Contacts OLV 
                }

            }
        }

        /// <summary>
        /// Set Fast Tags
        /// </summary>
        /// <param name="tag">Tags Mask</param>
        private void SetTagItemContacts(short tag)
        {
            if (olvContacts.SelectedObjects != null)                 // If selected Item
            {

                databaseEntities db = new databaseEntities();
                foreach (var item in olvContacts.SelectedObjects) // Find Object
                {
                    Contacts contact = db.Contacts.Find(((Contacts)item).ID);
                    contact.FastTags |= tag;
                }
                db.SaveChanges();                           // Save to DB
                UpdateConOLV();                             // Update Contacts OLV 
            }
        }

        /// <summary>
        /// Set active (excluded)
        /// </summary>
        /// <param name="active"></param>
        private void SetActiveContacts(bool active)
        {
            if (olvContacts.SelectedObjects != null)                 // If selected Item
            {
                databaseEntities db = new databaseEntities();

                foreach (var item in olvContacts.SelectedObjects) // Find Object
                {
                    Contacts itm = db.Contacts.Find(((Contacts)item).ID);
                    itm.Active = active;
                }
                db.SaveChanges();                           // Save to DB
                UpdateConOLV();                             // Update Contacts OLV 
            }
        }

        #endregion

        #region Filter

        /// <summary>
        /// Update Filter ComboBox
        /// </summary>
        private void UpdateCBFilterContacts()
        {
            cbFilterCol.Items.Add(Lng.Get("All"));
            cbFilterCol.Items.Add(Lng.Get("Name"));
            cbFilterCol.Items.Add(Lng.Get("Surname"));
            cbFilterCol.Items.Add(Lng.Get("Nick"));
            cbFilterCol.Items.Add(Lng.Get("Phone"));
            cbFilterCol.Items.Add(Lng.Get("Email"));
            cbFilterCol.Items.Add(Lng.Get("Address"));
            cbFilterCol.Items.Add(Lng.Get("Company"));
            cbFilterCol.SelectedIndex = 0;

            cbFastFilterCol.Items.Add(Lng.Get("All"));
            cbFastFilterCol.Items.Add(Lng.Get("Name"));
            cbFastFilterCol.Items.Add(Lng.Get("Surname"));
            cbFastFilterCol.Items.Add(Lng.Get("Nick"));
            cbFastFilterCol.Items.Add(Lng.Get("Phone"));
            cbFastFilterCol.Items.Add(Lng.Get("Email"));
            cbFastFilterCol.Items.Add(Lng.Get("Address"));
            cbFastFilterCol.Items.Add(Lng.Get("Company"));
            cbFastFilterCol.SelectedIndex = 0;
        }

        /// <summary>
        /// Use Filters
        /// </summary>
        private void UseFiltersContacts()
        {
            olvContacts.UseFiltering = true;
            olvContacts.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { FastFilter, FastFilterTags, StandardFilter });
        }

        /// <summary>
        /// Use Fast Filter
        /// </summary>
        private void UseFastFilterContacts()
        {
            if (FastFilterList.Count == 0)
                FastFilter = TextMatchFilter.Contains(olvContacts, "");
            else
            {
                string[] filterArray = FastFilterList.ToArray();
                FastFilter = TextMatchFilter.Prefix(olvContacts, filterArray);
            }
            if (cbFastFilterCol.SelectedIndex == 0)
                FastFilter.Columns = new OLVColumn[] { conName, conSurname, conNick, conPhone, conEmail, conAddress, conCompany };
            else if (cbFastFilterCol.SelectedIndex == 1)
                FastFilter.Columns = new OLVColumn[] { conName };
            else if (cbFastFilterCol.SelectedIndex == 2)
                FastFilter.Columns = new OLVColumn[] { conSurname };
            else if (cbFastFilterCol.SelectedIndex == 3)
                FastFilter.Columns = new OLVColumn[] { conNick };
            else if (cbFastFilterCol.SelectedIndex == 4)
                FastFilter.Columns = new OLVColumn[] { conPhone };
            else if (cbFastFilterCol.SelectedIndex == 5)
                FastFilter.Columns = new OLVColumn[] { conEmail };
            else if (cbFastFilterCol.SelectedIndex == 6)
                FastFilter.Columns = new OLVColumn[] { conAddress };
            else if (cbFastFilterCol.SelectedIndex == 7)
                FastFilter.Columns = new OLVColumn[] { conCompany };
        }

        /// <summary>
        /// Use Fast Tag Filter
        /// </summary>
        private void UseFastTagFilterContacts()
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

        /// <summary>
        /// Use Standard Filter
        /// </summary>
        private void UseStandardFilterContacts()
        {
            StandardFilter = TextMatchFilter.Contains(olvContacts, txtFilter.Text);

            if (cbFilterCol.SelectedIndex == 0)
                StandardFilter.Columns = new OLVColumn[] { conName, conSurname, conNick, conPhone, conEmail, conAddress, conCompany };
            else if (cbFilterCol.SelectedIndex == 1)
                StandardFilter.Columns = new OLVColumn[] { conName };
            else if (cbFilterCol.SelectedIndex == 2)
                StandardFilter.Columns = new OLVColumn[] { conSurname };
            else if (cbFilterCol.SelectedIndex == 3)
                StandardFilter.Columns = new OLVColumn[] { conNick };
            else if (cbFilterCol.SelectedIndex == 4)
                StandardFilter.Columns = new OLVColumn[] { conPhone };
            else if (cbFilterCol.SelectedIndex == 5)
                StandardFilter.Columns = new OLVColumn[] { conEmail };
            else if (cbFilterCol.SelectedIndex == 6)
                StandardFilter.Columns = new OLVColumn[] { conAddress };
            else if (cbFilterCol.SelectedIndex == 7)
                StandardFilter.Columns = new OLVColumn[] { conCompany };
        }

        #endregion

        #region Import / Export

        private void ImportContacts(string fileName)
        {
            List<Contacts> con = global.ImportContactsCSV(fileName);
            if (con == null)
            {
                Dialogs.ShowErr(Lng.Get("ParseFileError", "Parse file error") + ".", Lng.Get("Error"));
                return;
            }

            databaseEntities db = new databaseEntities();

            foreach (var item in con)
            {


                Contacts contact;
                // ----- ID -----
                if (item.ID != Guid.Empty)
                {
                    contact = db.Contacts.Find(item.ID);
                    if (contact != null)
                        Conv.CopyClassPropetries(contact, item);
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
            Dialogs.ShowInfo(Lng.Get("SuccesfullyImport", "Import was succesfully done") + ".", Lng.Get("Import"));
        }

        private void ExportContacts(string fileName)
        {
            List<Contacts> con = new List<Contacts>();

            foreach (var item in olvContacts.FilteredObjects)
            {
                con.Add((Contacts)item);
            }
            global.ExportContactsCSV(fileName, con);
        }

        #endregion

        #region Items


        private void mnuCSetTag_Click(object sender, EventArgs e)
        {
            short tag = 0;
            if (((ToolStripItem)sender).Tag == "1") tag = 0x01;
            if (((ToolStripItem)sender).Tag == "2") tag = 0x02;
            if (((ToolStripItem)sender).Tag == "3") tag = 0x04;
            if (((ToolStripItem)sender).Tag == "4") tag = 0x08;
            if (((ToolStripItem)sender).Tag == "5") tag = 0x10;
            if (((ToolStripItem)sender).Tag == "6") tag = 0x20;

            SetTagItem(tag);
        }

        private void mnuCActive_Click(object sender, EventArgs e)
        {
            
        }

        #endregion



    }
}
