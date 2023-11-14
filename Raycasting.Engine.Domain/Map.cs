using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Raycasting.Domain
{
    public class Map
    {
        public int[,] currentMap = new int[,]
        {
                {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,7,7,7,7,7,7,7,7},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,7},
                {4,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7},
                {4,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7},
                {4,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,0,0,0,0,0,7},
                {4,0,0,4,0,0,0,0,5,5,5,5,5,5,5,5,5,7,7,0,7,7,7,7,7},
                {4,0,0,5,0,0,0,0,5,0,5,0,5,0,5,0,5,7,0,0,0,7,7,7,1},
                {4,0,0,6,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,0,0,0,8},
                {4,0,0,7,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,7,7,1},
                {4,0,0,8,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,0,0,0,8},
                {4,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,5,7,0,0,0,7,7,7,1},
                {4,0,0,0,0,0,0,0,5,5,5,5,0,5,5,5,5,7,7,7,7,7,7,7,1},
                {6,6,6,6,6,6,6,6,6,6,6,6,0,6,6,6,6,6,6,6,6,6,6,6,6},
                {8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {6,6,6,6,6,6,6,0,6,6,6,6,0,6,6,6,6,6,6,6,6,6,6,6,6},
                {4,4,4,4,4,4,4,0,4,4,4,6,0,6,2,2,2,2,2,2,2,3,3,3,3},
                {4,0,0,0,0,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,0,0,0,2},
                {4,0,0,0,0,0,0,0,0,0,0,0,0,6,2,0,0,5,0,0,2,0,0,0,2},
                {4,0,0,0,0,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,2,0,2,2},
                {4,0,0,6,0,6,0,0,0,0,4,6,0,0,0,0,0,5,0,0,0,0,0,0,2},
                {4,0,0,0,5,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,2,0,2,2},
                {4,0,0,6,0,6,0,0,0,0,4,6,0,6,2,0,0,5,0,0,2,0,0,0,2},
                {4,0,0,0,0,0,0,0,0,0,4,6,0,6,2,0,0,0,0,0,2,0,0,0,2},
                {4,4,4,4,4,4,4,4,4,4,4,1,1,1,2,2,2,2,2,2,3,3,3,3,3}
        };

        private readonly Bitmap[] _textures = new Bitmap[8];
        private readonly byte[][] _bytes = new byte[8][];

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

            for(var x = 0; x < 8; x++)
            {
                _bytes[x] = new byte[TextureWidth * TextureHeight * 4];

                var bitmapData = _textures[x].LockBits(new Rectangle(0, 0, TextureWidth, TextureHeight), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                var length = bitmapData.Stride * bitmapData.Height;

                // Copy bitmap to byte[]
                Marshal.Copy(bitmapData.Scan0, _bytes[x], 0, length);
                _textures[x].UnlockBits(bitmapData);
            }
        }

        private Bitmap getBitmap(string filePath)
        {
            Bitmap bmp = new(Image.FromFile(filename: $"{Directory.GetCurrentDirectory()}{filePath}"));
            return bmp;
        }

        internal int GetColor(int texX, int texY, int texNum)
        {
            int index = (texY * TextureWidth + texX) * 4;
            byte b = _bytes[texNum][index];
            byte g = _bytes[texNum][index + 1];
            byte r = _bytes[texNum][index + 2];
            byte a = _bytes[texNum][index + 3];

            return (a << 24) | (r << 16) | (g << 8) | b;
        }
    }
}
