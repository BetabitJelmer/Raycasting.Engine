using System.Numerics;

namespace Raycasting.Domain
{
    public class Camera
    {
        public Vector2 Plane { get; private set; } = new Vector2(0, 1);

        public void MovePlane(float x, float y)
        {
            Plane = new Vector2(x, y);
        }
    }
}
