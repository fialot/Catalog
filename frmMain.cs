using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Katalog
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

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

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            frmAbout formAbout = new frmAbout();
            formAbout.ShowDialog();
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab == tabContacts)
            {
                frmEditContacts form = new frmEditContacts();
                form.ShowDialog();
            }
        }

        private void mnuImport_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabContacts)
            {
                frmImport form = new frmImport();
                form.ShowDialog();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            UpdateConOLV();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            UpdateConOLV();
        }
    }
}
