using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.ComboBox;

namespace ChartControls
{
    public partial class SeriesConfigurationControl : UserControl
    {


        bool CanShowSymbol(SeriesChartType t) => ((t == SeriesChartType.StackedColumn) || (t == SeriesChartType.FastLine) || (t == SeriesChartType.FastPoint)) ? false : true;

        bool CanShowLine(SeriesChartType t) => ((t == SeriesChartType.FastLine) || (t == SeriesChartType.Point) || (t == SeriesChartType.FastPoint) || (t == SeriesChartType.Column) || (t == SeriesChartType.StackedColumn) ? false : true);


        public SeriesConfigurationControl()
        {
            InitializeComponent();

            string[] names = new string[]
            {"Point",
            "FastPoint",
            "Line",
            "Spline",
            "StepLine",
            "FastLine",
            "Column",
            "StackedColumn",
            //"StackedColumn100",
            "Area",
            "SplineArea"

            };
            seriesStyleCombo.Items.AddRange(names);

            int[] lineStyles = (int[])Enum.GetValues(typeof(ChartDashStyle));
            string[] lineStyleNames = (string[])Enum.GetNames(typeof(ChartDashStyle));
            foreach (string s in lineStyleNames)
                lineComboBox.Items.Add(s);

            int[] markerStyles = (int[])Enum.GetValues(typeof(MarkerStyle));
            string[] markerStyleNames = (string[])Enum.GetNames(typeof(MarkerStyle));
            foreach (string s in markerStyleNames)
                symbolComboBox.Items.Add(s);

            ConfigureEvents();
        }


        void IndexChanged()
        {
        }


        Color? SelectColor()
        {
            Color? c = null;
            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    c = dlg.Color;
                }
            }

            return c;
        }

        void ConfigureEvents()
        {
            seriesList.SelectedIndexChanged += (s, evt) =>
        {
            if (Series == null) return;

            int index = seriesList.SelectedIndex > _chart.Series.Count() ? 0 : seriesList.SelectedIndex;
            Series series = _chart.Series.ElementAt(seriesList.SelectedIndex);
            LoadSeries(series);
        };

            seriesStyleCombo.SelectedValueChanged += (s, evt) =>
        {
            if (Series == null) return;

            string styleName = this.seriesStyleCombo.SelectedItem.ToString();

            Series.ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), seriesStyleCombo.SelectedItem.ToString());

            bool showSymbol = CanShowSymbol(Series.ChartType);
            this.symbolSizeNumeric.Enabled = showSymbol;
            this.symbolComboBox.Enabled = showSymbol;
            this.symbolColorButton.Enabled = showSymbol;
            symbolCheckBox.Enabled = showSymbol;

            bool showLine = CanShowLine(Series.ChartType);
            this.lineComboBox.Enabled = showLine;
            this.lineColorButton.Enabled = showLine;
            this.lineThicknessNumeric.Enabled = showLine;
            this.lineCheckbox.Enabled = showLine;

            primaryColorArea.Enabled = styleName.ToLower().Contains("area");
            secondaryColorArea.Enabled = styleName.ToLower().Contains("area");

             
            primaryColorArea.BackColor = Series.Color;
            secondaryColorArea.BackColor = Series.BackSecondaryColor;

            try
            {
                SeriesChartType style = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), styleName);
                if ((style == SeriesChartType.FastPoint) || (style == SeriesChartType.Point))
                {
                    Series.MarkerStyle = MarkerStyle.Square;
                    symbolSizeNumeric.Enabled = true;
                    symbolColorButton.Enabled = true;
                    string symbolStyleName = Enum.GetName(typeof(MarkerStyle), Series.MarkerStyle);
                    symbolComboBox.SelectedItem = symbolStyleName;
                    symbolCheckBox.Checked = (style == SeriesChartType.Point);

                }

                if (style == SeriesChartType.FastLine)
                {
                    lineCheckbox.Checked = true;
                    lineThicknessNumeric.Value = 1;
                    lineColorButton.Enabled = true;
                }


                Series.ChartType = style;
            }
            //    if ((style == SeriesChartType.SplineArea) || (style == SeriesChartType.Area) || (style == SeriesChartType.Bar) || (style == SeriesChartType.Column) || (style == SeriesChartType.Point) || (style == SeriesChartType.FastPoint))
            //    {
            //        this.lineCheckbox.Checked = false;
            //        this.lineCheckbox.Enabled = false;
            //        this.lineThicknessNumeric.Enabled = false;
            //        this.lineComboBox.Enabled = false;

            //        //if ((style == SeriesChartType.Point) || (style == SeriesChartType.FastPoint))
            //        //this.lineColorButton.Enabled = false;

            //        //if ((style == SeriesChartType.Bar) || (style == SeriesChartType.Column))
            //        //this.lineComboBox.Enabled = false;


            //    }
            //    else if (style == SeriesChartType.FastLine)
            //    {
            //        this.symbolCheckBox.Enabled = false;
            //        this.symbolSizeNumeric.Enabled = false;
            //        this.symbolComboBox.Enabled = false;
            //        this.symbolColorButton.Enabled = false;
            //    }


            //}
            catch (Exception err)
            {
                Series.ChartType = SeriesChartType.FastLine;
            }
            LoadSeries(Series);
        };

            symbolCheckBox.CheckedChanged += (obj, evt) =>
        {
            if (Series == null) return;

            Series s = Series;
            if (symbolCheckBox.Checked == true)
            {
                if (s.MarkerStyle == MarkerStyle.None)
                    s.MarkerStyle = MarkerStyle.Square;

                if (s.MarkerSize <= 0)
                {
                    this.symbolSizeNumeric.Value = (int)(3);
                }
            }
            else
            {
                s.MarkerStyle = MarkerStyle.None;
            }


            string symbolStyleName = Enum.GetName(typeof(MarkerStyle), s.MarkerStyle);
            symbolComboBox.SelectedItem = symbolStyleName;
        };

            symbolColorButton.Click += (obj, evt) =>
        {
            if (Series == null) return;
            Color? c = SelectColor();
            if (c != null)
            {
                Series s = Series;
                symbolColorButton.BackColor = (Color)c;
                s.MarkerColor = symbolColorButton.BackColor;
                s.MarkerBorderColor = symbolColorButton.BackColor;
            }


        };

            symbolComboBox.SelectedIndexChanged += (obj, evt) =>
        {
            if (Series == null) return;

            Series.MarkerStyle = Series.MarkerStyle = (MarkerStyle)Enum.Parse(typeof(MarkerStyle), symbolComboBox.SelectedItem.ToString());
        };

            symbolSizeNumeric.ValueChanged += (obj, evt) =>
        {
            if (Series == null) return;
            Series.MarkerSize = (int)(symbolSizeNumeric.Value);
        };

            primaryColorArea.Click += (obj, evt) =>
            {
                if (Series == null) return;
                Color? c = SelectColor();

                if (c != null)
                {
                    Series s = Series;
                    primaryColorArea.BackColor = (Color)c;
                    Series.Color = primaryColorArea.BackColor;
                }
            };

            secondaryColorArea.Click += (obj, evt) =>
            {
                if (Series == null) return;
                Color? c = SelectColor();

                if (c != null)
                {
                    Series s = Series;
                    secondaryColorArea.BackColor = (Color)c;
                    s.BackSecondaryColor = secondaryColorArea.BackColor;
                }
            };

            lineColorButton.Click += (obj, evt) =>
          {
              if (Series == null) return;
              Color? c = SelectColor();

              if  (c != null)
              {
                  Series s = Series;
                  lineColorButton.BackColor = (Color)c;
                  s.BorderColor = lineColorButton.BackColor;
                  s.Color = lineColorButton.BackColor;

              }
          };

            lineThicknessNumeric.ValueChanged += (obj, evt) =>
         {
             if (Series == null) return;

             Series.BorderWidth = (int)(this.lineThicknessNumeric.Value);
         };

            lineComboBox.SelectedIndexChanged += (obj, evt) =>
        {
            if (Series == null) return;

            string dashName = lineComboBox.SelectedItem.ToString();
            ChartDashStyle style = (ChartDashStyle)Enum.Parse(typeof(ChartDashStyle), dashName);
            Series.BorderDashStyle = style;
        };

            lineCheckbox.CheckedChanged += (obj, evt) =>
        {
            if (Series == null) return;
            if (!this.lineCheckbox.Checked)
                this.lineThicknessNumeric.Value = 0;
            else
              if ((int)(this.lineThicknessNumeric.Value) == 0)
                this.lineThicknessNumeric.Value = 1;//lineThicknessNumeric.Value = 0;
        };

            nameTextBox.TextChanged += (s, evt) => { if (Series != null) Series.Name = nameTextBox.Text; };
            visibleCheckBox.CheckedChanged += (s, evt) => { if (Series != null) Series.Enabled = visibleCheckBox.Checked; };
            legendCheckBox.CheckedChanged += (s, evt) => { if (Series != null) Series.IsVisibleInLegend = legendCheckBox.Checked; };
            deleteButton.Click += (s, evt) => { if (Series != null) { _chart.Series.Remove(Series); _UpdateUi(); } };

        }

        void LoadSeries(Series series)
        {
            if (series == null) return;

            nameTextBox.Text = series.Name;

            string name = Enum.GetName(typeof(SeriesChartType), series.ChartType);
            seriesStyleCombo.SelectedItem = seriesStyleCombo.Items[seriesStyleCombo.Items.IndexOf(name)];

            visibleCheckBox.Checked = series.Enabled;
            legendCheckBox.Checked = series.IsVisibleInLegend;

            symbolCheckBox.Checked = (series.MarkerStyle != MarkerStyle.None);
            string symbolStyleName = Enum.GetName(typeof(MarkerStyle), series.MarkerStyle);
            symbolComboBox.SelectedItem = symbolStyleName;
            symbolColorButton.BackColor = series.MarkerColor;
            symbolSizeNumeric.Value = (int)(series.MarkerSize);

            lineCheckbox.Checked = series.BorderWidth > 0;
            lineColorButton.BackColor = series.BorderColor;
            //lineColorButton.ForeColor = series.BorderColor;
            string linetyleName = Enum.GetName(typeof(ChartDashStyle), series.BorderDashStyle);
            lineComboBox.SelectedItem = linetyleName;
            lineThicknessNumeric.Value = series.BorderWidth;


            areaConfig.Enabled = name.ToLower().Contains("area");
            if (!areaConfig.Enabled)
            {
                primaryColorArea.BackColor = Color.Gray;
                secondaryColorArea.BackColor = Color.Gray;
            }
            else
            {

                //primaryColorArea.Enabled = name.ToLower().Contains("area");
                //secondaryColorArea.Enabled = name.ToLower().Contains("area");
                primaryColorArea.BackColor = series.Color;
                secondaryColorArea.BackColor = series.BackSecondaryColor;
            }
        }

        Series Series
        {
            get
            {
                return ((_chart == null) || (seriesList.SelectedIndex < 0) || (seriesList.SelectedIndex > _chart.Series.Count() - 1)) ?
                        null : _chart.Series.ElementAt(seriesList.SelectedIndex);
            }
        }

        Chart _chart;
        public Chart Chart
        {
            get => _chart;
            set
            {
                _chart = value;
                _UpdateUi();
            }
        }
        private void _UpdateUi(int index = 0)
        {
            if ((_chart == null) || (_chart.Series.Count() < 1)) return;
            if (_chart.Series.Count() < 1) return;

            seriesList.Items.Clear();
            seriesList.Items.AddRange(_chart.Series.Select(t => t.Name).ToArray());

            //LoadSeries(_chart.Series.ElementAt(0));
            seriesList.SelectedIndex = index;
        }

    }

}
