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
        
        Color SelectColor = Color.SkyBlue;                  // FastTags Select color

        databaseEntities db = new databaseEntities();       // Database
        Guid ID = Guid.Empty;                               // Selected Item GUID (No Guid = new item)


        int ItemCount = 1;                                  // Item count
        List<string> InvNumbers = new List<string>();       // Items Inventory numbers
        List<string> Locations = new List<string>();        // Items Locations 
        int SelSpecimen = 0;

        long TempMaxInvNum = MaxInvNumbers.Boardgame;       // Max Inv. Number
        bool IsUsed = false;

        Communication com = new Communication();
        public delegate void MyDelegate(comStatus status);
        string Barcode = "";

        #endregion

        #region Constructor

        public frmEditBoardGames()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Load


        List<string> DeleteDuplicates(List<string> list)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                bool find = false;
                for (int j = 0; j < res.Count; j++)
                {
                    if (list[i] == res[j])
                    {
                        find = true;
                        break;
                    }
                }
                if (!find && list[i] != "")
                    res.Add(list[i]);
            }
            return res;
        }


        /// <summary>
        /// ShowDialog with ID (Edit)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(Guid ID)
        {
            this.ID = ID;
            return base.ShowDialog();
        }


        private void frmEditBoardGames_Load(object sender, EventArgs e)
        {
            // ----- Init COM (Barcode) -----
            com.ReceivedData += new ReceivedEventHandler(DataReceive);
            try
            {
                com.ConnectSP(Properties.Settings.Default.scanCOM);
            }
            catch { }

            var categoryList = db.Boardgames.Select(x => x.Category.Trim()).ToList();
            var locationList = db.Boardgames.Select(x => x.Location.Trim()).ToList();

            for (int i = locationList.Count - 1; i >= 0; i--)
            {
                string[] temp = locationList[i].Split(new string[] { ";" }, StringSplitOptions.None);
                if (temp.Length > 1)
                {
                    locationList.RemoveAt(i);
                    foreach (var item in temp)
                    {
                        locationList.Insert(i, item);
                    }
                }
            }

            categoryList = DeleteDuplicates(categoryList);
            locationList = DeleteDuplicates(locationList);

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
            cbSpecimen.Items.Clear();
            cbSpecimen.Items.Add("1");
            cbSpecimen.SelectedIndex = 0;
            InvNumbers.Add("");
            Locations.Add("");

            // ----- Set Acquisition date -----
            dtAcqDate.Value = DateTime.Now;
            numMinPlayers.Value = 2;          // Min Players
            numMaxPlayers.Value = 4;          // Max Players

            // ----- New Inv Number -----
            TempMaxInvNum++;
            txtInvNum.Text = Properties.Settings.Default.BoardPrefix + (TempMaxInvNum).ToString("D" + Properties.Settings.Default.BoardMinCharLen.ToString()) + Properties.Settings.Default.BoardSuffix;

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

                dtAcqDate.Value = itm.AcquisitionDate ?? DateTime.Now;  // Acqusition date
                txtPrice.Text = itm.Price.ToString();               // Price
                txtCondition.Text = itm.Condition.Trim();           // Condition
                chbExcluded.Checked = itm.Excluded ?? false;        // Excluded

                txtRating.Text = itm.Rating.ToString();             // Rating
                txtMyRating.Text = itm.MyRating.ToString();         // My rating
                

                // ----- Fill Specimen -----
                ItemCount = itm.Count ?? 1;                         // Get counts
                if (ItemCount > 1) btnDelSpecimen.Enabled = true;
                if ((itm.Available ?? ItemCount) < ItemCount)
                    IsUsed = true;

                cbSpecimen.Items.Clear();
                InvNumbers.Clear();
                Locations.Clear();

                string[] invNums = itm.InventoryNumber.Trim().Split(new string[] { ";" }, StringSplitOptions.None);
                string[] locs = itm.Location.Trim().Split(new string[] { ";" }, StringSplitOptions.None);

                for (int i = 0; i < ItemCount; i++)                 // Fill specimen
                {
                    cbSpecimen.Items.Add((i + 1).ToString());
                    if (i < invNums.Length)                         // Inventory numbers list
                        InvNumbers.Add(invNums[i]);
                    else
                        InvNumbers.Add("");
                    if (i < locs.Length)                            // Locations list
                        Locations.Add(locs[i]);
                    else
                        Locations.Add("");
                }
                txtInvNum.Text = InvNumbers[0];                     // Inventory number
                txtLocation.Text = Locations[0];                    // Location
                cbSpecimen.SelectedIndex = 0;


                lblCount.Text = "/ " + ItemCount.ToString();        // Counts

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
        }

        #endregion

        #region Form Close

        /// <summary>
        /// Check Duplicate Inventory number
        /// </summary>
        /// <param name="InvNum">Ger duplicate Inventory number</param>
        /// <returns>Returns true if duplicate exist</returns>
        private bool IsDuplicate(out string InvNum)
        {
            InvNum = "";
            var list = db.Boardgames.Where(x => x.ID != ID).Select(x => x.InventoryNumber).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                string[] separate = list[i].Trim().Split(new string[] { ";" }, StringSplitOptions.None);
                for (int j = 0; j < separate.Length; j++)
                {
                    for (int k = 0; k < ItemCount; k++)
                    {
                        if (separate[j] == InvNumbers[k])
                        {
                            InvNum = InvNumbers[k];
                            return true;
                        }
                    }
                }
            }
            return false;
        }

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

            itm.Price = Conv.ToDoubleNull(txtPrice.Text);   // Price
            itm.AcquisitionDate = dtAcqDate.Value;          // Acqusition date
            itm.Excluded = chbExcluded.Checked;             // Excluded
            itm.Condition = txtCondition.Text;              // Condition

            itm.Rating = Conv.ToShortNull(txtRating.Text);  // Rating
            itm.MyRating = Conv.ToShortNull(txtMyRating.Text);  // My rating

            // ----- Fill Specimen -----
            itm.Count = (short)ItemCount;                   // Get counts

            string invNums = "", locs = "";
            for (int i = 0; i < ItemCount; i++)             // Fill specimen
            {
                if (invNums != "") invNums += ";";
                invNums += InvNumbers[i];
                if (locs != "") locs += ";";
                locs += Locations[i];

                long maxNum = Conv.ToNumber(InvNumbers[i]);
                if (maxNum > MaxInvNumbers.Boardgame) MaxInvNumbers.Boardgame = maxNum;
            }
            itm.InventoryNumber = invNums;
            itm.Location = locs;
            if (ItemCount == 1)
                itm.Barcode = Conv.ToNumber(itm.InventoryNumber);
            else
                itm.Barcode = 0;

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

        private void SaveItem()
        {
            Boardgames itm;

            // ----- Save last Specimen values -----
            InvNumbers.RemoveAt(SelSpecimen);
            Locations.RemoveAt(SelSpecimen);
            InvNumbers.Insert(SelSpecimen, txtInvNum.Text);
            Locations.Insert(SelSpecimen, txtLocation.Text);

            // ----- Check Duplicate InvNum -----
            string DulpicateInvNUm = "";
            if (IsDuplicate(out DulpicateInvNUm))
            {
                if (Dialogs.ShowQuest(String.Format(Lng.Get("DuplicateInvNum", "The inventory number {0} is already in use. Do you really write to database?"), DulpicateInvNUm), Lng.Get("Warning")) != DialogResult.Yes) return;
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

        #endregion

        #region Barcode


        private void DataReceive(object source, comStatus status)
        {
            txtInvNum.Invoke(new MyDelegate(updateLog), new Object[] { status }); //BeginInvoke

        }

        public void updateLog(comStatus status)
        {
            if (status == comStatus.Close)
            {

            }
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


        #region Specimen

        private void btnAddSpecimen_Click(object sender, EventArgs e)
        {
            if (ID != Guid.Empty)
                if (IsUsed)
                {
                    Dialogs.ShowWar(Lng.Get("ItmIsUsed", "Speciman count cannot change, because item is used (borrowed/reserved)."), Lng.Get("Warning"));
                    return;
                }

            ItemCount++;
            string InvNum = txtInvNum.Text;
            // ----- Increment Inv Num -----
            if (Properties.Settings.Default.IncSpecimenInv)
            {
                TempMaxInvNum++;
                InvNum = Properties.Settings.Default.ItemPrefix + (TempMaxInvNum).ToString("D" + Properties.Settings.Default.ItemMinCharLen.ToString()) + Properties.Settings.Default.ItemSuffix;
            }
            InvNumbers.Add(InvNum);
            Locations.Add(txtLocation.Text);
            cbSpecimen.Items.Add((cbSpecimen.Items.Count + 1).ToString());
            cbSpecimen.SelectedIndex = cbSpecimen.Items.Count - 1;
            btnDelSpecimen.Enabled = true;
            lblCount.Text = "/ " + ItemCount.ToString();        // Counts
        }

        private void btnDelSpecimen_Click(object sender, EventArgs e)
        {
            if (ID != Guid.Empty)
                if (IsUsed)
                {
                    Dialogs.ShowWar(Lng.Get("ItmIsUsed", "Speciman count cannot change, because item is used (borrowed/reserved)."), Lng.Get("Warning"));
                    return;
                }

            if (ItemCount > 1)
            {
                int sel = cbSpecimen.SelectedIndex;
                ItemCount--;
                InvNumbers.RemoveAt(sel);
                Locations.RemoveAt(sel);
                cbSpecimen.Items.RemoveAt(cbSpecimen.Items.Count - 1);
                if (sel >= cbSpecimen.Items.Count)
                    cbSpecimen.SelectedIndex = sel - 1;
                else
                {
                    txtInvNum.Text = InvNumbers[sel];
                    txtLocation.Text = Locations[sel];
                }

                lblCount.Text = "/ " + ItemCount.ToString();        // Counts
                if (ItemCount == 1)
                {
                    btnDelSpecimen.Enabled = false;
                }
            }
        }

        private void cbSpecimen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InvNumbers.Count > 0)
            {
                if (SelSpecimen < InvNumbers.Count)
                {
                    InvNumbers.RemoveAt(SelSpecimen);
                    Locations.RemoveAt(SelSpecimen);
                    InvNumbers.Insert(SelSpecimen, txtInvNum.Text);
                    Locations.Insert(SelSpecimen, txtLocation.Text);
                }
                txtInvNum.Text = InvNumbers[cbSpecimen.SelectedIndex];
                txtLocation.Text = Locations[cbSpecimen.SelectedIndex];
                SelSpecimen = cbSpecimen.SelectedIndex;
            }
        }

        private void btnPlace_Click(object sender, EventArgs e)
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
        private void frmEditBoardGames_FormClosing(object sender, FormClosingEventArgs e)
        {
            com.Close();
        }

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
