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
        public frmEditContacts()
        {
            InitializeComponent();
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();

            Contacts contact = new Contacts();

            // ----- ID -----
            contact.Id = Guid.NewGuid();

            // ----- Avatar -----
            contact.Avatar = ImageToByteArray(imgAvatar.Image);

            // ----- Name -----
            contact.Name = txtName.Text;
            contact.Surname = txtSurname.Text;
            contact.Nick = txtNick.Text;

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
            contact.sex = cbSex.Text;

            contact.code = txtCode.Text;
            contact.update = DateTime.Now;

            contact.Company = txtCompany.Text;
            contact.Position = txtPosition.Text;


            // ----- Unused now -----
            contact.Partner = "";
            contact.Childs = "";
            contact.Parrents = "";
            contact.GoogleID = "";




            db.Contacts.Add(contact);
            db.SaveChanges();

            this.DialogResult = DialogResult.OK;
        }

        private void frmEditContacts_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
