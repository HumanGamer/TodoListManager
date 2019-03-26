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
			using (Stream s = File.OpenRead(path))
			using (BinaryReader br = new BinaryReader(s))
			{
				string magic = Encoding.ASCII.GetString(br.ReadBytes(4));
				if (magic != "TODO")
					return false;

				int count = br.ReadInt32();
				for (int i = 0; i < count; i++)
				{
					string text = br.ReadString();
					bool done = br.ReadBoolean();
					Items.Add(new TodoListItem(text, done));
				}
			}

			Dirty = false;

			return true;
		}

		public void Write(string path)
		{
			using (Stream s = File.Open(path, FileMode.Create))
			using (BinaryWriter bw = new BinaryWriter(s))
			{
				bw.Write(Encoding.ASCII.GetBytes("TODO"));

				bw.Write(Items.Count);
				for (int i = 0; i < Items.Count; i++)
				{
					TodoListItem item = Items[i];
					bw.Write(item.Text);
					bw.Write(item.Done);
				}
			}

			Dirty = false;
		}

	}
}
