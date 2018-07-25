using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using myFunctions;
using Communications;
using TCPClient;

namespace Katalog
{
    public partial class frmEditBorrowing : Form
    {
        #region Variables
                
        // ----- Item Lists -----
        List<Guid> ID = new List<Guid>();                   // List of Borrowing IDs
        Guid PersonGuid = Guid.Empty;                       // Person ID

        List<CInfo> contList = new List<CInfo>();           // Contact list
        List<IInfo> itemList = new List<IInfo>();           // Item list

        List<IInfo> selItemList = new List<IInfo>();        // Selected Item List
        List<IInfo> origItemList = new List<IInfo>();       // Item list befor change

        // ----- Fast Tags -----
        Color SelectColor = Color.SkyBlue;                  // FastTags Select color

        // ----- Barcode -----
        Communication com = new Communication();            // Barcode reader communication
        string Barcode = "";                                // Readed barcode
        public delegate void MyDelegate(comStatus status);  // Communication delegate

        #endregion

        #region Constructor

        public frmEditBorrowing()
        {
            InitializeComponent();
        }

        #endregion
        
        #region Load Form
        
        /// <summary>
        /// Find other Borrowings to fill Item list
        /// </summary>
        /// <param name="ID">Main ID</param>
        /// <returns>Borrowings list</returns>
        private List<Guid> FindOtherBorrowings(Guid ID)
        {
            databaseEntities db = new databaseEntities();

            List<Guid> list = new List<Guid>();

            Borrowing borr = db.Borrowing.Find(ID);
            if (borr != null)
            {
                list = db.Borrowing.Where(x => x.PersonID == borr.PersonID && x.Status == borr.Status && x.From == borr.From && x.To == borr.To).Select(x => x.ID).ToList();
            }

            return list;
        }
        
        /// <summary>
        /// Show dialog with edit related items
        /// </summary>
        /// <param name="ID">Item ID</param>
        /// <returns></returns>
        public DialogResult ShowDialog(Guid ID)
        {
            this.ID = FindOtherBorrowings(ID);
            return base.ShowDialog();
        }

        /// <summary>
        /// Show dialog with selected related items
        /// </summary>
        /// <param name="ID">Item IDs</param>
        /// <returns></returns>
        public DialogResult ShowDialog(List<Guid> ID)
        {
            this.ID = ID;
            return base.ShowDialog();
        }

        /// <summary>
        /// Load Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditBorrowing_Load(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();

            // ----- Create connection to barcode reader -----
            com.ReceivedData += new ReceivedEventHandler(DataReceive);
            try
            {
                com.ConnectSP(Properties.Settings.Default.scanCOM);
            }
            catch { }
            
            // ----- Prepare Status Combo box -----
            cbStatus.Items.Clear();
            cbStatus.Items.Add(Lng.Get("Reserved"));
            cbStatus.Items.Add(Lng.Get("Borrowed"));
            cbStatus.Items.Add(Lng.Get("Returned"));
            cbStatus.Items.Add(Lng.Get("Canceled"));
            cbStatus.SelectedIndex = 1;

            // ----- Prepare Contacts autocomplete Context -----
            SetContactsContext();

            // ----- Set Default values -----
            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now.AddDays(Properties.Settings.Default.DefaultBorrInterval);

            // ----- If Edit items -----
            if (ID.Count > 0)
            {
                Borrowing borr = new Borrowing();

                // ----- Find all edited items -----
                foreach (var itm in ID)
                {
                    borr = db.Borrowing.Find(itm);

                    // ----- Fill Item list -----
                    IInfo info = new IInfo();
                    info.Name = borr.Item.Trim();
                    if (borr.ItemInvNum != null)
                        info.InvNum = borr.ItemInvNum.Trim();
                    if (borr.Note != null)
                        info.Note = borr.Note.Trim();

                    selItemList.Add(info);                      // Selected list
                    origItemList.Add(info);                     // Original list
                }


                // ----- Fill Person -----
                Contacts person = db.Contacts.Find(borr.PersonID);
                if (person != null)
                {
                    txtPerson.Text = person.Name.Trim() + " " + person.Surname.Trim();
                    lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": " + person.PersonCode.Trim();
                }

                // ----- Fill other values -----
                dtFrom.Value = borr.From ?? DateTime.Now;
                dtTo.Value = borr.To ?? DateTime.Now;
                cbStatus.SelectedIndex = borr.Status ?? 1;

                // ----- Fill Fast tags -----
                FastFlags flag = (FastFlags)(borr.FastTags ?? 0);
                if (flag.HasFlag(FastFlags.FLAG1)) btnTag1.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG2)) btnTag2.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG3)) btnTag3.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG4)) btnTag4.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG5)) btnTag5.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG6)) btnTag6.BackColor = SelectColor;

                PersonGuid = borr.PersonID ?? Guid.Empty;

                // ----- Update Items OLV -----
                UpdateOLV();
            }
        }

        #endregion

        #region Close Form

        /// <summary>
        /// Fill Borrowing Items
        /// </summary>
        /// <param name="borr">Borrowing DB item</param>
        /// <param name="item">New Item</param>
        private void FillBorr(ref Borrowing borr, IInfo item)
        {
            // ----- Item -----
            borr.Item = item.Name;
            borr.ItemInvNum = item.InvNum;
            borr.Note = item.Note;

            // ----- Other -----
            borr.PersonID = PersonGuid;
            borr.From = dtFrom.Value;
            borr.To = dtTo.Value;
            borr.Status = (short)cbStatus.SelectedIndex;

            // ----- Fast tags -----
            short fastTag = 0;
            if (btnTag1.BackColor == SelectColor) fastTag |= 0x01;
            if (btnTag2.BackColor == SelectColor) fastTag |= 0x02;
            if (btnTag3.BackColor == SelectColor) fastTag |= 0x04;
            if (btnTag4.BackColor == SelectColor) fastTag |= 0x08;
            if (btnTag5.BackColor == SelectColor) fastTag |= 0x10;
            if (btnTag6.BackColor == SelectColor) fastTag |= 0x20;
            borr.FastTags = fastTag;
        }

        /// <summary>
        /// Button Ok
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // ----- Find selected person in DB -----
            FindPerson();

            // ----- Check if record valid -----
            if (PersonGuid == Guid.Empty)
            {
                Dialogs.ShowWar(Lng.Get("NoSelPerson", "Not selected person!"), Lng.Get("Warning"));
                return;
            }

            if (selItemList.Count == 0)
            {
                Dialogs.ShowWar(Lng.Get("NoSelItem", "Not selected item!"), Lng.Get("Warning"));
                return;
            }
            
            databaseEntities db = new databaseEntities();

            Borrowing borr;

            // ----- Delete old data -----
            if (ID.Count > 0)
            {
                foreach (var itm in ID)
                {
                    borr = db.Borrowing.Find(itm);
                    db.Borrowing.Remove(borr);
                }
                db.SaveChanges();
            }

            // ----- Create new -----
            foreach (var itm in selItemList)
            {
                borr = new Borrowing();
                borr.ID = Guid.NewGuid();
                FillBorr(ref borr, itm);
                db.Borrowing.Add(borr);
            }

            // ----- Save to DB -----
            db.SaveChanges();
            
            // ----- Close Barcode reader connection -----
            com.Close();

            this.DialogResult = DialogResult.OK;

        }
        
        /// <summary>
        /// Button Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // ----- Close Barcode reader connection -----
            com.Close();

            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Form Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditBorrowing_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ----- Close Barcode reader connection -----
            com.Close();
        }

        #endregion

        #region Person
        
        /// <summary>
        /// Find person by Index from txtPerson (Fill PersonGUID)
        /// </summary>
        private void FindPerson()
        {
            //PersonGuid = Guid.Empty;
            string text = txtPerson.Text;
            int pos = text.IndexOf("#");
            if (pos >= 0)
            {
                int val = Conv.ToIntDef(text.Substring(pos + 1), -1);
                if (val >= 0 && val < contList.Count)
                {
                    PersonGuid = contList[val].ID;
                }
            }
        }

        /// <summary>
        /// Find Person in DB by Code
        /// </summary>
        /// <param name="barcode">Person Code</param>
        /// <returns>Person ID</returns>
        private Guid FindPersonByBarcode(string barcode)
        {
            long iCode = Conv.ToNumber(barcode);
            /*long num = -1;
            foreach (var item in contList)
            {
                num = Conv.ToNumber(item.PersonalNum);
                if (num == iCode || num == iCode / 10)
                {
                    return item.ID;
                }
            }*/

            // ----- Try find Person by barcode -----
            databaseEntities db = new databaseEntities();
            var list = db.Contacts.Where(x => x.Barcode == iCode).ToList();
            if (list.Count > 0)
                return list[0].ID;
            // ----- If no found -> tryfind Person by barcode without checksum -----
            else
            {
                list = db.Contacts.Where(x => x.Barcode == iCode / 10).ToList();    // remove EAN checksum
                if (list.Count > 0)
                    return list[0].ID;
            }
            // ----- If not found -> return Empty GUID -----
            return Guid.Empty;
        }

        /// <summary>
        /// Create Contact list for autocomplete function
        /// </summary>
        private void SetContactsContext()
        {
            databaseEntities db = new databaseEntities();

            contList = db.Contacts.Where(x => x.Active ?? true).Select(x => new CInfo { ID = x.ID, Name = x.Name.Trim(), Surname = x.Surname.Trim(), PersonalNum = x.PersonCode.Trim() }).ToList();
            for (int i = 0; i < contList.Count; i++)
            {
                txtPerson.AutoCompleteCustomSource.Add(contList[i].Name + " " + contList[i].Surname + " #" + i.ToString());
                txtPerson.AutoCompleteCustomSource.Add(contList[i].Surname + " " + contList[i].Name + " #" + i.ToString());
            }
        }
        
        /// <summary>
        /// Textbox Person Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPerson_TextChanged(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();

            PersonGuid = Guid.Empty;
            FindPerson();
            if (PersonGuid != Guid.Empty)
            {
                Contacts person = db.Contacts.Find(PersonGuid);
                lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": " + person.PersonCode.Trim();
            } else
            {
                lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": -";
            }
        }

        /// <summary>
        /// Button add person
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();

            // ----- Create new Contact -----
            Guid ID;
            frmEditContacts form = new frmEditContacts();
            form.ShowDialog(out ID);

            // ----- Select this Contact -----
            Contacts person = db.Contacts.Find(ID);
            if (person != null)
            {
                txtPerson.Text = person.Name.Trim() + " " + person.Surname.Trim();
                PersonGuid = ID;
                lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": " + person.PersonCode.Trim();
            }
        }
        
        /// <summary>
        /// Enter key -> Jump to Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPerson_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItem.Focus();
            }
        }


        #endregion

        #region Item 

        /// <summary>
        /// Update Items OLV
        /// </summary>
        private void UpdateOLV()
        {
            // ----- Column Name -----
            itName.AspectGetter = delegate (object x) {
                if (((IInfo)x).Name != null)
                    return ((IInfo)x).Name.Trim();
                return "";
            };
            // ----- Column Inventory number -----
            itInvNumber.AspectGetter = delegate (object x) {
                if (((IInfo)x).InvNum != null)
                    return ((IInfo)x).InvNum.Trim();
                return "";
            };
            // ----- Column Note -----
            itNote.AspectGetter = delegate (object x) {
                if (((IInfo)x).Note != null)
                    return ((IInfo)x).Note.Trim();
                return "";
            };

            // ----- Set model to OLV -----
            olvItem.SetObjects(selItemList);
        }

        /// <summary>
        /// Add Item to list
        /// </summary>
        private void AddItem()
        {
            // ----- Find selected Item -----
            if (txtItem.Text == "")
            {
                Dialogs.ShowWar(Lng.Get("NoSelItem", "Not selected item!"), Lng.Get("Warning"));
                return;
            }

            // ----- Add selected item to list -----
            IInfo newItem = new IInfo();
            string[] split = txtItem.Text.Split(new string[] { ";" }, StringSplitOptions.None);
            newItem.Name = split[0];
            if (split.Length > 1)
                newItem.InvNum = split[1];
            if (split.Length > 2)
                newItem.Note = split[2];
            selItemList.Add(newItem);
            UpdateOLV();

            // ----- Refresh Items TextBox -----
            txtItem.Text = "";
        }

        /// <summary>
        /// Button Add Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddItem();      // Add Item to list
        }

        /// <summary>
        /// Add Item by Enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddItem();      // Add Item to list
            }
        }

        /// <summary>
        /// Button Delete Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelItem_Click(object sender, EventArgs e)
        {
            // ----- If select Item -----
            if (olvItem.SelectedIndex >= 0)
            {
                // ----- Remove -----
                IInfo info = (IInfo)olvItem.SelectedItem.RowObject;
                selItemList.Remove(info);

                // ----- Update OLV -----
                UpdateOLV();
                olvItem.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Change OLV Selected Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ----- Enable / Disable Delete button -----
            if (olvItem.SelectedIndex >= 0)
            {
                btnDelItem.Enabled = true;
            }
            else
            {
                btnDelItem.Enabled = false;
            }
        }

        #endregion

        #region Barcode

        /// <summary>
        /// Data receive delegate
        /// </summary>
        /// <param name="source"></param>
        /// <param name="status"></param>
        private void DataReceive(object source, comStatus status)
        {
            txtPerson.Invoke(new MyDelegate(DataProcess), new Object[] { status }); //BeginInvoke

        }

        /// <summary>
        /// Data process function
        /// </summary>
        /// <param name="status"></param>
        public void DataProcess(comStatus status)
        {
            if (status == comStatus.Close)
            {
                
            }
            // ----- Status Incoming data -----
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

        /// <summary>
        /// Process data after timeout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeOut_Tick(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();

            TimeOut.Enabled = false;
            // ----- Fill Person by Barcode -----
            if (txtPerson.Focused)
            {
                Guid ID = FindPersonByBarcode(Barcode);
                if (ID != Guid.Empty)
                {
                    Contacts person = db.Contacts.Find(ID);
                    txtPerson.Text = person.Name.Trim() + " " + person.Surname.Trim();
                    lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": " + person.PersonCode.Trim();
                    PersonGuid = person.ID;
                    txtItem.Focus();
                }
                else
                    Dialogs.ShowWar(Lng.Get("NoPersonNumber", "This ID have no person!"), Lng.Get("Warning"));
               
            }
            // ----- Fill Inventory number -----
            else if (txtItem.Focused)
            {
                txtItem.Text += "; " + Barcode;
                AddItem();
            }
            Barcode = "";
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

    }
}
