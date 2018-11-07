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
    public partial class frmEditLending : Form
    {
        #region Variables

        // ----- Item Lists -----
        List<Guid> ID = new List<Guid>();                   // List of Lending IDs
        Guid LastItemGuid = Guid.Empty;                     // Last Item ID
        Guid ItemGuid = Guid.Empty;                         // Item ID
        Guid PersonGuid = Guid.Empty;                       // Person ID
        string ItemInvNum = "";

        // ----- Autofill -----
        List<CInfo> contList = new List<CInfo>();           // Contact list
        List<IInfo> itemList = new List<IInfo>();           // Item list

        // ----- Selected items -----
        List<Copies> selItemList = new List<Copies>();      // Selected Item List
        //List<Copies> origItemList = new List<Copies>();     // Item list befor change

        // ----- Fast Tags -----
        Color SelectColor = Color.SkyBlue;                  // FastTags Select color

        // ----- Barcode -----
        Communication com = new Communication();            // Barcode reader communication
        string Barcode = "";                                // Readed barcode
        public delegate void MyDelegate(comStatus status);  // Communication delegate

        #endregion

        #region Constructor

        public frmEditLending()
        {
            InitializeComponent();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Set Items textbox context
        /// </summary>
        private void SetItemsContext()
        {
            databaseEntities db = new databaseEntities();

            txtItem.AutoCompleteCustomSource.Clear();

            itemList = db.Copies.Where(x => !(x.Excluded ?? false) && ((x.Status ?? (short)LendStatus.Returned) == (short)LendStatus.Canceled || (x.Status ?? (short)LendStatus.Returned) == (short)LendStatus.Returned)).Select(x => new IInfo { ID = x.ID, ItemID = x.ItemID ?? Guid.Empty, InventoryNumber = x.InventoryNumber.Trim(), ItemType = x.ItemType.Trim(), Barcode = x.Barcode ?? 0}).ToList();


            for (int j = 0; j < selItemList.Count; j++)
            {
                for (int i = itemList.Count - 1; i >= 0; i--)
                {
                    if (itemList[i].ID == selItemList[j].ID)
                    {
                        itemList.RemoveAt(i);
                        break;
                    }
                }
            }

            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].Name = global.GetLendingItemName(itemList[i].ItemType.ToString(), itemList[i].ItemID);
                txtItem.AutoCompleteCustomSource.Add(itemList[i].Name + " #" + i.ToString());
            }
        }
            
        /// <summary>
        /// Refresh Available Items
        /// </summary>
        /// <param name="list"></param>
        private void RefreshAvailableItems(List<Copies> list)
        {
            databaseEntities db = new databaseEntities();

            foreach (var itm in list)
            {
                // ----- Refresh available items -----
                var borr = db.Copies.Where(p => (p.ItemID == itm.ItemID) && p.ItemType.Contains(itm.ItemType.ToString()) && ((p.Status ?? 1) == (short)LendStatus.Reserved || (p.Status ?? 1) == (short)LendStatus.Lended)).Select(c => c.ID).ToList();

                if (global.GetItemType(itm.ItemType) == ItemTypes.item)
                {
                    Items item = db.Items.Find(itm.ItemID);
                    if (item != null)
                        item.Available = (short)((item.Count ?? 1) - borr.Count);
                }
                else if (global.GetItemType(itm.ItemType) == ItemTypes.book)
                {
                    Books book = db.Books.Find(itm.ItemID);
                    if (book != null)
                        book.Available = (short)((book.Count ?? 1) - borr.Count);
                }
                else if (global.GetItemType(itm.ItemType) == ItemTypes.boardgame)
                {
                    Boardgames board = db.Boardgames.Find(itm.ItemID);
                    if (board != null)
                        board.Available = (short)((board.Count ?? 1) - borr.Count);
                }
            }
            db.SaveChanges();
        }
        
        #endregion

        #region Load Form

        /// <summary>
        /// Fing other Lendings to fill Item list
        /// </summary>
        /// <param name="ID">Main ID</param>
        /// <returns></returns>
        private List<Guid> FindOtherLendings(Guid ID)
        {
            databaseEntities db = new databaseEntities();

            List<Guid> list = new List<Guid>();

            Lending borr = db.Lending.Find(ID);
            if (borr != null)
            {
                list = db.Lending.Where(x => x.PersonID == borr.PersonID && x.Status == borr.Status && x.From == borr.From && x.To == borr.To).Select(x => x.ID).ToList();
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
            this.ID = FindOtherLendings(ID);
            return base.ShowDialog();
        }
        
        /// <summary>
        /// Show dialog with edit related items
        /// </summary>
        /// <param name="ID">Item ID</param>
        /// <returns></returns>
        public DialogResult ShowPersonDialog(Guid ID)
        {
            PersonGuid = ID;
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
        private void frmEditLending_Load(object sender, EventArgs e)
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
            
            // ----- Prepare autocomplete Context -----
            SetContactsContext();               // Contacts
            SetItemsContext();                  // Items

            // ----- Set Default values -----
            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now.AddDays(Properties.Settings.Default.DefaultBorrInterval);

            // ----- If Edit items -----
            if (ID.Count > 0)
            {
                Lending lend = new Lending();

                // ----- Find all edited items -----
                foreach (var itm in ID)
                {
                    lend = db.Lending.Find(itm);

                    // ----- Fill Item list -----
                    Copies copy = db.Copies.Find(lend.CopyID);
                    selItemList.Add(copy);          // Selected list
                    //origItemList.Add(copy);         // Original list    
                }
                
                // ----- Fill Person -----
                Contacts person = db.Contacts.Find(lend.PersonID);
                if (person != null)
                {
                    txtPerson.Text = person.Name.Trim() + " " + person.Surname.Trim();
                    lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": " + person.PersonCode.Trim();
                }

                // ----- Fill other values -----
                dtFrom.Value = lend.From ?? DateTime.Now;
                dtTo.Value = lend.To ?? DateTime.Now;
                int stat = lend.Status ?? 1;
                if (stat < cbStatus.Items.Count)
                    cbStatus.SelectedIndex = stat;
                txtNote.Text = lend.Note;

                ItemGuid = lend.CopyID ?? Guid.Empty;
                LastItemGuid = ItemGuid;
                PersonGuid = lend.PersonID ?? Guid.Empty;

                // ----- Fill Fast tags -----
                FastFlags flag = (FastFlags)(lend.FastTags ?? 0);
                if (flag.HasFlag(FastFlags.FLAG1)) btnTag1.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG2)) btnTag2.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG3)) btnTag3.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG4)) btnTag4.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG5)) btnTag5.BackColor = SelectColor;
                if (flag.HasFlag(FastFlags.FLAG6)) btnTag6.BackColor = SelectColor;

                // ----- Update Items OLV -----
                UpdateOLV();
            }
            // ----- Lending to person -----
            else if (PersonGuid != Guid.Empty)
            {
                Guid temp = PersonGuid;
                // ----- Fill Person -----
                Contacts person = db.Contacts.Find(PersonGuid);
                if (person != null)
                {
                    txtPerson.Text = person.Name.Trim() + " " + person.Surname.Trim();
                    lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": " + person.PersonCode.Trim();
                }
                PersonGuid = temp;
                txtPerson.Enabled = false;
                btnAddPerson.Enabled = false;
                this.ActiveControl = txtItem;
            }
        }

        #endregion

        #region Close Form

        /// <summary>
        /// Fill Lending Items
        /// </summary>
        /// <param name="lend"></param>
        /// <param name="item"></param>
        private void FillLend(ref Lending lend, Copies item)
        {
            // ----- Item -----
            lend.CopyType = item.ItemType.ToString();
            lend.CopyID = item.ID;
            
            // ----- Other -----
            lend.PersonID = PersonGuid;
            lend.From = dtFrom.Value;
            lend.To = dtTo.Value;
            lend.Status = (short)cbStatus.SelectedIndex;
            lend.Note = txtNote.Text;

            // ----- Fast tags -----
            short fastTag = 0;
            if (btnTag1.BackColor == SelectColor) fastTag |= 0x01;
            if (btnTag2.BackColor == SelectColor) fastTag |= 0x02;
            if (btnTag3.BackColor == SelectColor) fastTag |= 0x04;
            if (btnTag4.BackColor == SelectColor) fastTag |= 0x08;
            if (btnTag5.BackColor == SelectColor) fastTag |= 0x10;
            if (btnTag6.BackColor == SelectColor) fastTag |= 0x20;
            lend.FastTags = fastTag;
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

            Lending borr;

            // ----- Delete old data -----
            if (ID.Count > 0)
            {
                foreach (var itm in ID)
                {
                    borr = db.Lending.Find(itm);
                    db.Lending.Remove(borr);
                }
                db.SaveChanges();
            }

            // ----- Create new -----
            foreach (var itm in selItemList)
            {
                borr = new Lending();
                borr.ID = Guid.NewGuid();
                FillLend(ref borr, itm);
                db.Lending.Add(borr);
            }

            // ----- Save to DB -----
            db.SaveChanges();

            // ----- Refresh Copy Status -----
            global.RefreshCopiesStatus(selItemList, (short)cbStatus.SelectedIndex);

            // ----- Refresh Available Items in Items Tables -----
            RefreshAvailableItems(selItemList);
            
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
        private void frmEditLending_FormClosing(object sender, FormClosingEventArgs e)
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
        private Guid FindPersonByCode(string barcode)
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

            AutoCompleteStringCollection auto = new AutoCompleteStringCollection();

            contList = db.Contacts.Where(x => x.Active ?? true).Select(x => new CInfo { ID = x.ID, Name = x.Name.Trim(), Surname = x.Surname.Trim(), PersonalNum = x.PersonCode.Trim() }).ToList();
            for (int i = 0; i < contList.Count; i++)
            {
                auto.Add(contList[i].Name + " " + contList[i].Surname + " #" + i.ToString());
                auto.Add(contList[i].Surname + " " + contList[i].Name + " #" + i.ToString());
                //txtPerson.AutoCompleteCustomSource.Add(contList[i].Name + " " + contList[i].Surname + " #" + i.ToString());
                //txtPerson.AutoCompleteCustomSource.Add(contList[i].Surname + " " + contList[i].Name + " #" + i.ToString());
            }
            txtPerson.AutoCompleteCustomSource = auto;
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
        /// Find item by Barcode
        /// </summary>
        /// <param name="code">Barcode</param>
        /// <returns></returns>
        private Guid FindItemByCode(string code)
        {
            long iCode = Conv.ToNumber(code);

            /*databaseEntities db = new databaseEntities();
            var items = db.Copies.Where(x => (x.Barcode ?? 0) == iCode).Select(x => x.ID).ToList();*/


            // ----- Find by barcode -----
            foreach (var item in itemList)
            {
                if (item.Barcode == iCode)
                    return item.ID;
            }

            // ----- Barcode without checksum -----
            foreach (var item in itemList)
            {
                if (item.Barcode == iCode / 10)
                    return item.ID;
            }

            return Guid.Empty;
        }

        /// <summary>
        /// Find Item by selected Name
        /// </summary>
        private void FindItem()
        {
            //PersonGuid = Guid.Empty;
            string text = txtItem.Text;
            int pos = text.IndexOf("#");
            if (pos >= 0)
            {
                int val = Conv.ToIntDef(text.Substring(pos + 1), -1);
                if (val >= 0 && val < itemList.Count)
                {
                    ItemGuid = itemList[val].ID;
                }
            }
        }
        
        /// <summary>
        /// Update Items OLV
        /// </summary>
        private void UpdateOLV()
        {

            // ----- Column Name -----
            itName.AspectGetter = delegate (object x) {
                return global.GetLendingItemName(((Copies)x).ItemType, ((Copies)x).ItemID ?? Guid.Empty);
            };
            // ----- Column Inventory number -----
            itInvNum.AspectGetter = delegate (object x) {
                if (((Copies)x).InventoryNumber != null)
                    return ((Copies)x).InventoryNumber.Trim();
                return "";
            };
            // ----- Column Number -----
            itType.AspectGetter = delegate (object x) {
                return global.GetItemTypeName(((Copies)x).ItemType);
            };

            // ----- Set model to OLV -----
            olvItem.SetObjects(selItemList);
        }
            
        /// <summary>
        /// Add Item to list
        /// </summary>
        private void AddItem()
        {
            databaseEntities db = new databaseEntities();

            // ----- Find selected Item -----
            FindItem();

            if (ItemGuid == Guid.Empty)
            {
                Dialogs.ShowWar(Lng.Get("NoSelItem", "Not selected item!"), Lng.Get("Warning"));
                return;
            }

            // ----- Add selected item to list -----
            var newItem = db.Copies.Find(ItemGuid);
            selItemList.Add(newItem);
            UpdateOLV();

            // ----- Refresh Items TextBox -----
            SetItemsContext();
            txtItem.Text = "";
        }

        /// <summary>
        /// Item Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();

            ItemGuid = Guid.Empty;
            FindItem();
            if (ItemGuid != Guid.Empty)
            {
                var copy = db.Copies.Find(ItemGuid);
                lblInvNum.Text = Lng.Get("InventoryNumber", "Inventory number") + ": " + copy.InventoryNumber;
            }
            else
            {
                lblInvNum.Text = Lng.Get("InventoryNumber", "Inventory number") + ": -";
            }

            LastItemGuid = ItemGuid;
        }

        /// <summary>
        /// Button Add Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddItem();              // Add Item to list
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
                var info = (Copies)olvItem.SelectedItem.RowObject;
                selItemList.Remove(info);

                // ----- Update OLV -----
                UpdateOLV();
                olvItem.SelectedIndex = -1;

                // ----- Refresh Items TextBox -----
                SetItemsContext();
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
                Guid ID = FindPersonByCode(Barcode);
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
                Guid ID = FindItemByCode(Barcode);
                if (ID != Guid.Empty)
                {
                    var copy = db.Copies.Find(ID);
                    ItemGuid = ID;
                    txtItem.Text = global.GetLendingItemName(copy.ItemType, copy.ItemID ?? Guid.Empty);
                    lblInvNum.Text = Lng.Get("InventoryNumber", "Inventory number") + ": " + copy.InventoryNumber;

                    AddItem();
                }
                else
                    Dialogs.ShowWar(Lng.Get("NoItemNumber", "This ID have no item!"), Lng.Get("Warning"));
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
