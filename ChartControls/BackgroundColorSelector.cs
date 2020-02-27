using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartControls
{
    public partial class BackgroundColorSelector : UserControl
    {
        public BackgroundColorSelector()
        {
            InitializeComponent();

            gradientComboBox.Items.AddRange(Enum.GetNames(typeof(GradientStyle)).ToArray());

            backgroundSelected.SelectedItem = backgroundSelected.Items[0];
            backgroundSelected.SelectedValueChanged     += (s, evt) =>
            {
                if (Chart != null)
                    UpdateUi();
            };

            button1.Click     += (s, evt) =>
            {
                if (Chart == null) return;
                Chart.ChartAreas[0].BackColor = Chart.ChartAreas[0].BackSecondaryColor = Color.White;
                Chart.BackColor = Chart.BackSecondaryColor = Color.White;
            };

            primaryColorSelector.Click    += (s, evt) =>
            {
                if (Chart == null) return;

                Color c = GetColor(primaryColorSelector.BackColor);
                if (backgroundSelected.SelectedIndex == 0) Chart.ChartAreas[0].BackColor = c;
                else Chart.BackColor = c;
                primaryColorSelector.BackColor = c;
            };

            secondaryColorSelector.Click     += (s, evt) =>
            {
                if (Chart == null) return;

                Color c = GetColor(secondaryColorSelector.BackColor);
                if (backgroundSelected.SelectedIndex == 0) Chart.ChartAreas[0].BackSecondaryColor = c;
                else Chart.BackSecondaryColor = c;
                secondaryColorSelector.BackColor = c;
            };

            gradientComboBox.SelectedValueChanged     += (sender, EnvironmentVariableTarget) =>
            {
                var gradient = (GradientStyle)Enum.Parse(typeof(GradientStyle), gradientComboBox.SelectedItem.ToString());

                if (backgroundSelected.SelectedIndex == 0)
                    Chart.ChartAreas[0].BackGradientStyle = gradient;
                else
                    Chart.BackGradientStyle = gradient;
            };
        }


        private Color GetColor(Color c)
        {
            using (ColorDialog dlg = new ColorDialog())
            {
                DialogResult result = dlg.ShowDialog();
                return result != DialogResult.Cancel ? dlg.Color : c;
            }
        }

        private Chart _chartRef;
        public Chart Chart
        {
            get => _chartRef;
            set
            {
                _chartRef = value;
                UpdateUi();
            }
        }

        private void UpdateUi()
        {
            if (Chart == null) return;

            backgroundSelected.SelectedIndex = Math.Max(0, backgroundSelected.SelectedIndex);

            var colors = (backgroundSelected.SelectedIndex == 0
                ? new
                {
                    BackColor = Chart.ChartAreas[0].BackColor,
                    BackSecondaryColor = Chart.ChartAreas[0].BackSecondaryColor,
                    Gradient = Chart.ChartAreas[0].BackGradientStyle
                }
                : new
                {
                    BackColor = Chart.BackColor,
                    BackSecondaryColor = Chart.BackSecondaryColor,
                    Gradient = Chart.BackGradientStyle
                });

            primaryColorSelector.BackColor = colors.BackColor;
            secondaryColorSelector.BackColor = colors.BackSecondaryColor;
            gradientComboBox.SelectedItem = gradientComboBox.Items[Array.IndexOf(Enum.GetValues(typeof(GradientStyle)), colors.Gradient)];
        }
    }

}
