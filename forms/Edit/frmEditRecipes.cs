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
    public partial class frmEditRecipes : Form
    {

        #region Variables

        // ----- Fast Tags -----
        Color SelectColor = Color.SkyBlue;                  // FastTags Select color

        // ----- Database -----
        databaseEntities db = new databaseEntities();       // Database
        Guid ID = Guid.Empty;                               // Selected Item GUID (No Guid = new item)

        #endregion

        #region Constructor

        public frmEditRecipes()
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
            this.ID = ID;                   // Item ID
            return base.ShowDialog();       // Base ShowDialog

        }

        private void frmEditRecipes_Load(object sender, EventArgs e)
        {

            // ----- Get Autofill lists -----
            var categoryList = db.Recipes.Select(x => x.Category.Trim()).ToList();
            var subcategoryList = db.Recipes.Select(x => x.Subcategory.Trim()).ToList();

            // ----- Delete duplicates -----
            categoryList = global.DeleteDuplicates(categoryList);
            subcategoryList = global.DeleteDuplicates(subcategoryList);

            // ----- Prepare autocomplete -----
            foreach (var item in categoryList)
                txtCategory.AutoCompleteCustomSource.Add(item);
            foreach (var item in subcategoryList)
                txtSubCategory.AutoCompleteCustomSource.Add(item);

            // ----- If Edit -> fill form -----
            if (ID != Guid.Empty)
            {
                Recipes itm = db.Recipes.Find(ID);

                // ----- Fill Image -----
                imgImg.Image = Conv.ByteArrayToImage(itm.Image);

                // ----- Fill main data -----   
                txtName.Text = itm.Name.Trim();                     // Name
                txtCategory.Text = itm.Category.Trim();             // Category
                txtSubCategory.Text = itm.Subcategory.Trim();       // SubCategory
                txtKeywords.Text = itm.Keywords.Trim();             // Keywords
                txtNote.Text = itm.Note.Trim();                     // Note

                // ----- Recipes -----
                txtDescription.Text = itm.Description.Trim();
                txtResources.Text = itm.Resources.Trim();
                txtProcedure.Text = itm.Procedure.Trim();
                txtURL.Text = itm.URL.Trim();

                // ----- Rating -----
                txtRating.Text = itm.Rating.ToString();
                txtMyRating.Text = itm.MyRating.ToString();

                // ----- Fast tags -----
                FastFlags flag = (FastFlags)(itm.FastTags ?? 0);
                if (flag.HasFlag(FastFlags.FLAG1)) btnTag1.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG2)) btnTag2.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG3)) btnTag3.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG4)) btnTag4.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG5)) btnTag5.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG6)) btnTag6.BackColor = SelectColor;

                // ----- Excluded -----
                chbExcluded.Checked = itm.Excluded ?? false;

                // ----- Update -----
                lblUpdated.Text = Lng.Get("LastUpdate", "Last update") + ": " + (itm.Updated ?? DateTime.Now).ToShortDateString();
            }
            else
            {

            }
        }

        #endregion


        #region Form Close

        /// <summary>
        /// Fill Recipes values
        /// </summary>
        /// <param name="itm"></param>
        private void FillItem(ref Recipes itm)
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

            // ----- Recipes -----
            itm.Description = txtDescription.Text;
            itm.Resources = txtResources.Text;
            itm.Procedure = txtProcedure.Text;
            itm.URL = txtURL.Text;

            // ----- Rating -----
            itm.Rating = Conv.ToShortNull(txtRating.Text);
            itm.MyRating = Conv.ToShortNull(txtMyRating.Text);

            // ----- Fast tags -----
            short fastTag = 0;
            if (btnTag1.BackColor == SelectColor) fastTag |= 0x01;
            if (btnTag2.BackColor == SelectColor) fastTag |= 0x02;
            if (btnTag3.BackColor == SelectColor) fastTag |= 0x04;
            if (btnTag4.BackColor == SelectColor) fastTag |= 0x08;
            if (btnTag5.BackColor == SelectColor) fastTag |= 0x10;
            if (btnTag6.BackColor == SelectColor) fastTag |= 0x20;
            itm.FastTags = fastTag;

            // ----- Excluded -----
            itm.Excluded = chbExcluded.Checked;

            // ----- Last Update -----
            itm.Updated = DateTime.Now;
        }

        /// <summary>
        /// Save edited items to DB
        /// </summary>
        private void SaveItem()
        {
            Recipes itm;

            // ----- Item ID -----
            if (ID != Guid.Empty)
            {
                itm = db.Recipes.Find(ID);
            }
            else
            {
                itm = new Recipes();
                itm.ID = Guid.NewGuid();
            }
            
            // ----- Fill Item values -----
            FillItem(ref itm);

            // ----- Update database -----
            if (ID == Guid.Empty) db.Recipes.Add(itm);
            db.SaveChanges();
        }

        /// <summary>
        /// Button OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // ----- Save to DB -----
            SaveItem();

            // ----- Exit -----
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Button Save - New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            // ----- Save to DB -----
            SaveItem();

            // ----- Exit -----
            this.DialogResult = DialogResult.Yes;
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
