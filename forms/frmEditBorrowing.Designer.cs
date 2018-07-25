namespace Katalog
{
    partial class frmEditBorrowing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditBorrowing));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.lblItem = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.mnuGetPerson = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.jménoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.gbItem = new System.Windows.Forms.GroupBox();
            this.lblItemFormatInfo = new System.Windows.Forms.Label();
            this.btnDelItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.olvItem = new BrightIdeasSoftware.FastObjectListView();
            this.itName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itInvNumber = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itNote = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itInvNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gbPerson = new System.Windows.Forms.GroupBox();
            this.lblPersonNum = new System.Windows.Forms.Label();
            this.txtPerson = new System.Windows.Forms.TextBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.lblPerson = new System.Windows.Forms.Label();
            this.gbTerm = new System.Windows.Forms.GroupBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.TimeOut = new System.Windows.Forms.Timer(this.components);
            this.btnTag6 = new System.Windows.Forms.Button();
            this.btnTag5 = new System.Windows.Forms.Button();
            this.btnTag4 = new System.Windows.Forms.Button();
            this.btnTag3 = new System.Windows.Forms.Button();
            this.btnTag2 = new System.Windows.Forms.Button();
            this.btnTag1 = new System.Windows.Forms.Button();
            this.mnuGetPerson.SuspendLayout();
            this.gbItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvItem)).BeginInit();
            this.gbPerson.SuspendLayout();
            this.gbTerm.SuspendLayout();
            this.SuspendLayout();
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
            // dtFrom
            // 
            resources.ApplyResources(this.dtFrom, "dtFrom");
            this.dtFrom.Name = "dtFrom";
            // 
            // dtTo
            // 
            resources.ApplyResources(this.dtTo, "dtTo");
            this.dtTo.Name = "dtTo";
            // 
            // lblItem
            // 
            resources.ApplyResources(this.lblItem, "lblItem");
            this.lblItem.Name = "lblItem";
            // 
            // lblFrom
            // 
            resources.ApplyResources(this.lblFrom, "lblFrom");
            this.lblFrom.Name = "lblFrom";
            // 
            // lblTo
            // 
            resources.ApplyResources(this.lblTo, "lblTo");
            this.lblTo.Name = "lblTo";
            // 
            // mnuGetPerson
            // 
            resources.ApplyResources(this.mnuGetPerson, "mnuGetPerson");
            this.mnuGetPerson.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuGetPerson.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jménoToolStripMenuItem});
            this.mnuGetPerson.Name = "mnuGetPerson";
            // 
            // jménoToolStripMenuItem
            // 
            resources.ApplyResources(this.jménoToolStripMenuItem, "jménoToolStripMenuItem");
            this.jménoToolStripMenuItem.Name = "jménoToolStripMenuItem";
            // 
            // txtItem
            // 
            resources.ApplyResources(this.txtItem, "txtItem");
            this.txtItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtItem.Name = "txtItem";
            this.txtItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItem_KeyDown);
            // 
            // gbItem
            // 
            resources.ApplyResources(this.gbItem, "gbItem");
            this.gbItem.Controls.Add(this.lblItemFormatInfo);
            this.gbItem.Controls.Add(this.btnDelItem);
            this.gbItem.Controls.Add(this.btnAddItem);
            this.gbItem.Controls.Add(this.olvItem);
            this.gbItem.Controls.Add(this.lblItem);
            this.gbItem.Controls.Add(this.txtItem);
            this.gbItem.Name = "gbItem";
            this.gbItem.TabStop = false;
            // 
            // lblItemFormatInfo
            // 
            resources.ApplyResources(this.lblItemFormatInfo, "lblItemFormatInfo");
            this.lblItemFormatInfo.Name = "lblItemFormatInfo";
            // 
            // btnDelItem
            // 
            resources.ApplyResources(this.btnDelItem, "btnDelItem");
            this.btnDelItem.Name = "btnDelItem";
            this.btnDelItem.UseVisualStyleBackColor = true;
            this.btnDelItem.Click += new System.EventHandler(this.btnDelItem_Click);
            // 
            // btnAddItem
            // 
            resources.ApplyResources(this.btnAddItem, "btnAddItem");
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // olvItem
            // 
            resources.ApplyResources(this.olvItem, "olvItem");
            this.olvItem.AllColumns.Add(this.itName);
            this.olvItem.AllColumns.Add(this.itInvNumber);
            this.olvItem.AllColumns.Add(this.itNote);
            this.olvItem.CellEditUseWholeCell = false;
            this.olvItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itName,
            this.itInvNumber,
            this.itNote});
            this.olvItem.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvItem.FullRowSelect = true;
            this.olvItem.GridLines = true;
            this.olvItem.Name = "olvItem";
            this.olvItem.OverlayText.Text = resources.GetString("resource.Text");
            this.olvItem.ShowGroups = false;
            this.olvItem.TabStop = false;
            this.olvItem.UseCompatibleStateImageBehavior = false;
            this.olvItem.View = System.Windows.Forms.View.Details;
            this.olvItem.VirtualMode = true;
            this.olvItem.SelectedIndexChanged += new System.EventHandler(this.olvItem_SelectedIndexChanged);
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
            // itNote
            // 
            resources.ApplyResources(this.itNote, "itNote");
            // 
            // itInvNum
            // 
            resources.ApplyResources(this.itInvNum, "itInvNum");
            // 
            // gbPerson
            // 
            resources.ApplyResources(this.gbPerson, "gbPerson");
            this.gbPerson.Controls.Add(this.lblPersonNum);
            this.gbPerson.Controls.Add(this.txtPerson);
            this.gbPerson.Controls.Add(this.btnAddPerson);
            this.gbPerson.Controls.Add(this.lblPerson);
            this.gbPerson.Name = "gbPerson";
            this.gbPerson.TabStop = false;
            // 
            // lblPersonNum
            // 
            resources.ApplyResources(this.lblPersonNum, "lblPersonNum");
            this.lblPersonNum.Name = "lblPersonNum";
            // 
            // txtPerson
            // 
            resources.ApplyResources(this.txtPerson, "txtPerson");
            this.txtPerson.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtPerson.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPerson.Name = "txtPerson";
            this.txtPerson.TextChanged += new System.EventHandler(this.txtPerson_TextChanged);
            this.txtPerson.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPerson_KeyDown);
            // 
            // btnAddPerson
            // 
            resources.ApplyResources(this.btnAddPerson, "btnAddPerson");
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // lblPerson
            // 
            resources.ApplyResources(this.lblPerson, "lblPerson");
            this.lblPerson.Name = "lblPerson";
            // 
            // gbTerm
            // 
            resources.ApplyResources(this.gbTerm, "gbTerm");
            this.gbTerm.Controls.Add(this.lblFrom);
            this.gbTerm.Controls.Add(this.dtFrom);
            this.gbTerm.Controls.Add(this.dtTo);
            this.gbTerm.Controls.Add(this.lblTo);
            this.gbTerm.Name = "gbTerm";
            this.gbTerm.TabStop = false;
            // 
            // cbStatus
            // 
            resources.ApplyResources(this.cbStatus, "cbStatus");
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Name = "cbStatus";
            // 
            // TimeOut
            // 
            this.TimeOut.Interval = 200;
            this.TimeOut.Tick += new System.EventHandler(this.TimeOut_Tick);
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
            // frmEditBorrowing
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnTag6);
            this.Controls.Add(this.btnTag5);
            this.Controls.Add(this.btnTag4);
            this.Controls.Add(this.btnTag3);
            this.Controls.Add(this.btnTag2);
            this.Controls.Add(this.btnTag1);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.gbTerm);
            this.Controls.Add(this.gbPerson);
            this.Controls.Add(this.gbItem);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmEditBorrowing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditBorrowing_FormClosing);
            this.Load += new System.EventHandler(this.frmEditBorrowing_Load);
            this.mnuGetPerson.ResumeLayout(false);
            this.gbItem.ResumeLayout(false);
            this.gbItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvItem)).EndInit();
            this.gbPerson.ResumeLayout(false);
            this.gbPerson.PerformLayout();
            this.gbTerm.ResumeLayout(false);
            this.gbTerm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.ContextMenuStrip mnuGetPerson;
        private System.Windows.Forms.ToolStripMenuItem jménoToolStripMenuItem;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.GroupBox gbItem;
        private System.Windows.Forms.GroupBox gbPerson;
        private System.Windows.Forms.Label lblPersonNum;
        private System.Windows.Forms.TextBox txtPerson;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.GroupBox gbTerm;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Button btnAddItem;
        private BrightIdeasSoftware.FastObjectListView olvItem;
        private BrightIdeasSoftware.OLVColumn itName;
        private BrightIdeasSoftware.OLVColumn itInvNumber;
        private System.Windows.Forms.Button btnDelItem;
        private System.Windows.Forms.Timer TimeOut;
        private BrightIdeasSoftware.OLVColumn itInvNum;
        private System.Windows.Forms.Label lblItemFormatInfo;
        private BrightIdeasSoftware.OLVColumn itNote;
        private System.Windows.Forms.Button btnTag6;
        private System.Windows.Forms.Button btnTag5;
        private System.Windows.Forms.Button btnTag4;
        private System.Windows.Forms.Button btnTag3;
        private System.Windows.Forms.Button btnTag2;
        private System.Windows.Forms.Button btnTag1;
    }
}