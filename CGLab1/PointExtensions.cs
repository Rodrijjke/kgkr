using System.Drawing;
using OpenTK;

namespace CGLab1
{
    public static class PointExtensions
    {
        public static Point Invert(this Point source)
        {
            var temp = source.X;
            source.X = source.Y;
            source.Y = temp;
            return source;
        }

        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static Point Add(this Point source, Point pointToAdd)
        {
            return new Point(source.X + pointToAdd.X, source.Y + pointToAdd.Y);
        }
    }
}
