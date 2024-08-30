using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace ConsoleApp6
{
    public class Game : GameWindow
    {
        private LetraT LetraT;
        private int shaderProgramObject;
        private Matrix4 model;
        private Matrix4 view;
        private Matrix4 projection;
        private float rotationAngle;
        private float rotationSpeed = 0.1f;

        public Game()
            : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            CenterWindow(new Vector2i(800, 600));
        }

        protected override void OnLoad()
        {
            GL.ClearColor(new Color4(0.3f, 0.4f, 0.5f, 1f));

            // posision (x,y,z), float anchura x, float altura y, float profundidad z, float largo del brazo en x
            LetraT = new LetraT(new Vector3(0, 0, 0), 0.2f, 1f, 0.2f, 1.5f); 

            int vertexShaderObject = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderObject, Shaders.VertexShaderCode);
            GL.CompileShader(vertexShaderObject);

            int pixelShaderObject = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(pixelShaderObject, Shaders.PixelShaderCode);
            GL.CompileShader(pixelShaderObject);

            shaderProgramObject = GL.CreateProgram();
            GL.AttachShader(shaderProgramObject, vertexShaderObject);
            GL.AttachShader(shaderProgramObject, pixelShaderObject);
            GL.LinkProgram(shaderProgramObject);
            GL.DetachShader(shaderProgramObject, vertexShaderObject);
            GL.DetachShader(shaderProgramObject, pixelShaderObject);
            GL.DeleteShader(vertexShaderObject);
            GL.DeleteShader(pixelShaderObject);

            model = Matrix4.Identity;
            view = Matrix4.LookAt(new Vector3(0, 0, 3), Vector3.Zero, Vector3.UnitY);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), Size.X / (float)Size.Y, 0.1f, 100.0f);

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            rotationAngle += rotationSpeed * (float)args.Time;
            model = Matrix4.CreateRotationY(rotationAngle) * Matrix4.CreateRotationX(rotationAngle / 2);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(shaderProgramObject);

            int modelLocation = GL.GetUniformLocation(shaderProgramObject, "model");
            int viewLocation = GL.GetUniformLocation(shaderProgramObject, "view");
            int projectionLocation = GL.GetUniformLocation(shaderProgramObject, "projection");

            GL.UniformMatrix4(modelLocation, false, ref model);
            GL.UniformMatrix4(viewLocation, false, ref view);
            GL.UniformMatrix4(projectionLocation, false, ref projection);

            LetraT.Dibujar();

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Up))
            {
                rotationSpeed += 0.1f;
            }

            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Down))
            {
                rotationSpeed = Math.Max(0.1f, rotationSpeed - 0.1f);
            }

            base.OnUpdateFrame(args);
        }

        protected override void OnUnload()
        {
            LetraT.Dispose();
            GL.UseProgram(0);
            GL.DeleteProgram(shaderProgramObject);
            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), e.Width / (float)e.Height, 0.1f, 100.0f);
            base.OnResize(e);
        }
    }
}
