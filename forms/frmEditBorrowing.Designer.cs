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
            this.lblPerson = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.chbReturned = new System.Windows.Forms.CheckBox();
            this.mnuGetPerson = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.jménoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtPerson = new System.Windows.Forms.TextBox();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.mnuGetPerson.SuspendLayout();
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
            // 
            // lblPerson
            // 
            resources.ApplyResources(this.lblPerson, "lblPerson");
            this.lblPerson.Name = "lblPerson";
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
            // btnAddPerson
            // 
            resources.ApplyResources(this.btnAddPerson, "btnAddPerson");
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            // 
            // chbReturned
            // 
            resources.ApplyResources(this.chbReturned, "chbReturned");
            this.chbReturned.Name = "chbReturned";
            this.chbReturned.UseVisualStyleBackColor = true;
            // 
            // mnuGetPerson
            // 
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
            // txtPerson
            // 
            this.txtPerson.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtPerson.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            resources.ApplyResources(this.txtPerson, "txtPerson");
            this.txtPerson.Name = "txtPerson";
            this.txtPerson.TextChanged += new System.EventHandler(this.txtPerson_TextChanged);
            // 
            // txtItem
            // 
            this.txtItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            resources.ApplyResources(this.txtItem, "txtItem");
            this.txtItem.Name = "txtItem";
            this.txtItem.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // frmEditBorrowing
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtItem);
            this.Controls.Add(this.txtPerson);
            this.Controls.Add(this.chbReturned);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.lblPerson);
            this.Controls.Add(this.cbItemType);
            this.Controls.Add(this.lblItem);
            this.Controls.Add(this.lblItemType);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmEditBorrowing";
            this.Load += new System.EventHandler(this.frmEditBorrowing_Load);
            this.mnuGetPerson.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label lblItemType;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.ComboBox cbItemType;
        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.CheckBox chbReturned;
        private System.Windows.Forms.ContextMenuStrip mnuGetPerson;
        private System.Windows.Forms.ToolStripMenuItem jménoToolStripMenuItem;
        private System.Windows.Forms.TextBox txtPerson;
        private System.Windows.Forms.TextBox txtItem;
    }
}