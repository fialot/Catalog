using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Katalog
{
    public partial class frmAddObject : Form
    {
        #region Variables

        Guid SelectedID = Guid.Empty;                       // Selected Object ID
        databaseEntities db = new databaseEntities();       // Database

        List<PInfo> objList = new List<PInfo>();            // Object List
        List<string> TypeList = new List<string>();
        List<string> CatList = new List<string>();


        #endregion

        #region Constructor

        public frmAddObject()
        {
            InitializeComponent();
        }

        #endregion
        
        #region Form Load

        /// <summary>
        /// ShowDialog Edit Objects
        /// </summary>
        /// <param name="text">Object string</param>
        /// <param name="objectForm">Is Object form indication</param>
        /// <returns></returns>
        public DialogResult ShowDialog(ref Guid ID)
        {
            SelectedID = ID;
            DialogResult res = base.ShowDialog();       // Base ShowDialog
            if (res == DialogResult.OK)
            {
                // ----- If selected Object -----
                if (cbSelectObject.SelectedIndex >= 0)
                {
                    ID = objList[cbSelectObject.SelectedIndex].ID;
                }
                // ----- Not selected -----
                else
                    ID = Guid.Empty;
                
            }
            return res;
        }

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAddObject_Load(object sender, EventArgs e)
        {
            RefreshItems();
            RefreshComboBox();
        }

        #endregion

        private void RefreshItems()
        {
            // ----- Get Object List -----
            if (cbType.Text != "" && cbCategory.Text != "")
                objList = db.Objects.Where(x => (x.Active == true) && (x.Type == cbType.Text) && (x.Category == cbCategory.Text)).Select(s => new PInfo { ID = s.ID, Name = s.Name }).ToList();
            else if (cbType.Text != "" )
                objList = db.Objects.Where(x => (x.Active == true) && (x.Type == cbType.Text)).Select(s => new PInfo { ID = s.ID, Name = s.Name }).ToList();
            else if (cbCategory.Text != "")
                objList = db.Objects.Where(x => (x.Active == true) && (x.Category == cbCategory.Text)).Select(s => new PInfo { ID = s.ID, Name = s.Name }).ToList();
            else
                objList = db.Objects.Where(x => x.Active == true).Select(s => new PInfo { ID = s.ID, Name = s.Name }).ToList();

            objList.Sort(new PInfoComparer());

            // ----- Add to ComboBox -----
            cbSelectObject.Items.Clear();
            foreach (var item in objList)
            {
                cbSelectObject.Items.Add(item.Name);
                if (item.ID == SelectedID)
                    cbSelectObject.SelectedIndex = cbSelectObject.Items.Count - 1;
            }
        }

        private void RefreshComboBox()
        {
            TypeList = new List<string>();
            CatList = new List<string>();

            // ----- Refresh type combobox -----
            
            var typeList = db.Objects.Where(x => x.Active == true).Select(s => s.Type).ToList();
            typeList.Sort();

            var lastItem = "";
            foreach (var item in typeList)
            {
                if (item != "")
                {
                    if (item != lastItem)
                    {
                        TypeList.Add(item);
                        lastItem = item;
                    }
                }
            }

            cbType.Items.Clear();
            cbType.Items.Add("");
            foreach (var item in TypeList) cbType.Items.Add(item);

            // ----- Refresh category combobox

            var catList = db.Objects.Where(x => x.Active == true).Select(s => s.Category).ToList();
            catList.Sort();
                        

            lastItem = "";
            foreach (var item in catList)
            {
                if (item != "")
                {
                    if (item != lastItem)
                    {
                        CatList.Add(item);
                        lastItem = item;
                    }
                }
            }
                 
            cbCategory.Items.Clear();
            cbCategory.Items.Add("");
            foreach (var item in CatList) cbCategory.Items.Add(item);
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshItems();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshItems();
        }
    }
}
