using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace ГрафикаCшарп_1_v1
{
    class Transfer : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color resultColor = Color.Black;
            if (x + 50 < sourceImage.Width)
            {
                resultColor = sourceImage.GetPixel(x + 50, y);
            }
            return resultColor;
        }
    }
}
