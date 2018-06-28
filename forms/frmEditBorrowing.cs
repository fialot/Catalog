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
    public struct ItemVals
    {
        public string Name;
        public Guid ID;
    }

    public partial class frmEditBorrowing : Form
    {
        databaseEntities db = new databaseEntities();
        Guid ID = Guid.Empty;
        Guid ItemGuid = Guid.Empty;
        Guid PersonGuid = Guid.Empty;

        List<Contacts> contList = new List<Contacts>();
        List<ItemVals> itemList = new List<ItemVals>();

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
                    PersonGuid = contList[val].Id;
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
                borr.type = "item";
            else if (cbItemType.SelectedIndex == 1)
                borr.type = "book";

            borr.item = ItemGuid;
            borr.person = PersonGuid;

            borr.from = dtFrom.Value;
            borr.to = dtFrom.Value;
            borr.returned = chbReturned.Checked;

        }

        private void SetItemsContext()
        {
            txtItem.AutoCompleteCustomSource.Clear();

            if (cbItemType.SelectedIndex == 0)
            {
                List<Items> itm = db.Items.ToList();
                itemList.Clear();
                for (int i = 0; i < itm.Count; i++)
                {
                    ItemVals vals = new ItemVals();
                    vals.ID = itm[i].Id;
                    vals.Name = itm[i].Name;
                    itemList.Add(vals);
                    txtItem.AutoCompleteCustomSource.Add(vals.Name.Trim() + " #" + i.ToString());
                }
            }
            else if (cbItemType.SelectedIndex == 1)
            {
                List<Books> books = db.Books.ToList();
                itemList.Clear();
                for (int i = 0; i < books.Count; i++)
                {
                    ItemVals vals = new ItemVals();
                    vals.ID = books[i].Id;
                    vals.Name = books[i].Name;
                    itemList.Add(vals);
                    txtItem.AutoCompleteCustomSource.Add(vals.Name.Trim() + " #" + i.ToString());
                }
            }
            
        }

        private void frmEditBorrowing_Load(object sender, EventArgs e)
        {
            

            cbItemType.Items.Clear();
            cbItemType.Items.Add(Lng.Get("Item"));
            cbItemType.Items.Add(Lng.Get("Book"));
            cbItemType.SelectedIndex = 0;

            contList = db.Contacts.ToList();
            for (int i = 0; i < contList.Count; i++)
            {
                txtPerson.AutoCompleteCustomSource.Add(contList[i].Name.Trim() + " " + contList[i].Surname.Trim() + " #" + i.ToString());
                txtPerson.AutoCompleteCustomSource.Add(contList[i].Surname.Trim() + " " + contList[i].Name.Trim() + " #" + i.ToString());
            }


            SetItemsContext();

            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now.AddDays(Properties.Settings.Default.DefaultBorrInterval);

            if (ID != Guid.Empty)
            {
                

                Borrowing borr = db.Borrowing.Find(ID);

                switch (borr.type.Trim())
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
                    Items itm = db.Items.Find(borr.item);
                    if (itm != null)
                        txtItem.Text = itm.Name.Trim();
                }
                else if (cbItemType.SelectedIndex == 1)
                {
                    Books book = db.Books.Find(borr.item);
                    
                    if (book != null)
                        txtItem.Text = book.Name.Trim();
                }

                

                Contacts person = db.Contacts.Find(borr.person);
                if (person != null)
                    txtPerson.Text = person.Name.Trim() + " " + person.Surname.Trim();

                dtFrom.Value = borr.from ?? DateTime.Now;
                dtTo.Value = borr.to ?? DateTime.Now;
                chbReturned.Checked = borr.returned ?? false;

                ItemGuid = borr.item ?? Guid.Empty;
                PersonGuid = borr.person ?? Guid.Empty;
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
                borr.Id = Guid.NewGuid();
            }

            FillBorr(ref borr);

            if (ID == Guid.Empty) db.Borrowing.Add(borr);
            db.SaveChanges();

            this.DialogResult = DialogResult.OK;
        }

        
        private void txtPerson_TextChanged(object sender, EventArgs e)
        {
            PersonGuid = Guid.Empty;
            /*if (txtPerson.Text.Length > 2)
            {
                PersonGuid = Guid.Empty;
                PersonName = "";
                databaseEntities db = new databaseEntities();

                contList = db.Contacts.Where(p => p.Name.Contains(txtPerson.Text) || p.Surname.Contains(txtPerson.Text)).ToList();


                //int cursor = txtPerson.SelectionStart;
                txtPerson.AutoCompleteCustomSource.Clear();
                int count = contList.Count;
                if (count > 20) count = 20;
                for (int i = 0; i < contList.Count; i++)
                {
                    txtPerson.AutoCompleteCustomSource.Add(contList[i].Name.Trim() + " " + contList[i].Surname.Trim());
                }
               // txtPerson.SelectionStart = cursor;

            }*/
        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            ItemGuid = Guid.Empty;
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
            }
                
            
        }
    }
}
