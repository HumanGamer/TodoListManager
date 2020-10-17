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
		public List<TodoListItem> Items;
		public bool Dirty;

		public TodoList()
		{
			Items = new List<TodoListItem>();
		}

		public static TodoList ReadTodoList(string path)
		{
			TodoList result = new TodoList();
			if (!result.Read(path))
				return null;
			return result;
		}

		private bool Read(string path)
		{
			Items = JsonConvert.DeserializeObject<List<TodoListItem>>(File.ReadAllText(path));

			Dirty = false;

			return true;
		}

		public void Write(string path)
		{
			File.WriteAllText(path, JsonConvert.SerializeObject(Items));

			Dirty = false;
		}

	}
}
