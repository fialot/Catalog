namespace Katalog
{
    partial class frmEditItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditItem));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.imgImg = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnTag6 = new System.Windows.Forms.Button();
            this.btnTag5 = new System.Windows.Forms.Button();
            this.btnTag4 = new System.Windows.Forms.Button();
            this.btnTag3 = new System.Windows.Forms.Button();
            this.btnTag2 = new System.Windows.Forms.Button();
            this.btnTag1 = new System.Windows.Forms.Button();
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.lblKeywords = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.gbSpecimen = new System.Windows.Forms.GroupBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnPlace = new System.Windows.Forms.Button();
            this.btnDelSpecimen = new System.Windows.Forms.Button();
            this.btnAddSpecimen = new System.Windows.Forms.Button();
            this.cbSpecimen = new System.Windows.Forms.ComboBox();
            this.lblSpecimen = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtInvNum = new System.Windows.Forms.TextBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblInvNum = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.dtAcqDate = new System.Windows.Forms.DateTimePicker();
            this.lblAcqDate = new System.Windows.Forms.Label();
            this.txtSubCategory = new System.Windows.Forms.TextBox();
            this.lblSubCategory = new System.Windows.Forms.Label();
            this.chbExcluded = new System.Windows.Forms.CheckBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.txtCondition = new System.Windows.Forms.TextBox();
            this.lblUpdated = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgImg)).BeginInit();
            this.gbSpecimen.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // imgImg
            // 
            this.imgImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.imgImg, "imgImg");
            this.imgImg.Name = "imgImg";
            this.imgImg.TabStop = false;
            this.imgImg.Click += new System.EventHandler(this.imgImg_Click);
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // btnTag6
            // 
            resources.ApplyResources(this.btnTag6, "btnTag6");
            this.btnTag6.BackColor = System.Drawing.SystemColors.Control;
            this.btnTag6.Image = global::Katalog.Properties.Resources.Circle_Blue;
            this.btnTag6.Name = "btnTag6";
            this.btnTag6.UseVisualStyleBackColor = false;
            this.btnTag6.Click += new System.EventHandler(this.btnTag1_Click);
            // 
            // btnTag5
            // 
            resources.ApplyResources(this.btnTag5, "btnTag5");
            this.btnTag5.Image = global::Katalog.Properties.Resources.circ_grey;
            this.btnTag5.Name = "btnTag5";
            this.btnTag5.UseVisualStyleBackColor = true;
            this.btnTag5.Click += new System.EventHandler(this.btnTag1_Click);
            // 
            // btnTag4
            // 
            resources.ApplyResources(this.btnTag4, "btnTag4");
            this.btnTag4.Image = global::Katalog.Properties.Resources.circ_yellow;
            this.btnTag4.Name = "btnTag4";
            this.btnTag4.UseVisualStyleBackColor = true;
            this.btnTag4.Click += new System.EventHandler(this.btnTag1_Click);
            // 
            // btnTag3
            // 
            resources.ApplyResources(this.btnTag3, "btnTag3");
            this.btnTag3.Image = global::Katalog.Properties.Resources.circ_orange;
            this.btnTag3.Name = "btnTag3";
            this.btnTag3.UseVisualStyleBackColor = true;
            this.btnTag3.Click += new System.EventHandler(this.btnTag1_Click);
            // 
            // btnTag2
            // 
            resources.ApplyResources(this.btnTag2, "btnTag2");
            this.btnTag2.BackColor = System.Drawing.SystemColors.Control;
            this.btnTag2.Image = global::Katalog.Properties.Resources.circ_red;
            this.btnTag2.Name = "btnTag2";
            this.btnTag2.UseVisualStyleBackColor = false;
            this.btnTag2.Click += new System.EventHandler(this.btnTag1_Click);
            // 
            // btnTag1
            // 
            resources.ApplyResources(this.btnTag1, "btnTag1");
            this.btnTag1.BackColor = System.Drawing.SystemColors.Control;
            this.btnTag1.Image = global::Katalog.Properties.Resources.circ_green;
            this.btnTag1.Name = "btnTag1";
            this.btnTag1.UseVisualStyleBackColor = false;
            this.btnTag1.Click += new System.EventHandler(this.btnTag1_Click);
            // 
            // txtKeywords
            // 
            resources.ApplyResources(this.txtKeywords, "txtKeywords");
            this.txtKeywords.Name = "txtKeywords";
            // 
            // lblKeywords
            // 
            resources.ApplyResources(this.lblKeywords, "lblKeywords");
            this.lblKeywords.Name = "lblKeywords";
            // 
            // lblNote
            // 
            resources.ApplyResources(this.lblNote, "lblNote");
            this.lblNote.Name = "lblNote";
            // 
            // txtNote
            // 
            resources.ApplyResources(this.txtNote, "txtNote");
            this.txtNote.Name = "txtNote";
            // 
            // txtCategory
            // 
            this.txtCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            resources.ApplyResources(this.txtCategory, "txtCategory");
            this.txtCategory.Name = "txtCategory";
            // 
            // lblCategory
            // 
            resources.ApplyResources(this.lblCategory, "lblCategory");
            this.lblCategory.Name = "lblCategory";
            // 
            // gbSpecimen
            // 
            this.gbSpecimen.Controls.Add(this.lblCount);
            this.gbSpecimen.Controls.Add(this.btnPlace);
            this.gbSpecimen.Controls.Add(this.btnDelSpecimen);
            this.gbSpecimen.Controls.Add(this.btnAddSpecimen);
            this.gbSpecimen.Controls.Add(this.cbSpecimen);
            this.gbSpecimen.Controls.Add(this.lblSpecimen);
            this.gbSpecimen.Controls.Add(this.lblLocation);
            this.gbSpecimen.Controls.Add(this.txtInvNum);
            this.gbSpecimen.Controls.Add(this.txtLocation);
            this.gbSpecimen.Controls.Add(this.lblInvNum);
            resources.ApplyResources(this.gbSpecimen, "gbSpecimen");
            this.gbSpecimen.Name = "gbSpecimen";
            this.gbSpecimen.TabStop = false;
            // 
            // lblCount
            // 
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            // 
            // btnPlace
            // 
            resources.ApplyResources(this.btnPlace, "btnPlace");
            this.btnPlace.Name = "btnPlace";
            this.btnPlace.UseVisualStyleBackColor = true;
            this.btnPlace.Click += new System.EventHandler(this.btnPlace_Click);
            // 
            // btnDelSpecimen
            // 
            resources.ApplyResources(this.btnDelSpecimen, "btnDelSpecimen");
            this.btnDelSpecimen.Name = "btnDelSpecimen";
            this.btnDelSpecimen.UseVisualStyleBackColor = true;
            this.btnDelSpecimen.Click += new System.EventHandler(this.btnDelSpecimen_Click);
            // 
            // btnAddSpecimen
            // 
            resources.ApplyResources(this.btnAddSpecimen, "btnAddSpecimen");
            this.btnAddSpecimen.Name = "btnAddSpecimen";
            this.btnAddSpecimen.UseVisualStyleBackColor = true;
            this.btnAddSpecimen.Click += new System.EventHandler(this.btnAddSpecimen_Click);
            // 
            // cbSpecimen
            // 
            this.cbSpecimen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSpecimen.FormattingEnabled = true;
            resources.ApplyResources(this.cbSpecimen, "cbSpecimen");
            this.cbSpecimen.Name = "cbSpecimen";
            this.cbSpecimen.SelectedIndexChanged += new System.EventHandler(this.cbSpecimen_SelectedIndexChanged);
            // 
            // lblSpecimen
            // 
            resources.ApplyResources(this.lblSpecimen, "lblSpecimen");
            this.lblSpecimen.Name = "lblSpecimen";
            // 
            // lblLocation
            // 
            resources.ApplyResources(this.lblLocation, "lblLocation");
            this.lblLocation.Name = "lblLocation";
            // 
            // txtInvNum
            // 
            resources.ApplyResources(this.txtInvNum, "txtInvNum");
            this.txtInvNum.Name = "txtInvNum";
            // 
            // txtLocation
            // 
            this.txtLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            resources.ApplyResources(this.txtLocation, "txtLocation");
            this.txtLocation.Name = "txtLocation";
            // 
            // lblInvNum
            // 
            resources.ApplyResources(this.lblInvNum, "lblInvNum");
            this.lblInvNum.Name = "lblInvNum";
            // 
            // lblPrice
            // 
            resources.ApplyResources(this.lblPrice, "lblPrice");
            this.lblPrice.Name = "lblPrice";
            // 
            // txtPrice
            // 
            resources.ApplyResources(this.txtPrice, "txtPrice");
            this.txtPrice.Name = "txtPrice";
            // 
            // dtAcqDate
            // 
            resources.ApplyResources(this.dtAcqDate, "dtAcqDate");
            this.dtAcqDate.Name = "dtAcqDate";
            // 
            // lblAcqDate
            // 
            resources.ApplyResources(this.lblAcqDate, "lblAcqDate");
            this.lblAcqDate.Name = "lblAcqDate";
            // 
            // txtSubCategory
            // 
            this.txtSubCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSubCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            resources.ApplyResources(this.txtSubCategory, "txtSubCategory");
            this.txtSubCategory.Name = "txtSubCategory";
            // 
            // lblSubCategory
            // 
            resources.ApplyResources(this.lblSubCategory, "lblSubCategory");
            this.lblSubCategory.Name = "lblSubCategory";
            // 
            // chbExcluded
            // 
            resources.ApplyResources(this.chbExcluded, "chbExcluded");
            this.chbExcluded.Name = "chbExcluded";
            this.chbExcluded.UseVisualStyleBackColor = true;
            // 
            // lblCondition
            // 
            resources.ApplyResources(this.lblCondition, "lblCondition");
            this.lblCondition.Name = "lblCondition";
            // 
            // txtCondition
            // 
            this.txtCondition.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCondition.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            resources.ApplyResources(this.txtCondition, "txtCondition");
            this.txtCondition.Name = "txtCondition";
            // 
            // lblUpdated
            // 
            resources.ApplyResources(this.lblUpdated, "lblUpdated");
            this.lblUpdated.Name = "lblUpdated";
            // 
            // frmEditItem
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblUpdated);
            this.Controls.Add(this.lblCondition);
            this.Controls.Add(this.txtCondition);
            this.Controls.Add(this.chbExcluded);
            this.Controls.Add(this.txtSubCategory);
            this.Controls.Add(this.lblSubCategory);
            this.Controls.Add(this.gbSpecimen);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtKeywords);
            this.Controls.Add(this.lblKeywords);
            this.Controls.Add(this.btnTag6);
            this.Controls.Add(this.dtAcqDate);
            this.Controls.Add(this.btnTag5);
            this.Controls.Add(this.lblAcqDate);
            this.Controls.Add(this.btnTag4);
            this.Controls.Add(this.btnTag3);
            this.Controls.Add(this.btnTag2);
            this.Controls.Add(this.btnTag1);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.imgImg);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmEditItem";
            this.Load += new System.EventHandler(this.frmEditItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgImg)).EndInit();
            this.gbSpecimen.ResumeLayout(false);
            this.gbSpecimen.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox imgImg;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnTag6;
        private System.Windows.Forms.Button btnTag5;
        private System.Windows.Forms.Button btnTag4;
        private System.Windows.Forms.Button btnTag3;
        private System.Windows.Forms.Button btnTag2;
        private System.Windows.Forms.Button btnTag1;
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.Label lblKeywords;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.GroupBox gbSpecimen;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnPlace;
        private System.Windows.Forms.Button btnDelSpecimen;
        private System.Windows.Forms.Button btnAddSpecimen;
        private System.Windows.Forms.ComboBox cbSpecimen;
        private System.Windows.Forms.Label lblSpecimen;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtInvNum;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblInvNum;
        private System.Windows.Forms.DateTimePicker dtAcqDate;
        private System.Windows.Forms.Label lblAcqDate;
        private System.Windows.Forms.TextBox txtSubCategory;
        private System.Windows.Forms.Label lblSubCategory;
        private System.Windows.Forms.CheckBox chbExcluded;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.TextBox txtCondition;
        private System.Windows.Forms.Label lblUpdated;
    }
}