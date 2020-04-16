using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace ГрафикаCшарп_1_v1
{
    class LinearCorrectionFilter : Filters
    {
        int m_min_Y = 255, m_max_Y = 0;

        protected override Bitmap preprocessImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Color sourceColor;
            int Y;
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    sourceColor = sourceImage.GetPixel(i, j);
                    Y = (int)(255 * sourceColor.GetBrightness());
                    m_min_Y = Math.Min(m_min_Y, Y);
                    m_max_Y = Math.Max(m_max_Y, Y);
                }
            }
            return sourceImage;
        }

        private int correct(int value)
        {
            return (value - m_min_Y) *(255 - 0) / (m_max_Y - m_min_Y);
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(correct(sourceColor.R),
                                               correct(sourceColor.G),
                                               correct(sourceColor.B));
            return resultColor;
        }
    }
}
