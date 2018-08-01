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
using Communications;
using myFunctions;

namespace Katalog
{
    public partial class frmEditPersonBorrowing : Form
    {

        #region Variables

        // ----- Database -----
        databaseEntities db = new databaseEntities();

        // ----- Item Lists -----
        Guid PersonID = Guid.Empty;                         // Person ID  
        List<Guid> ID = new List<Guid>();                   // List of Borrowing IDs
        List<Borrowing> borrList = new List<Borrowing>();   // Borrowing list

        // ----- Barcode -----
        Communication com = new Communication();            // Barcode reader communication
        string Barcode = "";                                // Readed barcode
        public delegate void MyDelegate(comStatus status);  // Communication delegate

        // ----- Status -----
        bool changed = false;

        #endregion

        #region Constructor

        public frmEditPersonBorrowing()
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

            list = db.Borrowing.Where(x => x.PersonID == ID && (x.Status == (short)LendStatus.Reserved || x.Status == (short)LendStatus.Lended)).Select(x => x.ID).ToList();

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
            this.ID = FindOtherBorrowings(PersonID);
            return base.ShowDialog();
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditPersonBorrowing_Load(object sender, EventArgs e)
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

            RefreshItems();
        }


        #endregion

        #region Form Closing
        
        /// <summary>
        /// Save changes to DB
        /// </summary>
        private void SaveChanges()
        {
            // ----- Save changes -----
            db.SaveChanges();

            // ----- No changes indicator -----
            changed = false;
        }

        /// <summary>
        /// Form Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditPersonBorrowing_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ----- Close Barcode reader connection -----
            com.Close();
        }

        /// <summary>
        /// Button OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // ----- Save changes to DB -----
            SaveChanges();

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

        #region Borrowings
        
        /// <summary>
        /// Update Items OLV
        /// </summary>
        private void UpdateOLV()
        {
            // ----- Column Name -----
            itName.AspectGetter = delegate (object x) {
                if (((Borrowing)x).Item != null)
                    return ((Borrowing)x).Item.Trim();
                return "";
            };
            // ----- Column Inventory number -----
            itInvNumber.AspectGetter = delegate (object x) {
                if (((Borrowing)x).ItemInvNum != null)
                    return ((Borrowing)x).ItemInvNum.Trim();
                return "";
            };
            // ----- Column From -----
            itFrom.AspectGetter = delegate (object x) {
                if (((Borrowing)x).From == null) return "";
                DateTime t = ((Borrowing)x).From ?? DateTime.Now;
                return t.ToShortDateString();
            };
            // ----- Column To -----
            itTo.AspectGetter = delegate (object x) {
                if (((Borrowing)x).To == null) return "";
                DateTime t = ((Borrowing)x).To ?? DateTime.Now;
                return t.ToShortDateString();
            };
            // ----- Column Status -----
            itStatus.ImageGetter = delegate (object x) {
                int status = ((Borrowing)x).Status ?? 1;
                if (status == (short)LendStatus.Returned)       // Returned
                    return 6;
                else if (status == (short)LendStatus.Reserved)  // Reserved
                    return 9;
                else if (status == (short)LendStatus.Canceled)  // Canceled
                    return 7;
                else return 10;                                 // Borrowed
            };
            itStatus.AspectGetter = delegate (object x) {
                int status = ((Borrowing)x).Status ?? 1;
                if (status == (short)LendStatus.Returned)       // Returned
                    return Lng.Get("Returned");
                else if (status == (short)LendStatus.Reserved)  // Reserved
                    return Lng.Get("Reserved");
                else if (status == (short)LendStatus.Canceled)  // Canceled
                    return Lng.Get("Canceled");
                else return Lng.Get("Borrowed");                // Borrowed
            };
            // ----- Column Note -----
            itNote.AspectGetter = delegate (object x) {
                if (((Borrowing)x).Note != null)
                    return ((Borrowing)x).Note.Trim();
                return "";
            };

            // ----- Set model to OLV -----
            olvItem.SetObjects(borrList);
        }

        /// <summary>
        /// Refresh Items from DB to OLV
        /// </summary>
        private void RefreshItems()
        {
            this.ID = FindOtherBorrowings(PersonID);

            borrList.Clear();
            foreach (var item in this.ID)
            {
                var borr = db.Borrowing.Find(item);
                borrList.Add(borr);
            }

            UpdateOLV();
        }
        
        /// <summary>
        /// OLV selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvItem_SelectionChanged(object sender, EventArgs e)
        {
            if (olvItem.SelectedObjects.Count > 0)
            {
                btnReturn.Enabled = true;
            }
            else btnReturn.Enabled = false;
        }

        /// <summary>
        /// OLV color row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvItem_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            var itm = (Borrowing)e.Model;
            DateTime now = DateTime.Now;
            if (itm.Status == 2 || itm.Status == 3)
                e.Item.ForeColor = Color.Gray;
            else if ((itm.To ?? now) < now)
                e.Item.ForeColor = Color.Red;
            else
                e.Item.ForeColor = Color.Black;
        }

        #endregion
        
        #region Status

        /// <summary>
        /// Button Lend
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLend_Click(object sender, EventArgs e)
        {
            // ----- Lend selected -----
            if (olvItem.SelectedObjects.Count > 0)
            {
                changed = true;
                for (int i = olvItem.SelectedObjects.Count - 1; i >= 0; i--)
                {
                    ((Borrowing)(olvItem.SelectedObjects[i])).Status = (short)LendStatus.Lended;
                    ((Borrowing)(olvItem.SelectedObjects[i])).To = DateTime.Now.AddMonths(1);
                }
                UpdateOLV();
                olvItem.SelectedIndex = -1;
            }
            // ----- Lend new -----
            else
            {
                // ----- Check changes -----
                if (changed)
                {
                    if (Dialogs.ShowQuest(Lng.Get("saveChangesBeforeLending", "Before lending new items you must save changes to database. Save changes?"), Lng.Get("SaveChanges", "Save changes?")) == DialogResult.No)
                        return;
                }

                // ----- Close Barcode reader connection -----
                com.Close();

                // ----- Save changes to DB -----
                SaveChanges();

                // ----- Show Lending Dialog -----
                frmEditBorrowing form = new frmEditBorrowing();
                form.ShowPersonDialog(PersonID);

                RefreshItems();

                // ----- Start Barcode reader Connection -----
                try
                {
                    com.ConnectSP(Properties.Settings.Default.scanCOM);
                }
                catch { }
            }
        }

        /// <summary>
        /// Button return
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (olvItem.SelectedObjects.Count > 0)
            {
                changed = true;
                for (int i = olvItem.SelectedObjects.Count - 1; i >= 0; i--)
                {
                    // ----- Lended -----
                    if (((Borrowing)(olvItem.SelectedObjects[i])).Status == (short)LendStatus.Lended)
                    {
                        ((Borrowing)(olvItem.SelectedObjects[i])).Status = (short)LendStatus.Returned;
                        ((Borrowing)(olvItem.SelectedObjects[i])).To = DateTime.Now;
                    }
                    // ----- Reserved -----
                    else if (((Borrowing)(olvItem.SelectedObjects[i])).Status == (short)LendStatus.Reserved)
                        ((Borrowing)(olvItem.SelectedObjects[i])).Status = (short)LendStatus.Canceled;
                }
                UpdateOLV();
            }
        }

        /// <summary>
        /// Button Cancel All Reservations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            changed = true;
            foreach (var item in borrList)
            {
                if (item.Status == (short)LendStatus.Reserved) // Reserved
                    item.Status = (short)LendStatus.Canceled;
            }
            UpdateOLV();
        }

        /// <summary>
        /// Button Return All
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturnAll_Click(object sender, EventArgs e)
        {
            changed = true;

            foreach (var item in borrList)
            {
                if (item.Status == (short)LendStatus.Lended)       // Lended
                {
                    item.Status = (short)LendStatus.Returned;
                    item.To = DateTime.Now;
                }
            }
            UpdateOLV();
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

        #region Print

        private void btnPrintLend_Click(object sender, EventArgs e)
        {
            PrintPDF.PrintTable(lblPerson.Text + " - " + Lng.Get("Borrowed"), global.GetTable(borrList));
        }

        #endregion

    }
}
