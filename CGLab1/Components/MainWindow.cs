using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.XPath;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace CGLab1.Components
{
    public class MainWindow : GameWindow
    {
        private const string title = "Курсовая работа";
        private int program;
        private int vertexArray;

        public MainWindow()
            : base(820,
                560,
                GraphicsMode.Default,
                title,
                GameWindowFlags.Default,
                DisplayDevice.Default,
                4,
                0,
                GraphicsContextFlags.Default)
        {
            Title += ": OpenGL Version: " + GL.GetString(StringName.Version);
        }

        protected override void OnLoad(EventArgs e)
        {
            CursorVisible = true;
            program = CompileShaders();
            GL.GenVertexArrays(1, out vertexArray);
            GL.BindVertexArray(vertexArray);
            Closed += OnClosed;
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            Exit();
        }

        public override void Exit()
        {
            GL.DeleteVertexArrays(1, ref vertexArray);
            GL.DeleteProgram(program);
            base.Exit();
        }

        private int CompileShaders()
        {
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, File.ReadAllText(@"Shaders\vertexShader.vert"));
            GL.CompileShader(vertexShader);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, File.ReadAllText(@"Shaders\fragmentShader.frag"));
            GL.CompileShader(fragmentShader);

            var prog = GL.CreateProgram();
            GL.AttachShader(prog, vertexShader);
            GL.AttachShader(prog, fragmentShader);
            GL.LinkProgram(prog);

            GL.DetachShader(prog, vertexShader);
            GL.DetachShader(prog, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            return prog;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(program);

            // Рисование тут:
            DrawLine(new Point(0, 0), new Point(100, 200), 5, Color4.White);
            
            var point = new Point(100, 50);
            var rotatedPoint = point.Rotate(new Point(-100, 0), Math.PI / 2);
            DrawPoint(rotatedPoint.X, rotatedPoint.Y, 5, Color4.Blue);

            SwapBuffers();
        }

        private void DrawLine(Point start, Point end, int width, Color4 color)
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

        private void DrawPoint(float x, float y, float size, Color4 color)
        {
            var position = new Vector2(x / Width, y / Height);
            GL.VertexAttrib2(0, position);
            var colorArr = new[] { color.R, color.G, color.B, color.A };
            GL.VertexAttrib4(1, colorArr);
            GL.PointSize(size);
            GL.DrawArrays(PrimitiveType.Points, 0, 1);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            var keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Key.Escape))
                Exit();
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
        }
    }
}