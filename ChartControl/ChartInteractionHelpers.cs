using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartControlExtensions
{
    public static class ChartInteractionHelpers
    {
       
        public static void CreateExampleChart( Chart chart1)
        {
            if (chart1 == null) return;

            IEnumerable<float> GetGaussianRandom(int N, float mean, float stdDev)
            {
                float[] v = new float[N];
                Random randx = new Random();// 112334);
                for (int n = 0; n < v.Count(); n++)
                {
                    double u1 = 1.0 - randx.NextDouble();
                    double u2 = 1.0 - randx.NextDouble();
                    double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                    double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

                    v[n] = (float)(randNormal);
                }

                return v;
            }
            //two distributions
            List<float> v1 = new List<float>();
            v1.AddRange(GetGaussianRandom(200, 3.31e3f, 0.12e3f));
            v1.AddRange(GetGaussianRandom(200, 2.11e3f, 0.28e3f));

            //one histogram 
            int nbins = 30;
            Series h = chart1.AddHistogram("Histogram", v1.ToArray(), SeriesOptions.Histogram, nbins);
            h.Enabled = true;
            h.YAxisType = AxisType.Secondary;


            //one scattered
            Random rand = new Random( DateTime.Now.Second);
            float minX = v1.Min() - 0.01f * Math.Abs(v1.Min()), maxX = v1.Max() * 1.001f, delta = (maxX - minX) / nbins;
            List<PointF> pts = new List<PointF>();
            for (int n = 0; n < nbins; n++, minX += delta)
                pts.Add(new PointF(minX, (-50.0f + 1.3f * minX) * (1.0f + 0.07f * rand.Next(-1, 1))));// 0.01f * rand.Next(-55, 95)));

            Color[] colors = { Color.Red, Color.Black, Color.Blue, Color.Yellow, Color.Orange, Color.Gray, Color.Green};
            SeriesOptions options = SeriesOptions.XY;
            options.yEditable = false;
            options.MarkerColor =  colors[  rand.Next( 0, colors.Length-1)];
            chart1.AddXYSeries("Scattered 1", pts, options).Enabled = true;


            //one scattered
            pts = new List<PointF>();
            for (int n = 0; n < 30; n++, minX += delta)
            {
                float x = minX;
                float y = (-50.0f + 1.3f * minX) * (1.0f + 0.127f * rand.Next(-1, 1));
                pts.Add(new PointF(minX, y));
            }
            options.yEditable = true;
            options.MarkerColor = colors[rand.Next(0, colors.Length - 1)];
            chart1.AddXYSeries("Scattered 2", pts, options).Enabled = true;
     
            chart1.AdjustAxes();

        }


    }
}
