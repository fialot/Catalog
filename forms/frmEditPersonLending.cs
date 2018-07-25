using Communications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCPClient;

namespace Katalog
{
    public partial class frmEditPersonLending : Form
    {

        #region Variables

        databaseEntities db = new databaseEntities();

        // ----- Item Lists -----
        Guid PersonID = Guid.Empty;                         // Person ID  
        List<Guid> ID = new List<Guid>();                   // List of Borrowing IDs
        List<Lending> lendList = new List<Lending>();       // Lending list

        // ----- Barcode -----
        Communication com = new Communication();            // Barcode reader communication
        string Barcode = "";                                // Readed barcode
        public delegate void MyDelegate(comStatus status);  // Communication delegate

        #endregion

        #region Constructor

        public frmEditPersonLending()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Find other Borrowings to fill Item list
        /// </summary>
        /// <param name="ID">Main ID</param>
        /// <returns>Borrowings list</returns>
        private List<Guid> FindOtherBorrowings(Guid ID)
        {
            databaseEntities db = new databaseEntities();

            List<Guid> list = new List<Guid>();

            list = db.Lending.Where(x => x.PersonID == ID && (x.Status == 0 || x.Status == 1)).Select(x => x.ID).ToList();

            return list;
        }

        /// <summary>
        /// Show dialog with Person ID
        /// </summary>
        /// <param name="ID">Item ID</param>
        /// <returns></returns>
        public DialogResult ShowDialog(Guid ID)
        {
            PersonID = ID;
            this.ID = FindOtherBorrowings(ID);
            return base.ShowDialog();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditPersonLending_Load(object sender, EventArgs e)
        {

            // ----- Create connection to barcode reader -----
            com.ReceivedData += new ReceivedEventHandler(DataReceive);
            try
            {
                com.ConnectSP(Properties.Settings.Default.scanCOM);
            }
            catch { }

            // ----- Find selected Contact -----
            Contacts person = db.Contacts.Find(PersonID);
            if (person == null) this.DialogResult = DialogResult.Cancel;

            // ----- Fill Contact -----
            lblPerson.Text = person.Name.Trim() + " " + person.Surname.Trim();
            lblPersonNum.Text = Lng.Get("PersonNum", "Person number") + ": " + person.PersonCode.Trim();

            foreach (var item in this.ID)
            {
                var lend = db.Lending.Find(item);
                lendList.Add(lend);
            }
            
            UpdateOLV();
        }

        #endregion

        #region Form Closing

        /// <summary>
        /// Form Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditPersonLending_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ----- Close Barcode reader connection -----
            com.Close();
        }

        /// <summary>
        /// Refresh Available Items
        /// </summary>
        /// <param name="list"></param>
        private void RefreshAvailableItems()
        {

            databaseEntities db = new databaseEntities();
            List<short?> borr = new List<short?>();

            foreach (var itm in lendList)
            {
                borr = db.Lending.Where(p => (p.ItemID == itm.ItemID) && p.ItemType.Contains(itm.ItemType.ToString()) && (p.Status ?? 1) != 2).Select(c => c.ItemNum).ToList();

                if (itm.ItemType == "item")
                {
                    Items item = db.Items.Find(itm.ItemID);
                    if (item != null)
                        item.Available = (short)((item.Count ?? 1) - borr.Count);
                }
                else if (itm.ItemType == "book")
                {
                    Books book = db.Books.Find(itm.ItemID);
                    if (book != null)
                        book.Available = (short)((book.Count ?? 1) - borr.Count);
                }
                else if (itm.ItemType == "boardgame")
                {
                    Boardgames board = db.Boardgames.Find(itm.ItemID);
                    if (board != null)
                        board.Available = (short)((board.Count ?? 1) - borr.Count);
                }
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Button OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            db.SaveChanges();

            RefreshAvailableItems();

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

        #endregion

        /// <summary>
        /// Update Items OLV
        /// </summary>
        private void UpdateOLV()
        {
            // ----- Column Name -----
            itName.AspectGetter = delegate (object x) {
                return global.GetLendingItemName(((Lending)x).ItemType.Trim(), ((Lending)x).ItemID ?? Guid.Empty);
            };
            // ----- Column Type -----
            itType.AspectGetter = delegate (object x) {
                switch (((Lending)x).ItemType.Trim())
                {
                    case "item":
                        return Lng.Get("Item");
                    case "book":
                        return Lng.Get("Book");
                    case "boardgame":
                        return Lng.Get("Boardgame", "Board game");
                }
                return Lng.Get("Unknown");
            };
            // ----- Column Inventory number -----
            itInvNumber.AspectGetter = delegate (object x) {
                if (((Lending)x).ItemInvNum != null)
                    return ((Lending)x).ItemInvNum.Trim();
                return "";
            };
            // ----- Column From -----
            itFrom.AspectGetter = delegate (object x) {
                if (((Lending)x).From == null) return "";
                DateTime t = ((Lending)x).From ?? DateTime.Now;
                return t.ToShortDateString();
            };
            // ----- Column To -----
            itTo.AspectGetter = delegate (object x) {
                if (((Lending)x).To == null) return "";
                DateTime t = ((Lending)x).To ?? DateTime.Now;
                return t.ToShortDateString();
            };
            // ----- Column Status -----
            itStatus.ImageGetter = delegate (object x) {
                int status = ((Lending)x).Status ?? 1;
                if (status == 2)        // Returned
                    return 6;
                else if (status == 0)   // Reserved
                    return 9;
                else if (status == 3)   // Canceled
                    return 7;
                else return 10;         // Borrowed
            };
            itStatus.AspectGetter = delegate (object x) {
                int status = ((Lending)x).Status ?? 1;
                if (status == 2)        // Returned
                    return Lng.Get("Returned");
                else if (status == 0)   // Reserved
                    return Lng.Get("Reserved");
                else if (status == 3)   // Canceled
                    return Lng.Get("Canceled");
                else return Lng.Get("Borrowed"); // Borrowed
            };
            // ----- Column Note -----
            itNote.AspectGetter = delegate (object x) {
                if (((Lending)x).Note != null)
                    return ((Lending)x).Note.Trim();
                return "";
            };

            // ----- Set model to OLV -----
            olvItem.SetObjects(lendList);
        }

        #region Barcode

        /// <summary>
        /// Data receive delegate
        /// </summary>
        /// <param name="source"></param>
        /// <param name="status"></param>
        private void DataReceive(object source, comStatus status)
        {
            lblPerson.Invoke(new MyDelegate(DataProcess), new Object[] { status }); //BeginInvoke

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
            /*// ----- Fill Person by Barcode -----
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
            }*/
            Barcode = "";
        }


        #endregion

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            foreach (var item in lendList)
            {
                if (item.Status == 0) // Reserved
                    item.Status = 3;
            }
            UpdateOLV();
        }

        private void btnReturnAll_Click(object sender, EventArgs e)
        {

            foreach (var item in lendList)
            {
                if (item.Status == 1)       // Lended
                {  
                    item.Status = 2;
                    item.To = DateTime.Now;
                }
            }
            UpdateOLV();
        }

        private void btnLend_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (olvItem.SelectedObjects.Count > 0)
            {
                for (int i = olvItem.SelectedObjects.Count - 1; i >= 0; i--)
                {
                    // ----- Lended -----
                    if (((Lending)(olvItem.SelectedObjects[i])).Status == 1)
                    {
                        ((Lending)(olvItem.SelectedObjects[i])).Status = 2;
                        ((Lending)(olvItem.SelectedObjects[i])).To = DateTime.Now;
                    }
                    // ----- Reserved -----
                    else if (((Lending)(olvItem.SelectedObjects[i])).Status == 0) ((Lending)(olvItem.SelectedObjects[i])).Status = 3;
                }
                UpdateOLV();
            }
        }
    }
}
