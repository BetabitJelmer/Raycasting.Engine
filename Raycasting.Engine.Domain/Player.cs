using System.Numerics;

namespace Raycasting.Engine.Domain
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
            if (angle is >= 337.5 or < 22.5)
            {
                return "EAST";
            }
            else if (angle is >= 22.5 and < 67.5)
            {
                return "NORTH-EAST";
            }
            else if (angle is >= 67.5 and < 112.5)
            {
                return "NORTH";
            }
            else if (angle is >= 112.5 and < 157.5)
            {
                return "NORTH-WEST";
            }
            else if (angle is >= 157.5 and < 202.5)
            {
                return "WEST";
            }
            else if (angle is >= 202.5 and < 247.5)
            {
                return "SOUTH-WEST";
            }
            else
            {
                return angle is >= 247.5 and < 292.5 ? "SOUTH" : "SOUTH-EAST";
            }
        }
    }
}
