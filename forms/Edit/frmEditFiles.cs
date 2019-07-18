using System;
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
        #region Variables

        string FileText = "";                               // File/Object text string
        string RelativePath = "";                           // Path for relative paths
        List<FInfo> FileList = new List<FInfo>();           // File list
        List<Objects> ObjectList = new List<Objects>();     // Object list

        bool objectForm = false;                            // Is Object form indication

        #endregion

        #region Constructor

        public frmEditFiles()
        {
            InitializeComponent();
        }

        #endregion

        #region Load Form

        /// <summary>
        /// ShowDialog Edit Files
        /// </summary>
        /// <param name="text">File text string</param>
        /// <param name="relativePath">Relative path</param>
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

        /// <summary>
        /// ShowDialog Edit Objects
        /// </summary>
        /// <param name="text">Object string</param>
        /// <param name="objectForm">Is Object form indication</param>
        /// <returns></returns>
        public DialogResult ShowDialog(ref string text, bool objectForm)
        {
            FileText = text;
            this.objectForm = objectForm;
            DialogResult res = base.ShowDialog();       // Base ShowDialog
            if (res == DialogResult.OK)
                text = FileText;
            return res;
        }

        /// <summary>
        /// Load Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEditFiles_Load(object sender, EventArgs e)
        {
            // ----- Is Object form -----
            if (objectForm)
            {
                this.Text = Lng.Get("Objects");
                this.Icon = Properties.Resources.IconObject;

                ObjectList = global.GetObjectsFromText(FileText);
                UpdateOLVObject();                  // Update OLV
            }
            // ----- Is Files Form -----
            else
            {
                FileList = global.GetFInfoList(FileText);
                UpdateOLV();                        // Update OLV
            }   
        }

        #endregion

        #region OLV

        /// <summary>
        /// Update Files OLV
        /// </summary>
        private void UpdateOLV()
        {
            fileName.ImageGetter = delegate (object x)
            {
                try
                {
                    Icon ico;
                    string path = ((FInfo)x).Path;
                    if (File.Exists(path))
                        ico = IconReader.GetFileIcon(path, IconReader.IconSize.Small, false);   //ico = Icon.ExtractAssociatedIcon(path);
                    else if (Directory.Exists(path))
                        ico = IconReader.GetFolderIcon(path, IconReader.IconSize.Small, IconReader.FolderType.Open);
                    else
                        ico = IconReader.GetFileIcon(RelativePath + Path.DirectorySeparatorChar + path, IconReader.IconSize.Small, false);
                    //ico = Icon.ExtractAssociatedIcon(RelativePath + Path.DirectorySeparatorChar + path);

                    //Icon ico = Icon.ExtractAssociatedIcon(((FInfo)x).Path);
                    //ico = IconReader.GetFileIcon(((FInfo)x).Path, IconReader.IconSize.Small, false);
                    
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
                
        /// <summary>
        /// Update Objects OLV
        /// </summary>
        private void UpdateOLVObject()
        {
            fileName.ImageGetter = delegate (object x)
            {
                Image genImg = new Icon(Properties.Resources.IconObject, 16, 16).ToBitmap();
                try
                {
                    Image img = Conv.ByteArrayToImage(((Objects)x).Image);
                    if (img == null) return genImg;
                    return (Image)(new Bitmap(img, new Size(16, 16))); ;
                }
                catch
                {
                    return genImg;
                }
            };
            fileName.AspectGetter = delegate (object x) {
                return ((Objects)x).Name;
            };
            filePath.AspectGetter = delegate (object x) {
                return ((Objects)x).Folder;
            };
            fileVersion.AspectGetter = delegate (object x) {
                return ((Objects)x).Version;
            };
            fileGroup.AspectGetter = delegate (object x) {
                return ((Objects)x).Category;
            };
            fileDescription.AspectGetter = delegate (object x) {
                return ((Objects)x).Description;
            };

            this.fileName.GroupKeyGetter = delegate (object x) { return ((Objects)x).Category; };
            this.filePath.GroupKeyGetter = delegate (object x) { return ((Objects)x).Category; };
            this.fileVersion.GroupKeyGetter = delegate (object x) { return ((Objects)x).Category; };
            this.fileGroup.GroupKeyGetter = delegate (object x) { return ((Objects)x).Category; };
            this.fileDescription.GroupKeyGetter = delegate (object x) { return ((Objects)x).Category; };

            olvFiles.ShowGroups = true;

            olvFiles.SetObjects(ObjectList);
        }

        /// <summary>
        /// OLV Double Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void olvFiles_DoubleClick(object sender, EventArgs e)
        {
            if (olvFiles.SelectedIndex >= 0)                    // If selected Item
            {
                // ----- Objects -----
                if (objectForm)
                {
                    frmEditObjects form = new frmEditObjects();
                    form.ShowDialog(((Objects)olvFiles.SelectedObject).ID);
                    form.Dispose();
                    UpdateOLVObject();                  // Update OLV
                }
                // ----- Files -----
                else
                {
                    FInfo info = (FInfo)olvFiles.SelectedObject;

                    try
                    {
                        if (File.Exists(info.Path) || Directory.Exists(info.Path))
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

        #endregion

        #region Edit Buttons

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // ----- Objects -----
            if (objectForm)
            {
                frmAddObject form = new frmAddObject();
                Guid ID = Guid.Empty;
                form.ShowDialog(ref ID);
                if (ID != Guid.Empty)
                {
                    databaseEntities db = new databaseEntities();       // Database
                    var obj = db.Objects.Find(ID);
                    if (obj != null)
                        ObjectList.Add(obj);
                    FileText = global.GetTextFromObjects(ObjectList);

                    UpdateOLVObject();
                }
            }
            // ----- Files -----
            else
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
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (olvFiles.SelectedObject != null)
            {
                // ----- Objects -----
                if (objectForm)
                {
                    frmAddObject form = new frmAddObject();
                    Guid ID = ((Objects)olvFiles.SelectedObject).ID;
                    string OrigID = ID.ToString();
                    form.ShowDialog(ref ID);
                    if (ID != Guid.Empty)
                    {
                        FileText = FileText.Replace(OrigID, ID.ToString());
                        FileText = FileText.Replace(";;", ";");
                        ObjectList = global.GetObjectsFromText(FileText);

                        UpdateOLVObject();
                    }
                }
                // ----- Files -----
                else
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
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int count = olvFiles.SelectedObjects.Count;
            // ----- If selected 1 Item -----
            if (count == 1)                    
            {
                // ----- Objects -----
                if (objectForm)
                {
                    Objects info = (Objects)olvFiles.SelectedObject;
                    string itemID = info.ID.ToString();

                    if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + info.Name + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        FileText = FileText.Replace(itemID, "");
                        FileText = FileText.Replace(";;", ";");
                        if (FileText == ";") FileText = "";

                        ObjectList = global.GetObjectsFromText(FileText);

                        UpdateOLVObject();
                    }
                }
                // ----- Files -----
                else
                {
                    FInfo info = (FInfo)olvFiles.SelectedObject;
                    string text = global.FInfoToText(info);

                    if (Dialogs.ShowQuest(Lng.Get("DeleteItem", "Really delete item") + " \"" + info.Name + "\"?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        FileText = FileText.Replace(text, "");
                        FileText = FileText.Replace(";;", ";");
                        if (FileText == ";") FileText = "";

                        FileList = global.GetFInfoList(FileText);

                        UpdateOLV();
                    }
                }
            }
            // ----- If selected more Items -----
            else if (count > 1)                 // If selected Item
            {
                // ----- Objects -----
                if (objectForm)
                {
                    if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items") + " (" + count.ToString() + ")?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        foreach (var item in olvFiles.SelectedObjects) // Find Object
                        {
                            string itemID = ((Objects)item).ID.ToString();
                            FileText = FileText.Replace(itemID, "");
                            FileText = FileText.Replace(";;", ";");
                            if (FileText == ";") FileText = "";
                        }
                        ObjectList = global.GetObjectsFromText(FileText);
                        UpdateOLVObject();
                    }
                }
                // ----- Files -----
                else
                {
                   
                    if (Dialogs.ShowQuest(Lng.Get("DeleteItems", "Really delete selected items") + " (" + count.ToString() + ")?", Lng.Get("Delete")) == DialogResult.Yes)
                    {
                        foreach (var item in olvFiles.SelectedObjects) // Find Object
                        {
                            string text = global.FInfoToText((FInfo)item);
                            FileText = FileText.Replace(text, "");
                            FileText = FileText.Replace(";;", ";");
                            if (FileText == ";") FileText = "";
                        }
                        FileList = global.GetFInfoList(FileText);
                        UpdateOLV();
                    }
                }
            }
        }

        #endregion

        
    }
}
