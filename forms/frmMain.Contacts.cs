using BrightIdeasSoftware;
using myFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Katalog
{

    public partial class frmMain
    {

        #region OLV

        string GetOnlyValue(string text)
        {
            string res = "";
            string[] items = text.Split(new string[] { ";" }, StringSplitOptions.None);
            for (int i = 0; i < items.Length; i++)
            {
                string[] vals = items[i].Split(new string[] { "," }, StringSplitOptions.None);
                if (res != "") res += ", ";
                res += vals[0];
            }
            return res;
        }

        /// <summary>
        /// Update Contacts ObjectListView
        /// </summary>
        void UpdateConOLV()
        {
            databaseEntities db = new databaseEntities();

            List<Contacts> con;

            if (chbShowUnactivCon.Checked)
                con = db.Contacts.ToList();
            else
                con = db.Contacts.Where(p => (p.Active ?? true) == true).ToList();

            conFastTags.Renderer = new ImageRenderer();
            conFastTags.AspectGetter = delegate (object x) {
                if (x == null) return "";
                List<int> ret = new List<int>();
                FastFlags flag = (FastFlags)((Contacts)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) ret.Add(0);
                if (flag.HasFlag(FastFlags.FLAG2)) ret.Add(1);
                if (flag.HasFlag(FastFlags.FLAG3)) ret.Add(2);
                if (flag.HasFlag(FastFlags.FLAG4)) ret.Add(3);
                if (flag.HasFlag(FastFlags.FLAG5)) ret.Add(4);
                if (flag.HasFlag(FastFlags.FLAG6)) ret.Add(5);

                return ret;
            };
            conFastTagsNum.AspectGetter = delegate (object x) {
                if (x == null) return "";
                string res = "";
                FastFlags flag = (FastFlags)((Contacts)x).FastTags;
                if (flag.HasFlag(FastFlags.FLAG1)) res += "1";
                if (flag.HasFlag(FastFlags.FLAG2)) res += "2";
                if (flag.HasFlag(FastFlags.FLAG3)) res += "3";
                if (flag.HasFlag(FastFlags.FLAG4)) res += "4";
                if (flag.HasFlag(FastFlags.FLAG5)) res += "5";
                if (flag.HasFlag(FastFlags.FLAG6)) res += "6";
                return res;
            };
            conName.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Contacts)x).Name;
            };
            conSurname.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Contacts)x).Surname;
            };
            conNick.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Contacts)x).Nick;
            };
            conPhone.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return GetOnlyValue(((Contacts)x).Phone);
            };
            conEmail.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return GetOnlyValue(((Contacts)x).Email);
            };
            conAddress.AspectGetter = delegate (object x) {
                if (x == null) return "";
                string address = ((Contacts)x).Street;
                string city = ((Contacts)x).City;
                string country = ((Contacts)x).Country;

                if ((city != null && city != "") && (address != null && address != ""))
                    address += ", ";
                address += city;

                if ((country != null && country != "") && (address != null && address != ""))
                    address += ", ";
                address += country;
                return address;
            };
            conCompany.AspectGetter = delegate (object x) {
                if (x == null) return "";
                return ((Contacts)x).Company;
            };
            olvContacts.SetObjects(con);
        }

        /// <summary>
        /// OLV Contacts selected index change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableEditItems();
        }


        private void olvContacts_FormatRow(object sender, FormatRowEventArgs e)
        {
            if (e.Model == null) return;

            Contacts itm = (Contacts)e.Model;
            DateTime now = DateTime.Now;
            if ((itm.Active ?? true) == false)
                e.Item.ForeColor = Color.Gray;
            else
                e.Item.ForeColor = Color.Black;
        }

        private void chbUnactivateContacts_CheckedChanged(object sender, EventArgs e)
        {
            UpdateConOLV();
        }

        #endregion


        #region Items


        private void mnuCSetTag_Click(object sender, EventArgs e)
        {
            short tag = 0;
            if (((ToolStripItem)sender).Tag == "1") tag = 0x01;
            if (((ToolStripItem)sender).Tag == "2") tag = 0x02;
            if (((ToolStripItem)sender).Tag == "3") tag = 0x04;
            if (((ToolStripItem)sender).Tag == "4") tag = 0x08;
            if (((ToolStripItem)sender).Tag == "5") tag = 0x10;
            if (((ToolStripItem)sender).Tag == "6") tag = 0x20;

            SetTagItem(tag);
        }

        private void mnuCActive_Click(object sender, EventArgs e)
        {
            
        }

        #endregion

    }
}
