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
    public partial class frmEditContacts : Form
    {
        #region Variables

        Color SelectColor = Color.SkyBlue;                  // FastTags Select color

        databaseEntities db = new databaseEntities();       // Database
        Guid ID = Guid.Empty;                               // Selected Item GUID (No Guid = new item)

        #endregion

        #region Constructor

        public frmEditContacts()
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
        /// ShowDialog - create new Contact, return ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(out Guid ID)
        {
            DialogResult res = base.ShowDialog();
            ID = this.ID;
            return res;
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditContacts_Load(object sender, EventArgs e)
        {
            // ----- Add Sex -----
            cbSex.Items.Clear();
            cbSex.Items.Add("");
            cbSex.Items.Add(Lng.Get("Male"));
            cbSex.Items.Add(Lng.Get("Female"));
            cbSex.SelectedIndex = 0;

            // ----- If Edit -> fill form -----
            if (ID != Guid.Empty)
            {
                Contacts contact = db.Contacts.Find(ID);

                // ----- Avatar -----
                imgAvatar.Image = Conv.ByteArrayToImage(contact.Avatar);

                // ----- Name -----
                txtName.Text = contact.Name.Trim();
                txtSurname.Text = contact.Surname.Trim();
                txtNick.Text = contact.Nick.Trim();
                if (contact.sex.Trim() == "M") cbSex.SelectedIndex = 1;
                else if (contact.sex.Trim() == "F") cbSex.SelectedIndex = 2;
                else cbSex.SelectedIndex = 0;

                // ----- Contacts -----
                //for (int i = 0; i < )
                cbPhone.Text = contact.Phone.Trim();

                cbEmail.Text = contact.Email.Trim();
                cbWWW.Text = contact.WWW.Trim();
                txtIM.Text = contact.IM.Trim();


                // ----- Address -----
                txtStreet.Text = contact.Street.Trim();
                txtCity.Text = contact.City.Trim();
                txtRegion.Text = contact.Region.Trim();
                txtState.Text = contact.Country.Trim();
                txtPostCode.Text = contact.PostCode.Trim();

                txtNote.Text = contact.Note.Trim();
                dateBirth.Value = contact.Birth ?? DateTime.Now;
                txtTag.Text = contact.Tags.Trim();
                //contact.FastTags = 0;


                txtCode.Text = contact.code.Trim();

                txtCompany.Text = contact.Company.Trim();
                txtPosition.Text = contact.Position.Trim();

                chbActive.Checked = contact.Active ?? true;

                // ----- Fast tags -----
                FastFlags flag = (FastFlags)(contact.FastTags ?? 0);
                if (flag.HasFlag(FastFlags.FLAG1)) btnTag1.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG2)) btnTag2.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG3)) btnTag3.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG4)) btnTag4.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG5)) btnTag5.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG6)) btnTag6.BackColor = Color.SkyBlue;

                // ----- Unused now -----
                /*contact.Partner = "";
                contact.Childs = "";
                contact.Parrents = "";
                contact.GoogleID = "";*/
            }
        }

        #endregion

        #region Form Close

        /// <summary>
        /// Fill Contact values
        /// </summary>
        /// <param name="contact">Contact</param>
        private void FillContact(ref Contacts contact)
        {
            // ----- Avatar -----
            contact.Avatar = Conv.ImageToByteArray(imgAvatar.Image);

            // ----- Name -----
            contact.Name = txtName.Text;
            contact.Surname = txtSurname.Text;
            contact.Nick = txtNick.Text;

            if (cbSex.SelectedIndex == 1) contact.sex = "M";
            else if (cbSex.SelectedIndex == 2) contact.sex = "F" ;
            else contact.sex = "";

            // ----- Contacts -----
            //for (int i = 0; i < )
            contact.Phone = cbPhone.Text;

            contact.Email = cbEmail.Text;
            contact.WWW = cbWWW.Text;
            contact.IM = txtIM.Text;


            // ----- Address -----
            contact.Street = txtStreet.Text;
            contact.City = txtCity.Text;
            contact.Region = txtRegion.Text;
            contact.Country = txtState.Text;
            contact.PostCode = txtPostCode.Text;

            contact.Note = txtNote.Text;
            contact.Birth = dateBirth.Value;
            contact.Tags = txtTag.Text;
            //contact.FastTags = 0;

            contact.code = txtCode.Text;
            contact.update = DateTime.Now;

            contact.Company = txtCompany.Text;
            contact.Position = txtPosition.Text;

            contact.Active = chbActive.Checked;

            // ----- Fast tags -----
            short fastTag = 0;
            if (btnTag1.BackColor == SelectColor) fastTag |= 0x01;
            if (btnTag2.BackColor == SelectColor) fastTag |= 0x02;
            if (btnTag3.BackColor == SelectColor) fastTag |= 0x04;
            if (btnTag4.BackColor == SelectColor) fastTag |= 0x08;
            if (btnTag5.BackColor == SelectColor) fastTag |= 0x10;
            if (btnTag6.BackColor == SelectColor) fastTag |= 0x20;
            contact.FastTags = fastTag;

            // ----- Unused now -----
            contact.Partner = "";
            contact.Childs = "";
            contact.Parrents = "";
            contact.GoogleID = "";
        }

        private void SaveItem()
        {
            Contacts contact;

            // ----- ID -----
            if (ID != Guid.Empty)
            {
                contact = db.Contacts.Find(ID);
            }
            else
            {
                contact = new Contacts();
                contact.Id = Guid.NewGuid();
            }

            FillContact(ref contact);

            if (ID == Guid.Empty) db.Contacts.Add(contact);
            db.SaveChanges();

            ID = contact.Id;
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

        #region Fast Tags

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
        private void imgAvatar_Click(object sender, EventArgs e)
        {
            Image img = imgAvatar.Image;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Lng.Get("Images") + " |*.jpg;*.jpeg;*.jpe;*.tiff;*.png;*.gif";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imgAvatar.Load(dialog.FileName);
                }
                catch
                {
                    imgAvatar.Image = img;
                    img.Dispose();
                    Dialogs.ShowErr(Lng.Get("ErrLoadImg", "Image cannot load!"), Lng.Get("Error"));
                }
            }
        }

        #endregion

    }
}
