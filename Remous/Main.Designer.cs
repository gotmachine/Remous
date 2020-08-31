namespace Remous
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.btnSearchComPorts = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.m1GroupBox = new System.Windows.Forms.GroupBox();
            this.m1TextBoxFrequencySource = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.m1TextBoxFrequency = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.m1ComboBoxUnit = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m1TextBoxPower = new System.Windows.Forms.TextBox();
            this.m1ButtonTest = new System.Windows.Forms.Button();
            this.m1TextBoxTitle = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m1ComboBoxCOM = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m2TextBoxFrequencySource = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m2TextBoxFrequency = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.m2ComboBoxUnit = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.m2TextBoxPower = new System.Windows.Forms.TextBox();
            this.m2ButtonTest = new System.Windows.Forms.Button();
            this.m2TextBoxTitle = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.m2ComboBoxCOM = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.graphicDurationNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.graphicIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.graphicModeComboBox = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxSaveData = new System.Windows.Forms.CheckBox();
            this.checkBoxSaveErrors = new System.Windows.Forms.CheckBox();
            this.graphicShowOperatorCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.m1GroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphicDurationNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphicIntervalNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearchComPorts
            // 
            this.btnSearchComPorts.Location = new System.Drawing.Point(12, 12);
            this.btnSearchComPorts.Name = "btnSearchComPorts";
            this.btnSearchComPorts.Size = new System.Drawing.Size(210, 23);
            this.btnSearchComPorts.TabIndex = 5;
            this.btnSearchComPorts.Text = "Detecter les ports COM";
            this.btnSearchComPorts.UseVisualStyleBackColor = true;
            this.btnSearchComPorts.Click += new System.EventHandler(this.btnSearchComPorts_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(230, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(209, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "Ouvrir le graphique";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(230, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(209, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Ouvrir le graphique (plein écran)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // m1GroupBox
            // 
            this.m1GroupBox.Controls.Add(this.m1TextBoxFrequencySource);
            this.m1GroupBox.Controls.Add(this.label13);
            this.m1GroupBox.Controls.Add(this.m1TextBoxFrequency);
            this.m1GroupBox.Controls.Add(this.label12);
            this.m1GroupBox.Controls.Add(this.m1ComboBoxUnit);
            this.m1GroupBox.Controls.Add(this.label11);
            this.m1GroupBox.Controls.Add(this.m1TextBoxPower);
            this.m1GroupBox.Controls.Add(this.m1ButtonTest);
            this.m1GroupBox.Controls.Add(this.m1TextBoxTitle);
            this.m1GroupBox.Controls.Add(this.label6);
            this.m1GroupBox.Controls.Add(this.label5);
            this.m1GroupBox.Controls.Add(this.m1ComboBoxCOM);
            this.m1GroupBox.Location = new System.Drawing.Point(12, 70);
            this.m1GroupBox.Name = "m1GroupBox";
            this.m1GroupBox.Size = new System.Drawing.Size(285, 190);
            this.m1GroupBox.TabIndex = 21;
            this.m1GroupBox.TabStop = false;
            this.m1GroupBox.Text = "Mesureur n°1";
            // 
            // m1TextBoxFrequencySource
            // 
            this.m1TextBoxFrequencySource.Location = new System.Drawing.Point(131, 159);
            this.m1TextBoxFrequencySource.Name = "m1TextBoxFrequencySource";
            this.m1TextBoxFrequencySource.ReadOnly = true;
            this.m1TextBoxFrequencySource.Size = new System.Drawing.Size(148, 20);
            this.m1TextBoxFrequencySource.TabIndex = 11;
            this.m1TextBoxFrequencySource.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 162);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Fréquence";
            // 
            // m1TextBoxFrequency
            // 
            this.m1TextBoxFrequency.Location = new System.Drawing.Point(65, 159);
            this.m1TextBoxFrequency.Name = "m1TextBoxFrequency";
            this.m1TextBoxFrequency.ReadOnly = true;
            this.m1TextBoxFrequency.Size = new System.Drawing.Size(60, 20);
            this.m1TextBoxFrequency.TabIndex = 9;
            this.m1TextBoxFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 77);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Unité";
            // 
            // m1ComboBoxUnit
            // 
            this.m1ComboBoxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m1ComboBoxUnit.FormattingEnabled = true;
            this.m1ComboBoxUnit.Items.AddRange(new object[] {
            "V/m",
            "mW/m²"});
            this.m1ComboBoxUnit.Location = new System.Drawing.Point(65, 74);
            this.m1ComboBoxUnit.Name = "m1ComboBoxUnit";
            this.m1ComboBoxUnit.Size = new System.Drawing.Size(214, 21);
            this.m1ComboBoxUnit.TabIndex = 7;
            this.toolTip.SetToolTip(this.m1ComboBoxUnit, "Doit correspondre à l\'unité selectionnée sur le mesureur");
            this.m1ComboBoxUnit.SelectedIndexChanged += new System.EventHandler(this.m1ComboBoxUnit_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 136);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Intensité";
            // 
            // m1TextBoxPower
            // 
            this.m1TextBoxPower.Location = new System.Drawing.Point(65, 133);
            this.m1TextBoxPower.Name = "m1TextBoxPower";
            this.m1TextBoxPower.ReadOnly = true;
            this.m1TextBoxPower.Size = new System.Drawing.Size(214, 20);
            this.m1TextBoxPower.TabIndex = 5;
            this.m1TextBoxPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m1ButtonTest
            // 
            this.m1ButtonTest.Location = new System.Drawing.Point(6, 104);
            this.m1ButtonTest.Name = "m1ButtonTest";
            this.m1ButtonTest.Size = new System.Drawing.Size(273, 23);
            this.m1ButtonTest.TabIndex = 4;
            this.m1ButtonTest.Text = "Tester la connexion";
            this.m1ButtonTest.UseVisualStyleBackColor = true;
            this.m1ButtonTest.Click += new System.EventHandler(this.m1ButtonTest_Click);
            // 
            // m1TextBoxTitle
            // 
            this.m1TextBoxTitle.Location = new System.Drawing.Point(65, 47);
            this.m1TextBoxTitle.Name = "m1TextBoxTitle";
            this.m1TextBoxTitle.Size = new System.Drawing.Size(214, 20);
            this.m1TextBoxTitle.TabIndex = 3;
            this.m1TextBoxTitle.Text = "Emplacement mesureur n°1";
            this.m1TextBoxTitle.TextChanged += new System.EventHandler(this.m1TextBoxTitle_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Titre";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Port COM";
            // 
            // m1ComboBoxCOM
            // 
            this.m1ComboBoxCOM.FormattingEnabled = true;
            this.m1ComboBoxCOM.Location = new System.Drawing.Point(65, 19);
            this.m1ComboBoxCOM.Name = "m1ComboBoxCOM";
            this.m1ComboBoxCOM.Size = new System.Drawing.Size(214, 21);
            this.m1ComboBoxCOM.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Intervalle (s)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Durée (s)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m2TextBoxFrequencySource);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.m2TextBoxFrequency);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.m2ComboBoxUnit);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.m2TextBoxPower);
            this.groupBox1.Controls.Add(this.m2ButtonTest);
            this.groupBox1.Controls.Add(this.m2TextBoxTitle);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.m2ComboBoxCOM);
            this.groupBox1.Location = new System.Drawing.Point(303, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 190);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mesureur n°2";
            // 
            // m2TextBoxFrequencySource
            // 
            this.m2TextBoxFrequencySource.Location = new System.Drawing.Point(131, 159);
            this.m2TextBoxFrequencySource.Name = "m2TextBoxFrequencySource";
            this.m2TextBoxFrequencySource.ReadOnly = true;
            this.m2TextBoxFrequencySource.Size = new System.Drawing.Size(148, 20);
            this.m2TextBoxFrequencySource.TabIndex = 11;
            this.m2TextBoxFrequencySource.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Fréquence";
            // 
            // m2TextBoxFrequency
            // 
            this.m2TextBoxFrequency.Location = new System.Drawing.Point(65, 159);
            this.m2TextBoxFrequency.Name = "m2TextBoxFrequency";
            this.m2TextBoxFrequency.ReadOnly = true;
            this.m2TextBoxFrequency.Size = new System.Drawing.Size(60, 20);
            this.m2TextBoxFrequency.TabIndex = 9;
            this.m2TextBoxFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Unité";
            // 
            // m2ComboBoxUnit
            // 
            this.m2ComboBoxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m2ComboBoxUnit.FormattingEnabled = true;
            this.m2ComboBoxUnit.Items.AddRange(new object[] {
            "V/m",
            "mW/m²"});
            this.m2ComboBoxUnit.Location = new System.Drawing.Point(65, 74);
            this.m2ComboBoxUnit.Name = "m2ComboBoxUnit";
            this.m2ComboBoxUnit.Size = new System.Drawing.Size(214, 21);
            this.m2ComboBoxUnit.TabIndex = 7;
            this.toolTip.SetToolTip(this.m2ComboBoxUnit, "Doit correspondre à l\'unité selectionnée sur le mesureur");
            this.m2ComboBoxUnit.SelectedIndexChanged += new System.EventHandler(this.m2ComboBoxUnit_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 136);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Intensité";
            // 
            // m2TextBoxPower
            // 
            this.m2TextBoxPower.Location = new System.Drawing.Point(65, 133);
            this.m2TextBoxPower.Name = "m2TextBoxPower";
            this.m2TextBoxPower.ReadOnly = true;
            this.m2TextBoxPower.Size = new System.Drawing.Size(214, 20);
            this.m2TextBoxPower.TabIndex = 5;
            this.m2TextBoxPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m2ButtonTest
            // 
            this.m2ButtonTest.Location = new System.Drawing.Point(6, 104);
            this.m2ButtonTest.Name = "m2ButtonTest";
            this.m2ButtonTest.Size = new System.Drawing.Size(273, 23);
            this.m2ButtonTest.TabIndex = 4;
            this.m2ButtonTest.Text = "Tester la connexion";
            this.m2ButtonTest.UseVisualStyleBackColor = true;
            this.m2ButtonTest.Click += new System.EventHandler(this.m2ButtonTest_Click);
            // 
            // m2TextBoxTitle
            // 
            this.m2TextBoxTitle.Location = new System.Drawing.Point(65, 47);
            this.m2TextBoxTitle.Name = "m2TextBoxTitle";
            this.m2TextBoxTitle.Size = new System.Drawing.Size(214, 20);
            this.m2TextBoxTitle.TabIndex = 3;
            this.m2TextBoxTitle.Text = "Emplacement mesureur n°2";
            this.m2TextBoxTitle.TextChanged += new System.EventHandler(this.m2TextBoxTitle_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 50);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(28, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Titre";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Port COM";
            // 
            // m2ComboBoxCOM
            // 
            this.m2ComboBoxCOM.FormattingEnabled = true;
            this.m2ComboBoxCOM.Location = new System.Drawing.Point(65, 19);
            this.m2ComboBoxCOM.Name = "m2ComboBoxCOM";
            this.m2ComboBoxCOM.Size = new System.Drawing.Size(214, 21);
            this.m2ComboBoxCOM.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.graphicShowOperatorCheckBox);
            this.groupBox2.Controls.Add(this.graphicDurationNumericUpDown);
            this.groupBox2.Controls.Add(this.graphicIntervalNumericUpDown);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.graphicModeComboBox);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(12, 266);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(576, 81);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Paramètres du graphique";
            // 
            // graphicDurationNumericUpDown
            // 
            this.graphicDurationNumericUpDown.Increment = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.graphicDurationNumericUpDown.Location = new System.Drawing.Point(138, 46);
            this.graphicDurationNumericUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.graphicDurationNumericUpDown.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.graphicDurationNumericUpDown.Name = "graphicDurationNumericUpDown";
            this.graphicDurationNumericUpDown.Size = new System.Drawing.Size(141, 20);
            this.graphicDurationNumericUpDown.TabIndex = 30;
            this.toolTip.SetToolTip(this.graphicDurationNumericUpDown, "Determine le nombre de points affichés sur le graphique");
            this.graphicDurationNumericUpDown.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.graphicDurationNumericUpDown.ValueChanged += new System.EventHandler(this.graphicDurationNumericUpDown_ValueChanged);
            // 
            // graphicIntervalNumericUpDown
            // 
            this.graphicIntervalNumericUpDown.DecimalPlaces = 1;
            this.graphicIntervalNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.graphicIntervalNumericUpDown.Location = new System.Drawing.Point(138, 20);
            this.graphicIntervalNumericUpDown.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.graphicIntervalNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.graphicIntervalNumericUpDown.Name = "graphicIntervalNumericUpDown";
            this.graphicIntervalNumericUpDown.Size = new System.Drawing.Size(141, 20);
            this.graphicIntervalNumericUpDown.TabIndex = 29;
            this.toolTip.SetToolTip(this.graphicIntervalNumericUpDown, "Ne doit pas être inférieur à la fréquence  séléctionnée sur le mesureur");
            this.graphicIntervalNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.graphicIntervalNumericUpDown.ValueChanged += new System.EventHandler(this.graphicIntervalNumericUpDown_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(301, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 13);
            this.label17.TabIndex = 28;
            this.label17.Text = "Mode d\'affichage";
            // 
            // graphicModeComboBox
            // 
            this.graphicModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.graphicModeComboBox.FormattingEnabled = true;
            this.graphicModeComboBox.Items.AddRange(new object[] {
            "Barres",
            "Courbes"});
            this.graphicModeComboBox.Location = new System.Drawing.Point(424, 19);
            this.graphicModeComboBox.Name = "graphicModeComboBox";
            this.graphicModeComboBox.Size = new System.Drawing.Size(140, 21);
            this.graphicModeComboBox.TabIndex = 27;
            this.graphicModeComboBox.SelectedIndexChanged += new System.EventHandler(this.graphicModeComboBox_SelectedIndexChanged);
            // 
            // checkBoxSaveData
            // 
            this.checkBoxSaveData.AutoSize = true;
            this.checkBoxSaveData.Location = new System.Drawing.Point(454, 16);
            this.checkBoxSaveData.Name = "checkBoxSaveData";
            this.checkBoxSaveData.Size = new System.Drawing.Size(134, 17);
            this.checkBoxSaveData.TabIndex = 28;
            this.checkBoxSaveData.Text = "Enregistrer les mesures";
            this.checkBoxSaveData.UseVisualStyleBackColor = true;
            this.checkBoxSaveData.CheckedChanged += new System.EventHandler(this.checkBoxSaveData_CheckedChanged);
            // 
            // checkBoxSaveErrors
            // 
            this.checkBoxSaveErrors.AutoSize = true;
            this.checkBoxSaveErrors.Checked = true;
            this.checkBoxSaveErrors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSaveErrors.Location = new System.Drawing.Point(454, 45);
            this.checkBoxSaveErrors.Name = "checkBoxSaveErrors";
            this.checkBoxSaveErrors.Size = new System.Drawing.Size(127, 17);
            this.checkBoxSaveErrors.TabIndex = 29;
            this.checkBoxSaveErrors.Text = "Enregistrer les erreurs";
            this.checkBoxSaveErrors.UseVisualStyleBackColor = true;
            this.checkBoxSaveErrors.CheckedChanged += new System.EventHandler(this.checkBoxSaveErrors_CheckedChanged);
            // 
            // graphicShowOperatorCheckBox
            // 
            this.graphicShowOperatorCheckBox.AutoSize = true;
            this.graphicShowOperatorCheckBox.Location = new System.Drawing.Point(304, 49);
            this.graphicShowOperatorCheckBox.Name = "graphicShowOperatorCheckBox";
            this.graphicShowOperatorCheckBox.Size = new System.Drawing.Size(164, 17);
            this.graphicShowOperatorCheckBox.TabIndex = 31;
            this.graphicShowOperatorCheckBox.Text = "Afficher les opérateurs mobile";
            this.graphicShowOperatorCheckBox.UseVisualStyleBackColor = true;
            this.graphicShowOperatorCheckBox.CheckedChanged += new System.EventHandler(this.graphicShowOperatorCheckBox_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(210, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "Ouvrir le gestionnaire de périphériques";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(603, 359);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBoxSaveErrors);
            this.Controls.Add(this.checkBoxSaveData);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m1GroupBox);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnSearchComPorts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(619, 352);
            this.Name = "mainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Remous";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.mainForm_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.m1GroupBox.ResumeLayout(false);
            this.m1GroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphicDurationNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphicIntervalNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSearchComPorts;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox m1GroupBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m1TextBoxPower;
        private System.Windows.Forms.Button m1ButtonTest;
        private System.Windows.Forms.TextBox m1TextBoxTitle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox m1ComboBoxCOM;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox m1ComboBoxUnit;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox m1TextBoxFrequency;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox m2TextBoxFrequency;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox m2ComboBoxUnit;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox m2TextBoxPower;
        private System.Windows.Forms.Button m2ButtonTest;
        private System.Windows.Forms.TextBox m2TextBoxTitle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox m2ComboBoxCOM;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox graphicModeComboBox;
        private System.Windows.Forms.NumericUpDown graphicDurationNumericUpDown;
        private System.Windows.Forms.NumericUpDown graphicIntervalNumericUpDown;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TextBox m1TextBoxFrequencySource;
        private System.Windows.Forms.TextBox m2TextBoxFrequencySource;
        private System.Windows.Forms.CheckBox checkBoxSaveData;
        private System.Windows.Forms.CheckBox checkBoxSaveErrors;
        private System.Windows.Forms.CheckBox graphicShowOperatorCheckBox;
        private System.Windows.Forms.Button button1;
    }
}

