using ConsoleApp6.ConsoleApp6;
using OpenTK.Mathematics;

namespace ConsoleApp6
{
    public class LetraT
    {
        private ParteSuperiorT parteSuperior;
        private ParteInferiorT parteInferior;

        public LetraT(Vector3 origin, float width, float height, float depth, float horizontalStretch)
        {
            parteSuperior = new ParteSuperiorT(origin, width*(1.2f), height*(0.4f), depth, horizontalStretch);
            parteInferior = new ParteInferiorT(origin - new Vector3(0, height / 2, 0), width, height, depth);
        }

        public void Dibujar()
        {
            parteSuperior.Dibujar();
            parteInferior.Dibujar();
        }

        public void Dispose()
        {
            parteSuperior.Dispose();
            parteInferior.Dispose();
        }
    }
}