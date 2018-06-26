namespace Katalog
{
    partial class frmEditBooks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditBooks));
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblTranslator = new System.Windows.Forms.Label();
            this.lblIllustrator = new System.Windows.Forms.Label();
            this.gbOriginal = new System.Windows.Forms.GroupBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.lblSeries = new System.Windows.Forms.Label();
            this.txtSeries = new System.Windows.Forms.TextBox();
            this.dtAcqDate = new System.Windows.Forms.DateTimePicker();
            this.lblAcqDate = new System.Windows.Forms.Label();
            this.lblContent = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.lblGenre = new System.Windows.Forms.Label();
            this.txtGenre = new System.Windows.Forms.TextBox();
            this.lblInvNum = new System.Windows.Forms.Label();
            this.txtInvNum = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnTag6 = new System.Windows.Forms.Button();
            this.btnTag5 = new System.Windows.Forms.Button();
            this.btnTag4 = new System.Windows.Forms.Button();
            this.btnTag3 = new System.Windows.Forms.Button();
            this.btnTag2 = new System.Windows.Forms.Button();
            this.btnTag1 = new System.Windows.Forms.Button();
            this.imgCover = new System.Windows.Forms.PictureBox();
            this.lblHero = new System.Windows.Forms.Label();
            this.txtHero = new System.Windows.Forms.TextBox();
            this.lblISBN = new System.Windows.Forms.Label();
            this.txtISBN = new System.Windows.Forms.TextBox();
            this.txtTranslator = new System.Windows.Forms.TextBox();
            this.txtIllustrator = new System.Windows.Forms.TextBox();
            this.txtSNumber = new System.Windows.Forms.TextBox();
            this.lblSNumber = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.txtEdition = new System.Windows.Forms.TextBox();
            this.lblEdition = new System.Windows.Forms.Label();
            this.txtOrigYear = new System.Windows.Forms.TextBox();
            this.lblOrigYear = new System.Windows.Forms.Label();
            this.txtOrigLang = new System.Windows.Forms.TextBox();
            this.lblOrigLang = new System.Windows.Forms.Label();
            this.txtLanguage = new System.Windows.Forms.TextBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.txtPublisher = new System.Windows.Forms.TextBox();
            this.lblPublusher = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.lblSubGenre = new System.Windows.Forms.Label();
            this.txtSubGenre = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblOrigName = new System.Windows.Forms.Label();
            this.txtOrigName = new System.Windows.Forms.TextBox();
            this.gbRating = new System.Windows.Forms.GroupBox();
            this.lblRating = new System.Windows.Forms.Label();
            this.txtRating = new System.Windows.Forms.TextBox();
            this.lblMyRating = new System.Windows.Forms.Label();
            this.txtMyRating = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtPages = new System.Windows.Forms.TextBox();
            this.lblPages = new System.Windows.Forms.Label();
            this.lblPlace = new System.Windows.Forms.Label();
            this.txtPlace = new System.Windows.Forms.TextBox();
            this.gbOriginal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCover)).BeginInit();
            this.gbRating.SuspendLayout();
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
            // lblAuthor
            // 
            resources.ApplyResources(this.lblAuthor, "lblAuthor");
            this.lblAuthor.Name = "lblAuthor";
            // 
            // txtAuthor
            // 
            resources.ApplyResources(this.txtAuthor, "txtAuthor");
            this.txtAuthor.Name = "txtAuthor";
            // 
            // lblTranslator
            // 
            resources.ApplyResources(this.lblTranslator, "lblTranslator");
            this.lblTranslator.Name = "lblTranslator";
            // 
            // lblIllustrator
            // 
            resources.ApplyResources(this.lblIllustrator, "lblIllustrator");
            this.lblIllustrator.Name = "lblIllustrator";
            // 
            // gbOriginal
            // 
            this.gbOriginal.Controls.Add(this.lblOrigName);
            this.gbOriginal.Controls.Add(this.txtOrigName);
            this.gbOriginal.Controls.Add(this.txtOrigLang);
            this.gbOriginal.Controls.Add(this.lblOrigLang);
            this.gbOriginal.Controls.Add(this.lblOrigYear);
            this.gbOriginal.Controls.Add(this.txtOrigYear);
            resources.ApplyResources(this.gbOriginal, "gbOriginal");
            this.gbOriginal.Name = "gbOriginal";
            this.gbOriginal.TabStop = false;
            // 
            // lblType
            // 
            resources.ApplyResources(this.lblType, "lblType");
            this.lblType.Name = "lblType";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            resources.ApplyResources(this.cbType, "cbType");
            this.cbType.Name = "cbType";
            // 
            // lblSeries
            // 
            resources.ApplyResources(this.lblSeries, "lblSeries");
            this.lblSeries.Name = "lblSeries";
            // 
            // txtSeries
            // 
            resources.ApplyResources(this.txtSeries, "txtSeries");
            this.txtSeries.Name = "txtSeries";
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
            // lblContent
            // 
            resources.ApplyResources(this.lblContent, "lblContent");
            this.lblContent.Name = "lblContent";
            // 
            // txtContent
            // 
            resources.ApplyResources(this.txtContent, "txtContent");
            this.txtContent.Name = "txtContent";
            // 
            // lblGenre
            // 
            resources.ApplyResources(this.lblGenre, "lblGenre");
            this.lblGenre.Name = "lblGenre";
            // 
            // txtGenre
            // 
            resources.ApplyResources(this.txtGenre, "txtGenre");
            this.txtGenre.Name = "txtGenre";
            // 
            // lblInvNum
            // 
            resources.ApplyResources(this.lblInvNum, "lblInvNum");
            this.lblInvNum.Name = "lblInvNum";
            // 
            // txtInvNum
            // 
            resources.ApplyResources(this.txtInvNum, "txtInvNum");
            this.txtInvNum.Name = "txtInvNum";
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
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnTag6
            // 
            this.btnTag6.Image = global::Katalog.Properties.Resources.Circle_Blue;
            resources.ApplyResources(this.btnTag6, "btnTag6");
            this.btnTag6.Name = "btnTag6";
            this.btnTag6.UseVisualStyleBackColor = true;
            // 
            // btnTag5
            // 
            this.btnTag5.Image = global::Katalog.Properties.Resources.circ_grey;
            resources.ApplyResources(this.btnTag5, "btnTag5");
            this.btnTag5.Name = "btnTag5";
            this.btnTag5.UseVisualStyleBackColor = true;
            // 
            // btnTag4
            // 
            this.btnTag4.Image = global::Katalog.Properties.Resources.circ_yellow;
            resources.ApplyResources(this.btnTag4, "btnTag4");
            this.btnTag4.Name = "btnTag4";
            this.btnTag4.UseVisualStyleBackColor = true;
            // 
            // btnTag3
            // 
            this.btnTag3.Image = global::Katalog.Properties.Resources.circ_orange;
            resources.ApplyResources(this.btnTag3, "btnTag3");
            this.btnTag3.Name = "btnTag3";
            this.btnTag3.UseVisualStyleBackColor = true;
            // 
            // btnTag2
            // 
            resources.ApplyResources(this.btnTag2, "btnTag2");
            this.btnTag2.Image = global::Katalog.Properties.Resources.circ_red;
            this.btnTag2.Name = "btnTag2";
            this.btnTag2.UseVisualStyleBackColor = true;
            // 
            // btnTag1
            // 
            resources.ApplyResources(this.btnTag1, "btnTag1");
            this.btnTag1.Image = global::Katalog.Properties.Resources.circ_green;
            this.btnTag1.Name = "btnTag1";
            this.btnTag1.UseVisualStyleBackColor = true;
            // 
            // imgCover
            // 
            this.imgCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.imgCover, "imgCover");
            this.imgCover.Name = "imgCover";
            this.imgCover.TabStop = false;
            // 
            // lblHero
            // 
            resources.ApplyResources(this.lblHero, "lblHero");
            this.lblHero.Name = "lblHero";
            // 
            // txtHero
            // 
            resources.ApplyResources(this.txtHero, "txtHero");
            this.txtHero.Name = "txtHero";
            // 
            // lblISBN
            // 
            resources.ApplyResources(this.lblISBN, "lblISBN");
            this.lblISBN.Name = "lblISBN";
            // 
            // txtISBN
            // 
            resources.ApplyResources(this.txtISBN, "txtISBN");
            this.txtISBN.Name = "txtISBN";
            // 
            // txtTranslator
            // 
            resources.ApplyResources(this.txtTranslator, "txtTranslator");
            this.txtTranslator.Name = "txtTranslator";
            // 
            // txtIllustrator
            // 
            resources.ApplyResources(this.txtIllustrator, "txtIllustrator");
            this.txtIllustrator.Name = "txtIllustrator";
            // 
            // txtSNumber
            // 
            resources.ApplyResources(this.txtSNumber, "txtSNumber");
            this.txtSNumber.Name = "txtSNumber";
            // 
            // lblSNumber
            // 
            resources.ApplyResources(this.lblSNumber, "lblSNumber");
            this.lblSNumber.Name = "lblSNumber";
            // 
            // txtYear
            // 
            resources.ApplyResources(this.txtYear, "txtYear");
            this.txtYear.Name = "txtYear";
            // 
            // lblYear
            // 
            resources.ApplyResources(this.lblYear, "lblYear");
            this.lblYear.Name = "lblYear";
            // 
            // txtEdition
            // 
            resources.ApplyResources(this.txtEdition, "txtEdition");
            this.txtEdition.Name = "txtEdition";
            // 
            // lblEdition
            // 
            resources.ApplyResources(this.lblEdition, "lblEdition");
            this.lblEdition.Name = "lblEdition";
            // 
            // txtOrigYear
            // 
            resources.ApplyResources(this.txtOrigYear, "txtOrigYear");
            this.txtOrigYear.Name = "txtOrigYear";
            // 
            // lblOrigYear
            // 
            resources.ApplyResources(this.lblOrigYear, "lblOrigYear");
            this.lblOrigYear.Name = "lblOrigYear";
            // 
            // txtOrigLang
            // 
            resources.ApplyResources(this.txtOrigLang, "txtOrigLang");
            this.txtOrigLang.Name = "txtOrigLang";
            // 
            // lblOrigLang
            // 
            resources.ApplyResources(this.lblOrigLang, "lblOrigLang");
            this.lblOrigLang.Name = "lblOrigLang";
            // 
            // txtLanguage
            // 
            resources.ApplyResources(this.txtLanguage, "txtLanguage");
            this.txtLanguage.Name = "txtLanguage";
            // 
            // lblLanguage
            // 
            resources.ApplyResources(this.lblLanguage, "lblLanguage");
            this.lblLanguage.Name = "lblLanguage";
            // 
            // txtPublisher
            // 
            resources.ApplyResources(this.txtPublisher, "txtPublisher");
            this.txtPublisher.Name = "txtPublisher";
            // 
            // lblPublusher
            // 
            resources.ApplyResources(this.lblPublusher, "lblPublusher");
            this.lblPublusher.Name = "lblPublusher";
            // 
            // txtCount
            // 
            resources.ApplyResources(this.txtCount, "txtCount");
            this.txtCount.Name = "txtCount";
            // 
            // lblSubGenre
            // 
            resources.ApplyResources(this.lblSubGenre, "lblSubGenre");
            this.lblSubGenre.Name = "lblSubGenre";
            // 
            // txtSubGenre
            // 
            resources.ApplyResources(this.txtSubGenre, "txtSubGenre");
            this.txtSubGenre.Name = "txtSubGenre";
            // 
            // lblCount
            // 
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            // 
            // lblOrigName
            // 
            resources.ApplyResources(this.lblOrigName, "lblOrigName");
            this.lblOrigName.Name = "lblOrigName";
            // 
            // txtOrigName
            // 
            resources.ApplyResources(this.txtOrigName, "txtOrigName");
            this.txtOrigName.Name = "txtOrigName";
            // 
            // gbRating
            // 
            this.gbRating.Controls.Add(this.lblMyRating);
            this.gbRating.Controls.Add(this.txtMyRating);
            this.gbRating.Controls.Add(this.lblRating);
            this.gbRating.Controls.Add(this.txtRating);
            resources.ApplyResources(this.gbRating, "gbRating");
            this.gbRating.Name = "gbRating";
            this.gbRating.TabStop = false;
            // 
            // lblRating
            // 
            resources.ApplyResources(this.lblRating, "lblRating");
            this.lblRating.Name = "lblRating";
            // 
            // txtRating
            // 
            resources.ApplyResources(this.txtRating, "txtRating");
            this.txtRating.Name = "txtRating";
            // 
            // lblMyRating
            // 
            resources.ApplyResources(this.lblMyRating, "lblMyRating");
            this.lblMyRating.Name = "lblMyRating";
            // 
            // txtMyRating
            // 
            resources.ApplyResources(this.txtMyRating, "txtMyRating");
            this.txtMyRating.Name = "txtMyRating";
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
            // lblURL
            // 
            resources.ApplyResources(this.lblURL, "lblURL");
            this.lblURL.Name = "lblURL";
            // 
            // txtURL
            // 
            resources.ApplyResources(this.txtURL, "txtURL");
            this.txtURL.Name = "txtURL";
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
            // txtPages
            // 
            resources.ApplyResources(this.txtPages, "txtPages");
            this.txtPages.Name = "txtPages";
            // 
            // lblPages
            // 
            resources.ApplyResources(this.lblPages, "lblPages");
            this.lblPages.Name = "lblPages";
            // 
            // lblPlace
            // 
            resources.ApplyResources(this.lblPlace, "lblPlace");
            this.lblPlace.Name = "lblPlace";
            // 
            // txtPlace
            // 
            resources.ApplyResources(this.txtPlace, "txtPlace");
            this.txtPlace.Name = "txtPlace";
            // 
            // frmEditBooks
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPlace);
            this.Controls.Add(this.txtPlace);
            this.Controls.Add(this.txtPages);
            this.Controls.Add(this.lblPages);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.gbRating);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblSubGenre);
            this.Controls.Add(this.txtSubGenre);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.txtPublisher);
            this.Controls.Add(this.lblPublusher);
            this.Controls.Add(this.txtLanguage);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.txtEdition);
            this.Controls.Add(this.lblEdition);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.txtSNumber);
            this.Controls.Add(this.lblSNumber);
            this.Controls.Add(this.txtIllustrator);
            this.Controls.Add(this.txtTranslator);
            this.Controls.Add(this.txtISBN);
            this.Controls.Add(this.lblISBN);
            this.Controls.Add(this.lblHero);
            this.Controls.Add(this.txtHero);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblInvNum);
            this.Controls.Add(this.txtInvNum);
            this.Controls.Add(this.lblGenre);
            this.Controls.Add(this.txtGenre);
            this.Controls.Add(this.btnTag6);
            this.Controls.Add(this.btnTag5);
            this.Controls.Add(this.btnTag4);
            this.Controls.Add(this.btnTag3);
            this.Controls.Add(this.btnTag2);
            this.Controls.Add(this.btnTag1);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.lblAcqDate);
            this.Controls.Add(this.dtAcqDate);
            this.Controls.Add(this.lblSeries);
            this.Controls.Add(this.txtSeries);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.gbOriginal);
            this.Controls.Add(this.lblIllustrator);
            this.Controls.Add(this.lblTranslator);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.imgCover);
            this.Controls.Add(this.txtName);
            this.Name = "frmEditBooks";
            this.Load += new System.EventHandler(this.frmEditContacts_Load);
            this.gbOriginal.ResumeLayout(false);
            this.gbOriginal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCover)).EndInit();
            this.gbRating.ResumeLayout(false);
            this.gbRating.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.PictureBox imgCover;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label lblTranslator;
        private System.Windows.Forms.Label lblIllustrator;
        private System.Windows.Forms.GroupBox gbOriginal;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label lblSeries;
        private System.Windows.Forms.TextBox txtSeries;
        private System.Windows.Forms.DateTimePicker dtAcqDate;
        private System.Windows.Forms.Label lblAcqDate;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Button btnTag1;
        private System.Windows.Forms.Button btnTag2;
        private System.Windows.Forms.Button btnTag3;
        private System.Windows.Forms.Button btnTag4;
        private System.Windows.Forms.Button btnTag5;
        private System.Windows.Forms.Button btnTag6;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.TextBox txtGenre;
        private System.Windows.Forms.Label lblInvNum;
        private System.Windows.Forms.TextBox txtInvNum;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblHero;
        private System.Windows.Forms.TextBox txtHero;
        private System.Windows.Forms.Label lblISBN;
        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.TextBox txtTranslator;
        private System.Windows.Forms.TextBox txtIllustrator;
        private System.Windows.Forms.TextBox txtSNumber;
        private System.Windows.Forms.Label lblSNumber;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.TextBox txtEdition;
        private System.Windows.Forms.Label lblEdition;
        private System.Windows.Forms.TextBox txtOrigYear;
        private System.Windows.Forms.Label lblOrigYear;
        private System.Windows.Forms.TextBox txtOrigLang;
        private System.Windows.Forms.Label lblOrigLang;
        private System.Windows.Forms.TextBox txtLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.TextBox txtPublisher;
        private System.Windows.Forms.Label lblPublusher;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label lblOrigName;
        private System.Windows.Forms.TextBox txtOrigName;
        private System.Windows.Forms.Label lblSubGenre;
        private System.Windows.Forms.TextBox txtSubGenre;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.GroupBox gbRating;
        private System.Windows.Forms.Label lblMyRating;
        private System.Windows.Forms.TextBox txtMyRating;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.TextBox txtRating;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtPages;
        private System.Windows.Forms.Label lblPages;
        private System.Windows.Forms.Label lblPlace;
        private System.Windows.Forms.TextBox txtPlace;
    }
}