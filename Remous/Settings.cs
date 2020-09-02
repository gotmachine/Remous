using System.IO.Ports;

namespace Remous
{
    public class Settings
    {
        public string M1COMPort { get; set; }
        public string M1Title { get; set; }
        public string M1Unit { get; set; }
        public string M2COMPort { get; set; }
        public string M2Title { get; set; }
        public string M2Unit { get; set; }
        public double GraphicInterval { get; set; }
        public double GraphicIntervalWithMargin => GraphicInterval + 0.15;
        public int GraphicDuration { get; set; }
        public string GraphicMode { get; set; }
        public bool GraphicShowOperator { get; set; }
        public float GraphicTitleTextScale { get; set; }
        public float GraphicLabelTextScale { get; set; }

        public int SerialBaudRate { get; set; } = 9600;
        public Parity SerialParity { get; set; } = Parity.None;
        public int SerialDataBits { get; set; } = 8;
        public StopBits SerialStopBits { get; set; } = StopBits.One;
    }
}
