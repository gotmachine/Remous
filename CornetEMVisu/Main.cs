using CornetEMVisu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static SerialPort comPort;
        static bool isRecording = false;
        static List<string> buffer = new List<string>();
        static string tickData = string.Empty;
        static bool test = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SearchComPorts();
        }

        private void btnSearchComPorts_Click(object sender, EventArgs e)
        {
            SearchComPorts();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            StopComPort();

            if (isRecording)
            {
                isRecording = false;
                BtnStart.Text = "Start";
                comPort = null;
                timer.Enabled = false;
                return;
            }

            string portName;

            if (comboBox1.SelectedItem != null)
                portName = (string)comboBox1.SelectedItem;
            else
                portName = comboBox1.Text;

            comPort = new SerialPort();
            comPort.PortName = portName;
            comPort.BaudRate = 9600;
            comPort.Parity = Parity.None;
            comPort.DataBits = 8;
            comPort.StopBits = StopBits.One;
            comPort.Handshake = Handshake.None;
            comPort.ReadTimeout = 500;
            comPort.WriteTimeout = 500;

            if (!IsCornetFound(comPort))
                return;

            isRecording = true;
            BtnStart.Text = "Stop";

            
            comPort.Open();
            comPort.DataReceived += ComPortDataReceived;
            timer.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            test = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            test = false;
        }

        private void SearchComPorts()
        {
            string[] ports = SerialPort.GetPortNames();

            if (ports.Length == 0)
            {
                MessageBox.Show("Aucun port COM trouvé");
                return;
            }

            comboBox1.Items.Clear();

            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }

            comboBox1.SelectedIndex = 0;
            BtnStart.Enabled = true;
        }

        private static bool IsCornetFound(SerialPort comPort)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                comPort.Open();
            }
            catch
            {
                MessageBox.Show($"Erreur à l'ouverture du port {comPort.PortName}");
                return false;
            }
            
            while (stopwatch.Elapsed < TimeSpan.FromSeconds(10.0))
            {
                try
                {
                    if (comPort.ReadLine().Split(',')[0].Length > 0)
                    {
                        comPort.Close();
                        return true;
                    }
                }
                catch (TimeoutException)
                {
                }
            }
            comPort.Close();
            MessageBox.Show($"Cornet non trouvé sur le port {comPort.PortName}");
            return false;
        }

        private static void StopComPort()
        {
            try
            {
                if (comPort != null && comPort.IsOpen)
                {
                    comPort.DataReceived -= ComPortDataReceived;
                    comPort.Close();
                }
            }
            catch (Exception)
            {

            }
        }

        private static void ComPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (buffer)
                {
                    buffer.Add(comPort.ReadLine());
                }
            }
            catch
            {
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (test)
            {
                Random rnd = new Random();
                buffer.Add($"{rnd.Next()},{rnd.Next()}\r");
            }

            if (buffer.Count == 0)
            {
                return;
            }
            lock (buffer)
            {
                foreach (string recvData in buffer)
                {
                    tickData += recvData;
                }
                buffer.Clear();
            }
            int start;
            while (true)
            {
                if (tickData.IndexOf("\r") == -1)
                {
                    tickData = "";
                    return;
                }
                start = 0;
                int end = tickData.IndexOf("\r");
                if (start != -1 || end != -1)
                {
                    if (start == -1)
                    {

                        ProcessDataLine(tickData);
                        return;
                    }
                    if (end == -1)
                    {
                        break;
                    }
                    if (start < end)
                    {
                        tickData = tickData.Substring(start, end);
                        ProcessDataLine(tickData);
                    }
                    continue;
                }
                return;
            }
            tickData = tickData.Substring(start);
        }

        private void ProcessDataLine(string line)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    textOutput.AppendText("RAW DATA : '" + line + "'" + Environment.NewLine);
                }


                string[] array = line.Split(',');
                double rf;
                double rfFrequency;
                if (array.Length == 2)
                {
                    rf = double.Parse(array[0].Trim());
                    rfFrequency = double.Parse(array[1].Trim());
                }
                else
                {
                    rf = double.Parse(array[4].Trim());
                    rfFrequency = double.Parse(array[7].Trim());
                }

                textOutput.AppendText("RF : " + rf.ToString("0.0000000 mW/m²") + " / " + Math.Pow(rf * 0.377, 0.5).ToString("0.0000000 V/m") + Environment.NewLine);

                //foreach (DataPlotterBase plotter in Program.plotters)
                //{
                //    plotter.AddPoint(rf);
                //}
            }
            catch
            {

            }
        }

        public void SetFullScreenForm(Form form)
        {
            // This is required if the form reaches this code in maximized state
            // otherwise the TaskBar remains on top of the form
            form.WindowState = FormWindowState.Normal;

            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            //form.mainPanel.Dock = DockStyle.Fill;
            form.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SetChartParams();
            Chart chart = new Chart();
            chart.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetChartParams();
            Chart chart = new Chart(true);
            //Program.plotters.Add(plot.barPlot);
            chart.AutoSize = true;
            chart.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            chart.Show();
            SetFullScreenForm(chart);
        }

        private void SetChartParams()
        {
            if (!int.TryParse(textBox1.Text, out int interval))
            {
                textBox1.Text = "500";
                interval = 500;
            }

            if (!int.TryParse(textBox2.Text, out int duration))
            {
                textBox2.Text = "60";
                duration = 60;
            }

            Chart.chartInterval = interval;
            Chart.duration = duration;



        }
    }
}
