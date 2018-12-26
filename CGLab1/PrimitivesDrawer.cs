using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace CGLab1
{
    public class PrimitivesDrawer
    {
        private readonly int Width;
        private readonly int Height;

        public PrimitivesDrawer(int windowWidth, int windowHeight)
        {
            Width = windowWidth;
            Height = windowHeight;
        }

        public void DrawLine(Point start, Point end, int width, Color4 color)
        {
            var points = GetPointsOfLine(start, end);
            foreach (var point in points)
                DrawPoint(point.X, point.Y, width, color);
        }

        private static IEnumerable<Point> GetPointsOfLine(Point start, Point end)
        {
            var steep = false;
            if (Math.Abs(start.X - end.X) < Math.Abs(start.Y - end.Y))
            {
                start = start.Invert();
                end = end.Invert();
                steep = true;
            }
            if (start.X > end.X)
            {
                var temp = start;
                start = end;
                end = temp;
            }

            for (var x = start.X; x <= end.X; x++)
            {
                var t = (x - start.X) / (float)(end.X - start.X);
                var y = (int)Math.Round(start.Y * (1.0f - t) + end.Y * t);
                if (steep)
                    yield return new Point(y, x);
                else
                    yield return new Point(x, y);
            }
        }

        public void DrawPoint(float x, float y, float size, Color4 color)
        {
            var position = new Vector2(x / Width, y / Height);
            GL.VertexAttrib2(0, position);
            var colorArr = new[] { color.R, color.G, color.B, color.A };
            GL.VertexAttrib4(1, colorArr);
            GL.PointSize(size);
            GL.DrawArrays(PrimitiveType.Points, 0, 1);
        }
    }
}
