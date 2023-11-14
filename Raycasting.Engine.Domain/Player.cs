using System.Numerics;

namespace Raycasting.Domain
{
    public class Player
    {
        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; private set; }

        public void MoveTo(float x, float y)
        {
            Position = new Vector2(x, y);
        }

        public void FaceTo(float x, float y)
        {
            Direction = new Vector2(x, y);
        }
    }
}
