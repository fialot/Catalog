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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditBooks));
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblTranslator = new System.Windows.Forms.Label();
            this.lblIllustrator = new System.Windows.Forms.Label();
            this.gbOriginal = new System.Windows.Forms.GroupBox();
            this.lblOrigName = new System.Windows.Forms.Label();
            this.txtOrigName = new System.Windows.Forms.TextBox();
            this.txtOrigLang = new System.Windows.Forms.TextBox();
            this.lblOrigLang = new System.Windows.Forms.Label();
            this.lblOrigYear = new System.Windows.Forms.Label();
            this.txtOrigYear = new System.Windows.Forms.TextBox();
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
            this.txtLanguage = new System.Windows.Forms.TextBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.txtPublisher = new System.Windows.Forms.TextBox();
            this.lblPublusher = new System.Windows.Forms.Label();
            this.lblSubGenre = new System.Windows.Forms.Label();
            this.txtSubGenre = new System.Windows.Forms.TextBox();
            this.lblCopy = new System.Windows.Forms.Label();
            this.gbRating = new System.Windows.Forms.GroupBox();
            this.chbReaded = new System.Windows.Forms.CheckBox();
            this.lblMyRating = new System.Windows.Forms.Label();
            this.txtMyRating = new System.Windows.Forms.TextBox();
            this.lblRating = new System.Windows.Forms.Label();
            this.txtRating = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtPages = new System.Windows.Forms.TextBox();
            this.lblPages = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.gbCopies = new System.Windows.Forms.GroupBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.txtCondition = new System.Windows.Forms.TextBox();
            this.chbExcluded = new System.Windows.Forms.CheckBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnLocation = new System.Windows.Forms.Button();
            this.btnDelCopy = new System.Windows.Forms.Button();
            this.btnAddCopy = new System.Windows.Forms.Button();
            this.cbCopy = new System.Windows.Forms.ComboBox();
            this.gbInclusion = new System.Windows.Forms.GroupBox();
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.lblKeywords = new System.Windows.Forms.Label();
            this.lblBookbinding = new System.Windows.Forms.Label();
            this.cbBookbinding = new System.Windows.Forms.ComboBox();
            this.lblAuthorSurname = new System.Windows.Forms.Label();
            this.txtAuthorSurname = new System.Windows.Forms.TextBox();
            this.brnGetDataISBN = new System.Windows.Forms.Button();
            this.btnSaveNew = new System.Windows.Forms.Button();
            this.lblUpdated = new System.Windows.Forms.Label();
            this.TimeOut = new System.Windows.Forms.Timer(this.components);
            this.gbOriginal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCover)).BeginInit();
            this.gbRating.SuspendLayout();
            this.gbCopies.SuspendLayout();
            this.gbInclusion.SuspendLayout();
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
            resources.ApplyResources(this.gbOriginal, "gbOriginal");
            this.gbOriginal.Controls.Add(this.lblOrigName);
            this.gbOriginal.Controls.Add(this.txtOrigName);
            this.gbOriginal.Controls.Add(this.txtOrigLang);
            this.gbOriginal.Controls.Add(this.lblOrigLang);
            this.gbOriginal.Controls.Add(this.lblOrigYear);
            this.gbOriginal.Controls.Add(this.txtOrigYear);
            this.gbOriginal.Name = "gbOriginal";
            this.gbOriginal.TabStop = false;
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
            // lblOrigYear
            // 
            resources.ApplyResources(this.lblOrigYear, "lblOrigYear");
            this.lblOrigYear.Name = "lblOrigYear";
            // 
            // txtOrigYear
            // 
            resources.ApplyResources(this.txtOrigYear, "txtOrigYear");
            this.txtOrigYear.Name = "txtOrigYear";
            // 
            // lblType
            // 
            resources.ApplyResources(this.lblType, "lblType");
            this.lblType.Name = "lblType";
            // 
            // cbType
            // 
            resources.ApplyResources(this.cbType, "cbType");
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Name = "cbType";
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
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
            // imgCover
            // 
            resources.ApplyResources(this.imgCover, "imgCover");
            this.imgCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgCover.Name = "imgCover";
            this.imgCover.TabStop = false;
            this.imgCover.Click += new System.EventHandler(this.imgCover_Click);
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
            this.txtISBN.TextChanged += new System.EventHandler(this.txtISBN_TextChanged);
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
            // lblCopy
            // 
            resources.ApplyResources(this.lblCopy, "lblCopy");
            this.lblCopy.Name = "lblCopy";
            // 
            // gbRating
            // 
            resources.ApplyResources(this.gbRating, "gbRating");
            this.gbRating.Controls.Add(this.chbReaded);
            this.gbRating.Controls.Add(this.lblMyRating);
            this.gbRating.Controls.Add(this.txtMyRating);
            this.gbRating.Controls.Add(this.lblRating);
            this.gbRating.Controls.Add(this.txtRating);
            this.gbRating.Name = "gbRating";
            this.gbRating.TabStop = false;
            // 
            // chbReaded
            // 
            resources.ApplyResources(this.chbReaded, "chbReaded");
            this.chbReaded.Name = "chbReaded";
            this.chbReaded.UseVisualStyleBackColor = true;
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
            // lblLocation
            // 
            resources.ApplyResources(this.lblLocation, "lblLocation");
            this.lblLocation.Name = "lblLocation";
            // 
            // txtLocation
            // 
            resources.ApplyResources(this.txtLocation, "txtLocation");
            this.txtLocation.Name = "txtLocation";
            // 
            // gbCopies
            // 
            resources.ApplyResources(this.gbCopies, "gbCopies");
            this.gbCopies.Controls.Add(this.lblCondition);
            this.gbCopies.Controls.Add(this.txtCondition);
            this.gbCopies.Controls.Add(this.chbExcluded);
            this.gbCopies.Controls.Add(this.lblCount);
            this.gbCopies.Controls.Add(this.btnLocation);
            this.gbCopies.Controls.Add(this.btnDelCopy);
            this.gbCopies.Controls.Add(this.btnAddCopy);
            this.gbCopies.Controls.Add(this.cbCopy);
            this.gbCopies.Controls.Add(this.lblCopy);
            this.gbCopies.Controls.Add(this.lblPrice);
            this.gbCopies.Controls.Add(this.lblLocation);
            this.gbCopies.Controls.Add(this.txtPrice);
            this.gbCopies.Controls.Add(this.txtInvNum);
            this.gbCopies.Controls.Add(this.txtLocation);
            this.gbCopies.Controls.Add(this.lblInvNum);
            this.gbCopies.Controls.Add(this.dtAcqDate);
            this.gbCopies.Controls.Add(this.lblAcqDate);
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
            resources.ApplyResources(this.txtCondition, "txtCondition");
            this.txtCondition.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCondition.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCondition.Name = "txtCondition";
            // 
            // chbExcluded
            // 
            resources.ApplyResources(this.chbExcluded, "chbExcluded");
            this.chbExcluded.Name = "chbExcluded";
            this.chbExcluded.UseVisualStyleBackColor = true;
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
            resources.ApplyResources(this.cbCopy, "cbCopy");
            this.cbCopy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCopy.FormattingEnabled = true;
            this.cbCopy.Name = "cbCopy";
            this.cbCopy.SelectedIndexChanged += new System.EventHandler(this.cbCopy_SelectedIndexChanged);
            // 
            // gbInclusion
            // 
            resources.ApplyResources(this.gbInclusion, "gbInclusion");
            this.gbInclusion.Controls.Add(this.txtKeywords);
            this.gbInclusion.Controls.Add(this.lblKeywords);
            this.gbInclusion.Controls.Add(this.lblGenre);
            this.gbInclusion.Controls.Add(this.txtSeries);
            this.gbInclusion.Controls.Add(this.lblSeries);
            this.gbInclusion.Controls.Add(this.txtGenre);
            this.gbInclusion.Controls.Add(this.lblSNumber);
            this.gbInclusion.Controls.Add(this.txtSNumber);
            this.gbInclusion.Controls.Add(this.txtSubGenre);
            this.gbInclusion.Controls.Add(this.lblSubGenre);
            this.gbInclusion.Name = "gbInclusion";
            this.gbInclusion.TabStop = false;
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
            // lblBookbinding
            // 
            resources.ApplyResources(this.lblBookbinding, "lblBookbinding");
            this.lblBookbinding.Name = "lblBookbinding";
            // 
            // cbBookbinding
            // 
            resources.ApplyResources(this.cbBookbinding, "cbBookbinding");
            this.cbBookbinding.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbBookbinding.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbBookbinding.FormattingEnabled = true;
            this.cbBookbinding.Name = "cbBookbinding";
            // 
            // lblAuthorSurname
            // 
            resources.ApplyResources(this.lblAuthorSurname, "lblAuthorSurname");
            this.lblAuthorSurname.Name = "lblAuthorSurname";
            // 
            // txtAuthorSurname
            // 
            resources.ApplyResources(this.txtAuthorSurname, "txtAuthorSurname");
            this.txtAuthorSurname.Name = "txtAuthorSurname";
            // 
            // brnGetDataISBN
            // 
            resources.ApplyResources(this.brnGetDataISBN, "brnGetDataISBN");
            this.brnGetDataISBN.Name = "brnGetDataISBN";
            this.brnGetDataISBN.UseVisualStyleBackColor = true;
            this.brnGetDataISBN.Click += new System.EventHandler(this.brnGetDataISBN_Click);
            // 
            // btnSaveNew
            // 
            resources.ApplyResources(this.btnSaveNew, "btnSaveNew");
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.UseVisualStyleBackColor = true;
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click);
            // 
            // lblUpdated
            // 
            resources.ApplyResources(this.lblUpdated, "lblUpdated");
            this.lblUpdated.Name = "lblUpdated";
            // 
            // TimeOut
            // 
            this.TimeOut.Interval = 200;
            this.TimeOut.Tick += new System.EventHandler(this.TimeOut_Tick);
            // 
            // frmEditBooks
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblUpdated);
            this.Controls.Add(this.btnSaveNew);
            this.Controls.Add(this.brnGetDataISBN);
            this.Controls.Add(this.lblAuthorSurname);
            this.Controls.Add(this.txtAuthorSurname);
            this.Controls.Add(this.cbBookbinding);
            this.Controls.Add(this.lblBookbinding);
            this.Controls.Add(this.gbInclusion);
            this.Controls.Add(this.gbCopies);
            this.Controls.Add(this.txtPages);
            this.Controls.Add(this.lblPages);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.gbRating);
            this.Controls.Add(this.txtPublisher);
            this.Controls.Add(this.lblPublusher);
            this.Controls.Add(this.txtLanguage);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.txtEdition);
            this.Controls.Add(this.lblEdition);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.txtIllustrator);
            this.Controls.Add(this.txtTranslator);
            this.Controls.Add(this.txtISBN);
            this.Controls.Add(this.lblISBN);
            this.Controls.Add(this.lblHero);
            this.Controls.Add(this.txtHero);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnTag6);
            this.Controls.Add(this.btnTag5);
            this.Controls.Add(this.btnTag4);
            this.Controls.Add(this.btnTag3);
            this.Controls.Add(this.btnTag2);
            this.Controls.Add(this.btnTag1);
            this.Controls.Add(this.lblContent);
            this.Controls.Add(this.txtContent);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmEditBooks";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditBooks_FormClosing);
            this.Load += new System.EventHandler(this.frmEditBooks_Load);
            this.gbOriginal.ResumeLayout(false);
            this.gbOriginal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgCover)).EndInit();
            this.gbRating.ResumeLayout(false);
            this.gbRating.PerformLayout();
            this.gbCopies.ResumeLayout(false);
            this.gbCopies.PerformLayout();
            this.gbInclusion.ResumeLayout(false);
            this.gbInclusion.PerformLayout();
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
        private System.Windows.Forms.Label lblOrigName;
        private System.Windows.Forms.TextBox txtOrigName;
        private System.Windows.Forms.Label lblSubGenre;
        private System.Windows.Forms.TextBox txtSubGenre;
        private System.Windows.Forms.Label lblCopy;
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
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.GroupBox gbCopies;
        private System.Windows.Forms.ComboBox cbCopy;
        private System.Windows.Forms.GroupBox gbInclusion;
        private System.Windows.Forms.Button btnDelCopy;
        private System.Windows.Forms.Button btnAddCopy;
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.Label lblKeywords;
        private System.Windows.Forms.Label lblBookbinding;
        private System.Windows.Forms.ComboBox cbBookbinding;
        private System.Windows.Forms.CheckBox chbReaded;
        private System.Windows.Forms.Button btnLocation;
        private System.Windows.Forms.Label lblAuthorSurname;
        private System.Windows.Forms.TextBox txtAuthorSurname;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button brnGetDataISBN;
        private System.Windows.Forms.Button btnSaveNew;
        private System.Windows.Forms.Label lblUpdated;
        private System.Windows.Forms.Timer TimeOut;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.TextBox txtCondition;
        private System.Windows.Forms.CheckBox chbExcluded;
    }
}