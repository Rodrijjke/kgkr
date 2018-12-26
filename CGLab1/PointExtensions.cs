using System;
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

        public static Point Substract(this Point source, Point pointToAdd)
        {
            return new Point(source.X - pointToAdd.X, source.Y - pointToAdd.Y);
        }

        public static Point Rotate(this Point source, Point rotatationBase, double radAngle)
        {
            // Для начала перенесём точку в новую систему координат с началом в rotatationBase:
            var displacedPoint = source.Substract(rotatationBase);

            // Поворот осуществляется путём умножения исходной точки на матрицу поворота:
            // [x']   [ cos(a)  -sin(a)]   [x]
            // [  ] = [                ] X [ ]
            // [y']   [-sin(a)   cos(a)]   [y

            var newX = displacedPoint.X * Math.Cos(radAngle) - displacedPoint.Y * Math.Sin(radAngle);
            var newY = displacedPoint.X * Math.Sin(radAngle) + displacedPoint.Y * Math.Cos(radAngle);

            // Вернём точку в первоначальную систему координат:
            var resultPoint = new Point((int) newX, (int) newY).Add(rotatationBase);

            return resultPoint;
        }
    }
}
