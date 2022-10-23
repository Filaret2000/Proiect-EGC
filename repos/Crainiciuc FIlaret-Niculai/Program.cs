using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

//Crainiciuc FIlaret-Niculai
//Grupa 3131A

namespace Program
{
    class SimpleWindow3D : GameWindow
    {

        float angle = 0f;
        float moving_angle = 5f;
        bool showCube = true;
        KeyboardState lastKeyPress;
        MouseState lastMouseState;

        public SimpleWindow3D() : base(800, 600)
        {
            VSync = VSyncMode.On;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.SteelBlue);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard[Key.Escape])
            {
                Exit();
                return;
            }
            else if (keyboard[Key.P] && !keyboard.Equals(lastKeyPress))
            {
                if (showCube == true)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }

            lastKeyPress = keyboard;

            if(keyboard[Key.A])
            {
                angle -= moving_angle;
            }

            if (keyboard[Key.D])
            {
                angle += moving_angle;
            }
            

            if (!mouse.Equals(lastMouseState) && mouse[MouseButton.Left])
            {
                if (lastMouseState.X < mouse.X)
                {
                    angle -= moving_angle;
                }
                else if (lastMouseState.X > mouse.X)
                {
                    angle += moving_angle;
                }
            }

            lastMouseState = mouse;
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            Matrix4 lookat = Matrix4.LookAt(15, 50, 15, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
            GL.Rotate(angle, 0.0f, 1.0f, 0.0f);

            if (showCube == true)
            {
                DrawCube();
                DrawAxes_OLD();
            }
            SwapBuffers();
        }
        private void DrawAxes_OLD()
        {
            GL.Begin(PrimitiveType.Lines);
            // X
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(20, 0, 0);
            // Y
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 20, 0);
            // Z
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 20);

            GL.End();
        }
        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Silver);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color3(Color.DeepPink);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            GL.Color3(Color.Moccasin);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(Color.LawnGreen);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(Color.PaleVioletRed);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(Color.ForestGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            GL.End();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow3D example = new SimpleWindow3D())
            {
                example.Run(60.0, 0.0);
            }
        }
    }
}
