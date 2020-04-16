using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ГрафикаCшарп_1_v1
{
    class BlackHatFilter : Filters
    {
        Bitmap m_closed_image = null;


        protected override Bitmap preprocessImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Filters filter;
            filter = new DilationFilter();
            m_closed_image = new Bitmap(filter.processImage(sourceImage));
            filter = new ErosionFilter();
            m_closed_image = new Bitmap(filter.processImage(sourceImage));
            return sourceImage;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color closedImageColor = Color.Black;
            Color sourceImageColor = sourceImage.GetPixel(x, y);

            if (m_closed_image != null)
                closedImageColor = m_closed_image.GetPixel(x, y);
            int source_Y = (int)(255 * sourceImageColor.GetBrightness());
            int closed_Y = (int)(255 * closedImageColor.GetBrightness());
            return Color.FromArgb(
                             Clamp(source_Y - closed_Y, 0, 255),
                             Clamp(source_Y - closed_Y, 0, 255),
                             Clamp(source_Y - closed_Y, 0, 255)
                            );
        }
    }
}
