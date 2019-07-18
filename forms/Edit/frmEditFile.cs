using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Katalog
{
    public partial class frmEditFile : Form
    {

        string FileText = "";
        string RelativePath = "";

        public frmEditFile()
        {
            InitializeComponent();
        }


        /// <summary>
        /// ShowDialog with ID (Edit)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(ref string text, string relativePath)
        {
            FileText = text;                  
            RelativePath = relativePath;
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

        private void btnPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Lng.Get("AllFiles", "All files") + " |*.*";

            if (txtPath.Text != "")
            {

                if (File.Exists(txtPath.Text) || Directory.Exists(txtPath.Text))
                    dialog.InitialDirectory = Path.GetDirectoryName(txtPath.Text);
                else if (RelativePath != "")
                    dialog.InitialDirectory = Path.GetDirectoryName(RelativePath + Path.DirectorySeparatorChar + txtPath.Text);
            }
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (RelativePath != "")
                {
                    txtPath.Text = dialog.FileName.Replace(RelativePath, "");
                    if (txtPath.Text.Length > 0 && txtPath.Text[0] == '\\') txtPath.Text = txtPath.Text.Remove(0, 1);
                } else 
                    txtPath.Text = dialog.FileName;
                if (txtName.Text == "")
                    txtName.Text = System.IO.Path.GetFileName(txtPath.Text);
            }
        }
    }
}
