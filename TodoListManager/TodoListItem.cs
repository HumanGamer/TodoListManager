using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager
{
	public class TodoListItem
	{
		[JsonIgnore]
		public TodoListItem Parent;

		[JsonIgnore]
		public int Index;

		public Guid ID { get; set; }

		public string Text { get; set; }

		public bool Done { get; set; }

		public List<TodoListItem> SubItems { get; set; }

		public TodoListItem(TodoListItem parent, int index, string text, bool done, params TodoListItem[] subitems)
		{
			ID = Guid.NewGuid();
			Parent = parent;
			Index = index;
			Text = text;
			Done = done;
			SubItems = subitems.ToList();
		}
	}
}
