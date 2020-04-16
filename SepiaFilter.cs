using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ГрафикаCшарп_1_v1
{
    class SepiaFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int k = 8;
            int Intensity = (int)(sourceColor.R * 0.299 + sourceColor.G * 0.587 + sourceColor.B * 0.114);
            //Intensity = Clamp(Intensity, 0, 255);
            Color resultColor = Color.FromArgb(Clamp(Intensity + 2 * k, 0, 255),
                                               Clamp((int)(Intensity + 0.5 * k), 0, 255),
                                               Clamp(Intensity - 1 * k, 0, 255)
                );
            return resultColor;
        }  
    }
}
