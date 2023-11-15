using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace Raycasting.Domain
{
    public class Map
    {

        public int[,] currentMap = new int[,]
        {
            {8,8,8,8,8,8,8,8,8,8,8,4,4,6,4,4,6,4,6,4,4,4,6,4},
            {8,0,0,0,0,0,0,0,0,0,8,4,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,3,3,0,0,0,0,0,8,8,4,0,0,0,0,0,0,0,0,0,0,0,6},
            {8,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6},
            {8,0,3,3,0,0,0,0,0,8,8,4,0,0,0,0,0,0,0,0,0,0,0,4},
            {8,0,0,0,0,0,0,0,0,0,8,4,0,0,0,0,0,6,6,6,0,6,4,6},
            {8,8,8,8,0,8,8,8,8,8,8,4,4,4,4,4,4,6,0,0,0,0,0,6},
            {7,7,7,7,0,7,7,7,7,0,8,0,8,0,8,0,8,4,0,4,0,6,0,6},
            {7,7,0,0,0,0,0,0,7,8,0,8,0,8,0,8,8,6,0,0,0,0,0,6},
            {7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,6,0,0,0,0,0,4},
            {7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,6,0,6,0,6,0,6},
            {7,7,0,0,0,0,0,0,7,8,0,8,0,8,0,8,8,6,4,6,0,6,6,6},
            {7,7,7,7,0,7,7,7,7,8,8,4,0,6,8,4,8,3,3,3,0,3,3,3},
            {2,2,2,2,0,2,2,2,2,4,6,4,0,0,6,0,6,3,0,0,0,0,0,3},
            {2,2,0,0,0,0,0,2,2,4,0,0,0,0,0,0,4,3,0,0,0,0,0,3},
            {2,0,0,0,0,0,0,0,2,4,0,0,0,0,0,0,4,3,0,0,0,0,0,3},
            {1,0,0,0,0,0,0,0,1,4,4,4,4,4,6,0,6,3,3,0,0,0,3,3},
            {2,0,0,0,0,0,0,0,2,2,2,1,2,2,2,6,6,0,0,5,0,5,0,5},
            {2,2,0,0,0,0,0,2,2,2,0,0,0,2,2,0,5,0,5,0,0,0,5,5},
            {2,0,0,0,0,0,0,0,2,0,0,0,0,0,2,5,0,5,0,5,0,5,0,5},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5},
            {2,0,0,0,0,0,0,0,2,0,0,0,0,0,2,5,0,5,0,5,0,5,0,5},
            {2,2,0,0,0,0,0,2,2,2,0,0,0,2,2,0,5,0,5,0,0,0,5,5},
            {2,2,2,2,1,2,2,2,2,2,2,1,2,2,2,5,5,5,5,5,5,5,5,5}
        };

        private readonly Bitmap[] _textures = new Bitmap[8];
        private readonly byte[][] _textureBytes = new byte[8][];

        private readonly Bitmap[] _objects = new Bitmap[3];
        private readonly byte[][] _objectBytes = new byte[3][];

        public Entity[] entities = Array.Empty<Entity>();

        // Texture variables.
        public int TextureHeight = 64;
        public int TextureWidth = 64;

        public Map()
        {
            _textures[0] = getBitmap("\\Resources\\bluestone.png");
            _textures[1] = getBitmap("\\Resources\\colorstone.png");
            _textures[2] = getBitmap("\\Resources\\eagle.png");
            _textures[3] = getBitmap("\\Resources\\greystone.png");
            _textures[4] = getBitmap("\\Resources\\mossy.png");
            _textures[5] = getBitmap("\\Resources\\purplestone.png");
            _textures[6] = getBitmap("\\Resources\\redbrick.png");
            _textures[7] = getBitmap("\\Resources\\wood.png");

            _objects[0] = getBitmap("\\Resources\\barrel.png");
            _objects[1] = getBitmap("\\Resources\\pillar.png");
            _objects[2] = getBitmap("\\Resources\\greenlight.png");

            //Walls
            for(var x = 0; x < _textures.Length; x++)
            {
                _textureBytes[x] = new byte[TextureWidth * TextureHeight * 4];

                var bitmapData = _textures[x].LockBits(new Rectangle(0, 0, TextureWidth, TextureHeight), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                var length = bitmapData.Stride * bitmapData.Height;

                // Copy bitmap to byte[]
                Marshal.Copy(bitmapData.Scan0, _textureBytes[x], 0, length);
                _textures[x].UnlockBits(bitmapData);
            }

            //Entities
            entities = new Entity[]
            {
                new Entity { X = 20.5, Y = 11.5, Texture = 1 },
                new Entity { X = 18.5, Y = 4.5, Texture = 2 },
                new Entity { X = 10.0, Y = 4.5, Texture = 2 },
                new Entity { X = 10.0, Y = 12.5, Texture = 2 },
                new Entity { X = 3.5, Y = 6.5, Texture = 2 },
                new Entity { X = 3.5, Y = 20.5, Texture = 2 },
                new Entity { X = 3.5, Y = 14.5, Texture = 2 },
                new Entity { X = 14.5, Y = 20.5, Texture = 2 },
                new Entity { X = 18.5, Y = 10.5, Texture = 1 },
                new Entity { X = 18.5, Y = 11.5, Texture = 1 },
                new Entity { X = 18.5, Y = 12.5, Texture = 1 },
                new Entity { X = 21.5, Y = 1.5, Texture = 0 },
                new Entity { X = 15.5, Y = 1.5, Texture = 0 },
                new Entity { X = 16.0, Y = 1.8, Texture = 0 },
                new Entity { X = 16.2, Y = 1.2, Texture = 0 },
                new Entity { X = 3.5, Y = 2.5, Texture = 0 },
                new Entity { X = 9.5, Y = 15.5, Texture = 0 },
                new Entity { X = 10.0, Y = 15.1, Texture = 0 },
                new Entity { X = 10.5, Y = 15.8, Texture = 0 }
            };

            for (var x = 0; x < _objects.Length; x++)
            {
                _objectBytes[x] = new byte[TextureWidth * TextureHeight * 4];

                var bitmapData = _objects[x].LockBits(new Rectangle(0, 0, TextureWidth, TextureHeight), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                var length = bitmapData.Stride * bitmapData.Height;

                // Copy bitmap to byte[]
                Marshal.Copy(bitmapData.Scan0, _objectBytes[x], 0, length);
                _objects[x].UnlockBits(bitmapData);
            }
        }

        private Bitmap getBitmap(string filePath)
        {
            Bitmap bmp = new(Image.FromFile(filename: $"{Directory.GetCurrentDirectory()}{filePath}"));
            return bmp;
        }

        internal int GetColorForTexture(int texX, int texY, int texNum, int side)
        {
            int index = (texY * TextureWidth + texX) * 4;
            byte b = _textureBytes[texNum][index];
            byte g = _textureBytes[texNum][index + 1];
            byte r = _textureBytes[texNum][index + 2];
            byte a = _textureBytes[texNum][index + 3];

            //make color darker for y-sides: R, G and B byte
            if (side == 0)
            {
                r = (byte)(r >> 1);
                g = (byte)(g >> 1);
                b = (byte)(b >> 1);
            }

            return (a << 24) | (r << 16) | (g << 8) | b;
        }

        internal int GetColorForObject(int objX, int objY, int objNum)
        {
            int index = (objY * TextureWidth + objX) * 4;
            byte b = _objectBytes[objNum][index];
            byte g = _objectBytes[objNum][index + 1];
            byte r = _objectBytes[objNum][index + 2];
            byte a = _objectBytes[objNum][index + 3];

            return (a << 24) | (r << 16) | (g << 8) | b;
        }
    }
}
