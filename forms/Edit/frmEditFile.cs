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
    public partial class frmEditFile : Form
    {

        string FileText = "";


        public frmEditFile()
        {
            InitializeComponent();
        }


        /// <summary>
        /// ShowDialog with ID (Edit)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(ref string text)
        {
            FileText = text;                   // Item ID
            DialogResult res = base.ShowDialog();       // Base ShowDialog
            if (res == DialogResult.OK)
            {
                FInfo info = new FInfo();

                info.Name = txtName.Text;
                info.Path = txtPath.Text;
                info.Version = txtVersion.Text;
                info.Group = txtGroup.Text;
                info.Description = txtDescription.Text;

                text = global.FInfoToText(info);
            }
                
            return res;

        }


        private void frmEditFile_Load(object sender, EventArgs e)
        {
            FInfo info = global.GetFInfo(FileText);

            if (info != null)
            {
                txtName.Text = info.Name;
                txtPath.Text = info.Path;
                txtVersion.Text = info.Version;
                txtGroup.Text = info.Group;
                txtDescription.Text = info.Description;
            }
        }
    }
}
