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
			return JsonConvert.DeserializeObject<TodoList>(json);
        }

		public void Write(string path)
		{
			File.WriteAllText(path, JsonConvert.SerializeObject(this));

			Dirty = false;
		}

	}
}
