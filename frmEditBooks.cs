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

namespace Katalog
{
    public partial class frmEditBooks : Form
    {
        Guid ID = Guid.Empty;

        public frmEditBooks()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(Guid ID)
        {
            this.ID = ID;
            return base.ShowDialog();
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn != null)
            {
                using (var ms = new MemoryStream())
                {
                    imageIn.Save(ms, imageIn.RawFormat);
                    return ms.ToArray();
                }
            }
            else return new byte[0];
            
        }

        private void FillContact(ref Contacts contact)
        {
            // ----- Avatar -----
            contact.Avatar = ImageToByteArray(imgCover.Image);

            // ----- Name -----
            contact.Name = txtName.Text;
            contact.Surname = txtOrigName.Text;
            contact.Nick = txtAuthor.Text;

            if (cbType.SelectedIndex == 1) contact.sex = "M";
            else if (cbType.SelectedIndex == 2) contact.sex = "F" ;
            else contact.sex = "";

            // ----- Contacts -----
            //for (int i = 0; i < )
            contact.Phone = cbPhone.Text;

            contact.Email = cbEmail.Text;
            contact.WWW = cbWWW.Text;
            contact.IM = txtSeries.Text;


            // ----- Address -----
            contact.Street = txtStreet.Text;
            contact.City = txtCity.Text;
            contact.Region = txtRegion.Text;
            contact.Country = txtState.Text;
            contact.PostCode = txtPostCode.Text;

            contact.Note = txtContent.Text;
            contact.Birth = dtAcqDate.Value;
            contact.Tags = txtGenre.Text;
            //contact.FastTags = 0;

            contact.code = txtInvNum.Text;
            contact.update = DateTime.Now;

            contact.Company = txtHero.Text;
            contact.Position = txtISBN.Text;


            // ----- Unused now -----
            contact.Partner = "";
            contact.Childs = "";
            contact.Parrents = "";
            contact.GoogleID = "";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();

            Contacts contact;

            // ----- ID -----
            if (ID != Guid.Empty)
            {
                contact = db.Contacts.Find(ID);
            } else
            {
                contact = new Contacts();
                contact.Id = Guid.NewGuid();
            }

            FillContact(ref contact);

            if (ID == Guid.Empty) db.Contacts.Add(contact);
            db.SaveChanges();

            this.DialogResult = DialogResult.OK;
        }

        private void frmEditContacts_Load(object sender, EventArgs e)
        {
            cbType.Items.Clear();
            cbType.Items.Add("");
            cbType.Items.Add(Lng.Get("Male"));
            cbType.Items.Add(Lng.Get("Female"));
            cbType.SelectedIndex = 0;

            if (ID != Guid.Empty)
            {
                databaseEntities db = new databaseEntities();

                Contacts contact = db.Contacts.Find(ID);

                // ----- Avatar -----
                //contact.Avatar = ImageToByteArray(imgAvatar.Image);

                // ----- Name -----
                txtName.Text = contact.Name.Trim();
                txtOrigName.Text = contact.Surname.Trim();
                txtAuthor.Text = contact.Nick.Trim();
                if (contact.sex.Trim() == "M") cbType.SelectedIndex = 1;
                else if (contact.sex.Trim() == "F") cbType.SelectedIndex = 2;
                else cbType.SelectedIndex = 0;

                // ----- Contacts -----
                //for (int i = 0; i < )
                cbPhone.Text = contact.Phone.Trim();

                cbEmail.Text = contact.Email.Trim();
                cbWWW.Text = contact.WWW.Trim();
                txtSeries.Text = contact.IM.Trim();


                // ----- Address -----
                txtStreet.Text = contact.Street.Trim();
                txtCity.Text = contact.City.Trim();
                txtRegion.Text = contact.Region.Trim();
                txtState.Text = contact.Country.Trim();
                txtPostCode.Text = contact.PostCode.Trim();

                txtContent.Text = contact.Note.Trim();
                dtAcqDate.Value = contact.Birth ?? DateTime.Now;
                txtGenre.Text = contact.Tags.Trim();
                //contact.FastTags = 0;
                

                txtInvNum.Text = contact.code.Trim();

                txtHero.Text = contact.Company.Trim();
                txtISBN.Text = contact.Position.Trim();


                // ----- Unused now -----
                /*contact.Partner = "";
                contact.Childs = "";
                contact.Parrents = "";
                contact.GoogleID = "";*/
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmEditContacts_Load_1(object sender, EventArgs e)
        {

        }
    }
}
