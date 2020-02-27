using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartControlExtensions
{
    public class SeriesOptions
    {
        static MarkerStyle GetRandomMarker()
        {
            var styles = Enum.GetValues(typeof(MarkerStyle));
            var i = new Random(Guid.NewGuid().GetHashCode()).Next(styles.Length);
            return (MarkerStyle)styles.GetValue(i);
        }

        public static SeriesOptions Histogram
        {
            get
            {
                SeriesOptions s = new SeriesOptions();
                s.xEditable = false;
                s.yEditable = false;
                s.CurveType = SeriesType.HISTOGRAM;

                s.MarkerStyle = MarkerStyle.None;
                s.ChartType = SeriesChartType.Area;
                s.BorderWidth = 1;

                s.BackGradientStyle = GradientStyle.DiagonalRight;
                s.BackSecondaryColor = Color.DarkTurquoise;
                s.ForeColor = Color.DodgerBlue;

                return s;
            }
        }

        public static SeriesOptions HorizontalLine
        {
            get
            {
                SeriesOptions s = new SeriesOptions();
                s.xEditable = false;
                s.yEditable = true;
                s.CurveType = SeriesType.HORIZONTALLEVEL;

                s.MarkerStyle = MarkerStyle.Diamond;
                s.MarkerColor = Color.Red;
                s.ChartType = SeriesChartType.Line;
                s.BorderWidth = 2;
                s.MarkerSize = 12;
                //s.BorderColor = Color.Red;
                s.ForeColor = Color.Red;

                return s;
            }
        }

        public static SeriesOptions VerticalLine
        {
            get
            {
                SeriesOptions s = new SeriesOptions();
                s.xEditable = true;
                s.yEditable = false;
                s.CurveType = SeriesType.VERTICALEVEL;

                s.MarkerStyle = MarkerStyle.Diamond;
                s.MarkerColor = Color.Red;
                s.ChartType = SeriesChartType.Line;
                s.BorderWidth = 2;
                s.MarkerSize = 12;
                //s.BorderColor = Color.Red;
                s.ForeColor = Color.Red;

                return s;
            }
        }

        public static SeriesOptions XY
        {
            get
            {
                SeriesOptions s = new SeriesOptions();
                s.xEditable = true;
                s.yEditable = true;
                s.CurveType = SeriesType.XY;
                s.MarkerSize = 10;
                s.MarkerStyle = GetRandomMarker();
                s.MarkerColor = Color.Black; 
                s.ChartType = SeriesChartType.Point;
                s.BorderWidth = 2;

                return s;
            }
        }

        public GradientStyle BackGradientStyle { get; set; }
        public Color BackSecondaryColor { get; set; }
        public int BorderWidth { get; set; } = 1;
        public SeriesChartType ChartType { get; set; } = SeriesChartType.Line;
        public SeriesType CurveType { get; set; } = SeriesType.XY;

        public Color ForeColor { get; set; }

        [Browsable(true)]
        public int MarkerSize { get; set; } = 5;

        [Browsable(true)]
        public Color MarkerColor { get; set; } 
        public MarkerStyle MarkerStyle { get; set; } = MarkerStyle.None;
        public bool xEditable { get; set; } = false;
        public bool yEditable { get; set; } = false;
        public void CopyToSeries(Series s)
        {
            s.MarkerStyle = MarkerStyle;
            s.MarkerSize = MarkerSize;
            s.MarkerColor = MarkerColor;
            s.ChartType = ChartType;
            s.BorderWidth = BorderWidth;

            s.BackGradientStyle = BackGradientStyle;
            s.BackSecondaryColor = BackSecondaryColor;
            s.Color = ForeColor;
            s.BorderColor = s.Color;

            s["XEditable"] = xEditable == true ? "True" : "False";
            s["YEditable"] = yEditable == true ? "True" : "False";

            s.Tag = this;
        }
    };
}
