﻿namespace testfdb_cs
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("ノード0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("NoTag");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("ノード2", new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewSplitContainer = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ListViewPanel = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SearchComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.TagTreeView = new System.Windows.Forms.TreeView();
            this.TagTreeViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddTagMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TabContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ListViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.nameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip.SuspendLayout();
            this.ViewSplitContainer.Panel1.SuspendLayout();
            this.ViewSplitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.TagTreeViewContextMenu.SuspendLayout();
            this.ListViewContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(627, 24);
            this.MainMenuStrip.TabIndex = 10;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.toolToolStripMenuItem.Text = "Tool";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // ViewSplitContainer
            // 
            this.ViewSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.ViewSplitContainer.Name = "ViewSplitContainer";
            this.ViewSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ViewSplitContainer.Panel1
            // 
            this.ViewSplitContainer.Panel1.Controls.Add(this.panel1);
            this.ViewSplitContainer.Panel1.Controls.Add(this.toolStrip1);
            this.ViewSplitContainer.Size = new System.Drawing.Size(502, 433);
            this.ViewSplitContainer.SplitterDistance = 240;
            this.ViewSplitContainer.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ListViewPanel);
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Controls.Add(this.tabControl2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(502, 215);
            this.panel1.TabIndex = 16;
            // 
            // ListViewPanel
            // 
            this.ListViewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewPanel.Location = new System.Drawing.Point(0, 46);
            this.ListViewPanel.Name = "ListViewPanel";
            this.ListViewPanel.Size = new System.Drawing.Size(502, 169);
            this.ListViewPanel.TabIndex = 3;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1,
            this.toolStripButton3});
            this.toolStrip2.Location = new System.Drawing.Point(0, 21);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Size = new System.Drawing.Size(502, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // tabControl2
            // 
            this.tabControl2.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl2.ItemSize = new System.Drawing.Size(100, 20);
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.Padding = new System.Drawing.Point(3, 3);
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(502, 21);
            this.tabControl2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl2.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.SearchComboBox,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(502, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SearchComboBox
            // 
            this.SearchComboBox.Name = "SearchComboBox";
            this.SearchComboBox.Size = new System.Drawing.Size(120, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.TagTreeView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ViewSplitContainer);
            this.splitContainer2.Size = new System.Drawing.Size(627, 433);
            this.splitContainer2.SplitterDistance = 121;
            this.splitContainer2.TabIndex = 15;
            // 
            // TagTreeView
            // 
            this.TagTreeView.AllowDrop = true;
            this.TagTreeView.ContextMenuStrip = this.TagTreeViewContextMenu;
            this.TagTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TagTreeView.HideSelection = false;
            this.TagTreeView.Indent = 14;
            this.TagTreeView.Location = new System.Drawing.Point(0, 0);
            this.TagTreeView.Name = "TagTreeView";
            treeNode1.Name = "ノード0";
            treeNode1.Text = "ノード0";
            treeNode2.Name = "ノード1";
            treeNode2.Text = "NoTag";
            treeNode3.Name = "TagNode";
            treeNode3.Text = "ノード2";
            this.TagTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3});
            this.TagTreeView.ShowLines = false;
            this.TagTreeView.Size = new System.Drawing.Size(121, 433);
            this.TagTreeView.TabIndex = 0;
            this.TagTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.TagTreeView_DragDrop);
            this.TagTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TagTreeView_MouseDown);
            this.TagTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.TagTreeView_DragEnter);
            this.TagTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.TagTreeView_DragOver);
            // 
            // TagTreeViewContextMenu
            // 
            this.TagTreeViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddTagMenuItem});
            this.TagTreeViewContextMenu.Name = "TagcontextMenuStrip1";
            this.TagTreeViewContextMenu.Size = new System.Drawing.Size(169, 26);
            // 
            // AddTagMenuItem
            // 
            this.AddTagMenuItem.Name = "AddTagMenuItem";
            this.AddTagMenuItem.Size = new System.Drawing.Size(168, 22);
            this.AddTagMenuItem.Text = "toolStripMenuItem1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 457);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(627, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TabContextMenu
            // 
            this.TabContextMenu.Name = "TabContextMenu";
            this.TabContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // ListViewContextMenu
            // 
            this.ListViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFolderMenuItem});
            this.ListViewContextMenu.Name = "ListViewContextMenu";
            this.ListViewContextMenu.Size = new System.Drawing.Size(133, 26);
            // 
            // OpenFolderMenuItem
            // 
            this.OpenFolderMenuItem.Name = "OpenFolderMenuItem";
            this.OpenFolderMenuItem.Size = new System.Drawing.Size(132, 22);
            this.OpenFolderMenuItem.Text = "Open Folder";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // nameToolStripMenuItem
            // 
            this.nameToolStripMenuItem.CheckOnClick = true;
            this.nameToolStripMenuItem.Name = "nameToolStripMenuItem";
            this.nameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nameToolStripMenuItem.Text = "name";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 479);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainMenuStrip);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ViewSplitContainer.Panel1.ResumeLayout(false);
            this.ViewSplitContainer.Panel1.PerformLayout();
            this.ViewSplitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.TagTreeViewContextMenu.ResumeLayout(false);
            this.ListViewContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.SplitContainer ViewSplitContainer;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox SearchComboBox;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView TagTreeView;
        private System.Windows.Forms.ContextMenuStrip TagTreeViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem AddTagMenuItem;
        private System.Windows.Forms.Panel ListViewPanel;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ContextMenuStrip TabContextMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ListViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem nameToolStripMenuItem;
    }
}

