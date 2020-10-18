using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoListManager
{
	public partial class AddTaskDialog : Form
	{
		public TodoListItem Item
		{
			get;
			private set;
		}

		public TodoListItem ParentItem
		{
			get;
			set;
		}

		public AddTaskDialog()
		{
			InitializeComponent();

			ParentItem = null;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			int index = -1;
			if (ParentItem != null)
				index = ParentItem.SubItems.Count;
			Item = new TodoListItem(ParentItem, index, txtText.Text, false);
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Item = null;
			Close();
		}
	}
}
