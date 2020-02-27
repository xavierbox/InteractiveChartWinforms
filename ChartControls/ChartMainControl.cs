using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartControls
{
  public partial class ChartMainControl : Form
  {
    public ChartMainControl(Chart chart = null)
    {
      InitializeComponent();      
      Chart = chart;
    }

    Chart _chartRef;
    public Chart Chart
    {
      get => _chartRef;
      set
      {
        _chartRef = value;
        UpdateUI();
      }
    }

     
    private void UpdateUI()
    {
      axisConfigurationControl1.Chart = Chart;
      titlesControl1.Chart = Chart;
      backgroundColorSelector1.Chart = Chart;
      seriesConfigurationControl1.Chart = Chart;
    }
  }
}
