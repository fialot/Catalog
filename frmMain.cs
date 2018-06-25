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
    }
}
