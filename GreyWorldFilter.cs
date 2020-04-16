using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace ГрафикаCшарп_1_v1
{
    class GreyWorldFilter : Filters
    {
        int m_avg=0, m_R=0, m_G=0, m_B=0;

        protected override Bitmap preprocessImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(i, j);
                    m_R += sourceColor.R;
                    m_G += sourceColor.G;
                    m_B += sourceColor.B;
                }
            }
            int pixelsCount = sourceImage.Width * sourceImage.Height;
            m_R /= pixelsCount;
            m_G /= pixelsCount;
            m_B /= pixelsCount;
            m_avg = (m_R + m_G + m_B) / 3;
            return sourceImage;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            // TODO: деление на ноль на черной картинке
            Color resultColor = Color.FromArgb(Clamp(sourceColor.R * m_avg / m_R, 0 , 255),
                                               Clamp(sourceColor.G * m_avg / m_G, 0, 255),
                                               Clamp(sourceColor.B * m_avg / m_B, 0, 255));
            return resultColor;
        }
    }
}
