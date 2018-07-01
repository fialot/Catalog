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
            this.lblItemType = new System.Windows.Forms.Label();
            this.lblItem = new System.Windows.Forms.Label();
            this.cbItemType = new System.Windows.Forms.ComboBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.mnuGetPerson = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.jménoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.cbItemNum = new System.Windows.Forms.ComboBox();
            this.lblItemNum = new System.Windows.Forms.Label();
            this.gbItem = new System.Windows.Forms.GroupBox();
            this.lblInvNum = new System.Windows.Forms.Label();
            this.gbPerson = new System.Windows.Forms.GroupBox();
            this.lblPersonNum = new System.Windows.Forms.Label();
            this.txtPerson = new System.Windows.Forms.TextBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.lblPerson = new System.Windows.Forms.Label();
            this.gbTerm = new System.Windows.Forms.GroupBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.mnuGetPerson.SuspendLayout();
            this.gbItem.SuspendLayout();
            this.gbPerson.SuspendLayout();
            this.gbTerm.SuspendLayout();
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
            // lblItemType
            // 
            resources.ApplyResources(this.lblItemType, "lblItemType");
            this.lblItemType.Name = "lblItemType";
            // 
            // lblItem
            // 
            resources.ApplyResources(this.lblItem, "lblItem");
            this.lblItem.Name = "lblItem";
            // 
            // cbItemType
            // 
            this.cbItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemType.FormattingEnabled = true;
            resources.ApplyResources(this.cbItemType, "cbItemType");
            this.cbItemType.Name = "cbItemType";
            this.cbItemType.SelectedIndexChanged += new System.EventHandler(this.cbItemType_SelectedIndexChanged);
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
            this.mnuGetPerson.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuGetPerson.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jménoToolStripMenuItem});
            this.mnuGetPerson.Name = "mnuGetPerson";
            resources.ApplyResources(this.mnuGetPerson, "mnuGetPerson");
            // 
            // jménoToolStripMenuItem
            // 
            this.jménoToolStripMenuItem.Name = "jménoToolStripMenuItem";
            resources.ApplyResources(this.jménoToolStripMenuItem, "jménoToolStripMenuItem");
            // 
            // txtItem
            // 
            this.txtItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            resources.ApplyResources(this.txtItem, "txtItem");
            this.txtItem.Name = "txtItem";
            this.txtItem.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // cbItemNum
            // 
            this.cbItemNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemNum.FormattingEnabled = true;
            resources.ApplyResources(this.cbItemNum, "cbItemNum");
            this.cbItemNum.Name = "cbItemNum";
            this.cbItemNum.SelectedIndexChanged += new System.EventHandler(this.cbItemNum_SelectedIndexChanged);
            // 
            // lblItemNum
            // 
            resources.ApplyResources(this.lblItemNum, "lblItemNum");
            this.lblItemNum.Name = "lblItemNum";
            // 
            // gbItem
            // 
            this.gbItem.Controls.Add(this.lblInvNum);
            this.gbItem.Controls.Add(this.lblItemType);
            this.gbItem.Controls.Add(this.lblItemNum);
            this.gbItem.Controls.Add(this.lblItem);
            this.gbItem.Controls.Add(this.cbItemNum);
            this.gbItem.Controls.Add(this.cbItemType);
            this.gbItem.Controls.Add(this.txtItem);
            resources.ApplyResources(this.gbItem, "gbItem");
            this.gbItem.Name = "gbItem";
            this.gbItem.TabStop = false;
            // 
            // lblInvNum
            // 
            resources.ApplyResources(this.lblInvNum, "lblInvNum");
            this.lblInvNum.Name = "lblInvNum";
            // 
            // gbPerson
            // 
            this.gbPerson.Controls.Add(this.lblPersonNum);
            this.gbPerson.Controls.Add(this.txtPerson);
            this.gbPerson.Controls.Add(this.btnAddPerson);
            this.gbPerson.Controls.Add(this.lblPerson);
            resources.ApplyResources(this.gbPerson, "gbPerson");
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
            this.txtPerson.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtPerson.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            resources.ApplyResources(this.txtPerson, "txtPerson");
            this.txtPerson.Name = "txtPerson";
            this.txtPerson.TextChanged += new System.EventHandler(this.txtPerson_TextChanged);
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
            this.gbTerm.Controls.Add(this.lblFrom);
            this.gbTerm.Controls.Add(this.dtFrom);
            this.gbTerm.Controls.Add(this.dtTo);
            this.gbTerm.Controls.Add(this.lblTo);
            resources.ApplyResources(this.gbTerm, "gbTerm");
            this.gbTerm.Name = "gbTerm";
            this.gbTerm.TabStop = false;
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            resources.ApplyResources(this.cbStatus, "cbStatus");
            this.cbStatus.Name = "cbStatus";
            // 
            // frmEditBorrowing
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.gbTerm);
            this.Controls.Add(this.gbPerson);
            this.Controls.Add(this.gbItem);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmEditBorrowing";
            this.Load += new System.EventHandler(this.frmEditBorrowing_Load);
            this.mnuGetPerson.ResumeLayout(false);
            this.gbItem.ResumeLayout(false);
            this.gbItem.PerformLayout();
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
        private System.Windows.Forms.Label lblItemType;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.ComboBox cbItemType;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.ContextMenuStrip mnuGetPerson;
        private System.Windows.Forms.ToolStripMenuItem jménoToolStripMenuItem;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.ComboBox cbItemNum;
        private System.Windows.Forms.Label lblItemNum;
        private System.Windows.Forms.GroupBox gbItem;
        private System.Windows.Forms.Label lblInvNum;
        private System.Windows.Forms.GroupBox gbPerson;
        private System.Windows.Forms.Label lblPersonNum;
        private System.Windows.Forms.TextBox txtPerson;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.GroupBox gbTerm;
        private System.Windows.Forms.ComboBox cbStatus;
    }
}