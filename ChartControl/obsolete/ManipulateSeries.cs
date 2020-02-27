using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace ChartControlExtensions
{
    public partial class InteractiveChart//: UserControl
    {

        public void Clear() => chart1.Series.Clear();

        public Series GetOrCreateSeries(string name, SeriesOptions options = null, List<PointF> pts = null)
        {
            Series h = chart1.Series.FindByName(name) ?? chart1.Series.Add(name);
            h.Enabled = false;
            (options ?? new SeriesOptions()).CopyToSeries(h);
            SetSeriesData(h, pts);
            return h;
        }

        public void SetSeriesData(Series s, List<PointF> pts)
        {
            s.Points.Clear();
            foreach (PointF p in pts) s.Points.AddXY(p.X, p.Y);
        }
        public virtual Series AddXYSeries(string name, List<PointF> pts, SeriesOptions options = null) => GetOrCreateSeries(name, options ?? SeriesOptions.XY, pts);


        #region series

        public virtual Series AddVerticalLevel(string name, float value, float minY = 0f, float maxY = 1.0f, SeriesOptions options = null)
        {
            List<PointF> pts = new List<PointF>()
  {
 new PointF(value, minY),
 new PointF(value, maxY)
  };

            Series s = GetOrCreateSeries(name, options ?? SeriesOptions.VerticalLine, pts);
            s.IsVisibleInLegend = false;
            return s;
        }

        public virtual Series AddHorizontalLevel(string name, float value, float minX = 0.05f, float maxX = 1.0f, SeriesOptions options = null)
        {
            List<PointF> pts = new List<PointF>()
  {
 new PointF(minX,value),
 new PointF(maxX,value)
  };
            Series s = GetOrCreateSeries(name, options ?? SeriesOptions.HorizontalLine, pts);
            s.IsVisibleInLegend = false;
            return s;
        }

      


        #endregion series



        #region sample data/view

        virtual public void CreateSampleData()
        {
            IEnumerable<float> GetGaussianRandom(int N, float mean, float stdDev)
            {
                float[] v = new float[N];
                Random randx = new Random();// 112334);
                for (int n = 0; n < v.Count(); n++)
                {
                    double u1 = 1.0 - randx.NextDouble(); //uniform(0,1] random doubles
                    double u2 = 1.0 - randx.NextDouble();
                    double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                    double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

                    v[n] = (float)(randNormal);
                }

                return v;
            }

            List<float> v1 = new List<float>();
            v1.AddRange(GetGaussianRandom(200, 3.31e3f, 0.12e3f));
            v1.AddRange(GetGaussianRandom(200, 2.11e3f, 0.28e3f));

            //Series h = AddHistogram("Histogram", v1.ToArray(), bins: 20);// pts);

            Series s = AddVerticalLevel("Line", 2.4e3f);

            Random rand = new Random();
            float maxX = v1.Max(), minX = v1.Min();
            float delta = (maxX - minX) / 20;
            float xo = minX;
            List<PointF> pts = new List<PointF>();
            for (int n = 0; n < 20; n++)
            {
                pts.Add(new PointF(xo, 0.01f * rand.Next(5, 95)));
                xo += delta;
            }
            Series w = AddXYSeries("Scattered", pts);

            Series z = AddHorizontalLevel("Horizontal", 0.5f, 4000f);// pts.Select(t=>(float)(t.Y)).Max()*0.5f);

            //h.Enabled = true;
            s.Enabled = true;
            w.Enabled = true;
            z.Enabled = true;

            //scondary axis
            pts.Clear();
            for (int n = 0; n < 30; n++)
            {
                pts.Add(new PointF(minX, 0.1f * rand.Next(5, 95)));
                minX += delta;
            }


            pts.Clear();
            pts.Add(new PointF(-1.5f, -1.8f));
            pts.Add(new PointF(2.5f, 2.8f));
            Series secondary = AddXYSeries("ScatteredSecondary", pts);
            secondary.YAxisType = AxisType.Secondary;
            secondary.Enabled = true;



            AdjustAxes();
        }

        protected List<PointF> GetHistogram(float[] values, int nBins = 100)
        {
            float max = values.Max() * 1.05f;
            float min = values.Min() * 0.95f;
            float delta = (max - min) / nBins;
            float[] count = Enumerable.Repeat(0f, nBins).ToArray();

            foreach (float v in values)
            {
                int bin = (int)((v - min) / delta);
                count[bin] += 1;
            }
            float invMaxCount = 1.0f / count.Max();

            List<PointF> pts = new List<PointF>();
            for (int n = 0; n < count.Count(); n++)
            {
                float x = min + (n + 0.5f) * delta;
                pts.Add(new PointF(x, count[n] * invMaxCount));
            }

            //remove zeros at both ends to we dont have more than 2 continuous columns with zero count.
            //lets start at the begining
            if (pts[0].Y < float.Epsilon)
            {
                while (pts[1].Y < float.Epsilon)
                    pts.RemoveAt(1);
            }
            int No = pts.Count - 1;
            if (pts[No].Y < float.Epsilon)
            {
                while (pts[No - 1].Y < float.Epsilon)
                { pts.RemoveAt(No - 1); No -= 1; }
            }

            return pts;
        }



        #endregion sample data/view

    }


}