using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ГрафикаCшарп_1_v1
{
    class DilationFilter : MatrixFilter
    {
        public DilationFilter()
        {
            int sixeX = 3;
            int sixeY = 3;
            kernel = new float[sixeX, sixeY];
            kernel[0, 0] = 0; kernel[0, 1] = 1; kernel[0, 2] = 0;
            kernel[1, 0] = 1; kernel[1, 1] = 1; kernel[1, 2] = 1;
            kernel[2, 0] = 0; kernel[2, 1] = 1; kernel[2, 2] = 0;

        }

        public DilationFilter(float[,] kernel) 
        {
            this.kernel = kernel;
        }
        
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            int max = 0, Y;

            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    Y = (int)(255 * neighborColor.GetBrightness());
                    if (((int)kernel[k+radiusX, l+radiusY] != 0) && (Y > max))
                    {
                        max = Y;
                    }

                }
            return Color.FromArgb(
                Clamp(max, 0, 255),
                Clamp(max, 0, 255),
                Clamp(max, 0, 255)
                );
        }


    }
}
