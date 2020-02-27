using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartControlExtensions
{
    public static class ChartExtensions
    {
        public static void Clear(this Chart chart1) => chart1.Series.Clear();

        //public static void SetCursorForInteractionMode(this Chart chart1, ChartInteractionMode InteractionMode)
        //{
        //    switch (InteractionMode)
        //    {
        //        case ChartInteractionMode.Zoom:
        //            {
        //                chart1.Cursor = Cursors.Cross;
        //                break;
        //            }
        //        case ChartInteractionMode.Edit:
        //            {
        //                chart1.Cursor = Cursors.Help;
        //                break;
        //            }
        //        case ChartInteractionMode.Pan:
        //            {
        //                chart1.Cursor = Cursors.SizeAll;
        //                break;
        //            }
        //        case ChartInteractionMode.View:
        //            {
        //                chart1.Cursor = Cursors.Arrow;
        //                break;
        //            }
        //        default:
        //            {
        //                chart1.Cursor = Cursors.UpArrow;
        //                break;
        //            }
        //    }
        //}

        public static Series CreateReplaceSeries(this Chart chart1, string name, SeriesOptions inputOptions = null, List<PointF> pts = null)//, IEnumerable<float> z = null)
        {
            if (chart1 == null) return null;
            SeriesOptions options = inputOptions ?? new SeriesOptions();
            Series h = chart1.Series.FindByName(name) ?? chart1.Series.Add(name);

            h.Enabled = false;
            options.CopyToSeries(h);
            chart1.SetSeriesData(h, pts);//, z);
            return h;
        }
 
        public static void SetSeriesData(this Chart chart1, Series s, List<PointF> pts)//, IEnumerable<float> hue = null)
        {
            s.Points.Clear();
            foreach (PointF p in pts) s.Points.AddXY(p.X, p.Y);
        }

        public static Series AddXYSeries(this Chart chart1, string name, List<PointF> pts, SeriesOptions options = null)//, IEnumerable<float> z = null)
        => chart1.CreateReplaceSeries(name, options ?? SeriesOptions.XY, pts);
   
        public static void GetSeriesLimits(this Chart chart1, out float[] maxRange, out float[] minRange, AxisType yAxisType)
        {
            maxRange = new float[2] { 1.0f, 1.0f };
            minRange = new float[2] { 0.0f, 0.0f };

            if (chart1.Series.Count() < 1) return;

            //get all the series for which the axis type is the requested, either primary or secondary
            //and that have at least one data point (not empty series) 
            var series = chart1.Series.Where(t => ( t.Enabled == true )  && (t.Points.Count() > 0) && (t.YAxisType == yAxisType)).ToArray();//no lazzy eval

            //if there are none, return; 
            if (series.Count() < 1) return;

            //if there are series, we need to get the limits. Here we only initialize the initial values for the limits.
            maxRange[0] = minRange[0] = (float)series.ElementAt(0).Points[0].XValue;
            maxRange[1] = minRange[1] = (float)series.ElementAt(0).Points[0].YValues[0];

            //index 0 was already done. Start in 1.
            for (int n = 0; n < series.Count(); n++)
            {
                Series s = series.ElementAt(n);
                //min/max x values
                var vals = s.Points.Select(t => t.XValue).ToArray();
                minRange[0] = (float)Math.Min(minRange[0], vals.Min());
                maxRange[0] = (float)Math.Max(maxRange[0], vals.Max());

                //the yvalues
                vals = s.Points.Select(t => t.YValues.ElementAt(0)).ToArray();   //take Element at zero because it is (X, [Y, Yerror, YError, etc...]) 
                maxRange[1] = (float)Math.Max(maxRange[1], vals.Max());
                minRange[1] = (float)Math.Min(minRange[1], vals.Min());
            }
        }

        public static void AdjustAxes(this Chart chart1)
        {
            //returns [ maxX, maxY] and [minX, minY] for the series displayed in the primary y axis
            chart1.GetSeriesLimits(out float[] maxRangePrimaryAxis, out float[] minRangePrimaryAxis, AxisType.Primary);

            //returns [ maxX, maxY] and [minX, minY] for the series displayed in the secondary y axis
            chart1.GetSeriesLimits(out float[] maxRangeSecondaryAxis, out float[] minRangeSecondaryAxis, AxisType.Secondary);

            //adjust x axis
            float[] xRange = { Math.Min(minRangePrimaryAxis[0], minRangeSecondaryAxis[0]), Math.Max(maxRangePrimaryAxis[0], maxRangeSecondaryAxis[0]) };
            chart1.SetAxisInterval(xRange[0] * 0.99, xRange[1] * 1.01, chart1.ChartAreas[0].AxisX);

            //adjust primary y axis 
            chart1.SetAxisInterval(minRangePrimaryAxis[1] * 0.99, maxRangePrimaryAxis[1] * 1.01, chart1.ChartAreas[0].AxisY);

            //adjust secondary y axis 
            chart1.SetAxisInterval(minRangeSecondaryAxis[1] * 0.99, maxRangeSecondaryAxis[1] * 1.01, chart1.ChartAreas[0].AxisY2);

        }

        public static void SetAxisInterval(this Chart chart1, double min, double max, Axis axis)
        {
            double unit = (Math.Ceiling(max) - Math.Floor(min)) / 10;
            double grade = Math.Floor(Math.Log10(unit));
            double sunit = unit / (Math.Pow(10, grade));
            double interval = 1.0;

            if (sunit < Math.Sqrt(2.0)) interval = Math.Pow(10.0, grade);      //(10 * *grade) * 1;
            else if (sunit < Math.Sqrt(10.0)) interval = 2.0 * Math.Pow(10.0, grade);
            else if (sunit < Math.Sqrt(50.0)) interval = 5.0 * Math.Pow(10.0, grade);
            else interval = 10.0 * Math.Pow(10.0, grade);
            double maxscale = Math.Ceiling(max / interval) * (interval);
            double minscale = (int)(min / interval) * (interval);//     interval;

            while (maxscale - interval > max) maxscale -= interval;

            while (minscale - 1.0 * interval > min) minscale -= interval;

            axis.Maximum = maxscale;//     0.5*interval;
            axis.Minimum = minscale;// -0.5*interval;
            axis.Interval = interval;
            axis.LabelStyle.Format = (interval < 1e3 ? "{F2}" : "{E1}");
            if (interval < 1e3) axis.LabelStyle.Format = "{F2}";
        }

        public static Series AddHistogram(this Chart chart1, string name, float[] v1, SeriesOptions ioptions = null, int bins = 20)
        {
            return CreateReplaceSeries(chart1, name, SeriesOptions.Histogram, GetHistogram(v1, bins));
        }

        private static  List<PointF> GetHistogram(float[] values, int nBins = 100)
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



    }

}
