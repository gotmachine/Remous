using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Windows;

namespace Remous
{
    public class Connection
    {
        private static Random R { get; set; } = new Random();

        public object comLock = new object();
        private int testMode = 0;
        private bool isValid;
        public bool IsValid => isValid;
        private SerialPort comPort;
        private List<string> rawDataBuffer = new List<string>();

        private double lastTestPower;
        

        private bool enabled;
        public bool Enabled 
        { 
            get => enabled && (isValid || testMode > 0);
            set
            {
                if (testMode > 0)
                {
                    enabled = value;
                }

                if (isValid)
                {
                    try
                    {
                        comPort.Open();
                        comPort.DataReceived += OnDataReceived;
                        enabled = value;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Erreur à l'ouverture du port {comPort.PortName}\n{e.Message}");
                    }
                }
            }
        }

        public bool Connect(string portName, bool showErrors = true)
        {
            isValid = false;
            testMode = 0;

            if (string.IsNullOrEmpty(portName))
            {
                if (showErrors) MessageBox.Show($"Le port COM ne peut être vide");
                return false;
            }

            if (portName == "Données de test m1")
            {
                testMode = 1;
                return true;
            }

            if (portName == "Données de test m2")
            {
                testMode = 2;
                return true;
            }

            comPort = new SerialPort();
            comPort.PortName = portName;
            comPort.BaudRate = Program.settings.SerialBaudRate;
            comPort.Parity = Program.settings.SerialParity;
            comPort.DataBits = Program.settings.SerialDataBits;
            comPort.StopBits = Program.settings.SerialStopBits;
            comPort.Handshake = Handshake.None;
            comPort.ReadTimeout = 500;
            comPort.WriteTimeout = 500;

            try
            {
                comPort.Open();
            }
            catch (Exception e)
            {
                if (showErrors) MessageBox.Show($"Erreur à l'ouverture du port {comPort.PortName}\n{e.Message}");
                return false;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.Elapsed < TimeSpan.FromSeconds(2.0))
            {
                try
                {
                    rawDataBuffer.Add(comPort.ReadLine());
                    if (ProcessRawData(out double power, out double frequency))
                    {
                        comPort.Close();
                        isValid = true;
                        return true;
                    }
                }
                catch (TimeoutException)
                {
                    if (showErrors) MessageBox.Show($"Mesureur non trouvé sur le port {comPort.PortName}");
                    comPort.Close();
                    return false;
                }
            }

            comPort.Close();
            return false;
        }



        public bool ProcessRawData(out double power, out double frequency)
        {
            if (testMode > 0)
            {
                GenerateRandomData(out power, out frequency);
                return true;
            }

            string[] data;
            lock (comLock)
            {
                data = rawDataBuffer.ToArray();
            }

            power = 0.0;
            frequency = 0.0;

            if (data.Length == 0)
                return false;

            int powerCount = 0;
            for (int i = 0; i < data.Length; i++)
            {
                string line = data[i];
                int sepIdx = line.IndexOf(',');

                if (sepIdx == -1)
                {
                    continue;
                }

                try
                {
                    power += double.Parse(line.Substring(0, sepIdx));
                    powerCount++;
                    if (frequency == 0.0)
                    {
                        frequency = double.Parse(line.Substring(sepIdx + 1));
                    }
                }
                catch
                {
                    continue;
                }
            }

            if (power == 0.0)
                return false;

            power /= powerCount;

            return true;
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (comLock)
                {
                    rawDataBuffer.Add(comPort.ReadLine());
                }
            }
            catch
            {
            }
        }

        private void GenerateRandomData(out double power, out double frequency)
        {
            power = lastTestPower;

            if (testMode == 1)
            {
                if (power >= 4)
                {
                    power -= R.Next(0, 10) * 0.1;
                }
                else if (power <= 0.5)
                {
                    power += R.Next(0, 5) * 0.1;
                }
                else if (R.Next(1, 10) > 5)
                {
                    power += R.Next(0, 5) * 0.05;
                }
                else
                {
                    power -= R.Next(0, 5) * 0.05;
                }
                power = Math.Max(0.014, Math.Min(5, power));
            }
            else
            {
                if (power >= 0.8)
                {
                    power -= R.Next(0, 10) * 0.1;
                }
                else if (power <= 0.02)
                {
                    power += +R.Next(0, 10) * 0.001;
                }
                else if (R.Next(1, 10) > 5)
                {
                    power += R.Next(0, 3) * 0.05;
                }
                else
                {
                    power -= R.Next(0, 3) * 0.05;
                }

                power = Math.Max(0.014, Math.Min(1, power));
            }

            lastTestPower = power;

            frequency = R.Next(750, 2500);
        }

    }
}
