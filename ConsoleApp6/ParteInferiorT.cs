using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    using OpenTK.Mathematics;
    using OpenTK.Graphics.OpenGL;

    namespace ConsoleApp6
    {
        public class ParteInferiorT
        {
            private int vertexArrayObject;
            private int vertexbufferObject;
            private int elementBufferObject;

            private float[] vertices;
            private uint[] indices;

            public ParteInferiorT(Vector3 origin, float width, float height, float depth)
            {
                vertices = GenerateVertices(origin, width, height, depth);
                indices = GenerateIndices();
                InitializeBuffers();
            }

            private float[] GenerateVertices(Vector3 origin, float width, float height, float depth)
            {
                float halfWidth = width / 2;
                float halfHeight = height / 2;
                float halfDepth = depth / 2;

                return new float[]
                {
                // Vértices de la parte vertical de la T
                origin.X - halfWidth, origin.Y + halfHeight, origin.Z + halfDepth,  1.0f, 0.0f, 0.0f,
                origin.X + halfWidth, origin.Y + halfHeight, origin.Z + halfDepth,  1.0f, 0.0f, 0.0f,
                origin.X + halfWidth, origin.Y - halfHeight, origin.Z + halfDepth,  1.0f, 0.0f, 0.0f,
                origin.X - halfWidth, origin.Y - halfHeight, origin.Z + halfDepth,  1.0f, 0.0f, 0.0f,

                origin.X - halfWidth, origin.Y + halfHeight, origin.Z - halfDepth,  1.0f, 0.0f, 0.0f,
                origin.X + halfWidth, origin.Y + halfHeight, origin.Z - halfDepth,  1.0f, 0.0f, 0.0f,
                origin.X + halfWidth, origin.Y - halfHeight, origin.Z - halfDepth,  1.0f, 0.0f, 0.0f,
                origin.X - halfWidth, origin.Y - halfHeight, origin.Z - halfDepth,  1.0f, 0.0f, 0.0f,
                };
            }

            private uint[] GenerateIndices()
            {
                return new uint[]
                {
                0, 1, 2, 0, 2, 3,
                4, 5, 6, 4, 6, 7,
                0, 4, 7, 0, 7, 3,
                1, 5, 6, 1, 6, 2,
                0, 1, 5, 0, 5, 4,
                3, 2, 6, 3, 6, 7,
                };
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
}
