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
    public partial class frmEditContacts : Form
    {
        Guid ID = Guid.Empty;

        public frmEditContacts()
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
            contact.Avatar = ImageToByteArray(imgAvatar.Image);

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
            cbSex.Items.Clear();
            cbSex.Items.Add("");
            cbSex.Items.Add(Lng.Get("Male"));
            cbSex.Items.Add(Lng.Get("Female"));
            cbSex.SelectedIndex = 0;

            if (ID != Guid.Empty)
            {
                databaseEntities db = new databaseEntities();

                Contacts contact = db.Contacts.Find(ID);

                // ----- Avatar -----
                //contact.Avatar = ImageToByteArray(imgAvatar.Image);

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
    }
}
