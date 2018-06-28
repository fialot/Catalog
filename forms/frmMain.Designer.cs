namespace Katalog
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.tabCatalog = new System.Windows.Forms.TabControl();
            this.tabContacts = new System.Windows.Forms.TabPage();
            this.btnTest = new System.Windows.Forms.Button();
            this.olvContacts = new BrightIdeasSoftware.FastObjectListView();
            this.conFastTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conSurname = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conPhone = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conEmail = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conAddress = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imgOLV = new System.Windows.Forms.ImageList(this.components);
            this.tabBorrowing = new System.Windows.Forms.TabPage();
            this.chbShowReturned = new System.Windows.Forms.CheckBox();
            this.olvBorrowing = new BrightIdeasSoftware.FastObjectListView();
            this.brType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brPerson = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brFrom = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brTo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brReturned = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabRezervation = new System.Windows.Forms.TabPage();
            this.tabItems = new System.Windows.Forms.TabPage();
            this.chbShowExcluded = new System.Windows.Forms.CheckBox();
            this.olvItem = new BrightIdeasSoftware.FastObjectListView();
            this.itFastTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itCategory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itSubcategory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itInvNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itLocation = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itKeywords = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itCounts = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itBorrowed = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itExcluded = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabBooks = new System.Windows.Forms.TabPage();
            this.olvBooks = new BrightIdeasSoftware.FastObjectListView();
            this.bkFastTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkAuthor = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkYear = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkGenre = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkSubgenre = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkInvNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkLocation = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkKeywords = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkSeries = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabGames = new System.Windows.Forms.TabPage();
            this.tabAudio = new System.Windows.Forms.TabPage();
            this.tabVideo = new System.Windows.Forms.TabPage();
            this.tabFoto = new System.Windows.Forms.TabPage();
            this.imgBarList = new System.Windows.Forms.ImageList(this.components);
            this.toolStripEditItem = new System.Windows.Forms.ToolStrip();
            this.btnNewItem = new System.Windows.Forms.ToolStripButton();
            this.btnEditItem = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripFilter = new System.Windows.Forms.ToolStrip();
            this.lblFilter = new System.Windows.Forms.ToolStripLabel();
            this.txtFilter = new System.Windows.Forms.ToolStripTextBox();
            this.cbFilterCol = new System.Windows.Forms.ToolStripComboBox();
            this.btnFilterPin1 = new System.Windows.Forms.ToolStripButton();
            this.btnFilterPin2 = new System.Windows.Forms.ToolStripButton();
            this.btnFilterPin3 = new System.Windows.Forms.ToolStripButton();
            this.btnFilterPin4 = new System.Windows.Forms.ToolStripButton();
            this.btnFilterPin5 = new System.Windows.Forms.ToolStripButton();
            this.btnFilterPin6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripFastFilter = new System.Windows.Forms.ToolStrip();
            this.btnFilterA = new System.Windows.Forms.ToolStripButton();
            this.btnFilterB = new System.Windows.Forms.ToolStripButton();
            this.btnFilterC = new System.Windows.Forms.ToolStripButton();
            this.btnFilterCC = new System.Windows.Forms.ToolStripButton();
            this.btnFilterD = new System.Windows.Forms.ToolStripButton();
            this.btnFilterDD = new System.Windows.Forms.ToolStripButton();
            this.btnFilterE = new System.Windows.Forms.ToolStripButton();
            this.btnFilterF = new System.Windows.Forms.ToolStripButton();
            this.btnFilterG = new System.Windows.Forms.ToolStripButton();
            this.btnFilterH = new System.Windows.Forms.ToolStripButton();
            this.btnFilterCH = new System.Windows.Forms.ToolStripButton();
            this.btnFilterI = new System.Windows.Forms.ToolStripButton();
            this.btnFilterJ = new System.Windows.Forms.ToolStripButton();
            this.btnFilterK = new System.Windows.Forms.ToolStripButton();
            this.btnFilterL = new System.Windows.Forms.ToolStripButton();
            this.btnFilterM = new System.Windows.Forms.ToolStripButton();
            this.btnFilterN = new System.Windows.Forms.ToolStripButton();
            this.btnFilterNN = new System.Windows.Forms.ToolStripButton();
            this.btnFilterO = new System.Windows.Forms.ToolStripButton();
            this.btnFilterP = new System.Windows.Forms.ToolStripButton();
            this.btnFilterQ = new System.Windows.Forms.ToolStripButton();
            this.btnFilterR = new System.Windows.Forms.ToolStripButton();
            this.btnFilterRR = new System.Windows.Forms.ToolStripButton();
            this.btnFilterS = new System.Windows.Forms.ToolStripButton();
            this.btnFilterSS = new System.Windows.Forms.ToolStripButton();
            this.btnFilterT = new System.Windows.Forms.ToolStripButton();
            this.btnFilterTT = new System.Windows.Forms.ToolStripButton();
            this.btnFilterU = new System.Windows.Forms.ToolStripButton();
            this.btnFilterV = new System.Windows.Forms.ToolStripButton();
            this.btnFilterW = new System.Windows.Forms.ToolStripButton();
            this.btnFilterX = new System.Windows.Forms.ToolStripButton();
            this.btnFilterY = new System.Windows.Forms.ToolStripButton();
            this.btnFilterZ = new System.Windows.Forms.ToolStripButton();
            this.btnFilterZZ = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFilter09 = new System.Windows.Forms.ToolStripButton();
            this.cbFastFilterCol = new System.Windows.Forms.ToolStripComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewDB = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenDB = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSavaAsDB = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolBars = new System.Windows.Forms.ToolStripMenuItem();
            this.filtryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowContacts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowBorrowing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowReservations = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowBoardGames = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowAudio = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowVideo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowPhoto = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLists = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBookLists = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAuthorList = new System.Windows.Forms.ToolStripMenuItem();
            this.překladatelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIllustratorList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenre = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSeries = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLanguageLists = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPublishing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLangEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.mnuShowItems = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.tabCatalog.SuspendLayout();
            this.tabContacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvContacts)).BeginInit();
            this.tabBorrowing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBorrowing)).BeginInit();
            this.tabItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvItem)).BeginInit();
            this.tabBooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBooks)).BeginInit();
            this.toolStripEditItem.SuspendLayout();
            this.toolStripFilter.SuspendLayout();
            this.toolStripFastFilter.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tabCatalog);
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripEditItem);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripFilter);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripFastFilter);
            // 
            // tabCatalog
            // 
            this.tabCatalog.Controls.Add(this.tabContacts);
            this.tabCatalog.Controls.Add(this.tabBorrowing);
            this.tabCatalog.Controls.Add(this.tabRezervation);
            this.tabCatalog.Controls.Add(this.tabItems);
            this.tabCatalog.Controls.Add(this.tabBooks);
            this.tabCatalog.Controls.Add(this.tabGames);
            this.tabCatalog.Controls.Add(this.tabAudio);
            this.tabCatalog.Controls.Add(this.tabVideo);
            this.tabCatalog.Controls.Add(this.tabFoto);
            resources.ApplyResources(this.tabCatalog, "tabCatalog");
            this.tabCatalog.ImageList = this.imgBarList;
            this.tabCatalog.Name = "tabCatalog";
            this.tabCatalog.SelectedIndex = 0;
            this.tabCatalog.SelectedIndexChanged += new System.EventHandler(this.tabCatalog_SelectedIndexChanged);
            // 
            // tabContacts
            // 
            this.tabContacts.Controls.Add(this.btnTest);
            this.tabContacts.Controls.Add(this.olvContacts);
            resources.ApplyResources(this.tabContacts, "tabContacts");
            this.tabContacts.Name = "tabContacts";
            this.tabContacts.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            resources.ApplyResources(this.btnTest, "btnTest");
            this.btnTest.Name = "btnTest";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // olvContacts
            // 
            this.olvContacts.AllColumns.Add(this.conFastTags);
            this.olvContacts.AllColumns.Add(this.conName);
            this.olvContacts.AllColumns.Add(this.conSurname);
            this.olvContacts.AllColumns.Add(this.conPhone);
            this.olvContacts.AllColumns.Add(this.conEmail);
            this.olvContacts.AllColumns.Add(this.conAddress);
            resources.ApplyResources(this.olvContacts, "olvContacts");
            this.olvContacts.CellEditUseWholeCell = false;
            this.olvContacts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.conFastTags,
            this.conName,
            this.conSurname,
            this.conPhone,
            this.conEmail,
            this.conAddress});
            this.olvContacts.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvContacts.FullRowSelect = true;
            this.olvContacts.GridLines = true;
            this.olvContacts.Name = "olvContacts";
            this.olvContacts.ShowGroups = false;
            this.olvContacts.SmallImageList = this.imgOLV;
            this.olvContacts.UseCompatibleStateImageBehavior = false;
            this.olvContacts.View = System.Windows.Forms.View.Details;
            this.olvContacts.VirtualMode = true;
            this.olvContacts.SelectedIndexChanged += new System.EventHandler(this.olvContacts_SelectedIndexChanged);
            this.olvContacts.DoubleClick += new System.EventHandler(this.olvContacts_DoubleClick);
            // 
            // conFastTags
            // 
            resources.ApplyResources(this.conFastTags, "conFastTags");
            // 
            // conName
            // 
            this.conName.AspectName = "";
            resources.ApplyResources(this.conName, "conName");
            // 
            // conSurname
            // 
            this.conSurname.AspectName = "";
            resources.ApplyResources(this.conSurname, "conSurname");
            // 
            // conPhone
            // 
            this.conPhone.AspectName = "";
            resources.ApplyResources(this.conPhone, "conPhone");
            // 
            // conEmail
            // 
            this.conEmail.AspectName = "";
            resources.ApplyResources(this.conEmail, "conEmail");
            // 
            // conAddress
            // 
            resources.ApplyResources(this.conAddress, "conAddress");
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
            // 
            // tabBorrowing
            // 
            this.tabBorrowing.Controls.Add(this.chbShowReturned);
            this.tabBorrowing.Controls.Add(this.olvBorrowing);
            resources.ApplyResources(this.tabBorrowing, "tabBorrowing");
            this.tabBorrowing.Name = "tabBorrowing";
            this.tabBorrowing.UseVisualStyleBackColor = true;
            // 
            // chbShowReturned
            // 
            resources.ApplyResources(this.chbShowReturned, "chbShowReturned");
            this.chbShowReturned.Name = "chbShowReturned";
            this.chbShowReturned.UseVisualStyleBackColor = true;
            this.chbShowReturned.CheckedChanged += new System.EventHandler(this.chbShowReturned_CheckedChanged);
            // 
            // olvBorrowing
            // 
            this.olvBorrowing.AllColumns.Add(this.brType);
            this.olvBorrowing.AllColumns.Add(this.brName);
            this.olvBorrowing.AllColumns.Add(this.brPerson);
            this.olvBorrowing.AllColumns.Add(this.brFrom);
            this.olvBorrowing.AllColumns.Add(this.brTo);
            this.olvBorrowing.AllColumns.Add(this.brReturned);
            resources.ApplyResources(this.olvBorrowing, "olvBorrowing");
            this.olvBorrowing.CellEditUseWholeCell = false;
            this.olvBorrowing.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.brType,
            this.brName,
            this.brPerson,
            this.brFrom,
            this.brTo,
            this.brReturned});
            this.olvBorrowing.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvBorrowing.FullRowSelect = true;
            this.olvBorrowing.GridLines = true;
            this.olvBorrowing.Name = "olvBorrowing";
            this.olvBorrowing.ShowGroups = false;
            this.olvBorrowing.SmallImageList = this.imgOLV;
            this.olvBorrowing.UseCompatibleStateImageBehavior = false;
            this.olvBorrowing.View = System.Windows.Forms.View.Details;
            this.olvBorrowing.VirtualMode = true;
            this.olvBorrowing.SelectedIndexChanged += new System.EventHandler(this.olvBorrowing_SelectedIndexChanged);
            this.olvBorrowing.DoubleClick += new System.EventHandler(this.olvBorrowing_DoubleClick);
            // 
            // brType
            // 
            this.brType.AspectName = "";
            resources.ApplyResources(this.brType, "brType");
            // 
            // brName
            // 
            this.brName.AspectName = "";
            resources.ApplyResources(this.brName, "brName");
            // 
            // brPerson
            // 
            resources.ApplyResources(this.brPerson, "brPerson");
            // 
            // brFrom
            // 
            this.brFrom.AspectName = "";
            resources.ApplyResources(this.brFrom, "brFrom");
            // 
            // brTo
            // 
            this.brTo.AspectName = "";
            resources.ApplyResources(this.brTo, "brTo");
            // 
            // brReturned
            // 
            resources.ApplyResources(this.brReturned, "brReturned");
            // 
            // tabRezervation
            // 
            resources.ApplyResources(this.tabRezervation, "tabRezervation");
            this.tabRezervation.Name = "tabRezervation";
            this.tabRezervation.UseVisualStyleBackColor = true;
            // 
            // tabItems
            // 
            this.tabItems.Controls.Add(this.chbShowExcluded);
            this.tabItems.Controls.Add(this.olvItem);
            resources.ApplyResources(this.tabItems, "tabItems");
            this.tabItems.Name = "tabItems";
            this.tabItems.UseVisualStyleBackColor = true;
            // 
            // chbShowExcluded
            // 
            resources.ApplyResources(this.chbShowExcluded, "chbShowExcluded");
            this.chbShowExcluded.Name = "chbShowExcluded";
            this.chbShowExcluded.UseVisualStyleBackColor = true;
            this.chbShowExcluded.CheckedChanged += new System.EventHandler(this.chbShowExcluded_CheckedChanged);
            // 
            // olvItem
            // 
            this.olvItem.AllColumns.Add(this.itFastTags);
            this.olvItem.AllColumns.Add(this.itName);
            this.olvItem.AllColumns.Add(this.itCategory);
            this.olvItem.AllColumns.Add(this.itSubcategory);
            this.olvItem.AllColumns.Add(this.itInvNum);
            this.olvItem.AllColumns.Add(this.itLocation);
            this.olvItem.AllColumns.Add(this.itKeywords);
            this.olvItem.AllColumns.Add(this.itCounts);
            this.olvItem.AllColumns.Add(this.itBorrowed);
            this.olvItem.AllColumns.Add(this.itExcluded);
            resources.ApplyResources(this.olvItem, "olvItem");
            this.olvItem.CellEditUseWholeCell = false;
            this.olvItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itFastTags,
            this.itName,
            this.itCategory,
            this.itSubcategory,
            this.itInvNum,
            this.itLocation,
            this.itKeywords,
            this.itCounts,
            this.itBorrowed,
            this.itExcluded});
            this.olvItem.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvItem.FullRowSelect = true;
            this.olvItem.GridLines = true;
            this.olvItem.Name = "olvItem";
            this.olvItem.ShowGroups = false;
            this.olvItem.SmallImageList = this.imgOLV;
            this.olvItem.UseCompatibleStateImageBehavior = false;
            this.olvItem.View = System.Windows.Forms.View.Details;
            this.olvItem.VirtualMode = true;
            this.olvItem.SelectedIndexChanged += new System.EventHandler(this.olvItem_SelectedIndexChanged);
            this.olvItem.DoubleClick += new System.EventHandler(this.olvItem_DoubleClick);
            // 
            // itFastTags
            // 
            resources.ApplyResources(this.itFastTags, "itFastTags");
            // 
            // itName
            // 
            this.itName.AspectName = "";
            resources.ApplyResources(this.itName, "itName");
            // 
            // itCategory
            // 
            this.itCategory.AspectName = "";
            resources.ApplyResources(this.itCategory, "itCategory");
            // 
            // itSubcategory
            // 
            resources.ApplyResources(this.itSubcategory, "itSubcategory");
            // 
            // itInvNum
            // 
            resources.ApplyResources(this.itInvNum, "itInvNum");
            // 
            // itLocation
            // 
            resources.ApplyResources(this.itLocation, "itLocation");
            // 
            // itKeywords
            // 
            this.itKeywords.AspectName = "";
            resources.ApplyResources(this.itKeywords, "itKeywords");
            // 
            // itCounts
            // 
            this.itCounts.AspectName = "";
            resources.ApplyResources(this.itCounts, "itCounts");
            // 
            // itBorrowed
            // 
            resources.ApplyResources(this.itBorrowed, "itBorrowed");
            // 
            // itExcluded
            // 
            resources.ApplyResources(this.itExcluded, "itExcluded");
            // 
            // tabBooks
            // 
            this.tabBooks.Controls.Add(this.olvBooks);
            resources.ApplyResources(this.tabBooks, "tabBooks");
            this.tabBooks.Name = "tabBooks";
            this.tabBooks.UseVisualStyleBackColor = true;
            // 
            // olvBooks
            // 
            this.olvBooks.AllColumns.Add(this.bkFastTags);
            this.olvBooks.AllColumns.Add(this.bkName);
            this.olvBooks.AllColumns.Add(this.bkAuthor);
            this.olvBooks.AllColumns.Add(this.bkType);
            this.olvBooks.AllColumns.Add(this.bkYear);
            this.olvBooks.AllColumns.Add(this.bkGenre);
            this.olvBooks.AllColumns.Add(this.bkSubgenre);
            this.olvBooks.AllColumns.Add(this.bkInvNum);
            this.olvBooks.AllColumns.Add(this.bkLocation);
            this.olvBooks.AllColumns.Add(this.bkKeywords);
            this.olvBooks.AllColumns.Add(this.bkSeries);
            resources.ApplyResources(this.olvBooks, "olvBooks");
            this.olvBooks.CellEditUseWholeCell = false;
            this.olvBooks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.bkFastTags,
            this.bkName,
            this.bkAuthor,
            this.bkType,
            this.bkYear,
            this.bkGenre,
            this.bkSubgenre,
            this.bkInvNum,
            this.bkLocation,
            this.bkKeywords,
            this.bkSeries});
            this.olvBooks.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvBooks.FullRowSelect = true;
            this.olvBooks.GridLines = true;
            this.olvBooks.Name = "olvBooks";
            this.olvBooks.ShowGroups = false;
            this.olvBooks.SmallImageList = this.imgOLV;
            this.olvBooks.UseCompatibleStateImageBehavior = false;
            this.olvBooks.View = System.Windows.Forms.View.Details;
            this.olvBooks.VirtualMode = true;
            this.olvBooks.SelectedIndexChanged += new System.EventHandler(this.olvBooks_SelectedIndexChanged);
            this.olvBooks.DoubleClick += new System.EventHandler(this.olvBooks_DoubleClick);
            // 
            // bkFastTags
            // 
            resources.ApplyResources(this.bkFastTags, "bkFastTags");
            // 
            // bkName
            // 
            this.bkName.AspectName = "";
            resources.ApplyResources(this.bkName, "bkName");
            // 
            // bkAuthor
            // 
            this.bkAuthor.AspectName = "";
            resources.ApplyResources(this.bkAuthor, "bkAuthor");
            // 
            // bkType
            // 
            resources.ApplyResources(this.bkType, "bkType");
            // 
            // bkYear
            // 
            this.bkYear.AspectName = "";
            resources.ApplyResources(this.bkYear, "bkYear");
            // 
            // bkGenre
            // 
            this.bkGenre.AspectName = "";
            resources.ApplyResources(this.bkGenre, "bkGenre");
            // 
            // bkSubgenre
            // 
            resources.ApplyResources(this.bkSubgenre, "bkSubgenre");
            // 
            // bkInvNum
            // 
            resources.ApplyResources(this.bkInvNum, "bkInvNum");
            // 
            // bkLocation
            // 
            resources.ApplyResources(this.bkLocation, "bkLocation");
            // 
            // bkKeywords
            // 
            resources.ApplyResources(this.bkKeywords, "bkKeywords");
            // 
            // bkSeries
            // 
            resources.ApplyResources(this.bkSeries, "bkSeries");
            // 
            // tabGames
            // 
            resources.ApplyResources(this.tabGames, "tabGames");
            this.tabGames.Name = "tabGames";
            this.tabGames.UseVisualStyleBackColor = true;
            // 
            // tabAudio
            // 
            resources.ApplyResources(this.tabAudio, "tabAudio");
            this.tabAudio.Name = "tabAudio";
            this.tabAudio.UseVisualStyleBackColor = true;
            // 
            // tabVideo
            // 
            resources.ApplyResources(this.tabVideo, "tabVideo");
            this.tabVideo.Name = "tabVideo";
            this.tabVideo.UseVisualStyleBackColor = true;
            // 
            // tabFoto
            // 
            resources.ApplyResources(this.tabFoto, "tabFoto");
            this.tabFoto.Name = "tabFoto";
            this.tabFoto.UseVisualStyleBackColor = true;
            // 
            // imgBarList
            // 
            this.imgBarList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgBarList.ImageStream")));
            this.imgBarList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgBarList.Images.SetKeyName(0, "Contact");
            this.imgBarList.Images.SetKeyName(1, "Borrowing");
            this.imgBarList.Images.SetKeyName(2, "Rezervations");
            this.imgBarList.Images.SetKeyName(3, "Item");
            this.imgBarList.Images.SetKeyName(4, "Books");
            this.imgBarList.Images.SetKeyName(5, "Dice");
            this.imgBarList.Images.SetKeyName(6, "Song");
            this.imgBarList.Images.SetKeyName(7, "Video");
            // 
            // toolStripEditItem
            // 
            resources.ApplyResources(this.toolStripEditItem, "toolStripEditItem");
            this.toolStripEditItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewItem,
            this.btnEditItem,
            this.btnDeleteItem});
            this.toolStripEditItem.Name = "toolStripEditItem";
            // 
            // btnNewItem
            // 
            this.btnNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewItem.Image = global::Katalog.Properties.Resources.newItem;
            resources.ApplyResources(this.btnNewItem, "btnNewItem");
            this.btnNewItem.Name = "btnNewItem";
            this.btnNewItem.Click += new System.EventHandler(this.btnNewItem_Click);
            // 
            // btnEditItem
            // 
            this.btnEditItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditItem.Image = global::Katalog.Properties.Resources.edit;
            resources.ApplyResources(this.btnEditItem, "btnEditItem");
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Click += new System.EventHandler(this.btnEditItem_Click);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteItem.Image = global::Katalog.Properties.Resources.delete;
            resources.ApplyResources(this.btnDeleteItem, "btnDeleteItem");
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // toolStripFilter
            // 
            resources.ApplyResources(this.toolStripFilter, "toolStripFilter");
            this.toolStripFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFilter,
            this.txtFilter,
            this.cbFilterCol,
            this.btnFilterPin1,
            this.btnFilterPin2,
            this.btnFilterPin3,
            this.btnFilterPin4,
            this.btnFilterPin5,
            this.btnFilterPin6});
            this.toolStripFilter.Name = "toolStripFilter";
            // 
            // lblFilter
            // 
            this.lblFilter.Name = "lblFilter";
            resources.ApplyResources(this.lblFilter, "lblFilter");
            // 
            // txtFilter
            // 
            this.txtFilter.Name = "txtFilter";
            resources.ApplyResources(this.txtFilter, "txtFilter");
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // cbFilterCol
            // 
            this.cbFilterCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterCol.Name = "cbFilterCol";
            resources.ApplyResources(this.cbFilterCol, "cbFilterCol");
            this.cbFilterCol.SelectedIndexChanged += new System.EventHandler(this.cbFilterCol_SelectedIndexChanged);
            // 
            // btnFilterPin1
            // 
            this.btnFilterPin1.CheckOnClick = true;
            this.btnFilterPin1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterPin1.Image = global::Katalog.Properties.Resources.circ_green;
            resources.ApplyResources(this.btnFilterPin1, "btnFilterPin1");
            this.btnFilterPin1.Name = "btnFilterPin1";
            // 
            // btnFilterPin2
            // 
            this.btnFilterPin2.CheckOnClick = true;
            this.btnFilterPin2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterPin2.Image = global::Katalog.Properties.Resources.circ_red;
            resources.ApplyResources(this.btnFilterPin2, "btnFilterPin2");
            this.btnFilterPin2.Name = "btnFilterPin2";
            // 
            // btnFilterPin3
            // 
            this.btnFilterPin3.CheckOnClick = true;
            this.btnFilterPin3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterPin3.Image = global::Katalog.Properties.Resources.circ_orange;
            resources.ApplyResources(this.btnFilterPin3, "btnFilterPin3");
            this.btnFilterPin3.Name = "btnFilterPin3";
            // 
            // btnFilterPin4
            // 
            this.btnFilterPin4.CheckOnClick = true;
            this.btnFilterPin4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterPin4.Image = global::Katalog.Properties.Resources.circ_yellow;
            resources.ApplyResources(this.btnFilterPin4, "btnFilterPin4");
            this.btnFilterPin4.Name = "btnFilterPin4";
            // 
            // btnFilterPin5
            // 
            this.btnFilterPin5.CheckOnClick = true;
            this.btnFilterPin5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterPin5.Image = global::Katalog.Properties.Resources.circ_grey;
            resources.ApplyResources(this.btnFilterPin5, "btnFilterPin5");
            this.btnFilterPin5.Name = "btnFilterPin5";
            // 
            // btnFilterPin6
            // 
            this.btnFilterPin6.CheckOnClick = true;
            this.btnFilterPin6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterPin6.Image = global::Katalog.Properties.Resources.Circle_Blue;
            resources.ApplyResources(this.btnFilterPin6, "btnFilterPin6");
            this.btnFilterPin6.Name = "btnFilterPin6";
            // 
            // toolStripFastFilter
            // 
            resources.ApplyResources(this.toolStripFastFilter, "toolStripFastFilter");
            this.toolStripFastFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFilterA,
            this.btnFilterB,
            this.btnFilterC,
            this.btnFilterCC,
            this.btnFilterD,
            this.btnFilterDD,
            this.btnFilterE,
            this.btnFilterF,
            this.btnFilterG,
            this.btnFilterH,
            this.btnFilterCH,
            this.btnFilterI,
            this.btnFilterJ,
            this.btnFilterK,
            this.btnFilterL,
            this.btnFilterM,
            this.btnFilterN,
            this.btnFilterNN,
            this.btnFilterO,
            this.btnFilterP,
            this.btnFilterQ,
            this.btnFilterR,
            this.btnFilterRR,
            this.btnFilterS,
            this.btnFilterSS,
            this.btnFilterT,
            this.btnFilterTT,
            this.btnFilterU,
            this.btnFilterV,
            this.btnFilterW,
            this.btnFilterX,
            this.btnFilterY,
            this.btnFilterZ,
            this.btnFilterZZ,
            this.toolStripSeparator1,
            this.btnFilter09,
            this.cbFastFilterCol});
            this.toolStripFastFilter.Name = "toolStripFastFilter";
            // 
            // btnFilterA
            // 
            this.btnFilterA.CheckOnClick = true;
            this.btnFilterA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterA, "btnFilterA");
            this.btnFilterA.Name = "btnFilterA";
            this.btnFilterA.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterB
            // 
            this.btnFilterB.CheckOnClick = true;
            this.btnFilterB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterB, "btnFilterB");
            this.btnFilterB.Name = "btnFilterB";
            this.btnFilterB.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterC
            // 
            this.btnFilterC.CheckOnClick = true;
            this.btnFilterC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterC, "btnFilterC");
            this.btnFilterC.Name = "btnFilterC";
            this.btnFilterC.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterCC
            // 
            this.btnFilterCC.CheckOnClick = true;
            this.btnFilterCC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterCC, "btnFilterCC");
            this.btnFilterCC.Name = "btnFilterCC";
            this.btnFilterCC.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterD
            // 
            this.btnFilterD.CheckOnClick = true;
            this.btnFilterD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterD, "btnFilterD");
            this.btnFilterD.Name = "btnFilterD";
            this.btnFilterD.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterDD
            // 
            this.btnFilterDD.CheckOnClick = true;
            this.btnFilterDD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterDD, "btnFilterDD");
            this.btnFilterDD.Name = "btnFilterDD";
            this.btnFilterDD.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterE
            // 
            this.btnFilterE.CheckOnClick = true;
            this.btnFilterE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterE, "btnFilterE");
            this.btnFilterE.Name = "btnFilterE";
            this.btnFilterE.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterF
            // 
            this.btnFilterF.CheckOnClick = true;
            this.btnFilterF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterF, "btnFilterF");
            this.btnFilterF.Name = "btnFilterF";
            this.btnFilterF.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterG
            // 
            this.btnFilterG.CheckOnClick = true;
            this.btnFilterG.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterG, "btnFilterG");
            this.btnFilterG.Name = "btnFilterG";
            this.btnFilterG.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterH
            // 
            this.btnFilterH.CheckOnClick = true;
            this.btnFilterH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterH, "btnFilterH");
            this.btnFilterH.Name = "btnFilterH";
            this.btnFilterH.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterCH
            // 
            this.btnFilterCH.CheckOnClick = true;
            this.btnFilterCH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterCH, "btnFilterCH");
            this.btnFilterCH.Name = "btnFilterCH";
            this.btnFilterCH.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterI
            // 
            this.btnFilterI.CheckOnClick = true;
            this.btnFilterI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterI, "btnFilterI");
            this.btnFilterI.Name = "btnFilterI";
            this.btnFilterI.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterJ
            // 
            this.btnFilterJ.CheckOnClick = true;
            this.btnFilterJ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterJ, "btnFilterJ");
            this.btnFilterJ.Name = "btnFilterJ";
            this.btnFilterJ.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterK
            // 
            this.btnFilterK.CheckOnClick = true;
            this.btnFilterK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterK, "btnFilterK");
            this.btnFilterK.Name = "btnFilterK";
            this.btnFilterK.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterL
            // 
            this.btnFilterL.CheckOnClick = true;
            this.btnFilterL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterL, "btnFilterL");
            this.btnFilterL.Name = "btnFilterL";
            this.btnFilterL.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterM
            // 
            this.btnFilterM.CheckOnClick = true;
            this.btnFilterM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterM, "btnFilterM");
            this.btnFilterM.Name = "btnFilterM";
            this.btnFilterM.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterN
            // 
            this.btnFilterN.CheckOnClick = true;
            this.btnFilterN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterN, "btnFilterN");
            this.btnFilterN.Name = "btnFilterN";
            this.btnFilterN.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterNN
            // 
            this.btnFilterNN.CheckOnClick = true;
            this.btnFilterNN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterNN, "btnFilterNN");
            this.btnFilterNN.Name = "btnFilterNN";
            this.btnFilterNN.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterO
            // 
            this.btnFilterO.CheckOnClick = true;
            this.btnFilterO.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterO, "btnFilterO");
            this.btnFilterO.Name = "btnFilterO";
            this.btnFilterO.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterP
            // 
            this.btnFilterP.CheckOnClick = true;
            this.btnFilterP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterP, "btnFilterP");
            this.btnFilterP.Name = "btnFilterP";
            this.btnFilterP.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterQ
            // 
            this.btnFilterQ.CheckOnClick = true;
            this.btnFilterQ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterQ, "btnFilterQ");
            this.btnFilterQ.Name = "btnFilterQ";
            this.btnFilterQ.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterR
            // 
            this.btnFilterR.CheckOnClick = true;
            this.btnFilterR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterR, "btnFilterR");
            this.btnFilterR.Name = "btnFilterR";
            this.btnFilterR.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterRR
            // 
            this.btnFilterRR.CheckOnClick = true;
            this.btnFilterRR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterRR, "btnFilterRR");
            this.btnFilterRR.Name = "btnFilterRR";
            this.btnFilterRR.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterS
            // 
            this.btnFilterS.CheckOnClick = true;
            this.btnFilterS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterS, "btnFilterS");
            this.btnFilterS.Name = "btnFilterS";
            this.btnFilterS.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterSS
            // 
            this.btnFilterSS.CheckOnClick = true;
            this.btnFilterSS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterSS, "btnFilterSS");
            this.btnFilterSS.Name = "btnFilterSS";
            this.btnFilterSS.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterT
            // 
            this.btnFilterT.CheckOnClick = true;
            this.btnFilterT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterT, "btnFilterT");
            this.btnFilterT.Name = "btnFilterT";
            this.btnFilterT.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterTT
            // 
            this.btnFilterTT.CheckOnClick = true;
            this.btnFilterTT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterTT, "btnFilterTT");
            this.btnFilterTT.Name = "btnFilterTT";
            this.btnFilterTT.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterU
            // 
            this.btnFilterU.CheckOnClick = true;
            this.btnFilterU.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterU, "btnFilterU");
            this.btnFilterU.Name = "btnFilterU";
            this.btnFilterU.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterV
            // 
            this.btnFilterV.CheckOnClick = true;
            this.btnFilterV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterV, "btnFilterV");
            this.btnFilterV.Name = "btnFilterV";
            this.btnFilterV.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterW
            // 
            this.btnFilterW.CheckOnClick = true;
            this.btnFilterW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterW, "btnFilterW");
            this.btnFilterW.Name = "btnFilterW";
            this.btnFilterW.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterX
            // 
            this.btnFilterX.CheckOnClick = true;
            this.btnFilterX.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterX, "btnFilterX");
            this.btnFilterX.Name = "btnFilterX";
            this.btnFilterX.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterY
            // 
            this.btnFilterY.CheckOnClick = true;
            this.btnFilterY.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterY, "btnFilterY");
            this.btnFilterY.Name = "btnFilterY";
            this.btnFilterY.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterZ
            // 
            this.btnFilterZ.CheckOnClick = true;
            this.btnFilterZ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterZ, "btnFilterZ");
            this.btnFilterZ.Name = "btnFilterZ";
            this.btnFilterZ.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // btnFilterZZ
            // 
            this.btnFilterZZ.CheckOnClick = true;
            this.btnFilterZZ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilterZZ, "btnFilterZZ");
            this.btnFilterZZ.Name = "btnFilterZZ";
            this.btnFilterZZ.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // btnFilter09
            // 
            this.btnFilter09.CheckOnClick = true;
            this.btnFilter09.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnFilter09, "btnFilter09");
            this.btnFilter09.Name = "btnFilter09";
            this.btnFilter09.Click += new System.EventHandler(this.btnFilterA_Click);
            // 
            // cbFastFilterCol
            // 
            this.cbFastFilterCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFastFilterCol.Name = "cbFastFilterCol";
            resources.ApplyResources(this.cbFastFilterCol, "cbFastFilterCol");
            this.cbFastFilterCol.SelectedIndexChanged += new System.EventHandler(this.cbFastFilterCol_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuShow,
            this.mnuLists,
            this.mnuTools,
            this.mnuHelp});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewDB,
            this.mnuOpenDB,
            this.mnuSavaAsDB,
            this.toolStripMenuItem1,
            this.mnuImport,
            this.mnuExport,
            this.toolStripMenuItem4,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            resources.ApplyResources(this.mnuFile, "mnuFile");
            // 
            // mnuNewDB
            // 
            this.mnuNewDB.Name = "mnuNewDB";
            resources.ApplyResources(this.mnuNewDB, "mnuNewDB");
            // 
            // mnuOpenDB
            // 
            this.mnuOpenDB.Name = "mnuOpenDB";
            resources.ApplyResources(this.mnuOpenDB, "mnuOpenDB");
            // 
            // mnuSavaAsDB
            // 
            this.mnuSavaAsDB.Image = global::Katalog.Properties.Resources.save;
            this.mnuSavaAsDB.Name = "mnuSavaAsDB";
            resources.ApplyResources(this.mnuSavaAsDB, "mnuSavaAsDB");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // mnuImport
            // 
            this.mnuImport.Name = "mnuImport";
            resources.ApplyResources(this.mnuImport, "mnuImport");
            this.mnuImport.Click += new System.EventHandler(this.mnuImport_Click);
            // 
            // mnuExport
            // 
            this.mnuExport.Name = "mnuExport";
            resources.ApplyResources(this.mnuExport, "mnuExport");
            this.mnuExport.Click += new System.EventHandler(this.mnuExport_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            resources.ApplyResources(this.mnuExit, "mnuExit");
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewItem,
            this.mnuEditItem,
            this.mnuDelItem});
            this.mnuEdit.Name = "mnuEdit";
            resources.ApplyResources(this.mnuEdit, "mnuEdit");
            // 
            // mnuNewItem
            // 
            this.mnuNewItem.Image = global::Katalog.Properties.Resources.newItem;
            this.mnuNewItem.Name = "mnuNewItem";
            resources.ApplyResources(this.mnuNewItem, "mnuNewItem");
            this.mnuNewItem.Click += new System.EventHandler(this.btnNewItem_Click);
            // 
            // mnuEditItem
            // 
            this.mnuEditItem.Image = global::Katalog.Properties.Resources.edit;
            this.mnuEditItem.Name = "mnuEditItem";
            resources.ApplyResources(this.mnuEditItem, "mnuEditItem");
            this.mnuEditItem.Click += new System.EventHandler(this.btnEditItem_Click);
            // 
            // mnuDelItem
            // 
            this.mnuDelItem.Image = global::Katalog.Properties.Resources.delete;
            this.mnuDelItem.Name = "mnuDelItem";
            resources.ApplyResources(this.mnuDelItem, "mnuDelItem");
            this.mnuDelItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // mnuShow
            // 
            this.mnuShow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolBars,
            this.toolStripMenuItem2,
            this.mnuShowContacts,
            this.mnuShowBorrowing,
            this.mnuShowReservations,
            this.toolStripMenuItem3,
            this.mnuShowItems,
            this.mnuShowBooks,
            this.mnuShowBoardGames,
            this.mnuShowAudio,
            this.mnuShowVideo,
            this.mnuShowPhoto});
            this.mnuShow.Name = "mnuShow";
            resources.ApplyResources(this.mnuShow, "mnuShow");
            // 
            // mnuToolBars
            // 
            this.mnuToolBars.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filtryToolStripMenuItem});
            this.mnuToolBars.Name = "mnuToolBars";
            resources.ApplyResources(this.mnuToolBars, "mnuToolBars");
            // 
            // filtryToolStripMenuItem
            // 
            this.filtryToolStripMenuItem.Name = "filtryToolStripMenuItem";
            resources.ApplyResources(this.filtryToolStripMenuItem, "filtryToolStripMenuItem");
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // mnuShowContacts
            // 
            this.mnuShowContacts.Name = "mnuShowContacts";
            resources.ApplyResources(this.mnuShowContacts, "mnuShowContacts");
            // 
            // mnuShowBorrowing
            // 
            this.mnuShowBorrowing.Name = "mnuShowBorrowing";
            resources.ApplyResources(this.mnuShowBorrowing, "mnuShowBorrowing");
            // 
            // mnuShowReservations
            // 
            this.mnuShowReservations.Name = "mnuShowReservations";
            resources.ApplyResources(this.mnuShowReservations, "mnuShowReservations");
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // mnuShowBooks
            // 
            this.mnuShowBooks.Name = "mnuShowBooks";
            resources.ApplyResources(this.mnuShowBooks, "mnuShowBooks");
            // 
            // mnuShowBoardGames
            // 
            this.mnuShowBoardGames.Name = "mnuShowBoardGames";
            resources.ApplyResources(this.mnuShowBoardGames, "mnuShowBoardGames");
            // 
            // mnuShowAudio
            // 
            this.mnuShowAudio.Name = "mnuShowAudio";
            resources.ApplyResources(this.mnuShowAudio, "mnuShowAudio");
            // 
            // mnuShowVideo
            // 
            this.mnuShowVideo.Name = "mnuShowVideo";
            resources.ApplyResources(this.mnuShowVideo, "mnuShowVideo");
            // 
            // mnuShowPhoto
            // 
            this.mnuShowPhoto.Name = "mnuShowPhoto";
            resources.ApplyResources(this.mnuShowPhoto, "mnuShowPhoto");
            // 
            // mnuLists
            // 
            this.mnuLists.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBookLists});
            this.mnuLists.Name = "mnuLists";
            resources.ApplyResources(this.mnuLists, "mnuLists");
            // 
            // mnuBookLists
            // 
            this.mnuBookLists.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAuthorList,
            this.překladatelToolStripMenuItem,
            this.mnuIllustratorList,
            this.mnuGenre,
            this.mnuSeries,
            this.mnuLanguageLists,
            this.mnuPublishing});
            this.mnuBookLists.Name = "mnuBookLists";
            resources.ApplyResources(this.mnuBookLists, "mnuBookLists");
            // 
            // mnuAuthorList
            // 
            this.mnuAuthorList.Name = "mnuAuthorList";
            resources.ApplyResources(this.mnuAuthorList, "mnuAuthorList");
            // 
            // překladatelToolStripMenuItem
            // 
            this.překladatelToolStripMenuItem.Name = "překladatelToolStripMenuItem";
            resources.ApplyResources(this.překladatelToolStripMenuItem, "překladatelToolStripMenuItem");
            // 
            // mnuIllustratorList
            // 
            this.mnuIllustratorList.Name = "mnuIllustratorList";
            resources.ApplyResources(this.mnuIllustratorList, "mnuIllustratorList");
            // 
            // mnuGenre
            // 
            this.mnuGenre.Name = "mnuGenre";
            resources.ApplyResources(this.mnuGenre, "mnuGenre");
            // 
            // mnuSeries
            // 
            this.mnuSeries.Name = "mnuSeries";
            resources.ApplyResources(this.mnuSeries, "mnuSeries");
            // 
            // mnuLanguageLists
            // 
            this.mnuLanguageLists.Name = "mnuLanguageLists";
            resources.ApplyResources(this.mnuLanguageLists, "mnuLanguageLists");
            // 
            // mnuPublishing
            // 
            this.mnuPublishing.Name = "mnuPublishing";
            resources.ApplyResources(this.mnuPublishing, "mnuPublishing");
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLanguage,
            this.toolStripMenuItem5,
            this.mnuOptions});
            this.mnuTools.Name = "mnuTools";
            resources.ApplyResources(this.mnuTools, "mnuTools");
            // 
            // mnuLanguage
            // 
            this.mnuLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLangEnglish});
            this.mnuLanguage.Name = "mnuLanguage";
            resources.ApplyResources(this.mnuLanguage, "mnuLanguage");
            // 
            // mnuLangEnglish
            // 
            this.mnuLangEnglish.Name = "mnuLangEnglish";
            resources.ApplyResources(this.mnuLangEnglish, "mnuLangEnglish");
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // mnuOptions
            // 
            this.mnuOptions.Image = global::Katalog.Properties.Resources.settings;
            this.mnuOptions.Name = "mnuOptions";
            resources.ApplyResources(this.mnuOptions, "mnuOptions");
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            resources.ApplyResources(this.mnuHelp, "mnuHelp");
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            resources.ApplyResources(this.mnuAbout, "mnuAbout");
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // statusBar
            // 
            resources.ApplyResources(this.statusBar, "statusBar");
            this.statusBar.Name = "statusBar";
            // 
            // mnuShowItems
            // 
            this.mnuShowItems.Name = "mnuShowItems";
            resources.ApplyResources(this.mnuShowItems, "mnuShowItems");
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.tabCatalog.ResumeLayout(false);
            this.tabContacts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvContacts)).EndInit();
            this.tabBorrowing.ResumeLayout(false);
            this.tabBorrowing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBorrowing)).EndInit();
            this.tabItems.ResumeLayout(false);
            this.tabItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvItem)).EndInit();
            this.tabBooks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvBooks)).EndInit();
            this.toolStripEditItem.ResumeLayout(false);
            this.toolStripEditItem.PerformLayout();
            this.toolStripFilter.ResumeLayout(false);
            this.toolStripFilter.PerformLayout();
            this.toolStripFastFilter.ResumeLayout(false);
            this.toolStripFastFilter.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.TabControl tabCatalog;
        private System.Windows.Forms.TabPage tabContacts;
        private System.Windows.Forms.TabPage tabBorrowing;
        private System.Windows.Forms.ToolStrip toolStripFilter;
        private System.Windows.Forms.ToolStripMenuItem mnuNewDB;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenDB;
        private System.Windows.Forms.ToolStripMenuItem mnuSavaAsDB;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuShow;
        private System.Windows.Forms.ToolStripMenuItem mnuToolBars;
        private System.Windows.Forms.ToolStripMenuItem filtryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuShowContacts;
        private System.Windows.Forms.ToolStripMenuItem mnuShowBorrowing;
        private System.Windows.Forms.ToolStripMenuItem mnuShowReservations;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuShowBooks;
        private System.Windows.Forms.ToolStripMenuItem mnuShowBoardGames;
        private System.Windows.Forms.ToolStripMenuItem mnuShowVideo;
        private System.Windows.Forms.ToolStripMenuItem mnuLists;
        private System.Windows.Forms.ToolStripMenuItem mnuBookLists;
        private System.Windows.Forms.ToolStripMenuItem mnuAuthorList;
        private System.Windows.Forms.ToolStripMenuItem překladatelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuIllustratorList;
        private System.Windows.Forms.ToolStripMenuItem mnuGenre;
        private System.Windows.Forms.ToolStripMenuItem mnuSeries;
        private System.Windows.Forms.ToolStripMenuItem mnuLanguageLists;
        private System.Windows.Forms.ToolStripMenuItem mnuPublishing;
        private System.Windows.Forms.ToolStripMenuItem mnuImport;
        private System.Windows.Forms.ToolStripMenuItem mnuExport;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.TabPage tabRezervation;
        private System.Windows.Forms.TabPage tabBooks;
        private System.Windows.Forms.TabPage tabGames;
        private System.Windows.Forms.TabPage tabAudio;
        private System.Windows.Forms.TabPage tabVideo;
        private System.Windows.Forms.TabPage tabFoto;
        private System.Windows.Forms.ToolStripLabel lblFilter;
        private System.Windows.Forms.ToolStripTextBox txtFilter;
        private System.Windows.Forms.ToolStripComboBox cbFilterCol;
        private System.Windows.Forms.ToolStripButton btnFilterPin1;
        private System.Windows.Forms.ToolStripButton btnFilterPin2;
        private System.Windows.Forms.ToolStripButton btnFilterPin3;
        private System.Windows.Forms.ToolStripButton btnFilterPin4;
        private System.Windows.Forms.ToolStripButton btnFilterPin5;
        private System.Windows.Forms.ToolStripButton btnFilterPin6;
        private System.Windows.Forms.ToolStrip toolStripEditItem;
        private System.Windows.Forms.ToolStripButton btnNewItem;
        private System.Windows.Forms.ToolStripButton btnEditItem;
        private System.Windows.Forms.ToolStripButton btnDeleteItem;
        private System.Windows.Forms.ToolStripMenuItem mnuNewItem;
        private System.Windows.Forms.ToolStripMenuItem mnuEditItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDelItem;
        private System.Windows.Forms.ToolStripMenuItem mnuShowAudio;
        private System.Windows.Forms.ToolStripMenuItem mnuShowPhoto;
        private System.Windows.Forms.ToolStripMenuItem mnuLanguage;
        private System.Windows.Forms.ToolStripMenuItem mnuLangEnglish;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStrip toolStripFastFilter;
        private System.Windows.Forms.ToolStripButton btnFilterA;
        private System.Windows.Forms.ToolStripButton btnFilterB;
        private System.Windows.Forms.ToolStripButton btnFilterC;
        private System.Windows.Forms.ToolStripButton btnFilterCC;
        private System.Windows.Forms.ToolStripButton btnFilterD;
        private System.Windows.Forms.ToolStripButton btnFilterE;
        private System.Windows.Forms.ToolStripButton btnFilterF;
        private System.Windows.Forms.ToolStripButton btnFilterG;
        private System.Windows.Forms.ToolStripButton btnFilterH;
        private System.Windows.Forms.ToolStripButton btnFilterCH;
        private System.Windows.Forms.ToolStripButton btnFilterI;
        private System.Windows.Forms.ToolStripButton btnFilterJ;
        private System.Windows.Forms.ToolStripButton btnFilterK;
        private System.Windows.Forms.ToolStripButton btnFilterL;
        private System.Windows.Forms.ToolStripButton btnFilterM;
        private System.Windows.Forms.ToolStripButton btnFilterN;
        private System.Windows.Forms.ToolStripButton btnFilterNN;
        private System.Windows.Forms.ToolStripButton btnFilterO;
        private System.Windows.Forms.ToolStripButton btnFilterP;
        private System.Windows.Forms.ToolStripButton btnFilterQ;
        private System.Windows.Forms.ToolStripButton btnFilterR;
        private System.Windows.Forms.ToolStripButton btnFilterRR;
        private System.Windows.Forms.ToolStripButton btnFilterS;
        private System.Windows.Forms.ToolStripButton btnFilterSS;
        private System.Windows.Forms.ToolStripButton btnFilterT;
        private System.Windows.Forms.ToolStripButton btnFilterTT;
        private System.Windows.Forms.ToolStripButton btnFilterU;
        private System.Windows.Forms.ToolStripButton btnFilterV;
        private System.Windows.Forms.ToolStripButton btnFilterW;
        private System.Windows.Forms.ToolStripButton btnFilterX;
        private System.Windows.Forms.ToolStripButton btnFilterY;
        private System.Windows.Forms.ToolStripButton btnFilterZ;
        private System.Windows.Forms.ToolStripButton btnFilterZZ;
        private System.Windows.Forms.ToolStripButton btnFilterDD;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFilter09;
        private System.Windows.Forms.ToolStripComboBox cbFastFilterCol;
        private BrightIdeasSoftware.FastObjectListView olvContacts;
        private System.Windows.Forms.ImageList imgBarList;
        private BrightIdeasSoftware.OLVColumn conName;
        private System.Windows.Forms.Button btnTest;
        private BrightIdeasSoftware.OLVColumn conSurname;
        private BrightIdeasSoftware.OLVColumn conPhone;
        private BrightIdeasSoftware.OLVColumn conEmail;
        private BrightIdeasSoftware.OLVColumn conAddress;
        private BrightIdeasSoftware.FastObjectListView olvBooks;
        private BrightIdeasSoftware.OLVColumn bkName;
        private BrightIdeasSoftware.OLVColumn bkAuthor;
        private BrightIdeasSoftware.OLVColumn bkYear;
        private BrightIdeasSoftware.OLVColumn bkGenre;
        private BrightIdeasSoftware.OLVColumn bkSubgenre;
        private BrightIdeasSoftware.OLVColumn bkKeywords;
        private BrightIdeasSoftware.OLVColumn bkInvNum;
        private BrightIdeasSoftware.OLVColumn bkLocation;
        private BrightIdeasSoftware.OLVColumn bkType;
        private BrightIdeasSoftware.OLVColumn bkSeries;
        private BrightIdeasSoftware.FastObjectListView olvBorrowing;
        private BrightIdeasSoftware.OLVColumn brType;
        private BrightIdeasSoftware.OLVColumn brName;
        private BrightIdeasSoftware.OLVColumn brPerson;
        private BrightIdeasSoftware.OLVColumn brFrom;
        private BrightIdeasSoftware.OLVColumn brTo;
        private BrightIdeasSoftware.OLVColumn brReturned;
        private System.Windows.Forms.CheckBox chbShowReturned;
        private System.Windows.Forms.TabPage tabItems;
        private BrightIdeasSoftware.FastObjectListView olvItem;
        private BrightIdeasSoftware.OLVColumn itName;
        private BrightIdeasSoftware.OLVColumn itCategory;
        private BrightIdeasSoftware.OLVColumn itSubcategory;
        private BrightIdeasSoftware.OLVColumn itKeywords;
        private BrightIdeasSoftware.OLVColumn itCounts;
        private BrightIdeasSoftware.OLVColumn itLocation;
        private BrightIdeasSoftware.OLVColumn itInvNum;
        private BrightIdeasSoftware.OLVColumn itBorrowed;
        private BrightIdeasSoftware.OLVColumn itExcluded;
        private BrightIdeasSoftware.OLVColumn itFastTags;
        private System.Windows.Forms.ImageList imgOLV;
        private System.Windows.Forms.CheckBox chbShowExcluded;
        private BrightIdeasSoftware.OLVColumn conFastTags;
        private BrightIdeasSoftware.OLVColumn bkFastTags;
        private System.Windows.Forms.ToolStripMenuItem mnuShowItems;
    }
}

