﻿using System;
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
    public partial class frmEditGames : Form
    {

        #region Variables

        // ----- Fast Tags -----
        Color SelectColor = Color.SkyBlue;                  // FastTags Select color

        // ----- Database -----
        databaseEntities db = new databaseEntities();       // Database
        Guid ID = Guid.Empty;                               // Selected Item GUID (No Guid = new item)

        string files = "";

        #endregion

        #region Constructor

        public frmEditGames()
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

        private void frmEditGames_Load(object sender, EventArgs e)
        {

            // ----- Get Autofill lists -----
            var categoryList = db.Games.Select(x => x.Category.Trim()).ToList();
            var subcategoryList = db.Games.Select(x => x.Subcategory.Trim()).ToList();
            var enviromentList = db.Games.Select(x => x.Environment.Trim()).ToList();

            // ----- Delete duplicates -----
            categoryList = global.DeleteDuplicates(categoryList);
            subcategoryList = global.DeleteDuplicates(subcategoryList);
            enviromentList = global.DeleteDuplicates(enviromentList);

            // ----- Prepare autocomplete -----
            foreach (var item in categoryList)
                txtCategory.AutoCompleteCustomSource.Add(item);
            foreach (var item in subcategoryList)
                txtSubCategory.AutoCompleteCustomSource.Add(item);
            foreach (var item in enviromentList)
                txtEnviroment.AutoCompleteCustomSource.Add(item);

            // ----- If Edit -> fill form -----
            if (ID != Guid.Empty)
            {
                Games itm = db.Games.Find(ID);

                // ----- Fill Image -----
                imgImg.Image = Conv.ByteArrayToImage(itm.Image);

                // ----- Fill main data -----   
                txtName.Text = itm.Name.Trim();                     // Name
                txtCategory.Text = itm.Category.Trim();             // Category
                txtSubCategory.Text = itm.Subcategory.Trim();       // SubCategory
                txtKeywords.Text = itm.Keywords.Trim();             // Keywords
                txtNote.Text = itm.Note.Trim();                     // Note

                // ----- Games -----
                txtDescription.Text = itm.Description.Trim();
                txtPlayerAge.Text = itm.PlayerAge.Trim();
                txtMinPlayers.Text = itm.MinPlayers != null ? itm.MinPlayers.ToString() : "";
                txtMaxPlayers.Text = itm.MaxPlayers != null ? itm.MaxPlayers.ToString() : "";
                txtPlayerAge.Text = itm.PlayerAge != null ? itm.PlayerAge.ToString() : "";
                txtDuration.Text = itm.Duration != null ? itm.Duration.ToString() : "";
                txtDurPreparation.Text = itm.DurationPreparation != null ? itm.DurationPreparation.ToString() : "";
                txtThings.Text = itm.Things.Trim();
                txtPreparation.Text = itm.Preparation.Trim();
                txtEnviroment.Text = itm.Environment.Trim();
                txtRules.Text = itm.Rules.Trim();
                txtURL.Text = itm.URL.Trim();
                files = itm.Files.Trim();
                if (files != "")
                {
                    btnFiles.ForeColor = Color.Green;
                    btnFiles.Font = new Font(btnFiles.Font, FontStyle.Bold);
                }

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
        private void FillItem(ref Games itm)
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
            
            // ----- Games -----
            itm.Description = txtDescription.Text;
            itm.PlayerAge = txtPlayerAge.Text;
            itm.MinPlayers = Conv.ToShortNull(txtMinPlayers.Text);
            itm.MaxPlayers = Conv.ToShortNull(txtMaxPlayers.Text);
            itm.PlayerAge = txtPlayerAge.Text;
            itm.Duration = Conv.ToShortNull(txtDuration.Text);
            itm.DurationPreparation = Conv.ToShortNull(txtDurPreparation.Text);
            itm.Things = txtThings.Text;
            itm.Preparation = txtPreparation.Text;
            itm.Environment = txtEnviroment.Text;
            itm.Rules = txtRules.Text;
            itm.URL = txtURL.Text;
            itm.Files = files;

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
            Games itm;

            // ----- Item ID -----
            if (ID != Guid.Empty)
            {
                itm = db.Games.Find(ID);
            }
            else
            {
                itm = new Games();
                itm.ID = Guid.NewGuid();
            }

            // ----- Fill Item values -----
            FillItem(ref itm);

            // ----- Update database -----
            if (ID == Guid.Empty) db.Games.Add(itm);
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

        private void btnFiles_Click(object sender, EventArgs e)
        {
            frmEditFiles form = new frmEditFiles();
            form.ShowDialog(ref files);
            if (files != "")
            {
                btnFiles.ForeColor = Color.Green;
                btnFiles.Font = new Font(btnFiles.Font, FontStyle.Bold);
            }
        }
    }
}
