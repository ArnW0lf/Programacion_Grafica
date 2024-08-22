using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class Cubo
    {
        private int vertexArrayObject;
        private int vertexbufferObject;
        private int elementBufferObject;

        private Vector3 origin;
        private float halfWidth, halfHeight, halfDepth;

        private float[] vertices;
        private uint[] indices;
        private Color4 color; // Color del cubo

        public Cubo(Vector3 origin, float width, float height, float depth, Color4 color)
        {
            this.origin = origin;
            this.halfWidth = width / 2;
            this.halfHeight = height / 2;
            this.halfDepth = depth / 2;
            this.color = color;

            // Define vertices and indices relative to the origin
            CreateVertexData();
            CreateIndexData();
            InitializeBuffers();
        }

        private void CreateVertexData()
        {
            vertices = new float[]
            {
            // Posiciones (x, y, z) y Colores (r, g, b)
            // Primer cubo (parte horizontal de la T)
            origin.X - halfWidth, origin.Y + halfHeight, origin.Z + halfDepth,  color.R, color.G, color.B,
            origin.X + halfWidth, origin.Y + halfHeight, origin.Z + halfDepth,  color.R, color.G, color.B,
            origin.X + halfWidth, origin.Y - halfHeight, origin.Z + halfDepth,  color.R, color.G, color.B,
            origin.X - halfWidth, origin.Y - halfHeight, origin.Z + halfDepth,  color.R, color.G, color.B,

            origin.X - halfWidth, origin.Y + halfHeight, origin.Z - halfDepth,  color.R, color.G, color.B,
            origin.X + halfWidth, origin.Y + halfHeight, origin.Z - halfDepth,  color.R, color.G, color.B,
            origin.X + halfWidth, origin.Y - halfHeight, origin.Z - halfDepth,  color.R, color.G, color.B,
            origin.X - halfWidth, origin.Y - halfHeight, origin.Z - halfDepth,  color.R, color.G, color.B,

            };
        }

        private void CreateIndexData()
        {
            indices = new uint[]
            {
            // Primer cubo
            0, 1, 2, 0, 2, 3,
            4, 5, 6, 4, 6, 7,
            0, 4, 7, 0, 7, 3,
            1, 5, 6, 1, 6, 2,
            0, 1, 5, 0, 5, 4,
            3, 2, 6, 3, 6, 7,

            // Segundo cubo
            // ... (Puedes definir los índices del segundo cubo si es necesario)
            // Segundo cubo
            8, 9, 10, 8, 10, 11, // Cara frontal
            12, 13, 14, 12, 14, 15, // Cara trasera
            8, 12, 15, 8, 15, 11,   // Cara izquierda
            9, 13, 14, 9, 14, 10,   // Cara derecha
            8, 9, 13, 8, 13, 12,    // Cara superior
            11, 10, 14, 11, 14, 15  // Cara inferior
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

            // Configura las posiciones
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Configura los colores
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
