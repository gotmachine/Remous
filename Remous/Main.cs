﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Remous
{
    public partial class mainForm : Form
    {
        private bool m1TestingEnabled;
        private bool m2TestingEnabled;

        public mainForm()
        {
            InitializeComponent();

            m1ComboBoxUnit.SelectedIndex = 0;
            m2ComboBoxUnit.SelectedIndex = 0;
            graphicModeComboBox.SelectedIndex = 0;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            try
            {
                Program.dpiScale = g.DpiY / 96f;
            }
            catch
            {
                Program.dpiScale = 1f;
            }
            finally
            {
                g.Dispose();
            }

            Debug.WriteLine($"DPI : {Program.dpiScale}");


            SearchComPorts();

            if (File.Exists("RemousSettings.xml"))
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Settings));
                    using (FileStream fs = File.OpenRead("RemousSettings.xml"))
                    {
                        Program.settings = (Settings)ser.Deserialize(fs);
                    }
                }
                catch
                {

                }

                SelectTextInCombo(m1ComboBoxCOM, Program.settings.M1COMPort);
                m1TextBoxTitle.Text = Program.settings.M1Title;
                SelectTextInCombo(m1ComboBoxUnit, Program.settings.M1Unit);

                SelectTextInCombo(m2ComboBoxCOM, Program.settings.M2COMPort);
                m2TextBoxTitle.Text = Program.settings.M2Title;
                SelectTextInCombo(m2ComboBoxUnit, Program.settings.M2Unit);

                try
                {
                    graphicIntervalNumericUpDown.Value = (decimal)Program.settings.GraphicInterval;
                    graphicDurationNumericUpDown.Value = Program.settings.GraphicDuration;
                    graphicTitleTextScaleNumericUpDown.Value = (decimal)Program.settings.GraphicTitleTextScale;
                    graphicLabelTextScaleNumericUpDown.Value = (decimal)Program.settings.GraphicLabelTextScale;
                    SelectTextInCombo(graphicModeComboBox, Program.settings.GraphicMode);
                    graphicShowOperatorCheckBox.Checked = Program.settings.GraphicShowOperator;
                }
                catch
                {

                }
            }

            SetSettings();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("RemousSettings.xml"))
            {
                SetSettings();
                XmlSerializer ser = new XmlSerializer(typeof(Settings));
                ser.Serialize(sw, Program.settings);
            }
        }

        private void SetSettings()
        {
            Program.settings.M1COMPort = m1ComboBoxCOM.Text;
            Program.settings.M1Title = m1TextBoxTitle.Text;
            Program.settings.M1Unit = m1ComboBoxUnit.Text;

            Program.settings.M2COMPort = m2ComboBoxCOM.Text;
            Program.settings.M2Title = m2TextBoxTitle.Text;
            Program.settings.M2Unit = m2ComboBoxUnit.Text;

            Program.settings.GraphicInterval = (double)graphicIntervalNumericUpDown.Value;
            Program.settings.GraphicDuration = (int)graphicDurationNumericUpDown.Value;
            Program.settings.GraphicTitleTextScale = (float)graphicTitleTextScaleNumericUpDown.Value;
            Program.settings.GraphicLabelTextScale = (float)graphicLabelTextScaleNumericUpDown.Value;
            Program.settings.GraphicMode = graphicModeComboBox.Text;
            Program.settings.GraphicShowOperator = graphicShowOperatorCheckBox.Checked;
        }

        private void SelectTextInCombo(ComboBox combo, string textToSelect)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                string item = (string)combo.Items[i];
                if (item == textToSelect)
                {
                    combo.SelectedIndex = i;
                    continue;
                }
            }
        }

        private void btnSearchComPorts_Click(object sender, EventArgs e)
        {
            SearchComPorts();
        }

        private void SearchComPorts()
        {
            m1ComboBoxCOM.Items.Clear();
            m2ComboBoxCOM.Items.Clear();

            string[] ports = SerialPort.GetPortNames();

            if (ports.Length == 0)
            {
                MessageBox.Show("Aucun port COM trouvé");
                m1ComboBoxCOM.Items.Add("test1");
                m1ComboBoxCOM.Items.Add("test2");
                m2ComboBoxCOM.Items.Add("test1");
                m2ComboBoxCOM.Items.Add("test2");
                return;
            }

            for (int i = 0; i < ports.Length; i++)
            {
                m1ComboBoxCOM.Items.Add(ports[i]);
                m2ComboBoxCOM.Items.Add(ports[i]);

                if (i == 0)
                {
                    m1ComboBoxCOM.SelectedIndex = 0;
                    m2ComboBoxCOM.SelectedIndex = 0;
                }

                if (i == 1)
                {
                    m2ComboBoxCOM.SelectedIndex = 1;
                }
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            ClearM1Tests();
            ClearM2Tests();
            StartChart(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearM1Tests();
            ClearM2Tests();
            StartChart(true);
        }

        private void ClearM1Tests()
        {
            m1TextBoxPower.Text = "";
            m1TextBoxFrequency.Text = "";
            m1TextBoxFrequencySource.Text = "";
        }

        private void ClearM2Tests()
        {
            m2TextBoxPower.Text = "";
            m2TextBoxFrequency.Text = "";
            m2TextBoxFrequencySource.Text = "";
        }

        private void StartChart(bool fullscreen)
        {
            if (m1TestingEnabled)
                m1ButtonTest_Click(null, null);

            if (m2TestingEnabled)
                m2ButtonTest_Click(null, null);

            Program.settings.M1COMPort = m1ComboBoxCOM.Text;
            Program.settings.M2COMPort = m2ComboBoxCOM.Text;

            if (Program.settings.M1COMPort == Program.settings.M2COMPort)
            {
                MessageBox.Show("Le mesureur n°1 ne peut pas être le même que le mesureur N°2\nlaissez le champ COM vide pour le mesureur N°2, ou selectionnez un autre port COM");
                return;
            }

            Program.GraphicEnabled = true;

            Program.m1Connection.Connect(m1ComboBoxCOM.Text);
            Program.m2Connection.Connect(m2ComboBoxCOM.Text, false);

            bool useCurve = graphicModeComboBox.Text == "Barres" ? false : true;

            Chart chart = new Chart(this, fullscreen, useCurve, Program.m2Connection.Enabled);

            if (fullscreen)
            {
                chart.AutoSize = true;
                chart.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            }

            chart.Show();

            if (fullscreen)
            {
                SetFullScreenForm(chart);
            }

            Visible = false;

            //this.Enabled = false;
            //SetInteractable(false);
        }

        public void SetInteractable(bool interactable)
        {
            foreach (Control child in Controls)
            {
                child.Enabled = interactable;
            }
        }

        private void SetFullScreenForm(Form form)
        {
            // This is required if the form reaches this code in maximized state
            // otherwise the TaskBar remains on top of the form
            form.WindowState = FormWindowState.Normal;

            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            //form.mainPanel.Dock = DockStyle.Fill;
            form.BringToFront();
        }

        private void graphicIntervalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Program.settings.GraphicInterval = (double)graphicIntervalNumericUpDown.Value;
        }

        private void graphicDurationNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Program.settings.GraphicDuration = (int)graphicDurationNumericUpDown.Value;
        }

        private void graphicTextScaleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Program.settings.GraphicTitleTextScale = (float)graphicTitleTextScaleNumericUpDown.Value;
        }

        private void graphicLabelTextScaleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Program.settings.GraphicLabelTextScale = (float)graphicLabelTextScaleNumericUpDown.Value;
        }

        private void graphicModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.settings.GraphicMode = graphicModeComboBox.Text;
        }

        private void m1TextBoxTitle_TextChanged(object sender, EventArgs e)
        {
            Program.settings.M1Title = m1TextBoxTitle.Text;
        }

        private void m2TextBoxTitle_TextChanged(object sender, EventArgs e)
        {
            Program.settings.M2Title = m2TextBoxTitle.Text;
        }

        private void m1ButtonTest_Click(object sender, EventArgs e)
        {
            if (m1TestingEnabled)
            {
                m1ButtonTest.Text = "Tester la connexion";
                Program.m1Connection.Disconnect();
                ClearM1Tests();
                if (!m2TestingEnabled)
                {
                    StopTesting();
                }
            }
            else
            {
                if (!Program.m1Connection.Connect(m1ComboBoxCOM.Text))
                    return;

                m1ButtonTest.Text = "Mesures en cours...";
                if (!m2TestingEnabled)
                {
                    StartTesting();
                }
            }

            m1TestingEnabled = !m1TestingEnabled;
        }

        private void m2ButtonTest_Click(object sender, EventArgs e)
        {
            if (m2TestingEnabled)
            {
                m2ButtonTest.Text = "Tester la connexion";
                Program.m2Connection.Disconnect();
                ClearM2Tests();
                if (!m1TestingEnabled)
                {
                    StopTesting();
                }
            }
            else
            {
                if (!Program.m2Connection.Connect(m2ComboBoxCOM.Text))
                    return;

                m2ButtonTest.Text = "Mesures en cours...";
                if (!m1TestingEnabled)
                {
                    StartTesting();
                }
            }

            m2TestingEnabled = !m2TestingEnabled;
        }

        private void StopTesting()
        {
            Program.TestingEnabled = false;
            Program.OnTimerTick -= OnTimerTick;
        }

        private void StartTesting()
        {
            Program.TestingEnabled = true;
            Program.OnTimerTick += OnTimerTick;
        }

        private void OnTimerTick(double m1Power, double m1Frequency, double m2Power, double m2Frequency)
        {
            if (m1TestingEnabled)
            {
                if (Program.settings.M1Unit == "V/m")
                    m1TextBoxPower.Text = m1Power.ToString("0.000 V/m");
                else
                    m1TextBoxPower.Text = m1Power.ToString("0.000000 mW/m²");

                m1TextBoxFrequency.Text = m1Frequency.ToString("0.0 Mhz");

                if (FrequencyAnalyzer.AnalyzeFrequency(m1Frequency, out FrequencySource source, out Operator sourceOperator))
                {
                    if (sourceOperator == null)
                        m1TextBoxFrequencySource.Text = source.name;
                    else
                        m1TextBoxFrequencySource.Text = $"{source.name} ({sourceOperator.name})";
                }
                else
                {
                    m1TextBoxFrequencySource.Text = "";
                }
            }

            if (m2TestingEnabled)
            {
                if (Program.settings.M2Unit == "V/m")
                    m2TextBoxPower.Text = m2Power.ToString("0.000 V/m");
                else
                    m2TextBoxPower.Text = m2Power.ToString("0.000000 mW/m²");

                m2TextBoxFrequency.Text = m2Frequency.ToString("0.0 Mhz");

                if (FrequencyAnalyzer.AnalyzeFrequency(m2Frequency, out FrequencySource source, out Operator sourceOperator))
                {
                    if (sourceOperator == null)
                        m2TextBoxFrequencySource.Text = source.name;
                    else
                        m2TextBoxFrequencySource.Text = $"{source.name} ({sourceOperator.name})";
                }
                else
                {
                    m1TextBoxFrequencySource.Text = "";
                }
            }
        }

        private void m1ComboBoxUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.settings.M1Unit = m1ComboBoxUnit.Text;
        }

        private void m2ComboBoxUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.settings.M2Unit = m2ComboBoxUnit.Text;
        }

        private void mainForm_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void checkBoxSaveData_CheckedChanged(object sender, EventArgs e)
        {
            Program.logData = checkBoxSaveData.Checked;
        }

        private void checkBoxSaveErrors_CheckedChanged(object sender, EventArgs e)
        {
            Program.logErrors = checkBoxSaveErrors.Checked;
        }

        private void graphicShowOperatorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.settings.GraphicShowOperator = graphicShowOperatorCheckBox.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Show the device manager.
            Process.Start("devmgmt.msc");
        }


    }
}
