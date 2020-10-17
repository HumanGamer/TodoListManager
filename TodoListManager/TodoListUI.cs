using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoListManager
{
    public class TodoListUI : UserControl
    {
        private TodoList _todoList;

        public TodoList TodoList
        {
            get => _todoList;
            set
            {
                _todoList = value;

                Invalidate();
            }
        }

        public TodoListUI()
        {
            BackColor = SystemColors.Control;

            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var g = e.Graphics;

            g.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
            g.DrawRectangle(new Pen(Color.Black), new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1));
        }
    }
}
