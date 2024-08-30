using OpenTK.Mathematics;

namespace ConsoleApp6
{
    public static class VerticesT
    {
        public static float[] GenerateVertices(Vector3 origin, float width, float height, float depth, float horizontalStretch)
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

        // Vértices de la parte horizontal de la T (brazos)
        origin.X - 2 * halfWidth * horizontalStretch, origin.Y + halfHeight, origin.Z + halfDepth,  0.0f, 1.0f, 0.0f,
        origin.X + 2 * halfWidth * horizontalStretch, origin.Y + halfHeight, origin.Z + halfDepth,  0.0f, 1.0f, 0.0f,
        origin.X + 2 * halfWidth * horizontalStretch, origin.Y + halfHeight - 0.2f, origin.Z + halfDepth,  0.0f, 1.0f, 0.0f,
        origin.X - 2 * halfWidth * horizontalStretch, origin.Y + halfHeight - 0.2f, origin.Z + halfDepth,  0.0f, 1.0f, 0.0f,

        origin.X - 2 * halfWidth * horizontalStretch, origin.Y + halfHeight, origin.Z - halfDepth,  0.0f, 1.0f, 0.0f,
        origin.X + 2 * halfWidth * horizontalStretch, origin.Y + halfHeight, origin.Z - halfDepth,  0.0f, 1.0f, 0.0f,
        origin.X + 2 * halfWidth * horizontalStretch, origin.Y + halfHeight - 0.2f, origin.Z - halfDepth,  0.0f, 1.0f, 0.0f,
        origin.X - 2 * halfWidth * horizontalStretch, origin.Y + halfHeight - 0.2f, origin.Z - halfDepth,  0.0f, 1.0f, 0.0f,
            };
        }


        public static uint[] GenerateIndices()
        {
            return new uint[]
            {
                // Indices para la parte vertical de la T
                0, 1, 2, 0, 2, 3,
                4, 5, 6, 4, 6, 7,
                0, 4, 7, 0, 7, 3,
                1, 5, 6, 1, 6, 2,
                0, 1, 5, 0, 5, 4,
                3, 2, 6, 3, 6, 7,

                // Indices para la parte horizontal de la T
                8, 9, 10, 8, 10, 11,
                12, 13, 14, 12, 14, 15,
                8, 12, 15, 8, 15, 11,
                9, 13, 14, 9, 14, 10,
                8, 9, 13, 8, 13, 12,
                11, 10, 14, 11, 14, 15,
            };
        }
    }
}
