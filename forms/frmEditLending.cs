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

        public frmEditLending()
        {
            InitializeComponent();
        }

        #endregion

        #region Functions

       

        private List<long> GetBarcodes(string InvNums)
        {
            List<long> res = new List<long>();
            string[] split = InvNums.Split(new string[] { ";" }, StringSplitOptions.None);
            foreach (var item in split)
                res.Add(Conv.ToNumber(item));
            return res;
        }

        private Guid FindItemByCode(string code, out ItemTypes type, out short ItemNum)
        {
            type = ItemTypes.item;
            long iCode = Conv.ToNumber(code);

            databaseEntities db = new databaseEntities();
            ItemNum = 1;

            // ----- Try find Inventary code -----
            var itmList = db.Items.Where(x => x.Barcode == iCode).Select(x => new IInfo { ID = x.ID, Name = x.Name.Trim(), InvNum = x.InventoryNumber.Trim(), Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1 }).ToList();
            if (itmList.Count > 0)
            {
                type = ItemTypes.item;
                return itmList[0].ID;
            }
            else
            {
                itmList = db.Items.Where(x => x.Barcode == iCode / 10).Select(x => new IInfo { ID = x.ID, Name = x.Name.Trim(), InvNum = x.InventoryNumber.Trim(), Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1 }).ToList();    // remove EAN checksum
                if (itmList.Count > 0)
                {
                    type = ItemTypes.item;
                    return itmList[0].ID;
                }
            }

            var bookList = db.Books.Where(x => x.Barcode == iCode).Select(x => new IInfo { ID = x.ID, Name = x.Title.Trim(), InvNum = x.InventoryNumber.Trim(), Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1 }).ToList();
            if (bookList.Count > 0)
            {
                type = ItemTypes.book;
                return bookList[0].ID;
            }
            else
            {
                bookList = db.Books.Where(x => x.Barcode == iCode / 10).Select(x => new IInfo { ID = x.ID, Name = x.Title.Trim(), InvNum = x.InventoryNumber.Trim(), Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1 }).ToList();    // remove EAN checksum
                if (bookList.Count > 0)
                {
                    type = ItemTypes.book;
                    return bookList[0].ID;
                }
            }

            var boardList = db.Boardgames.Where(x => x.Barcode == iCode).Select(x => new IInfo { ID = x.ID, Name = x.Name.Trim(), InvNum = x.InventoryNumber.Trim(), Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1 }).ToList();
            if (boardList.Count > 0)
            {
                type = ItemTypes.boardgame;
                return boardList[0].ID;
            }
            else
            {
                boardList = db.Boardgames.Where(x => x.Barcode == iCode / 10).Select(x => new IInfo { ID = x.ID, Name = x.Name.Trim(), InvNum = x.InventoryNumber.Trim(), Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1 }).ToList();    // remove EAN checksum
                if (boardList.Count > 0)
                {
                    type = ItemTypes.boardgame;
                    return boardList[0].ID;
                }
            }


            // ----- Find from more inventary codes ------

            itmList = db.Items.Where(x => x.Barcode == 0).Select(x => new IInfo { ID = x.ID, Name = x.Name.Trim(), InvNum = x.InventoryNumber.Trim(), Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1 }).ToList();
            foreach(var item in itmList)
            {
                var bar = GetBarcodes(item.InvNum);
                for (int i = 0; i < bar.Count; i++)
                    if (bar[i] == iCode || bar[i] == iCode/10)
                    {
                        type = ItemTypes.item;
                        ItemNum = (short)i;
                        return item.ID;
                    }
            }

            bookList = db.Books.Where(x => x.Barcode == 0).Select(x => new IInfo { ID = x.ID, Name = x.Title.Trim(), InvNum = x.InventoryNumber.Trim(), Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1 }).ToList();
            foreach (var item in itmList)
            {
                var bar = GetBarcodes(item.InvNum);
                for (int i = 0; i < bar.Count; i++)
                    if (bar[i] == iCode || bar[i] == iCode / 10)
                    {
                        type = ItemTypes.book;
                        ItemNum = (short)i;
                        return item.ID;
                    }
            }

            boardList = db.Boardgames.Where(x => x.Barcode == 0).Select(x => new IInfo { ID = x.ID, Name = x.Name.Trim(), InvNum = x.InventoryNumber.Trim(), Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1 }).ToList();
            foreach (var item in boardList)
            {
                var bar = GetBarcodes(item.InvNum);
                for (int i = 0; i < bar.Count; i++)
                    if (bar[i] == iCode || bar[i] == iCode / 10)
                    {
                        type = ItemTypes.boardgame;
                        ItemNum = (short)i;
                        return item.ID;
                    }
            }


            /*long num = -1;
            type = ItemTypes.item;

            foreach (var item in itemList)
            {
                num = Conv.ToNumber(item.InvNum);
                if (num == iCode || num == iCode / 10)
                {
                    type = (ItemTypes)cbItemType.SelectedIndex;
                    return item.ID;
                }
            }*/

            return Guid.Empty;
        }


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

        private List<IInfo> CreateCountList()
        {
            List<IInfo> list = new List<IInfo>();

            // selItemList
            foreach (var item in selItemList)
            {
                bool find = false;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].ID == item.ID)
                    {
                        IInfo x = list[i];
                        x.Count++;
                        list.RemoveAt(i);
                        list.Insert(i, x);
                        find = true;
                        break;
                    }
                }
                if (!find)
                {
                    IInfo x = item;
                    x.Count = 1;
                    list.Add(x);
                }
            }

            foreach (var item in origItemList)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].ID == item.ID)
                    {
                        IInfo x = list[i];
                        x.Count--;
                        list.RemoveAt(i);
                        list.Insert(i, x);
                        break;
                    }
                }
            }

            return list;
        }

        private List<IInfo> RemoveUsed(List<IInfo> list)
        {
            List<IInfo> counts = CreateCountList();

            for (int j = list.Count-1; j >= 0 ; j--)
            {
                for (int i = 0; i < counts.Count; i++)
                {
                    if(counts[i].ID == list[j].ID)
                    {
                        if (list[j].Available <= counts[i].Count)
                        {
                            list.Remove(list[j]);
                            break;
                        }
                            
                    }
                }
            }
            return list;
        }

        private void SetItemsContext()
        {
            databaseEntities db = new databaseEntities();

            txtItem.AutoCompleteCustomSource.Clear();

            if (cbItemType.SelectedIndex == 0)
            {
                itemList = db.Items.Where(x => !(x.Excluded ?? false) && (x.Available ?? (x.Count ?? 1)) > 0).Select(x => new IInfo { ID = x.ID, Name = x.Name.Trim(), InvNum = x.InventoryNumber.Trim(), Available = x.Available ?? (x.Count ?? 1), Count = x.Count ?? 1}).ToList();
                itemList = RemoveUsed(itemList);
            }
            else if (cbItemType.SelectedIndex == 1)
            {
                itemList = db.Books.Where(x => !(x.Excluded ?? false) && (x.Available ?? (x.Count ?? 1)) > 0).Select(x => new IInfo { ID = x.ID, Name = x.Title.Trim(), InvNum = x.InventoryNumber.Trim(), Available = x.Available ?? (short)(x.Count ?? 1), Count = (short)(x.Count ?? 1) }).ToList();
                itemList = RemoveUsed(itemList);
            }
            else if (cbItemType.SelectedIndex == 2)
            {
                itemList = db.Boardgames.Where(x => !(x.Excluded ?? false) && (x.Available ?? (x.Count ?? 1)) > 0).Select(x => new IInfo { ID = x.ID, Name = x.Name.Trim(), InvNum = x.InventoryNumber.Trim(), Available = x.Available ?? (short)(x.Count ?? 1), Count = (short)(x.Count ?? 1) }).ToList();
                itemList = RemoveUsed(itemList);
            }

            for (int i = 0; i < itemList.Count; i++)
            {
                txtItem.AutoCompleteCustomSource.Add(itemList[i].Name + " #" + i.ToString());
            }
        }

              
        private void RefreshAvailableItems(List<IInfo> list)
        {
            databaseEntities db = new databaseEntities();
            List<short?> borr = new List<short?>();

            foreach (var itm in list)
            {
                borr = db.Lending.Where(p => (p.ItemID == itm.ID) && p.ItemType.Contains(itm.ItemType.ToString()) && ((p.Status ?? 1) == (short)LendStatus.Reserved || (p.Status ?? 1) == (short)LendStatus.Lended)).Select(c => c.ItemNum).ToList();

                if (itm.ItemType == ItemTypes.item)
                {
                    Items item = db.Items.Find(itm.ID);
                    if (item != null)
                        item.Available = (short)((item.Count ?? 1) - borr.Count);
                }
                else if (itm.ItemType == ItemTypes.book)
                {
                    Books book = db.Books.Find(itm.ID);
                    if (book != null)
                        book.Available = (short)((book.Count ?? 1) - borr.Count);
                }
                else if (itm.ItemType == ItemTypes.boardgame)
                {
                    Boardgames board = db.Boardgames.Find(itm.ID);
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

            // ----- Prepare Item type Combo box -----
            cbItemType.Items.Clear();
            cbItemType.Items.Add(Lng.Get("Item"));
            cbItemType.Items.Add(Lng.Get("Book"));
            cbItemType.Items.Add(Lng.Get("Board game"));
            cbItemType.SelectedIndex = 0;

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
                    IInfo info = GetInfo(lend.ItemID ?? Guid.Empty, (ItemTypes)Enum.Parse(typeof(ItemTypes), lend.ItemType, true), lend.ItemNum ?? 1);

                    selItemList.Add(info);      // Selected list
                    origItemList.Add(info);     // Original list
                }


                // ----- Fill Inventory number -----
                FillInventoryNumber();
                //cbItemNum.Text = (borr.ItemNum ?? 1).ToString();

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

                ItemGuid = lend.ItemID ?? Guid.Empty;
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
        private void FillLend(ref Lending lend, IInfo item)
        {
            // ----- Item -----
            lend.ItemType = item.ItemType.ToString();
            lend.ItemID = item.ID;
            lend.ItemNum = (short)item.ItemNum;
            lend.ItemInvNum = item.InvNum;
            
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
            itInvNum.AspectGetter = delegate (object x) {
                if (((IInfo)x).InvNum != null)
                    return ((IInfo)x).InvNum.Trim();
                return "";
            };
            // ----- Column Number -----
            itNumber.AspectGetter = delegate (object x) {
                return ((IInfo)x).ItemNum;
            };

            // ----- Set model to OLV -----
            olvItem.SetObjects(selItemList);
        }

        private void FillInventoryNumber()
        {
            databaseEntities db = new databaseEntities();

            if (ItemGuid != Guid.Empty)
            {
                List<IInfo> itm = new List<IInfo>();
                // ----- Fill Inventory number -----
                if (cbItemType.SelectedIndex == 0)
                {
                    itm = db.Items.Where(x => x.ID == ItemGuid).Select(x => new IInfo { ID = x.ID, Name = x.Name.Trim(), InvNum = (x.InventoryNumber ?? "").Trim(), Count = x.Count ?? 1 }).ToList();
                }
                else if (cbItemType.SelectedIndex == 1)
                {
                    itm = db.Books.Where(x => x.ID == ItemGuid).Select(x => new IInfo { ID = x.ID, Name = x.Title.Trim(), InvNum = (x.InventoryNumber ?? "").Trim(), Count = x.Count ?? 1 }).ToList();
                }
                else if (cbItemType.SelectedIndex == 2)
                {
                    itm = db.Boardgames.Where(x => x.ID == ItemGuid).Select(x => new IInfo { ID = x.ID, Name = x.Name.Trim(), InvNum = (x.InventoryNumber ?? "").Trim(), Count = x.Count ?? 1 }).ToList();
                }

                if (itm.Count == 1)
                {
                    // ----- Change GUID -----
                    if (LastItemGuid != ItemGuid)
                    {
                        List<int> nums = GetItemNums(itm[0]);
                        cbItemNum.Items.Clear();
                        for (int i = 0; i < nums.Count; i++)
                            cbItemNum.Items.Add(nums[i].ToString());
                        if (cbItemNum.Items.Count > 0)
                            cbItemNum.SelectedIndex = 0;
                    }
                    string[] invNumbers = itm[0].InvNum.Split(new string[] { ";" }, StringSplitOptions.None);
                    int invNumIdx = Conv.ToShortDef(cbItemNum.Text, 0);
                    if (invNumIdx > 0)
                    {
                        ItemInvNum = invNumbers[invNumIdx - 1];
                        lblInvNum.Text = Lng.Get("InventoryNumber", "Inventory number") + ": " + ItemInvNum;
                    }
                    else lblInvNum.Text = Lng.Get("InventoryNumber", "Inventory number") + ": -";
                }
            }
            else
            {
                ItemInvNum = "";
                cbItemNum.Items.Clear();
                lblInvNum.Text = Lng.Get("InventoryNumber", "Inventory number") + ": -";
            }
        }

        private List<int> RemoveSelectedItemNums(List<int> list)
        {
            foreach(var item in selItemList)
            {
                if (item.ID == ItemGuid)
                {
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        if (list[i] == item.ItemNum)
                            list.RemoveAt(i);
                    }
                }
            }
            return list;
        }

        private List<int> GetItemNums(IInfo itm)
        {
            databaseEntities db = new databaseEntities();

            List<int> res = new List<int>();

            var borr = db.Lending.Where(x => (x.ItemID == ItemGuid) && ((x.Status ?? 1) == (short)LendStatus.Reserved || (x.Status ?? 1) == (short)LendStatus.Lended)).Select(x => x.ItemNum ?? 1).ToList();
            //var borr = db.Lending.Where(x => x.ItemNum.ToString().Any(x.ID != ID) && (x.ItemID == ItemGuid) && (x.Status ?? 1) != 2).Select(x => x.ItemNum ?? 1).ToList();
            int Count = itm.Count;

            for (int i = 1; i <= Count; i++)
            {
                bool find = false;
                for (int j = 0; j < borr.Count; j++)
                    if (borr[j] == i) find = true;
                if (!find)
                    res.Add(i);
            }
            res = RemoveSelectedItemNums(res);
            return res;
        }
        
        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            ItemGuid = Guid.Empty;
            FindItem();
            FillInventoryNumber();
            LastItemGuid = ItemGuid;
        }

        private void cbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetItemsContext();
            txtItem.Text = "";
            txtItem.Focus();
        }
        
        private void cbItemNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            LastItemGuid = ItemGuid;
            FillInventoryNumber();
        }
        
        /// <summary>
        /// Get Item info from DB
        /// </summary>
        /// <param name="ID">Item ID</param>
        /// <param name="type">Item type (Item, Book...)</param>
        /// <param name="ItemNum">Select Item Number</param>
        /// <returns></returns>
        private IInfo GetInfo(Guid ID, ItemTypes type, int ItemNum)
        {
            databaseEntities db = new databaseEntities();

            IInfo newItem = new IInfo();
            List<IInfo> list = null;

            // ----- Items -----
            if (type == ItemTypes.item)
            {
                Items itm = db.Items.Find(ID);          // Find Item
                if (itm != null)
                {
                    list = db.Items.Where(x => x.ID == ID).Select(x => new IInfo { ID = x.ID, Name = x.Name.Trim(), InvNum = x.InventoryNumber.Trim() }).ToList();
                }
            }
            // ----- Books -----
            else if (type == ItemTypes.book)
            {
                Books book = db.Books.Find(ID);         // Find Book
                if (book != null)
                {
                    list = db.Books.Where(x => x.ID == ID).Select(x => new IInfo { ID = x.ID, Name = x.Title.Trim(), InvNum = x.InventoryNumber.Trim() }).ToList();
                }
            }
            // ----- BoardGames -----
            else if (type == ItemTypes.boardgame)
            {
                Boardgames boardgame = db.Boardgames.Find(ID);         // Find Boardgame
                if (boardgame != null)
                {
                    list = db.Boardgames.Where(x => x.ID == ID).Select(x => new IInfo { ID = x.ID, Name = x.Name.Trim(), InvNum = x.InventoryNumber.Trim() }).ToList();
                }
            }


            if (list != null)                           // If found
            {
                newItem = list[0];
                newItem.ItemNum = ItemNum;              // Fill Values
                string[] invNumbers = newItem.InvNum.Split(new string[] { ";" }, StringSplitOptions.None);
                if (newItem.ItemNum <= invNumbers.Length)
                {
                    newItem.InvNum = invNumbers[newItem.ItemNum - 1];
                }
                newItem.ItemType = type;
            }
            return newItem;
        }

        /// <summary>
        /// Add Item to list
        /// </summary>
        private void AddItem()
        {
            // ----- Find selected Item -----
            FindItem();

            if (ItemGuid == Guid.Empty)
            {
                Dialogs.ShowWar(Lng.Get("NoSelItem", "Not selected item!"), Lng.Get("Warning"));
                return;
            }

            if (Conv.ToIntDef(cbItemNum.Text, 0) == 0)
            {
                Dialogs.ShowWar(Lng.Get("NoSelItemNum", "Not selected item number!"), Lng.Get("Warning"));
                return;
            }

            // ----- Add selected item to list -----
            IInfo newItem = GetInfo(ItemGuid, (ItemTypes)cbItemType.SelectedIndex, Conv.ToIntDef(cbItemNum.Text, 0));
            selItemList.Add(newItem);
            UpdateOLV();

            // ----- Refresh Items TextBox -----
            SetItemsContext();
            txtItem.Text = "";
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
                IInfo info = (IInfo)olvItem.SelectedItem.RowObject;
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
                ItemTypes type;
                short ItemNum;
                Guid ID = FindItemByCode(Barcode, out type, out ItemNum);
                if (ID != Guid.Empty)
                {
                    cbItemType.SelectedIndex = (int)type;
                    // ----- Item -----
                    if (type == ItemTypes.item)
                    {
                        Items itm = db.Items.Find(ID);
                        txtItem.Text = itm.Name.Trim();
                    }
                    // ----- Book -----
                    else if (type == ItemTypes.book)
                    {
                        Books book = db.Books.Find(ID);
                        txtItem.Text = book.Title.Trim();
                    }
                    // ----- Board game -----
                    else if (type == ItemTypes.boardgame)
                    {
                        Boardgames board = db.Boardgames.Find(ID);
                        txtItem.Text = board.Name.Trim();
                    }
                    ItemGuid = ID;
                    FillInventoryNumber();
                    cbItemNum.Text = ItemNum.ToString();

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
