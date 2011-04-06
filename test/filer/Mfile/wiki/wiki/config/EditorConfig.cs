﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sgry.Azuki.WinForms;
using wiki.Properties;
using wiki.config;

namespace wiki {
    public partial class EditorConfig : UserControl {
        public Config config { get; set; }
        public List<string> SnippetList {
            get {
                var ret = new List<string>(); 
                var cnt = SnippetListBox.Items.Count;
                for (int i = 0; i < cnt; i++){
			        ret.Add(SnippetListBox.Items[i].ToString());
                }
                return ret;
            }
        }
        private AzukiControl editor;
        //private bool 
        public EditorConfig(Config config) {
            this.config = config;

            InitializeComponent();
            editor = new AzukiControl();
            editor.ShowsHScrollBar = false;
            editor.Dock = DockStyle.Fill;
            editor.Text = Resources.EditorPreviewText;
            panel.Controls.Add(editor);

            FontSelectButton.Text = this.config.EditorFont.Name + "(" + this.config.EditorFont.Size + ")";
            FontSelectButton.Click += (sender, e) => {
                var res = EditorFontDialog.ShowDialog();
                if (res == DialogResult.OK) {
                    var EditorFont = EditorFontDialog.Font;
                    FontSelectButton.Text = EditorFont.Name + "(" + EditorFont.Size + ")";
                    editor.Font = EditorFont;
                }
            };

            FontColorButton.Click += (sender, e) => {
                var res = EditorColorDialog.ShowDialog();
                if (res == DialogResult.OK) {
                    editor.ForeColor = EditorColorDialog.Color;
                }
            };

            BackColorButton.Click += (sender, e) => {
                var res = EditorColorDialog.ShowDialog();
                if (res == DialogResult.OK) {
                    editor.BackColor = EditorColorDialog.Color;
                }
            };

            ShowTabCheckBox.CheckedChanged += (s, e) => {
                editor.View.DrawsTab = ShowTabCheckBox.Checked;
            };
            ShowEolCheckBox.CheckedChanged += (s, e) => {
                editor.View.DrawsEolCode = ShowEolCheckBox.Checked;
            };
            ShowSpaceCheckBox.CheckedChanged += (s, e) => {
                editor.View.DrawsSpace = ShowSpaceCheckBox.Checked;
            };
            ShowZenSpaceCheckBox.CheckedChanged += (s, e) => {
                editor.View.DrawsFullWidthSpace = ShowZenSpaceCheckBox.Checked;
            };

            //
            foreach (var item in this.config.SnippetList){
                SnippetListBox.Items.Add(item);
            }
           
            NewButton.Click += (s, e) => {
                var edit = new SnippetEditForm();
                edit.StartPosition = FormStartPosition.CenterParent;
                var res = edit.ShowDialog(this);
                if (res == DialogResult.OK) {
                    SnippetListBox.Items.Add(edit.Code);
                }
                edit.Close();
            };
            EditButton.Click += (s, e) => {
                var edit = new SnippetEditForm();
                edit.Code = SnippetListBox.SelectedItem.ToString();
                var res = edit.ShowDialog(this);
                if (res == DialogResult.OK) {
                    SnippetListBox.SelectedItem = edit.Code;
                }
            };
            DeleteButton.Click += (s, e) => {
                SnippetListBox.Items.RemoveAt(SnippetListBox.SelectedIndex);
            };

            this.Load += (s, e) => {
                editor.Font = config.EditorFont;
                editor.ForeColor = config.EditorFontColor;
                editor.BackColor = config.EditorBackColor;

                editor.DrawsTab = config.ShowTab;
                editor.DrawsEolCode = config.ShowEol;
                editor.DrawsSpace = config.ShowSpace;
                editor.DrawsFullWidthSpace = config.ShowZenSpace;

                ShowTabCheckBox.Checked = editor.DrawsTab;
                ShowEolCheckBox.Checked = editor.DrawsEolCode;
                ShowSpaceCheckBox.Checked = editor.DrawsSpace;
                ShowZenSpaceCheckBox.Checked = editor.DrawsFullWidthSpace;
            };
            this.Disposed += (s, e) => {
                if (editor != null) {
                    editor.Dispose();
                }
            };
        }

        public void Accept() {
            config.ShowTab = editor.View.DrawsTab;
            config.ShowEol = editor.View.DrawsEofMark;
            config.ShowSpace = editor.View.DrawsSpace;
            config.ShowZenSpace = editor.View.DrawsFullWidthSpace;

            config.EditorFontColor = editor.ForeColor;
            config.EditorBackColor = editor.BackColor;
        }
    }
}
