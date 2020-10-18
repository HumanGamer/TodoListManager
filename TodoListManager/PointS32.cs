using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager
{
    public struct PointS32
    {
        public int X;
        public int Y;

        public PointS32(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override /*readonly*/ bool Equals(object obj) => (obj is PointS32 that) && that.X == X && that.Y == Y;
        public override /*readonly*/ int GetHashCode() => X ^ Y;

        public static bool operator ==(PointS32 a, PointS32 b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(PointS32 a, PointS32 b) => a.X != b.X || a.Y != b.Y;

        public static PointS32 operator +(PointS32 a, PointS32 b) => new PointS32(a.X + b.X, a.Y + b.Y);
        public static PointS32 operator -(PointS32 a, PointS32 b) => new PointS32(a.X - b.X, a.Y - b.Y);
        public static PointS32 operator *(PointS32 a, PointS32 b) => new PointS32(a.X * b.X, a.Y * b.Y);
        public static PointS32 operator /(PointS32 a, PointS32 b) => new PointS32(a.X / b.X, a.Y / b.Y);

        public static PointS32 operator *(PointS32 p, int s) => new PointS32(p.X * s, p.Y * s);
        public static PointS32 operator /(PointS32 p, int s) => new PointS32(p.X / s, p.Y / s);

        public static PointS32 operator >>(PointS32 p, int s) => new PointS32(p.X >> s, p.Y >> s);
        public static PointS32 operator <<(PointS32 p, int s) => new PointS32(p.X << s, p.Y << s);

        public static implicit operator Point(PointS32 p) => new Point(p.X, p.Y);
        public static implicit operator PointS32(Point p) => new PointS32(p.X, p.Y);
        public static implicit operator PointF(PointS32 p) => new PointF(p.X, p.Y);
        public static implicit operator PointS32(PointF p) => new PointS32((int)p.X, (int)p.Y);
    }
}
