using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ГрафикаCшарп_1_v1
{
    class BlurFilter : MatrixFilter
    {
        public BlurFilter()
        {
            int sixeX = 3;
            int sixeY = 3;
            kernel = new float[sixeX, sixeY];
            for (int i = 0; i < sixeX; i++)
                for (int j = 0; j < sixeY; j++)
                    kernel[i, j] = 1.0f / (float)(sixeX * sixeY);
        }

       
    }
}
