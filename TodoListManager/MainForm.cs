﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoListManager
{
	public partial class MainForm : Form
	{
		private const string BaseTitle = "Todo List Manager";

		private bool _loaded;
		private bool _updatingDisplay;
		private TodoList _todoList;
		private string _path;

		public MainForm()
		{
			InitializeComponent();

			UpdateDisplay();
		}

		private void UpdateDisplay()
		{
			_updatingDisplay = true;
			trvMain.Nodes.Clear();

			if (_todoList == null)
			{
				Enable = false;
				UpdateTitle();

				_updatingDisplay = false;
				return;
			}

			Enable = true;

			trvMain.Nodes.AddRange(ToTree(_todoList.Items));

			trvMain.ExpandAll();

			UpdateTitle();

			_updatingDisplay = false;
		}

		private void UpdateTitle()
		{
			if (_todoList == null)
				Text = BaseTitle;
			else
				Text = BaseTitle + " - [" + (_path ?? "Untitled Todo List") + (_todoList.Dirty ? "*" : "") + "]";
		}

		private bool ShouldCloseFile()
		{
			if (_todoList == null || !_todoList.Dirty)
				return true;

			DialogResult result = MessageBox.Show(this,
				"There are unsaved changes in this file.\n\nWould you like to save your changes?", "Save?",
				MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
			switch (result)
			{
				case DialogResult.Yes:
					return Save();
				case DialogResult.No:
					return true;
				default: // Cancel or close
					return false;
			}
		}

		private bool _enable;

		private bool Enable
		{
			get => _enable;
			set
			{
				_enable = value;
				trvMain.Enabled = _enable;
				addItemToolStripMenuItem.Enabled = _enable;
				removeItemToolStripMenuItem.Enabled = false; //_enable;
				tsbAddItem.Enabled = _enable;
				tsbRemoveItem.Enabled = false; //_enable && _todoList != null && _todoList.Items.Count > 0;
				undoToolStripMenuItem.Enabled = _enable;
				redoToolStripMenuItem.Enabled = _enable;
				cutToolStripMenuItem.Enabled = _enable;
				copyToolStripMenuItem.Enabled = _enable;
				pasteToolStripMenuItem.Enabled = _enable;
				selectAllToolStripMenuItem.Enabled = _enable;
				tsbUndo.Enabled = _enable;
				tsbRedo.Enabled = _enable;
				tsbCut.Enabled = _enable;
				tsbCopy.Enabled = _enable;
				tsbPaste.Enabled = _enable;
				saveToolStripMenuItem.Enabled = _enable;
				saveAsToolStripMenuItem.Enabled = _enable;
				tsbSave.Enabled = _enable;
				closeFileToolStripMenuItem.Enabled = _enable;
			}
		}

		private void New()
		{
			if (!ShouldCloseFile())
				return;

			_todoList = new TodoList();
			_path = null;
			UpdateDisplay();
		}

		private void Open()
		{
			if (!ShouldCloseFile())
				return;

			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "Todo Lists|*.todo|All Files|*.*";
			DialogResult result = ofd.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				Open(ofd.FileName);
			}
		}

		public void Open(string path)
		{
			_todoList = TodoList.ReadTodoList(path);
			_path = path;
			UpdateDisplay();
		}

		private bool Save()
		{
			if (_todoList == null)
				return false;

			if (string.IsNullOrWhiteSpace(_path))
				return SaveAs();
			else
				return Save(_path);
		}

		private bool SaveAs()
		{
			if (_todoList == null)
				return false;

			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Todo Lists|*.todo|All Files|*.*";
			DialogResult result = sfd.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				return Save(sfd.FileName);
			}

			return false;
		}

		private bool Save(string path)
		{
			if (_todoList == null)
				return false;

			_todoList.Write(path);
			_path = path;

			UpdateTitle();

			return true;
		}

		private void CloseFile()
		{
			if (!ShouldCloseFile())
				return;

			_todoList = null;
			_path = null;
			UpdateDisplay();
		}

		private void Exit()
		{
			Application.Exit();
		}

		private void Undo()
		{
			if (_todoList == null)
				return;
		}

		private void Redo()
		{
			if (_todoList == null)
				return;
		}

		private void Cut()
		{
			if (_todoList == null)
				return;
		}

		private void Copy()
		{
			if (_todoList == null)
				return;
		}

		private void Paste()
		{
			if (_todoList == null)
				return;
		}

		private void SelectAll()
		{
			if (_todoList == null)
				return;
		}

		private void AddSubItem()
        {
			if (_todoList == null || trvMain.SelectedNode == null)
				return;

			AddTaskDialog dlg = new AddTaskDialog();
			dlg.ParentItem = GetItem(trvMain.SelectedNode);
			dlg.ShowDialog(this);
			if (dlg.Item == null || string.IsNullOrWhiteSpace(dlg.Item.Text))
				return;
			GetItem(trvMain.SelectedNode).SubItems.Add(dlg.Item);
			_todoList.Dirty = true;
			UpdateDisplay();
		}

		private void AddItem()
		{
			if (_todoList == null)
				return;

			AddTaskDialog dlg = new AddTaskDialog();
			dlg.ShowDialog(this);
			if (dlg.Item == null || string.IsNullOrWhiteSpace(dlg.Item.Text))
				return;
			_todoList.Items.Add(dlg.Item);
			_todoList.Dirty = true;
			UpdateDisplay();
		}

		private void RemoveItem()
		{
			if (_todoList == null)
				return;

			if (trvMain.SelectedNode == null)
				return;

			var item = GetItem(trvMain.SelectedNode);
			if (item.Parent == null)
			{
				int index = trvMain.SelectedNode.Index;
				_todoList.Items.RemoveAt(index);
			} else
            {
				item.Parent.SubItems.RemoveAt(item.Index);
            }
			_todoList.Dirty = true;
			UpdateDisplay();
		}

		public void About()
		{
			MessageBox.Show(this, "Created by HumanGamer", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		#region Menu Event Handlers

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			New();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Open();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveAs();
		}
		private void closeFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseFile();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Exit();
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Undo();
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Redo();
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Cut();
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Copy();
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Paste();
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SelectAll();
		}

		private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddItem();
		}

		private void addSubItemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AddSubItem();
		}

		private void removeItemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RemoveItem();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			About();
		}

		private void tsbNew_Click(object sender, EventArgs e)
		{
			New();
		}

		private void tsbOpen_Click(object sender, EventArgs e)
		{
			Open();
		}

		private void tsbSave_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void tsbUndo_Click(object sender, EventArgs e)
		{
			Undo();
		}

		private void tsbRedo_Click(object sender, EventArgs e)
		{
			Redo();
		}

		private void tsbCut_Click(object sender, EventArgs e)
		{
			Cut();
		}

		private void tsbCopy_Click(object sender, EventArgs e)
		{
			Copy();
		}

		private void tsbPaste_Click(object sender, EventArgs e)
		{
			Paste();
		}

		private void tsbAddItem_Click(object sender, EventArgs e)
		{
			AddItem();
		}

		private void tspAddSubItem_Click(object sender, EventArgs e)
		{
			AddSubItem();
		}

		private void tsbRemoveItem_Click(object sender, EventArgs e)
		{
			RemoveItem();
		}

		#endregion

		private void trvMain_AfterCheck(object sender, TreeViewEventArgs e)
		{
			var node = e.Node;
			if (_todoList == null || node == null)
				return;
			GetItem(node).Done = e.Node.Checked;
			if (!_updatingDisplay && _loaded)
				_todoList.Dirty = true;
			UpdateTitle();
		}

		private void trvMain_AfterSelect(object sender, TreeViewEventArgs e)
		{
			bool hasSelection = trvMain.SelectedNode != null;
			removeItemToolStripMenuItem.Enabled = hasSelection;
			tsbRemoveItem.Enabled = hasSelection;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			_loaded = true;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!ShouldCloseFile())
				e.Cancel = true;
		}

		public TodoListItem GetItem(TreeNode node)
		{
			IEnumerable<TreeNode> nodes = EnumerateNodes(node);
			TodoListItem item = _todoList.Items[nodes.First().Index];

			foreach (TreeNode node2 in nodes.Skip(1))
			{
				item = item.SubItems[node2.Index];
			}
			return item;
		}

		public static IEnumerable<TreeNode> EnumerateNodes(TreeNode node)
		{
			if (node is null) { yield break; }
			foreach (var n in EnumerateNodes(node.Parent as TreeNode))
			{
				yield return n;
			}
			yield return node;
		}

		public TreeNode GetNode(TodoListItem item)
		{
			IEnumerable<TodoListItem> items = EnumerateItems(item);
			TreeNode node = trvMain.Nodes[items.First().Index];

			foreach (TodoListItem item2 in items.Skip(1))
			{
				node = node.Nodes[item2.Index];
			}
			return node;
		}

		public static IEnumerable<TodoListItem> EnumerateItems(TodoListItem item)
		{
			if (item is null) { yield break; }
			foreach (var n in EnumerateItems(item.Parent as TodoListItem))
			{
				yield return n;
			}
			yield return item;
		}

		public static TreeNode[] ToTree(List<TodoListItem> items, TodoListItem parent = null)
		{
			if (items is null || items.Count == 0) { return new TreeNode[0]; }
			return Enumerable.Range(0, items.Count).Select(i => new TreeNode(items[i].Text, ToTree(items[i].SubItems, items[i]))
			{
				Checked = items[i].Done
			}).ToArray();
		}
	}
}
