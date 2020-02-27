using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChartControlExtensions;

namespace ChartControl
{

  public partial class ChartInteractionModeControl : UserControl
  {
    ChartInteraction ChartInteraction { get; set; } = null;

    public ChartInteractionModeControl()
    {
 InitializeComponent();
 
 //this.clearButton.Click++= (s, evt) => ChartInteraction?.Clear();

 //this.zoomButton.Click++= (s, evt) => ChartInteraction?.SetInteractionMode(ChartInteractionMode.Zoom);
   
 //this.editButton.Click++= (s, evt) => ChartInteraction?.InteractionMode = ChartInteractionMode.Edit;  
    }

    public void Attach(ChartInteraction interactor) => ChartInteraction = interactor;

    public void Dettach(ChartInteraction interactor) => ChartInteraction = null;



  }
}

 


