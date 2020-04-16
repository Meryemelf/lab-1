using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ГрафикаCшарп_1_v1
{
    class SharpnessFilter : MatrixFilter
    {
        public SharpnessFilter()
        {
            int sixeX = 3;
            int sixeY = 3;
            kernel = new float[sixeX, sixeY];
            kernel[0, 0] = -1; kernel[0, 1] = -1; kernel[0, 2] = -1;
            kernel[1, 0] = -1; kernel[1, 1] = 9; kernel[1, 2] = -1;
            kernel[2, 0] = -1; kernel[2, 1] = -1; kernel[2, 2] = -1;

        }
        public SharpnessFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }
    }

}
