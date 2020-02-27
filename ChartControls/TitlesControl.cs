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
    public partial class TitlesControl : UserControl
    {
        public TitlesControl()
        {
            InitializeComponent();

            fontSelectorButton.Click     += (s, evt) =>
            {
                if (Chart != null)
                {
                    CreateTitlesIfNeeded(0);
                    Chart.Titles[0].Font = GetFont(defaultFont: Chart.Titles[0].Font);
                }
            };

            secondaryFontSelector.Click     += (s, evt) =>
            {
                if (Chart != null)
                {
                    CreateTitlesIfNeeded(1);
                    Chart.Titles[1].Font = GetFont(defaultFont: Chart.Titles[1].Font);
                }
                };

            textBox1.KeyUp     += (s, evt) => { ProcessKeyStrole(evt, 0); };// CreateTitlesIfNeeded( 0 ); Chart.Titles[0].Text = textBox1.Text; };
            textBox2.KeyUp     += (s, evt) => { ProcessKeyStrole(evt, 1); };
            //{ CreateTitlesIfNeeded( 1 ); Chart.Titles[1].Text = textBox2.Text; };
        }


        void ProcessKeyStrole( KeyEventArgs evt, int titleIndex )
        {
            if (Chart == null) return; 
    
            //pressed delete or deleted all text 
            TextBox[] controls = new TextBox[] {  textBox1,textBox2 };
            if (/*(evt.KeyCode == Keys.Delete) ||*/ (controls[titleIndex].Text.Trim() == string.Empty))
            {
                DeleteTitles(titleIndex);
            }
            else
            {
                CreateTitlesIfNeeded(titleIndex);
                Chart.Titles[titleIndex].Text = controls[titleIndex].Text;
            }
        }

        void DeleteTitles(int titleIndex)
        {
            if (Chart == null) return;

            if (titleIndex == 1)
            {
                textBox2.Text = string.Empty;
                if (_chartRef.Titles.Count() > 1)
                    _chartRef.Titles.RemoveAt(1);
            }

            else if (titleIndex == 0)
            {
                textBox2.Text = string.Empty;
                textBox1.Text = string.Empty;
                _chartRef.Titles.Clear();
            }


        }

        void CreateTitlesIfNeeded(int index)
        {
            int needed = 1  +    index - Chart.Titles.Count();

            for(int n = 1; n <= needed; n++        )
                Chart.Titles.Add("");
 
        }

        public Chart _chartRef;
        public Chart Chart
        {
            get => _chartRef;
            set
            {
                _chartRef = value;
                UpdateUi();
            }
        }

        Font GetFont(Font defaultFont)
        {
            using (FontDialog dlg = new FontDialog())
            {
                return  (dlg.ShowDialog() != DialogResult.Cancel ? dlg.Font : defaultFont);
            }
        }

        private void UpdateUi()
        {
            var titles = Chart?.Titles;
            
            if (titles == null || titles.Count() < 1 ) return;

            textBox1.Text = Chart.Titles[0].Text;
            if(Chart.Titles.Count()>1)
            textBox2.Text = Chart.Titles[1].Text;
        }
    }
}
