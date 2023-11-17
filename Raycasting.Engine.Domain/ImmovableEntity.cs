using System.Numerics;

namespace Raycasting.Domain
{
    public interface Entity
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int Texture { get; set; }
    }
}
