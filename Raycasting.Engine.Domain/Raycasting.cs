using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Numerics;
using System.Security.Cryptography.Xml;

namespace Raycasting.Domain
{
    public class Raycasting
    {
        private readonly Map _map = new();
        public readonly Player _player = new();
        public readonly Camera _camera = new();

        // This is used to keep track of the current frames per second.
        private double _framesPerSecond = 0;
        // Keeps track of how many frames have been drawn since last fps check.
        private int _framesRendered;
        // Keeps track of the last time fps was checked.
        private DateTime _lastTime;

        // These are for the moving and turning of the player
        private double _moveSpeed = 0;
        private double _rotSpeed = 0;

        public Raycasting()
        {
            _player.MoveTo(2, 2);
            _player.FaceTo(-1, 0);
        }

        public void Turn(bool turnRight)
        {
            if (!turnRight)
            {
                // We use a rotation matrix to rotate the plane and direction vectors.
                // First we keep track of the old direction, so that the transformation on X first
                // doesn't affect the Y transformation.
                Vector2 oldDirection = new Vector2(_player.Direction.X, _player.Direction.Y);
                var playerx = (float)(_player.Direction.X * Math.Cos(_rotSpeed) - _player.Direction.Y * Math.Sin(_rotSpeed));
                var playery = (float)(oldDirection.X * Math.Sin(_rotSpeed) + _player.Direction.Y * Math.Cos(_rotSpeed));

                _player.FaceTo(playerx, playery);

                Vector2 oldPlane = new Vector2(_camera.Plane.X, _camera.Plane.Y);
                var camerax = (float)(_camera.Plane.X * Math.Cos(_rotSpeed) - _camera.Plane.Y * Math.Sin(_rotSpeed));
                var camery = (float)(oldPlane.X * Math.Sin(_rotSpeed) + _camera.Plane.Y * Math.Cos(_rotSpeed));

                _camera.MovePlane(camerax, camery);
            }
            else
            {
                Vector2 oldDirection = new Vector2(_player.Direction.X, _player.Direction.Y);
                var playerx = (float)(_player.Direction.X * Math.Cos(-_rotSpeed) - _player.Direction.Y * Math.Sin(-_rotSpeed));
                var playery = (float)(oldDirection.X * Math.Sin(-_rotSpeed) + _player.Direction.Y * Math.Cos(-_rotSpeed));

                _player.FaceTo(playerx, playery);

                Vector2 oldPlane = new Vector2(_camera.Plane.X, _camera.Plane.Y);
                var camerax = (float)(_camera.Plane.X * Math.Cos(-_rotSpeed) - _camera.Plane.Y * Math.Sin(-_rotSpeed));
                var camery = (float)(oldPlane.X * Math.Sin(-_rotSpeed) + _camera.Plane.Y * Math.Cos(-_rotSpeed));

                _camera.MovePlane(camerax, camery);
            }
        }

        public void Move(bool? forwards, bool? left)
        {
            // If we're not moving, we can just return.
            if (forwards == null && left == null)
            {
                return;
            }

            var playerx = _player.Position.X;
            var playery = _player.Position.Y;

            if (left == true)
            {
                var newPositionX = (_player.Position.X - _camera.Plane.X * _moveSpeed);
                var newPositionY = (_player.Position.Y - _camera.Plane.Y * _moveSpeed);

                // First we check that moving wont put us in a wall
                if ((newPositionX > 0 && newPositionX < _map.currentMap.GetLength(1)) && _map.currentMap[(int)newPositionX, (int)(_player.Position.Y)] == 0)
                {
                    // If it doesnt put us in a wall, we can move forwards (or backwards).
                    playerx = (float)(_player.Position.X - _camera.Plane.X * _moveSpeed);
                }
                if ((newPositionY > 0.00 && newPositionY < _map.currentMap.GetLength(0)) && _map.currentMap[(int)(_player.Position.X), (int)newPositionY] == 0)
                {
                    playery = (float)(_player.Position.Y - _camera.Plane.Y * _moveSpeed);
                }
            }
            else if (left == false)
            {
                var newPositionX = (_player.Position.X + _camera.Plane.X * _moveSpeed);
                var newPositionY = (_player.Position.Y + _camera.Plane.Y * _moveSpeed);

                // First we check that moving wont put us in a wall
                if ((newPositionX > 0 && newPositionX < _map.currentMap.GetLength(1)) && _map.currentMap[(int)newPositionX, (int)(_player.Position.Y)] == 0)
                {
                    // If it doesnt put us in a wall, we can move forwards (or backwards).
                    playerx = (float)(_player.Position.X + _camera.Plane.X * _moveSpeed);
                }
                if ((newPositionY > 0.00 && newPositionY < _map.currentMap.GetLength(0)) && _map.currentMap[(int)(_player.Position.X), (int)newPositionY] == 0)
                {
                    playery = (float)(_player.Position.Y + _camera.Plane.Y * _moveSpeed);
                }
            }

            if (forwards == true)
            {
                var newPositionX = (_player.Position.X + _player.Direction.X * _moveSpeed);
                var newPositionY = (_player.Position.Y + _player.Direction.Y * _moveSpeed);

                // First we check that moving wont put us in a wall
                if ((newPositionX > 0 && newPositionX < _map.currentMap.GetLength(1)) && _map.currentMap[(int)newPositionX, (int)(_player.Position.Y)] == 0)
                {
                    // If it doesnt put us in a wall, we can move forwards (or backwards).
                    playerx = (float)(_player.Position.X + _player.Direction.X * _moveSpeed);
                }
                if ((newPositionY > 0.00 && newPositionY < _map.currentMap.GetLength(0)) && _map.currentMap[(int)(_player.Position.X), (int)newPositionY] == 0)
                {
                    playery = (float)(_player.Position.Y + _player.Direction.Y * _moveSpeed);
                }
            }
            else if(forwards == false)
            {
                var newPositionX = (_player.Position.X - _player.Direction.X * _moveSpeed);
                var newPositionY = (_player.Position.Y - _player.Direction.Y * _moveSpeed);

                // First we check that moving wont put us in a wall
                if ((newPositionX > 0.00 && newPositionX < _map.currentMap.GetLength(1)) && _map.currentMap[(int)newPositionX, (int)(_player.Position.Y)] == 0)
                {
                    // If it doesnt put us in a wall, we can move forwards (or backwards).
                    playerx = (float)(_player.Position.X - _player.Direction.X * _moveSpeed);
                }
                if ((newPositionY > 0.00 && newPositionY < _map.currentMap.GetLength(0)) && _map.currentMap[(int)(_player.Position.X), (int)newPositionY] == 0)
                {
                    playery = (float)(_player.Position.Y - _player.Direction.Y * _moveSpeed);
                }
            }

            _player.MoveTo(playerx, playery);
        }

        public void UpdateFramerate(double frameTime)
        {
            // Add one to the count of frames rendered.
            _framesRendered++;
            // If a second has elapsed, we can update the fps.
            if ((DateTime.Now - _lastTime).TotalSeconds >= 1)
            {
                _lastTime = DateTime.Now;
                _framesPerSecond = _framesRendered;
                _framesRendered = 0;
            }

            // The frameTime is the time between 2 frames, it is used to keep speed constant regardless of the 
            // framerate that the application is running at.
            frameTime = frameTime / 1000;
            // Each frame we update the speed based on how long the frames are taking.
            _moveSpeed = frameTime * 5.0;
            _rotSpeed = frameTime * 3.0;
        }

        /// <summary>
        /// Adds text to the given frame.
        /// </summary>
        /// <param name="x">Text X position</param>
        /// <param name="y">Text Y position</param>
        /// <param name="size">Text size</param>
        /// <param name="text">Text to be drawn</param>
        /// <param name="color">Text color</param>
        /// <param name="frame">Frame to add to</param>
        private void AddTextToFrame(int x, int y, int size, string text, Color color, Bitmap frame)
        {
            using Graphics g = Graphics.FromImage(frame);
            SolidBrush b = new(color);
            Font f = new("Consolas", size);
            TextRenderer.DrawText(g, text, f, new Point(x, y), color);
        }

        public unsafe Image NewFrame(int width, int height)
        {
            double[] ZBuffer = new double[width];
            var frame = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            var data = frame.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            var span = new Span<int>(data.Scan0.ToPointer(), width * height);

            var halfSpan = span.Slice(0, span.Length / 2);
            halfSpan.Fill(unchecked((int)0xFF242121));

            var secondHalfSpan = span.Slice(span.Length / 2);
            secondHalfSpan.Fill(unchecked((int)0xFF5C5858));

            var stride = data.Stride;
            var scan0 = data.Scan0;

            Parallel.For(0, width, x =>
            {
                // This var tracks the relative position of the ray on the camera plane, from -1 to 1, with 0 being screen centre
                // so that we can use it to muliply the half-length of the camera plane to get the right direction of the ray.
                double cameraX = 2 * (x / Convert.ToDouble(width)) - 1;
                // This vector holds the direction the current ray is pointing.
                Vector2 rayDir = new Vector2(
                    (float)(_player.Direction.X + _camera.Plane.X * cameraX),
                    (float)(_player.Direction.Y + _camera.Plane.Y * cameraX));

                // This holds the absolute SQUARE of the map the ray is in, regardless of position
                // within that square.
                int mapX = (int)_player.Position.X;
                int mapY = (int)_player.Position.Y;
                // These two variables track the distance to the next side of a map square from the player, 
                // e.g where the ray touches the horizontal side of a square, the distance is sideDistX and vertical square sideDistY.
                double sideDistX;
                double sideDistY;
                // These two variables are the distance between map square side intersections
                double deltaDistX = Math.Abs(1 / rayDir.X);
                double deltaDistY = Math.Abs(1 / rayDir.Y);

                // Each time we check the next square we step either 1 in the x or 1 in the y, they will be 1 or -1 depending on whether 
                // the character is facing towards the origin or away.
                int stepX;
                int stepY;

                // Finally, these two track whether a wall was hit, and the side tracks which side, horizontal or vertical was hit.
                // A horizontal side givess 0 and a vertical side is 1.
                bool wallHit = false;

                int side = 0;

                // Now we calculate the way we will step based upon the direction the character is facing
                // And the initial sideDist based upon this direction, and the deltaDist
                if (rayDir.X < 0)
                {
                    stepX = -1;
                    sideDistX = (_player.Position.X - mapX) * deltaDistX;
                }
                else
                {
                    stepX = 1;
                    sideDistX = (mapX + 1.0 - _player.Position.X) * deltaDistX;
                }
                if (rayDir.Y < 0)
                {
                    stepY = -1;
                    sideDistY = (_player.Position.Y - mapY) * deltaDistY;
                }
                else
                {
                    stepY = 1;
                    sideDistY = (mapY + 1.0 - _player.Position.Y) * deltaDistY;
                }

                // Now we loop steping until we hit a wall
                while (!wallHit)
                {
                    // Here we check which distance is closer to us, x or y, and increment the lesser
                    if (sideDistX < sideDistY)
                    {
                        // Increase the distance we've travelled.
                        sideDistX += deltaDistX;
                        // Set the ray's mapX to the new square we've reached.
                        mapX += stepX;
                        // Set it so the side we're currently on is an X side.
                        side = 0;
                    }
                    else
                    {
                        sideDistY += deltaDistY;
                        mapY += stepY;
                        side = 1;
                    }

                    // Check if the ray is not on the side of a square that is a wall.
                    if (_map.currentMap[mapX, mapY] > 0)
                    {
                        wallHit = true;
                    }
                }

                drawWall(mapX, mapY, side, x, rayDir, stride, scan0, height, stepX, stepY, ZBuffer);
            });
            drawEntities(width, height, stride, scan0, ZBuffer);

            frame.UnlockBits(data);

            // Finally we add any text we want to the frame now its all drawn.
            AddTextToFrame(10, 10, 12, _framesPerSecond.ToString(), Color.White, frame);

            return frame;
        }

        // parameters
        // mapX, mapY: the coordinates of the wall on the map
        // side: 0 for horizontal, 1 for vertical
        // x: the x coordinate on the screen, calculated earlier
        // rayDir: the direction of the ray
        // stride: the stride of the bitmap
        // scan0: the scan0 of the bitmap
        // height: the height of the screen
        // stepX, stepY: the direction of the ray
        private unsafe void drawWall(int mapX, int mapY, int side, int x, Vector2 rayDir, int stride, nint scan0, int height, int stepX, int stepY, double[] ZBuffer)
        {
            // This var is for the overall length of the ray calculations
            double perpWallDist;
            // Now we've found where the next wall is, we have to find the actual distance.
            if (side == 0)
            {
                perpWallDist = ((mapX - _player.Position.X + ((1 - stepX) / 2)) / rayDir.X);
            }
            else
            {
                perpWallDist = ((mapY - _player.Position.Y + ((1 - stepY)) / 2)) / rayDir.Y;
            }

            // Here we'll start drawing the column of pixels, now we know what, and how far away.
            // First we find the height of the wall, e.g how much of the screen it should take up
            int columnHeight = (int)(height / perpWallDist);
            // Next we need to find where to start drawing the column and where to stop, since the walls
            // will be in the centre of the screen, finding the start and end is quite simple.
            int drawStart = ((height / 2) + (columnHeight / 2));
            // If we are going to be drawing off-screen, then draw just on screen.
            if (drawStart >= height)
            {
                drawStart = height - 1;
            }
            int drawEnd = ((height / 2) - (columnHeight / 2));
            if (drawEnd < 0)
            {
                drawEnd = 0;
            }

            // Now we pick the colour to draw the line in, this is based upon the colour of the wall
            // and is then made darker if the wall is x aligned or y aligned.
            int texNum = _map.currentMap[mapX, mapY] - 1;

            double wallX;

            if (side == 0)
            {
                wallX = _player.Position.Y + perpWallDist * rayDir.Y;

            }
            else
            {
                wallX = _player.Position.X + perpWallDist * rayDir.X;
            }

            wallX -= Math.Floor(wallX);

            int texX = (int)(wallX * _map.TextureWidth);

            var column = (int*)(scan0 + (x * 4));

            for (int y = drawEnd; y < drawStart; y++)
            {
                int d = y * 256 - height * 128 + columnHeight * 128;

                int texY = d * _map.TextureHeight / columnHeight / 256;

                if (texY < 0)
                {
                    texY = 0;
                }

                column[y * stride / 4] = _map.GetColorForTexture(texX, texY, texNum, side);
            }

            //SET THE ZBUFFER FOR THE SPRITE CASTING
            ZBuffer[x] = perpWallDist; //perpendicular distance is used
        }

        // parameters
        // width: the width of the screen
        // height: the height of the screen
        // stride: the stride of the bitmap
        // scan0: the scan0 of the bitmap
        // ZBuffer: the ZBuffer of the screen
        private unsafe void drawEntities(int width, int height, int stride, nint scan0, double[] ZBuffer)
        {
            var length = _map.entities.Length;
            var spriteOrder = new int[length];
            var spriteDistance = new double[length];

            for (int i = 0; i < length; i++)
            {
                spriteOrder[i] = i;
                spriteDistance[i] = ((_player.Position.X - _map.entities[i].X) * (_player.Position.X - _map.entities[i].X) + (_player.Position.Y - _map.entities[i].Y) * (_player.Position.Y - _map.entities[i].Y)); //sqrt not taken, unneeded
            }

            sortSprites(spriteOrder, spriteDistance, length);

            //after sorting the sprites, do the projection and draw them
            for (int i = 0; i < length; i++)
            {
                //translate sprite position to relative to camera
                double spriteX = _map.entities[spriteOrder[i]].X - _player.Position.X;
                double spriteY = _map.entities[spriteOrder[i]].Y - _player.Position.Y;

                double invDet = 1.0 / (_camera.Plane.X * _player.Direction.Y - _player.Direction.X * _camera.Plane.Y);
                double transformX = invDet * (_player.Direction.Y * spriteX - _player.Direction.X * spriteY);
                double transformY = invDet * (-_camera.Plane.Y * spriteX + _camera.Plane.X * spriteY); //this is actually the depth inside the screen, that what Z is in 3D

                int spriteScreenX = (int)((width / 2) * (1 + transformX / transformY));
                //calculate height of the sprite on screen
                int spriteHeight = Math.Abs((int)(height / (transformY))); //using 'transformY' instead of the real distance prevents fisheye
                                                               //calculate lowest and highest pixel to fill in current stripe
                int drawStartY = -spriteHeight / 2 + height / 2;
                if (drawStartY < 0)
                {
                    drawStartY = 0;
                }
                int drawEndY = spriteHeight / 2 + height / 2;
                if (drawEndY >= height)
                {
                    drawEndY = height - 1;
                }

                //calculate width of the sprite
                int spriteWidth = Math.Abs((int)(height / (transformY)));
                int drawStartX = -spriteWidth / 2 + spriteScreenX;
                if (drawStartX < 0)
                { 
                    drawStartX = 0; 
                }
                int drawEndX = spriteWidth / 2 + spriteScreenX;
                if (drawEndX >= width)
                {
                    drawEndX = width - 1;
                }

                //loop through every vertical stripe of the sprite on screen
                for (int stripe = drawStartX; stripe < drawEndX; stripe++)
                {
                    int texX = (int)(256 * (stripe - (-spriteWidth / 2 + spriteScreenX)) * _map.TextureWidth / spriteWidth) / 256;
                    //the conditions in the if are:
                    //1) it's in front of camera plane so you don't see things behind you
                    //2) it's on the screen (left)
                    //3) it's on the screen (right)
                    //4) ZBuffer, with perpendicular distance

                    var column = (int*)(scan0 + (stripe * 4));
                    var floatHeight = height * 128;
                    var floatSpriteHeight = spriteHeight * 128;

                    if (transformY > 0 && stripe > 0 && stripe < width && transformY < ZBuffer[stripe])
                    {
                        for (int y = drawStartY; y < drawEndY; y++) //for every pixel of the current stripe
                        {
                            int d = (y) * 256 - floatHeight + floatSpriteHeight; //256 and 128 factors to avoid floats
                            int texY = ((d * _map.TextureHeight) / spriteHeight) / 256;
                            if (texY < 0)
                            {
                                texY = 0;
                            }
                            if (texX < 0)
                            {
                                texX = 0;
                            }
                            var pixel = _map.GetColorForObject(texX, texY, _map.entities[spriteOrder[i]].Texture);
                            if ((pixel & 0x00FFFFFF) != 0)
                            {
                                column[y * stride / 4] = pixel;
                            }
                        }
                    }
                }
            }
        }

        private void sortSprites(int[] order, double[] dist, int amount)
        {
            var sprites = new List<(double, int)>(amount);
            for (int i = 0; i < amount; i++)
            {
                sprites.Add((dist[i], order[i]));
            }
            sprites.Sort();
            for (int i = 0; i < amount; i++)
            {
                dist[i] = sprites[amount - i - 1].Item1;
                order[i] = sprites[amount - i - 1].Item2;
            }
        }
    }
}