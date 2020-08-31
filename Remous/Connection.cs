using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Windows;

namespace Remous
{
    public class Connection
    {
        private static Random R { get; set; } = new Random();

        public object comLock = new object();
        private int testMode = 0;
        private SerialPort comPort;
        private List<string> rawDataBuffer = new List<string>();
        private DateTime lastReception;

        public bool tryToReconnect;

        private double lastTestPower;
        
        public bool Enabled { get; private set; }

        public void Disconnect()
        {
            tryToReconnect = false;

            if (testMode > 0)
            {
                Enabled = false;
            }

            if (comPort != null)
            {
                comPort.DataReceived -= OnDataReceived;

                if (comPort.IsOpen)
                {
                    comPort.Close();
                    Stopwatch closeWatch = new Stopwatch();
                    closeWatch.Start();

                    while (closeWatch.Elapsed < TimeSpan.FromSeconds(5.0))
                    {
                        Thread.Sleep(10);
                        if (!comPort.IsOpen) break;
                    }

                    closeWatch.Stop();
                }

                comPort.Dispose();
                comPort = null;
            }

            
            Enabled = false;
        }

        public bool Connect(string portName, bool showErrors = true)
        {
            tryToReconnect = false;
            testMode = 0;

            if (string.IsNullOrEmpty(portName))
            {
                if (showErrors) MessageBox.Show($"Le port COM ne peut être vide");
                return false;
            }

            if (portName == "Données de test m1")
            {
                Enabled = true;
                testMode = 1;
                return true;
            }

            if (portName == "Données de test m2")
            {
                Enabled = true;
                testMode = 2;
                return true;
            }

            Disconnect();

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
                if (Program.logErrors)
                {
                    Logger.Log($"Erreur à l'ouverture du port {comPort.PortName}\n{e.Message}");
                    Logger.WriteLog();
                }

                if (showErrors) MessageBox.Show($"Erreur à l'ouverture du port {comPort.PortName}\n{e.Message}");
                return false;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.Elapsed < TimeSpan.FromSeconds(5.0))
            {
                Thread.Sleep(50);
                try
                {
                    rawDataBuffer.Add(comPort.ReadLine());
                    if (ProcessRawData(out double power, out double frequency))
                    {
                        lastReception = DateTime.Now;
                        comPort.DataReceived += OnDataReceived;
                        Enabled = true;
                        return true;
                    }
                }
                catch (TimeoutException)
                {
                    if (Program.logErrors)
                    {
                        Logger.Log($"Mesureur non trouvé sur le port {comPort.PortName}");
                        Logger.WriteLog();
                    }

                    if (showErrors) MessageBox.Show($"Mesureur non trouvé sur le port {comPort.PortName}");
                    comPort.Close();
                    return false;
                }
            }

            comPort.Close();
            return false;
        }

        public bool IsReceiving()
        {
            if (testMode > 0)
            {
                return true;
            }

            if (!comPort.IsOpen)
            {
                Logger.Log($"{Program.settings.M2COMPort} : connexion perdue, le port COM est fermé");
                return false;
            }

            if ((DateTime.Now - lastReception).TotalSeconds > 5.0)
            {
                Logger.Log($"{Program.settings.M2COMPort} : connexion perdue (timeout > 5s)");
                return false;
            }

            return true;
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
                rawDataBuffer.Clear();
            }

            power = 0.0;
            frequency = 0.0;

            if (data.Length == 0)
                return false;

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
                    double newPower = double.Parse(line.Substring(0, sepIdx));
                    if (newPower > power)
                    {
                        power = newPower;
                    }

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

            return true;
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (comLock)
                {
                    rawDataBuffer.Add(comPort.ReadLine());
                    lastReception = DateTime.Now;
                }
            }
            catch (Exception exc)
            {
                if (Program.logErrors)
                {
                    Logger.Log($"Erreur lecture port COM {comPort.PortName}\n{exc.Message}");
                }
            }
        }

        private void GenerateRandomData(out double power, out double frequency)
        {
            power = lastTestPower;
            const double max = 15.0;
            const double varfactor = 10.0;

            if (testMode == 1)
            {
                if (power >= max)
                {
                    power -= R.Next(0, 10) * 0.1 * varfactor;
                }
                else if (power <= 0.5)
                {
                    power += R.Next(0, 5) * 0.1 * varfactor;
                }
                else if (R.Next(1, 10) > 5)
                {
                    power += R.Next(0, 5) * 0.05;
                }
                else
                {
                    power -= R.Next(0, 5) * 0.05;
                }
                power = Math.Max(0.014, Math.Min(max, power));
                power = 7;
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
