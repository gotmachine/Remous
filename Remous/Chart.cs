using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Remous
{
    public partial class Chart : Form
    {
        DataPointCollection serie1;
        DataPointCollection serie2;
        private double maxPoints;

        public Chart(bool hideCursor = false, bool useCurves = false, bool isM2Enabled = true)
        {
            InitializeComponent();

            maxPoints = Program.settings.GraphicDuration / Program.settings.GraphicInterval;
            chart1.ChartAreas[0].AxisX.ScaleView.Size = maxPoints;

            serie1 = chart1.Series["Series1"].Points;
            serie2 = chart1.Series["Series2"].Points;

            chart1.Series["Series1"].LegendText = Program.settings.M1Title;
            chart1.Series["Series2"].LegendText = Program.settings.M2Title;

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

        public void AddPoints(double m1Power, double m1Frequency, double m2Power, double m2frequency)
        {
            if (m1Power > 0.0)
            {
                if (Program.settings.M1Unit != "V/m")
                    m1Power = Math.Pow(m1Power * 0.377, 0.5);

                serie1.AddY(m1Power);
            }
            else
            {
                serie1.AddY(-10.0);
            }

            if (m2Power > 0.0)
            {
                if (Program.settings.M2Unit != "V/m")
                    m2Power = Math.Pow(m2Power * 0.377, 0.5);

                serie2.AddY(m2Power);
            }
            else
            {
                serie2.AddY(-10.0);
            }

            if (serie1.Count > maxPoints)
            {
                serie1.RemoveAt(0);
                serie2.RemoveAt(0);
            }

            AdjustGraphics();
        }

        private void AdjustGraphics()
        {
            chart1.ChartAreas[0].AxisY2.Minimum = 0.0; // sets the Minimum to NaN
            chart1.ChartAreas[0].AxisY2.Maximum = double.NaN; // sets the Minimum to NaN
            chart1.ChartAreas[0].RecalculateAxesScale(); // recalculates the Maximum and Minimum values, since they are set to NaN
            chart1.ChartAreas[0].AxisY.Minimum = 0.0; // sets the Minimum to NaN
            chart1.ChartAreas[0].AxisY.Maximum = chart1.ChartAreas[0].AxisY2.Maximum; // sets the Minimum to NaN

            if (chart1.ChartAreas[0].AxisY.Maximum > 0.75)
            {
                chart1.ChartAreas[0].AxisY.CustomLabels[4].ToPosition = chart1.ChartAreas[0].AxisY.Maximum;
            }
            else
            {
                chart1.ChartAreas[0].AxisY.CustomLabels[4].ToPosition = 10.0;
            }


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
        }
    }
}
