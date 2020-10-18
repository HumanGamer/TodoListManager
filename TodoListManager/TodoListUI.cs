using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoListManager
{
    public class TodoListUI : Panel
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

            AutoScrollMinSize = new Size(Width, 1000);
            VScroll = true;

            DoubleBuffered = true;

            BorderStyle = BorderStyle.FixedSingle;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            g.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);

            if (_todoList == null)
            {
                g.DrawString("No TodoList Loaded", Font, new SolidBrush(ForeColor), new Point(20, 20));
                return;
            }

            var items = _todoList.Items;

            PointS32 itemMargin = new PointS32(5, 5);
            PointS32 itemPadding = new PointS32(5, 5);
            Size itemSize = new Size(ClientSize.Width - (itemMargin.X * 2), 50);

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                PointS32 pos = new PointS32(itemMargin.X, itemMargin.Y + (itemSize.Height + itemMargin.Y) * i);

                Rectangle itemRect = new Rectangle(pos, itemSize);

                string id = item.ID.ToString();
                string text = item.Text;
                bool done = item.Done;
                var subItems = item.SubItems;

                g.FillRectangle(new LinearGradientBrush(itemRect, Util.FromRGBA(0xAAAAAA), Util.FromRGBA(0x888888), 90.0f), new Rectangle(pos.X, pos.Y, itemSize.Width, itemSize.Height));
                g.DrawRectangle(new Pen(Color.Black), new Rectangle(pos.X, pos.Y, itemSize.Width, itemSize.Height));

                g.DrawStringWithShadow(text, Font, new SolidBrush(Color.White), pos + itemPadding);

                //PointS32 idPos = new PointS32((int)(itemSize.Width - itemMargin.X - itemPadding.X - g.MeasureString(id, Font).Width), pos.Y + itemPadding.Y);
                //g.DrawStringWithShadow(id, Font, new SolidBrush(Color.Gray), idPos);

            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var g = e.Graphics;

            g.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
        }
    }
}
