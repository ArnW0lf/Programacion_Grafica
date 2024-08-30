using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace ConsoleApp6
{
    public class LetraT
    {
        private int vertexArrayObject;
        private int vertexbufferObject;
        private int elementBufferObject;

        private float[] vertices;
        private uint[] indices;

        public LetraT(Vector3 origin, float width, float height, float depth, float horizontalStretch)
        {
            vertices = VerticesT.GenerateVertices(origin, width, height, depth, horizontalStretch);
            indices = VerticesT.GenerateIndices();
            InitializeBuffers();
        }


        private void InitializeBuffers()
        {
            vertexbufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexbufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexbufferObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BindVertexArray(0);
        }

        public void Dibujar()
        {
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(vertexbufferObject);
            GL.DeleteBuffer(elementBufferObject);
            GL.DeleteVertexArray(vertexArrayObject);
        }
    }
}
