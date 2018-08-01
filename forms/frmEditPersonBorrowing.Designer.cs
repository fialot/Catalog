namespace Katalog
{
    partial class frmEditPersonBorrowing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditPersonBorrowing));
            this.gbPerson = new System.Windows.Forms.GroupBox();
            this.btnPrintLend = new System.Windows.Forms.Button();
            this.lblPersonNum = new System.Windows.Forms.Label();
            this.lblPerson = new System.Windows.Forms.Label();
            this.gbItem = new System.Windows.Forms.GroupBox();
            this.btnLend = new System.Windows.Forms.Button();
            this.btnCancelAll = new System.Windows.Forms.Button();
            this.btnReturnAll = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.olvItem = new BrightIdeasSoftware.FastObjectListView();
            this.itName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itInvNumber = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itFrom = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itTo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itNote = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imgOLV = new System.Windows.Forms.ImageList(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.TimeOut = new System.Windows.Forms.Timer(this.components);
            this.gbPerson.SuspendLayout();
            this.gbItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvItem)).BeginInit();
            this.SuspendLayout();
            // 
            // gbPerson
            // 
            resources.ApplyResources(this.gbPerson, "gbPerson");
            this.gbPerson.Controls.Add(this.btnPrintLend);
            this.gbPerson.Controls.Add(this.lblPersonNum);
            this.gbPerson.Controls.Add(this.lblPerson);
            this.gbPerson.Name = "gbPerson";
            this.gbPerson.TabStop = false;
            // 
            // btnPrintLend
            // 
            resources.ApplyResources(this.btnPrintLend, "btnPrintLend");
            this.btnPrintLend.Name = "btnPrintLend";
            this.btnPrintLend.UseVisualStyleBackColor = true;
            this.btnPrintLend.Click += new System.EventHandler(this.btnPrintLend_Click);
            // 
            // lblPersonNum
            // 
            resources.ApplyResources(this.lblPersonNum, "lblPersonNum");
            this.lblPersonNum.Name = "lblPersonNum";
            // 
            // lblPerson
            // 
            resources.ApplyResources(this.lblPerson, "lblPerson");
            this.lblPerson.ForeColor = System.Drawing.Color.Red;
            this.lblPerson.Name = "lblPerson";
            // 
            // gbItem
            // 
            resources.ApplyResources(this.gbItem, "gbItem");
            this.gbItem.Controls.Add(this.btnLend);
            this.gbItem.Controls.Add(this.btnCancelAll);
            this.gbItem.Controls.Add(this.btnReturnAll);
            this.gbItem.Controls.Add(this.btnReturn);
            this.gbItem.Controls.Add(this.olvItem);
            this.gbItem.Name = "gbItem";
            this.gbItem.TabStop = false;
            // 
            // btnLend
            // 
            resources.ApplyResources(this.btnLend, "btnLend");
            this.btnLend.Name = "btnLend";
            this.btnLend.UseVisualStyleBackColor = true;
            this.btnLend.Click += new System.EventHandler(this.btnLend_Click);
            // 
            // btnCancelAll
            // 
            resources.ApplyResources(this.btnCancelAll, "btnCancelAll");
            this.btnCancelAll.Name = "btnCancelAll";
            this.btnCancelAll.UseVisualStyleBackColor = true;
            this.btnCancelAll.Click += new System.EventHandler(this.btnCancelAll_Click);
            // 
            // btnReturnAll
            // 
            resources.ApplyResources(this.btnReturnAll, "btnReturnAll");
            this.btnReturnAll.Name = "btnReturnAll";
            this.btnReturnAll.UseVisualStyleBackColor = true;
            this.btnReturnAll.Click += new System.EventHandler(this.btnReturnAll_Click);
            // 
            // btnReturn
            // 
            resources.ApplyResources(this.btnReturn, "btnReturn");
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // olvItem
            // 
            resources.ApplyResources(this.olvItem, "olvItem");
            this.olvItem.AllColumns.Add(this.itName);
            this.olvItem.AllColumns.Add(this.itInvNumber);
            this.olvItem.AllColumns.Add(this.itFrom);
            this.olvItem.AllColumns.Add(this.itTo);
            this.olvItem.AllColumns.Add(this.itStatus);
            this.olvItem.AllColumns.Add(this.itNote);
            this.olvItem.CellEditUseWholeCell = false;
            this.olvItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itName,
            this.itInvNumber,
            this.itFrom,
            this.itTo,
            this.itStatus,
            this.itNote});
            this.olvItem.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvItem.FullRowSelect = true;
            this.olvItem.GridLines = true;
            this.olvItem.Name = "olvItem";
            this.olvItem.OverlayText.Text = resources.GetString("resource.Text");
            this.olvItem.ShowGroups = false;
            this.olvItem.SmallImageList = this.imgOLV;
            this.olvItem.TabStop = false;
            this.olvItem.UseCompatibleStateImageBehavior = false;
            this.olvItem.View = System.Windows.Forms.View.Details;
            this.olvItem.VirtualMode = true;
            this.olvItem.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvItem_FormatRow);
            this.olvItem.SelectionChanged += new System.EventHandler(this.olvItem_SelectionChanged);
            // 
            // itName
            // 
            this.itName.AspectName = "";
            resources.ApplyResources(this.itName, "itName");
            // 
            // itInvNumber
            // 
            resources.ApplyResources(this.itInvNumber, "itInvNumber");
            // 
            // itFrom
            // 
            resources.ApplyResources(this.itFrom, "itFrom");
            // 
            // itTo
            // 
            resources.ApplyResources(this.itTo, "itTo");
            // 
            // itStatus
            // 
            resources.ApplyResources(this.itStatus, "itStatus");
            // 
            // itNote
            // 
            resources.ApplyResources(this.itNote, "itNote");
            // 
            // imgOLV
            // 
            this.imgOLV.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgOLV.ImageStream")));
            this.imgOLV.TransparentColor = System.Drawing.Color.Transparent;
            this.imgOLV.Images.SetKeyName(0, "Green");
            this.imgOLV.Images.SetKeyName(1, "Red");
            this.imgOLV.Images.SetKeyName(2, "Orange");
            this.imgOLV.Images.SetKeyName(3, "Yellow");
            this.imgOLV.Images.SetKeyName(4, "Grey");
            this.imgOLV.Images.SetKeyName(5, "Blue");
            this.imgOLV.Images.SetKeyName(6, "Yes");
            this.imgOLV.Images.SetKeyName(7, "No");
            this.imgOLV.Images.SetKeyName(8, "Stop");
            this.imgOLV.Images.SetKeyName(9, "Reserved");
            this.imgOLV.Images.SetKeyName(10, "Lend");
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // TimeOut
            // 
            this.TimeOut.Interval = 200;
            this.TimeOut.Tick += new System.EventHandler(this.TimeOut_Tick);
            // 
            // frmEditPersonBorrowing
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbPerson);
            this.Controls.Add(this.gbItem);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmEditPersonBorrowing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditPersonBorrowing_FormClosing);
            this.Load += new System.EventHandler(this.frmEditPersonBorrowing_Load);
            this.gbPerson.ResumeLayout(false);
            this.gbPerson.PerformLayout();
            this.gbItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPerson;
        private System.Windows.Forms.Button btnPrintLend;
        private System.Windows.Forms.Label lblPersonNum;
        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.GroupBox gbItem;
        private System.Windows.Forms.Button btnLend;
        private System.Windows.Forms.Button btnCancelAll;
        private System.Windows.Forms.Button btnReturnAll;
        private System.Windows.Forms.Button btnReturn;
        private BrightIdeasSoftware.FastObjectListView olvItem;
        private BrightIdeasSoftware.OLVColumn itName;
        private BrightIdeasSoftware.OLVColumn itInvNumber;
        private BrightIdeasSoftware.OLVColumn itFrom;
        private BrightIdeasSoftware.OLVColumn itTo;
        private BrightIdeasSoftware.OLVColumn itStatus;
        private BrightIdeasSoftware.OLVColumn itNote;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Timer TimeOut;
        private System.Windows.Forms.ImageList imgOLV;
    }
}