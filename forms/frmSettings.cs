using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using myFunctions;

namespace Katalog
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ContactStart = Conv.ToLongDef(txtConStart.Text, 1);
            Properties.Settings.Default.ContactMinCharLen = Conv.ToIntDef(txtConMinCharLen.Text, 1);
            Properties.Settings.Default.ContactPrefix = txtConPrefix.Text;
            Properties.Settings.Default.ContactSuffix = txtConSuffix.Text;

            Properties.Settings.Default.ItemStart = Conv.ToLongDef(txtItemStart.Text,1);
            Properties.Settings.Default.ItemMinCharLen = Conv.ToIntDef(txtItemMinCharLen.Text, 1);
            Properties.Settings.Default.ItemPrefix = txtItemPrefix.Text;
            Properties.Settings.Default.ItemSuffix = txtItemSuffix.Text;

            Properties.Settings.Default.BookStart = Conv.ToLongDef(txtBookStart.Text, 1);
            Properties.Settings.Default.BookMinCharLen = Conv.ToIntDef(txtBookMinCharLen.Text, 1);
            Properties.Settings.Default.BookPrefix = txtBookPrefix.Text;
            Properties.Settings.Default.BookSuffix = txtBookSuffix.Text;
            Properties.Settings.Default.BookUseISBN = chbUseISBN.Checked;

            Properties.Settings.Default.BoardStart = Conv.ToLongDef(txtBoardStart.Text, 1);
            Properties.Settings.Default.BoardMinCharLen = Conv.ToIntDef(txtBoardMinCharLen.Text, 1);
            Properties.Settings.Default.BoardPrefix = txtBoardPrefix.Text;
            Properties.Settings.Default.BoardSuffix = txtBoardSuffix.Text;

            Properties.Settings.Default.IncSpecimenInv = chbIncSpecimen.Checked;

            Properties.Settings.Default.scanCOM = cbScanCOM.Text;

            Properties.Settings.Default.BookFolder = txtPathBooks.Text;
            Properties.Settings.Default.GameFolder = txtPathGames.Text;
            Properties.Settings.Default.ObjectFolder = txtPathObjects.Text;

            Properties.Settings.Default.Save();

            if (MaxInvNumbers.Contact < Properties.Settings.Default.ContactStart) MaxInvNumbers.Item = Properties.Settings.Default.ContactStart - 1;
            if (MaxInvNumbers.Item < Properties.Settings.Default.ItemStart) MaxInvNumbers.Item = Properties.Settings.Default.ItemStart - 1;

            this.DialogResult = DialogResult.OK;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            RefreshCOMPorts();

            txtConStart.Text = Properties.Settings.Default.ContactStart.ToString();
            txtConMinCharLen.Text = Properties.Settings.Default.ContactMinCharLen.ToString();
            txtConPrefix.Text = Properties.Settings.Default.ContactPrefix;
            txtConSuffix.Text = Properties.Settings.Default.ContactSuffix;

            txtItemStart.Text = Properties.Settings.Default.ItemStart.ToString();
            txtItemMinCharLen.Text = Properties.Settings.Default.ItemMinCharLen.ToString();
            txtItemPrefix.Text = Properties.Settings.Default.ItemPrefix;
            txtItemSuffix.Text = Properties.Settings.Default.ItemSuffix;

            txtBookStart.Text = Properties.Settings.Default.BookStart.ToString();
            txtBookMinCharLen.Text = Properties.Settings.Default.BookMinCharLen.ToString();
            txtBookPrefix.Text = Properties.Settings.Default.BookPrefix;
            txtBookSuffix.Text = Properties.Settings.Default.BookSuffix;
            chbUseISBN.Checked = Properties.Settings.Default.BookUseISBN;

            txtBoardStart.Text = Properties.Settings.Default.BoardStart.ToString();
            txtBoardMinCharLen.Text = Properties.Settings.Default.BoardMinCharLen.ToString();
            txtBoardPrefix.Text = Properties.Settings.Default.BoardPrefix;
            txtBoardSuffix.Text = Properties.Settings.Default.BoardSuffix;

            chbIncSpecimen.Checked = Properties.Settings.Default.IncSpecimenInv;

            cbScanCOM.Text = Properties.Settings.Default.scanCOM;

            txtPathBooks.Text = Properties.Settings.Default.BookFolder;
            txtPathGames.Text = Properties.Settings.Default.GameFolder;
            txtPathObjects.Text = Properties.Settings.Default.ObjectFolder;
        }

        private void btnScanRefresh_Click(object sender, EventArgs e)
        {
            RefreshCOMPorts();
        }

        void RefreshCOMPorts()
        {
            string COM = cbScanCOM.Text;
            cbScanCOM.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                cbScanCOM.Items.Add(ports[i]);
            }

            cbScanCOM.Text = COM;
        }

        private void btnPathBooks_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            // ----- Set Init Dir -----
            if (System.IO.Directory.Exists(txtPathBooks.Text))
                dialog.InitialDirectory = txtPathBooks.Text;


            // ----- Show open dialog -----
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                txtPathBooks.Text = dialog.FileName;
            dialog.Dispose();
        }

        private void btnPathGames_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            // ----- Set Init Dir -----
            if (System.IO.Directory.Exists(txtPathBooks.Text))
                dialog.InitialDirectory = txtPathGames.Text;


            // ----- Show open dialog -----
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                txtPathGames.Text = dialog.FileName;
            dialog.Dispose();
        }

        private void btnPathObjects_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            // ----- Set Init Dir -----
            if (System.IO.Directory.Exists(txtPathBooks.Text))
                dialog.InitialDirectory = txtPathObjects.Text;


            // ----- Show open dialog -----
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                txtPathObjects.Text = dialog.FileName;
            dialog.Dispose();
        }
    }
}
