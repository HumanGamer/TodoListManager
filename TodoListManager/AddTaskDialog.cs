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

		public AddTaskDialog()
		{
			InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Item = new TodoListItem(txtText.Text, false);
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Item = null;
			Close();
		}
	}
}
