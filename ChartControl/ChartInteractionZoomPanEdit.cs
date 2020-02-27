using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Cursor = System.Windows.Forms.Cursor;
using System.Windows.Input;

//https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.mousebuttons?view=netframework-4.8


namespace ChartControlExtensions
{

    public class ChartInteractionZoomPanEdit : ChartInteraction
    {
        const int PIXELTHRESHOLD = 10;
        const int EXTRAWIDHTSELECTEDPOINT = 5;


        private Color _selectedMarkerColor;

        public static ChartInteraction CreateInstance(Chart chart)
        {
            return new ChartInteractionZoomPanEdit(chart);
        }

        public ChartInteractionZoomPanEdit(Chart chart)
        {
            Attach(chart);
        }

        #region events

        private bool ClickedCloseToAPoint(MouseEventArgs e, Point p, HitTestResult objClicked)
        {
            bool isClose = false;
            if (objClicked.ChartElementType != ChartElementType.DataPoint) return false;

            SelectedDataPoint = (DataPoint)(objClicked.Object);
            SelectedSeries = (Series)(objClicked.Series);

            double xValue = SelectedDataPoint.XValue;
            double yValue = SelectedDataPoint.YValues[0];
            PixelPositionToValue(e.X, e.Y, out double xChartValue, out double yChartValue, out double y2ChartValue);
            PixelResolution(out double resX, out double resY, out double resY2);

            //the control actually selects points if you click the line. This is wrong and it is a bug from microsoft
            //we correct that here. 
            if (Math.Abs(xChartValue - xValue) < PIXELTHRESHOLD * resX)
            {
                double dy = (SelectedSeries.YAxisType == AxisType.Secondary ? y2ChartValue - yValue : yChartValue - yValue);
                if (Math.Abs(dy) < PIXELTHRESHOLD * (SelectedSeries.YAxisType == AxisType.Secondary ? resY2 : resY))
                {
                    isClose = true;
                }
            }

            return isClose;
        }

        protected override void MouseDown(object sender, MouseEventArgs e)
        {
            ClearSelection();

            EndPoint = StartPoint = (new Point(e.X, e.Y));
            MouseIsDown = e.Location;

            if (e.Button == MouseButtons.Right)
            {
                InteractionMode = ChartInteractionMode.Pan;
            }

            else if (e.Button == MouseButtons.Left)
            {
                HitTestResult hitResult = _chart.HitTest(e.X, e.Y);

                if (hitResult.ChartElementType == ChartElementType.PlottingArea)
                {
                    //it is a candidate to zoom but dragging needs to start first. so we deffer everything for later
                }

                //cicked on something ?  ...what is it? D
                //data points are different so we treat them apart. 
                if (ClickedCloseToAPoint(e, EndPoint, hitResult))
                {
                    Boolean.TryParse(SelectedDataPoint["XEditable"], out bool xEditable);
                    Boolean.TryParse(SelectedDataPoint["YEditable"], out bool yEditable);

                    if (xEditable || yEditable)
                    {
                        _selectedMarkerColor = SelectedDataPoint.MarkerBorderColor;
                        SelectedSeries = (Series)(hitResult.Series);
                        SelectedDataPoint = (DataPoint)hitResult.Object;
                       // SelectedDataPoint.MarkerBorderColor = Color.Red;
                       // SelectedDataPoint.MarkerBorderWidth = SelectedDataPoint.MarkerBorderWidth + EXTRAWIDHTSELECTEDPOINT;
                        InteractionMode = ChartInteractionMode.Edit;
                    }
                }


            }//left button 
        }

        protected override void MouseUp(object sender, MouseEventArgs e)
        {
            EndPoint = (new Point(e.X, e.Y));
            int dx = EndPoint.X - StartPoint.X, dy = EndPoint.Y - StartPoint.Y;
            bool quickClick = (dx * dx < PIXELTHRESHOLD * PIXELTHRESHOLD) && (dy * dy < PIXELTHRESHOLD * PIXELTHRESHOLD) ? true : false;

            MouseIsDown = null;

            if ((quickClick) && (e.Button == MouseButtons.Right)) //reset view 
            {
                _chart.AdjustAxes();
                _storedAxesViews.Clear();
                InteractionMode = ChartInteractionMode.View;
                return;
            }

            if (e.Button == MouseButtons.Middle) //do nothing 
            {
                return;
            }
            else
            {
                if (_storedAxesViews.Count < 1) StoreAxisViews();

                if (e.Button == MouseButtons.Right) //pan or reset 
                {
                    FinalizePan();
                }

                else //if (e.Button == MouseButtons.Left) //finish a zoom
                {

                    if (InteractionMode == ChartInteractionMode.Zoom)
                    {
                        FinalizeZoom();
                    }

                    if (InteractionMode == ChartInteractionMode.Edit)
                        FinalizeEdit();
                }

                StoreAxisViews();
                InteractionMode = ChartInteractionMode.View;
            }//else 

            ClearSelection();
        }

        private void FinalizeEdit()
        {
            if (SelectedDataPoint != null)
            {
               // SelectedDataPoint.MarkerBorderColor = _selectedMarkerColor;
               // SelectedDataPoint.MarkerBorderWidth = Math.Max(0, SelectedDataPoint.MarkerBorderWidth - EXTRAWIDHTSELECTEDPOINT);
            }
        }

        protected override void MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseIsDown == null) return;

            if (InteractionMode == ChartInteractionMode.Zoom)
            {
                DrawZoomRectangle();  //delete prev rectangle 
                UpdateZoomRectangle(StartPoint, new Point(e.X, e.Y));
                DrawZoomRectangle();  //paint the rectangle
            }

            else if (InteractionMode == ChartInteractionMode.Edit)
            {
                Boolean.TryParse(SelectedDataPoint["XEditable"], out bool xEditable);
                Boolean.TryParse(SelectedDataPoint["YEditable"], out bool yEditable);

                double xValue = SelectedDataPoint.XValue;
                double yValue = SelectedDataPoint.YValues[0];

                PixelPositionToValue(e.X, e.Y, out double xChartValue, out double yChartValue, out double y2ChartValue);
                PixelResolution(out double resX, out double resY, out double resY2);

                if (xEditable) SelectedDataPoint.XValue += xChartValue - xValue;
                if (yEditable) SelectedDataPoint.YValues[0] += (SelectedSeries.YAxisType == AxisType.Secondary ? y2ChartValue - yValue : yChartValue - yValue);
            }

            else if (InteractionMode == ChartInteractionMode.Pan)
            {
            }


            //it is dragging a zoom? is it an Edit? it is just the 
            else 
            {
                if (e.Button == MouseButtons.Left)
                {
                    int dx = e.X - StartPoint.X;
                    int dy = e.Y - StartPoint.Y;
                    if ((dx * dx + dy * dy) > 200)       //it is dragging the left button. It has to be a zoom
                    {
                        InteractionMode = ChartInteractionMode.Zoom;
                        UpdateZoomRectangle(StartPoint, EndPoint);
                        DrawZoomRectangle();
                    }
                }
            }




        }

        protected override void MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > PIXELTHRESHOLD)
            {
                _storedAxesViews.RestoreNextAxisView();
            }

            else if (e.Delta < -PIXELTHRESHOLD)
            {
                _storedAxesViews.RestorePreviousAxisView();
            }
        }

        protected override void KeyDown(object sender, KeyEventArgs e)
        {

            if ((e.KeyCode == Keys.ShiftKey) || (e.KeyCode == Keys.Shift))
            {
                ;
            }

            if ((e.KeyCode == Keys.T))
            {
                if (MouseIsDown != null)
                {
                    Console.WriteLine("Add text activated. Mouse left pressed plus letter t");
                    Label l = new Label();
                    l.Text = "I am a text";
                    l.Parent = _chart;
                    l.Location = (Point)MouseIsDown;
                    l.BackColor = Color.Transparent;
                }
            }

        }

        #endregion

        protected void DrawZoomRectangle()
        {
            Pen pen = new Pen(Color.Black, 2.0f);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Rectangle screenRect = _chart.RectangleToScreen(ZoomRectangle);
            ControlPaint.DrawReversibleFrame(screenRect, _chart.BackColor, FrameStyle.Dashed);
        }

        protected void FinalizeZoom()
        {
            //ZoomStarted = false;
            if ((ZoomRectangle.Width > PIXELTHRESHOLD) && ZoomRectangle.Height > PIXELTHRESHOLD)
            {
                PixelPositionToValue(ZoomRectangle.X, ZoomRectangle.Y, out double xmin, out double ymin, out double y2min);
                PixelPositionToValue(ZoomRectangle.X + ZoomRectangle.Width, ZoomRectangle.Y + ZoomRectangle.Height, out double xmax, out double ymax, out double y2max);
                _chart.SetAxisInterval(Math.Min(xmin, xmax), Math.Max(xmin, xmax), DefaultChartArea.AxisX);
                _chart.SetAxisInterval(Math.Min(ymin, ymax), Math.Max(ymin, ymax), DefaultChartArea.AxisY);
                _chart.SetAxisInterval(Math.Min(y2min, y2max), Math.Max(y2min, y2max), DefaultChartArea.AxisY2);
                if (Control.ModifierKeys != Keys.Shift) //it is an inverse zoom (zoom-out)
                {
                    //implement zoom-out here
                }
                
            }



            //delete previous 
            DrawZoomRectangle();
        }

        protected void FinalizePan()
        {
            //this is the equivalent in data units to the drag length.
            PixelPositionToValue(StartPoint.X, StartPoint.Y,
                                 out double startValueX,
                                 out double startValueY,
                                 out double startValueY2);

            PixelPositionToValue(EndPoint.X, EndPoint.Y,
                                 out double endValueX,
                                 out double endValueY,
                                 out double endValueY2);


            Axis[] axes = new Axis[] { DefaultChartArea.AxisX, DefaultChartArea.AxisY, DefaultChartArea.AxisY2 };
            double[] delta = { (endValueX - startValueX), (endValueY - startValueY), (endValueY2 - startValueY2) };

            for (int d = 0; d < 3; d++)
            {
                axes[d].Minimum = axes[d].ScaleView.ViewMinimum - delta[d];
                axes[d].Maximum = axes[d].ScaleView.ViewMaximum - delta[d];
            }




            //Axis[] axes = new Axis[] { DefaultChartArea.AxisX, DefaultChartArea.AxisY, DefaultChartArea.AxisY2 };
            //double[] delta = { (endValueX - startValueX), (endValueY - startValueY), (endValueY2 - startValueY2) };
            //for (int d = 0; d < 3; d        )
            //{
            //  var minView = axes[d].ScaleView.ViewMinimum - delta[d];
            //  var maxView = axes[d].ScaleView.ViewMaximum - delta[d];
            //  _chart.SetAxisInterval(minView, maxView, axes[d]);
            //}
        }

        protected void UpdateZoomRectangle(Point p1, Point p2)
          => ZoomRectangle = new Rectangle(new Point(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y)), new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y)));
    }//class 


}//namespace 





