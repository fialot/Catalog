namespace Katalog
{
    partial class frmEditFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditFile));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtName.Name = "txtName";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // txtPath
            // 
            resources.ApplyResources(this.txtPath, "txtPath");
            this.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPath.Name = "txtPath";
            // 
            // lblPath
            // 
            resources.ApplyResources(this.lblPath, "lblPath");
            this.lblPath.Name = "lblPath";
            // 
            // txtVersion
            // 
            resources.ApplyResources(this.txtVersion, "txtVersion");
            this.txtVersion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtVersion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtVersion.Name = "txtVersion";
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Name = "lblVersion";
            // 
            // txtGroup
            // 
            resources.ApplyResources(this.txtGroup, "txtGroup");
            this.txtGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtGroup.Name = "txtGroup";
            // 
            // lblGroup
            // 
            resources.ApplyResources(this.lblGroup, "lblGroup");
            this.lblGroup.Name = "lblGroup";
            // 
            // txtDescription
            // 
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtDescription.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtDescription.Name = "txtDescription";
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // btnPath
            // 
            resources.ApplyResources(this.btnPath, "btnPath");
            this.btnPath.Name = "btnPath";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // frmEditFile
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtGroup);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "frmEditFile";
            this.Load += new System.EventHandler(this.frmEditFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnPath;
    }
}