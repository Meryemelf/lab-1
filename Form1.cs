using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ГрафикаCшарп_1_v1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap image;

      private void UpdateControls()
      {

      }

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files|*.png;*.jpg;*bmp|All files(*.*)|*.*:";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InverFilter filter = new InverFilter();
            backgroundWorker1.RunWorkerAsync(filter);
           // Bitmap resultImage = filter.processImage(image);
           // pictureBox1.Image = resultImage;
           // pictureBox1.Refresh();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage;
            if (e.Argument is ProcessingTask)
            {
                ProcessingTask task = (ProcessingTask)e.Argument;
                newImage = task.image;
                for(int i=0; i < task.filters.Count(); i++)
                {
                    Filters filter = task.createFilter(task.filters[i]);
                    newImage = filter.processImage(newImage, backgroundWorker1);
                }
            } else
            {
                newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            }

            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрГауссаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GreyWorldFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void линейнаяКоррекцияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new LinearCorrectionFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void оттенкиСерогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new EmbossingFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void переносToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Transfer();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GlassFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void расширениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new DilationFilter(GetKernel());
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private float[,] GetKernel()
        { 
            float[,] kernel = null;
            if( rb_circle3x3.Checked)
            {
                kernel = new float[3, 3];
                kernel[0, 0] = 0; kernel[0, 1] = 1; kernel[0, 2] = 0;
                kernel[1, 0] = 1; kernel[1, 1] = 1; kernel[1, 2] = 1;
                kernel[2, 0] = 0; kernel[2, 1] = 1; kernel[2, 2] = 0;
            } else if (rb_circle5x5.Checked)
            {
                kernel = new float[5, 5];
                kernel[0, 0] = 0; kernel[0, 1] = 0; kernel[0, 2] = 1; kernel[0, 3] = 0; kernel[0, 4] = 0;
                kernel[1, 0] = 0; kernel[1, 1] = 1; kernel[1, 2] = 1; kernel[1, 3] = 1; kernel[1, 4] = 0;
                kernel[2, 0] = 1; kernel[2, 1] = 1; kernel[2, 2] = 1; kernel[2, 3] = 1; kernel[2, 4] = 1;
                kernel[3, 0] = 0; kernel[3, 1] = 1; kernel[3, 2] = 1; kernel[3, 3] = 1; kernel[3, 4] = 0;
                kernel[4, 0] = 0; kernel[4, 1] = 0; kernel[4, 2] = 1; kernel[4, 3] = 0; kernel[4, 4] = 0;

            } else if (rb_square3x3.Checked)
            {
                kernel = new float[3, 3];
                kernel[0, 0] = 1; kernel[0, 1] = 1; kernel[0, 2] = 1;
                kernel[1, 0] = 1; kernel[1, 1] = 1; kernel[1, 2] = 1;
                kernel[2, 0] = 1; kernel[2, 1] = 1; kernel[2, 2] = 1;
            }
            else if (rb_square5x5.Checked)
            {
                kernel = new float[5, 5];
                kernel[0, 0] = 1; kernel[0, 1] = 1; kernel[0, 2] = 1; kernel[0, 3] = 1; kernel[0, 4] = 1;
                kernel[1, 0] = 1; kernel[1, 1] = 1; kernel[1, 2] = 1; kernel[1, 3] = 1; kernel[1, 4] = 1;
                kernel[2, 0] = 1; kernel[2, 1] = 1; kernel[2, 2] = 1; kernel[2, 3] = 1; kernel[2, 4] = 1;
                kernel[3, 0] = 1; kernel[3, 1] = 1; kernel[3, 2] = 1; kernel[3, 3] = 1; kernel[3, 4] = 1;
                kernel[4, 0] = 1; kernel[4, 1] = 1; kernel[4, 2] = 1; kernel[4, 3] = 1; kernel[4, 4] = 1;
            }
            return kernel;
        }

        private void сужениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ErosionFilter(GetKernel());
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void открытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String[] filters = new String[2];
            filters[1] = "DilationFilter";
            filters[0] = "ErosionFilter";
            ProcessingTask task = new ProcessingTask(image, filters);
            backgroundWorker1.RunWorkerAsync(task);
        }

        private void закрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String[] filters = new String[2];
            filters[0] = "DilationFilter";
            filters[1] = "ErosionFilter";
            ProcessingTask task = new ProcessingTask(image, filters);
            backgroundWorker1.RunWorkerAsync(task);
        }

        private void blackHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlackHatFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MedianFilter(GetKernel());
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void увеличениеЯркостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BrightnessPlusFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
    }
}
