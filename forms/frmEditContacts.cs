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
        /// <summary>
        /// Structure data with tags
        /// </summary>
        public struct DataItem
        {
            public string name;     // Item name
            public string tag;      // item tag

            /// <summary>
            /// Constructor with Item name
            /// </summary>
            /// <param name="Name">Item name</param>
            public DataItem(string Name)
            {
                name = Name;
                tag = "";
            }

            /// <summary>
            /// Constructor with Item name & Tag
            /// </summary>
            /// <param name="Name"></param>
            /// <param name="Tag"></param>
            public DataItem(string Name, string Tag)
            {
                name = Name;
                tag = Tag;
            }
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

        int PhoneIndex = -1;
        int EmailIndex = -1;
        int URLIndex = -1;
        int IMIndex = -1;

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
        /// Convert text to DataItem list
        /// </summary>
        /// <param name="text">Input text from DB</param>
        /// <returns>Returns DataItem list</returns>
        private List<DataItem> GetDataItem(string text)
        {
            // ----- Split items -----
            List<DataItem> list = new List<DataItem>();
            string[] itemSplit = text.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < itemSplit.Length; i++)
            {
                // ----- Split name & tag -----
                string[] tagSplit = itemSplit[i].Split(new string[] { "," }, StringSplitOptions.None);
                DataItem itm = new DataItem();
                itm.name = tagSplit[0];

                 // ----- Convert Special tags -----
                if (tagSplit.Length > 1)
                {
                    if (tagSplit[1] == "#home") itm.tag = Lng.Get("tagHome", "Home");
                    else if (tagSplit[1] == "#work") itm.tag = Lng.Get("tagWork", "Work");
                    else if (tagSplit[1] == "#main") itm.tag = Lng.Get("tagMain", "Main");
                    else if (tagSplit[1] == "#mobile") itm.tag = Lng.Get("tagMobile", "Mobile");
                    else if (tagSplit[1] == "#skype") itm.tag = Lng.Get("tagSkype", "Skype");
                    else if (tagSplit[1] == "#facebook") itm.tag = Lng.Get("tagFacebook", "Facebook");
                    else
                        itm.tag = tagSplit[1];
                }
                    
                // ----- Add item to list -----
                list.Add(itm);
            }
            return list;
        }

        /// <summary>
        /// Convert DataItem list to DB text
        /// </summary>
        /// <param name="list">DataItem list</param>
        /// <returns>Retruns DB text</returns>
        private string GetTextFromDataItem(List<DataItem> list)
        {
            string text = "";
            for (int i = 0; i < list.Count; i++)
            {
                // ----- Convert special tags -----
                string tag;
                if (list[i].tag == Lng.Get("tagHome", "Home")) tag = "#home";
                else if (list[i].tag == Lng.Get("tagWork", "Work")) tag = "#work";
                else if (list[i].tag == Lng.Get("tagMain", "Main")) tag = "#main";
                else if (list[i].tag == Lng.Get("tagMobile", "Mobile")) tag = "#mobile";
                else if (list[i].tag == Lng.Get("tagSkype", "Skype")) tag = "#skype";
                else if (list[i].tag == Lng.Get("tagFacebook", "Facebook")) tag = "#facebook";
                else
                    tag = list[i].tag;

                // ----- Fill text -----
                if (text != "") text += ";";
                text += list[i].name + "," + tag;
            }

            // ----- Return text -----
            return text;
        }

        #region Prepare Combobox

        /// <summary>
        /// Prepare Sex ComboBox
        /// </summary>
        private void PrepareCbSex()
        {
            // ----- Add Sex -----
            cbSex.Items.Clear();
            cbSex.Items.Add("");
            cbSex.Items.Add(Lng.Get("Male"));
            cbSex.Items.Add(Lng.Get("Female"));
            cbSex.SelectedIndex = 0;
        }

        /// <summary>
        /// Prepare Phone ComboBoxes
        /// </summary>
        private void PreparePhone()
        {
            // ----- Add tags -----
            cbPhoneTag.Items.Clear();
            cbPhoneTag.Items.Add(Lng.Get("tagHome", "Home"));
            cbPhoneTag.Items.Add(Lng.Get("tagWork", "Work"));
            cbPhoneTag.Items.Add(Lng.Get("tagMobile", "Mobile"));
            cbPhoneTag.Items.Add(Lng.Get("tagMain", "Main"));

            // ----- Add Autocomplete tags -----
            cbPhoneTag.AutoCompleteCustomSource.Clear();
            cbPhoneTag.AutoCompleteCustomSource.Add(Lng.Get("tagHome", "Home"));
            cbPhoneTag.AutoCompleteCustomSource.Add(Lng.Get("tagWork", "Work"));
            cbPhoneTag.AutoCompleteCustomSource.Add(Lng.Get("tagMobile", "Mobile"));
            cbPhoneTag.AutoCompleteCustomSource.Add(Lng.Get("tagMain", "Main"));

            // ----- Create Empty phone -----
            CreatePhone();
        }

        /// <summary>
        /// Prepare Email ComboBoxes
        /// </summary>
        private void PrepareEmail()
        {
            // ----- Add tags -----
            cbEmailTag.Items.Clear();
            cbEmailTag.Items.Add(Lng.Get("tagHome", "Home"));
            cbEmailTag.Items.Add(Lng.Get("tagWork", "Work"));
            cbEmailTag.Items.Add(Lng.Get("tagMain", "Main"));

            // ----- Add Autocomplete tags -----
            cbEmailTag.AutoCompleteCustomSource.Clear();
            cbEmailTag.AutoCompleteCustomSource.Add(Lng.Get("tagHome", "Home"));
            cbEmailTag.AutoCompleteCustomSource.Add(Lng.Get("tagWork", "Work"));
            cbEmailTag.AutoCompleteCustomSource.Add(Lng.Get("tagMain", "Main"));

            // ----- Create empty email item -----
            CreateEmail();
        }

        /// <summary>
        /// Prepare URL ComboBoxes
        /// </summary>
        private void PrepareURL()
        {
            // ----- Add tags -----
            cbURLTag.Items.Clear();
            cbURLTag.Items.Add(Lng.Get("tagHome", "Home"));
            cbURLTag.Items.Add(Lng.Get("tagWork", "Work"));

            // ----- Add Autocomplete tags -----
            cbURLTag.AutoCompleteCustomSource.Clear();
            cbURLTag.AutoCompleteCustomSource.Add(Lng.Get("tagHome", "Home"));
            cbURLTag.AutoCompleteCustomSource.Add(Lng.Get("tagWork", "Work"));

            // ----- Create empty URL item -----
            CreateURL();
        }

        /// <summary>
        /// Prepare IM ComboBoxes
        /// </summary>
        private void PrepareIM()
        {
            // ----- Add tags -----
            cbIMTag.Items.Clear();
            cbIMTag.Items.Add(Lng.Get("tagSkype", "Skype"));
            cbIMTag.Items.Add(Lng.Get("tagFacebook", "Facebook"));

            // ----- Add Autocomplete tags -----
            cbIMTag.AutoCompleteCustomSource.Clear();
            cbIMTag.AutoCompleteCustomSource.Add(Lng.Get("tagSkype", "Skype"));
            cbIMTag.AutoCompleteCustomSource.Add(Lng.Get("tagFacebook", "Facebook"));

            // ----- Create empty IM item -----
            CreateIM();
        }

        #endregion

        /// <summary>
        /// Create empty phone item
        /// </summary>
        private void CreatePhone()
        {
            // ----- Create First PhoneNum item -----
            PhoneNums.Clear();
            DataItem itm = new DataItem("");
            PhoneNums.Add(itm);

            cbPhone.Items.Clear();
            cbPhone.Items.Add(itm.name);

            // ----- Select PhoneNum item -----
            SelectPhone(0);
        }

        /// <summary>
        /// Create empty email item
        /// </summary>
        private void CreateEmail()
        {
            // ----- Create First PhoneNum item -----
            Emails.Clear();
            DataItem itm = new DataItem("");
            Emails.Add(itm);

            cbEmail.Items.Clear();
            cbEmail.Items.Add(itm.name);

            // ----- Select PhoneNum item -----
            SelectPhone(0);
        }

        /// <summary>
        /// Create empty URL item
        /// </summary>
        private void CreateURL()
        {
            // ----- Create First URL item -----
            URLs.Clear();
            DataItem itm = new DataItem("");
            URLs.Add(itm);

            cbURL.Items.Clear();
            cbURL.Items.Add(itm.name);

            // ----- Select URL item -----
            SelectURL(0);
        }

        /// <summary>
        /// Create empty IM item
        /// </summary>
        private void CreateIM()
        {
            // ----- Create First IM item -----
            IMs.Clear();
            DataItem itm = new DataItem("");
            IMs.Add(itm);

            cbIM.Items.Clear();
            cbIM.Items.Add(itm.name);

            // ----- Select IM item -----
            SelectIM(0);
        }

        /// <summary>
        /// Select Phone number
        /// </summary>
        /// <param name="index">Phone number index</param>
        private void SelectPhone(int index)
        {
            // ----- Check if index in range -----
            if (index >= PhoneNums.Count) index = 0;
            if (index < 0) index = 0;

            // ----- If selected index OK -----
            if (index < PhoneNums.Count)
            {
                PhoneIndex = index;
                cbPhone.Text = PhoneNums[index].name;       // Fill Phone comboBox
                cbPhoneTag.Text = PhoneNums[index].tag;
            }
            // ----- No selected item
            else
            {
                PhoneIndex = -1;
            }
        }

        /// <summary>
        /// Select Email
        /// </summary>
        /// <param name="index">Email index</param>
        private void SelectEmail(int index)
        {
            // ----- Check if index in range -----
            if (index >= Emails.Count) index = 0;
            if (index < 0) index = 0;

            // ----- If selected index OK -----
            if (index < Emails.Count)
            {
                EmailIndex = index;
                cbEmail.Text = Emails[index].name;       // Fill Emails comboBox
                cbEmailTag.Text = Emails[index].tag;
            }
            // ----- No selected item
            else
            {
                EmailIndex = -1;
            }
        }

        /// <summary>
        /// Select URL
        /// </summary>
        /// <param name="index">WWW index</param>
        private void SelectURL(int index)
        {
            // ----- Check if index in range -----
            if (index >= URLs.Count) index = 0;
            if (index < 0) index = 0;

            // ----- If selected index OK -----
            if (index < URLs.Count)
            {
                URLIndex = index;
                cbURL.Text = URLs[index].name;       // Fill URL comboBox
                cbURLTag.Text = URLs[index].tag;
            }
            // ----- No selected item
            else
            {
                URLIndex = -1;
            }
        }

        /// <summary>
        /// Select Instant Messenger
        /// </summary>
        /// <param name="index">IM index</param>
        private void SelectIM(int index)
        {
            // ----- Check if index in range -----
            if (index >= IMs.Count) index = 0;
            if (index < 0) index = 0;

            // ----- If selected index OK -----
            if (index < IMs.Count)
            {
                IMIndex = index;
                cbIM.Text = IMs[index].name;       // Fill IM comboBox
                cbIMTag.Text = IMs[index].tag;
            }
            // ----- No selected item
            else
            {
                IMIndex = -1;
            }

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

            PrepareCbSex();

            PreparePhone();
            PrepareEmail();
            PrepareURL();
            PrepareIM();

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

                // ----- Phone -----
                PhoneNums = GetDataItem(contact.Phone);
                if (PhoneNums.Count > 0)
                {
                    cbPhone.Items.Clear();
                    for (int i = 0; i < PhoneNums.Count; i++)
                    {
                        cbPhone.Items.Add(PhoneNums[i].name);
                    }
                    if (PhoneNums.Count > 1) btnDelPhone.Enabled = true;
                    SelectPhone(0);
                }
                else
                {
                    CreatePhone();
                }
                

                // ----- Email -----
                Emails = GetDataItem(contact.Email);
                if (Emails.Count > 0)
                {
                    cbEmail.Items.Clear();
                    for (int i = 0; i < Emails.Count; i++)
                    {
                        cbEmail.Items.Add(Emails[i].name);
                    }
                    if (Emails.Count > 1) btnDelEmail.Enabled = true;
                    SelectEmail(0);
                }
                else
                {
                    CreateEmail();
                }

                // ----- URL -----
                URLs = GetDataItem(contact.WWW);
                if (URLs.Count > 0)
                {
                    cbURL.Items.Clear();
                    for (int i = 0; i < URLs.Count; i++)
                    {
                        cbURL.Items.Add(URLs[i].name);
                    }
                    if (URLs.Count > 1) btnDelURL.Enabled = true;
                    SelectURL(0);
                }
                else
                {
                    CreateURL();
                }

                // ----- IM -----
                IMs = GetDataItem(contact.IM);
                if (IMs.Count > 0)
                {
                    cbIM.Items.Clear();
                    for (int i = 0; i < IMs.Count; i++)
                    {
                        cbIM.Items.Add(IMs[i].name);
                    }
                    if (IMs.Count > 1) btnDelIM.Enabled = true;
                    SelectIM(0);
                }
                else
                {
                    CreateIM();
                }

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
                lblGoogleID.Text = "Google ID: " + contact.GoogleID;

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
            contact.Phone = GetTextFromDataItem(PhoneNums);
            contact.Email = GetTextFromDataItem(Emails);
            contact.WWW = GetTextFromDataItem(URLs);
            contact.IM = GetTextFromDataItem(IMs);


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
            if (contact.Partner == null) contact.Partner = "";
            if (contact.Childs == null) contact.Childs = "";
            if (contact.Parrents == null) contact.Parrents = "";
            if (contact.GoogleID == null) contact.GoogleID = "";
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

        private void frmEditContacts_FormClosing(object sender, FormClosingEventArgs e)
        {
            com.Close();
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

        #region Phone Items

        /// <summary>
        /// Button Add Phone number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPhone_Click(object sender, EventArgs e)
        {
            DataItem itm = new DataItem("");
            PhoneNums.Add(itm);
            cbPhone.Items.Add("");
            SelectPhone(PhoneNums.Count - 1);
            btnDelPhone.Enabled = true;
            cbPhone.Focus();
        }

        /// <summary>
        /// Button Delete Phone number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelPhone_Click(object sender, EventArgs e)
        {
            if (PhoneIndex >= 0 && PhoneNums.Count > 1)
            {
                PhoneNums.RemoveAt(PhoneIndex);
                cbPhone.Items.RemoveAt(PhoneIndex);
                SelectPhone(0);

                if (PhoneNums.Count == 1) btnDelPhone.Enabled = false;
            }
        }
        
        /// <summary>
        /// Refresh Phone
        /// </summary>
        private void RefreshPhone()
        {
            if (PhoneIndex >= 0)
            {
                DataItem itm = PhoneNums[PhoneIndex];
                itm.name = cbPhone.Text;
                PhoneNums.RemoveAt(PhoneIndex);
                PhoneNums.Insert(PhoneIndex, itm);
                cbPhone.Items.RemoveAt(PhoneIndex);
                cbPhone.Items.Insert(PhoneIndex, itm.name);
                cbPhone.Text = itm.name;
            }
        }

        /// <summary>
        /// Phone ComboBox Selected Index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPhone_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectPhone(cbPhone.SelectedIndex);
        }

        /// <summary>
        /// Phone Key Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RefreshPhone();
            }
        }

        /// <summary>
        /// Phone Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPhone_Leave(object sender, EventArgs e)
        {
            RefreshPhone();
        }

        /// <summary>
        /// Phone Tag Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPhoneTag_Leave(object sender, EventArgs e)
        {
            if (PhoneIndex >= 0)
            {
                DataItem itm = PhoneNums[PhoneIndex];
                itm.tag = cbPhoneTag.Text;
                PhoneNums.RemoveAt(PhoneIndex);
                PhoneNums.Insert(PhoneIndex, itm);
            }
        }

        #endregion

        #region Emails 

        /// <summary>
        /// Button Add Email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEmail_Click(object sender, EventArgs e)
        {
            DataItem itm = new DataItem("");
            Emails.Add(itm);
            cbEmail.Items.Add("");
            SelectEmail(Emails.Count - 1);
            btnDelEmail.Enabled = true;
            cbEmail.Focus();
        }

        /// <summary>
        /// Button Delete Email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelEmail_Click(object sender, EventArgs e)
        {
            if (EmailIndex >= 0 && Emails.Count > 1)
            {
                Emails.RemoveAt(EmailIndex);
                cbEmail.Items.RemoveAt(EmailIndex);
                SelectEmail(0);

                if (Emails.Count == 1) btnDelEmail.Enabled = false;
            }
        }

        /// <summary>
        /// Refresh Email
        /// </summary>
        private void RefreshEmail()
        {
            if (EmailIndex >= 0)
            {
                DataItem itm = Emails[EmailIndex];
                itm.name = cbEmail.Text;
                Emails.RemoveAt(EmailIndex);
                Emails.Insert(EmailIndex, itm);
                cbEmail.Items.RemoveAt(EmailIndex);
                cbEmail.Items.Insert(EmailIndex, itm.name);
                cbEmail.Text = itm.name;
            }
        }

        /// <summary>
        /// Email ComboBox Selected Index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectEmail(cbEmail.SelectedIndex);
        }

        /// <summary>
        /// Email Key Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RefreshEmail();
            }
        }

        /// <summary>
        /// Email Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbEmail_Leave(object sender, EventArgs e)
        {
            RefreshEmail();
        }

        /// <summary>
        /// Email Tag Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbEmailTag_Leave(object sender, EventArgs e)
        {
            if (EmailIndex >= 0)
            {
                DataItem itm = Emails[EmailIndex];
                itm.tag = cbEmailTag.Text;
                Emails.RemoveAt(EmailIndex);
                Emails.Insert(EmailIndex, itm);
            }
        }

        #endregion

        #region URLs

        /// <summary>
        /// Button Add URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddURL_Click(object sender, EventArgs e)
        {
            DataItem itm = new DataItem("");
            URLs.Add(itm);
            cbURL.Items.Add("");
            SelectURL(URLs.Count - 1);
            btnDelURL.Enabled = true;
            cbURL.Focus();
        }

        /// <summary>
        /// Button Delete URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelURL_Click(object sender, EventArgs e)
        {
            if (URLIndex >= 0 && URLs.Count > 1)
            {
                URLs.RemoveAt(URLIndex);
                cbURL.Items.RemoveAt(URLIndex);
                SelectURL(0);

                if (URLs.Count == 1) btnDelURL.Enabled = false;
            }
        }
        
        /// <summary>
        /// Refresh URL
        /// </summary>
        private void RefreshURL()
        {
            if (URLIndex >= 0)
            {
                DataItem itm = URLs[URLIndex];
                itm.name = cbURL.Text;
                URLs.RemoveAt(URLIndex);
                URLs.Insert(URLIndex, itm);
                cbURL.Items.RemoveAt(URLIndex);
                cbURL.Items.Insert(URLIndex, itm.name);
                cbURL.Text = itm.name;
            }
        }
        
        /// <summary>
        /// URL ComboBox Selected Index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbURL_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectURL(cbURL.SelectedIndex);
        }

        /// <summary>
        /// URL Key Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RefreshURL();
            }
        }

        /// <summary>
        /// URL Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbURL_Leave(object sender, EventArgs e)
        {
            RefreshURL();
        }

        /// <summary>
        /// URL Tag Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbURLTag_Leave(object sender, EventArgs e)
        {
            if (URLIndex >= 0)
            {
                DataItem itm = URLs[URLIndex];
                itm.tag = cbURLTag.Text;
                URLs.RemoveAt(URLIndex);
                URLs.Insert(URLIndex, itm);
            }
        }


        #endregion

        #region IM

        /// <summary>
        /// Button Add IM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddIM_Click(object sender, EventArgs e)
        {
            DataItem itm = new DataItem("");
            IMs.Add(itm);
            cbIM.Items.Add("");
            SelectIM(IMs.Count - 1);
            btnDelIM.Enabled = true;
            cbIM.Focus();
        }

        /// <summary>
        /// Button Delete IM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelIM_Click(object sender, EventArgs e)
        {
            if (IMIndex >= 0 && IMs.Count > 1)
            {
                IMs.RemoveAt(IMIndex);
                cbIM.Items.RemoveAt(IMIndex);
                SelectIM(0);

                if (IMs.Count == 1) btnDelIM.Enabled = false;
            }
        }


        /// <summary>
        /// Refresh IM
        /// </summary>
        private void RefreshIM()
        {
            if (IMIndex >= 0)
            {
                DataItem itm = IMs[IMIndex];
                itm.name = cbIM.Text;
                IMs.RemoveAt(IMIndex);
                IMs.Insert(IMIndex, itm);
                cbIM.Items.RemoveAt(IMIndex);
                cbIM.Items.Insert(IMIndex, itm.name);
                cbIM.Text = itm.name;
            }
        }


        /// <summary>
        /// IM ComboBox Selected Index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbIM_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectIM(cbIM.SelectedIndex);
        }

        /// <summary>
        /// IM Key Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbIM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RefreshIM();
            }
        }

        /// <summary>
        /// IM Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbIM_Leave(object sender, EventArgs e)
        {
            RefreshIM();
        }

        /// <summary>
        /// IM Tag Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbIMTag_Leave(object sender, EventArgs e)
        {
            if (IMIndex >= 0)
            {
                DataItem itm = IMs[IMIndex];
                itm.tag = cbIMTag.Text;
                IMs.RemoveAt(IMIndex);
                IMs.Insert(IMIndex, itm);
            }
        }

        #endregion
    }
}
