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

        #endregion

        #region Constructor

        public frmEditBooks()
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
            cbType.Items.Clear();
            cbType.Items.Add("");
            cbType.Items.Add(Lng.Get("EBook", "E-Book"));
            cbType.Items.Add(Lng.Get("Book"));
            cbType.Items.Add(Lng.Get("Magazine"));
            cbType.Items.Add(Lng.Get("Newspaper"));
            cbType.Items.Add(Lng.Get("Document"));
            cbType.Items.Add(Lng.Get("Map"));
            cbType.SelectedIndex = 0;

            cbBookbinding.Items.Clear();
            cbBookbinding.Items.Add("");
            cbBookbinding.Items.Add(Lng.Get("Notebook"));
            cbBookbinding.Items.Add(Lng.Get("Soft"));
            cbBookbinding.Items.Add(Lng.Get("Hard"));
            cbBookbinding.Items.Add(Lng.Get("Ring"));
            cbBookbinding.SelectedIndex = 0;

            cbSpecimen.Items.Clear();
            cbSpecimen.Items.Add("1");
            cbSpecimen.SelectedIndex = 0;

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

                // ----- Specimen -----
                txtInvNum.Text = book.InventoryNumber.Trim();
                txtLocation.Text = book.Location.Trim();
                dtAcqDate.Value = book.AcquisitionDate ?? DateTime.Now;
                txtPrice.Text = book.Price.ToString();

                // ----- Fast tags -----
                FastFlags flag = (FastFlags)(book.FastTags ?? 0);
                if (flag.HasFlag(FastFlags.FLAG1)) btnTag1.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG2)) btnTag2.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG3)) btnTag3.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG4)) btnTag4.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG5)) btnTag5.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG6)) btnTag6.BackColor = Color.SkyBlue;
            }
        }

        #endregion

        #region Form Close

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

            // ----- Specimen -----
            book.InventoryNumber = txtInvNum.Text;
            book.Location = txtLocation.Text;
            book.AcquisitionDate = dtAcqDate.Value;
            book.Price = Conv.ToDoubleNull(txtPrice.Text);

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

            // ----- ID -----
            if (ID != Guid.Empty)
            {
                book = db.Books.Find(ID);
            } else
            {
                book = new Books();
                book.Id = Guid.NewGuid();
            }

            FillBook(ref book);

            if (ID == Guid.Empty) db.Books.Add(book);
            db.SaveChanges();

            this.DialogResult = DialogResult.OK;
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
    }
}
