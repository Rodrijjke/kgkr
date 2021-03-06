﻿using System;
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
        private readonly PrimitivesDrawer drawer;

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

            drawer = new PrimitivesDrawer(Width, Height);
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
            drawer.DrawLine(new Point(0, 0), new Point(100, 200), 5, Color4.White);
            
            var point = new Point(100, 50);
            var rotatedPoint = point.Rotate(new Point(-100, 0), Math.PI / 2);
            drawer.DrawPoint(rotatedPoint.X, rotatedPoint.Y, 5, Color4.Blue);

            SwapBuffers();
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