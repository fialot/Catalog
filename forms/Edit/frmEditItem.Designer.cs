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
            this.components = new System.ComponentModel.Container();
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
            this.gbCopies = new System.Windows.Forms.GroupBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.txtCondition = new System.Windows.Forms.TextBox();
            this.chbExcluded = new System.Windows.Forms.CheckBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.dtAcqDate = new System.Windows.Forms.DateTimePicker();
            this.lblAcqDate = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnLocation = new System.Windows.Forms.Button();
            this.btnDelCopy = new System.Windows.Forms.Button();
            this.btnAddCopy = new System.Windows.Forms.Button();
            this.cbCopy = new System.Windows.Forms.ComboBox();
            this.lblCopy = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtInvNum = new System.Windows.Forms.TextBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblInvNum = new System.Windows.Forms.Label();
            this.txtSubCategory = new System.Windows.Forms.TextBox();
            this.lblSubCategory = new System.Windows.Forms.Label();
            this.lblUpdated = new System.Windows.Forms.Label();
            this.btnSaveNew = new System.Windows.Forms.Button();
            this.TimeOut = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgImg)).BeginInit();
            this.gbCopies.SuspendLayout();
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
            // gbCopies
            // 
            this.gbCopies.Controls.Add(this.lblCondition);
            this.gbCopies.Controls.Add(this.txtCondition);
            this.gbCopies.Controls.Add(this.chbExcluded);
            this.gbCopies.Controls.Add(this.lblPrice);
            this.gbCopies.Controls.Add(this.txtPrice);
            this.gbCopies.Controls.Add(this.dtAcqDate);
            this.gbCopies.Controls.Add(this.lblAcqDate);
            this.gbCopies.Controls.Add(this.lblCount);
            this.gbCopies.Controls.Add(this.btnLocation);
            this.gbCopies.Controls.Add(this.btnDelCopy);
            this.gbCopies.Controls.Add(this.btnAddCopy);
            this.gbCopies.Controls.Add(this.cbCopy);
            this.gbCopies.Controls.Add(this.lblCopy);
            this.gbCopies.Controls.Add(this.lblLocation);
            this.gbCopies.Controls.Add(this.txtInvNum);
            this.gbCopies.Controls.Add(this.txtLocation);
            this.gbCopies.Controls.Add(this.lblInvNum);
            resources.ApplyResources(this.gbCopies, "gbCopies");
            this.gbCopies.Name = "gbCopies";
            this.gbCopies.TabStop = false;
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
            // chbExcluded
            // 
            resources.ApplyResources(this.chbExcluded, "chbExcluded");
            this.chbExcluded.Name = "chbExcluded";
            this.chbExcluded.UseVisualStyleBackColor = true;
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
            this.dtAcqDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtAcqDate, "dtAcqDate");
            this.dtAcqDate.Name = "dtAcqDate";
            // 
            // lblAcqDate
            // 
            resources.ApplyResources(this.lblAcqDate, "lblAcqDate");
            this.lblAcqDate.Name = "lblAcqDate";
            // 
            // lblCount
            // 
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            // 
            // btnLocation
            // 
            resources.ApplyResources(this.btnLocation, "btnLocation");
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.UseVisualStyleBackColor = true;
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnDelCopy
            // 
            resources.ApplyResources(this.btnDelCopy, "btnDelCopy");
            this.btnDelCopy.Name = "btnDelCopy";
            this.btnDelCopy.UseVisualStyleBackColor = true;
            this.btnDelCopy.Click += new System.EventHandler(this.btnDelCopy_Click);
            // 
            // btnAddCopy
            // 
            resources.ApplyResources(this.btnAddCopy, "btnAddCopy");
            this.btnAddCopy.Name = "btnAddCopy";
            this.btnAddCopy.UseVisualStyleBackColor = true;
            this.btnAddCopy.Click += new System.EventHandler(this.btnAddCopy_Click);
            // 
            // cbCopy
            // 
            this.cbCopy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCopy.FormattingEnabled = true;
            resources.ApplyResources(this.cbCopy, "cbCopy");
            this.cbCopy.Name = "cbCopy";
            this.cbCopy.SelectedIndexChanged += new System.EventHandler(this.cbCopy_SelectedIndexChanged);
            // 
            // lblCopy
            // 
            resources.ApplyResources(this.lblCopy, "lblCopy");
            this.lblCopy.Name = "lblCopy";
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
            // lblUpdated
            // 
            resources.ApplyResources(this.lblUpdated, "lblUpdated");
            this.lblUpdated.Name = "lblUpdated";
            // 
            // btnSaveNew
            // 
            resources.ApplyResources(this.btnSaveNew, "btnSaveNew");
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.UseVisualStyleBackColor = true;
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click);
            // 
            // TimeOut
            // 
            this.TimeOut.Interval = 200;
            this.TimeOut.Tick += new System.EventHandler(this.TimeOut_Tick);
            // 
            // frmEditItem
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSaveNew);
            this.Controls.Add(this.lblUpdated);
            this.Controls.Add(this.txtSubCategory);
            this.Controls.Add(this.lblSubCategory);
            this.Controls.Add(this.gbCopies);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.txtKeywords);
            this.Controls.Add(this.lblKeywords);
            this.Controls.Add(this.btnTag6);
            this.Controls.Add(this.btnTag5);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditItem_FormClosing);
            this.Load += new System.EventHandler(this.frmEditItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgImg)).EndInit();
            this.gbCopies.ResumeLayout(false);
            this.gbCopies.PerformLayout();
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
        private System.Windows.Forms.GroupBox gbCopies;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnLocation;
        private System.Windows.Forms.Button btnDelCopy;
        private System.Windows.Forms.Button btnAddCopy;
        private System.Windows.Forms.ComboBox cbCopy;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtInvNum;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblInvNum;
        private System.Windows.Forms.TextBox txtSubCategory;
        private System.Windows.Forms.Label lblSubCategory;
        private System.Windows.Forms.Label lblUpdated;
        private System.Windows.Forms.Button btnSaveNew;
        private System.Windows.Forms.Timer TimeOut;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.TextBox txtCondition;
        private System.Windows.Forms.CheckBox chbExcluded;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.DateTimePicker dtAcqDate;
        private System.Windows.Forms.Label lblAcqDate;
    }
}