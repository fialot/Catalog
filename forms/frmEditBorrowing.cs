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

namespace Katalog
{

    public partial class frmEditBorrowing : Form
    {
        databaseEntities db = new databaseEntities();
        Guid ID = Guid.Empty;
        Guid LastItemGuid = Guid.Empty;
        Guid ItemGuid = Guid.Empty;
        Guid PersonGuid = Guid.Empty;
        string ItemInvNum = "";

        List<CInfo> contList = new List<CInfo>();
        List<IInfo> itemList = new List<IInfo>();

        public frmEditBorrowing()
        {
            InitializeComponent();
        }
                
        public DialogResult ShowDialog(Guid ID)
        {
            this.ID = ID;
            return base.ShowDialog();
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

        private void FillBorr(ref Borrowing borr)
        {
            if (cbItemType.SelectedIndex == 0)
                borr.ItemType = "item";
            else if (cbItemType.SelectedIndex == 1)
                borr.ItemType = "book";

            borr.ItemID = ItemGuid;
            borr.ItemNum = Conv.ToShortNull(cbItemNum.Text);
            borr.ItemInvNum = ItemInvNum;
            borr.PersonID = PersonGuid;

            borr.From = dtFrom.Value;
            borr.To = dtFrom.Value;
            borr.Returned = chbReturned.Checked;

        }

        private void SetItemsContext()
        {
            txtItem.AutoCompleteCustomSource.Clear();

            if (cbItemType.SelectedIndex == 0)
            {
                itemList = db.Items.Where(x => !(x.Excluded ?? false) && (x.Available ?? (x.Count ?? 1)) > 0).Select(x => new IInfo { ID = x.Id, Name = x.Name.Trim(), InvNum = x.InvNumber.Trim() }).ToList();
            }
            else if (cbItemType.SelectedIndex == 1)
            {
                itemList = db.Books.Where(x => !(x.Excluded ?? false) && (x.Available ?? (x.Count ?? 1)) > 0).Select(x => new IInfo { ID = x.Id, Name = x.Name.Trim(), InvNum = x.InventoryNumber.Trim() }).ToList();
            }

            for (int i = 0; i < itemList.Count; i++)
            {
                txtItem.AutoCompleteCustomSource.Add(itemList[i].Name + " #" + i.ToString());
            }
        }

        private void SetContactsContext()
        {
            contList = db.Contacts.Where(x => x.Active ?? true).Select(x => new CInfo { ID = x.Id, Name = x.Name.Trim(), Surname = x.Surname.Trim(), PersonalNum = x.code.Trim() }).ToList();
            for (int i = 0; i < contList.Count; i++)
            {
                txtPerson.AutoCompleteCustomSource.Add(contList[i].Name + " " + contList[i].Surname + " #" + i.ToString());
                txtPerson.AutoCompleteCustomSource.Add(contList[i].Surname + " " + contList[i].Name + " #" + i.ToString());
            }
        }

        private void frmEditBorrowing_Load(object sender, EventArgs e)
        {
            cbItemType.Items.Clear();
            cbItemType.Items.Add(Lng.Get("Item"));
            cbItemType.Items.Add(Lng.Get("Book"));
            cbItemType.SelectedIndex = 0;

            SetContactsContext();
            SetItemsContext();

            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now.AddDays(Properties.Settings.Default.DefaultBorrInterval);

            if (ID != Guid.Empty)
            {
                Borrowing borr = db.Borrowing.Find(ID);

                switch (borr.ItemType.Trim())
                {
                    case "item":
                        cbItemType.SelectedIndex = 0;
                        break;
                    case "book":
                        cbItemType.SelectedIndex = 1;
                        break;
                }

                

                if (cbItemType.SelectedIndex == 0)
                {
                    Items itm = db.Items.Find(borr.ItemID);
                    if (itm != null)
                    {
                        txtItem.Text = itm.Name.Trim();
                    }
                }
                else if (cbItemType.SelectedIndex == 1)
                {
                    Books book = db.Books.Find(borr.ItemID);
                    
                    if (book != null)
                    {
                        txtItem.Text = book.Name.Trim();
                    }
                }

                // ----- Fill Inventory number
                ItemGuid = borr.ItemID ?? Guid.Empty;
                FillInventoryNumber();
                cbItemNum.Text = (borr.ItemNum ?? 1).ToString();

                Contacts person = db.Contacts.Find(borr.PersonID);
                if (person != null)
                {
                    txtPerson.Text = person.Name.Trim() + " " + person.Surname.Trim();
                    lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": " + person.code.Trim();
                }
                    

                dtFrom.Value = borr.From ?? DateTime.Now;
                dtTo.Value = borr.To ?? DateTime.Now;
                chbReturned.Checked = borr.Returned ?? false;

                ItemGuid = borr.ItemID ?? Guid.Empty;
                LastItemGuid = ItemGuid;
                PersonGuid = borr.PersonID ?? Guid.Empty;
            }
        }

        private void RefreshAvailableItems()
        {
            List<short?> borr = new List<short?>();

            if (cbItemType.SelectedIndex == 0)
            {
                borr = db.Borrowing.Where(p => (p.ItemID == ItemGuid) && p.ItemType.Contains("item") && !(p.Returned ?? false)).Select(c => c.ItemNum).ToList();
                Items itm = db.Items.Find(ItemGuid);
                itm.Available = (short)(itm.Count -borr.Count);
                db.SaveChanges();
            }
            else if (cbItemType.SelectedIndex == 1)
            {
                borr = db.Borrowing.Where(p => (p.ItemID == ItemGuid) && p.ItemType.Contains("book") && !(p.Returned ?? false)).Select(c => c.ItemNum).ToList();
                Books itm = db.Books.Find(ItemGuid);
                itm.Available = (short)(itm.Count ?? 1 - borr.Count);
                db.SaveChanges();
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            FindPerson();
            FindItem();

            if (PersonGuid == Guid.Empty)
            {
                Dialogs.ShowWar(Lng.Get("NoSelPerson", "Not selected person!"), Lng.Get("Warning"));
                return;
            }

            if (ItemGuid == Guid.Empty)
            {
                Dialogs.ShowWar(Lng.Get("NoSelItem", "Not selected item!"), Lng.Get("Warning"));
                return;
            }

            if (Conv.ToIntDef(cbItemNum.Text,0) == 0)
            {
                Dialogs.ShowWar(Lng.Get("NoSelItemNum", "Not selected item number!"), Lng.Get("Warning"));
                return;
            }

            databaseEntities db = new databaseEntities();

            Borrowing borr;

            // ----- ID -----
            if (ID != Guid.Empty)
            {
                borr = db.Borrowing.Find(ID);
            }
            else
            {
                borr = new Borrowing();
                borr.ID = Guid.NewGuid();
            }

            FillBorr(ref borr);

            if (ID == Guid.Empty) db.Borrowing.Add(borr);
            db.SaveChanges();

            RefreshAvailableItems();
            

            this.DialogResult = DialogResult.OK;
        }

        
        private void txtPerson_TextChanged(object sender, EventArgs e)
        {
            PersonGuid = Guid.Empty;
            FindPerson();
            if (PersonGuid != Guid.Empty)
            {
                Contacts person = db.Contacts.Find(PersonGuid);
                lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": " + person.code.Trim();
            } else
            {
                lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": -";
            }
        }

        private void FillInventoryNumber()
        {
            if (ItemGuid != Guid.Empty)
            {
                List<IInfo> itm = new List<IInfo>();
                // ----- Fill Inventory number -----
                if (cbItemType.SelectedIndex == 0)
                {
                    itm = db.Items.Where(x => x.Id == ItemGuid).Select(x => new IInfo { ID = x.Id, Name = x.Name.Trim(), InvNum = (x.InvNumber ?? "").Trim(), Count = x.Count ?? 1 }).ToList();
                }
                else if (cbItemType.SelectedIndex == 1)
                {
                    itm = db.Books.Where(x => x.Id == ItemGuid).Select(x => new IInfo { ID = x.Id, Name = x.Name.Trim(), InvNum = (x.InventoryNumber ?? "").Trim(), Count = (int)(x.Count ?? 1) }).ToList();
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

        private List<int> GetItemNums(IInfo itm)
        {
            List<int> res = new List<int>();

            var borr = db.Borrowing.Where(x => (x.ID != ID) && (x.ItemID == ItemGuid) && !(x.Returned ?? false)).Select(x => x.ItemNum ?? 1).ToList();

            int Count = itm.Count;

            for (int i = 1; i <= Count; i++)
            {
                bool find = false;
                for (int j = 0; j < borr.Count; j++)
                    if (borr[j] == i) find = true;
                if (!find)
                    res.Add(i);
            }
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

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Guid ID;
            frmEditContacts form = new frmEditContacts();
            form.ShowDialog(out ID);

            Contacts person = db.Contacts.Find(ID);
            if (person != null)
            {
                txtPerson.Text = person.Name.Trim() + " " + person.Surname.Trim();
                PersonGuid = ID;
                lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": " + person.code.Trim();
            }
        }

        private void cbItemNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            LastItemGuid = ItemGuid;
            FillInventoryNumber();
        }
    }
	
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
        public int Count { get; set; }
    }
}
