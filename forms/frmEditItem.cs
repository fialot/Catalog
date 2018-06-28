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

using myFunctions;

namespace Katalog
{
    public partial class frmEditItem : Form
    {

        #region Variables

        Color SelectColor = Color.SkyBlue;                  // FastTags Select color

        databaseEntities db = new databaseEntities();       // Database
        Guid ID = Guid.Empty;                               // Selected Item GUID (No Guid = new item)
        

        int ItemCount = 1;                                  // Item count
        List<string> InvNumbers = new List<string>();       // Items Inventory numbers
        List<string> Locations = new List<string>();        // Items Locations 
        int SelSpecimen = 0;

        #endregion

        #region Constructor

        public frmEditItem()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Load

        /// <summary>
        /// ShowDialog with ID (Edit)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(Guid ID)
        {
            this.ID = ID;
            return base.ShowDialog();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditItem_Load(object sender, EventArgs e)
        {
            // ----- Add Specimen -----
            cbSpecimen.Items.Clear();
            cbSpecimen.Items.Add("1");
            cbSpecimen.SelectedIndex = 0;
            InvNumbers.Add("");
            Locations.Add("");

            // ----- Set Acquisition date -----
            dtAcqDate.Value = DateTime.Now;

            // ----- If Edit -> fill form -----
            if (ID != Guid.Empty)
            {
                Items itm = db.Items.Find(ID);

                // ----- Fill Image -----
                imgImg.Image = Conv.ByteArrayToImage(itm.Image);

                // ----- Fill main data -----   
                txtName.Text = itm.Name.Trim();                     // Name
                txtCategory.Text = itm.Category.Trim();             // Category
                txtSubCategory.Text = itm.Subcategory.Trim();       // SubCategory
                txtKeywords.Text = itm.Keywords.Trim();             // Keywords
                txtNote.Text = itm.Note.Trim();                     // Note

                txtPrice.Text = itm.Price.ToString();               // Price
                dtAcqDate.Value = itm.AcqDate ?? DateTime.Now;      // Acqusition date
                chbExcluded.Checked = itm.Excluded ?? false;        // Excluded

                // ----- Fill Specimen -----
                ItemCount = itm.Count ?? 1;                         // Get counts

                cbSpecimen.Items.Clear();
                InvNumbers.Clear();
                Locations.Clear();

                string[] invNums = itm.InvNumber.Trim().Split(new string[] { ";" }, StringSplitOptions.None);
                string[] locs = itm.Location.Trim().Split(new string[] { ";" }, StringSplitOptions.None);

                for (int i = 0; i < ItemCount; i++)                 // Fill specimen
                {
                    cbSpecimen.Items.Add((i+1).ToString());
                    if (i < invNums.Length)                         // Inventory numbers list
                        InvNumbers.Add(invNums[i]);
                    else
                        InvNumbers.Add("");
                    if (i < locs.Length)                            // Locations list
                        Locations.Add(locs[i]);
                    else
                        Locations.Add("");
                }
                txtInvNum.Text = InvNumbers[0];                     // Inventory number
                txtLocation.Text = Locations[0];                    // Location
                cbSpecimen.SelectedIndex = 0;
                

                lblCount.Text = "/ " + ItemCount.ToString();        // Counts

                // ----- Fast tags -----
                FastFlags flag = (FastFlags)(itm.FastTags ?? 0);
                if (flag.HasFlag(FastFlags.FLAG1)) btnTag1.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG2)) btnTag2.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG3)) btnTag3.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG4)) btnTag4.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG5)) btnTag5.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG6)) btnTag6.BackColor = Color.SkyBlue;
            }
        }

        #endregion

        #region Form Close

        /// <summary>
        /// Fill Item values
        /// </summary>
        /// <param name="itm"></param>
        private void FillItem(ref Items itm)
        {
            // ----- Fill Image -----
            byte[] bytes = Conv.ImageToByteArray(imgImg.Image);
            if (bytes != null) itm.Image = bytes;

            // ----- Fill main data -----   
            itm.Name = txtName.Text;                        // Name
            itm.Category = txtCategory.Text;                // Category
            itm.Subcategory = txtSubCategory.Text;          // SubCategory
            itm.Keywords = txtKeywords.Text;                // Keywords
            itm.Note = txtNote.Text;                        // Note

            itm.Price = Conv.ToDoubleNull(txtPrice.Text);   // Price
            itm.AcqDate = dtAcqDate.Value;                  // Acqusition date
            itm.Excluded = chbExcluded.Checked;             // Excluded

            // ----- Fill Specimen -----
            itm.Count = (short)ItemCount;                   // Get counts

            string invNums = "", locs = "";
            for (int i = 0; i < ItemCount; i++)             // Fill specimen
            {
                if (invNums != "") invNums += ";";
                invNums += InvNumbers[i];
                if (locs != "") locs += ";";
                locs += Locations[i];
            }
            itm.InvNumber = invNums;
            itm.Location = locs;


            // ----- Fast tags -----
            short fastTag = 0;
            if (btnTag1.BackColor == SelectColor) fastTag |= 0x01;
            if (btnTag2.BackColor == SelectColor) fastTag |= 0x02;
            if (btnTag3.BackColor == SelectColor) fastTag |= 0x04;
            if (btnTag4.BackColor == SelectColor) fastTag |= 0x08;
            if (btnTag5.BackColor == SelectColor) fastTag |= 0x10;
            if (btnTag6.BackColor == SelectColor) fastTag |= 0x20;
            itm.FastTags = fastTag;
        }

        /// <summary>
        /// Button OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            Items itm;

            // ----- Save last Specimen values -----
            InvNumbers.RemoveAt(SelSpecimen);
            Locations.RemoveAt(SelSpecimen);
            InvNumbers.Insert(SelSpecimen, txtInvNum.Text);
            Locations.Insert(SelSpecimen, txtLocation.Text);

            // ----- ID -----
            if (ID != Guid.Empty)
            {
                itm = db.Items.Find(ID);
            }
            else
            {
                itm = new Items();
                itm.Id = Guid.NewGuid();
            }

            FillItem(ref itm);

            if (ID == Guid.Empty) db.Items.Add(itm);
            db.SaveChanges();

            this.DialogResult = DialogResult.OK;
        }

        #endregion

        #region Specimen

        private void btnAddSpecimen_Click(object sender, EventArgs e)
        {
            ItemCount++;
            InvNumbers.Add(txtInvNum.Text);
            Locations.Add(txtLocation.Text);
            cbSpecimen.Items.Add((cbSpecimen.Items.Count + 1).ToString());
            cbSpecimen.SelectedIndex = cbSpecimen.Items.Count - 1;
            btnDelSpecimen.Enabled = true;
            lblCount.Text = "/ " + ItemCount.ToString();        // Counts
        }

        private void btnDelSpecimen_Click(object sender, EventArgs e)
        {
            if (ItemCount > 1)
            {
                int sel = cbSpecimen.SelectedIndex;
                ItemCount--;
                InvNumbers.RemoveAt(sel);
                Locations.RemoveAt(sel);
                cbSpecimen.Items.RemoveAt(cbSpecimen.Items.Count - 1);
                if (sel >= cbSpecimen.Items.Count)
                    cbSpecimen.SelectedIndex = sel - 1;
                else
                {
                    txtInvNum.Text = InvNumbers[sel];
                    txtLocation.Text = Locations[sel];
                }
                
                lblCount.Text = "/ " + ItemCount.ToString();        // Counts
                if (ItemCount == 1)
                {
                    btnDelSpecimen.Enabled = false;
                }
            }
        }

        private void cbSpecimen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InvNumbers.Count > 0)
            {
                if (SelSpecimen < InvNumbers.Count)
                {
                    InvNumbers.RemoveAt(SelSpecimen);
                    Locations.RemoveAt(SelSpecimen);
                    InvNumbers.Insert(SelSpecimen, txtInvNum.Text);
                    Locations.Insert(SelSpecimen, txtLocation.Text);
                }
                txtInvNum.Text = InvNumbers[cbSpecimen.SelectedIndex];
                txtLocation.Text = Locations[cbSpecimen.SelectedIndex];
                SelSpecimen = cbSpecimen.SelectedIndex;
            }
        }

        #endregion

        #region FastTags

        /// <summary>
        /// Set fast tag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTag1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == SystemColors.Control)
                ((Button)sender).BackColor = SelectColor;
            else
                ((Button)sender).BackColor = SystemColors.Control;
        }

        #endregion

        #region Image

        /// <summary>
        /// Load Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgImg_Click(object sender, EventArgs e)
        {
            Image img = imgImg.Image;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Lng.Get("Images") + " |*.jpg;*.jpeg;*.jpe;*.tiff;*.png;*.gif";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imgImg.Load(dialog.FileName);
                }
                catch
                {
                    imgImg.Image = img;
                    img.Dispose();
                    Dialogs.ShowErr(Lng.Get("ErrLoadImg", "Image cannot load!"), Lng.Get("Error"));
                }
            }
        }

        #endregion
    }
}
