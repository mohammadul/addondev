﻿namespace wiki
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.BrowserContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ListViewSplitContainer = new System.Windows.Forms.SplitContainer();
            this.ItemTabControl = new System.Windows.Forms.TabControl();
            this.SearchPanel = new System.Windows.Forms.Panel();
            this.SearchComboBox = new System.Windows.Forms.ComboBox();
            this.SearchButtonPanel = new System.Windows.Forms.Panel();
            this.ViewEditorSplitContainer = new System.Windows.Forms.SplitContainer();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.EditorPanel = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.CloseEditorToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.NewItemToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ReloadToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.NextToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.PageToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.PreToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToggleListToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.SearchContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RegexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MigemoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BrowserContextMenuStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ListViewSplitContainer.Panel1.SuspendLayout();
            this.ListViewSplitContainer.Panel2.SuspendLayout();
            this.ListViewSplitContainer.SuspendLayout();
            this.SearchPanel.SuspendLayout();
            this.ViewEditorSplitContainer.Panel1.SuspendLayout();
            this.ViewEditorSplitContainer.Panel2.SuspendLayout();
            this.ViewEditorSplitContainer.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SearchContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // BrowserContextMenuStrip
            // 
            this.BrowserContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.BrowserContextMenuStrip.Name = "contextMenuStrip1";
            this.BrowserContextMenuStrip.Size = new System.Drawing.Size(169, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ListViewSplitContainer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(747, 469);
            this.panel2.TabIndex = 0;
            // 
            // ListViewSplitContainer
            // 
            this.ListViewSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.ListViewSplitContainer.Name = "ListViewSplitContainer";
            // 
            // ListViewSplitContainer.Panel1
            // 
            this.ListViewSplitContainer.Panel1.Controls.Add(this.ItemTabControl);
            this.ListViewSplitContainer.Panel1.Controls.Add(this.SearchPanel);
            // 
            // ListViewSplitContainer.Panel2
            // 
            this.ListViewSplitContainer.Panel2.Controls.Add(this.ViewEditorSplitContainer);
            this.ListViewSplitContainer.Size = new System.Drawing.Size(747, 469);
            this.ListViewSplitContainer.SplitterDistance = 185;
            this.ListViewSplitContainer.TabIndex = 3;
            // 
            // ItemTabControl
            // 
            this.ItemTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.ItemTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemTabControl.ItemSize = new System.Drawing.Size(70, 20);
            this.ItemTabControl.Location = new System.Drawing.Point(0, 20);
            this.ItemTabControl.Multiline = true;
            this.ItemTabControl.Name = "ItemTabControl";
            this.ItemTabControl.SelectedIndex = 0;
            this.ItemTabControl.Size = new System.Drawing.Size(185, 449);
            this.ItemTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.ItemTabControl.TabIndex = 1;
            // 
            // SearchPanel
            // 
            this.SearchPanel.Controls.Add(this.SearchComboBox);
            this.SearchPanel.Controls.Add(this.SearchButtonPanel);
            this.SearchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SearchPanel.Location = new System.Drawing.Point(0, 0);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(185, 20);
            this.SearchPanel.TabIndex = 1;
            // 
            // SearchComboBox
            // 
            this.SearchComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchComboBox.FormattingEnabled = true;
            this.SearchComboBox.Location = new System.Drawing.Point(31, 0);
            this.SearchComboBox.Name = "SearchComboBox";
            this.SearchComboBox.Size = new System.Drawing.Size(154, 20);
            this.SearchComboBox.TabIndex = 1;
            // 
            // SearchButtonPanel
            // 
            this.SearchButtonPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SearchButtonPanel.Location = new System.Drawing.Point(0, 0);
            this.SearchButtonPanel.Name = "SearchButtonPanel";
            this.SearchButtonPanel.Size = new System.Drawing.Size(31, 20);
            this.SearchButtonPanel.TabIndex = 2;
            // 
            // ViewEditorSplitContainer
            // 
            this.ViewEditorSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewEditorSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.ViewEditorSplitContainer.Name = "ViewEditorSplitContainer";
            this.ViewEditorSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ViewEditorSplitContainer.Panel1
            // 
            this.ViewEditorSplitContainer.Panel1.Controls.Add(this.webBrowser1);
            // 
            // ViewEditorSplitContainer.Panel2
            // 
            this.ViewEditorSplitContainer.Panel2.Controls.Add(this.EditorPanel);
            this.ViewEditorSplitContainer.Panel2.Controls.Add(this.toolStrip2);
            this.ViewEditorSplitContainer.Size = new System.Drawing.Size(558, 469);
            this.ViewEditorSplitContainer.SplitterDistance = 257;
            this.ViewEditorSplitContainer.TabIndex = 1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.ContextMenuStrip = this.BrowserContextMenuStrip;
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(558, 257);
            this.webBrowser1.TabIndex = 0;
            // 
            // EditorPanel
            // 
            this.EditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditorPanel.Location = new System.Drawing.Point(0, 25);
            this.EditorPanel.Name = "EditorPanel";
            this.EditorPanel.Size = new System.Drawing.Size(558, 183);
            this.EditorPanel.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseEditorToolStripButton});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(558, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // CloseEditorToolStripButton
            // 
            this.CloseEditorToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CloseEditorToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CloseEditorToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseEditorToolStripButton.Image")));
            this.CloseEditorToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseEditorToolStripButton.Name = "CloseEditorToolStripButton";
            this.CloseEditorToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.CloseEditorToolStripButton.Text = "Close";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(747, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newItemToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newItemToolStripMenuItem
            // 
            this.newItemToolStripMenuItem.Name = "newItemToolStripMenuItem";
            this.newItemToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.newItemToolStripMenuItem.Text = "newItem";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timeToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // timeToolStripMenuItem
            // 
            this.timeToolStripMenuItem.Name = "timeToolStripMenuItem";
            this.timeToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.timeToolStripMenuItem.Text = "Time";
            this.timeToolStripMenuItem.Click += new System.EventHandler(this.timeToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewItemToolStripButton,
            this.ReloadToolStripButton,
            this.NextToolStripButton,
            this.PageToolStripLabel,
            this.PreToolStripButton,
            this.ToggleListToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(747, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // NewItemToolStripButton
            // 
            this.NewItemToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewItemToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("NewItemToolStripButton.Image")));
            this.NewItemToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewItemToolStripButton.Name = "NewItemToolStripButton";
            this.NewItemToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.NewItemToolStripButton.Text = "toolStripButton1";
            this.NewItemToolStripButton.ToolTipText = "New";
            // 
            // ReloadToolStripButton
            // 
            this.ReloadToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ReloadToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ReloadToolStripButton.Image")));
            this.ReloadToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReloadToolStripButton.Name = "ReloadToolStripButton";
            this.ReloadToolStripButton.Size = new System.Drawing.Size(44, 22);
            this.ReloadToolStripButton.Text = "Reload";
            this.ReloadToolStripButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // NextToolStripButton
            // 
            this.NextToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.NextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NextToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("NextToolStripButton.Image")));
            this.NextToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NextToolStripButton.Name = "NextToolStripButton";
            this.NextToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.NextToolStripButton.Text = ">";
            // 
            // PageToolStripLabel
            // 
            this.PageToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.PageToolStripLabel.Name = "PageToolStripLabel";
            this.PageToolStripLabel.Size = new System.Drawing.Size(81, 22);
            this.PageToolStripLabel.Text = "toolStripLabel1";
            // 
            // PreToolStripButton
            // 
            this.PreToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.PreToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PreToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("PreToolStripButton.Image")));
            this.PreToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PreToolStripButton.Name = "PreToolStripButton";
            this.PreToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.PreToolStripButton.Text = "<";
            // 
            // ToggleListToolStripButton
            // 
            this.ToggleListToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ToggleListToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToggleListToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ToggleListToolStripButton.Image")));
            this.ToggleListToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToggleListToolStripButton.Name = "ToggleListToolStripButton";
            this.ToggleListToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.ToggleListToolStripButton.Text = "List";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 518);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(747, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(114, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // SearchContextMenuStrip
            // 
            this.SearchContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NormalToolStripMenuItem,
            this.RegexToolStripMenuItem,
            this.MigemoToolStripMenuItem});
            this.SearchContextMenuStrip.Name = "SearchContextMenuStrip";
            this.SearchContextMenuStrip.Size = new System.Drawing.Size(110, 70);
            // 
            // NormalToolStripMenuItem
            // 
            this.NormalToolStripMenuItem.CheckOnClick = true;
            this.NormalToolStripMenuItem.Name = "NormalToolStripMenuItem";
            this.NormalToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.NormalToolStripMenuItem.Text = "Normal";
            // 
            // RegexToolStripMenuItem
            // 
            this.RegexToolStripMenuItem.Name = "RegexToolStripMenuItem";
            this.RegexToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.RegexToolStripMenuItem.Text = "Regex";
            // 
            // MigemoToolStripMenuItem
            // 
            this.MigemoToolStripMenuItem.Name = "MigemoToolStripMenuItem";
            this.MigemoToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.MigemoToolStripMenuItem.Text = "Migemo";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 540);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.BrowserContextMenuStrip.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ListViewSplitContainer.Panel1.ResumeLayout(false);
            this.ListViewSplitContainer.Panel2.ResumeLayout(false);
            this.ListViewSplitContainer.ResumeLayout(false);
            this.SearchPanel.ResumeLayout(false);
            this.ViewEditorSplitContainer.Panel1.ResumeLayout(false);
            this.ViewEditorSplitContainer.Panel2.ResumeLayout(false);
            this.ViewEditorSplitContainer.Panel2.PerformLayout();
            this.ViewEditorSplitContainer.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.SearchContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer ListViewSplitContainer;
        private System.Windows.Forms.ContextMenuStrip BrowserContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl ItemTabControl;
        private System.Windows.Forms.SplitContainer ViewEditorSplitContainer;
        private System.Windows.Forms.ToolStripButton NewItemToolStripButton;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ComboBox SearchComboBox;
        private System.Windows.Forms.ToolStripMenuItem timeToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ReloadToolStripButton;
        private System.Windows.Forms.Panel EditorPanel;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton NextToolStripButton;
        private System.Windows.Forms.ToolStripButton PreToolStripButton;
        private System.Windows.Forms.ToolStripLabel PageToolStripLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton CloseEditorToolStripButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton ToggleListToolStripButton;
        private System.Windows.Forms.Panel SearchPanel;
        private System.Windows.Forms.Panel SearchButtonPanel;
        private System.Windows.Forms.ContextMenuStrip SearchContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem NormalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RegexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MigemoToolStripMenuItem;
    }
}

