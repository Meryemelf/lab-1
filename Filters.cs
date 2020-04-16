using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace ГрафикаCшарп_1_v1
{
    class ProcessingTask
    {
        String[] m_filters;
        Bitmap m_image;

        public Bitmap image
        {
            get => m_image;
            set => m_image = value;
        }

        public String[] filters
        {
            get => m_filters;
            set => m_filters = value;
        }

        public ProcessingTask( Bitmap image, String[] filters)
        {
            m_filters = filters;
            m_image = image;
        }

        public Filters createFilter(String name)
        {
            Filters filter;
            switch (name)
            {
                case "GrayScaleFilter":
                    filter = new GrayScaleFilter();
                    break;
                case "DilationFilter":
                    filter = new DilationFilter();
                    break;
                case "ErosionFilter":
                    filter = new ErosionFilter();
                    break;
                default:
                    filter = null;
                    break;
            }
            return filter;

        }


    }

    abstract class Filters
    {
        protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);

        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        protected virtual Bitmap preprocessImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            return sourceImage;
        }
        protected virtual Bitmap preprocessImage(Bitmap sourceImage)
        {
            return sourceImage;
        }

        



        public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {

            Bitmap preprocessedImage = new Bitmap(preprocessImage(sourceImage, worker));
            //return preprocessedImage;
            Bitmap resultImage = new Bitmap(preprocessedImage.Width, preprocessedImage.Height);
            for (int i = 0; i < preprocessedImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / preprocessedImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < preprocessedImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(preprocessedImage, i, j));
                }
            }

            return resultImage;
        }

        public Bitmap processImage(Bitmap sourceImage)
        {

            Bitmap preprocessedImage = new Bitmap(preprocessImage(sourceImage));
            Bitmap resultImage = new Bitmap(preprocessedImage.Width, preprocessedImage.Height);
            for (int i = 0; i < preprocessedImage.Width; i++)
            {
                for (int j = 0; j < preprocessedImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(preprocessedImage, i, j));
                }
            }

            return resultImage;
        }

    }
}
