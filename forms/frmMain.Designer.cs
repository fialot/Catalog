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
            this.TabBars = new System.Windows.Forms.TabControl();
            this.tabContacts = new System.Windows.Forms.TabPage();
            this.chbShowUnactivCon = new System.Windows.Forms.CheckBox();
            this.btnPrintTest = new System.Windows.Forms.Button();
            this.btnPersonalLending = new System.Windows.Forms.Button();
            this.imgBarList = new System.Windows.Forms.ImageList(this.components);
            this.btnTest = new System.Windows.Forms.Button();
            this.olvContacts = new BrightIdeasSoftware.FastObjectListView();
            this.conFastTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conSurname = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conNick = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conPhone = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conEmail = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conAddress = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conCompany = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.conFastTagsNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.mnuContacts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCLending = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCBorrowing = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.synchronizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCImportGoogle = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCExportGoogle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.setToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCSetGreenTag = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCSetRedTag = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCSetOrangeTag = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCSetYellowTag = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCSetGreyTag = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCSetBlueTag = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCActive = new System.Windows.Forms.ToolStripMenuItem();
            this.imgOLV = new System.Windows.Forms.ImageList(this.components);
            this.tabLending = new System.Windows.Forms.TabPage();
            this.olvLending = new BrightIdeasSoftware.FastObjectListView();
            this.ldFastTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ldPerson = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ldItemType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ldItemName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ldItemNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ldItemInvNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ldFrom = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ldTo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ldStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ldNote = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ldFastTagsNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cbLendingShow = new System.Windows.Forms.ComboBox();
            this.lblLendingShow = new System.Windows.Forms.Label();
            this.chbShowReturned = new System.Windows.Forms.CheckBox();
            this.tabBorrowing = new System.Windows.Forms.TabPage();
            this.btnClearOldReservations = new System.Windows.Forms.Button();
            this.btnBorrowings = new System.Windows.Forms.Button();
            this.cbBorrowingShow = new System.Windows.Forms.ComboBox();
            this.lblShowBorrowing = new System.Windows.Forms.Label();
            this.chbBorrowingReturned = new System.Windows.Forms.CheckBox();
            this.olvBorrowing = new BrightIdeasSoftware.FastObjectListView();
            this.brFastTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brPerson = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brItemName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brItemInvNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brFrom = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brTo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brNote = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brFastTagsNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
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
            this.itAvailable = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itExcluded = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.itFastTagsNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabBooks = new System.Windows.Forms.TabPage();
            this.olvBooks = new BrightIdeasSoftware.FastObjectListView();
            this.bkFastTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkAuthor = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkCount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkAvailable = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkYear = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkGenre = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkSubgenre = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkInvNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkLocation = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkKeywords = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkSeries = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bkFastTagsNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabBoardGames = new System.Windows.Forms.TabPage();
            this.chbShowExcludedBoard = new System.Windows.Forms.CheckBox();
            this.olvBoard = new BrightIdeasSoftware.FastObjectListView();
            this.bgTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bgName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bgCategory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bgInvNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bgLocation = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bgKeywords = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bgCounts = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bgAvailable = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bgExcluded = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.bgTagsNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabGames = new System.Windows.Forms.TabPage();
            this.chbShowExcludedGames = new System.Windows.Forms.CheckBox();
            this.olvGames = new BrightIdeasSoftware.FastObjectListView();
            this.gmTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gmName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gmCategory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gmEnviroment = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gmMinPlayers = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gmMaxPlayers = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gmPlayerAge = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gmDuration = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gmKeywords = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gmThings = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gmExcluded = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.gmTagsNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabRecipes = new System.Windows.Forms.TabPage();
            this.chbShowExcludedRecipes = new System.Windows.Forms.CheckBox();
            this.olvRecipes = new BrightIdeasSoftware.FastObjectListView();
            this.recTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.recName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.recCategory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.recKeywords = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.recExcluded = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.recTagsNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabObject = new System.Windows.Forms.TabPage();
            this.cbObjectShow = new System.Windows.Forms.ComboBox();
            this.lblObjectShow = new System.Windows.Forms.Label();
            this.chbShowExcludedObjects = new System.Windows.Forms.CheckBox();
            this.olvObjects = new BrightIdeasSoftware.FastObjectListView();
            this.objTags = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objCategory = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objNumber = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objParent = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objKeywords = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objCustomer = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objDevelopment = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objFolder = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objVersion = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objActive = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.objTagsNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabAudio = new System.Windows.Forms.TabPage();
            this.tabVideo = new System.Windows.Forms.TabPage();
            this.tabFoto = new System.Windows.Forms.TabPage();
            this.toolEditItem = new System.Windows.Forms.ToolStrip();
            this.btnNewItem = new System.Windows.Forms.ToolStripButton();
            this.btnEditItem = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolFastFilter = new System.Windows.Forms.ToolStrip();
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
            this.toolFilter = new System.Windows.Forms.ToolStrip();
            this.lblFilter = new System.Windows.Forms.ToolStripLabel();
            this.txtFilter = new System.Windows.Forms.ToolStripTextBox();
            this.cbFilterCol = new System.Windows.Forms.ToolStripComboBox();
            this.btnFilterPin1 = new System.Windows.Forms.ToolStripButton();
            this.btnFilterPin2 = new System.Windows.Forms.ToolStripButton();
            this.btnFilterPin3 = new System.Windows.Forms.ToolStripButton();
            this.btnFilterPin4 = new System.Windows.Forms.ToolStripButton();
            this.btnFilterPin5 = new System.Windows.Forms.ToolStripButton();
            this.btnFilterPin6 = new System.Windows.Forms.ToolStripButton();
            this.brType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.brItemNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewDB = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenDB = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSavaAsDB = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImportFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuImportGoogleContacts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolBars = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowFastFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowContacts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowLending = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowBorrowing = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowItems = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowBoardGames = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowGames = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowRecipes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowObjects = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSubTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecalculateAvailableItems = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.TabBars.SuspendLayout();
            this.tabContacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvContacts)).BeginInit();
            this.mnuContacts.SuspendLayout();
            this.tabLending.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvLending)).BeginInit();
            this.tabBorrowing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBorrowing)).BeginInit();
            this.tabItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvItem)).BeginInit();
            this.tabBooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBooks)).BeginInit();
            this.tabBoardGames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBoard)).BeginInit();
            this.tabGames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvGames)).BeginInit();
            this.tabRecipes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvRecipes)).BeginInit();
            this.tabObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvObjects)).BeginInit();
            this.toolEditItem.SuspendLayout();
            this.toolFastFilter.SuspendLayout();
            this.toolFilter.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.TabBars);
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolEditItem);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolFastFilter);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolFilter);
            // 
            // TabBars
            // 
            this.TabBars.Controls.Add(this.tabContacts);
            this.TabBars.Controls.Add(this.tabLending);
            this.TabBars.Controls.Add(this.tabBorrowing);
            this.TabBars.Controls.Add(this.tabItems);
            this.TabBars.Controls.Add(this.tabBooks);
            this.TabBars.Controls.Add(this.tabBoardGames);
            this.TabBars.Controls.Add(this.tabGames);
            this.TabBars.Controls.Add(this.tabRecipes);
            this.TabBars.Controls.Add(this.tabObject);
            this.TabBars.Controls.Add(this.tabAudio);
            this.TabBars.Controls.Add(this.tabVideo);
            this.TabBars.Controls.Add(this.tabFoto);
            resources.ApplyResources(this.TabBars, "TabBars");
            this.TabBars.ImageList = this.imgBarList;
            this.TabBars.Name = "TabBars";
            this.TabBars.SelectedIndex = 0;
            this.TabBars.SelectedIndexChanged += new System.EventHandler(this.tabCatalog_SelectedIndexChanged);
            // 
            // tabContacts
            // 
            this.tabContacts.Controls.Add(this.chbShowUnactivCon);
            this.tabContacts.Controls.Add(this.btnPrintTest);
            this.tabContacts.Controls.Add(this.btnPersonalLending);
            this.tabContacts.Controls.Add(this.btnTest);
            this.tabContacts.Controls.Add(this.olvContacts);
            resources.ApplyResources(this.tabContacts, "tabContacts");
            this.tabContacts.Name = "tabContacts";
            this.tabContacts.UseVisualStyleBackColor = true;
            // 
            // chbShowUnactivCon
            // 
            resources.ApplyResources(this.chbShowUnactivCon, "chbShowUnactivCon");
            this.chbShowUnactivCon.Name = "chbShowUnactivCon";
            this.chbShowUnactivCon.UseVisualStyleBackColor = true;
            this.chbShowUnactivCon.CheckedChanged += new System.EventHandler(this.chbUnactivateContacts_CheckedChanged);
            // 
            // btnPrintTest
            // 
            resources.ApplyResources(this.btnPrintTest, "btnPrintTest");
            this.btnPrintTest.Name = "btnPrintTest";
            this.btnPrintTest.UseVisualStyleBackColor = true;
            this.btnPrintTest.Click += new System.EventHandler(this.btnPrintTest_Click);
            // 
            // btnPersonalLending
            // 
            resources.ApplyResources(this.btnPersonalLending, "btnPersonalLending");
            this.btnPersonalLending.ImageList = this.imgBarList;
            this.btnPersonalLending.Name = "btnPersonalLending";
            this.btnPersonalLending.UseVisualStyleBackColor = true;
            this.btnPersonalLending.Click += new System.EventHandler(this.btnPersonalLending_Click);
            // 
            // imgBarList
            // 
            this.imgBarList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgBarList.ImageStream")));
            this.imgBarList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgBarList.Images.SetKeyName(0, "Contact");
            this.imgBarList.Images.SetKeyName(1, "Lending");
            this.imgBarList.Images.SetKeyName(2, "Borrowing");
            this.imgBarList.Images.SetKeyName(3, "Rezervations");
            this.imgBarList.Images.SetKeyName(4, "Item");
            this.imgBarList.Images.SetKeyName(5, "Books");
            this.imgBarList.Images.SetKeyName(6, "BoardGame");
            this.imgBarList.Images.SetKeyName(7, "Dice");
            this.imgBarList.Images.SetKeyName(8, "Recipes");
            this.imgBarList.Images.SetKeyName(9, "Object");
            this.imgBarList.Images.SetKeyName(10, "Song");
            this.imgBarList.Images.SetKeyName(11, "Video");
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
            this.olvContacts.AllColumns.Add(this.conNick);
            this.olvContacts.AllColumns.Add(this.conPhone);
            this.olvContacts.AllColumns.Add(this.conEmail);
            this.olvContacts.AllColumns.Add(this.conAddress);
            this.olvContacts.AllColumns.Add(this.conCompany);
            this.olvContacts.AllColumns.Add(this.conFastTagsNum);
            this.olvContacts.CellEditUseWholeCell = false;
            this.olvContacts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.conFastTags,
            this.conName,
            this.conSurname,
            this.conNick,
            this.conPhone,
            this.conEmail,
            this.conAddress,
            this.conCompany,
            this.conFastTagsNum});
            this.olvContacts.ContextMenuStrip = this.mnuContacts;
            this.olvContacts.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.olvContacts, "olvContacts");
            this.olvContacts.FullRowSelect = true;
            this.olvContacts.GridLines = true;
            this.olvContacts.Name = "olvContacts";
            this.olvContacts.ShowGroups = false;
            this.olvContacts.SmallImageList = this.imgOLV;
            this.olvContacts.UseCompatibleStateImageBehavior = false;
            this.olvContacts.View = System.Windows.Forms.View.Details;
            this.olvContacts.VirtualMode = true;
            this.olvContacts.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvContacts_FormatRow);
            this.olvContacts.SelectedIndexChanged += new System.EventHandler(this.olvContacts_SelectedIndexChanged);
            this.olvContacts.DoubleClick += new System.EventHandler(this.btnEditItem_Click);
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
            // conNick
            // 
            resources.ApplyResources(this.conNick, "conNick");
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
            // conCompany
            // 
            resources.ApplyResources(this.conCompany, "conCompany");
            // 
            // conFastTagsNum
            // 
            resources.ApplyResources(this.conFastTagsNum, "conFastTagsNum");
            // 
            // mnuContacts
            // 
            this.mnuContacts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCNew,
            this.mnuCEdit,
            this.mnuCDelete,
            this.toolStripMenuItem5,
            this.mnuCLending,
            this.mnuCBorrowing,
            this.toolStripMenuItem7,
            this.synchronizeToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripMenuItem6,
            this.setToolStripMenuItem,
            this.mnuCActive});
            this.mnuContacts.Name = "mnuContacts";
            resources.ApplyResources(this.mnuContacts, "mnuContacts");
            // 
            // mnuCNew
            // 
            this.mnuCNew.Image = global::Katalog.Properties.Resources.newItem;
            this.mnuCNew.Name = "mnuCNew";
            resources.ApplyResources(this.mnuCNew, "mnuCNew");
            this.mnuCNew.Click += new System.EventHandler(this.btnNewItem_Click);
            // 
            // mnuCEdit
            // 
            this.mnuCEdit.Image = global::Katalog.Properties.Resources.edit;
            this.mnuCEdit.Name = "mnuCEdit";
            resources.ApplyResources(this.mnuCEdit, "mnuCEdit");
            this.mnuCEdit.Click += new System.EventHandler(this.btnEditItem_Click);
            // 
            // mnuCDelete
            // 
            this.mnuCDelete.Image = global::Katalog.Properties.Resources.delete;
            this.mnuCDelete.Name = "mnuCDelete";
            resources.ApplyResources(this.mnuCDelete, "mnuCDelete");
            this.mnuCDelete.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // mnuCLending
            // 
            this.mnuCLending.Image = global::Katalog.Properties.Resources.connect;
            this.mnuCLending.Name = "mnuCLending";
            resources.ApplyResources(this.mnuCLending, "mnuCLending");
            this.mnuCLending.Click += new System.EventHandler(this.mnuCLending_Click);
            // 
            // mnuCBorrowing
            // 
            this.mnuCBorrowing.Image = global::Katalog.Properties.Resources.if_Stock_Index_Down_27880;
            this.mnuCBorrowing.Name = "mnuCBorrowing";
            resources.ApplyResources(this.mnuCBorrowing, "mnuCBorrowing");
            this.mnuCBorrowing.Click += new System.EventHandler(this.mnuCBorrowing_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
            // 
            // synchronizeToolStripMenuItem
            // 
            this.synchronizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCImportGoogle});
            this.synchronizeToolStripMenuItem.Name = "synchronizeToolStripMenuItem";
            resources.ApplyResources(this.synchronizeToolStripMenuItem, "synchronizeToolStripMenuItem");
            // 
            // mnuCImportGoogle
            // 
            this.mnuCImportGoogle.Name = "mnuCImportGoogle";
            resources.ApplyResources(this.mnuCImportGoogle, "mnuCImportGoogle");
            this.mnuCImportGoogle.Click += new System.EventHandler(this.mnuCImportGoogle_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCExportGoogle});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            resources.ApplyResources(this.exportToolStripMenuItem, "exportToolStripMenuItem");
            // 
            // mnuCExportGoogle
            // 
            this.mnuCExportGoogle.Name = "mnuCExportGoogle";
            resources.ApplyResources(this.mnuCExportGoogle, "mnuCExportGoogle");
            this.mnuCExportGoogle.Click += new System.EventHandler(this.mnuCExportGoogle_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
            // 
            // setToolStripMenuItem
            // 
            this.setToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCSetGreenTag,
            this.mnuCSetRedTag,
            this.mnuCSetOrangeTag,
            this.mnuCSetYellowTag,
            this.mnuCSetGreyTag,
            this.mnuCSetBlueTag});
            this.setToolStripMenuItem.Name = "setToolStripMenuItem";
            resources.ApplyResources(this.setToolStripMenuItem, "setToolStripMenuItem");
            // 
            // mnuCSetGreenTag
            // 
            this.mnuCSetGreenTag.Image = global::Katalog.Properties.Resources.circ_green;
            this.mnuCSetGreenTag.Name = "mnuCSetGreenTag";
            resources.ApplyResources(this.mnuCSetGreenTag, "mnuCSetGreenTag");
            this.mnuCSetGreenTag.Tag = "1";
            this.mnuCSetGreenTag.Click += new System.EventHandler(this.mnuCSetTag_Click);
            // 
            // mnuCSetRedTag
            // 
            this.mnuCSetRedTag.Image = global::Katalog.Properties.Resources.circ_red;
            this.mnuCSetRedTag.Name = "mnuCSetRedTag";
            resources.ApplyResources(this.mnuCSetRedTag, "mnuCSetRedTag");
            this.mnuCSetRedTag.Tag = "2";
            this.mnuCSetRedTag.Click += new System.EventHandler(this.mnuCSetTag_Click);
            // 
            // mnuCSetOrangeTag
            // 
            this.mnuCSetOrangeTag.Image = global::Katalog.Properties.Resources.circ_orange;
            this.mnuCSetOrangeTag.Name = "mnuCSetOrangeTag";
            resources.ApplyResources(this.mnuCSetOrangeTag, "mnuCSetOrangeTag");
            this.mnuCSetOrangeTag.Tag = "3";
            this.mnuCSetOrangeTag.Click += new System.EventHandler(this.mnuCSetTag_Click);
            // 
            // mnuCSetYellowTag
            // 
            this.mnuCSetYellowTag.Image = global::Katalog.Properties.Resources.circ_yellow;
            this.mnuCSetYellowTag.Name = "mnuCSetYellowTag";
            resources.ApplyResources(this.mnuCSetYellowTag, "mnuCSetYellowTag");
            this.mnuCSetYellowTag.Tag = "4";
            this.mnuCSetYellowTag.Click += new System.EventHandler(this.mnuCSetTag_Click);
            // 
            // mnuCSetGreyTag
            // 
            this.mnuCSetGreyTag.Image = global::Katalog.Properties.Resources.circ_grey;
            this.mnuCSetGreyTag.Name = "mnuCSetGreyTag";
            resources.ApplyResources(this.mnuCSetGreyTag, "mnuCSetGreyTag");
            this.mnuCSetGreyTag.Tag = "5";
            this.mnuCSetGreyTag.Click += new System.EventHandler(this.mnuCSetTag_Click);
            // 
            // mnuCSetBlueTag
            // 
            this.mnuCSetBlueTag.Image = global::Katalog.Properties.Resources.Circle_Blue;
            this.mnuCSetBlueTag.Name = "mnuCSetBlueTag";
            resources.ApplyResources(this.mnuCSetBlueTag, "mnuCSetBlueTag");
            this.mnuCSetBlueTag.Tag = "6";
            this.mnuCSetBlueTag.Click += new System.EventHandler(this.mnuCSetTag_Click);
            // 
            // mnuCActive
            // 
            this.mnuCActive.CheckOnClick = true;
            this.mnuCActive.Name = "mnuCActive";
            resources.ApplyResources(this.mnuCActive, "mnuCActive");
            this.mnuCActive.Click += new System.EventHandler(this.mnuCActive_Click);
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
            // tabLending
            // 
            this.tabLending.Controls.Add(this.olvLending);
            this.tabLending.Controls.Add(this.cbLendingShow);
            this.tabLending.Controls.Add(this.lblLendingShow);
            this.tabLending.Controls.Add(this.chbShowReturned);
            resources.ApplyResources(this.tabLending, "tabLending");
            this.tabLending.Name = "tabLending";
            this.tabLending.UseVisualStyleBackColor = true;
            // 
            // olvLending
            // 
            this.olvLending.AllColumns.Add(this.ldFastTags);
            this.olvLending.AllColumns.Add(this.ldPerson);
            this.olvLending.AllColumns.Add(this.ldItemType);
            this.olvLending.AllColumns.Add(this.ldItemName);
            this.olvLending.AllColumns.Add(this.ldItemNum);
            this.olvLending.AllColumns.Add(this.ldItemInvNum);
            this.olvLending.AllColumns.Add(this.ldFrom);
            this.olvLending.AllColumns.Add(this.ldTo);
            this.olvLending.AllColumns.Add(this.ldStatus);
            this.olvLending.AllColumns.Add(this.ldNote);
            this.olvLending.AllColumns.Add(this.ldFastTagsNum);
            this.olvLending.CellEditUseWholeCell = false;
            this.olvLending.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ldFastTags,
            this.ldPerson,
            this.ldItemType,
            this.ldItemName,
            this.ldItemNum,
            this.ldItemInvNum,
            this.ldFrom,
            this.ldTo,
            this.ldStatus,
            this.ldNote,
            this.ldFastTagsNum});
            this.olvLending.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.olvLending, "olvLending");
            this.olvLending.FullRowSelect = true;
            this.olvLending.GridLines = true;
            this.olvLending.Name = "olvLending";
            this.olvLending.ShowGroups = false;
            this.olvLending.SmallImageList = this.imgOLV;
            this.olvLending.UseCompatibleStateImageBehavior = false;
            this.olvLending.View = System.Windows.Forms.View.Details;
            this.olvLending.VirtualMode = true;
            this.olvLending.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvLending_FormatRow);
            this.olvLending.SelectedIndexChanged += new System.EventHandler(this.olvLending_SelectedIndexChanged);
            this.olvLending.DoubleClick += new System.EventHandler(this.olvLending_DoubleClick);
            // 
            // ldFastTags
            // 
            resources.ApplyResources(this.ldFastTags, "ldFastTags");
            // 
            // ldPerson
            // 
            resources.ApplyResources(this.ldPerson, "ldPerson");
            // 
            // ldItemType
            // 
            this.ldItemType.AspectName = "";
            resources.ApplyResources(this.ldItemType, "ldItemType");
            // 
            // ldItemName
            // 
            this.ldItemName.AspectName = "";
            resources.ApplyResources(this.ldItemName, "ldItemName");
            // 
            // ldItemNum
            // 
            resources.ApplyResources(this.ldItemNum, "ldItemNum");
            // 
            // ldItemInvNum
            // 
            resources.ApplyResources(this.ldItemInvNum, "ldItemInvNum");
            // 
            // ldFrom
            // 
            this.ldFrom.AspectName = "";
            resources.ApplyResources(this.ldFrom, "ldFrom");
            // 
            // ldTo
            // 
            this.ldTo.AspectName = "";
            resources.ApplyResources(this.ldTo, "ldTo");
            // 
            // ldStatus
            // 
            resources.ApplyResources(this.ldStatus, "ldStatus");
            // 
            // ldNote
            // 
            resources.ApplyResources(this.ldNote, "ldNote");
            // 
            // ldFastTagsNum
            // 
            resources.ApplyResources(this.ldFastTagsNum, "ldFastTagsNum");
            // 
            // cbLendingShow
            // 
            this.cbLendingShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLendingShow.FormattingEnabled = true;
            resources.ApplyResources(this.cbLendingShow, "cbLendingShow");
            this.cbLendingShow.Name = "cbLendingShow";
            this.cbLendingShow.SelectedIndexChanged += new System.EventHandler(this.cbLendingShow_SelectedIndexChanged);
            // 
            // lblLendingShow
            // 
            resources.ApplyResources(this.lblLendingShow, "lblLendingShow");
            this.lblLendingShow.Name = "lblLendingShow";
            // 
            // chbShowReturned
            // 
            resources.ApplyResources(this.chbShowReturned, "chbShowReturned");
            this.chbShowReturned.Name = "chbShowReturned";
            this.chbShowReturned.UseVisualStyleBackColor = true;
            this.chbShowReturned.CheckedChanged += new System.EventHandler(this.chbShowReturned_CheckedChanged);
            // 
            // tabBorrowing
            // 
            this.tabBorrowing.Controls.Add(this.btnClearOldReservations);
            this.tabBorrowing.Controls.Add(this.btnBorrowings);
            this.tabBorrowing.Controls.Add(this.cbBorrowingShow);
            this.tabBorrowing.Controls.Add(this.lblShowBorrowing);
            this.tabBorrowing.Controls.Add(this.chbBorrowingReturned);
            this.tabBorrowing.Controls.Add(this.olvBorrowing);
            resources.ApplyResources(this.tabBorrowing, "tabBorrowing");
            this.tabBorrowing.Name = "tabBorrowing";
            this.tabBorrowing.UseVisualStyleBackColor = true;
            // 
            // btnClearOldReservations
            // 
            resources.ApplyResources(this.btnClearOldReservations, "btnClearOldReservations");
            this.btnClearOldReservations.Name = "btnClearOldReservations";
            this.btnClearOldReservations.UseVisualStyleBackColor = true;
            // 
            // btnBorrowings
            // 
            resources.ApplyResources(this.btnBorrowings, "btnBorrowings");
            this.btnBorrowings.Name = "btnBorrowings";
            this.btnBorrowings.UseVisualStyleBackColor = true;
            // 
            // cbBorrowingShow
            // 
            this.cbBorrowingShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBorrowingShow.FormattingEnabled = true;
            resources.ApplyResources(this.cbBorrowingShow, "cbBorrowingShow");
            this.cbBorrowingShow.Name = "cbBorrowingShow";
            this.cbBorrowingShow.SelectedIndexChanged += new System.EventHandler(this.cbBorrowingShow_SelectedIndexChanged);
            // 
            // lblShowBorrowing
            // 
            resources.ApplyResources(this.lblShowBorrowing, "lblShowBorrowing");
            this.lblShowBorrowing.Name = "lblShowBorrowing";
            // 
            // chbBorrowingReturned
            // 
            resources.ApplyResources(this.chbBorrowingReturned, "chbBorrowingReturned");
            this.chbBorrowingReturned.Name = "chbBorrowingReturned";
            this.chbBorrowingReturned.UseVisualStyleBackColor = true;
            this.chbBorrowingReturned.CheckedChanged += new System.EventHandler(this.chbBorrowingReturned_CheckedChanged);
            // 
            // olvBorrowing
            // 
            this.olvBorrowing.AllColumns.Add(this.brFastTags);
            this.olvBorrowing.AllColumns.Add(this.brPerson);
            this.olvBorrowing.AllColumns.Add(this.brItemName);
            this.olvBorrowing.AllColumns.Add(this.brItemInvNum);
            this.olvBorrowing.AllColumns.Add(this.brFrom);
            this.olvBorrowing.AllColumns.Add(this.brTo);
            this.olvBorrowing.AllColumns.Add(this.brStatus);
            this.olvBorrowing.AllColumns.Add(this.brNote);
            this.olvBorrowing.AllColumns.Add(this.brFastTagsNum);
            this.olvBorrowing.CellEditUseWholeCell = false;
            this.olvBorrowing.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.brFastTags,
            this.brPerson,
            this.brItemName,
            this.brItemInvNum,
            this.brFrom,
            this.brTo,
            this.brStatus,
            this.brNote,
            this.brFastTagsNum});
            this.olvBorrowing.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.olvBorrowing, "olvBorrowing");
            this.olvBorrowing.FullRowSelect = true;
            this.olvBorrowing.GridLines = true;
            this.olvBorrowing.Name = "olvBorrowing";
            this.olvBorrowing.ShowGroups = false;
            this.olvBorrowing.SmallImageList = this.imgOLV;
            this.olvBorrowing.UseCompatibleStateImageBehavior = false;
            this.olvBorrowing.View = System.Windows.Forms.View.Details;
            this.olvBorrowing.VirtualMode = true;
            this.olvBorrowing.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvBorrowing_FormatRow);
            this.olvBorrowing.SelectedIndexChanged += new System.EventHandler(this.olvBorrowing_SelectedIndexChanged);
            this.olvBorrowing.DoubleClick += new System.EventHandler(this.olvBorrowing_DoubleClick);
            // 
            // brFastTags
            // 
            resources.ApplyResources(this.brFastTags, "brFastTags");
            // 
            // brPerson
            // 
            resources.ApplyResources(this.brPerson, "brPerson");
            // 
            // brItemName
            // 
            this.brItemName.AspectName = "";
            resources.ApplyResources(this.brItemName, "brItemName");
            // 
            // brItemInvNum
            // 
            resources.ApplyResources(this.brItemInvNum, "brItemInvNum");
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
            // brStatus
            // 
            resources.ApplyResources(this.brStatus, "brStatus");
            // 
            // brNote
            // 
            resources.ApplyResources(this.brNote, "brNote");
            // 
            // brFastTagsNum
            // 
            resources.ApplyResources(this.brFastTagsNum, "brFastTagsNum");
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
            this.olvItem.AllColumns.Add(this.itAvailable);
            this.olvItem.AllColumns.Add(this.itExcluded);
            this.olvItem.AllColumns.Add(this.itFastTagsNum);
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
            this.itAvailable,
            this.itExcluded,
            this.itFastTagsNum});
            this.olvItem.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.olvItem, "olvItem");
            this.olvItem.FullRowSelect = true;
            this.olvItem.GridLines = true;
            this.olvItem.Name = "olvItem";
            this.olvItem.ShowGroups = false;
            this.olvItem.SmallImageList = this.imgOLV;
            this.olvItem.UseCompatibleStateImageBehavior = false;
            this.olvItem.View = System.Windows.Forms.View.Details;
            this.olvItem.VirtualMode = true;
            this.olvItem.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvItem_FormatRow);
            this.olvItem.SelectedIndexChanged += new System.EventHandler(this.olvItem_SelectedIndexChanged);
            this.olvItem.DoubleClick += new System.EventHandler(this.btnEditItem_Click);
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
            // itAvailable
            // 
            resources.ApplyResources(this.itAvailable, "itAvailable");
            // 
            // itExcluded
            // 
            resources.ApplyResources(this.itExcluded, "itExcluded");
            // 
            // itFastTagsNum
            // 
            resources.ApplyResources(this.itFastTagsNum, "itFastTagsNum");
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
            this.olvBooks.AllColumns.Add(this.bkCount);
            this.olvBooks.AllColumns.Add(this.bkAvailable);
            this.olvBooks.AllColumns.Add(this.bkType);
            this.olvBooks.AllColumns.Add(this.bkYear);
            this.olvBooks.AllColumns.Add(this.bkGenre);
            this.olvBooks.AllColumns.Add(this.bkSubgenre);
            this.olvBooks.AllColumns.Add(this.bkInvNum);
            this.olvBooks.AllColumns.Add(this.bkLocation);
            this.olvBooks.AllColumns.Add(this.bkKeywords);
            this.olvBooks.AllColumns.Add(this.bkSeries);
            this.olvBooks.AllColumns.Add(this.bkFastTagsNum);
            this.olvBooks.CellEditUseWholeCell = false;
            this.olvBooks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.bkFastTags,
            this.bkName,
            this.bkAuthor,
            this.bkCount,
            this.bkAvailable,
            this.bkType,
            this.bkYear,
            this.bkGenre,
            this.bkSubgenre,
            this.bkInvNum,
            this.bkLocation,
            this.bkKeywords,
            this.bkSeries,
            this.bkFastTagsNum});
            this.olvBooks.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.olvBooks, "olvBooks");
            this.olvBooks.FullRowSelect = true;
            this.olvBooks.GridLines = true;
            this.olvBooks.Name = "olvBooks";
            this.olvBooks.ShowGroups = false;
            this.olvBooks.SmallImageList = this.imgOLV;
            this.olvBooks.UseCompatibleStateImageBehavior = false;
            this.olvBooks.View = System.Windows.Forms.View.Details;
            this.olvBooks.VirtualMode = true;
            this.olvBooks.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvBooks_FormatRow);
            this.olvBooks.SelectedIndexChanged += new System.EventHandler(this.olvBooks_SelectedIndexChanged);
            this.olvBooks.DoubleClick += new System.EventHandler(this.btnEditItem_Click);
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
            // bkCount
            // 
            resources.ApplyResources(this.bkCount, "bkCount");
            // 
            // bkAvailable
            // 
            resources.ApplyResources(this.bkAvailable, "bkAvailable");
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
            // bkFastTagsNum
            // 
            resources.ApplyResources(this.bkFastTagsNum, "bkFastTagsNum");
            // 
            // tabBoardGames
            // 
            this.tabBoardGames.Controls.Add(this.chbShowExcludedBoard);
            this.tabBoardGames.Controls.Add(this.olvBoard);
            resources.ApplyResources(this.tabBoardGames, "tabBoardGames");
            this.tabBoardGames.Name = "tabBoardGames";
            this.tabBoardGames.UseVisualStyleBackColor = true;
            // 
            // chbShowExcludedBoard
            // 
            resources.ApplyResources(this.chbShowExcludedBoard, "chbShowExcludedBoard");
            this.chbShowExcludedBoard.Name = "chbShowExcludedBoard";
            this.chbShowExcludedBoard.UseVisualStyleBackColor = true;
            this.chbShowExcludedBoard.CheckedChanged += new System.EventHandler(this.chbShowExcludedBoard_CheckedChanged);
            // 
            // olvBoard
            // 
            this.olvBoard.AllColumns.Add(this.bgTags);
            this.olvBoard.AllColumns.Add(this.bgName);
            this.olvBoard.AllColumns.Add(this.bgCategory);
            this.olvBoard.AllColumns.Add(this.bgInvNum);
            this.olvBoard.AllColumns.Add(this.bgLocation);
            this.olvBoard.AllColumns.Add(this.bgKeywords);
            this.olvBoard.AllColumns.Add(this.bgCounts);
            this.olvBoard.AllColumns.Add(this.bgAvailable);
            this.olvBoard.AllColumns.Add(this.bgExcluded);
            this.olvBoard.AllColumns.Add(this.bgTagsNum);
            this.olvBoard.CellEditUseWholeCell = false;
            this.olvBoard.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.bgTags,
            this.bgName,
            this.bgCategory,
            this.bgInvNum,
            this.bgLocation,
            this.bgKeywords,
            this.bgCounts,
            this.bgAvailable,
            this.bgExcluded,
            this.bgTagsNum});
            this.olvBoard.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.olvBoard, "olvBoard");
            this.olvBoard.FullRowSelect = true;
            this.olvBoard.GridLines = true;
            this.olvBoard.Name = "olvBoard";
            this.olvBoard.ShowGroups = false;
            this.olvBoard.SmallImageList = this.imgOLV;
            this.olvBoard.UseCompatibleStateImageBehavior = false;
            this.olvBoard.View = System.Windows.Forms.View.Details;
            this.olvBoard.VirtualMode = true;
            this.olvBoard.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvBoard_FormatRow);
            this.olvBoard.SelectedIndexChanged += new System.EventHandler(this.olvBoard_SelectedIndexChanged);
            this.olvBoard.DoubleClick += new System.EventHandler(this.btnEditItem_Click);
            // 
            // bgTags
            // 
            resources.ApplyResources(this.bgTags, "bgTags");
            // 
            // bgName
            // 
            this.bgName.AspectName = "";
            resources.ApplyResources(this.bgName, "bgName");
            // 
            // bgCategory
            // 
            this.bgCategory.AspectName = "";
            resources.ApplyResources(this.bgCategory, "bgCategory");
            // 
            // bgInvNum
            // 
            resources.ApplyResources(this.bgInvNum, "bgInvNum");
            // 
            // bgLocation
            // 
            resources.ApplyResources(this.bgLocation, "bgLocation");
            // 
            // bgKeywords
            // 
            this.bgKeywords.AspectName = "";
            resources.ApplyResources(this.bgKeywords, "bgKeywords");
            // 
            // bgCounts
            // 
            this.bgCounts.AspectName = "";
            resources.ApplyResources(this.bgCounts, "bgCounts");
            // 
            // bgAvailable
            // 
            resources.ApplyResources(this.bgAvailable, "bgAvailable");
            // 
            // bgExcluded
            // 
            resources.ApplyResources(this.bgExcluded, "bgExcluded");
            // 
            // bgTagsNum
            // 
            resources.ApplyResources(this.bgTagsNum, "bgTagsNum");
            // 
            // tabGames
            // 
            this.tabGames.Controls.Add(this.chbShowExcludedGames);
            this.tabGames.Controls.Add(this.olvGames);
            resources.ApplyResources(this.tabGames, "tabGames");
            this.tabGames.Name = "tabGames";
            this.tabGames.UseVisualStyleBackColor = true;
            // 
            // chbShowExcludedGames
            // 
            resources.ApplyResources(this.chbShowExcludedGames, "chbShowExcludedGames");
            this.chbShowExcludedGames.Name = "chbShowExcludedGames";
            this.chbShowExcludedGames.UseVisualStyleBackColor = true;
            // 
            // olvGames
            // 
            this.olvGames.AllColumns.Add(this.gmTags);
            this.olvGames.AllColumns.Add(this.gmName);
            this.olvGames.AllColumns.Add(this.gmCategory);
            this.olvGames.AllColumns.Add(this.gmEnviroment);
            this.olvGames.AllColumns.Add(this.gmMinPlayers);
            this.olvGames.AllColumns.Add(this.gmMaxPlayers);
            this.olvGames.AllColumns.Add(this.gmPlayerAge);
            this.olvGames.AllColumns.Add(this.gmDuration);
            this.olvGames.AllColumns.Add(this.gmKeywords);
            this.olvGames.AllColumns.Add(this.gmThings);
            this.olvGames.AllColumns.Add(this.gmExcluded);
            this.olvGames.AllColumns.Add(this.gmTagsNum);
            this.olvGames.CellEditUseWholeCell = false;
            this.olvGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.gmTags,
            this.gmName,
            this.gmCategory,
            this.gmEnviroment,
            this.gmMinPlayers,
            this.gmMaxPlayers,
            this.gmPlayerAge,
            this.gmDuration,
            this.gmKeywords,
            this.gmThings,
            this.gmExcluded,
            this.gmTagsNum});
            this.olvGames.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.olvGames, "olvGames");
            this.olvGames.FullRowSelect = true;
            this.olvGames.GridLines = true;
            this.olvGames.Name = "olvGames";
            this.olvGames.ShowGroups = false;
            this.olvGames.SmallImageList = this.imgOLV;
            this.olvGames.UseCompatibleStateImageBehavior = false;
            this.olvGames.View = System.Windows.Forms.View.Details;
            this.olvGames.VirtualMode = true;
            this.olvGames.SelectedIndexChanged += new System.EventHandler(this.olvGames_SelectedIndexChanged);
            this.olvGames.DoubleClick += new System.EventHandler(this.btnEditItem_Click);
            // 
            // gmTags
            // 
            resources.ApplyResources(this.gmTags, "gmTags");
            // 
            // gmName
            // 
            this.gmName.AspectName = "";
            resources.ApplyResources(this.gmName, "gmName");
            // 
            // gmCategory
            // 
            this.gmCategory.AspectName = "";
            resources.ApplyResources(this.gmCategory, "gmCategory");
            // 
            // gmEnviroment
            // 
            this.gmEnviroment.AspectName = "";
            resources.ApplyResources(this.gmEnviroment, "gmEnviroment");
            // 
            // gmMinPlayers
            // 
            resources.ApplyResources(this.gmMinPlayers, "gmMinPlayers");
            // 
            // gmMaxPlayers
            // 
            resources.ApplyResources(this.gmMaxPlayers, "gmMaxPlayers");
            // 
            // gmPlayerAge
            // 
            resources.ApplyResources(this.gmPlayerAge, "gmPlayerAge");
            // 
            // gmDuration
            // 
            resources.ApplyResources(this.gmDuration, "gmDuration");
            // 
            // gmKeywords
            // 
            this.gmKeywords.AspectName = "";
            resources.ApplyResources(this.gmKeywords, "gmKeywords");
            // 
            // gmThings
            // 
            resources.ApplyResources(this.gmThings, "gmThings");
            // 
            // gmExcluded
            // 
            resources.ApplyResources(this.gmExcluded, "gmExcluded");
            // 
            // gmTagsNum
            // 
            resources.ApplyResources(this.gmTagsNum, "gmTagsNum");
            // 
            // tabRecipes
            // 
            this.tabRecipes.Controls.Add(this.chbShowExcludedRecipes);
            this.tabRecipes.Controls.Add(this.olvRecipes);
            resources.ApplyResources(this.tabRecipes, "tabRecipes");
            this.tabRecipes.Name = "tabRecipes";
            this.tabRecipes.UseVisualStyleBackColor = true;
            // 
            // chbShowExcludedRecipes
            // 
            resources.ApplyResources(this.chbShowExcludedRecipes, "chbShowExcludedRecipes");
            this.chbShowExcludedRecipes.Name = "chbShowExcludedRecipes";
            this.chbShowExcludedRecipes.UseVisualStyleBackColor = true;
            this.chbShowExcludedRecipes.CheckedChanged += new System.EventHandler(this.chbShowExcludedRecipes_CheckedChanged);
            // 
            // olvRecipes
            // 
            this.olvRecipes.AllColumns.Add(this.recTags);
            this.olvRecipes.AllColumns.Add(this.recName);
            this.olvRecipes.AllColumns.Add(this.recCategory);
            this.olvRecipes.AllColumns.Add(this.recKeywords);
            this.olvRecipes.AllColumns.Add(this.recExcluded);
            this.olvRecipes.AllColumns.Add(this.recTagsNum);
            this.olvRecipes.CellEditUseWholeCell = false;
            this.olvRecipes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.recTags,
            this.recName,
            this.recCategory,
            this.recKeywords,
            this.recExcluded,
            this.recTagsNum});
            this.olvRecipes.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.olvRecipes, "olvRecipes");
            this.olvRecipes.FullRowSelect = true;
            this.olvRecipes.GridLines = true;
            this.olvRecipes.Name = "olvRecipes";
            this.olvRecipes.ShowGroups = false;
            this.olvRecipes.SmallImageList = this.imgOLV;
            this.olvRecipes.UseCompatibleStateImageBehavior = false;
            this.olvRecipes.View = System.Windows.Forms.View.Details;
            this.olvRecipes.VirtualMode = true;
            this.olvRecipes.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvRecipes_FormatRow);
            this.olvRecipes.SelectedIndexChanged += new System.EventHandler(this.olvRecipes_SelectedIndexChanged);
            this.olvRecipes.DoubleClick += new System.EventHandler(this.btnEditItem_Click);
            // 
            // recTags
            // 
            resources.ApplyResources(this.recTags, "recTags");
            // 
            // recName
            // 
            this.recName.AspectName = "";
            resources.ApplyResources(this.recName, "recName");
            // 
            // recCategory
            // 
            this.recCategory.AspectName = "";
            resources.ApplyResources(this.recCategory, "recCategory");
            // 
            // recKeywords
            // 
            this.recKeywords.AspectName = "";
            resources.ApplyResources(this.recKeywords, "recKeywords");
            // 
            // recExcluded
            // 
            resources.ApplyResources(this.recExcluded, "recExcluded");
            // 
            // recTagsNum
            // 
            resources.ApplyResources(this.recTagsNum, "recTagsNum");
            // 
            // tabObject
            // 
            this.tabObject.Controls.Add(this.cbObjectShow);
            this.tabObject.Controls.Add(this.lblObjectShow);
            this.tabObject.Controls.Add(this.chbShowExcludedObjects);
            this.tabObject.Controls.Add(this.olvObjects);
            resources.ApplyResources(this.tabObject, "tabObject");
            this.tabObject.Name = "tabObject";
            this.tabObject.UseVisualStyleBackColor = true;
            // 
            // cbObjectShow
            // 
            this.cbObjectShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObjectShow.FormattingEnabled = true;
            resources.ApplyResources(this.cbObjectShow, "cbObjectShow");
            this.cbObjectShow.Name = "cbObjectShow";
            this.cbObjectShow.SelectedIndexChanged += new System.EventHandler(this.cbObjectShow_SelectedIndexChanged);
            // 
            // lblObjectShow
            // 
            resources.ApplyResources(this.lblObjectShow, "lblObjectShow");
            this.lblObjectShow.Name = "lblObjectShow";
            // 
            // chbShowExcludedObjects
            // 
            resources.ApplyResources(this.chbShowExcludedObjects, "chbShowExcludedObjects");
            this.chbShowExcludedObjects.Name = "chbShowExcludedObjects";
            this.chbShowExcludedObjects.UseVisualStyleBackColor = true;
            this.chbShowExcludedObjects.CheckedChanged += new System.EventHandler(this.chbShowExcludedObjects_CheckedChanged);
            // 
            // olvObjects
            // 
            this.olvObjects.AllColumns.Add(this.objTags);
            this.olvObjects.AllColumns.Add(this.objName);
            this.olvObjects.AllColumns.Add(this.objType);
            this.olvObjects.AllColumns.Add(this.objCategory);
            this.olvObjects.AllColumns.Add(this.objNumber);
            this.olvObjects.AllColumns.Add(this.objParent);
            this.olvObjects.AllColumns.Add(this.objKeywords);
            this.olvObjects.AllColumns.Add(this.objCustomer);
            this.olvObjects.AllColumns.Add(this.objDevelopment);
            this.olvObjects.AllColumns.Add(this.objFolder);
            this.olvObjects.AllColumns.Add(this.objVersion);
            this.olvObjects.AllColumns.Add(this.objActive);
            this.olvObjects.AllColumns.Add(this.objTagsNum);
            this.olvObjects.CellEditUseWholeCell = false;
            this.olvObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.objTags,
            this.objName,
            this.objType,
            this.objCategory,
            this.objNumber,
            this.objParent,
            this.objKeywords,
            this.objCustomer,
            this.objDevelopment,
            this.objFolder,
            this.objVersion,
            this.objActive,
            this.objTagsNum});
            this.olvObjects.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.olvObjects, "olvObjects");
            this.olvObjects.FullRowSelect = true;
            this.olvObjects.GridLines = true;
            this.olvObjects.Name = "olvObjects";
            this.olvObjects.ShowGroups = false;
            this.olvObjects.SmallImageList = this.imgOLV;
            this.olvObjects.UseCompatibleStateImageBehavior = false;
            this.olvObjects.View = System.Windows.Forms.View.Details;
            this.olvObjects.VirtualMode = true;
            this.olvObjects.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.olvObjects_FormatRow);
            this.olvObjects.SelectedIndexChanged += new System.EventHandler(this.olvObjects_SelectedIndexChanged);
            this.olvObjects.DoubleClick += new System.EventHandler(this.btnEditItem_Click);
            // 
            // objTags
            // 
            resources.ApplyResources(this.objTags, "objTags");
            // 
            // objName
            // 
            this.objName.AspectName = "";
            resources.ApplyResources(this.objName, "objName");
            // 
            // objType
            // 
            resources.ApplyResources(this.objType, "objType");
            // 
            // objCategory
            // 
            this.objCategory.AspectName = "";
            resources.ApplyResources(this.objCategory, "objCategory");
            // 
            // objNumber
            // 
            resources.ApplyResources(this.objNumber, "objNumber");
            // 
            // objParent
            // 
            resources.ApplyResources(this.objParent, "objParent");
            // 
            // objKeywords
            // 
            this.objKeywords.AspectName = "";
            resources.ApplyResources(this.objKeywords, "objKeywords");
            // 
            // objCustomer
            // 
            resources.ApplyResources(this.objCustomer, "objCustomer");
            // 
            // objDevelopment
            // 
            resources.ApplyResources(this.objDevelopment, "objDevelopment");
            // 
            // objFolder
            // 
            this.objFolder.AspectName = "";
            resources.ApplyResources(this.objFolder, "objFolder");
            // 
            // objVersion
            // 
            resources.ApplyResources(this.objVersion, "objVersion");
            // 
            // objActive
            // 
            resources.ApplyResources(this.objActive, "objActive");
            // 
            // objTagsNum
            // 
            resources.ApplyResources(this.objTagsNum, "objTagsNum");
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
            // toolEditItem
            // 
            resources.ApplyResources(this.toolEditItem, "toolEditItem");
            this.toolEditItem.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolEditItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewItem,
            this.btnEditItem,
            this.btnDeleteItem});
            this.toolEditItem.Name = "toolEditItem";
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
            // toolFastFilter
            // 
            resources.ApplyResources(this.toolFastFilter, "toolFastFilter");
            this.toolFastFilter.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolFastFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.toolFastFilter.Name = "toolFastFilter";
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
            // toolFilter
            // 
            resources.ApplyResources(this.toolFilter, "toolFilter");
            this.toolFilter.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFilter,
            this.txtFilter,
            this.cbFilterCol,
            this.btnFilterPin1,
            this.btnFilterPin2,
            this.btnFilterPin3,
            this.btnFilterPin4,
            this.btnFilterPin5,
            this.btnFilterPin6});
            this.toolFilter.Name = "toolFilter";
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
            this.btnFilterPin1.Click += new System.EventHandler(this.btnFilterPin1_Click);
            // 
            // btnFilterPin2
            // 
            this.btnFilterPin2.CheckOnClick = true;
            this.btnFilterPin2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterPin2.Image = global::Katalog.Properties.Resources.circ_red;
            resources.ApplyResources(this.btnFilterPin2, "btnFilterPin2");
            this.btnFilterPin2.Name = "btnFilterPin2";
            this.btnFilterPin2.Click += new System.EventHandler(this.btnFilterPin1_Click);
            // 
            // btnFilterPin3
            // 
            this.btnFilterPin3.CheckOnClick = true;
            this.btnFilterPin3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterPin3.Image = global::Katalog.Properties.Resources.circ_orange;
            resources.ApplyResources(this.btnFilterPin3, "btnFilterPin3");
            this.btnFilterPin3.Name = "btnFilterPin3";
            this.btnFilterPin3.Click += new System.EventHandler(this.btnFilterPin1_Click);
            // 
            // btnFilterPin4
            // 
            this.btnFilterPin4.CheckOnClick = true;
            this.btnFilterPin4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterPin4.Image = global::Katalog.Properties.Resources.circ_yellow;
            resources.ApplyResources(this.btnFilterPin4, "btnFilterPin4");
            this.btnFilterPin4.Name = "btnFilterPin4";
            this.btnFilterPin4.Click += new System.EventHandler(this.btnFilterPin1_Click);
            // 
            // btnFilterPin5
            // 
            this.btnFilterPin5.CheckOnClick = true;
            this.btnFilterPin5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterPin5.Image = global::Katalog.Properties.Resources.circ_grey;
            resources.ApplyResources(this.btnFilterPin5, "btnFilterPin5");
            this.btnFilterPin5.Name = "btnFilterPin5";
            this.btnFilterPin5.Click += new System.EventHandler(this.btnFilterPin1_Click);
            // 
            // btnFilterPin6
            // 
            this.btnFilterPin6.CheckOnClick = true;
            this.btnFilterPin6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFilterPin6.Image = global::Katalog.Properties.Resources.Circle_Blue;
            resources.ApplyResources(this.btnFilterPin6, "btnFilterPin6");
            this.btnFilterPin6.Name = "btnFilterPin6";
            this.btnFilterPin6.Click += new System.EventHandler(this.btnFilterPin1_Click);
            // 
            // brType
            // 
            this.brType.AspectName = "";
            resources.ApplyResources(this.brType, "brType");
            // 
            // brItemNum
            // 
            resources.ApplyResources(this.brItemNum, "brItemNum");
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
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
            this.mnuImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuImportFromFile,
            this.toolStripMenuItem8,
            this.mnuImportGoogleContacts});
            this.mnuImport.Name = "mnuImport";
            resources.ApplyResources(this.mnuImport, "mnuImport");
            // 
            // mnuImportFromFile
            // 
            this.mnuImportFromFile.Name = "mnuImportFromFile";
            resources.ApplyResources(this.mnuImportFromFile, "mnuImportFromFile");
            this.mnuImportFromFile.Click += new System.EventHandler(this.mnuImport_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
            // 
            // mnuImportGoogleContacts
            // 
            this.mnuImportGoogleContacts.Name = "mnuImportGoogleContacts";
            resources.ApplyResources(this.mnuImportGoogleContacts, "mnuImportGoogleContacts");
            this.mnuImportGoogleContacts.Click += new System.EventHandler(this.mnuImportGoogleContacts_Click);
            // 
            // mnuExport
            // 
            this.mnuExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExportToFile});
            this.mnuExport.Name = "mnuExport";
            resources.ApplyResources(this.mnuExport, "mnuExport");
            // 
            // mnuExportToFile
            // 
            this.mnuExportToFile.Name = "mnuExportToFile";
            resources.ApplyResources(this.mnuExportToFile, "mnuExportToFile");
            this.mnuExportToFile.Click += new System.EventHandler(this.mnuExport_Click);
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
            this.mnuShowLending,
            this.mnuShowBorrowing,
            this.toolStripMenuItem3,
            this.mnuShowItems,
            this.mnuShowBooks,
            this.mnuShowBoardGames,
            this.toolStripMenuItem9,
            this.mnuShowGames,
            this.mnuShowRecipes,
            this.toolStripMenuItem10,
            this.mnuShowObjects});
            this.mnuShow.Name = "mnuShow";
            resources.ApplyResources(this.mnuShow, "mnuShow");
            // 
            // mnuToolBars
            // 
            this.mnuToolBars.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowFilter,
            this.mnuShowFastFilter});
            this.mnuToolBars.Name = "mnuToolBars";
            resources.ApplyResources(this.mnuToolBars, "mnuToolBars");
            // 
            // mnuShowFilter
            // 
            this.mnuShowFilter.Checked = true;
            this.mnuShowFilter.CheckOnClick = true;
            this.mnuShowFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowFilter.Name = "mnuShowFilter";
            resources.ApplyResources(this.mnuShowFilter, "mnuShowFilter");
            this.mnuShowFilter.Tag = "Filter";
            this.mnuShowFilter.Click += new System.EventHandler(this.mnuShowToolbars_Click);
            // 
            // mnuShowFastFilter
            // 
            this.mnuShowFastFilter.Checked = true;
            this.mnuShowFastFilter.CheckOnClick = true;
            this.mnuShowFastFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowFastFilter.Name = "mnuShowFastFilter";
            resources.ApplyResources(this.mnuShowFastFilter, "mnuShowFastFilter");
            this.mnuShowFastFilter.Tag = "FastFilter";
            this.mnuShowFastFilter.Click += new System.EventHandler(this.mnuShowToolbars_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // mnuShowContacts
            // 
            this.mnuShowContacts.Checked = true;
            this.mnuShowContacts.CheckOnClick = true;
            this.mnuShowContacts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowContacts.Name = "mnuShowContacts";
            resources.ApplyResources(this.mnuShowContacts, "mnuShowContacts");
            this.mnuShowContacts.Tag = "Contacts";
            this.mnuShowContacts.CheckedChanged += new System.EventHandler(this.mnuShowTabs_Click);
            // 
            // mnuShowLending
            // 
            this.mnuShowLending.Checked = true;
            this.mnuShowLending.CheckOnClick = true;
            this.mnuShowLending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowLending.Name = "mnuShowLending";
            resources.ApplyResources(this.mnuShowLending, "mnuShowLending");
            this.mnuShowLending.Tag = "Lending";
            this.mnuShowLending.CheckedChanged += new System.EventHandler(this.mnuShowTabs_Click);
            // 
            // mnuShowBorrowing
            // 
            this.mnuShowBorrowing.Checked = true;
            this.mnuShowBorrowing.CheckOnClick = true;
            this.mnuShowBorrowing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowBorrowing.Name = "mnuShowBorrowing";
            resources.ApplyResources(this.mnuShowBorrowing, "mnuShowBorrowing");
            this.mnuShowBorrowing.Tag = "Borrowing";
            this.mnuShowBorrowing.CheckedChanged += new System.EventHandler(this.mnuShowTabs_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // mnuShowItems
            // 
            this.mnuShowItems.Checked = true;
            this.mnuShowItems.CheckOnClick = true;
            this.mnuShowItems.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowItems.Name = "mnuShowItems";
            resources.ApplyResources(this.mnuShowItems, "mnuShowItems");
            this.mnuShowItems.Tag = "Items";
            this.mnuShowItems.CheckedChanged += new System.EventHandler(this.mnuShowTabs_Click);
            // 
            // mnuShowBooks
            // 
            this.mnuShowBooks.Checked = true;
            this.mnuShowBooks.CheckOnClick = true;
            this.mnuShowBooks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowBooks.Name = "mnuShowBooks";
            resources.ApplyResources(this.mnuShowBooks, "mnuShowBooks");
            this.mnuShowBooks.Tag = "Books";
            this.mnuShowBooks.CheckedChanged += new System.EventHandler(this.mnuShowTabs_Click);
            // 
            // mnuShowBoardGames
            // 
            this.mnuShowBoardGames.Checked = true;
            this.mnuShowBoardGames.CheckOnClick = true;
            this.mnuShowBoardGames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowBoardGames.Name = "mnuShowBoardGames";
            resources.ApplyResources(this.mnuShowBoardGames, "mnuShowBoardGames");
            this.mnuShowBoardGames.Tag = "Boardgames";
            this.mnuShowBoardGames.CheckedChanged += new System.EventHandler(this.mnuShowTabs_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            resources.ApplyResources(this.toolStripMenuItem9, "toolStripMenuItem9");
            // 
            // mnuShowGames
            // 
            this.mnuShowGames.Checked = true;
            this.mnuShowGames.CheckOnClick = true;
            this.mnuShowGames.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowGames.Name = "mnuShowGames";
            resources.ApplyResources(this.mnuShowGames, "mnuShowGames");
            this.mnuShowGames.Tag = "Games";
            this.mnuShowGames.CheckedChanged += new System.EventHandler(this.mnuShowTabs_Click);
            // 
            // mnuShowRecipes
            // 
            this.mnuShowRecipes.Checked = true;
            this.mnuShowRecipes.CheckOnClick = true;
            this.mnuShowRecipes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowRecipes.Name = "mnuShowRecipes";
            resources.ApplyResources(this.mnuShowRecipes, "mnuShowRecipes");
            this.mnuShowRecipes.Tag = "Recipes";
            this.mnuShowRecipes.CheckedChanged += new System.EventHandler(this.mnuShowTabs_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            resources.ApplyResources(this.toolStripMenuItem10, "toolStripMenuItem10");
            // 
            // mnuShowObjects
            // 
            this.mnuShowObjects.Checked = true;
            this.mnuShowObjects.CheckOnClick = true;
            this.mnuShowObjects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuShowObjects.Name = "mnuShowObjects";
            resources.ApplyResources(this.mnuShowObjects, "mnuShowObjects");
            this.mnuShowObjects.Tag = "Objects";
            this.mnuShowObjects.CheckedChanged += new System.EventHandler(this.mnuShowTabs_Click);
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
            this.mnuOptions,
            this.mnuSubTools});
            this.mnuTools.Name = "mnuTools";
            resources.ApplyResources(this.mnuTools, "mnuTools");
            // 
            // mnuOptions
            // 
            this.mnuOptions.Image = global::Katalog.Properties.Resources.settings;
            this.mnuOptions.Name = "mnuOptions";
            resources.ApplyResources(this.mnuOptions, "mnuOptions");
            this.mnuOptions.Click += new System.EventHandler(this.mnuOptions_Click);
            // 
            // mnuSubTools
            // 
            this.mnuSubTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRecalculateAvailableItems});
            this.mnuSubTools.Name = "mnuSubTools";
            resources.ApplyResources(this.mnuSubTools, "mnuSubTools");
            // 
            // mnuRecalculateAvailableItems
            // 
            this.mnuRecalculateAvailableItems.Name = "mnuRecalculateAvailableItems";
            resources.ApplyResources(this.mnuRecalculateAvailableItems, "mnuRecalculateAvailableItems");
            this.mnuRecalculateAvailableItems.Click += new System.EventHandler(this.mnuRecalculateAvailableItems_Click);
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
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.statusBar, "statusBar");
            this.statusBar.Name = "statusBar";
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.TabBars.ResumeLayout(false);
            this.tabContacts.ResumeLayout(false);
            this.tabContacts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvContacts)).EndInit();
            this.mnuContacts.ResumeLayout(false);
            this.tabLending.ResumeLayout(false);
            this.tabLending.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvLending)).EndInit();
            this.tabBorrowing.ResumeLayout(false);
            this.tabBorrowing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBorrowing)).EndInit();
            this.tabItems.ResumeLayout(false);
            this.tabItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvItem)).EndInit();
            this.tabBooks.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olvBooks)).EndInit();
            this.tabBoardGames.ResumeLayout(false);
            this.tabBoardGames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvBoard)).EndInit();
            this.tabGames.ResumeLayout(false);
            this.tabGames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvGames)).EndInit();
            this.tabRecipes.ResumeLayout(false);
            this.tabRecipes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvRecipes)).EndInit();
            this.tabObject.ResumeLayout(false);
            this.tabObject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvObjects)).EndInit();
            this.toolEditItem.ResumeLayout(false);
            this.toolEditItem.PerformLayout();
            this.toolFastFilter.ResumeLayout(false);
            this.toolFastFilter.PerformLayout();
            this.toolFilter.ResumeLayout(false);
            this.toolFilter.PerformLayout();
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
        private System.Windows.Forms.ToolStrip toolFilter;
        private System.Windows.Forms.ToolStripMenuItem mnuNewDB;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenDB;
        private System.Windows.Forms.ToolStripMenuItem mnuSavaAsDB;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuShow;
        private System.Windows.Forms.ToolStripMenuItem mnuToolBars;
        private System.Windows.Forms.ToolStripMenuItem mnuShowFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuShowContacts;
        private System.Windows.Forms.ToolStripMenuItem mnuShowBorrowing;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuShowBooks;
        private System.Windows.Forms.ToolStripMenuItem mnuShowBoardGames;
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
        private System.Windows.Forms.ToolStripLabel lblFilter;
        private System.Windows.Forms.ToolStripTextBox txtFilter;
        private System.Windows.Forms.ToolStripComboBox cbFilterCol;
        private System.Windows.Forms.ToolStripButton btnFilterPin1;
        private System.Windows.Forms.ToolStripButton btnFilterPin2;
        private System.Windows.Forms.ToolStripButton btnFilterPin3;
        private System.Windows.Forms.ToolStripButton btnFilterPin4;
        private System.Windows.Forms.ToolStripButton btnFilterPin5;
        private System.Windows.Forms.ToolStripButton btnFilterPin6;
        private System.Windows.Forms.ToolStrip toolEditItem;
        private System.Windows.Forms.ToolStripButton btnNewItem;
        private System.Windows.Forms.ToolStripButton btnEditItem;
        private System.Windows.Forms.ToolStripButton btnDeleteItem;
        private System.Windows.Forms.ToolStripMenuItem mnuNewItem;
        private System.Windows.Forms.ToolStripMenuItem mnuEditItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDelItem;
        private System.Windows.Forms.ToolStrip toolFastFilter;
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
        private System.Windows.Forms.ImageList imgBarList;
        private System.Windows.Forms.ImageList imgOLV;
        private System.Windows.Forms.ToolStripMenuItem mnuShowItems;
        private System.Windows.Forms.TabControl TabBars;
        private System.Windows.Forms.TabPage tabContacts;
        private System.Windows.Forms.Button btnTest;
        private BrightIdeasSoftware.FastObjectListView olvContacts;
        private BrightIdeasSoftware.OLVColumn conFastTags;
        private BrightIdeasSoftware.OLVColumn conName;
        private BrightIdeasSoftware.OLVColumn conSurname;
        private BrightIdeasSoftware.OLVColumn conPhone;
        private BrightIdeasSoftware.OLVColumn conEmail;
        private BrightIdeasSoftware.OLVColumn conAddress;
        private System.Windows.Forms.TabPage tabLending;
        private System.Windows.Forms.CheckBox chbShowReturned;
        private System.Windows.Forms.TabPage tabItems;
        private System.Windows.Forms.CheckBox chbShowExcluded;
        private BrightIdeasSoftware.FastObjectListView olvItem;
        private BrightIdeasSoftware.OLVColumn itFastTags;
        private BrightIdeasSoftware.OLVColumn itName;
        private BrightIdeasSoftware.OLVColumn itCategory;
        private BrightIdeasSoftware.OLVColumn itSubcategory;
        private BrightIdeasSoftware.OLVColumn itInvNum;
        private BrightIdeasSoftware.OLVColumn itLocation;
        private BrightIdeasSoftware.OLVColumn itKeywords;
        private BrightIdeasSoftware.OLVColumn itCounts;
        private BrightIdeasSoftware.OLVColumn itAvailable;
        private BrightIdeasSoftware.OLVColumn itExcluded;
        private System.Windows.Forms.TabPage tabBooks;
        private BrightIdeasSoftware.FastObjectListView olvBooks;
        private BrightIdeasSoftware.OLVColumn bkFastTags;
        private BrightIdeasSoftware.OLVColumn bkName;
        private BrightIdeasSoftware.OLVColumn bkAuthor;
        private BrightIdeasSoftware.OLVColumn bkCount;
        private BrightIdeasSoftware.OLVColumn bkAvailable;
        private BrightIdeasSoftware.OLVColumn bkType;
        private BrightIdeasSoftware.OLVColumn bkYear;
        private BrightIdeasSoftware.OLVColumn bkGenre;
        private BrightIdeasSoftware.OLVColumn bkSubgenre;
        private BrightIdeasSoftware.OLVColumn bkInvNum;
        private BrightIdeasSoftware.OLVColumn bkLocation;
        private BrightIdeasSoftware.OLVColumn bkKeywords;
        private BrightIdeasSoftware.OLVColumn bkSeries;
        private System.Windows.Forms.TabPage tabBoardGames;
        private System.Windows.Forms.TabPage tabAudio;
        private System.Windows.Forms.TabPage tabVideo;
        private System.Windows.Forms.TabPage tabFoto;
        private System.Windows.Forms.ComboBox cbLendingShow;
        private System.Windows.Forms.Label lblLendingShow;
        private BrightIdeasSoftware.OLVColumn itFastTagsNum;
        private BrightIdeasSoftware.OLVColumn conFastTagsNum;
        private BrightIdeasSoftware.OLVColumn bkFastTagsNum;
        private System.Windows.Forms.CheckBox chbShowExcludedBoard;
        private BrightIdeasSoftware.FastObjectListView olvBoard;
        private BrightIdeasSoftware.OLVColumn bgTags;
        private BrightIdeasSoftware.OLVColumn bgName;
        private BrightIdeasSoftware.OLVColumn bgCategory;
        private BrightIdeasSoftware.OLVColumn bgInvNum;
        private BrightIdeasSoftware.OLVColumn bgLocation;
        private BrightIdeasSoftware.OLVColumn bgKeywords;
        private BrightIdeasSoftware.OLVColumn bgCounts;
        private BrightIdeasSoftware.OLVColumn bgAvailable;
        private BrightIdeasSoftware.OLVColumn bgExcluded;
        private BrightIdeasSoftware.OLVColumn bgTagsNum;
        private System.Windows.Forms.TabPage tabBorrowing;
        private System.Windows.Forms.ComboBox cbBorrowingShow;
        private System.Windows.Forms.Label lblShowBorrowing;
        private System.Windows.Forms.CheckBox chbBorrowingReturned;
        private BrightIdeasSoftware.FastObjectListView olvBorrowing;
        private BrightIdeasSoftware.OLVColumn brPerson;
        private BrightIdeasSoftware.OLVColumn brItemName;
        private BrightIdeasSoftware.OLVColumn brItemInvNum;
        private BrightIdeasSoftware.OLVColumn brFrom;
        private BrightIdeasSoftware.OLVColumn brTo;
        private BrightIdeasSoftware.OLVColumn brStatus;
        private BrightIdeasSoftware.OLVColumn brType;
        private BrightIdeasSoftware.OLVColumn brItemNum;
        private BrightIdeasSoftware.FastObjectListView olvLending;
        private BrightIdeasSoftware.OLVColumn ldPerson;
        private BrightIdeasSoftware.OLVColumn ldItemType;
        private BrightIdeasSoftware.OLVColumn ldItemName;
        private BrightIdeasSoftware.OLVColumn ldItemNum;
        private BrightIdeasSoftware.OLVColumn ldItemInvNum;
        private BrightIdeasSoftware.OLVColumn ldFrom;
        private BrightIdeasSoftware.OLVColumn ldTo;
        private BrightIdeasSoftware.OLVColumn ldStatus;
        private BrightIdeasSoftware.OLVColumn brFastTags;
        private BrightIdeasSoftware.OLVColumn brNote;
        private BrightIdeasSoftware.OLVColumn brFastTagsNum;
        private System.Windows.Forms.Button btnClearOldReservations;
        private System.Windows.Forms.Button btnBorrowings;
        private BrightIdeasSoftware.OLVColumn ldFastTags;
        private BrightIdeasSoftware.OLVColumn ldFastTagsNum;
        private BrightIdeasSoftware.OLVColumn ldNote;
        private System.Windows.Forms.Button btnPersonalLending;
        private System.Windows.Forms.ToolStripMenuItem mnuSubTools;
        private System.Windows.Forms.ToolStripMenuItem mnuRecalculateAvailableItems;
        private System.Windows.Forms.Button btnPrintTest;
        private System.Windows.Forms.ToolStripMenuItem mnuShowLending;
        private System.Windows.Forms.ToolStripMenuItem mnuShowFastFilter;
        private BrightIdeasSoftware.OLVColumn conNick;
        private BrightIdeasSoftware.OLVColumn conCompany;
        private System.Windows.Forms.ContextMenuStrip mnuContacts;
        private System.Windows.Forms.ToolStripMenuItem mnuCNew;
        private System.Windows.Forms.ToolStripMenuItem mnuCEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuCDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuCLending;
        private System.Windows.Forms.ToolStripMenuItem mnuCBorrowing;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem synchronizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuCImportGoogle;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuCExportGoogle;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem setToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuCSetGreenTag;
        private System.Windows.Forms.ToolStripMenuItem mnuCSetRedTag;
        private System.Windows.Forms.ToolStripMenuItem mnuCSetOrangeTag;
        private System.Windows.Forms.ToolStripMenuItem mnuCSetYellowTag;
        private System.Windows.Forms.ToolStripMenuItem mnuCSetGreyTag;
        private System.Windows.Forms.ToolStripMenuItem mnuCSetBlueTag;
        private System.Windows.Forms.ToolStripMenuItem mnuCActive;
        private System.Windows.Forms.ToolStripMenuItem mnuImportFromFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem mnuImportGoogleContacts;
        private System.Windows.Forms.ToolStripMenuItem mnuExportToFile;
        private System.Windows.Forms.CheckBox chbShowUnactivCon;
        private System.Windows.Forms.TabPage tabGames;
        private System.Windows.Forms.TabPage tabRecipes;
        private System.Windows.Forms.CheckBox chbShowExcludedGames;
        private BrightIdeasSoftware.FastObjectListView olvGames;
        private BrightIdeasSoftware.OLVColumn gmTags;
        private BrightIdeasSoftware.OLVColumn gmName;
        private BrightIdeasSoftware.OLVColumn gmCategory;
        private BrightIdeasSoftware.OLVColumn gmMinPlayers;
        private BrightIdeasSoftware.OLVColumn gmMaxPlayers;
        private BrightIdeasSoftware.OLVColumn gmKeywords;
        private BrightIdeasSoftware.OLVColumn gmEnviroment;
        private BrightIdeasSoftware.OLVColumn gmExcluded;
        private BrightIdeasSoftware.OLVColumn gmTagsNum;
        private System.Windows.Forms.CheckBox chbShowExcludedRecipes;
        private BrightIdeasSoftware.FastObjectListView olvRecipes;
        private BrightIdeasSoftware.OLVColumn recTags;
        private BrightIdeasSoftware.OLVColumn recName;
        private BrightIdeasSoftware.OLVColumn recCategory;
        private BrightIdeasSoftware.OLVColumn recKeywords;
        private BrightIdeasSoftware.OLVColumn recExcluded;
        private BrightIdeasSoftware.OLVColumn recTagsNum;
        private BrightIdeasSoftware.OLVColumn gmPlayerAge;
        private BrightIdeasSoftware.OLVColumn gmDuration;
        private BrightIdeasSoftware.OLVColumn gmThings;
        private System.Windows.Forms.TabPage tabObject;
        private System.Windows.Forms.CheckBox chbShowExcludedObjects;
        private BrightIdeasSoftware.FastObjectListView olvObjects;
        private BrightIdeasSoftware.OLVColumn objTags;
        private BrightIdeasSoftware.OLVColumn objName;
        private BrightIdeasSoftware.OLVColumn objCategory;
        private BrightIdeasSoftware.OLVColumn objNumber;
        private BrightIdeasSoftware.OLVColumn objVersion;
        private BrightIdeasSoftware.OLVColumn objKeywords;
        private BrightIdeasSoftware.OLVColumn objFolder;
        private BrightIdeasSoftware.OLVColumn objType;
        private BrightIdeasSoftware.OLVColumn objActive;
        private BrightIdeasSoftware.OLVColumn objTagsNum;
        private BrightIdeasSoftware.OLVColumn objParent;
        private BrightIdeasSoftware.OLVColumn objCustomer;
        private BrightIdeasSoftware.OLVColumn objDevelopment;
        private System.Windows.Forms.ComboBox cbObjectShow;
        private System.Windows.Forms.Label lblObjectShow;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem mnuShowGames;
        private System.Windows.Forms.ToolStripMenuItem mnuShowRecipes;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem mnuShowObjects;
    }
}

