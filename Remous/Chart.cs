using System;
using System.Diagnostics;
using System.Drawing;
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

        CustomLabel clSbmLowText;
        CustomLabel clSbmLowTick;
        CustomLabel clSbmHighText;
        CustomLabel clSbmHighTick;
        CustomLabel clUELimit;
        CustomLabel cl6Limit;
        CustomLabel cl6LimitTick;
        CustomLabel cl12Limit;
        CustomLabel cl12LimitTick;
        CustomLabel cl20Limit;
        CustomLabel cl20LimitTick;

        FrequencySource s1LastSource = null;
        FrequencySource s2LastSource = null;
        int lastLabelDistance = 0;

        public Chart(mainForm mainForm, bool hideCursor = false, bool useCurves = false, bool isM2Enabled = true)
        {
            this.mainForm = mainForm;

            InitializeComponent();

            SetTextScale();

            NamedImage namedImage = new NamedImage("inonde_logo_noir_700px", global::Remous.Resources.inonde_logo_noir_700px);
            chart1.Images.Add(namedImage);
            ((ImageAnnotation)chart1.Annotations["logo"]).Image = "inonde_logo_noir_700px";

            maxPoints = Program.settings.GraphicDuration / Program.settings.GraphicIntervalWithMargin;
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
            AddPoint(s1, s1Points, Program.settings.M1Unit, Program.settings.M1Title, m1Power, m1Frequency, ref s1LastSource);
            AddPoint(s2, s2Points, Program.settings.M2Unit, Program.settings.M2Title, m2Power, m2Frequency, ref s2LastSource);

            if (s1Points.Count > maxPoints)
            {
                s1Points.RemoveAt(0);
                s2Points.RemoveAt(0);
            }

            lastLabelDistance++;

            AdjustGraphics();
        }

        private void AddPoint(Series serie, DataPointCollection points, string unit, string serieName, double power, double frequency, ref FrequencySource lastSource)
        {
            string legendText = serieName;
            FrequencySource source = null;
            Operator sourceOperator = null;

            if (power > 0.0)
            {
                if (unit != "V/m")
                    power = Math.Pow(power * 0.377, 0.5);

                legendText += $"\nIntensité : {power.ToString("0.000 V/m")}";

                if (frequency > 0.0)
                {
                    legendText += $"\nFréquence : {frequency.ToString("0.0 Mhz")}";

                    if (FrequencyAnalyzer.AnalyzeFrequency(frequency, out source, out sourceOperator))
                    {
                        legendText += $"\n{source.name}";

                        if (Program.settings.GraphicShowOperator && sourceOperator != null)
                        {
                            legendText += $" ({sourceOperator.name})";
                        }
                    }
                }

                serie.LegendText = legendText;

                int ptIdx = points.AddY(power);

                if (source != null)
                {
                    if (source != lastSource)
                    {
                        lastSource = source;

                        if (lastLabelDistance > 5)
                        {
                            lastLabelDistance = 0;

                            string label = "   \n"; // fix background being too small
                            label += source.name;
                            if (Program.settings.GraphicShowOperator && sourceOperator != null)
                            {
                                label += $"\n ({ sourceOperator.name})";
                            }
                            label += $"\n{frequency.ToString("0 Mhz")}";
                            label += "\n   "; // fix background being too small

                            points[ptIdx].Label = label;
                        }
                    }
                }
                else
                {
                    if (lastLabelDistance > 10)
                    {
                        lastSource = null;
                    }
                }
            }
            else
            {
                points.AddY(-10.0);
            }
        }

        private void AdjustGraphics()
        {
            // we want the minimum to always be 0.0
            chart1.ChartAreas[0].AxisY2.Minimum = -0.001;

            // sets the Maximum of the right axis to NaN for RecalculateAxesScale() to do its magic
            chart1.ChartAreas[0].AxisY2.Maximum = double.NaN; 
            chart1.ChartAreas[0].RecalculateAxesScale();

            chart1.ChartAreas[0].AxisY2.Maximum *= 1.15;

            // give us some space at the top so the legend and logo don't overlap
            if (chart1.ChartAreas[0].AxisY2.Maximum < 0.1)
            {
                chart1.ChartAreas[0].AxisY2.Maximum = 0.1;
            }

            // ensure that the left axis scale match the right axis
            chart1.ChartAreas[0].AxisY.Minimum = -0.001; 
            chart1.ChartAreas[0].AxisY.Maximum = chart1.ChartAreas[0].AxisY2.Maximum; // sets the Minimum to NaN

            // adjust the right axis intervals depending on the current scale
            // we don't use the automatic adjustement because it tend to change too often and to create very small intervals
            if (chart1.ChartAreas[0].AxisY2.Maximum < 0.2)
            {
                SetScaleInterval(0.01, "0.00 V/m");
            }
            else if (chart1.ChartAreas[0].AxisY2.Maximum < 0.5)
            {
                SetScaleInterval(0.02, "0.00 V/m");
            }
            else if (chart1.ChartAreas[0].AxisY2.Maximum < 1.0)
            {
                SetScaleInterval(0.05, "0.00 V/m");
            }
            else if (chart1.ChartAreas[0].AxisY2.Maximum < 2.0)
            {
                SetScaleInterval(0.1, "0.0 V/m", 0.05);
            }
            else if (chart1.ChartAreas[0].AxisY2.Maximum < 5.0)
            {
                SetScaleInterval(0.2, "0.0 V/m", 0.15);
            }
            else if (chart1.ChartAreas[0].AxisY2.Maximum < 10.0)
            {
                SetScaleInterval(0.5, "0.0 V/m", 0.25);
            }
            else
            {
                SetScaleInterval(1.0, "0.0 V/m", 0.5);
            }

            AdjustLabels(chart1.ChartAreas[0].AxisY.Maximum, chart1.ChartAreas[0].AxisY.Minimum);
        }

        private void AdjustLabels(double yMax, double yMin)
        {
            clSbmLowText = chart1.ChartAreas[0].AxisY.CustomLabels[0];
            clSbmLowTick = chart1.ChartAreas[0].AxisY.CustomLabels[1];
            clSbmHighText = chart1.ChartAreas[0].AxisY.CustomLabels[2];
            clSbmHighTick = chart1.ChartAreas[0].AxisY.CustomLabels[3];
            clUELimit = chart1.ChartAreas[0].AxisY.CustomLabels[4];
            cl6LimitTick = chart1.ChartAreas[0].AxisY.CustomLabels[5];
            cl6Limit = chart1.ChartAreas[0].AxisY.CustomLabels[6];
            cl12LimitTick = chart1.ChartAreas[0].AxisY.CustomLabels[7];
            cl12Limit = chart1.ChartAreas[0].AxisY.CustomLabels[8];
            cl20LimitTick = chart1.ChartAreas[0].AxisY.CustomLabels[9];
            cl20Limit = chart1.ChartAreas[0].AxisY.CustomLabels[10];

            if (yMax > 25.0)
            {
                clUELimit.FromPosition = yMin;
                clUELimit.ToPosition = yMax * 0.05 * (Program.settings.GraphicLabelTextScale * 0.01);
                clSbmLowTick.GridTicks = GridTickTypes.Gridline;
            }
            else if (yMax > 3.0)
            {
                clUELimit.FromPosition = 0.6;
                clUELimit.ToPosition = 0.6 + (yMax * 0.075 * (Program.settings.GraphicLabelTextScale * 0.01));
                clSbmLowTick.GridTicks = GridTickTypes.All;
            }
            else
            {
                clUELimit.FromPosition = 0.3;
                clUELimit.ToPosition = 0.9;
                clSbmLowTick.GridTicks = GridTickTypes.All;
            }

            if (yMax > 25.0)
            {
                clSbmLowText.Text = "";
                clSbmHighText.Text = "";
            }
            else if (yMax > 12.0 / (Program.settings.GraphicLabelTextScale * 0.01))
            {
                clSbmLowText.Text = "Baubio. idéal";
                clSbmHighText.Text = "Baubio. max";
            }
            else
            {
                clSbmHighText.Text = "Baubiologie\nMax";

                if (yMax > 6.0 / (Program.settings.GraphicLabelTextScale * 0.01))
                {
                    clSbmLowText.Text = "Idéal";
                }
                else
                {
                    clSbmLowText.Text = "Baubiologie\nIdéal";
                }
            }


        }

        private void SetScaleInterval(double interval, string format, double zeroNegativeOffset = 0.001)
        {
            chart1.ChartAreas[0].AxisY2.LabelStyle.Format = format;
            chart1.ChartAreas[0].AxisY2.MajorGrid.Interval = interval;
            chart1.ChartAreas[0].AxisY2.Interval = interval;
            chart1.ChartAreas[0].AxisY2.MajorTickMark.Interval = interval;
            chart1.ChartAreas[0].AxisY.Interval = interval;

            chart1.ChartAreas[0].AxisY2.Minimum = -zeroNegativeOffset;
            chart1.ChartAreas[0].AxisY.Minimum = -zeroNegativeOffset;
            chart1.ChartAreas[0].AxisY.CustomLabels[0].FromPosition = -zeroNegativeOffset;

            if (zeroNegativeOffset > 0.0)
            {
                chart1.ChartAreas[0].AxisY2.IntervalOffset = (interval * -2.0) + zeroNegativeOffset;
                
                chart1.ChartAreas[0].AxisY2.Maximum = ((Math.Ceiling(chart1.ChartAreas[0].AxisY2.Maximum / interval)) * interval) - 0.001;
                chart1.ChartAreas[0].AxisY.Maximum = chart1.ChartAreas[0].AxisY2.Maximum;
            }
            else
            {
                chart1.ChartAreas[0].AxisY2.IntervalOffset = 0.0;
            }
        }

        private void SetTextScale()
        {
            float titleFontSize = (14f * Program.settings.GraphicTitleTextScale * 0.01f) / Program.dpiScale;
            Font titleFont = new Font(this.Font.FontFamily, titleFontSize, FontStyle.Bold);

            foreach (Legend legend in chart1.Legends)
            {
                legend.Font = titleFont;
            }

            float labelFontSize = (12f * Program.settings.GraphicLabelTextScale * 0.01f) / Program.dpiScale;
            Font labelFont = new Font(this.Font.FontFamily, labelFontSize);

            chart1.ChartAreas[0].AxisY.LabelStyle.Font = labelFont;
            chart1.ChartAreas[0].AxisY2.LabelStyle.Font = labelFont;

            foreach (Series series in chart1.Series)
            {
                series.Font = labelFont;
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
            Program.m1Connection.Disconnect();
            Program.m2Connection.Disconnect();
            Program.OnTimerTick -= AddPoints;
            Program.GraphicEnabled = false;
            System.Windows.Forms.Cursor.Show();
            //mainForm.Enabled = true;
            //mainForm.SetInteractable(true);
            mainForm.Visible = true;
        }
    }
}
