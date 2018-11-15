﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using myFunctions;
using Etier.IconHelper;
using System.Diagnostics;

namespace Katalog
{
    public partial class frmEditFiles : Form
    {

        string FileText = "";
        string RelativePath = "";
        List<FInfo> FileList = new List<FInfo>();

        public frmEditFiles()
        {
            InitializeComponent();
        }


        /// <summary>
        /// ShowDialog with ID (Edit)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DialogResult ShowDialog(ref string text, string relativePath)
        {
            FileText = text;               
            RelativePath = relativePath;
            DialogResult res = base.ShowDialog();       // Base ShowDialog
            if (res == DialogResult.OK)
                text = FileText;
            return res;

        }


        private void frmEditFiles_Load(object sender, EventArgs e)
        {
            FileList = global.GetFInfoList(FileText);

            UpdateOLV();
        }

        private void UpdateOLV()
        {

            
            fileName.ImageGetter = delegate (object x)
            {
                try
                {
                    Icon ico = IconReader.GetFileIcon(((FInfo)x).Path, IconReader.IconSize.Small, false);
                    return ico.ToBitmap();
                }
                catch
                {
                    return null;
                }
            };
            fileName.AspectGetter = delegate (object x) {
                return ((FInfo)x).Name;
            };
            filePath.AspectGetter = delegate (object x) {
                return ((FInfo)x).Path;
            };
            fileVersion.AspectGetter = delegate (object x) {
                return ((FInfo)x).Version;
            };
            fileGroup.AspectGetter = delegate (object x) {
                return ((FInfo)x).Group;
            };
            fileDescription.AspectGetter = delegate (object x) {
                return ((FInfo)x).Description;
            };

            this.fileName.GroupKeyGetter = delegate (object x) { return ((FInfo)x).Group; };
            this.filePath.GroupKeyGetter = delegate (object x) { return ((FInfo)x).Group; };
            this.fileVersion.GroupKeyGetter = delegate (object x) { return ((FInfo)x).Group; };
            this.fileGroup.GroupKeyGetter = delegate (object x) { return ((FInfo)x).Group; };
            this.fileDescription.GroupKeyGetter = delegate (object x) { return ((FInfo)x).Group; };

            olvFiles.ShowGroups = true;

            olvFiles.SetObjects(FileList);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string text = "";
            frmEditFile form = new frmEditFile();
            form.ShowDialog(ref text, RelativePath);
            if (text != "")
            {
                if (FileText != "") FileText += ";";
                FileText += text;
                FileList = global.GetFInfoList(FileText);

                UpdateOLV();
            }
            

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (olvFiles.SelectedObject != null)
            {
                FInfo info = (FInfo)olvFiles.SelectedObject;
                string text = global.FInfoToText(info);
                string textOgig = text;
                frmEditFile form = new frmEditFile();
                if (form.ShowDialog(ref text, RelativePath) == DialogResult.OK)
                {
                    FileText = FileText.Replace(textOgig, text);
                    FileText = FileText.Replace(";;", ";");
                    FileList = global.GetFInfoList(FileText);

                    UpdateOLV();
                }
                
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (olvFiles.SelectedIndex >= 0)                    // If selected Item
            {                                                   // Find Object
                FInfo info = (FInfo)olvFiles.SelectedObject;
                string text = global.FInfoToText(info);

                if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + info.Name + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    FileText = FileText.Replace(text, "");
                    FileText = FileText.Replace(";;", ";");

                    FileList = global.GetFInfoList(FileText);

                    UpdateOLV();
                }
            }
            else if (olvFiles.SelectedObjects != null)                 // If selected Item
            {
                int count = olvFiles.SelectedObjects.Count;
                if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items") + " (" + count.ToString() + ")?", Lng.Get("Delete")) == DialogResult.Yes)
                {
                    foreach (var item in olvFiles.SelectedObjects) // Find Object
                    {
                        string text = global.FInfoToText((FInfo)item);
                        FileText = FileText.Replace(text, "");
                        FileText = FileText.Replace(";;", ";");
                    }
                    FileList = global.GetFInfoList(FileText);
                    UpdateOLV();
                }
            }
        }

        private void olvFiles_DoubleClick(object sender, EventArgs e)
        {
            if (olvFiles.SelectedIndex >= 0)                    // If selected Item
            {
                FInfo info = (FInfo)olvFiles.SelectedObject;
                
                try
                {
                    if (File.Exists(info.Path))
                        Process.Start(info.Path);
                    else
                        Process.Start(RelativePath + Path.DirectorySeparatorChar + info.Path);
                }
                catch (Exception Err)
                {
                    Dialogs.ShowErr(Err.Message, Lng.Get("Error"));
                }
                
            }
                
        }
    }
}