using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;


namespace ГрафикаCшарп_1_v1
{
    class GlassFilter : Filters
    {

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
                Color resultColor;
                Random temp = new Random();

            //rand = temp.Next(1);
            int newx = (int)(x + (temp.Next(1) - 0.5) * 10);
            int newy = (int)(y + (temp.Next(1) - 0.5) * 10);
            if (newx >= 0 && newx < sourceImage.Width && newy >= 0 && newy < sourceImage.Height)
                resultColor = sourceImage.GetPixel(newx, newy);
            else resultColor = sourceImage.GetPixel(x, y);


            return resultColor;
        }
    }
}
