using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Zoom.Net.YazSharp;
using myFunctions;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using Communications;
using TCPClient;

namespace Katalog
{
    public partial class frmEditBooks : Form
    {
        #region Variables

        Color SelectColor = Color.SkyBlue;                  // FastTags Select color

        databaseEntities db = new databaseEntities();       // Database
        Guid ID = Guid.Empty;                               // Selected Item GUID (No Guid = new item)

        int ItemCount = 1;                                  // Item count
        List<string> InvNumbers = new List<string>();       // Items Inventory numbers
        List<string> Locations = new List<string>();        // Items Locations 
        int SelSpecimen = 0;

        long TempMaxInvNum = MaxInvNumbers.Book;             // Max Inv. Number
        bool IsUsed = false;

        const int MaximumNumberOfResults = 20;

        Communication com = new Communication();
        public delegate void MyDelegate(comStatus status);
        string Barcode = "";

        #endregion

        #region Constructor

        public frmEditBooks()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Load

        private void SetBookbinding()
        {
            if (cbType.SelectedIndex == 1)
            {
                cbBookbinding.Items.Clear();
                cbBookbinding.Items.Add("");
                cbBookbinding.Items.Add(Lng.Get("epub"));
                cbBookbinding.Items.Add(Lng.Get("mobi"));
                cbBookbinding.Items.Add(Lng.Get("pdf"));
                cbBookbinding.Items.Add(Lng.Get("docx"));
                cbBookbinding.Items.Add(Lng.Get("xlsx"));
                cbBookbinding.SelectedIndex = 0;

                // ----- Prepare autocomplete -----
                cbBookbinding.AutoCompleteCustomSource.Clear();
                cbBookbinding.AutoCompleteCustomSource.Add(Lng.Get("epub"));
                cbBookbinding.AutoCompleteCustomSource.Add(Lng.Get("mobi"));
                cbBookbinding.AutoCompleteCustomSource.Add(Lng.Get("pdf"));
                cbBookbinding.AutoCompleteCustomSource.Add(Lng.Get("docx"));
                cbBookbinding.AutoCompleteCustomSource.Add(Lng.Get("xslx"));
            }
            else
            {
                cbBookbinding.Items.Clear();
                cbBookbinding.Items.Add("");
                cbBookbinding.Items.Add(Lng.Get("Notebook"));
                cbBookbinding.Items.Add(Lng.Get("Soft"));
                cbBookbinding.Items.Add(Lng.Get("Hard"));
                cbBookbinding.Items.Add(Lng.Get("Ring"));
                cbBookbinding.SelectedIndex = 0;

                // ----- Prepare autocomplete -----
                cbBookbinding.AutoCompleteCustomSource.Clear();
                cbBookbinding.AutoCompleteCustomSource.Add(Lng.Get("Notebook"));
                cbBookbinding.AutoCompleteCustomSource.Add(Lng.Get("Soft"));
                cbBookbinding.AutoCompleteCustomSource.Add(Lng.Get("Hard"));
                cbBookbinding.AutoCompleteCustomSource.Add(Lng.Get("Ring"));
            }
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

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditBooks_Load(object sender, EventArgs e)
        {
            com.ReceivedData += new ReceivedEventHandler(DataReceive);
            try
            {
                com.ConnectSP(Properties.Settings.Default.scanCOM);
            }
            catch { }

            // ----- Prepare type -----
            cbType.Items.Clear();
            cbType.Items.Add("");
            cbType.Items.Add(Lng.Get("EBook", "E-Book"));
            cbType.Items.Add(Lng.Get("Book"));
            cbType.Items.Add(Lng.Get("Magazine"));
            cbType.Items.Add(Lng.Get("Newspaper"));
            cbType.Items.Add(Lng.Get("Document"));
            cbType.Items.Add(Lng.Get("Map"));
            cbType.SelectedIndex = 0;

            // ----- Prepare bookbinding -----
            SetBookbinding();
            

            // ----- Add Specimen -----
            cbSpecimen.Items.Clear();
            cbSpecimen.Items.Add("1");
            cbSpecimen.SelectedIndex = 0;
            InvNumbers.Add("");
            Locations.Add("");

            // ----- New Inv Number -----
            TempMaxInvNum++;
            txtInvNum.Text = Properties.Settings.Default.BookPrefix + (TempMaxInvNum).ToString("D" + Properties.Settings.Default.BookMinCharLen.ToString()) + Properties.Settings.Default.BookSuffix;


            if (ID != Guid.Empty)
            {

                Books book = db.Books.Find(ID);

                // ----- Cover -----
                imgCover.Image = Conv.ByteArrayToImage(book.Cover);

                // ----- Book -----
                txtName.Text = book.Title.Trim();
                cbType.Text = book.Type.Trim();
                txtAuthor.Text = book.AuthorName.Trim();
                txtAuthorSurname.Text = book.AuthorSurname.Trim();
                cbBookbinding.Text = book.Bookbinding.Trim();
                txtIllustrator.Text = book.Illustrator.Trim();
                txtTranslator.Text = book.Translator.Trim();
                txtLanguage.Text = book.Language.Trim();
                txtISBN.Text = book.ISBN.Trim();
                txtPublisher.Text = book.Publisher.Trim();
                txtEdition.Text = book.Edition.Trim();
                txtYear.Text = book.Year.ToString();
                txtPages.Text = book.Pages.ToString();
                txtHero.Text = book.MainCharacter.Trim();
                txtURL.Text = book.URL.Trim();
                txtNote.Text = book.Note.Trim();
                txtContent.Text = book.Content.Trim();


                // ----- Original book -----
                txtOrigName.Text = book.OrigName.Trim();
                txtOrigLang.Text = book.OrigLanguage.Trim();
                txtOrigYear.Text = book.OrigYear.ToString();


                // ----- Inclusion -----
                txtGenre.Text = book.Genre.Trim();
                txtSubGenre.Text = book.SubGenre.Trim();
                txtSeries.Text = book.Series.Trim();
                txtSNumber.Text = book.SeriesNum.ToString();
                txtKeywords.Text = book.Keywords.Trim();


                // ----- Rating -----
                txtRating.Text = book.Rating.ToString();
                txtMyRating.Text = book.MyRating.ToString();
                chbReaded.Checked = book.Readed ?? false;

               // dtAcqDate.Value = book.AcquisitionDate ?? DateTime.Now;
                //txtPrice.Text = book.Price.ToString();

                // ----- Fill Specimen -----
                ItemCount = (int)(book.Count ?? 1);                         // Get counts
                if (ItemCount > 1) btnDelSpecimen.Enabled = true;
                if ((book.Available ?? ItemCount) < ItemCount)
                    IsUsed = true;

                cbSpecimen.Items.Clear();
                InvNumbers.Clear();
                Locations.Clear();

                //string[] invNums = book.InventoryNumber.Trim().Split(new string[] { ";" }, StringSplitOptions.None);
               // string[] locs = book.Location.Trim().Split(new string[] { ";" }, StringSplitOptions.None);

                /*for (int i = 0; i < ItemCount; i++)                 // Fill specimen
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
                }*/
                txtInvNum.Text = InvNumbers[0];                     // Inventory number
                txtLocation.Text = Locations[0];                    // Location
                cbSpecimen.SelectedIndex = 0;
                
                lblCount.Text = "/ " + ItemCount.ToString();        // Counts

                // ----- Fast tags -----
                FastFlags flag = (FastFlags)(book.FastTags ?? 0);
                if (flag.HasFlag(FastFlags.FLAG1)) btnTag1.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG2)) btnTag2.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG3)) btnTag3.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG4)) btnTag4.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG5)) btnTag5.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG6)) btnTag6.BackColor = SelectColor;


                // ----- Update -----
                lblUpdated.Text = Lng.Get("LastUpdate", "Last update") + ": " + (book.Updated ?? DateTime.Now).ToShortDateString();
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
            /*var list = db.Books.Where(x => x.ID != ID).Select(x => x.InventoryNumber).ToList();

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
            }*/
            return false;

        }
        /// <summary>
        /// Fill Book values
        /// </summary>
        /// <param name="book"></param>
        private void FillBook(ref Books book)
        {
            // ----- Cover -----
            byte[] bytes = Conv.ImageToByteArray(imgCover.Image);
            if (bytes != null) book.Cover = bytes;

            // ----- Book -----
            book.Title = txtName.Text;
            book.Type = cbType.Text;
            book.AuthorName = txtAuthor.Text;
            book.AuthorSurname = txtAuthorSurname.Text;
            book.Bookbinding = cbBookbinding.Text;
            book.Illustrator = txtIllustrator.Text;
            book.Translator = txtTranslator.Text;
            book.Language = txtLanguage.Text;
            book.ISBN = txtISBN.Text;
            book.Publisher = txtPublisher.Text;
            book.Edition = txtEdition.Text;
            book.Year = Conv.ToShortNull(txtYear.Text);
            book.Pages = Conv.ToShortNull(txtPages.Text);
            book.MainCharacter = txtHero.Text;
            book.URL = txtURL.Text;
            book.Note = txtNote.Text;
            book.Content = txtContent.Text;


            // ----- Original book -----
            book.OrigName = txtOrigName.Text;
            book.OrigLanguage = txtOrigLang.Text;
            book.OrigYear = Conv.ToShortNull(txtOrigYear.Text);


            // ----- Inclusion -----
            book.Genre = txtGenre.Text;
            book.SubGenre = txtSubGenre.Text;
            book.Series = txtSeries.Text;
            book.SeriesNum = Conv.ToIntNull(txtSNumber.Text);
            book.Keywords = txtKeywords.Text;


            // ----- Rating -----
            book.Rating = Conv.ToShortNull(txtRating.Text);
            book.MyRating = Conv.ToShortNull(txtMyRating.Text);
            book.Readed = chbReaded.Checked;

            //book.AcquisitionDate = dtAcqDate.Value;
            //book.Price = Conv.ToDoubleNull(txtPrice.Text);

            // ----- Fill Specimen -----
            book.Count = (short)ItemCount;                   // Get counts

            string invNums = "", locs = "";
            for (int i = 0; i < ItemCount; i++)             // Fill specimen
            {
                if (invNums != "") invNums += ";";
                invNums += InvNumbers[i];
                if (locs != "") locs += ";";
                locs += Locations[i];

                long maxNum = Conv.ToNumber(InvNumbers[i]);
                if (maxNum > MaxInvNumbers.Book) MaxInvNumbers.Book = maxNum;
            }
           /* book.InventoryNumber = invNums;
            book.Location = locs;
            if (Properties.Settings.Default.BookUseISBN)
                book.Barcode = Conv.ToNumber(InvNumbers[0]);
            else if (ItemCount == 1)
                book.Barcode = Conv.ToNumber(book.InventoryNumber);
            else
                book.Barcode = 0;*/

            // ----- Fast tags -----
            short fastTag = 0;
            if (btnTag1.BackColor == SelectColor) fastTag |= 0x01;
            if (btnTag2.BackColor == SelectColor) fastTag |= 0x02;
            if (btnTag3.BackColor == SelectColor) fastTag |= 0x04;
            if (btnTag4.BackColor == SelectColor) fastTag |= 0x08;
            if (btnTag5.BackColor == SelectColor) fastTag |= 0x10;
            if (btnTag6.BackColor == SelectColor) fastTag |= 0x20;
            book.FastTags = fastTag;
        }

        private void SaveItem()
        {
            Books book;

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
                book = db.Books.Find(ID);
            }
            else
            {
                book = new Books();
                book.ID = Guid.NewGuid();
            }

            // ----- Fill Book values -----
            FillBook(ref book);

            // ----- Update database -----
            if (ID == Guid.Empty) db.Books.Add(book);
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
            if (Properties.Settings.Default.BookUseISBN)
            {
                InvNum = txtISBN.Text;
            }
            else if (Properties.Settings.Default.IncSpecimenInv)
            {
                TempMaxInvNum++;
                InvNum = Properties.Settings.Default.BookPrefix + (TempMaxInvNum).ToString("D" + Properties.Settings.Default.BookMinCharLen.ToString()) + Properties.Settings.Default.BookSuffix;
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

        #endregion

        #region FastTags

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
        private void imgCover_Click(object sender, EventArgs e)
        {
            Image img = imgCover.Image;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Lng.Get("Images") + " |*.jpg;*.jpeg;*.jpe;*.tiff;*.png;*.gif";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imgCover.Load(dialog.FileName);
                }
                catch
                {
                    imgCover.Image = img;
                    img.Dispose();
                    Dialogs.ShowErr(Lng.Get("ErrLoadImg", "Image cannot load!"), Lng.Get("Error"));
                }
            }
        }


        #endregion

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetBookbinding();
        }

        private void brnGetDataISBN_Click(object sender, EventArgs e)
        {
            GetDataFromWeb();

        }

        private void GetDataFromWeb()
        {
            try
            {
                var connection = new Connection("aleph.nkp.cz", 9991)
                {
                    DatabaseName = "SKC-UTF", //"CNB-UTF",// "SKC-UTF", //"NKC-UTF",
                    Syntax = Zoom.Net.RecordSyntax.XML
                };
                connection.Connect();

                // Declare the query either in PQF or CQL according to whether the Z39.50 implementation at the endpoint support it.
                // The following query is in PQF format
                //var query = "@attr 1=4 krakatit";
                var query = "";
                if (txtISBN.Text != "")
                    query = "@attr 1=7 \"" + txtISBN.Text + "\"";
                else if (txtName.Text != "" && txtAuthorSurname.Text == "")
                    query = "@attr 1=4 @attr 3=3 \"" + global.RemoveDiacritics(txtName.Text) + "\" ";
                else if (txtName.Text == "" && global.RemoveDiacritics(txtAuthorSurname.Text) != "")
                    query = "@attr 1=1003 @attr 3=3 \"" + global.RemoveDiacritics(txtAuthorSurname.Text) + "\"";
                else if (txtName.Text != "" && txtAuthorSurname.Text != "")
                    query = "@and @attr 1=4 @attr 3=3 \"" + global.RemoveDiacritics(txtName.Text) + "\" @attr 1=1003 @attr 3=3 \"" + global.RemoveDiacritics(txtAuthorSurname.Text) + "\"";
                else
                {
                    Dialogs.ShowWar(Lng.Get("NoFindBookData", "No data (ISBN / Title) to booking search!"), Lng.Get("Warning"));
                    return;
                }
                var q = new PrefixQuery(query);

                // Get the search results in binary format
                // Note that each result is in XML format
                var results = connection.Search(q);

                if (results.Count > 0)
                {
                    var result = results[0];
                    string text = Encoding.UTF8.GetString(result.Content);
                    List<bookItem> list = XmlToBook(text);
                    Files.SaveFile("res.xml", text);

                    if (list.Count > 0)
                    {
                        bookItem itm = list[0];

                        // ----- Get Cober from "Obalkyknih.cz" -----
                        string urlAddress = "https://obalkyknih.cz/view?isbn=" + itm.ISBN.Replace("-", "");

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
                                            itm.coverPath = path;
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
                                        itm.Content = data.Substring(posStart, posStop - posStart).Trim();
                                    }
                                }
                            }
                            response.Close();
                            readStream.Close();


                        }

                        // ----- Get from databazeknih -----
                        urlAddress = "https://www.databazeknih.cz/search?q=" + itm.ISBN.Replace("-", "");

                        request = (HttpWebRequest)WebRequest.Create(urlAddress);
                        response = (HttpWebResponse)request.GetResponse();
                        itm.URL = response.ResponseUri.ToString();
                        request = (HttpWebRequest)WebRequest.Create(response.ResponseUri + "?show=binfo");
                        response = (HttpWebResponse)request.GetResponse();

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

                            int pos = data.IndexOf("'bpoints'");
                            if (pos >= 0)
                            {
                                int posStart = data.IndexOf(">", pos);
                                if (posStart >= 0)
                                {
                                    posStart += 1;
                                    int posStop = data.IndexOf("<", posStart);
                                    if (posStop >= 0)
                                    {
                                        itm.Rating = data.Substring(posStart, posStop - posStart - 1);
                                    }
                                }
                            }

                            pos = data.IndexOf("href='zanry");
                            while (pos >= 0)
                            {
                                int posStart = data.IndexOf(">", pos);
                                if (posStart >= 0)
                                {
                                    posStart += 1;
                                    int posStop = data.IndexOf("<", posStart);
                                    if (posStop >= 0)
                                    {
                                        if (itm.Genres != "") itm.Genres += ", ";
                                        itm.Genres += data.Substring(posStart, posStop - posStart);
                                        pos = data.IndexOf("href='zanry", posStop);
                                    }
                                }
                                else pos = -1;
                            }

                            pos = data.IndexOf("href='prekladatele");
                            if (pos >= 0)
                            {
                                int posStart = data.IndexOf(">", pos);
                                if (posStart >= 0)
                                {
                                    posStart += 1;
                                    int posStop = data.IndexOf("<", posStart);
                                    if (posStop >= 0)
                                    {
                                        itm.Translator = data.Substring(posStart, posStop - posStart);
                                    }
                                }
                            }

                            pos = data.IndexOf("href='serie");
                            if (pos >= 0)
                            {
                                int posStart = data.IndexOf(">", pos);
                                if (posStart >= 0)
                                {
                                    posStart += 1;
                                    int posStop = data.IndexOf("<", posStart);
                                    if (posStop >= 0)
                                    {
                                        itm.Series = data.Substring(posStart, posStop - posStart);

                                        pos = data.IndexOf("class='info'", posStop);
                                        if (pos >= 0)
                                        {
                                            posStart = data.IndexOf(">", pos);
                                            if (posStart >= 0)
                                            {
                                                posStart += 1;
                                                posStop = data.IndexOf("<", posStart);
                                                if (posStop >= 0)
                                                {
                                                    itm.SeriesNum = Conv.ToNumber(data.Substring(posStart, posStop - posStart)).ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            pos = data.IndexOf("Originální název:");
                            if (pos >= 0)
                            {
                                int posStart = data.IndexOf("<h4>", pos);
                                if (posStart >= 0)
                                {
                                    posStart += 4;
                                    int posStop = data.IndexOf("</h4>", posStart);
                                    if (posStop >= 0)
                                    {
                                        itm.OrigTitle = data.Substring(posStart, posStop - posStart);
                                        posStart = itm.OrigTitle.IndexOf("(");
                                        if (posStart >= 0)
                                        {
                                            posStart += 1;
                                            posStop = itm.OrigTitle.IndexOf(")", posStart);
                                            if (posStop >= 0)
                                            {
                                                itm.OrigYear = itm.OrigTitle.Substring(posStart, posStop - posStart);
                                                itm.OrigTitle = itm.OrigTitle.Substring(0, posStart - 1).Trim();
                                            }
                                        }
                                    }
                                }
                            }

                            response.Close();
                            readStream.Close();
                        }

                        FillByBookItems(itm);
                    }
                }
                else
                {
                    Dialogs.ShowWar(Lng.Get("BookNotFound", "Book not found!"), Lng.Get("Warning"));
                }
            }
            catch (Exception Err)
            {
                Dialogs.ShowErr(Lng.Get("ServerError", "Cannot connet to server!") + " (" + Err.Message + ")", Lng.Get("Warning"));
            }
        }

        private void FillByBookItems(bookItem itm)
        {
            txtName.Text = itm.Title;
            txtAuthor.Text = itm.AuthorName;
            txtAuthorSurname.Text = itm.AuthorSurname;
            txtISBN.Text = itm.ISBN;
            txtLanguage.Text = itm.language;
            txtPublisher.Text = itm.Publisher;
            txtYear.Text = itm.Year;
            txtPages.Text = itm.Pages;
            txtContent.Text = itm.Content;
            txtKeywords.Text = itm.Keywords;
            txtEdition.Text = itm.Publication;
            if (itm.coverPath != null || itm.coverPath != "")
                imgCover.Load(itm.coverPath);
            txtRating.Text = itm.Rating;
            txtTranslator.Text = itm.Translator;
            txtGenre.Text = itm.Genres;
            txtSeries.Text = itm.Series;
            txtSNumber.Text = itm.SeriesNum;
            txtOrigName.Text = itm.OrigTitle;
            txtOrigYear.Text = itm.OrigYear;
            txtURL.Text = itm.URL;
        }

        struct bookItem
        {
            public string language;
            public string ISBN;
            public string AuthorName;
            public string AuthorSurname;
            public string Title;
            public string Publication;
            public string Publisher;
            public string Year;
            public string Pages;
            public string Content;
            public string Keywords;
            public string coverPath;
            public string Rating;
            public string Genres;
            public string Translator;
            public string Series;
            public string SeriesNum;
            public string OrigTitle;
            public string OrigYear;
            public string URL;
        }

        List<bookItem> XmlToBook(string text)
        {
            List<bookItem> list = new List<bookItem>();
            

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(text);
            XmlNode node = xml.DocumentElement.SelectSingleNode("dc-record");
            while (node != null)
            {
                bookItem item = new bookItem();
                item.Genres = "";
                XmlNode childNode;

                // ----- Language -----
                childNode = node.SelectSingleNode("language");
                if (childNode != null)
                {
                    if (childNode.InnerText == "eng") item.language = Lng.Get("English");
                    else if (childNode.InnerText == "cze") item.language = Lng.Get("Czech");
                    else item.language = childNode.InnerText;
                }
                // ----- ISBN -----
                childNode = node.SelectSingleNode("identifier");
                if (childNode != null) item.ISBN = childNode.InnerText.Replace(";", "").Replace(":", "").Trim();
                // ----- Author -----
                childNode = node.SelectSingleNode("contributor");
                if (childNode != null)
                {
                    string[] split = childNode.InnerText.Split(new string[] { "," }, StringSplitOptions.None);
                    if (split.Length >= 1)
                        item.AuthorSurname = split[0].Trim();
                    if (split.Length >= 2)
                        item.AuthorName = split[1].Trim();
                }
                // ----- Title -----
                childNode = node.SelectSingleNode("title");
                if (childNode != null)
                {
                    item.Title = childNode.InnerText;
                    if (item.Title[item.Title.Length - 1] == '/') item.Title = item.Title.Substring(0, item.Title.Length - 1).Trim();
                }
                // ----- Publication -----
                childNode = node.SelectSingleNode("description");
                if (childNode != null)
                {
                    item.Publication = childNode.InnerText.Replace("vyd.", "").Trim();
                    if (item.Publication.ToLower().Contains("první")) item.Publication = "1.";
                    if (item.Publication.ToLower().Contains("druhé")) item.Publication = "2.";
                    if (item.Publication.ToLower().Contains("třetí")) item.Publication = "3.";
                    if (item.Publication.ToLower().Contains("čtvrté")) item.Publication = "4.";
                    if (item.Publication.ToLower().Contains("páté")) item.Publication = "5.";
                }
                // ----- Publisher -----
                childNode = node.SelectSingleNode("publisher");
                if (childNode != null)
                {
                    item.Publisher = childNode.InnerText;
                    if (item.Publisher[item.Publisher.Length - 1] == ',') item.Publisher = item.Publisher.Substring(0, item.Publisher.Length - 1).Trim();
                }
                // ----- Year -----
                childNode = node.SelectSingleNode("date");
                if (childNode != null) item.Year = Conv.ToNumber(childNode.InnerText).ToString();
                // ----- Pages -----
                childNode = node.SelectSingleNode("format");
                if (childNode != null) item.Pages = Conv.ToNumber(childNode.InnerText).ToString();
                // ----- Content -----
                childNode = childNode.NextSibling;
                //childNode = node.SelectSingleNode("description");
                if (childNode != null) item.Content = childNode.InnerText;
                // ----- Keywords -----
                childNode = node.SelectSingleNode("subject");
                if (childNode != null)
                {
                    item.Keywords = childNode.InnerText;
                    // ----- Keywords 2 -----
                    childNode = childNode.NextSibling;
                    if (item.Keywords != null && childNode != null) item.Keywords += ", " + childNode.InnerText;
                }
                
                list.Add(item);

                node = node.NextSibling;
            }
            

            return list;
        }

        private void txtISBN_TextChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.BookUseISBN)
            {
                txtInvNum.Text = txtISBN.Text;
            }
        }

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
            } else
            {
                txtISBN.Text = Barcode.Replace("\r", "").Replace("\n,", "");
                GetDataFromWeb();
                if (Properties.Settings.Default.BookUseISBN)
                    txtInvNum.Text = Barcode.Replace("\r", "").Replace("\n,", "");
            }
            Barcode = "";
        }

        #endregion

        private void frmEditBooks_FormClosing(object sender, FormClosingEventArgs e)
        {
            com.Close();
        }
    }
}
