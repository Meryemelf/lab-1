using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ГрафикаCшарп_1_v1
{
    class MedianFilter : MatrixFilter
    {
        public MedianFilter()
        {
            int sixeX = 3;
            int sixeY = 3;
            kernel = new float[sixeX, sixeY];
            kernel[0, 0] = 1; kernel[0, 1] = 1; kernel[0, 2] = 1;
            kernel[1, 0] = 1; kernel[1, 1] = 1; kernel[1, 2] = 1;
            kernel[2, 0] = 1; kernel[2, 1] = 1; kernel[2, 2] = 1;

        }
        public MedianFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            int count_value = 0;
            int[] y_values = new int[kernel.GetLength(0) * kernel.GetLength(1)];
            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    int Y = (int)(255 * neighborColor.GetBrightness());
                    y_values[count_value++] = Y;
                }
            Array.Sort(y_values);
            int median_Y = Clamp(y_values[count_value / 2], 0, 255);
            return Color.FromArgb(
                             median_Y,
                             median_Y,
                             median_Y
                            );
        }
    }
}
