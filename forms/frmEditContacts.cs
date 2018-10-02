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
using Communications;
using TCPClient;

namespace Katalog
{
    public partial class frmEditContacts : Form
    {
        public struct DataItem
        {
            public string name;
            public string tag;
        }

        #region Variables

        Color SelectColor = Color.SkyBlue;                  // FastTags Select color

        databaseEntities db = new databaseEntities();       // Database
        Guid ID = Guid.Empty;                               // Selected Item GUID (No Guid = new item)

        long TempMaxInvNum = MaxInvNumbers.Contact;             // Max Inv. Number

        Communication com = new Communication();
        public delegate void MyDelegate(comStatus status);
        string Barcode = "";

        List<DataItem> PhoneNums = new List<DataItem>();
        List<DataItem> Emails = new List<DataItem>();
        List<DataItem> URLs = new List<DataItem>();
        List<DataItem> IMs = new List<DataItem>();

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

        private List<DataItem> GetDataItem(string text)
        {
            List<DataItem> list = new List<DataItem>();
            string[] itemSplit = text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < itemSplit.Length; i++)
            {
                string[] tagSplit = itemSplit[i].Split(new string[] { "," }, StringSplitOptions.None);
                DataItem itm = new DataItem();
                itm.name = tagSplit[0];
                if (tagSplit.Length > 0)
                    itm.tag = tagSplit[1];
                list.Add(itm);
            }
            return list;
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditContacts_Load(object sender, EventArgs e)
        {
            com.ReceivedData += new ReceivedEventHandler(DataReceive);
            try
            {
                com.ConnectSP(Properties.Settings.Default.scanCOM);
            }
            catch { }

            // ----- Add Sex -----
            cbSex.Items.Clear();
            cbSex.Items.Add("");
            cbSex.Items.Add(Lng.Get("Male"));
            cbSex.Items.Add(Lng.Get("Female"));
            cbSex.SelectedIndex = 0;

            // ----- New Inv Number -----
            if (TempMaxInvNum < Properties.Settings.Default.ContactStart) TempMaxInvNum = Properties.Settings.Default.ContactStart;
            else TempMaxInvNum++;
            txtCode.Text = Properties.Settings.Default.ContactPrefix + (TempMaxInvNum).ToString("D" + Properties.Settings.Default.ContactMinCharLen.ToString()) + Properties.Settings.Default.ContactSuffix;

            // ----- If Edit -> fill form -----
            if (ID != Guid.Empty)
            {
                Contacts contact = db.Contacts.Find(ID);

                // ----- Avatar -----
                imgAvatar.Image = Conv.ByteArrayToImage(contact.Avatar);

                // ----- Name -----
                txtName.Text = contact.Name;
                txtSurname.Text = contact.Surname;
                txtNick.Text = contact.Nick;
                if (contact.Sex == "M") cbSex.SelectedIndex = 1;
                else if (contact.Sex == "F") cbSex.SelectedIndex = 2;
                else cbSex.SelectedIndex = 0;

                // ----- Contacts -----
                PhoneNums = GetDataItem(contact.Phone);
                Emails = GetDataItem(contact.Email);
                URLs = GetDataItem(contact.WWW);
                IMs = GetDataItem(contact.IM);

                //for (int i = 0; i < )
                cbPhone.Text = contact.Phone;
                cbEmail.Text = contact.Email;
                cbWWW.Text = contact.WWW;
                cbIM.Text = contact.IM;


                // ----- Address -----
                txtStreet.Text = contact.Street;
                txtCity.Text = contact.City;
                txtRegion.Text = contact.Region;
                txtState.Text = contact.Country;
                txtPostCode.Text = contact.PostCode;

                txtNote.Text = contact.Note;
                dateBirth.Value = contact.Birth ?? DateTime.Now;
                txtTag.Text = contact.Tags;
                //contact.FastTags = 0;


                txtCode.Text = contact.PersonCode;

                txtCompany.Text = contact.Company;
                txtPosition.Text = contact.Position;

                chbActive.Checked = contact.Active ?? true;

                // ----- Fast tags -----
                FastFlags flag = (FastFlags)(contact.FastTags ?? 0);
                if (flag.HasFlag(FastFlags.FLAG1)) btnTag1.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG2)) btnTag2.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG3)) btnTag3.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG4)) btnTag4.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG5)) btnTag5.BackColor = Color.SkyBlue;
                if (flag.HasFlag(FastFlags.FLAG6)) btnTag6.BackColor = Color.SkyBlue;

                // ----- Updated -----
                lblLastUpdate.Text = Lng.Get("LastUpdate", "Last update") + ": " + (contact.Updated ?? DateTime.Now).ToString();

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

            if (cbSex.SelectedIndex == 1) contact.Sex = "M";
            else if (cbSex.SelectedIndex == 2) contact.Sex = "F" ;
            else contact.Sex = "";

            // ----- Contacts -----
            //for (int i = 0; i < )
            contact.Phone = cbPhone.Text;

            contact.Email = cbEmail.Text;
            contact.WWW = cbWWW.Text;
            contact.IM = cbIM.Text;


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

            contact.PersonCode = txtCode.Text;
            contact.Barcode = Conv.ToNumber(txtCode.Text);
            contact.Updated = DateTime.Now;

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
                contact.ID = Guid.NewGuid();
            }

            FillContact(ref contact);

            if (ID == Guid.Empty) db.Contacts.Add(contact);
            db.SaveChanges();

            ID = contact.ID;
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


        #region Barcode


        private void DataReceive(object source, comStatus status)
        {
            txtCode.Invoke(new MyDelegate(updateLog), new Object[] { status }); //BeginInvoke

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
            if (txtCode.Focused)
            {
                txtCode.Text = Barcode.Replace("\r", "").Replace("\n,", "");
            }
            Barcode = "";
        }


        #endregion

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

        private void frmEditContacts_FormClosing(object sender, FormClosingEventArgs e)
        {
            com.Close();
        }
    }
}
