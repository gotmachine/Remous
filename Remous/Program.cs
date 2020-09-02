using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Linq;

namespace Remous
{
    static class Program
    {
        public static Settings settings = new Settings();

        private static System.Windows.Forms.Timer timer;
        public static Connection m1Connection = new Connection();
        public static Connection m2Connection = new Connection();

        public delegate void TickHandler(double m1Power, double m1Frequency, double m2Power, double m2frequency);
        public static event TickHandler OnTimerTick;

        public static bool logData;
        public static bool logErrors;

        private static bool testingEnabled;
        private static bool graphicEnabled;

        public static float dpiScale;

        public static bool TestingEnabled 
        { 
            get => testingEnabled; 
            set
            {
                if (value == true)
                {
                    timer.Interval = (int)(settings.GraphicIntervalWithMargin * 1000.0);
                    timer.Start();
                }
                else
                {
                    timer.Stop();
                }

                testingEnabled = value; 
            }
        }
        public static bool GraphicEnabled
        {
            get => graphicEnabled;
            set
            {
                if (value == true)
                {
                    timer.Interval = (int)(settings.GraphicIntervalWithMargin * 1000.0);
                    timer.Start();
                }
                else
                {
                    timer.Stop();
                }

                graphicEnabled = value;
            }
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            timer = new System.Windows.Forms.Timer();
            timer.Tick += OnTimerElapsed;

            Application.Run(new mainForm());
        }

        private static void OnTimerElapsed(object sender, EventArgs e)
        {
            if (!m1Connection.Enabled && !m2Connection.Enabled)
                return;

            if (!TestingEnabled && !GraphicEnabled)
                return;

            double m1Power = 0.0, m1Frequency = 0.0, m2Power = 0.0, m2Frequency = 0.0;

            if (m1Connection.tryToReconnect)
            {
                if (SerialPort.GetPortNames().Contains(settings.M1COMPort))
                {
                    if (!m1Connection.Connect(settings.M1COMPort, false))
                    {
                        Logger.Log($"{settings.M1COMPort} : echec de la reconnexion");
                    }
                    else
                    {
                        Logger.Log($"{settings.M1COMPort} : reconnecté !");
                        m1Connection.tryToReconnect = false;
                    }
                }
            }

            if (m1Connection.Enabled)
            {
                if (!m1Connection.IsReceiving())
                {
                    Logger.Log($"{settings.M1COMPort} : connexion perdue, tentative de reconnexion...");
                    m1Connection.tryToReconnect = true;
                }

                m1Connection.ProcessRawData(out m1Power, out m1Frequency);
                if (logData)
                {
                    if (FrequencyAnalyzer.AnalyzeFrequency(m1Frequency, out FrequencySource source, out Operator sourceOperator))
                    {
                        Logger.Log($";{settings.M1COMPort};{settings.M1Title};{m1Power.ToString("0.000000")};{settings.M1Unit};{m1Frequency.ToString("0.0")};Mhz;{source.name};{(sourceOperator != null ? sourceOperator.name : "")}");
                    }
                    else
                    {
                        Logger.Log($";{settings.M1COMPort};{settings.M1Title};{m1Power.ToString("0.000000")};{settings.M1Unit};{m1Frequency.ToString("0.0")};Mhz;;");
                    }
                }
            }

            if (m2Connection.tryToReconnect)
            {
                if (SerialPort.GetPortNames().Contains(settings.M2COMPort))
                {
                    if (!m2Connection.Connect(settings.M2COMPort, false))
                    {
                        Logger.Log($"{settings.M2COMPort} : echec de la reconnexion");
                    }
                    else
                    {
                        Logger.Log($"{settings.M2COMPort} : reconnecté !");
                        m2Connection.tryToReconnect = false;
                    }
                }
            }

            if (m2Connection.Enabled)
            {
                if (!m2Connection.IsReceiving())
                {
                    Logger.Log($"{settings.M2COMPort} : connexion perdue, tentative de reconnexion...");
                    m2Connection.tryToReconnect = true;
                }

                m2Connection.ProcessRawData(out m2Power, out m2Frequency);
                if (logData)
                {
                    if (FrequencyAnalyzer.AnalyzeFrequency(m1Frequency, out FrequencySource source, out Operator sourceOperator))
                    {
                        Logger.Log($";{settings.M2COMPort};{settings.M2Title};{m2Power.ToString("0.000000")};{settings.M2Unit};{m2Frequency.ToString("0.0")};Mhz;{source.name};{(sourceOperator != null ? sourceOperator.name : "")}");
                    }
                    else
                    {
                        Logger.Log($";{settings.M2COMPort};{settings.M2Title};{m2Power.ToString("0.000000")};{settings.M2Unit};{m2Frequency.ToString("0.0")};Mhz;;");
                    }
                }
            }

            if (OnTimerTick != null)
            {
                OnTimerTick(m1Power, m1Frequency, m2Power, m2Frequency);
            }

            Logger.WriteLog();
        }
    }
}
