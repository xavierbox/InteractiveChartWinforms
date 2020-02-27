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
    public enum ChartInteractionMode { Zoom = 0, Edit = 1, Pan = 2, View = 3 };

    public class AxisViewsList
    {
        class AxisView
        {
            public Axis[] Axis { get; set; }

            public double[] Min { get; set; }

            public double[] Max { get; set; }

            public double[] Interval { get; set; }
        }

        List<AxisView> _views = new List<AxisView>();
        int _current = 0;

        public void Append(Axis x, Axis y, Axis y2)
        {
            Console.WriteLine("I am a text");
            //Console.WriteLine("I am a text");
            /**/

            /*
            AxisView view = new AxisView()
            {
              Axis = new Axis[] { x, y, y2 },
              Min = new double[] { x.Minimum, y.Minimum, y2.Minimum },
              Max = new double[] { x.Maximum, y.Maximum, y2.Maximum },
              Interval = new double[] { x.Interval, y.Interval, y2.Interval }
            };

             */
            AxisView view = new AxisView()
            {
                Axis = new Axis[] { x, y, y2 },
                Min = new double[] { x.Minimum, y.Minimum, y2.Minimum },
                Max = new double[] { x.Maximum, y.Maximum, y2.Maximum },
                Interval = new double[] { x.Interval, y.Interval, y2.Interval }
            };

            _views.Add(view);//.Insert(0, view );
            _current = _views.Count - 1;
        }

        public void Clear() { _views.Clear(); }

        private void Restore(int index)
        {
            _current = index;
            AxisView view = _views.ElementAt(index);

            for (int n = 0; n < view.Axis.Count(); n++)
            {
                view.Axis[n].Minimum = view.Min[n];
                view.Axis[n].Maximum = view.Max[n];
                view.Axis[n].Interval = view.Interval[n];
            }
        }

        public void RestoreNextAxisView()
        {
            if (Count < 1) return;
            int next = Math.Min(_current + 1, Count - 1);

            Restore(next);
        }

        public void RestorePreviousAxisView()
        {
            if (Count < 1) return;
            int prev = Math.Max(_current - 1, 0);
            Restore(prev);
        }

        public int Count => _views.Count();

    };

    public class InteractionModeArgs : EventArgs
    {
        public InteractionModeArgs(ChartInteractionMode mode)
        {
            InteractionMode = mode;
        }

        public ChartInteractionMode InteractionMode { get; }

    }


    public abstract class ChartInteraction
    {

        public event EventHandler<InteractionModeArgs> InteractionModeChangedEvent = delegate { };

        public event EventHandler ChartAttached = delegate { };

        public event EventHandler ChartDettached = delegate { };

        protected Chart _chart = null;

        protected ChartArea DefaultChartArea => _chart.ChartAreas[0];

        protected HitTestResult HitResult { get; set; } = null;

        protected DataPoint SelectedDataPoint { get; set; } = null;

        protected Series SelectedSeries { get; set; } = null;

        protected Point StartPoint { get; set; } = default(Point);

        protected Point EndPoint { get; set; }

        protected Rectangle ZoomRectangle { get; set; } = default(Rectangle);

        protected bool ZoomStarted { get; set; } = false;

        protected Point? MouseIsDown { get; set; } = null;

        protected void ClearSelection()
        {
            StartPoint = new Point(1, 1);
            EndPoint = StartPoint;
            SelectedDataPoint = null;
            SelectedSeries = null;
            ZoomStarted = false;
            MouseIsDown = null;
            HitResult = null;
        }



        protected void StoreAxisViews()
        => _storedAxesViews.Append(DefaultChartArea.AxisX, DefaultChartArea.AxisY, DefaultChartArea.AxisY2);

        protected AxisViewsList _storedAxesViews = new AxisViewsList();

        protected ChartInteractionMode _interactionMode = ChartInteractionMode.View;

        public ChartInteractionMode InteractionMode
        {
            get => _interactionMode;

            set
            {
                _interactionMode = value;
                RaiseInteractionModeChanged(new InteractionModeArgs(_interactionMode));
            }

        }

        public virtual Chart Attach(Chart chart)
        {
            if (_chart == chart) return chart;

            Detach();

            if (chart != null)
            {
                _chart = chart;
                _chart.MouseEnter += new EventHandler(MouseEnter);
                _chart.MouseLeave += new EventHandler(MouseLeave);
                _chart.MouseDown += new MouseEventHandler(MouseDown);
                _chart.MouseUp += new MouseEventHandler(MouseUp);
                _chart.MouseMove += new MouseEventHandler(MouseMove);
                _chart.KeyDown += new KeyEventHandler(KeyDown);
                _chart.MouseWheel += new MouseEventHandler(MouseWheel);

                InteractionMode = ChartInteractionMode.View;
                SetCursorForInteractionMode(_chart, InteractionMode);
                InteractionModeChangedEvent += (s, e) =>
                {
                SetCursorForInteractionMode(_chart, InteractionMode);
                };


                ChartAttached?.Invoke(this, EventArgs.Empty);
            }

            _storedAxesViews.Clear();
            ClearSelection();


            return _chart;

        }

        //protected 
        public virtual void Detach()
        {
            if (_chart == null) return;

           // foreach (EventHandler<InteractionModeArgs> subscriber in InteractionModeChangedEvent.GetInvocationList())
          //  {
           //     InteractionModeChangedEvent -= subscriber;
          //  }

            _chart.MouseEnter -= MouseEnter;
            _chart.MouseLeave -= MouseLeave;
            _chart.MouseDown -= MouseDown;
            _chart.MouseUp -= MouseUp;
            _chart.MouseMove -= MouseMove;
            _chart.KeyDown -= KeyDown;
            _chart.MouseDoubleClick -= MouseWheel;

            _storedAxesViews.Clear();
            ClearSelection();

            _chart = null;
        }

        protected virtual void RaiseInteractionModeChanged(InteractionModeArgs e)
        {
            InteractionModeChangedEvent?.Invoke(this, e);
        }

        public virtual void Clear() => _chart?.Series.Clear();

        protected void PixelPositionToValue(int eX, int eY, out double x, out double y, out double y2)
        {
            x = DefaultChartArea.AxisX.PixelPositionToValue(Math.Max(1, Math.Min(eX, _chart.Width - 1)));
            y = DefaultChartArea.AxisY.PixelPositionToValue(Math.Max(1, Math.Min(eY, _chart.Height - 1)));
            y2 = DefaultChartArea.AxisY2.PixelPositionToValue(Math.Max(1, Math.Min(eY, _chart.Height - 1)));
        }

        protected void PixelResolution(out double x, out double y, out double y2)
        {
            x = Math.Abs(DefaultChartArea.AxisX.PixelPositionToValue(1) - DefaultChartArea.AxisX.PixelPositionToValue(10)) / 10.0;
            y = Math.Abs(DefaultChartArea.AxisY.PixelPositionToValue(1) - DefaultChartArea.AxisY.PixelPositionToValue(10)) / 10.0;
            y2 = Math.Abs(DefaultChartArea.AxisY2.PixelPositionToValue(1) - DefaultChartArea.AxisY2.PixelPositionToValue(10)) / 10.0;
        }

        public void AdjustAxes()
        {
            _chart.AdjustAxes();
            _storedAxesViews.Clear();
        }

        public void SetCursorForInteractionMode(Chart chart1, ChartInteractionMode InteractionMode)
        {
            switch (InteractionMode)
            {
                case ChartInteractionMode.Zoom:
                    {
                        chart1.Cursor = Cursors.Cross;
                        break;
                    }
                case ChartInteractionMode.Edit:
                    {
                        chart1.Cursor = Cursors.Help;
                        break;
                    }
                case ChartInteractionMode.Pan:
                    {
                        chart1.Cursor = Cursors.SizeAll;
                        break;
                    }
                case ChartInteractionMode.View:
                    {
                        chart1.Cursor = Cursors.Arrow;
                        break;
                    }
                default:
                    {
                        chart1.Cursor = Cursors.UpArrow;
                        break;
                    }
            }
        }


        #region events 
        protected abstract void MouseUp(object sender, MouseEventArgs e);

        protected abstract void MouseMove(object sender, MouseEventArgs e);

        protected abstract void MouseDown(object sender, MouseEventArgs e);

        protected virtual void MouseWheel(object sender, MouseEventArgs e) {; }

        protected virtual void MouseEnter(object sender, EventArgs e) => _chart.Focus();// _chart?.Focus();

        protected virtual void MouseLeave(object sender, EventArgs e) { _chart.Parent.Focus(); MouseIsDown = null; }


        protected abstract void KeyDown(object sender, KeyEventArgs e);


        #endregion



    }

}
