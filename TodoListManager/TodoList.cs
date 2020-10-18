using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager
{
	public class TodoList
	{
		public Guid id;
		public List<TodoListItem> Items;

		[JsonIgnore]
		public bool Dirty;

		public TodoList()
		{
			id = Guid.NewGuid();
			Items = new List<TodoListItem>();
		}

		public static TodoList ReadTodoList(string path)
		{
			return FromJSON(File.ReadAllText(path));
		}

		public static TodoList FromJSON(string json)
        {
			TodoList todo = JsonConvert.DeserializeObject<TodoList>(json);

			Initialize(todo.Items);

			return todo;
        }

		public static void Initialize(List<TodoListItem> items, TodoListItem parent = null)
		{
			for (int i = 0; i < items.Count; i++)
			{
				TodoListItem item = items[i];
				item.Index = i;
				item.Parent = parent;
				Initialize(item.SubItems, item);
			}
		}

		public void Write(string path)
		{
			File.WriteAllText(path, JsonConvert.SerializeObject(this));

			Dirty = false;
		}

	}
}
