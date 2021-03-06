﻿namespace Katalog
{
    partial class frmEditContacts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditContacts));
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSurname = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.lblNick = new System.Windows.Forms.Label();
            this.txtNick = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.gbAddress = new System.Windows.Forms.GroupBox();
            this.lblPostCode = new System.Windows.Forms.Label();
            this.txtPostCode = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.lblStreet = new System.Windows.Forms.Label();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.lblSex = new System.Windows.Forms.Label();
            this.cbSex = new System.Windows.Forms.ComboBox();
            this.lbURL = new System.Windows.Forms.Label();
            this.lblIM = new System.Windows.Forms.Label();
            this.dateBirth = new System.Windows.Forms.DateTimePicker();
            this.lblBirth = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblTag = new System.Windows.Forms.Label();
            this.txtTag = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnTag6 = new System.Windows.Forms.Button();
            this.btnTag5 = new System.Windows.Forms.Button();
            this.btnTag4 = new System.Windows.Forms.Button();
            this.btnTag3 = new System.Windows.Forms.Button();
            this.btnTag2 = new System.Windows.Forms.Button();
            this.btnTag1 = new System.Windows.Forms.Button();
            this.imgAvatar = new System.Windows.Forms.PictureBox();
            this.cbPhoneTag = new System.Windows.Forms.ComboBox();
            this.btnAddPhone = new System.Windows.Forms.Button();
            this.btnDelPhone = new System.Windows.Forms.Button();
            this.btnDelEmail = new System.Windows.Forms.Button();
            this.btnAddEmail = new System.Windows.Forms.Button();
            this.cbEmailTag = new System.Windows.Forms.ComboBox();
            this.btnDelURL = new System.Windows.Forms.Button();
            this.btnAddURL = new System.Windows.Forms.Button();
            this.cbURLTag = new System.Windows.Forms.ComboBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.cbPhone = new System.Windows.Forms.ComboBox();
            this.cbEmail = new System.Windows.Forms.ComboBox();
            this.cbURL = new System.Windows.Forms.ComboBox();
            this.chbActive = new System.Windows.Forms.CheckBox();
            this.btnSaveNew = new System.Windows.Forms.Button();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.TimeOut = new System.Windows.Forms.Timer(this.components);
            this.cbIM = new System.Windows.Forms.ComboBox();
            this.btnDelIM = new System.Windows.Forms.Button();
            this.btnAddIM = new System.Windows.Forms.Button();
            this.cbIMTag = new System.Windows.Forms.ComboBox();
            this.lblGoogleID = new System.Windows.Forms.Label();
            this.chbBirth = new System.Windows.Forms.CheckBox();
            this.gbAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // lblSurname
            // 
            resources.ApplyResources(this.lblSurname, "lblSurname");
            this.lblSurname.Name = "lblSurname";
            // 
            // txtSurname
            // 
            resources.ApplyResources(this.txtSurname, "txtSurname");
            this.txtSurname.Name = "txtSurname";
            // 
            // lblNick
            // 
            resources.ApplyResources(this.lblNick, "lblNick");
            this.lblNick.Name = "lblNick";
            // 
            // txtNick
            // 
            resources.ApplyResources(this.txtNick, "txtNick");
            this.txtNick.Name = "txtNick";
            // 
            // lblPhone
            // 
            resources.ApplyResources(this.lblPhone, "lblPhone");
            this.lblPhone.Name = "lblPhone";
            // 
            // lblEmail
            // 
            resources.ApplyResources(this.lblEmail, "lblEmail");
            this.lblEmail.Name = "lblEmail";
            // 
            // gbAddress
            // 
            this.gbAddress.Controls.Add(this.lblPostCode);
            this.gbAddress.Controls.Add(this.txtPostCode);
            this.gbAddress.Controls.Add(this.lblState);
            this.gbAddress.Controls.Add(this.txtState);
            this.gbAddress.Controls.Add(this.lblRegion);
            this.gbAddress.Controls.Add(this.txtRegion);
            this.gbAddress.Controls.Add(this.lblCity);
            this.gbAddress.Controls.Add(this.txtCity);
            this.gbAddress.Controls.Add(this.lblStreet);
            this.gbAddress.Controls.Add(this.txtStreet);
            resources.ApplyResources(this.gbAddress, "gbAddress");
            this.gbAddress.Name = "gbAddress";
            this.gbAddress.TabStop = false;
            // 
            // lblPostCode
            // 
            resources.ApplyResources(this.lblPostCode, "lblPostCode");
            this.lblPostCode.Name = "lblPostCode";
            // 
            // txtPostCode
            // 
            resources.ApplyResources(this.txtPostCode, "txtPostCode");
            this.txtPostCode.Name = "txtPostCode";
            // 
            // lblState
            // 
            resources.ApplyResources(this.lblState, "lblState");
            this.lblState.Name = "lblState";
            // 
            // txtState
            // 
            resources.ApplyResources(this.txtState, "txtState");
            this.txtState.Name = "txtState";
            // 
            // lblRegion
            // 
            resources.ApplyResources(this.lblRegion, "lblRegion");
            this.lblRegion.Name = "lblRegion";
            // 
            // txtRegion
            // 
            resources.ApplyResources(this.txtRegion, "txtRegion");
            this.txtRegion.Name = "txtRegion";
            // 
            // lblCity
            // 
            resources.ApplyResources(this.lblCity, "lblCity");
            this.lblCity.Name = "lblCity";
            // 
            // txtCity
            // 
            resources.ApplyResources(this.txtCity, "txtCity");
            this.txtCity.Name = "txtCity";
            // 
            // lblStreet
            // 
            resources.ApplyResources(this.lblStreet, "lblStreet");
            this.lblStreet.Name = "lblStreet";
            // 
            // txtStreet
            // 
            resources.ApplyResources(this.txtStreet, "txtStreet");
            this.txtStreet.Name = "txtStreet";
            // 
            // lblSex
            // 
            resources.ApplyResources(this.lblSex, "lblSex");
            this.lblSex.Name = "lblSex";
            // 
            // cbSex
            // 
            this.cbSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSex.FormattingEnabled = true;
            resources.ApplyResources(this.cbSex, "cbSex");
            this.cbSex.Name = "cbSex";
            // 
            // lbURL
            // 
            resources.ApplyResources(this.lbURL, "lbURL");
            this.lbURL.Name = "lbURL";
            // 
            // lblIM
            // 
            resources.ApplyResources(this.lblIM, "lblIM");
            this.lblIM.Name = "lblIM";
            // 
            // dateBirth
            // 
            this.dateBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dateBirth, "dateBirth");
            this.dateBirth.Name = "dateBirth";
            // 
            // lblBirth
            // 
            resources.ApplyResources(this.lblBirth, "lblBirth");
            this.lblBirth.Name = "lblBirth";
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
            // lblTag
            // 
            resources.ApplyResources(this.lblTag, "lblTag");
            this.lblTag.Name = "lblTag";
            // 
            // txtTag
            // 
            resources.ApplyResources(this.txtTag, "txtTag");
            this.txtTag.Name = "txtTag";
            // 
            // lblCode
            // 
            resources.ApplyResources(this.lblCode, "lblCode");
            this.lblCode.Name = "lblCode";
            // 
            // txtCode
            // 
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnTag6
            // 
            resources.ApplyResources(this.btnTag6, "btnTag6");
            this.btnTag6.Image = global::Katalog.Properties.Resources.Circle_Blue;
            this.btnTag6.Name = "btnTag6";
            this.btnTag6.UseVisualStyleBackColor = true;
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
            this.btnTag2.Image = global::Katalog.Properties.Resources.circ_red;
            this.btnTag2.Name = "btnTag2";
            this.btnTag2.UseVisualStyleBackColor = true;
            this.btnTag2.Click += new System.EventHandler(this.btnTag1_Click);
            // 
            // btnTag1
            // 
            resources.ApplyResources(this.btnTag1, "btnTag1");
            this.btnTag1.Image = global::Katalog.Properties.Resources.circ_green;
            this.btnTag1.Name = "btnTag1";
            this.btnTag1.UseVisualStyleBackColor = true;
            this.btnTag1.Click += new System.EventHandler(this.btnTag1_Click);
            // 
            // imgAvatar
            // 
            this.imgAvatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.imgAvatar, "imgAvatar");
            this.imgAvatar.Name = "imgAvatar";
            this.imgAvatar.TabStop = false;
            this.imgAvatar.Click += new System.EventHandler(this.imgAvatar_Click);
            // 
            // cbPhoneTag
            // 
            this.cbPhoneTag.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbPhoneTag.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbPhoneTag.FormattingEnabled = true;
            resources.ApplyResources(this.cbPhoneTag, "cbPhoneTag");
            this.cbPhoneTag.Name = "cbPhoneTag";
            this.cbPhoneTag.Leave += new System.EventHandler(this.cbPhoneTag_Leave);
            // 
            // btnAddPhone
            // 
            resources.ApplyResources(this.btnAddPhone, "btnAddPhone");
            this.btnAddPhone.Name = "btnAddPhone";
            this.btnAddPhone.UseVisualStyleBackColor = true;
            this.btnAddPhone.Click += new System.EventHandler(this.btnAddPhone_Click);
            // 
            // btnDelPhone
            // 
            resources.ApplyResources(this.btnDelPhone, "btnDelPhone");
            this.btnDelPhone.Name = "btnDelPhone";
            this.btnDelPhone.UseVisualStyleBackColor = true;
            this.btnDelPhone.Click += new System.EventHandler(this.btnDelPhone_Click);
            // 
            // btnDelEmail
            // 
            resources.ApplyResources(this.btnDelEmail, "btnDelEmail");
            this.btnDelEmail.Name = "btnDelEmail";
            this.btnDelEmail.UseVisualStyleBackColor = true;
            this.btnDelEmail.Click += new System.EventHandler(this.btnDelEmail_Click);
            // 
            // btnAddEmail
            // 
            resources.ApplyResources(this.btnAddEmail, "btnAddEmail");
            this.btnAddEmail.Name = "btnAddEmail";
            this.btnAddEmail.UseVisualStyleBackColor = true;
            this.btnAddEmail.Click += new System.EventHandler(this.btnAddEmail_Click);
            // 
            // cbEmailTag
            // 
            this.cbEmailTag.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbEmailTag.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbEmailTag.FormattingEnabled = true;
            resources.ApplyResources(this.cbEmailTag, "cbEmailTag");
            this.cbEmailTag.Name = "cbEmailTag";
            this.cbEmailTag.Leave += new System.EventHandler(this.cbEmailTag_Leave);
            // 
            // btnDelURL
            // 
            resources.ApplyResources(this.btnDelURL, "btnDelURL");
            this.btnDelURL.Name = "btnDelURL";
            this.btnDelURL.UseVisualStyleBackColor = true;
            this.btnDelURL.Click += new System.EventHandler(this.btnDelURL_Click);
            // 
            // btnAddURL
            // 
            resources.ApplyResources(this.btnAddURL, "btnAddURL");
            this.btnAddURL.Name = "btnAddURL";
            this.btnAddURL.UseVisualStyleBackColor = true;
            this.btnAddURL.Click += new System.EventHandler(this.btnAddURL_Click);
            // 
            // cbURLTag
            // 
            this.cbURLTag.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbURLTag.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbURLTag.FormattingEnabled = true;
            resources.ApplyResources(this.cbURLTag, "cbURLTag");
            this.cbURLTag.Name = "cbURLTag";
            this.cbURLTag.Leave += new System.EventHandler(this.cbURLTag_Leave);
            // 
            // lblCompany
            // 
            resources.ApplyResources(this.lblCompany, "lblCompany");
            this.lblCompany.Name = "lblCompany";
            // 
            // txtCompany
            // 
            resources.ApplyResources(this.txtCompany, "txtCompany");
            this.txtCompany.Name = "txtCompany";
            // 
            // lblPosition
            // 
            resources.ApplyResources(this.lblPosition, "lblPosition");
            this.lblPosition.Name = "lblPosition";
            // 
            // txtPosition
            // 
            resources.ApplyResources(this.txtPosition, "txtPosition");
            this.txtPosition.Name = "txtPosition";
            // 
            // cbPhone
            // 
            this.cbPhone.FormattingEnabled = true;
            resources.ApplyResources(this.cbPhone, "cbPhone");
            this.cbPhone.Name = "cbPhone";
            this.cbPhone.SelectedIndexChanged += new System.EventHandler(this.cbPhone_SelectedIndexChanged);
            this.cbPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbPhone_KeyDown);
            this.cbPhone.Leave += new System.EventHandler(this.cbPhone_Leave);
            // 
            // cbEmail
            // 
            this.cbEmail.FormattingEnabled = true;
            resources.ApplyResources(this.cbEmail, "cbEmail");
            this.cbEmail.Name = "cbEmail";
            this.cbEmail.SelectedIndexChanged += new System.EventHandler(this.cbEmail_SelectedIndexChanged);
            this.cbEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbEmail_KeyDown);
            this.cbEmail.Leave += new System.EventHandler(this.cbEmail_Leave);
            // 
            // cbURL
            // 
            this.cbURL.FormattingEnabled = true;
            resources.ApplyResources(this.cbURL, "cbURL");
            this.cbURL.Name = "cbURL";
            this.cbURL.SelectedIndexChanged += new System.EventHandler(this.cbURL_SelectedIndexChanged);
            this.cbURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbURL_KeyDown);
            this.cbURL.Leave += new System.EventHandler(this.cbURL_Leave);
            // 
            // chbActive
            // 
            resources.ApplyResources(this.chbActive, "chbActive");
            this.chbActive.Checked = true;
            this.chbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbActive.Name = "chbActive";
            this.chbActive.UseVisualStyleBackColor = true;
            // 
            // btnSaveNew
            // 
            resources.ApplyResources(this.btnSaveNew, "btnSaveNew");
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.UseVisualStyleBackColor = true;
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click);
            // 
            // lblLastUpdate
            // 
            resources.ApplyResources(this.lblLastUpdate, "lblLastUpdate");
            this.lblLastUpdate.Name = "lblLastUpdate";
            // 
            // TimeOut
            // 
            this.TimeOut.Interval = 200;
            this.TimeOut.Tick += new System.EventHandler(this.TimeOut_Tick);
            // 
            // cbIM
            // 
            this.cbIM.FormattingEnabled = true;
            resources.ApplyResources(this.cbIM, "cbIM");
            this.cbIM.Name = "cbIM";
            this.cbIM.SelectedIndexChanged += new System.EventHandler(this.cbIM_SelectedIndexChanged);
            this.cbIM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbIM_KeyDown);
            this.cbIM.Leave += new System.EventHandler(this.cbIM_Leave);
            // 
            // btnDelIM
            // 
            resources.ApplyResources(this.btnDelIM, "btnDelIM");
            this.btnDelIM.Name = "btnDelIM";
            this.btnDelIM.UseVisualStyleBackColor = true;
            this.btnDelIM.Click += new System.EventHandler(this.btnDelIM_Click);
            // 
            // btnAddIM
            // 
            resources.ApplyResources(this.btnAddIM, "btnAddIM");
            this.btnAddIM.Name = "btnAddIM";
            this.btnAddIM.UseVisualStyleBackColor = true;
            this.btnAddIM.Click += new System.EventHandler(this.btnAddIM_Click);
            // 
            // cbIMTag
            // 
            this.cbIMTag.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbIMTag.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbIMTag.FormattingEnabled = true;
            resources.ApplyResources(this.cbIMTag, "cbIMTag");
            this.cbIMTag.Name = "cbIMTag";
            this.cbIMTag.Leave += new System.EventHandler(this.cbIMTag_Leave);
            // 
            // lblGoogleID
            // 
            resources.ApplyResources(this.lblGoogleID, "lblGoogleID");
            this.lblGoogleID.Name = "lblGoogleID";
            // 
            // chbBirth
            // 
            resources.ApplyResources(this.chbBirth, "chbBirth");
            this.chbBirth.Name = "chbBirth";
            this.chbBirth.UseVisualStyleBackColor = true;
            // 
            // frmEditContacts
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbBirth);
            this.Controls.Add(this.lblGoogleID);
            this.Controls.Add(this.cbIM);
            this.Controls.Add(this.btnDelIM);
            this.Controls.Add(this.btnAddIM);
            this.Controls.Add(this.cbIMTag);
            this.Controls.Add(this.lblLastUpdate);
            this.Controls.Add(this.btnSaveNew);
            this.Controls.Add(this.chbActive);
            this.Controls.Add(this.cbURL);
            this.Controls.Add(this.cbEmail);
            this.Controls.Add(this.cbPhone);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.lblPosition);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.btnDelURL);
            this.Controls.Add(this.btnAddURL);
            this.Controls.Add(this.cbURLTag);
            this.Controls.Add(this.btnDelEmail);
            this.Controls.Add(this.btnAddEmail);
            this.Controls.Add(this.cbEmailTag);
            this.Controls.Add(this.btnDelPhone);
            this.Controls.Add(this.btnAddPhone);
            this.Controls.Add(this.cbPhoneTag);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.txtTag);
            this.Controls.Add(this.btnTag6);
            this.Controls.Add(this.btnTag5);
            this.Controls.Add(this.btnTag4);
            this.Controls.Add(this.btnTag3);
            this.Controls.Add(this.btnTag2);
            this.Controls.Add(this.btnTag1);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.lblBirth);
            this.Controls.Add(this.dateBirth);
            this.Controls.Add(this.lblIM);
            this.Controls.Add(this.lbURL);
            this.Controls.Add(this.cbSex);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.gbAddress);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblNick);
            this.Controls.Add(this.txtNick);
            this.Controls.Add(this.lblSurname);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.imgAvatar);
            this.Controls.Add(this.txtName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmEditContacts";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditContacts_FormClosing);
            this.Load += new System.EventHandler(this.frmEditContacts_Load);
            this.gbAddress.ResumeLayout(false);
            this.gbAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.PictureBox imgAvatar;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.Label lblNick;
        private System.Windows.Forms.TextBox txtNick;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.GroupBox gbAddress;
        private System.Windows.Forms.Label lblPostCode;
        private System.Windows.Forms.TextBox txtPostCode;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label lblStreet;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.ComboBox cbSex;
        private System.Windows.Forms.Label lbURL;
        private System.Windows.Forms.Label lblIM;
        private System.Windows.Forms.DateTimePicker dateBirth;
        private System.Windows.Forms.Label lblBirth;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Button btnTag1;
        private System.Windows.Forms.Button btnTag2;
        private System.Windows.Forms.Button btnTag3;
        private System.Windows.Forms.Button btnTag4;
        private System.Windows.Forms.Button btnTag5;
        private System.Windows.Forms.Button btnTag6;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.TextBox txtTag;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbPhoneTag;
        private System.Windows.Forms.Button btnAddPhone;
        private System.Windows.Forms.Button btnDelPhone;
        private System.Windows.Forms.Button btnDelEmail;
        private System.Windows.Forms.Button btnAddEmail;
        private System.Windows.Forms.ComboBox cbEmailTag;
        private System.Windows.Forms.Button btnDelURL;
        private System.Windows.Forms.Button btnAddURL;
        private System.Windows.Forms.ComboBox cbURLTag;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.ComboBox cbPhone;
        private System.Windows.Forms.ComboBox cbEmail;
        private System.Windows.Forms.ComboBox cbURL;
        private System.Windows.Forms.CheckBox chbActive;
        private System.Windows.Forms.Button btnSaveNew;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Timer TimeOut;
        private System.Windows.Forms.ComboBox cbIM;
        private System.Windows.Forms.Button btnDelIM;
        private System.Windows.Forms.Button btnAddIM;
        private System.Windows.Forms.ComboBox cbIMTag;
        private System.Windows.Forms.Label lblGoogleID;
        private System.Windows.Forms.CheckBox chbBirth;
    }
}