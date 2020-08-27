﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CornetEMVisu
{
    public partial class Chart : Form
    {
        public static int chartInterval = 500;
        public static int duration = 60;
        public static string serie1Name = "série 1";
        public static string serie2Name = "série 2";


        DataPointCollection serie1;
        DataPointCollection serie2;
        public Timer Timer { get; set; }
        public Random R { get; set; }

        public Chart(bool hideCursor = false)
        {
            
            InitializeComponent();

            chart1.ChartAreas[0].AxisX.ScaleView.Size = duration * 2;

            serie1 = chart1.Series["Series1"].Points;
            serie2 = chart1.Series["Series2"].Points;

            chart1.Series["Series1"].LegendText = serie1Name;
            chart1.Series["Series2"].LegendText = serie2Name;


            //chart1.Annotations["logo"]

            if (hideCursor)
            {
                System.Windows.Forms.Cursor.Hide();
            }
            


            //The next code simulates data changes every 500 ms
            Timer = new Timer
            {
                Interval = chartInterval
            };
            Timer.Tick += TimerOnTick;
            R = new Random();
            Timer.Start();



            //chart1.Series["Series1"].Color = Color.FromArgb(50, Color.Red);
            //chart1.Series["Series2"].Color = Color.FromArgb(50, Color.Blue);
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            int idx = serie1.Count - 1;
            if (idx == -1)
            {
                serie1.AddY(R.Next(0, 10) * 0.05);
                serie2.AddY(R.Next(0, 10) * 0.05);
            }
            else
            {
                double high = serie1[idx].YValues[0];
                if (high >= 4)
                {
                    high -= R.Next(0, 10) * 0.1;
                }
                else if (high <= 0.5)
                {
                    high += R.Next(0, 5) * 0.1;
                }
                else if (R.Next(1, 10) > 5)
                {
                    high += R.Next(0, 5) * 0.05;
                }
                else
                {
                    high -= R.Next(0, 5) * 0.05;
                }
                serie1.AddY(Math.Max(0.014, Math.Min(5, high)));


                double low = serie2[idx].YValues[0];
                if (low >= 0.8)
                {
                    low -= R.Next(0, 10) * 0.1;
                }
                else if (low <= 0.02)
                {
                    low += + R.Next(0, 10) * 0.001;
                }
                else if (R.Next(1, 10) > 5)
                {
                    low += R.Next(0, 3) * 0.05;
                }
                else
                {
                    low -= R.Next(0, 3) * 0.05;
                }

                serie2.AddY(Math.Max(0.014, Math.Min(1, low)));

            }



            if (serie1.Count > duration * 2)
            {
                serie1.RemoveAt(0);
                serie2.RemoveAt(0);


            }

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
            Timer.Stop();
            System.Windows.Forms.Cursor.Show();
        }
    }
}
