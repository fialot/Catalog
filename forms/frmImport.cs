using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GContacts;


namespace Katalog
{
    public partial class frmImport : Form
    {
        public frmImport()
        {
            InitializeComponent();
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            /*GoogleContacts gc = new GoogleContacts();
            gc.Login();
            gc.ImportGmail();
            MessageBox.Show("count: " + gc.Contact.Count.ToString());*/
        }
    }
}
