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

        public string GetDirection()
        {
            // Calculate the angle between the direction vector and the positive Y-axis
            double angle = Math.Atan2(Direction.Y, Direction.X) * (180 / Math.PI);

            // Adjust the angle to be positive
            if (angle < 0)
            {
                angle += 360;
            }

            // Determine the direction based on the angle
            if (angle >= 337.5 || angle < 22.5)
            {
                return "EAST";
            }
            else if (angle >= 22.5 && angle < 67.5)
            {
                return "NORTH-EAST";
            }
            else if (angle >= 67.5 && angle < 112.5)
            {
                return "NORTH";
            }
            else if (angle >= 112.5 && angle < 157.5)
            {
                return "NORTH-WEST";
            }
            else if (angle >= 157.5 && angle < 202.5)
            {
                return "WEST";
            }
            else if (angle >= 202.5 && angle < 247.5)
            {
                return "SOUTH-WEST";
            }
            else if (angle >= 247.5 && angle < 292.5)
            {
                return "SOUTH";
            }
            else // angle >= 292.5 && angle < 337.5
            {
                return "SOUTH-EAST";
            }
        }
    }
}
