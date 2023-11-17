using System.Numerics;

namespace Raycasting.Domain
{
    public class Door : Interractable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Texture { get; set; }
        public bool IsVisible { get; set; }

        public void Interract()
        {
            IsVisible = !IsVisible;
        }
    }
}
