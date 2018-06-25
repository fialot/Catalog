using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using myFunctions;

namespace Katalog
{
    public partial class frmMain : Form
    {


        #region Constructor

        public frmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region ObjectListView

        /// <summary>
        /// Update Contacts ObjectListView
        /// </summary>
        void UpdateConOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Contacts> con = db.Contacts.ToList();
            
            conName.AspectGetter = delegate (object x) {
                return ((Contacts)x).Name.Trim();
            };
            conSurname.AspectGetter = delegate (object x) {
                return ((Contacts)x).Surname.Trim();
            };
            conPhone.AspectGetter = delegate (object x) {
                return ((Contacts)x).Phone.Trim();
            };
            conEmail.AspectGetter = delegate (object x) {
                return ((Contacts)x).Email.Trim();
            };
            conAddress.AspectGetter = delegate (object x) {
                string address = ((Contacts)x).Street.Trim();
                if (address != "") address += ", ";
                address += ((Contacts)x).City.Trim();
                if (address != "") address += ", ";
                address += ((Contacts)x).Country.Trim();
                return address;
            };

            olvContacts.SetObjects(con);
        }

        /// <summary>
        /// OLV Contacts selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }

        #endregion

        #region Edit Items

        private void EnableEditItems()
        {
            if (tabCatalog.SelectedTab == tabContacts)
            {
                if (olvContacts.SelectedIndex >= 0)
                {
                    btnEditItem.Enabled = true;
                    btnDeleteItem.Enabled = true;
                }
                else
                {
                    btnEditItem.Enabled = false;
                    btnDeleteItem.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Button New Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewItem_Click(object sender, EventArgs e)
        {

            if (tabCatalog.SelectedTab == tabContacts)
            {
                frmEditContacts form = new frmEditContacts();
                form.ShowDialog();
                UpdateConOLV();
            }
        }

        /// <summary>
        /// Button Edit Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (olvContacts.SelectedIndex >= 0)
            {
                if (tabCatalog.SelectedTab == tabContacts)
                {

                    frmEditContacts form = new frmEditContacts();
                    form.ShowDialog(((Contacts)olvContacts.SelectedObject).Id);
                    UpdateConOLV();
                }
            }
        }

        /// <summary>
        /// Button Delete Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (olvContacts.SelectedIndex >= 0)
            {
                databaseEntities db = new databaseEntities();

                if (tabCatalog.SelectedTab == tabContacts)
                {
                    Contacts contact = db.Contacts.Find(((Contacts)olvContacts.SelectedObject).Id);
                    
                    if (Dialogs.ShowQuest("Delete Item \"" + contact.Name.Trim() + " " + contact.Surname.Trim() + "\"?", "Delete") == DialogResult.Yes)
                    {
                        db.Contacts.Remove(contact);
                        db.SaveChanges();
                        UpdateConOLV();
                    }
                    
                }
            }
        }

        #endregion

        #region Filter

        void UpdateFilter()
        {
            cbFilterCol.Items.Clear();
            cbFastFilterCol.Items.Clear();
            if (tabCatalog.SelectedTab == tabContacts)
            {
                cbFilterCol.Items.Add(Lng.Get("All", "All"));
                cbFilterCol.Items.Add(Lng.Get("Name","Name"));
                cbFilterCol.Items.Add(Lng.Get("Surname", "Surname"));
                cbFilterCol.Items.Add(Lng.Get("Phone", "Phone"));
                cbFilterCol.Items.Add(Lng.Get("Email", "Email"));
                cbFilterCol.Items.Add(Lng.Get("Address", "Address"));
                cbFilterCol.SelectedIndex = 0;

                cbFastFilterCol.Items.Add(Lng.Get("All", "All"));
                cbFastFilterCol.Items.Add(Lng.Get("Name", "Name"));
                cbFastFilterCol.Items.Add(Lng.Get("Surname", "Surname"));
                cbFastFilterCol.Items.Add(Lng.Get("Phone", "Phone"));
                cbFastFilterCol.Items.Add(Lng.Get("Email", "Email"));
                cbFastFilterCol.Items.Add(Lng.Get("Address", "Address"));
                cbFastFilterCol.SelectedIndex = 0;
            }

        }

        void SetFastFilter(bool set, string letter)
        {

        }

        private void btnFilterA_Click(object sender, EventArgs e)
        {
            string letter = ((ToolStripButton)sender).Text;
            bool set = ((ToolStripButton)sender).Pressed;
            SetFastFilter(set, letter);
        }

        #endregion

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            frmAbout formAbout = new frmAbout();
            formAbout.ShowDialog();
        }

        
        private void mnuImport_Click(object sender, EventArgs e)
        {
            if (tabCatalog.SelectedTab == tabContacts)
            {
                frmImport form = new frmImport();
                form.ShowDialog();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            UpdateConOLV();
            EnableEditItems();
            UpdateFilter();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            UpdateConOLV();
        }

        private void tabCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        
    }
}
