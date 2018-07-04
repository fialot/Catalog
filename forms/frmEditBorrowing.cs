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

        List<Guid> ID = new List<Guid>();
        Guid LastItemGuid = Guid.Empty;
        Guid ItemGuid = Guid.Empty;
        Guid PersonGuid = Guid.Empty;
        string ItemInvNum = "";

        List<CInfo> contList = new List<CInfo>();           // Contact list
        List<IInfo> itemList = new List<IInfo>();           // Item list

        List<IInfo> selItemList = new List<IInfo>();        // Selected Item List
        List<IInfo> origItemList = new List<IInfo>();       // Item list befor change
        Communication com = new Communication();
        string Barcode = "";

        public delegate void MyDelegate(comStatus status);

        #endregion

        #region Constructor

        public frmEditBorrowing()
        {
            InitializeComponent();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Fing other borrowings to fill Item list
        /// </summary>
        /// <param name="ID">Main ID</param>
        /// <returns></returns>
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


        private Guid FindPersonByCode(string code)
        {
            long iCode = Conv.ToNumber(code);
            /*long num = -1;
            foreach (var item in contList)
            {
                num = Conv.ToNumber(item.PersonalNum);
                if (num == iCode || num == iCode / 10)
                {
                    return item.ID;
                }
            }*/
            databaseEntities db = new databaseEntities();
            var list = db.Contacts.Where(x => x.Barcode == iCode).ToList();
            if (list.Count > 0)
                return list[0].ID;
            else
            {
                list = db.Contacts.Where(x => x.Barcode == iCode / 10).ToList();    // remove EAN checksum
                if (list.Count > 0)
                    return list[0].ID;
            }

            return Guid.Empty;
        }

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
                        type = ItemTypes.item;
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

            for (int i = 0; i < itemList.Count; i++)
            {
                txtItem.AutoCompleteCustomSource.Add(itemList[i].Name + " #" + i.ToString());
            }
        }

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

        private void UpdateOLV()
        {

            itName.AspectGetter = delegate (object x) {
                if (((IInfo)x).Name != null)
                    return ((IInfo)x).Name.Trim();
                return "";
            };
            itInvNum.AspectGetter = delegate (object x) {
                if (((IInfo)x).InvNum != null)
                    return ((IInfo)x).InvNum.Trim();
                return "";
            };
            itNumber.AspectGetter = delegate (object x) {
                return ((IInfo)x).ItemNum;
            };

            olvItem.SetObjects(selItemList);
        }

        private void RefreshAvailableItems(List<IInfo> list)
        {
            databaseEntities db = new databaseEntities();
            List<short?> borr = new List<short?>();

            foreach (var itm in list)
            {
                borr = db.Borrowing.Where(p => (p.ItemID == itm.ID) && p.ItemType.Contains(itm.ItemType.ToString()) && (p.Status ?? 1) != 2).Select(c => c.ItemNum).ToList();

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
            }
            db.SaveChanges();
        }

        #endregion

        #region Load Form

        public DialogResult ShowDialog(Guid ID)
        {
            this.ID = FindOtherBorrowings(ID);
            return base.ShowDialog();
        }

        public DialogResult ShowDialog(List<Guid> ID)
        {
            this.ID = ID;
            return base.ShowDialog();
        }

        private void frmEditBorrowing_Load(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();

            com.ReceivedData += new ReceivedEventHandler(DataReceive);
            try
            {
                com.ConnectSP(Properties.Settings.Default.scanCOM);
            }
            catch { }
            

            cbStatus.Items.Clear();
            cbStatus.Items.Add(Lng.Get("Reserved"));
            cbStatus.Items.Add(Lng.Get("Borrowed"));
            cbStatus.Items.Add(Lng.Get("Returned"));
            cbStatus.SelectedIndex = 1;

            cbItemType.Items.Clear();
            cbItemType.Items.Add(Lng.Get("Item"));
            cbItemType.Items.Add(Lng.Get("Book"));
            cbItemType.SelectedIndex = 0;

            SetContactsContext();
            SetItemsContext();

            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now.AddDays(Properties.Settings.Default.DefaultBorrInterval);

            if (ID.Count > 0)
            {
                Borrowing borr = new Borrowing();

                foreach (var itm in ID)
                {
                    borr = db.Borrowing.Find(itm);

                    IInfo info = GetInfo(borr.ItemID ?? Guid.Empty, (ItemTypes)Enum.Parse(typeof(ItemTypes), borr.ItemType, true), borr.ItemNum ?? 1);
                    selItemList.Add(info);
                    origItemList.Add(info);
                }


                // ----- Fill Inventory number
                FillInventoryNumber();
                //cbItemNum.Text = (borr.ItemNum ?? 1).ToString();

                Contacts person = db.Contacts.Find(borr.PersonID);
                if (person != null)
                {
                    txtPerson.Text = person.Name.Trim() + " " + person.Surname.Trim();
                    lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": " + person.PersonCode.Trim();
                }


                dtFrom.Value = borr.From ?? DateTime.Now;
                dtTo.Value = borr.To ?? DateTime.Now;
                cbStatus.SelectedIndex = borr.Status ?? 1;

                ItemGuid = borr.ItemID ?? Guid.Empty;
                LastItemGuid = ItemGuid;
                PersonGuid = borr.PersonID ?? Guid.Empty;

                UpdateOLV();
            }
        }

        #endregion

        #region Close Form

        private void FillBorr(ref Borrowing borr, IInfo item)
        {
            borr.ItemType = item.ItemType.ToString();
            borr.ItemID = item.ID;
            borr.ItemNum = (short)item.ItemNum;
            borr.ItemInvNum = item.InvNum;

            borr.PersonID = PersonGuid;
            borr.From = dtFrom.Value;
            borr.To = dtTo.Value;
            borr.Status = (short)cbStatus.SelectedIndex;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            FindPerson();


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

            // ----- Create new
            foreach (var itm in selItemList)
            {
                borr = new Borrowing();
                borr.ID = Guid.NewGuid();
                FillBorr(ref borr, itm);
                db.Borrowing.Add(borr);
            }
            db.SaveChanges();

            RefreshAvailableItems(selItemList);

            com.Close();

            this.DialogResult = DialogResult.OK;

        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            com.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region Person

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


        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            databaseEntities db = new databaseEntities();

            Guid ID;
            frmEditContacts form = new frmEditContacts();
            form.ShowDialog(out ID);

            Contacts person = db.Contacts.Find(ID);
            if (person != null)
            {
                txtPerson.Text = person.Name.Trim() + " " + person.Surname.Trim();
                PersonGuid = ID;
                lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": " + person.PersonCode.Trim();
            }
        }

        #endregion

        #region Item 

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

            var borr = db.Borrowing.Where(x => (x.ItemID == ItemGuid) && (x.Status ?? 1) != 2).Select(x => x.ItemNum ?? 1).ToList();
            //var borr = db.Borrowing.Where(x => x.ItemNum.ToString().Any(x.ID != ID) && (x.ItemID == ItemGuid) && (x.Status ?? 1) != 2).Select(x => x.ItemNum ?? 1).ToList();
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

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void btnDelItem_Click(object sender, EventArgs e)
        {
            if (olvItem.SelectedIndex >= 0)
            {
                IInfo info = (IInfo)olvItem.SelectedItem.RowObject;
                selItemList.Remove(info);
                UpdateOLV();

                // ----- Refresh Items TextBox -----
                SetItemsContext();
            }
        }


        #endregion


        #region Barcode


        private void DataReceive(object source, comStatus status)
        {
            txtPerson.Invoke(new MyDelegate(updateLog), new Object[] { status }); //BeginInvoke

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
               
            } else if(txtItem.Focused)
            {
                ItemTypes type;
                short ItemNum;
                Guid ID = FindItemByCode(Barcode, out type, out ItemNum);
                if (ID != Guid.Empty)
                {
                    cbItemType.SelectedIndex = (int)type;
                    if (type == ItemTypes.item)
                    {
                        Items itm = db.Items.Find(ID);
                        txtItem.Text = itm.Name.Trim();
                    }
                    else if (type == ItemTypes.book)
                    {
                        Books book = db.Books.Find(ID);
                        txtItem.Text = book.Title.Trim();
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

        private void frmEditBorrowing_FormClosing(object sender, FormClosingEventArgs e)
        {
            com.Close();
        }

        private void olvItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (olvItem.SelectedIndex >= 0)
            {
                btnDelItem.Enabled = true;
            }
            else
            {
                btnDelItem.Enabled = false;
            }
        }
    }

    public enum ItemTypes { item = 0, book = 1 }

    public class CInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalNum { get; set; }
    }

    public class IInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string InvNum { get; set; }
        public short Count { get; set; }
        public short Available { get; set; }
        public int ItemNum { get; set; }
        public ItemTypes ItemType { get; set; }
    }
}
