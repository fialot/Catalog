namespace Katalog
{
    partial class frmEditPersonLending
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditPersonLending));
            this.gbPerson = new System.Windows.Forms.GroupBox();
            this.lblPersonNum = new System.Windows.Forms.Label();
            this.lblPerson = new System.Windows.Forms.Label();
            this.gbItem = new System.Windows.Forms.GroupBox();
            this.btnLend = new System.Windows.Forms.Button();
            this.btnCancelAll = new System.Windows.Forms.Button();
            this.btnReturnAll = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.olvItem = new BrightIdeasSoftware.FastObjectListView();
            this.itName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itInvNumber = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itFrom = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itTo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itNote = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.TimeOut = new System.Windows.Forms.Timer(this.components);
            this.imgOLV = new System.Windows.Forms.ImageList(this.components);
            this.gbPerson.SuspendLayout();
            this.gbItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvItem)).BeginInit();
            this.SuspendLayout();
            // 
            // gbPerson
            // 
            this.gbPerson.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPerson.Controls.Add(this.lblPersonNum);
            this.gbPerson.Controls.Add(this.lblPerson);
            this.gbPerson.Location = new System.Drawing.Point(12, 12);
            this.gbPerson.Name = "gbPerson";
            this.gbPerson.Size = new System.Drawing.Size(691, 80);
            this.gbPerson.TabIndex = 76;
            this.gbPerson.TabStop = false;
            this.gbPerson.Text = "Person";
            // 
            // lblPersonNum
            // 
            this.lblPersonNum.AutoSize = true;
            this.lblPersonNum.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPersonNum.Location = new System.Drawing.Point(7, 55);
            this.lblPersonNum.Name = "lblPersonNum";
            this.lblPersonNum.Size = new System.Drawing.Size(87, 13);
            this.lblPersonNum.TabIndex = 14;
            this.lblPersonNum.Text = "Person number: -";
            // 
            // lblPerson
            // 
            this.lblPerson.AutoSize = true;
            this.lblPerson.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPerson.ForeColor = System.Drawing.Color.Red;
            this.lblPerson.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPerson.Location = new System.Drawing.Point(7, 16);
            this.lblPerson.Name = "lblPerson";
            this.lblPerson.Size = new System.Drawing.Size(106, 39);
            this.lblPerson.TabIndex = 11;
            this.lblPerson.Text = "Person";
            // 
            // gbItem
            // 
            this.gbItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbItem.Controls.Add(this.btnLend);
            this.gbItem.Controls.Add(this.btnCancelAll);
            this.gbItem.Controls.Add(this.btnReturnAll);
            this.gbItem.Controls.Add(this.btnReturn);
            this.gbItem.Controls.Add(this.olvItem);
            this.gbItem.Location = new System.Drawing.Point(12, 98);
            this.gbItem.Name = "gbItem";
            this.gbItem.Size = new System.Drawing.Size(691, 473);
            this.gbItem.TabIndex = 78;
            this.gbItem.TabStop = false;
            this.gbItem.Text = "Borrowed Items";
            // 
            // btnLend
            // 
            this.btnLend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLend.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLend.Location = new System.Drawing.Point(10, 444);
            this.btnLend.Name = "btnLend";
            this.btnLend.Size = new System.Drawing.Size(100, 23);
            this.btnLend.TabIndex = 81;
            this.btnLend.Text = "Lend";
            this.btnLend.UseVisualStyleBackColor = true;
            this.btnLend.Click += new System.EventHandler(this.btnLend_Click);
            // 
            // btnCancelAll
            // 
            this.btnCancelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancelAll.Location = new System.Drawing.Point(450, 444);
            this.btnCancelAll.Name = "btnCancelAll";
            this.btnCancelAll.Size = new System.Drawing.Size(151, 23);
            this.btnCancelAll.TabIndex = 83;
            this.btnCancelAll.Text = "Cancel all reservations";
            this.btnCancelAll.UseVisualStyleBackColor = true;
            this.btnCancelAll.Click += new System.EventHandler(this.btnCancelAll_Click);
            // 
            // btnReturnAll
            // 
            this.btnReturnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturnAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReturnAll.Location = new System.Drawing.Point(607, 444);
            this.btnReturnAll.Name = "btnReturnAll";
            this.btnReturnAll.Size = new System.Drawing.Size(75, 23);
            this.btnReturnAll.TabIndex = 82;
            this.btnReturnAll.Text = "Return all";
            this.btnReturnAll.UseVisualStyleBackColor = true;
            this.btnReturnAll.Click += new System.EventHandler(this.btnReturnAll_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReturn.Location = new System.Drawing.Point(116, 444);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(162, 23);
            this.btnReturn.TabIndex = 80;
            this.btnReturn.Text = "Return / Cancel reservation";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // olvItem
            // 
            this.olvItem.AllColumns.Add(this.itName);
            this.olvItem.AllColumns.Add(this.itType);
            this.olvItem.AllColumns.Add(this.itInvNumber);
            this.olvItem.AllColumns.Add(this.itFrom);
            this.olvItem.AllColumns.Add(this.itTo);
            this.olvItem.AllColumns.Add(this.itStatus);
            this.olvItem.AllColumns.Add(this.itNote);
            this.olvItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvItem.CellEditUseWholeCell = false;
            this.olvItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itName,
            this.itType,
            this.itInvNumber,
            this.itFrom,
            this.itTo,
            this.itStatus,
            this.itNote});
            this.olvItem.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvItem.FullRowSelect = true;
            this.olvItem.GridLines = true;
            this.olvItem.Location = new System.Drawing.Point(10, 19);
            this.olvItem.Name = "olvItem";
            this.olvItem.ShowGroups = false;
            this.olvItem.Size = new System.Drawing.Size(672, 419);
            this.olvItem.SmallImageList = this.imgOLV;
            this.olvItem.TabIndex = 36;
            this.olvItem.TabStop = false;
            this.olvItem.UseCompatibleStateImageBehavior = false;
            this.olvItem.View = System.Windows.Forms.View.Details;
            this.olvItem.VirtualMode = true;
            // 
            // itName
            // 
            this.itName.AspectName = "";
            this.itName.Text = "Name";
            this.itName.Width = 163;
            // 
            // itType
            // 
            this.itType.Text = "Type";
            // 
            // itInvNumber
            // 
            this.itInvNumber.Text = "Inv. Num.";
            this.itInvNumber.Width = 94;
            // 
            // itFrom
            // 
            this.itFrom.Text = "From";
            // 
            // itTo
            // 
            this.itTo.Text = "To";
            // 
            // itStatus
            // 
            this.itStatus.Text = "Status";
            // 
            // itNote
            // 
            this.itNote.Text = "Note";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(628, 577);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 80;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOk.Location = new System.Drawing.Point(547, 577);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 79;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // TimeOut
            // 
            this.TimeOut.Interval = 200;
            this.TimeOut.Tick += new System.EventHandler(this.TimeOut_Tick);
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
            // frmEditPersonLending
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 612);
            this.Controls.Add(this.gbPerson);
            this.Controls.Add(this.gbItem);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmEditPersonLending";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lending";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditPersonLending_FormClosing);
            this.Load += new System.EventHandler(this.frmEditPersonLending_Load);
            this.gbPerson.ResumeLayout(false);
            this.gbPerson.PerformLayout();
            this.gbItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPerson;
        private System.Windows.Forms.Label lblPersonNum;
        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.GroupBox gbItem;
        private BrightIdeasSoftware.FastObjectListView olvItem;
        private BrightIdeasSoftware.OLVColumn itName;
        private BrightIdeasSoftware.OLVColumn itInvNumber;
        private BrightIdeasSoftware.OLVColumn itNote;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnReturn;
        private BrightIdeasSoftware.OLVColumn itType;
        private BrightIdeasSoftware.OLVColumn itFrom;
        private BrightIdeasSoftware.OLVColumn itTo;
        private BrightIdeasSoftware.OLVColumn itStatus;
        private System.Windows.Forms.Button btnCancelAll;
        private System.Windows.Forms.Button btnReturnAll;
        private System.Windows.Forms.Button btnLend;
        private System.Windows.Forms.Timer TimeOut;
        private System.Windows.Forms.ImageList imgOLV;
    }
}