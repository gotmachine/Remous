using System;
using System.Windows.Forms;

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

        private static bool testingEnabled;
        private static bool graphicEnabled;

        public static bool TestingEnabled 
        { 
            get => testingEnabled; 
            set
            {
                if (value == true)
                {
                    timer.Interval = (int)(settings.GraphicInterval * 1000.0);
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
                    timer.Interval = (int)(settings.GraphicInterval * 1000.0);
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

            double m1Power = 0.0, m1Frequency = 0.0, m2Power = 0.0, m2frequency = 0.0;

            if (m1Connection.Enabled)
            {
                m1Connection.ProcessRawData(out m1Power, out m1Frequency);
            }

            if (m2Connection.Enabled)
            {
                m2Connection.ProcessRawData(out m2Power, out m2frequency);
            }

            if (OnTimerTick != null)
            {
                OnTimerTick(m1Power, m1Frequency, m2Power, m2frequency);
            }

            
        }
    }
}
