namespace BYML_Editor
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openXMLDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLittleEndianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBigEndianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yaz0CompressLittleEndianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decryptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.obfuscationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deobfuscateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reobfuscateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableDeletingTempFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTextboxToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openyamlFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bymltext = new System.Windows.Forms.TextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveyaz0FileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openxmlFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.gamefolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.opendictDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.createToolStripMenuItem1,
            this.saveToolStripMenuItem1,
            this.clearToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openXMLDisplayToolStripMenuItem});
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(108, 22);
            this.openToolStripMenuItem1.Text = "Open";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.openToolStripMenuItem.Text = "Open (YAML display)";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenYAMLDisplayToolStripMenuItem_Click);
            // 
            // openXMLDisplayToolStripMenuItem
            // 
            this.openXMLDisplayToolStripMenuItem.Name = "openXMLDisplayToolStripMenuItem";
            this.openXMLDisplayToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.openXMLDisplayToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.openXMLDisplayToolStripMenuItem.Text = "Open (XML display)";
            this.openXMLDisplayToolStripMenuItem.Click += new System.EventHandler(this.OpenXMLDisplayToolStripMenuItem_Click);
            // 
            // createToolStripMenuItem1
            // 
            this.createToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.createXMLToolStripMenuItem});
            this.createToolStripMenuItem1.Name = "createToolStripMenuItem1";
            this.createToolStripMenuItem1.Size = new System.Drawing.Size(108, 22);
            this.createToolStripMenuItem1.Text = "Create";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.createToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.createToolStripMenuItem.Text = "Create (YAML display)";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.CreateYAMLToolStripMenuItem_Click);
            // 
            // createXMLToolStripMenuItem
            // 
            this.createXMLToolStripMenuItem.Name = "createXMLToolStripMenuItem";
            this.createXMLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.N)));
            this.createXMLToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.createXMLToolStripMenuItem.Text = "Create (XML display)";
            this.createXMLToolStripMenuItem.Click += new System.EventHandler(this.CreateXMLToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveLittleEndianToolStripMenuItem,
            this.saveBigEndianToolStripMenuItem});
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(108, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            // 
            // saveLittleEndianToolStripMenuItem
            // 
            this.saveLittleEndianToolStripMenuItem.Name = "saveLittleEndianToolStripMenuItem";
            this.saveLittleEndianToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveLittleEndianToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.saveLittleEndianToolStripMenuItem.Text = "Save (little endian)";
            this.saveLittleEndianToolStripMenuItem.Click += new System.EventHandler(this.SaveLittleEndianToolStripMenuItem_Click);
            // 
            // saveBigEndianToolStripMenuItem
            // 
            this.saveBigEndianToolStripMenuItem.Name = "saveBigEndianToolStripMenuItem";
            this.saveBigEndianToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.saveBigEndianToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.saveBigEndianToolStripMenuItem.Text = "Save (big endian)";
            this.saveBigEndianToolStripMenuItem.Click += new System.EventHandler(this.SaveBigEndianToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.ShowShortcutKeys = false;
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yaz0CompressLittleEndianToolStripMenuItem,
            this.decryptToolStripMenuItem,
            this.obfuscationToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // yaz0CompressLittleEndianToolStripMenuItem
            // 
            this.yaz0CompressLittleEndianToolStripMenuItem.Name = "yaz0CompressLittleEndianToolStripMenuItem";
            this.yaz0CompressLittleEndianToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.yaz0CompressLittleEndianToolStripMenuItem.Text = "Yaz0 Compress (little endian)";
            this.yaz0CompressLittleEndianToolStripMenuItem.Click += new System.EventHandler(this.Yaz0CompressLittleEndianToolStripMenuItem_Click);
            // 
            // decryptToolStripMenuItem
            // 
            this.decryptToolStripMenuItem.Name = "decryptToolStripMenuItem";
            this.decryptToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.decryptToolStripMenuItem.Text = "Decrypt Nisasyst files";
            this.decryptToolStripMenuItem.Click += new System.EventHandler(this.DecryptToolStripMenuItem_Click);
            // 
            // obfuscationToolStripMenuItem
            // 
            this.obfuscationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deobfuscateToolStripMenuItem,
            this.reobfuscateToolStripMenuItem});
            this.obfuscationToolStripMenuItem.Name = "obfuscationToolStripMenuItem";
            this.obfuscationToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.obfuscationToolStripMenuItem.Text = "Obfuscation";
            // 
            // deobfuscateToolStripMenuItem
            // 
            this.deobfuscateToolStripMenuItem.Name = "deobfuscateToolStripMenuItem";
            this.deobfuscateToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.deobfuscateToolStripMenuItem.Text = "Deobfuscate";
            this.deobfuscateToolStripMenuItem.Click += new System.EventHandler(this.DeobfuscateToolStripMenuItem_Click);
            // 
            // reobfuscateToolStripMenuItem
            // 
            this.reobfuscateToolStripMenuItem.Name = "reobfuscateToolStripMenuItem";
            this.reobfuscateToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.reobfuscateToolStripMenuItem.Text = "Reobfuscate";
            this.reobfuscateToolStripMenuItem.Click += new System.EventHandler(this.ReobfuscateToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disableDeletingTempFolderToolStripMenuItem,
            this.saveTextboxToFileToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Visible = false;
            // 
            // disableDeletingTempFolderToolStripMenuItem
            // 
            this.disableDeletingTempFolderToolStripMenuItem.Name = "disableDeletingTempFolderToolStripMenuItem";
            this.disableDeletingTempFolderToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.disableDeletingTempFolderToolStripMenuItem.Text = "Disable deleting temporary folder";
            this.disableDeletingTempFolderToolStripMenuItem.Click += new System.EventHandler(this.DisableDeletingTempFolderToolStripMenuItem_Click);
            // 
            // saveTextboxToFileToolStripMenuItem
            // 
            this.saveTextboxToFileToolStripMenuItem.Name = "saveTextboxToFileToolStripMenuItem";
            this.saveTextboxToFileToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.saveTextboxToFileToolStripMenuItem.Text = "Save textbox to file";
            this.saveTextboxToFileToolStripMenuItem.Click += new System.EventHandler(this.SaveTextboxToFileToolStripMenuItem_Click);
            // 
            // openyamlFileDialog
            // 
            this.openyamlFileDialog.Filter = "BYML Files|*.byml;*.sbyml;*.bprm|All files (*.*)|*.*\"";
            this.openyamlFileDialog.Title = "Open Your BYML File";
            // 
            // bymltext
            // 
            this.bymltext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bymltext.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bymltext.Location = new System.Drawing.Point(12, 27);
            this.bymltext.Multiline = true;
            this.bymltext.Name = "bymltext";
            this.bymltext.ReadOnly = true;
            this.bymltext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.bymltext.Size = new System.Drawing.Size(776, 411);
            this.bymltext.TabIndex = 1;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "BYML Files|*.byml;*.bprm|All files (*.*)|*.*\"";
            this.saveFileDialog.Title = "Save Your BYML File";
            // 
            // saveyaz0FileDialog
            // 
            this.saveyaz0FileDialog.Filter = "Compressed BYML File|*.sbyml|All files (*.*)|*.*\"";
            // 
            // openxmlFileDialog
            // 
            this.openxmlFileDialog.Filter = "BYML Files|*.byml;*.bprm|All files (*.*)|*.*\"";
            this.openxmlFileDialog.Title = "Open Your BYML File";
            // 
            // gamefolderBrowserDialog
            // 
            this.gamefolderBrowserDialog.Description = "Select Your Splatoon 2 RomFS";
            this.gamefolderBrowserDialog.ShowNewFolderButton = false;
            // 
            // opendictDialog
            // 
            this.opendictDialog.Filter = "Dictionary Text File|*.txt|All files (*.*)|*.*\"";
            this.opendictDialog.Title = "Select A Dictionary ";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bymltext);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Editor";
            this.Text = "BYML-Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openyamlFileDialog;
        private System.Windows.Forms.TextBox bymltext;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openXMLDisplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveLittleEndianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveBigEndianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yaz0CompressLittleEndianToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveyaz0FileDialog;
        private System.Windows.Forms.OpenFileDialog openxmlFileDialog;
        private System.Windows.Forms.ToolStripMenuItem decryptToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog gamefolderBrowserDialog;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableDeletingTempFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem obfuscationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deobfuscateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reobfuscateToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog opendictDialog;
        private System.Windows.Forms.ToolStripMenuItem saveTextboxToFileToolStripMenuItem;
    }
}

