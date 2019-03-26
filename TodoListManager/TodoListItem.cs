using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager
{
	public class TodoListItem
	{
		public string Text { get; set; }

		public bool Done { get; set; }

		public TodoListItem(string text, bool done = false)
		{
			Text = text;
			Done = done;
		}
	}
}
