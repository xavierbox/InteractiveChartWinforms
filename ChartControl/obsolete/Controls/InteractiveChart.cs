using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

/*
        protected enum LASTACTION { POINTSELECTED, POINTDRAGGED, NONE, ENTER, LEAVE };
               protected LASTACTION lastAction;
  private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            return;
            if (InteractionMode == InteractionModes.Pan)
            {
                try
                {
                    if (e.Delta < 0)
                    {
                        chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                        chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                    }

                    if (e.Delta > 0)
                    {
                        double xMin = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                        double xMax = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                        double yMin = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                        double yMax = chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                        double intervalX = (xMax - xMin);
                        double intervalY = (yMax - yMin);

                        double dx = (0.25 / intervalX) * (chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - 0.5 * (xMax      xMin));
                        double dy = (0.25 / intervalY) * (chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - 0.5 * (yMax      yMin));

                        xMin = xMin      intervalX * 0.01;//, xMin);
                        xMax = xMax - intervalX * 0.01;//, xMax);
                        yMin = yMin      intervalY * 0.01;//, yMin );
                        yMax = yMax - intervalY * 0.01;//, yMax );

                        //NOW LETS DISPLACE THE ORIGIN
                        xMin     = dx;
                        xMax     = dx;

                        yMin     = dy;
                        yMax     = dy;

                        chart1.ChartAreas[0].AxisX.ScaleView.Zoom(xMin, xMax);
                        chart1.ChartAreas[0].AxisY.ScaleView.Zoom(yMin, yMax);
                    }
                }
                catch { }

                //System.Console.WriteLine( "Enetering ehre " );
                //System.Console.WriteLine( "Delta = "      e.Delta );

                //Axis x = this.chart( ).ChartAreas[ 0 ].AxisX;
                //double xmax = x.ScaleView.ViewMaximum;
                //double xmin = x.ScaleView.ViewMinimum;
                //double interval = xmax - xmin;
                //double x1=0, y1=0, k = e.Delta/Math.Abs(e.Delta);
                //pixelPositionToValue( e.X,e.Y,ref x1,ref y1);

                //double newMinX = x1 - k*0.9 * (interval);
                //double newMaxX = x1      k*0.9 * (interval);
                //x.Maximum = newMaxX;
                //x.Minimum = newMinX;

                //Axis y = this.chart( ).ChartAreas[ 0 ].AxisY;
                //double ymax = y.ScaleView.ViewMaximum;
                //double ymin = y.ScaleView.ViewMinimum;
                //interval = ymax - ymin;
                //double newMinY = Math.Min( ymin, y1 - 0.9 * (interval));
                //double newMaxY = Math.Min( ymax, y1      0.9 * (interval));
                //y.Maximum = newMaxY;
                //y.Minimum = newMinY;
                //roundAxes( );
            }
        }

   */

namespace ChartControlExtensions
{
  public delegate void SeriesChangedHandler(object s, SeriesChangedArgs e);

  public enum InteractionModes { Zoom = 0, Edit = 1, Pan = 2, None = 3, Select = 4 };

  public enum SeriesType { XY, VERTICALEVEL, HORIZONTALLEVEL, HISTOGRAM };

  public partial class InteractiveChart : UserControl
  {

    public event SeriesChangedHandler SeriesChanged;

    protected Color lastMarkerBorderColor = Color.Black;

    protected DataPoint selectedDataPoint;

    protected Series selectedSeries;

    protected Point startPoint, endPoint;

    protected Rectangle zoomRectangle;

    public InteractiveChart()
    {
      InitializeComponent();

      startPoint = new Point(1, 1);
      endPoint = startPoint;
      selectedDataPoint = null;
      selectedSeries = null;

      IndicatorVisible = true;
      //chart1.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
      //chart1.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
      Clear();
      CreateEvents();
    }


    public InteractionModes InteractionMode { get; set; } = InteractionModes.None;

    public int nSeries => chart1.Series.Count;


    #region view

    public bool IndicatorVisible
    {
      get => indicator.Visible;
      set => indicator.Visible = value;
    }

    const int MarkerSelectedBorderExtra = 2;

    public Color MarkerSelectedColor { get; } = Color.Orange;


    //we could indeed define GetSeriesLimits inside AdjustAxis snce the later is the only caller. However, this method may be 
    //useful for some calculations so it was left outside and public.
    public void GetSeriesLimits(out float[] maxRange, out float[] minRange, AxisType yAxisType)
    {
      maxRange = new float[2] { 1.0f, 1.0f };
      minRange = new float[2] { 0.0f, 0.0f };

      if (nSeries < 1) return;

      //get all the series for which the axis type is the requested, either primary or secondary
      //and that have at least one data point (not empty series) 
      var series = chart1.Series.Where(t => (t.Points.Count() > 0) && (t.YAxisType == yAxisType)).ToArray();//no lazzy eval

      //if there are none, return; 
      if (series.Count() < 1) return;

      //if there are series, we need to get the limits. Here we only initialize the initial values for the limits.
      maxRange[0] = minRange[0] = (float)series.ElementAt(0).Points[0].XValue;
      maxRange[1] = minRange[1] = (float)series.ElementAt(0).Points[0].YValues[0];

      //index 0 was already done. Start in 1.
      for( int n=0; n < series.Count();n++        )
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

    public void AdjustAxes()
    {
      //returns [ maxX, maxY] and [minX, minY] for the series displayed in the primary y axis
      GetSeriesLimits(out float[] maxRangePrimaryAxis, out float[] minRangePrimaryAxis, AxisType.Primary);

      //returns [ maxX, maxY] and [minX, minY] for the series displayed in the secondary y axis
      GetSeriesLimits(out float[] maxRangeSecondaryAxis, out float[] minRangeSecondaryAxis, AxisType.Secondary);

      Action<double, double, Axis> adjustAxisAction = ( min, max, axis )=>
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

          while (minscale - 0.0 * interval > min) minscale -= interval;

          axis.Maximum = maxscale;
          axis.Minimum = minscale;
          axis.Interval = interval;
          axis.LabelStyle.Format = (interval < 1e3 ? "{F2}" : "{E1}");
          if (interval < 1e3) axis.LabelStyle.Format = "{F2}";
        };

      //adjust x axis
      float[] xRange = { Math.Min(minRangePrimaryAxis[0], minRangeSecondaryAxis[0]), Math.Max(maxRangePrimaryAxis[0], maxRangeSecondaryAxis[0]) };
      adjustAxisAction( xRange[0]*0.99, xRange[1]*1.01, chart1.ChartAreas[0].AxisX);

      //adjust primary y axis 
      adjustAxisAction(minRangePrimaryAxis[1] * 0.99, maxRangePrimaryAxis[1] * 1.01, chart1.ChartAreas[0].AxisY);

      //adjust secondary y axis 
      adjustAxisAction(minRangeSecondaryAxis[1] * 0.99, maxRangeSecondaryAxis[1] * 1.01, chart1.ChartAreas[0].AxisY2);

    }

    public void AdjustAxesOLD()
    {
      GetSeriesLimits(out float[] maxRange, out float[] minRange, AxisType.Primary);

      for (int d = 0; d < 2; d++        )
      {
        double min = 0.999 * minRange[d], max = 1.001 * maxRange[d], unit = (Math.Ceiling(max) - Math.Floor(min)) / 10;
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

        while (minscale - 0.0 * interval > min) minscale -= interval;

        Axis axis = d == 0 ? chart1.ChartAreas[0].AxisX : chart1.ChartAreas[0].AxisY;

        axis.Maximum = maxscale;
        axis.Minimum = minscale;
        axis.Interval = interval;
        axis.LabelStyle.Format = (interval < 1e3 ? "{F2}" : "{E1}");
        if (interval < 1e3) axis.LabelStyle.Format = "{F2}";
      }
    }



    public void GetSeriesLimitsOLD(out float[] maxRange, out float[] minRange, AxisType yAxisType = AxisType.Primary)
    {
      maxRange = new float[2] { 1.0f, 1.0f };
      minRange = new float[2] { 0.0f, 0.0f };

      if (nSeries < 1) return;

      //get all the series for which the axis type is the requested, either primary or secondary
      //and that have at least one data point (not empty series) 
      var series = chart1.Series.Where(t => (t.Points.Count() > 0) && (t.YAxisType == yAxisType));

      //if there are none, return; 
      if (series.Count() < 1) return;

      //if there are series, we need to get the limits. Here we only initialize the values.
      maxRange[0] = (float)series.ElementAt(0).Points[0].XValue;
      maxRange[1] = (float)series.ElementAt(0).Points[0].YValues[0];
      minRange[0] = (float)maxRange[0];
      minRange[1] = (float)maxRange[1];





      //if (chart1.Series.Any(t => (t.Points.Count() > 0) && (t.YAxisType == yAxisType)))
      //{
      //  var s1 = chart1.Series.First(t => t.Points.Count() > 0);
      //  maxRange[0] = (float)s1.Points[0].XValue;
      //  maxRange[1] = (float)s1.Points[0].YValues[0];
      //  minRange[0] = (float)maxRange[0];
      //  minRange[1] = (float)maxRange[1];
      //}

      foreach (Series s in series)//chart1.Series)
      {
        //if ((s.Points.Count() > 1) && (s.YAxisType == yAxisType))
        //{

        //min/max x values
        var vals = s.Points.Select(t => t.XValue);
        minRange[0] = (float)Math.Min(minRange[0], vals.Min());
        maxRange[0] = (float)Math.Max(maxRange[0], vals.Max());

        //the yvalues
        vals = s.Points.Select(t => t.YValues.ElementAt(0));
        maxRange[1] = (float)Math.Max(maxRange[1], vals.Max());
        minRange[1] = (float)Math.Min(minRange[1], vals.Min());



        //f//oreach (var p in s.Points)
        //{
        //  maxRange[1] = (float)Math.Max(maxRange[1], p.YValues[0]);
        //  minRange[1] = (float)Math.Min(minRange[1], p.YValues[0]);
        //};
        //}
      }
    }
    #endregion view

    #region events

    public Chart Chart
    {
      get { return chart1; }
    }

    protected bool ZoomStarted { get; set; } = false;

    protected void chart1_MouseDown(object sender, MouseEventArgs e)
    {
      startPoint = (new Point(e.X, e.Y));
      endPoint = startPoint;
      selectedDataPoint = null;
      selectedSeries = null;

      if ((nSeries >= 1) && (e.Button == MouseButtons.Left))
      {
        processLeftMouseDown(e);
      }
    }



    protected void chart1_MouseEnter(object sender, EventArgs e)
    {
      if (!chart1.Focused) chart1.Focus();
      IndicatorVisible = true;
    }

    protected void chart1_MouseLeave(object sender, EventArgs e)
    {
      if (chart1.Focused) chart1.Parent.Focus();

      selectedDataPoint = null;
      selectedSeries = null;
      IndicatorVisible = false;
    }

    protected void chart1_MouseMove(object sender, MouseEventArgs e)
    {
      if (nSeries < 0) return;

      try
      {
        switch (InteractionMode)
        {
          case InteractionModes.Edit:
            {
              MoveInEditMode(e);
              break;
            }
          case InteractionModes.Zoom:
            {
              MoveInZoomMode(e);
              break;
            }

          default:
            break;
        }
      }

      catch { return; }
    }

    protected void chart1_MouseUp(object sender, MouseEventArgs e)
    {
      endPoint = (new Point(e.X, e.Y));
      //ZoomStarted = false;
      if (nSeries >= 1)
      {
        if (e.Button == MouseButtons.Left)
          processLeftMouseUp(e);

        if (e.Button == MouseButtons.Right)
          processRightMouseUp(e);
      }
      startPoint = endPoint;
    }


    protected void CreateEvents()
    {
      this.chart1.MouseEnter     += new System.EventHandler(this.chart1_MouseEnter);
      this.chart1.MouseLeave     += new System.EventHandler(this.chart1_MouseLeave);
      this.chart1.MouseDown     += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDown);
      this.chart1.MouseUp     += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseUp);
      this.chart1.MouseMove     += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
    }

    protected void DisplayPointLabel(double x, double y)
    {
      if (selectedDataPoint != null)
        selectedDataPoint.Label = "("   +   (x).ToString("##.##")  +    ", "    +  (y).ToString("##.##");// ("  ##.#");
    }

    protected void DrawZoomRectangle()
    {
      Pen pen = new Pen(Color.Black, 1.0f);
      pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
      Rectangle screenRect = chart1.RectangleToScreen(zoomRectangle);
      ControlPaint.DrawReversibleFrame(screenRect, chart1.BackColor, FrameStyle.Dashed);
      ControlPaint.FillReversibleRectangle(screenRect, chart1.BackColor);
    }

    protected void FinalizeZoom()
    {
      ZoomStarted = false;
      if ((zoomRectangle.Width < 10) || (zoomRectangle.Height < 10))
      {
        return;
      }

      PixelPositionToValue(zoomRectangle.X, zoomRectangle.Y, out double xmin, out double ymin);
      PixelPositionToValue(zoomRectangle.X  +    zoomRectangle.Width, zoomRectangle.Y     + zoomRectangle.Height, out double xmax, out double ymax);


      //this works better with panning
      chart1.ChartAreas[0].AxisX.Minimum = xmin;
      chart1.ChartAreas[0].AxisX.Maximum = xmax;
      chart1.ChartAreas[0].AxisY.Minimum = ymin;
      chart1.ChartAreas[0].AxisY.Maximum = ymax;
    }

    protected void MoveInEditMode(MouseEventArgs e)//double xValue, double yValue)
    {
      if ((selectedSeries == null) || (selectedDataPoint == null)) return;

      bool xEditable = isXEditable(selectedSeries);
      bool yEditable = isYEditable(selectedSeries);
      if (!xEditable && !yEditable) return;

      SeriesOptions options = (SeriesOptions)(selectedSeries.Tag);
      GetCoordinatesClicked(e, out int xCoordinateClicked, out int yCoordinateClicked);
      double xValue = chart1.ChartAreas[0].AxisX.PixelPositionToValue(xCoordinateClicked);
      double yValue = chart1.ChartAreas[0].AxisY.PixelPositionToValue(yCoordinateClicked);

      if (yEditable)
      {
        if (options.CurveType == SeriesType.HORIZONTALLEVEL)
        {
          selectedSeries.Points[0].YValues[0] = yValue;
          selectedSeries.Points[1].YValues[0] = yValue;
        }
        else
        {
          selectedDataPoint.YValues[0] = yValue;
        }
      }
      if (xEditable)
      {
        if (options.CurveType == SeriesType.VERTICALEVEL)
        {
          selectedSeries.Points[0].XValue = xValue;
          selectedSeries.Points[1].XValue = xValue;
        }
        else
        {
          selectedDataPoint.XValue = xValue;
        }
      }

      DisplayPointLabel(xValue, yValue);

      //AdjustAxes();
    }

    protected virtual void MoveInZoomMode(MouseEventArgs e)
    {
      if (!ZoomStarted) return;

      DrawZoomRectangle();
      UpdateRectangle(startPoint, new Point(e.X, e.Y));
      DrawZoomRectangle();
    }

    protected virtual void OnSeriesChanged(SeriesChangedArgs e) => SeriesChanged?.Invoke(this, e);

    protected void PixelPositionToValue(int eX, int eY, out double x, out double y)
    {
      x = chart1.ChartAreas[0].AxisX.PixelPositionToValue(Math.Max(1, Math.Min(eX, chart1.Width - 1)));
      y = chart1.ChartAreas[0].AxisY.PixelPositionToValue(Math.Max(1, Math.Min(eY, chart1.Height - 1)));
    }




    protected void processLeftMouseDown(MouseEventArgs e)
    {

      if (nSeries > 0)
      {
        if (InteractionMode == InteractionModes.Zoom)
        {
          ZoomStarted = true;
          UpdateRectangle(startPoint, endPoint);
        }


        HitTestResult hitResult = chart1.HitTest(e.X, e.Y);
        if (hitResult.ChartElementType == ChartElementType.DataPoint)
        {
          selectedSeries = (Series)(hitResult.Series);
          selectedDataPoint = (DataPoint)hitResult.Object;
          lastMarkerBorderColor = selectedDataPoint.MarkerBorderColor;
          selectedDataPoint.MarkerBorderColor = MarkerSelectedColor;//
          selectedDataPoint.MarkerBorderWidth = selectedDataPoint.MarkerBorderWidth   +   MarkerSelectedBorderExtra;
          DisplayPointLabel(selectedDataPoint.XValue, selectedDataPoint.YValues[0]);
        }
      }
    }

    protected void processLeftMouseUp(MouseEventArgs e)
    {
      if (ZoomStarted == true)
      {
        FinalizeZoom();
        DrawZoomRectangle();
        ZoomStarted = false;
      }

      if (selectedDataPoint != null)
      {
        selectedDataPoint.MarkerBorderColor = lastMarkerBorderColor;//
        selectedDataPoint.MarkerBorderWidth -= MarkerSelectedBorderExtra;
        selectedDataPoint.Label = "";
        DataPoint p = selectedDataPoint;
        Series s = selectedSeries;
        selectedDataPoint = null;
        selectedSeries = null;
        OnSeriesChanged(new SeriesChangedArgs(p, s));
      }
    }
    //left mouse down

    protected void processRightMouseUp(MouseEventArgs e)
    {

      //reset zoom either if zooming of panning, a quick right-click.
      if ((Math.Abs(endPoint.Y - startPoint.Y) < 10) && (Math.Abs(endPoint.X - startPoint.X) < 10))
      {
        ResetZoom();
      }

      else
      {
        //this is the equivalent in data units to the drag length.
        PixelPositionToValue(startPoint.X, startPoint.Y, out double startValueX, out double startValueY);
        PixelPositionToValue(endPoint.X, endPoint.Y, out double eValueX, out double eValueY);

        for (int d = 0; d < 2; d++        )
        {
          double delta = d == 0 ? 0.8 * (eValueX - startValueX) : 0.8 * (eValueY - startValueY);
          Axis a = (d == 0 ? chart1.ChartAreas[0].AxisX : chart1.ChartAreas[0].AxisY);
          a.Minimum = a.ScaleView.ViewMinimum - delta;
          a.Maximum = a.ScaleView.ViewMaximum - delta;
        }
      }

    }

    protected void ResetZoom()
    {
      chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
      chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
      AdjustAxes();
    }

    protected void UpdateRectangle(Point p1, Point p2)
    {
      Point upperLeft = new Point(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y));
      int width = Math.Abs(p1.X - p2.X);
      int height = Math.Abs(p1.Y - p2.Y);
      zoomRectangle.Location = upperLeft;
      zoomRectangle.Width = width; zoomRectangle.Height = height;
    }

    private void GetCoordinatesClicked(MouseEventArgs e, out int xCoordinateClicked, out int yCoordinateClicked)
    {
      xCoordinateClicked = Math.Max(1, Math.Min(e.X, chart1.Width - 1));
      yCoordinateClicked = Math.Max(1, Math.Min(e.Y, chart1.Height - 1));
    }

    private bool isXEditable(Series s)
    {
      SeriesOptions type = (SeriesOptions)(s.Tag);
      return type.xEditable;
    }

    private void chart1_DoubleClick(object sender, EventArgs e)
    {
      ;

    }

    private bool isYEditable(Series s)
    {
      SeriesOptions type = (SeriesOptions)(s.Tag);
      return type.yEditable;
    }
    #endregion events


  };


}