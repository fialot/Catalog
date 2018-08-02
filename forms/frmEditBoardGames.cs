using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Communications;
using myFunctions;
using TCPClient;

namespace Katalog
{
    public partial class frmEditBoardGames : Form
    {
        #region Variables

        // ----- Fast Tags -----
        Color SelectColor = Color.SkyBlue;                  // FastTags Select color

        // ----- Database -----
        databaseEntities db = new databaseEntities();       // Database
        Guid ID = Guid.Empty;                               // Selected Item GUID (No Guid = new item)

        // ----- Copies -----
        List<Copies> CopiesList = new List<Copies>();       // Copies list
        List<Copies> OriginalCopies = new List<Copies>();   // Original Copies list
        int SelCopy = 0;

        // ----- Inventory number -----
        long TempMaxInvNum = MaxInvNumbers.Boardgame;       // Max Inv. Number

        // ----- Barcode reader communication -----
        Communication com = new Communication();            // Barcode communication
        public delegate void MyDelegate(comStatus status);  // Communication delegate
        string Barcode = "";

        #endregion

        #region Constructor

        public frmEditBoardGames()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Load
        
        /// <summary>
        /// ShowDialog with ID (Edit)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(Guid ID)
        {
            this.ID = ID;                   // Item ID
            return base.ShowDialog();       // Base ShowDialog
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditBoardGames_Load(object sender, EventArgs e)
        {
            // ----- Create barcode reader connection -----
            com.ReceivedData += new ReceivedEventHandler(DataReceive);
            try
            {
                com.ConnectSP(Properties.Settings.Default.scanCOM);
            }
            catch { }

            // ----- Get Autofill lists -----
            var categoryList = db.Boardgames.Select(x => x.Category.Trim()).ToList();
            var locationList = db.Copies.Select(x => x.Location.Trim()).ToList();

            // ----- Delete duplicates -----
            categoryList = global.DeleteDuplicates(categoryList);
            locationList = global.DeleteDuplicates(locationList);

            // ----- Prepare autocomplete -----
            txtCondition.AutoCompleteCustomSource.Add(Lng.Get("New"));
            txtCondition.AutoCompleteCustomSource.Add(Lng.Get("Preserved"));
            txtCondition.AutoCompleteCustomSource.Add(Lng.Get("Damaged"));
            txtCondition.AutoCompleteCustomSource.Add(Lng.Get("Destroyed"));
            txtCondition.AutoCompleteCustomSource.Add(Lng.Get("Unfunctional"));

            foreach (var item in categoryList)
                txtCategory.AutoCompleteCustomSource.Add(item);
            foreach (var item in locationList)
                txtLocation.AutoCompleteCustomSource.Add(item);

            // ----- Add Specimen -----
            cbCopy.Items.Clear();
            cbCopy.Items.Add("1");
            cbCopy.SelectedIndex = 0;

            Copies copy = global.CreateCopy(ID, ItemTypes.boardgame);
            CopiesList.Add(copy);

            // ----- If Edit -> fill form -----
            if (ID != Guid.Empty)
            {
                Boardgames itm = db.Boardgames.Find(ID);

                // ----- Fill Image -----
                imgCover.Image = Conv.ByteArrayToImage(itm.Cover);
                img1.Image = Conv.ByteArrayToImage(itm.Img1);
                img2.Image = Conv.ByteArrayToImage(itm.Img2);
                img3.Image = Conv.ByteArrayToImage(itm.Img3);

                // ----- Fill main data -----   
                txtName.Text = itm.Name.Trim();                     // Name
                txtURL.Text = itm.URL.Trim();                       // URL
                txtCategory.Text = itm.Category.Trim();             // Category
                txtLanguage.Text = itm.Language.Trim();             // Language
                txtKeywords.Text = itm.Keywords.Trim();             // Keywords
                txtGameWorld.Text = itm.GameWorld.Trim();           // Game World
                txtPublisher.Text = itm.Publisher.Trim();           // Publisher
                txtAuthor.Text = itm.Author.Trim();                 // Author
                txtYear.Text = itm.Year.ToString();                 // Year

                txtNote.Text = itm.Note.Trim();                     // Note

                numMinPlayers.Value = itm.MinPlayers ?? 0;          // Min Players
                numMaxPlayers.Value = itm.MaxPlayers ?? 0;          // Max Players
                numMinAge.Value = itm.MinAge ?? 0;                  // Min Age
                numGameTime.Value = itm.GameTime ?? 0;              // Game Time

                txtFamily.Text = itm.Family.Trim();                 // Family
                chbExtension.Checked = itm.Extension ?? false;      // Extension
                numExtension.Value = itm.ExtensionNumber ?? 0;      // Extension number

                txtRating.Text = itm.Rating.ToString();             // Rating
                txtMyRating.Text = itm.MyRating.ToString();         // My rating


                // ----- Fill Specimen -----
                CopiesList = db.Copies.Where(x => x.ItemID == ID).ToList();
                OriginalCopies = db.Copies.Where(x => x.ItemID == ID).ToList();

                cbCopy.Items.Clear();

                // ----- If found copies -----
                if (CopiesList != null && CopiesList.Count > 0)
                {
                    // ----- Fill textboxes -----
                    FillFromCopies(CopiesList[0]);

                    // ----- Prepare buttons -----
                    if (CopiesList.Count > 1) btnDelCopy.Enabled = true;

                    // ----- Prepare combobox -----
                    for (int i = 0; i < CopiesList.Count; i++)              // Fill Copies combobox
                    {
                        cbCopy.Items.Add((i + 1).ToString());
                    }
                    cbCopy.SelectedIndex = 0;
                    lblCount.Text = "/ " + CopiesList.Count.ToString();            // Counts
                }

                // ----- Fast tags -----
                FastFlags flag = (FastFlags)(itm.FastTags ?? 0);
                if (flag.HasFlag(FastFlags.FLAG1)) btnTag1.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG2)) btnTag2.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG3)) btnTag3.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG4)) btnTag4.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG5)) btnTag5.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG6)) btnTag6.BackColor = SelectColor;

                // ----- Update -----
                lblUpdated.Text = Lng.Get("LastUpdate", "Last update") + ": " + (itm.Updated ?? DateTime.Now).ToShortDateString();
            }
            else
            {
                // ----- Set Acquisition date -----
                dtAcqDate.Value = DateTime.Now;
                numMinPlayers.Value = 2;          // Min Players
                numMaxPlayers.Value = 4;          // Max Players

                // ----- New Inv Number -----
                if(TempMaxInvNum < Properties.Settings.Default.BoardStart) TempMaxInvNum = Properties.Settings.Default.BoardStart;
                else TempMaxInvNum++;
                txtInvNum.Text = Properties.Settings.Default.BoardPrefix + (TempMaxInvNum).ToString("D" + Properties.Settings.Default.BoardMinCharLen.ToString()) + Properties.Settings.Default.BoardSuffix;
            }
        }

        #endregion

        #region Form Close

        /// <summary>
        /// Fill Item values
        /// </summary>
        /// <param name="itm"></param>
        private void FillItem(ref Boardgames itm)
        {
            // ----- Fill Image -----
            byte[] bytes = Conv.ImageToByteArray(imgCover.Image);
            if (bytes != null) itm.Cover = bytes;
            bytes = Conv.ImageToByteArray(img1.Image);
            if (bytes != null) itm.Img1 = bytes;
            bytes = Conv.ImageToByteArray(img2.Image);
            if (bytes != null) itm.Img2 = bytes;
            bytes = Conv.ImageToByteArray(img3.Image);
            if (bytes != null) itm.Img3 = bytes;


            // ----- Fill main data -----   
            itm.Name = txtName.Text;                        // Name
            itm.URL = txtURL.Text;                          // URL
            itm.Category = txtCategory.Text;                // Category

            itm.Language = txtLanguage.Text;                // Language
            itm.Keywords = txtKeywords.Text;                // Keywords
            itm.GameWorld = txtGameWorld.Text;              // Game World
            itm.Publisher = txtPublisher.Text;              // Publisher
            itm.Author = txtAuthor.Text;                    // Author
            itm.Year = Conv.ToShortNull(txtYear.Text);      // Year

            itm.Note = txtNote.Text;                        // Note

            itm.MinPlayers = (short)numMinPlayers.Value;    // Min Players
            itm.MaxPlayers = (short)numMaxPlayers.Value;    // Max Players
            itm.MinAge = (short)numMinAge.Value;            // Min Age
            itm.GameTime = (short)numGameTime.Value;        // Game Time

            itm.Family = txtFamily.Text;                    // Family
            itm.Extension = chbExtension.Checked;           // Extension
            itm.ExtensionNumber = (short)numExtension.Value;  // Extension number

            itm.Rating = Conv.ToShortNull(txtRating.Text);  // Rating
            itm.MyRating = Conv.ToShortNull(txtMyRating.Text);  // My rating

            itm.Description = "";
            itm.Rules = "";
            itm.MaterialPath = "";

            // ----- Status -----
            itm.Count = global.GetCopiesCount(CopiesList);          // Get counts
            itm.Available = global.GetAvailableCopies(CopiesList);  // Get available items

            // ----- Fast tags -----
            short fastTag = 0;
            if (btnTag1.BackColor == SelectColor) fastTag |= 0x01;
            if (btnTag2.BackColor == SelectColor) fastTag |= 0x02;
            if (btnTag3.BackColor == SelectColor) fastTag |= 0x04;
            if (btnTag4.BackColor == SelectColor) fastTag |= 0x08;
            if (btnTag5.BackColor == SelectColor) fastTag |= 0x10;
            if (btnTag6.BackColor == SelectColor) fastTag |= 0x20;
            itm.FastTags = fastTag;

            // ----- Last Update -----
            itm.Updated = DateTime.Now;
        }
        
        /// <summary>
        /// Fill Copies from Form
        /// </summary>
        /// <param name="itm">Copies item</param>
        private void FillCopies(ref Copies itm)
        {
            // ----- Fill Copies -----
            itm.Price = Conv.ToDoubleNull(txtPrice.Text);   // Price
            itm.AcquisitionDate = dtAcqDate.Value;          // Acqusition date
            itm.Excluded = chbExcluded.Checked;             // Excluded
            itm.Condition = txtCondition.Text;              // Condition
            itm.InventoryNumber = txtInvNum.Text;           // Inventory Number
            itm.Barcode = Conv.ToNumber(itm.InventoryNumber); // Barcode
            itm.Location = txtLocation.Text;                // Location

            // ----- Recalculate maxnum -----
            long maxNum = Conv.ToNumber(itm.InventoryNumber);
            if (maxNum > MaxInvNumbers.Boardgame) MaxInvNumbers.Boardgame = maxNum;
        }
        
        /// <summary>
        /// Fill Form from Copies
        /// </summary>
        /// <param name="itm">Copies Item</param>
        private void FillFromCopies(Copies itm)
        {
            // ----- Fill Copies -----
            txtPrice.Text = itm.Price.ToString();           // Price
            dtAcqDate.Value = itm.AcquisitionDate ?? DateTime.Now; // Acqusition date
            chbExcluded.Checked = itm.Excluded ?? false;    // Excluded
            txtCondition.Text = itm.Condition;       // Condition
            txtInvNum.Text = itm.InventoryNumber;    // Inventory Number
            txtLocation.Text = itm.Location;         // Location
        }

        /// <summary>
        /// Save edited items to DB
        /// </summary>
        private void SaveItem()
        {
            Boardgames itm;

            // ----- Save last Specimen values -----
            SaveCopy();

            // ----- Check Duplicate InvNum -----
            foreach (var item in CopiesList)
            {
                string DulpicateInvNUm = "";
                if (global.IsDuplicate(item.InventoryNumber, item.ID))
                {
                    if (Dialogs.ShowQuest(String.Format(Lng.Get("DuplicateInvNum", "The inventory number {0} is already in use. Do you really write to database?"), DulpicateInvNUm), Lng.Get("Warning")) != DialogResult.Yes) return;
                }
            }

            // ----- ID -----
            if (ID != Guid.Empty)
            {
                itm = db.Boardgames.Find(ID);
            }
            else
            {
                itm = new Boardgames();
                itm.ID = Guid.NewGuid();
            }

            // ----- Delete original Copies -----
            foreach (var item in OriginalCopies)
            {
                db.Copies.Remove(item);
            }
            db.SaveChanges();

            // ----- Add Copies to DB -----
            foreach (var item in CopiesList)
            {
                item.ItemID = itm.ID;
                db.Copies.Add(item);
            }
            db.SaveChanges();

            // ----- Fill Item values -----
            FillItem(ref itm);

            // ----- Update database -----
            if (ID == Guid.Empty) db.Boardgames.Add(itm);
            db.SaveChanges();
        }

        /// <summary>
        /// Button OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // ----- Save to DB -----
            SaveItem();

            // ----- Exit -----
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Button Save - New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            // ----- Save to DB -----
            SaveItem();

            // ----- Exit -----
            this.DialogResult = DialogResult.Yes;
        }

        /// <summary>
        /// Form Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditBoardGames_FormClosing(object sender, FormClosingEventArgs e)
        {
            com.Close();
        }

        #endregion

        #region Copies
        
        /// <summary>
        /// Save Form Items to selected Copy
        /// </summary>
        private void SaveCopy()
        {
            Copies copy = CopiesList[SelCopy];
            FillCopies(ref copy);
            CopiesList.RemoveAt(SelCopy);
            CopiesList.Insert(SelCopy, copy);
        }

        /// <summary>
        /// Button Add Copy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCopy_Click(object sender, EventArgs e)
        {
            string InvNum = txtInvNum.Text;
            // ----- Increment Inv Num -----
            if (Properties.Settings.Default.IncSpecimenInv)
            {
                TempMaxInvNum++;
                InvNum = Properties.Settings.Default.BoardPrefix + (TempMaxInvNum).ToString("D" + Properties.Settings.Default.BoardMinCharLen.ToString()) + Properties.Settings.Default.BoardSuffix;
            }

            //  ----- Save Form fill to selected Copy -----
            SaveCopy();

            // ----- Create new Copy -----
            Copies copy = global.CopyCopies(CopiesList[SelCopy]);
            copy.InventoryNumber = InvNum;
            CopiesList.Add(copy);

            // ----- Add New copy to combobox -----
            cbCopy.Items.Add((cbCopy.Items.Count + 1).ToString());
            cbCopy.SelectedIndex = cbCopy.Items.Count - 1;
            btnDelCopy.Enabled = true;

            // ---- Refresh Counts label -----
            lblCount.Text = "/ " + CopiesList.Count.ToString();        // Counts
        }

        /// <summary>
        /// Button Delete Copy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelCopy_Click(object sender, EventArgs e)
        {
            if (CopiesList.Count > 0)
            {
                if (!global.IsAvailable(CopiesList[SelCopy].Status))
                {
                    Dialogs.ShowWar(Lng.Get("ItmIsUsed", "Copy count cannot change, because item is used (borrowed/reserved)."), Lng.Get("Warning"));
                    return;
                }

                int sel = cbCopy.SelectedIndex;
                CopiesList.RemoveAt(sel);
                cbCopy.Items.RemoveAt(cbCopy.Items.Count - 1);
                if (sel >= cbCopy.Items.Count)
                    cbCopy.SelectedIndex = sel - 1;
                else
                {
                    FillFromCopies(CopiesList[sel]);
                }

                lblCount.Text = "/ " + CopiesList.Count.ToString();        // Counts
                if (CopiesList.Count == 1)
                {
                    btnDelCopy.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Copy Combobox change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCopy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CopiesList.Count > 0)
            {
                if (SelCopy < CopiesList.Count)
                {
                    Copies copy = CopiesList[SelCopy];
                    FillCopies(ref copy);
                    CopiesList.RemoveAt(SelCopy);
                    CopiesList.Insert(SelCopy, copy);
                }
                FillFromCopies(CopiesList[cbCopy.SelectedIndex]);
                SelCopy = cbCopy.SelectedIndex;
            }
        }

        /// <summary>
        /// Button Location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLocation_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Lng.Get("AllFiles", "All files") + "|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtLocation.Text = dialog.FileName;
            }
        }

        #endregion
        
        #region FastTags

        /// <summary>
        /// Set fast tag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTag1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).BackColor == SystemColors.Control)
                ((Button)sender).BackColor = SelectColor;
            else
                ((Button)sender).BackColor = SystemColors.Control;
        }

        #endregion

        #region Image

        /// <summary>
        /// Load Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgImg_Click(object sender, EventArgs e)
        {
            PictureBox imgImg = (PictureBox)sender;
            Image img = imgImg.Image;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Lng.Get("Images") + " |*.jpg;*.jpeg;*.jpe;*.tiff;*.png;*.gif";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imgImg.Load(dialog.FileName);
                }
                catch
                {
                    imgImg.Image = img;
                    img.Dispose();
                    Dialogs.ShowErr(Lng.Get("ErrLoadImg", "Image cannot load!"), Lng.Get("Error"));
                }
            }
        }

        #endregion

        #region Barcode

        /// <summary>
        /// Data receive delegate
        /// </summary>
        /// <param name="source"></param>
        /// <param name="status"></param>
        private void DataReceive(object source, comStatus status)
        {
            txtInvNum.Invoke(new MyDelegate(DataProcess), new Object[] { status }); //BeginInvoke

        }

        /// <summary>
        /// Data process function
        /// </summary>
        /// <param name="status"></param>
        public void DataProcess(comStatus status)
        {
            if (status == comStatus.Close)
            {

            }
            // ----- Status Incoming data -----
            else if (status == comStatus.OK)
            {
                TimeOut.Enabled = false;
                Barcode += com.ReadString();
                TimeOut.Enabled = true;
            }
            else if (status == comStatus.Open)
            {

            }
            else if (status == comStatus.OpenError)
            {

            }
        }

        /// <summary>
        /// Process data after timeout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeOut_Tick(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();

            TimeOut.Enabled = false;
            if (txtInvNum.Focused)
            {
                txtInvNum.Text = Barcode.Replace("\r", "").Replace("\n,", "");
            }
            Barcode = "";
        }
        
        #endregion

        private void btnGetFromURL_Click(object sender, EventArgs e)
        {
            GetBoardDesc(txtURL.Text);
        }

        private void GetBoardDesc(string url)
        {
            

            // ----- Get Cober from "Obalkyknih.cz" -----
            string urlAddress = url;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                int pos = data.IndexOf("table class=\"detail\"");
                if (pos >= 0)
                {
                    int posStart = data.IndexOf("<a href=", pos);
                    if (posStart >= 0)
                    {
                        posStart += 9;
                        int posStop = data.IndexOf("\"", posStart);
                        if (posStop >= 0)
                        {
                            string strImg = data.Substring(posStart, posStop - posStart);
                            using (WebClient client = new WebClient())
                            {
                                string path = Path.GetTempFileName();
                                client.DownloadFile(strImg, path);
                                //itm.coverPath = path;
                            }

                        }
                    }
                }

                // ----- Anotation -----
                pos = data.IndexOf("<h3>Anotace</h3>");
                if (pos >= 0)
                {
                    int posStart = data.IndexOf("<p>", pos);
                    if (posStart >= 0)
                    {
                        posStart += 3;
                        int posStop = data.IndexOf("</p>", posStart);
                        if (posStop >= 0)
                        {
                            //itm.Content = data.Substring(posStart, posStop - posStart).Trim();
                        }
                    }
                }
                response.Close();
                readStream.Close();


            }
        }
    }
}
