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

namespace ChartControls
{
    public partial class AxisConfigurationControl : UserControl
    {
        public AxisConfigurationControl()
        {
            InitializeComponent();

            xradioButton.CheckedChanged     += (s, evt) => { UpdateUi(); };
            yradioButton.CheckedChanged     += (s, evt) => { UpdateUi(); };
            zradioButton.CheckedChanged     += (s, evt) => { UpdateUi(); };

            arrowStyleCombobox.Items.AddRange(Enum.GetNames(typeof(AxisArrowStyle)).ToArray());
            arrowStyleCombobox.SelectedValueChanged     += (s, evt) =>
            {
                if (Axis != null) Axis.ArrowStyle = (AxisArrowStyle)Enum.Parse(typeof(AxisArrowStyle), arrowStyleCombobox.SelectedItem.ToString());
            };

            titleBox.KeyUp     += (s, evt) => { if (Axis != null) Axis.Title = titleBox.Text; };

            reverseCheckbox.CheckedChanged     += (s, evt) => { if (Axis != null) Axis.IsReversed = reverseCheckbox.Checked; };

            fontSelectorButton.Click     += (s, evt) =>
            {
                if (Axis == null) return;
                FontDialog dlg = new FontDialog();

                if (dlg.ShowDialog() != DialogResult.Cancel)
                { Axis.TitleFont = dlg.Font; }
            };

            gridCheckBox.CheckedChanged     += (s, evt) =>
            {
                if (Axis == null) return;
                Axis.MajorGrid.Enabled = gridCheckBox.Checked;
            };

 


        //minorGrid.CheckedChanged     += (s, evt) => { if (Axis != null) Axis.MinorGrid.Enabled = minorGrid.Checked; };
        //logaritmicCheck.CheckedChanged     += (s, evt) => { if (Axis != null) Axis.IsLogarithmic = logaritmicCheck.Checked; };
    }

        private Chart _chartRef = null;
        public Chart Chart
        {
            private get => _chartRef;
            set
            {
                _chartRef = value;
                xradioButton.Checked = false;
                xradioButton.Checked = true;

            }
        }

        private Axis Axis {
            get
            {
                if (_chartRef == null) return null;

                if (xradioButton.Checked)
                    return _chartRef.ChartAreas[0].AxisX;
                else if (yradioButton.Checked)
                    return _chartRef.ChartAreas[0].AxisY;
                else if (zradioButton.Checked)
                    return _chartRef.ChartAreas[0].AxisY2;
                else return null;
            }
        }
        
        private void UpdateUi()
        {
            if (Axis == null) return;

            Axis a = Axis;
            arrowStyleCombobox.SelectedItem = arrowStyleCombobox.Items[Array.IndexOf(Enum.GetValues(typeof(AxisArrowStyle)), a.ArrowStyle)];
            titleBox.Text = a.Title;
            gridCheckBox.Checked = Axis.MajorGrid.Enabled;

            reverseCheckbox.Checked = Axis.IsReversed;
            logaritmicCheck.Checked = Axis.IsLogarithmic;
        }
    }
}



