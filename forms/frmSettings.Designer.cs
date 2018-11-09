namespace Katalog
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.gbContacts = new System.Windows.Forms.GroupBox();
            this.lblConMinCharLen = new System.Windows.Forms.Label();
            this.txtConMinCharLen = new System.Windows.Forms.TextBox();
            this.lblConSuffix = new System.Windows.Forms.Label();
            this.lblConPrefix = new System.Windows.Forms.Label();
            this.txtConSuffix = new System.Windows.Forms.TextBox();
            this.txtConPrefix = new System.Windows.Forms.TextBox();
            this.lblConStart = new System.Windows.Forms.Label();
            this.txtConStart = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInvNums = new System.Windows.Forms.TabPage();
            this.gbBoardgames = new System.Windows.Forms.GroupBox();
            this.lblBoardMinCharLen = new System.Windows.Forms.Label();
            this.txtBoardMinCharLen = new System.Windows.Forms.TextBox();
            this.lblBoardSuffix = new System.Windows.Forms.Label();
            this.lblBoardPrefix = new System.Windows.Forms.Label();
            this.txtBoardSuffix = new System.Windows.Forms.TextBox();
            this.txtBoardPrefix = new System.Windows.Forms.TextBox();
            this.lblBoardStart = new System.Windows.Forms.Label();
            this.txtBoardStart = new System.Windows.Forms.TextBox();
            this.gbBooks = new System.Windows.Forms.GroupBox();
            this.chbUseISBN = new System.Windows.Forms.CheckBox();
            this.lblBookMinCharLen = new System.Windows.Forms.Label();
            this.txtBookMinCharLen = new System.Windows.Forms.TextBox();
            this.lblBookSuffix = new System.Windows.Forms.Label();
            this.lblBookPrefix = new System.Windows.Forms.Label();
            this.txtBookSuffix = new System.Windows.Forms.TextBox();
            this.txtBookPrefix = new System.Windows.Forms.TextBox();
            this.lblBookStart = new System.Windows.Forms.Label();
            this.txtBookStart = new System.Windows.Forms.TextBox();
            this.chbIncSpecimen = new System.Windows.Forms.CheckBox();
            this.gbItems = new System.Windows.Forms.GroupBox();
            this.lblItemMinCharLen = new System.Windows.Forms.Label();
            this.txtItemMinCharLen = new System.Windows.Forms.TextBox();
            this.lblItemSuffix = new System.Windows.Forms.Label();
            this.lblItemPrefix = new System.Windows.Forms.Label();
            this.txtItemSuffix = new System.Windows.Forms.TextBox();
            this.txtItemPrefix = new System.Windows.Forms.TextBox();
            this.lblItemStart = new System.Windows.Forms.Label();
            this.txtItemStart = new System.Windows.Forms.TextBox();
            this.tabOther = new System.Windows.Forms.TabPage();
            this.gbScanner = new System.Windows.Forms.GroupBox();
            this.btnScanRefresh = new System.Windows.Forms.Button();
            this.lblScanCOM = new System.Windows.Forms.Label();
            this.cbScanCOM = new System.Windows.Forms.ComboBox();
            this.tabRelativePaths = new System.Windows.Forms.TabPage();
            this.gbPathGames = new System.Windows.Forms.GroupBox();
            this.lblPathGames = new System.Windows.Forms.Label();
            this.txtPathGames = new System.Windows.Forms.TextBox();
            this.gmPathObjects = new System.Windows.Forms.GroupBox();
            this.lblPathObjects = new System.Windows.Forms.Label();
            this.txtPathObjects = new System.Windows.Forms.TextBox();
            this.gbPathBooks = new System.Windows.Forms.GroupBox();
            this.lblPathBooks = new System.Windows.Forms.Label();
            this.txtPathBooks = new System.Windows.Forms.TextBox();
            this.btnPathBooks = new System.Windows.Forms.Button();
            this.btnPathGames = new System.Windows.Forms.Button();
            this.btnPathObjects = new System.Windows.Forms.Button();
            this.gbContacts.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabInvNums.SuspendLayout();
            this.gbBoardgames.SuspendLayout();
            this.gbBooks.SuspendLayout();
            this.gbItems.SuspendLayout();
            this.tabOther.SuspendLayout();
            this.gbScanner.SuspendLayout();
            this.tabRelativePaths.SuspendLayout();
            this.gbPathGames.SuspendLayout();
            this.gmPathObjects.SuspendLayout();
            this.gbPathBooks.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(505, 426);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOk.Location = new System.Drawing.Point(424, 426);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 42;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gbContacts
            // 
            this.gbContacts.Controls.Add(this.lblConMinCharLen);
            this.gbContacts.Controls.Add(this.txtConMinCharLen);
            this.gbContacts.Controls.Add(this.lblConSuffix);
            this.gbContacts.Controls.Add(this.lblConPrefix);
            this.gbContacts.Controls.Add(this.txtConSuffix);
            this.gbContacts.Controls.Add(this.txtConPrefix);
            this.gbContacts.Controls.Add(this.lblConStart);
            this.gbContacts.Controls.Add(this.txtConStart);
            this.gbContacts.Location = new System.Drawing.Point(6, 6);
            this.gbContacts.Name = "gbContacts";
            this.gbContacts.Size = new System.Drawing.Size(548, 76);
            this.gbContacts.TabIndex = 44;
            this.gbContacts.TabStop = false;
            this.gbContacts.Text = "Contacts";
            // 
            // lblConMinCharLen
            // 
            this.lblConMinCharLen.AutoSize = true;
            this.lblConMinCharLen.Location = new System.Drawing.Point(226, 16);
            this.lblConMinCharLen.Name = "lblConMinCharLen";
            this.lblConMinCharLen.Size = new System.Drawing.Size(86, 13);
            this.lblConMinCharLen.TabIndex = 51;
            this.lblConMinCharLen.Text = "Min. char. length";
            // 
            // txtConMinCharLen
            // 
            this.txtConMinCharLen.Location = new System.Drawing.Point(229, 32);
            this.txtConMinCharLen.Name = "txtConMinCharLen";
            this.txtConMinCharLen.Size = new System.Drawing.Size(73, 20);
            this.txtConMinCharLen.TabIndex = 50;
            // 
            // lblConSuffix
            // 
            this.lblConSuffix.AutoSize = true;
            this.lblConSuffix.Location = new System.Drawing.Point(407, 16);
            this.lblConSuffix.Name = "lblConSuffix";
            this.lblConSuffix.Size = new System.Drawing.Size(33, 13);
            this.lblConSuffix.TabIndex = 49;
            this.lblConSuffix.Text = "Suffix";
            // 
            // lblConPrefix
            // 
            this.lblConPrefix.AutoSize = true;
            this.lblConPrefix.Location = new System.Drawing.Point(328, 16);
            this.lblConPrefix.Name = "lblConPrefix";
            this.lblConPrefix.Size = new System.Drawing.Size(33, 13);
            this.lblConPrefix.TabIndex = 47;
            this.lblConPrefix.Text = "Prefix";
            // 
            // txtConSuffix
            // 
            this.txtConSuffix.Location = new System.Drawing.Point(410, 32);
            this.txtConSuffix.Name = "txtConSuffix";
            this.txtConSuffix.Size = new System.Drawing.Size(73, 20);
            this.txtConSuffix.TabIndex = 48;
            // 
            // txtConPrefix
            // 
            this.txtConPrefix.Location = new System.Drawing.Point(331, 32);
            this.txtConPrefix.Name = "txtConPrefix";
            this.txtConPrefix.Size = new System.Drawing.Size(73, 20);
            this.txtConPrefix.TabIndex = 47;
            // 
            // lblConStart
            // 
            this.lblConStart.AutoSize = true;
            this.lblConStart.Location = new System.Drawing.Point(6, 16);
            this.lblConStart.Name = "lblConStart";
            this.lblConStart.Size = new System.Drawing.Size(132, 13);
            this.lblConStart.TabIndex = 46;
            this.lblConStart.Text = "Start inventory numbers by";
            // 
            // txtConStart
            // 
            this.txtConStart.Location = new System.Drawing.Point(9, 32);
            this.txtConStart.Name = "txtConStart";
            this.txtConStart.Size = new System.Drawing.Size(214, 20);
            this.txtConStart.TabIndex = 45;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabInvNums);
            this.tabControl1.Controls.Add(this.tabOther);
            this.tabControl1.Controls.Add(this.tabRelativePaths);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(568, 408);
            this.tabControl1.TabIndex = 45;
            // 
            // tabInvNums
            // 
            this.tabInvNums.Controls.Add(this.gbBoardgames);
            this.tabInvNums.Controls.Add(this.gbBooks);
            this.tabInvNums.Controls.Add(this.chbIncSpecimen);
            this.tabInvNums.Controls.Add(this.gbItems);
            this.tabInvNums.Controls.Add(this.gbContacts);
            this.tabInvNums.Location = new System.Drawing.Point(4, 22);
            this.tabInvNums.Name = "tabInvNums";
            this.tabInvNums.Padding = new System.Windows.Forms.Padding(3);
            this.tabInvNums.Size = new System.Drawing.Size(560, 382);
            this.tabInvNums.TabIndex = 0;
            this.tabInvNums.Text = "Inventory numbers";
            this.tabInvNums.UseVisualStyleBackColor = true;
            // 
            // gbBoardgames
            // 
            this.gbBoardgames.Controls.Add(this.lblBoardMinCharLen);
            this.gbBoardgames.Controls.Add(this.txtBoardMinCharLen);
            this.gbBoardgames.Controls.Add(this.lblBoardSuffix);
            this.gbBoardgames.Controls.Add(this.lblBoardPrefix);
            this.gbBoardgames.Controls.Add(this.txtBoardSuffix);
            this.gbBoardgames.Controls.Add(this.txtBoardPrefix);
            this.gbBoardgames.Controls.Add(this.lblBoardStart);
            this.gbBoardgames.Controls.Add(this.txtBoardStart);
            this.gbBoardgames.Location = new System.Drawing.Point(6, 273);
            this.gbBoardgames.Name = "gbBoardgames";
            this.gbBoardgames.Size = new System.Drawing.Size(548, 76);
            this.gbBoardgames.TabIndex = 53;
            this.gbBoardgames.TabStop = false;
            this.gbBoardgames.Text = "Board games";
            // 
            // lblBoardMinCharLen
            // 
            this.lblBoardMinCharLen.AutoSize = true;
            this.lblBoardMinCharLen.Location = new System.Drawing.Point(226, 16);
            this.lblBoardMinCharLen.Name = "lblBoardMinCharLen";
            this.lblBoardMinCharLen.Size = new System.Drawing.Size(86, 13);
            this.lblBoardMinCharLen.TabIndex = 53;
            this.lblBoardMinCharLen.Text = "Min. char. length";
            // 
            // txtBoardMinCharLen
            // 
            this.txtBoardMinCharLen.Location = new System.Drawing.Point(229, 32);
            this.txtBoardMinCharLen.Name = "txtBoardMinCharLen";
            this.txtBoardMinCharLen.Size = new System.Drawing.Size(73, 20);
            this.txtBoardMinCharLen.TabIndex = 52;
            // 
            // lblBoardSuffix
            // 
            this.lblBoardSuffix.AutoSize = true;
            this.lblBoardSuffix.Location = new System.Drawing.Point(407, 16);
            this.lblBoardSuffix.Name = "lblBoardSuffix";
            this.lblBoardSuffix.Size = new System.Drawing.Size(33, 13);
            this.lblBoardSuffix.TabIndex = 49;
            this.lblBoardSuffix.Text = "Suffix";
            // 
            // lblBoardPrefix
            // 
            this.lblBoardPrefix.AutoSize = true;
            this.lblBoardPrefix.Location = new System.Drawing.Point(328, 16);
            this.lblBoardPrefix.Name = "lblBoardPrefix";
            this.lblBoardPrefix.Size = new System.Drawing.Size(33, 13);
            this.lblBoardPrefix.TabIndex = 47;
            this.lblBoardPrefix.Text = "Prefix";
            // 
            // txtBoardSuffix
            // 
            this.txtBoardSuffix.Location = new System.Drawing.Point(410, 32);
            this.txtBoardSuffix.Name = "txtBoardSuffix";
            this.txtBoardSuffix.Size = new System.Drawing.Size(73, 20);
            this.txtBoardSuffix.TabIndex = 48;
            // 
            // txtBoardPrefix
            // 
            this.txtBoardPrefix.Location = new System.Drawing.Point(331, 32);
            this.txtBoardPrefix.Name = "txtBoardPrefix";
            this.txtBoardPrefix.Size = new System.Drawing.Size(73, 20);
            this.txtBoardPrefix.TabIndex = 47;
            // 
            // lblBoardStart
            // 
            this.lblBoardStart.AutoSize = true;
            this.lblBoardStart.Location = new System.Drawing.Point(6, 16);
            this.lblBoardStart.Name = "lblBoardStart";
            this.lblBoardStart.Size = new System.Drawing.Size(132, 13);
            this.lblBoardStart.TabIndex = 46;
            this.lblBoardStart.Text = "Start inventory numbers by";
            // 
            // txtBoardStart
            // 
            this.txtBoardStart.Location = new System.Drawing.Point(9, 32);
            this.txtBoardStart.Name = "txtBoardStart";
            this.txtBoardStart.Size = new System.Drawing.Size(214, 20);
            this.txtBoardStart.TabIndex = 45;
            // 
            // gbBooks
            // 
            this.gbBooks.Controls.Add(this.chbUseISBN);
            this.gbBooks.Controls.Add(this.lblBookMinCharLen);
            this.gbBooks.Controls.Add(this.txtBookMinCharLen);
            this.gbBooks.Controls.Add(this.lblBookSuffix);
            this.gbBooks.Controls.Add(this.lblBookPrefix);
            this.gbBooks.Controls.Add(this.txtBookSuffix);
            this.gbBooks.Controls.Add(this.txtBookPrefix);
            this.gbBooks.Controls.Add(this.lblBookStart);
            this.gbBooks.Controls.Add(this.txtBookStart);
            this.gbBooks.Location = new System.Drawing.Point(6, 170);
            this.gbBooks.Name = "gbBooks";
            this.gbBooks.Size = new System.Drawing.Size(548, 97);
            this.gbBooks.TabIndex = 52;
            this.gbBooks.TabStop = false;
            this.gbBooks.Text = "Books";
            // 
            // chbUseISBN
            // 
            this.chbUseISBN.AutoSize = true;
            this.chbUseISBN.Location = new System.Drawing.Point(9, 58);
            this.chbUseISBN.Name = "chbUseISBN";
            this.chbUseISBN.Size = new System.Drawing.Size(172, 17);
            this.chbUseISBN.TabIndex = 54;
            this.chbUseISBN.Text = "Use ISBN as Inventory number";
            this.chbUseISBN.UseVisualStyleBackColor = true;
            // 
            // lblBookMinCharLen
            // 
            this.lblBookMinCharLen.AutoSize = true;
            this.lblBookMinCharLen.Location = new System.Drawing.Point(226, 16);
            this.lblBookMinCharLen.Name = "lblBookMinCharLen";
            this.lblBookMinCharLen.Size = new System.Drawing.Size(86, 13);
            this.lblBookMinCharLen.TabIndex = 53;
            this.lblBookMinCharLen.Text = "Min. char. length";
            // 
            // txtBookMinCharLen
            // 
            this.txtBookMinCharLen.Location = new System.Drawing.Point(229, 32);
            this.txtBookMinCharLen.Name = "txtBookMinCharLen";
            this.txtBookMinCharLen.Size = new System.Drawing.Size(73, 20);
            this.txtBookMinCharLen.TabIndex = 52;
            // 
            // lblBookSuffix
            // 
            this.lblBookSuffix.AutoSize = true;
            this.lblBookSuffix.Location = new System.Drawing.Point(407, 16);
            this.lblBookSuffix.Name = "lblBookSuffix";
            this.lblBookSuffix.Size = new System.Drawing.Size(33, 13);
            this.lblBookSuffix.TabIndex = 49;
            this.lblBookSuffix.Text = "Suffix";
            // 
            // lblBookPrefix
            // 
            this.lblBookPrefix.AutoSize = true;
            this.lblBookPrefix.Location = new System.Drawing.Point(328, 16);
            this.lblBookPrefix.Name = "lblBookPrefix";
            this.lblBookPrefix.Size = new System.Drawing.Size(33, 13);
            this.lblBookPrefix.TabIndex = 47;
            this.lblBookPrefix.Text = "Prefix";
            // 
            // txtBookSuffix
            // 
            this.txtBookSuffix.Location = new System.Drawing.Point(410, 32);
            this.txtBookSuffix.Name = "txtBookSuffix";
            this.txtBookSuffix.Size = new System.Drawing.Size(73, 20);
            this.txtBookSuffix.TabIndex = 48;
            // 
            // txtBookPrefix
            // 
            this.txtBookPrefix.Location = new System.Drawing.Point(331, 32);
            this.txtBookPrefix.Name = "txtBookPrefix";
            this.txtBookPrefix.Size = new System.Drawing.Size(73, 20);
            this.txtBookPrefix.TabIndex = 47;
            // 
            // lblBookStart
            // 
            this.lblBookStart.AutoSize = true;
            this.lblBookStart.Location = new System.Drawing.Point(6, 16);
            this.lblBookStart.Name = "lblBookStart";
            this.lblBookStart.Size = new System.Drawing.Size(132, 13);
            this.lblBookStart.TabIndex = 46;
            this.lblBookStart.Text = "Start inventory numbers by";
            // 
            // txtBookStart
            // 
            this.txtBookStart.Location = new System.Drawing.Point(9, 32);
            this.txtBookStart.Name = "txtBookStart";
            this.txtBookStart.Size = new System.Drawing.Size(214, 20);
            this.txtBookStart.TabIndex = 45;
            // 
            // chbIncSpecimen
            // 
            this.chbIncSpecimen.AutoSize = true;
            this.chbIncSpecimen.Location = new System.Drawing.Point(6, 359);
            this.chbIncSpecimen.Name = "chbIncSpecimen";
            this.chbIncSpecimen.Size = new System.Drawing.Size(144, 17);
            this.chbIncSpecimen.TabIndex = 51;
            this.chbIncSpecimen.Text = "Increment new specimen";
            this.chbIncSpecimen.UseVisualStyleBackColor = true;
            // 
            // gbItems
            // 
            this.gbItems.Controls.Add(this.lblItemMinCharLen);
            this.gbItems.Controls.Add(this.txtItemMinCharLen);
            this.gbItems.Controls.Add(this.lblItemSuffix);
            this.gbItems.Controls.Add(this.lblItemPrefix);
            this.gbItems.Controls.Add(this.txtItemSuffix);
            this.gbItems.Controls.Add(this.txtItemPrefix);
            this.gbItems.Controls.Add(this.lblItemStart);
            this.gbItems.Controls.Add(this.txtItemStart);
            this.gbItems.Location = new System.Drawing.Point(6, 88);
            this.gbItems.Name = "gbItems";
            this.gbItems.Size = new System.Drawing.Size(548, 76);
            this.gbItems.TabIndex = 50;
            this.gbItems.TabStop = false;
            this.gbItems.Text = "Items";
            // 
            // lblItemMinCharLen
            // 
            this.lblItemMinCharLen.AutoSize = true;
            this.lblItemMinCharLen.Location = new System.Drawing.Point(226, 16);
            this.lblItemMinCharLen.Name = "lblItemMinCharLen";
            this.lblItemMinCharLen.Size = new System.Drawing.Size(86, 13);
            this.lblItemMinCharLen.TabIndex = 53;
            this.lblItemMinCharLen.Text = "Min. char. length";
            // 
            // txtItemMinCharLen
            // 
            this.txtItemMinCharLen.Location = new System.Drawing.Point(229, 32);
            this.txtItemMinCharLen.Name = "txtItemMinCharLen";
            this.txtItemMinCharLen.Size = new System.Drawing.Size(73, 20);
            this.txtItemMinCharLen.TabIndex = 52;
            // 
            // lblItemSuffix
            // 
            this.lblItemSuffix.AutoSize = true;
            this.lblItemSuffix.Location = new System.Drawing.Point(407, 16);
            this.lblItemSuffix.Name = "lblItemSuffix";
            this.lblItemSuffix.Size = new System.Drawing.Size(33, 13);
            this.lblItemSuffix.TabIndex = 49;
            this.lblItemSuffix.Text = "Suffix";
            // 
            // lblItemPrefix
            // 
            this.lblItemPrefix.AutoSize = true;
            this.lblItemPrefix.Location = new System.Drawing.Point(328, 16);
            this.lblItemPrefix.Name = "lblItemPrefix";
            this.lblItemPrefix.Size = new System.Drawing.Size(33, 13);
            this.lblItemPrefix.TabIndex = 47;
            this.lblItemPrefix.Text = "Prefix";
            // 
            // txtItemSuffix
            // 
            this.txtItemSuffix.Location = new System.Drawing.Point(410, 32);
            this.txtItemSuffix.Name = "txtItemSuffix";
            this.txtItemSuffix.Size = new System.Drawing.Size(73, 20);
            this.txtItemSuffix.TabIndex = 48;
            // 
            // txtItemPrefix
            // 
            this.txtItemPrefix.Location = new System.Drawing.Point(331, 32);
            this.txtItemPrefix.Name = "txtItemPrefix";
            this.txtItemPrefix.Size = new System.Drawing.Size(73, 20);
            this.txtItemPrefix.TabIndex = 47;
            // 
            // lblItemStart
            // 
            this.lblItemStart.AutoSize = true;
            this.lblItemStart.Location = new System.Drawing.Point(6, 16);
            this.lblItemStart.Name = "lblItemStart";
            this.lblItemStart.Size = new System.Drawing.Size(132, 13);
            this.lblItemStart.TabIndex = 46;
            this.lblItemStart.Text = "Start inventory numbers by";
            // 
            // txtItemStart
            // 
            this.txtItemStart.Location = new System.Drawing.Point(9, 32);
            this.txtItemStart.Name = "txtItemStart";
            this.txtItemStart.Size = new System.Drawing.Size(214, 20);
            this.txtItemStart.TabIndex = 45;
            // 
            // tabOther
            // 
            this.tabOther.Controls.Add(this.gbScanner);
            this.tabOther.Location = new System.Drawing.Point(4, 22);
            this.tabOther.Name = "tabOther";
            this.tabOther.Padding = new System.Windows.Forms.Padding(3);
            this.tabOther.Size = new System.Drawing.Size(560, 382);
            this.tabOther.TabIndex = 1;
            this.tabOther.Text = "Other";
            this.tabOther.UseVisualStyleBackColor = true;
            // 
            // gbScanner
            // 
            this.gbScanner.Controls.Add(this.btnScanRefresh);
            this.gbScanner.Controls.Add(this.lblScanCOM);
            this.gbScanner.Controls.Add(this.cbScanCOM);
            this.gbScanner.Location = new System.Drawing.Point(6, 6);
            this.gbScanner.Name = "gbScanner";
            this.gbScanner.Size = new System.Drawing.Size(548, 45);
            this.gbScanner.TabIndex = 2;
            this.gbScanner.TabStop = false;
            this.gbScanner.Text = "Barcode scanner";
            // 
            // btnScanRefresh
            // 
            this.btnScanRefresh.Image = global::Katalog.Properties.Resources.Refresh16;
            this.btnScanRefresh.Location = new System.Drawing.Point(171, 11);
            this.btnScanRefresh.Name = "btnScanRefresh";
            this.btnScanRefresh.Size = new System.Drawing.Size(26, 23);
            this.btnScanRefresh.TabIndex = 2;
            this.btnScanRefresh.UseVisualStyleBackColor = true;
            this.btnScanRefresh.Click += new System.EventHandler(this.btnScanRefresh_Click);
            // 
            // lblScanCOM
            // 
            this.lblScanCOM.AutoSize = true;
            this.lblScanCOM.Location = new System.Drawing.Point(6, 16);
            this.lblScanCOM.Name = "lblScanCOM";
            this.lblScanCOM.Size = new System.Drawing.Size(92, 13);
            this.lblScanCOM.TabIndex = 1;
            this.lblScanCOM.Text = "Scaner COM port:";
            // 
            // cbScanCOM
            // 
            this.cbScanCOM.FormattingEnabled = true;
            this.cbScanCOM.Location = new System.Drawing.Point(104, 13);
            this.cbScanCOM.Name = "cbScanCOM";
            this.cbScanCOM.Size = new System.Drawing.Size(61, 21);
            this.cbScanCOM.TabIndex = 0;
            // 
            // tabRelativePaths
            // 
            this.tabRelativePaths.Controls.Add(this.gbPathBooks);
            this.tabRelativePaths.Controls.Add(this.gmPathObjects);
            this.tabRelativePaths.Controls.Add(this.gbPathGames);
            this.tabRelativePaths.Location = new System.Drawing.Point(4, 22);
            this.tabRelativePaths.Name = "tabRelativePaths";
            this.tabRelativePaths.Size = new System.Drawing.Size(560, 382);
            this.tabRelativePaths.TabIndex = 2;
            this.tabRelativePaths.Text = "Relative Paths";
            this.tabRelativePaths.UseVisualStyleBackColor = true;
            // 
            // gbPathGames
            // 
            this.gbPathGames.Controls.Add(this.btnPathGames);
            this.gbPathGames.Controls.Add(this.lblPathGames);
            this.gbPathGames.Controls.Add(this.txtPathGames);
            this.gbPathGames.Location = new System.Drawing.Point(9, 98);
            this.gbPathGames.Name = "gbPathGames";
            this.gbPathGames.Size = new System.Drawing.Size(548, 76);
            this.gbPathGames.TabIndex = 54;
            this.gbPathGames.TabStop = false;
            this.gbPathGames.Text = "Games";
            // 
            // lblPathGames
            // 
            this.lblPathGames.AutoSize = true;
            this.lblPathGames.Location = new System.Drawing.Point(6, 16);
            this.lblPathGames.Name = "lblPathGames";
            this.lblPathGames.Size = new System.Drawing.Size(36, 13);
            this.lblPathGames.TabIndex = 46;
            this.lblPathGames.Text = "Folder";
            // 
            // txtPathGames
            // 
            this.txtPathGames.Location = new System.Drawing.Point(9, 32);
            this.txtPathGames.Name = "txtPathGames";
            this.txtPathGames.Size = new System.Drawing.Size(476, 20);
            this.txtPathGames.TabIndex = 45;
            // 
            // gmPathObjects
            // 
            this.gmPathObjects.Controls.Add(this.btnPathObjects);
            this.gmPathObjects.Controls.Add(this.lblPathObjects);
            this.gmPathObjects.Controls.Add(this.txtPathObjects);
            this.gmPathObjects.Location = new System.Drawing.Point(9, 180);
            this.gmPathObjects.Name = "gmPathObjects";
            this.gmPathObjects.Size = new System.Drawing.Size(548, 76);
            this.gmPathObjects.TabIndex = 55;
            this.gmPathObjects.TabStop = false;
            this.gmPathObjects.Text = "Objects";
            // 
            // lblPathObjects
            // 
            this.lblPathObjects.AutoSize = true;
            this.lblPathObjects.Location = new System.Drawing.Point(6, 16);
            this.lblPathObjects.Name = "lblPathObjects";
            this.lblPathObjects.Size = new System.Drawing.Size(36, 13);
            this.lblPathObjects.TabIndex = 46;
            this.lblPathObjects.Text = "Folder";
            // 
            // txtPathObjects
            // 
            this.txtPathObjects.Location = new System.Drawing.Point(9, 32);
            this.txtPathObjects.Name = "txtPathObjects";
            this.txtPathObjects.Size = new System.Drawing.Size(476, 20);
            this.txtPathObjects.TabIndex = 45;
            // 
            // gbPathBooks
            // 
            this.gbPathBooks.Controls.Add(this.btnPathBooks);
            this.gbPathBooks.Controls.Add(this.lblPathBooks);
            this.gbPathBooks.Controls.Add(this.txtPathBooks);
            this.gbPathBooks.Location = new System.Drawing.Point(9, 16);
            this.gbPathBooks.Name = "gbPathBooks";
            this.gbPathBooks.Size = new System.Drawing.Size(548, 76);
            this.gbPathBooks.TabIndex = 56;
            this.gbPathBooks.TabStop = false;
            this.gbPathBooks.Text = "Books";
            // 
            // lblPathBooks
            // 
            this.lblPathBooks.AutoSize = true;
            this.lblPathBooks.Location = new System.Drawing.Point(6, 16);
            this.lblPathBooks.Name = "lblPathBooks";
            this.lblPathBooks.Size = new System.Drawing.Size(36, 13);
            this.lblPathBooks.TabIndex = 46;
            this.lblPathBooks.Text = "Folder";
            // 
            // txtPathBooks
            // 
            this.txtPathBooks.Location = new System.Drawing.Point(9, 32);
            this.txtPathBooks.Name = "txtPathBooks";
            this.txtPathBooks.Size = new System.Drawing.Size(476, 20);
            this.txtPathBooks.TabIndex = 45;
            // 
            // btnPathBooks
            // 
            this.btnPathBooks.Location = new System.Drawing.Point(491, 30);
            this.btnPathBooks.Name = "btnPathBooks";
            this.btnPathBooks.Size = new System.Drawing.Size(26, 23);
            this.btnPathBooks.TabIndex = 47;
            this.btnPathBooks.Text = "...";
            this.btnPathBooks.UseVisualStyleBackColor = true;
            this.btnPathBooks.Click += new System.EventHandler(this.btnPathBooks_Click);
            // 
            // btnPathGames
            // 
            this.btnPathGames.Location = new System.Drawing.Point(491, 30);
            this.btnPathGames.Name = "btnPathGames";
            this.btnPathGames.Size = new System.Drawing.Size(26, 23);
            this.btnPathGames.TabIndex = 48;
            this.btnPathGames.Text = "...";
            this.btnPathGames.UseVisualStyleBackColor = true;
            this.btnPathGames.Click += new System.EventHandler(this.btnPathGames_Click);
            // 
            // btnPathObjects
            // 
            this.btnPathObjects.Location = new System.Drawing.Point(491, 30);
            this.btnPathObjects.Name = "btnPathObjects";
            this.btnPathObjects.Size = new System.Drawing.Size(26, 23);
            this.btnPathObjects.TabIndex = 48;
            this.btnPathObjects.Text = "...";
            this.btnPathObjects.UseVisualStyleBackColor = true;
            this.btnPathObjects.Click += new System.EventHandler(this.btnPathObjects_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 461);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.gbContacts.ResumeLayout(false);
            this.gbContacts.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabInvNums.ResumeLayout(false);
            this.tabInvNums.PerformLayout();
            this.gbBoardgames.ResumeLayout(false);
            this.gbBoardgames.PerformLayout();
            this.gbBooks.ResumeLayout(false);
            this.gbBooks.PerformLayout();
            this.gbItems.ResumeLayout(false);
            this.gbItems.PerformLayout();
            this.tabOther.ResumeLayout(false);
            this.gbScanner.ResumeLayout(false);
            this.gbScanner.PerformLayout();
            this.tabRelativePaths.ResumeLayout(false);
            this.gbPathGames.ResumeLayout(false);
            this.gbPathGames.PerformLayout();
            this.gmPathObjects.ResumeLayout(false);
            this.gmPathObjects.PerformLayout();
            this.gbPathBooks.ResumeLayout(false);
            this.gbPathBooks.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox gbContacts;
        private System.Windows.Forms.Label lblConStart;
        private System.Windows.Forms.TextBox txtConStart;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInvNums;
        private System.Windows.Forms.TabPage tabOther;
        private System.Windows.Forms.Label lblConSuffix;
        private System.Windows.Forms.Label lblConPrefix;
        private System.Windows.Forms.TextBox txtConSuffix;
        private System.Windows.Forms.TextBox txtConPrefix;
        private System.Windows.Forms.GroupBox gbItems;
        private System.Windows.Forms.Label lblItemSuffix;
        private System.Windows.Forms.Label lblItemPrefix;
        private System.Windows.Forms.TextBox txtItemSuffix;
        private System.Windows.Forms.TextBox txtItemPrefix;
        private System.Windows.Forms.Label lblItemStart;
        private System.Windows.Forms.TextBox txtItemStart;
        private System.Windows.Forms.Label lblConMinCharLen;
        private System.Windows.Forms.TextBox txtConMinCharLen;
        private System.Windows.Forms.Label lblItemMinCharLen;
        private System.Windows.Forms.TextBox txtItemMinCharLen;
        private System.Windows.Forms.GroupBox gbBooks;
        private System.Windows.Forms.Label lblBookMinCharLen;
        private System.Windows.Forms.TextBox txtBookMinCharLen;
        private System.Windows.Forms.Label lblBookSuffix;
        private System.Windows.Forms.Label lblBookPrefix;
        private System.Windows.Forms.TextBox txtBookSuffix;
        private System.Windows.Forms.TextBox txtBookPrefix;
        private System.Windows.Forms.Label lblBookStart;
        private System.Windows.Forms.TextBox txtBookStart;
        private System.Windows.Forms.CheckBox chbIncSpecimen;
        private System.Windows.Forms.GroupBox gbScanner;
        private System.Windows.Forms.Label lblScanCOM;
        private System.Windows.Forms.ComboBox cbScanCOM;
        private System.Windows.Forms.Button btnScanRefresh;
        private System.Windows.Forms.CheckBox chbUseISBN;
        private System.Windows.Forms.GroupBox gbBoardgames;
        private System.Windows.Forms.Label lblBoardMinCharLen;
        private System.Windows.Forms.TextBox txtBoardMinCharLen;
        private System.Windows.Forms.Label lblBoardSuffix;
        private System.Windows.Forms.Label lblBoardPrefix;
        private System.Windows.Forms.TextBox txtBoardSuffix;
        private System.Windows.Forms.TextBox txtBoardPrefix;
        private System.Windows.Forms.Label lblBoardStart;
        private System.Windows.Forms.TextBox txtBoardStart;
        private System.Windows.Forms.TabPage tabRelativePaths;
        private System.Windows.Forms.GroupBox gbPathBooks;
        private System.Windows.Forms.Button btnPathBooks;
        private System.Windows.Forms.Label lblPathBooks;
        private System.Windows.Forms.TextBox txtPathBooks;
        private System.Windows.Forms.GroupBox gmPathObjects;
        private System.Windows.Forms.Button btnPathObjects;
        private System.Windows.Forms.Label lblPathObjects;
        private System.Windows.Forms.TextBox txtPathObjects;
        private System.Windows.Forms.GroupBox gbPathGames;
        private System.Windows.Forms.Button btnPathGames;
        private System.Windows.Forms.Label lblPathGames;
        private System.Windows.Forms.TextBox txtPathGames;
    }
}