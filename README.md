# InteractiveChartWinforms
Work on progress

This project is a dll that allows to add Zoomin, Panning and Editing to winforms charts with one sinle line of code

 
<code>
  public Form1()
        {
            InitializeComponent();
             
            CreateExampleChart(chart2);

            ChartInteractor = ChartInteractionZoomPanEdit.CreateInstance(chart2);

        }
</code>
 
