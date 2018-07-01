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

using myFunctions;

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
                txtName.Text = book.Name.Trim();
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

                dtAcqDate.Value = book.AcquisitionDate ?? DateTime.Now;
                txtPrice.Text = book.Price.ToString();

                // ----- Fill Specimen -----
                ItemCount = (int)(book.Count ?? 1);                         // Get counts
                if (ItemCount > 1) btnDelSpecimen.Enabled = true;
                if ((book.Available ?? ItemCount) < ItemCount)
                    IsUsed = true;

                cbSpecimen.Items.Clear();
                InvNumbers.Clear();
                Locations.Clear();

                string[] invNums = book.InventoryNumber.Trim().Split(new string[] { ";" }, StringSplitOptions.None);
                string[] locs = book.Location.Trim().Split(new string[] { ";" }, StringSplitOptions.None);

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
                FastFlags flag = (FastFlags)(book.FastTags ?? 0);
                if (flag.HasFlag(FastFlags.FLAG1)) btnTag1.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG2)) btnTag2.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG3)) btnTag3.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG4)) btnTag4.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG5)) btnTag5.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG6)) btnTag6.BackColor = SelectColor;
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
            var list = db.Books.Where(x => x.Id != ID).Select(x => x.InventoryNumber).ToList();

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
        /// Fill Book values
        /// </summary>
        /// <param name="book"></param>
        private void FillBook(ref Books book)
        {
            // ----- Cover -----
            byte[] bytes = Conv.ImageToByteArray(imgCover.Image);
            if (bytes != null) book.Cover = bytes;

            // ----- Book -----
            book.Name = txtName.Text;
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

            book.AcquisitionDate = dtAcqDate.Value;
            book.Price = Conv.ToDoubleNull(txtPrice.Text);

            // ----- Fill Specimen -----
            book.Count = (short)ItemCount;                   // Get counts

            string invNums = "", locs = "";
            for (int i = 0; i < ItemCount; i++)             // Fill specimen
            {
                if (invNums != "") invNums += ";";
                invNums += InvNumbers[i];
                if (locs != "") locs += ";";
                locs += Locations[i];

                int maxNum = Conv.ToNumber(InvNumbers[i]);
                if (maxNum > MaxInvNumbers.Book) MaxInvNumbers.Book = maxNum;
            }
            book.InventoryNumber = invNums;
            book.Location = locs;


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

        /// <summary>
        /// Button OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
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
            } else
            {
                book = new Books();
                book.Id = Guid.NewGuid();
            }

            // ----- Fill Book values -----
            FillBook(ref book);

            // ----- Update database -----
            if (ID == Guid.Empty) db.Books.Add(book);
            db.SaveChanges();

            // ----- Exit -----
            this.DialogResult = DialogResult.OK;
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
    }
}
