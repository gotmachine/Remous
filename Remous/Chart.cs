using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Remous
{
    public partial class Chart : Form
    {
        DataPointCollection s1Points;
        DataPointCollection s2Points;
        Series s1;
        Series s2;
        private double maxPoints;
        mainForm mainForm;

        public Chart(mainForm mainForm, bool hideCursor = false, bool useCurves = false, bool isM2Enabled = true)
        {
            this.mainForm = mainForm;

            InitializeComponent();

            NamedImage namedImage = new NamedImage("inonde_logo_noir_700px", global::Remous.Resources.inonde_logo_noir_700px);
            chart1.Images.Add(namedImage);
            ((ImageAnnotation)chart1.Annotations["logo"]).Image = "inonde_logo_noir_700px";

            maxPoints = Program.settings.GraphicDuration / Program.settings.GraphicInterval;
            chart1.ChartAreas[0].AxisX.ScaleView.Size = maxPoints;

            s1 = chart1.Series["Series1"];
            s2 = chart1.Series["Series2"];

            s1Points = s1.Points;
            s2Points = s2.Points;

            if (!useCurves)
            {
                chart1.Series["Series1"].ChartType = SeriesChartType.Column;
                chart1.Series["Series2"].ChartType = SeriesChartType.Column;
            }

            if (!isM2Enabled)
            {
                chart1.Series["Series2"].Enabled = false;
            }

            if (hideCursor)
                System.Windows.Forms.Cursor.Hide();

            Program.OnTimerTick += AddPoints;
        }

        public void AddPoints(double m1Power, double m1Frequency, double m2Power, double m2Frequency)
        {
            if (m1Power > 0.0)
            {
                if (Program.settings.M1Unit != "V/m")
                    m1Power = Math.Pow(m1Power * 0.377, 0.5);

                s1.LegendText = $"{Program.settings.M1Title}\nIntensité : {m1Power.ToString("0.00 V/m")}\nFrequence : {m1Frequency.ToString("0.0 Mhz")}";

                s1Points.AddY(m1Power);
            }
            else
            {
                s1Points.AddY(-10.0);
            }

            if (m2Power > 0.0)
            {
                if (Program.settings.M2Unit != "V/m")
                    m2Power = Math.Pow(m2Power * 0.377, 0.5);

                s2.LegendText = $"{Program.settings.M2Title}\nIntensité : {m2Power.ToString("0.00 V/m")}\nFrequence : {m2Frequency.ToString("0.0 Mhz")}";

                s2Points.AddY(m2Power);
            }
            else
            {
                s2Points.AddY(-10.0);
            }

            if (s1Points.Count > maxPoints)
            {
                s1Points.RemoveAt(0);
                s2Points.RemoveAt(0);
            }

            AdjustGraphics();
        }

        private void AdjustGraphics()
        {
            // we want the minimum to always be 0.0
            chart1.ChartAreas[0].AxisY2.Minimum = 0.0; 

            // sets the Maximum of the right axis to NaN for RecalculateAxesScale() to do its magic
            chart1.ChartAreas[0].AxisY2.Maximum = double.NaN; 
            chart1.ChartAreas[0].RecalculateAxesScale();

            // give us some space at the top so the legend and logo don't overlap
            chart1.ChartAreas[0].AxisY2.Maximum *= 1.15; 

            // ensure that the left axis scale match the right axis
            chart1.ChartAreas[0].AxisY.Minimum = 0.0; 
            chart1.ChartAreas[0].AxisY.Maximum = chart1.ChartAreas[0].AxisY2.Maximum; // sets the Minimum to NaN

            // reposition the "Extrême" label on the left axis so it never intersect the maximum (prevent a black top border to appear)
            if (chart1.ChartAreas[0].AxisY.Maximum > 0.75)
            {
                chart1.ChartAreas[0].AxisY.CustomLabels[4].ToPosition = chart1.ChartAreas[0].AxisY.Maximum;
            }
            else
            {
                chart1.ChartAreas[0].AxisY.CustomLabels[4].ToPosition = 50.0;
            }

            // adjust the right axis intervals depending on the current scale
            // we don't use the automatic adjustement because it tend to change too often and to create very small intervals
            if (chart1.ChartAreas[0].AxisY2.Maximum < 0.5)
            {
                chart1.ChartAreas[0].AxisY2.MajorGrid.Interval = 0.02;
                chart1.ChartAreas[0].AxisY2.Interval = 0.02;
                chart1.ChartAreas[0].AxisY2.MajorTickMark.Interval = 0.02;
                chart1.ChartAreas[0].AxisY.Interval = 0.02;
            }
            else if (chart1.ChartAreas[0].AxisY2.Maximum < 1.0)
            {
                chart1.ChartAreas[0].AxisY2.MajorGrid.Interval = 0.05;
                chart1.ChartAreas[0].AxisY2.Interval = 0.05;
                chart1.ChartAreas[0].AxisY2.MajorTickMark.Interval = 0.05;
                chart1.ChartAreas[0].AxisY.Interval = 0.05;
            }
            else if (chart1.ChartAreas[0].AxisY2.Maximum < 2.0)
            {
                chart1.ChartAreas[0].AxisY2.MajorGrid.Interval = 0.1;
                chart1.ChartAreas[0].AxisY2.Interval = 0.1;
                chart1.ChartAreas[0].AxisY2.MajorTickMark.Interval = 0.1;
                chart1.ChartAreas[0].AxisY.Interval = 0.1;
            }
            else if (chart1.ChartAreas[0].AxisY2.Maximum < 5.0)
            {
                chart1.ChartAreas[0].AxisY2.MajorGrid.Interval = 0.2;
                chart1.ChartAreas[0].AxisY2.Interval = 0.2;
                chart1.ChartAreas[0].AxisY2.MajorTickMark.Interval = 0.2;
                chart1.ChartAreas[0].AxisY.Interval = 0.2;
            }
            else
            {
                chart1.ChartAreas[0].AxisY2.MajorGrid.Interval = 0.5;
                chart1.ChartAreas[0].AxisY2.Interval = 0.5;
                chart1.ChartAreas[0].AxisY2.MajorTickMark.Interval = 0.5;
                chart1.ChartAreas[0].AxisY.Interval = 0.5;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || keyData == Keys.Enter || keyData == Keys.Delete || keyData == Keys.Back)
            {
                // Handle key at form level.
                // Do not send event to focused control by returning true.
                // return true;
                
                Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Chart_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.m1Connection.Enabled = false;
            Program.m2Connection.Enabled = false;
            Program.OnTimerTick -= AddPoints;
            Program.GraphicEnabled = false;
            System.Windows.Forms.Cursor.Show();
            mainForm.Enabled = true;
        }
    }
}
