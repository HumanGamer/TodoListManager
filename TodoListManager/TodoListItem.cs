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

		public List<TodoListItem> SubItems { get; set; }

		public TodoListItem(string text, bool done, params TodoListItem[] subitems)
		{
			Text = text;
			Done = done;
			SubItems = subitems.ToList();
		}
	}
}
