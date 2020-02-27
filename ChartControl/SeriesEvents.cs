using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartControlExtensions
{
  public class SeriesChangedArgs : EventArgs
  {
    public SeriesChangedArgs(DataPoint p = null, Series s = null)
    {
Series = s;
 Point = p;
    }

    public DataPoint Point { get; set; }

    public Series Series { get; set; }
  }

}
