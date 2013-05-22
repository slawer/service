namespace SwitcherProtocol
{
    partial class MainForm
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
            this.listBoxStatusViewer = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxProtocols = new System.Windows.Forms.ComboBox();
            this.numericUpDowndeviceNumber = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonSwitchAll = new System.Windows.Forms.RadioButton();
            this.radioButtonSwitchOne = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTypeCrc = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDowndeviceNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxStatusViewer
            // 
            this.listBoxStatusViewer.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxStatusViewer.FormattingEnabled = true;
            this.listBoxStatusViewer.ItemHeight = 14;
            this.listBoxStatusViewer.Location = new System.Drawing.Point(12, 200);
            this.listBoxStatusViewer.Name = "listBoxStatusViewer";
            this.listBoxStatusViewer.Size = new System.Drawing.Size(551, 186);
            this.listBoxStatusViewer.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxTypeCrc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxProtocols);
            this.groupBox1.Controls.Add(this.numericUpDowndeviceNumber);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButtonSwitchAll);
            this.groupBox1.Controls.Add(this.radioButtonSwitchOne);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 182);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Опции переключения";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Переключить на протокол";
            // 
            // comboBoxProtocols
            // 
            this.comboBoxProtocols.FormattingEnabled = true;
            this.comboBoxProtocols.Items.AddRange(new object[] {
            "DSN",
            "ModBus"});
            this.comboBoxProtocols.Location = new System.Drawing.Point(152, 57);
            this.comboBoxProtocols.Name = "comboBoxProtocols";
            this.comboBoxProtocols.Size = new System.Drawing.Size(163, 21);
            this.comboBoxProtocols.TabIndex = 4;
            this.comboBoxProtocols.SelectedIndexChanged += new System.EventHandler(this.comboBoxProtocols_SelectedIndexChanged);
            // 
            // numericUpDowndeviceNumber
            // 
            this.numericUpDowndeviceNumber.Location = new System.Drawing.Point(152, 31);
            this.numericUpDowndeviceNumber.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numericUpDowndeviceNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDowndeviceNumber.Name = "numericUpDowndeviceNumber";
            this.numericUpDowndeviceNumber.Size = new System.Drawing.Size(49, 20);
            this.numericUpDowndeviceNumber.TabIndex = 3;
            this.numericUpDowndeviceNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Номер устройства";
            // 
            // radioButtonSwitchAll
            // 
            this.radioButtonSwitchAll.AutoSize = true;
            this.radioButtonSwitchAll.Location = new System.Drawing.Point(21, 147);
            this.radioButtonSwitchAll.Name = "radioButtonSwitchAll";
            this.radioButtonSwitchAll.Size = new System.Drawing.Size(222, 17);
            this.radioButtonSwitchAll.TabIndex = 1;
            this.radioButtonSwitchAll.Tag = "2";
            this.radioButtonSwitchAll.Text = "Переключить все устройства на линии";
            this.radioButtonSwitchAll.UseVisualStyleBackColor = true;
            this.radioButtonSwitchAll.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // radioButtonSwitchOne
            // 
            this.radioButtonSwitchOne.AutoSize = true;
            this.radioButtonSwitchOne.Checked = true;
            this.radioButtonSwitchOne.Location = new System.Drawing.Point(21, 124);
            this.radioButtonSwitchOne.Name = "radioButtonSwitchOne";
            this.radioButtonSwitchOne.Size = new System.Drawing.Size(180, 17);
            this.radioButtonSwitchOne.TabIndex = 0;
            this.radioButtonSwitchOne.TabStop = true;
            this.radioButtonSwitchOne.Tag = "";
            this.radioButtonSwitchOne.Text = "Переключить одно устройство";
            this.radioButtonSwitchOne.UseVisualStyleBackColor = true;
            this.radioButtonSwitchOne.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 392);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Настроить порт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(125, 392);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Переключить устройство(а)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 38400;
            this.serialPort.WriteBufferSize = 4096;
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.DataReceived);
            this.serialPort.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(this.ErrorReceived);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Тип контрольной суммы";
            // 
            // comboBoxTypeCrc
            // 
            this.comboBoxTypeCrc.FormattingEnabled = true;
            this.comboBoxTypeCrc.Items.AddRange(new object[] {
            "Алгоритм CRC-16",
            "Циклическая однобайтная",
            "Циклическая двухбайтная"});
            this.comboBoxTypeCrc.Location = new System.Drawing.Point(152, 84);
            this.comboBoxTypeCrc.Name = "comboBoxTypeCrc";
            this.comboBoxTypeCrc.Size = new System.Drawing.Size(163, 21);
            this.comboBoxTypeCrc.TabIndex = 7;
            this.comboBoxTypeCrc.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypeCrc_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 422);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBoxStatusViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Переключатель";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDowndeviceNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxStatusViewer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonSwitchAll;
        private System.Windows.Forms.RadioButton radioButtonSwitchOne;
        private System.Windows.Forms.NumericUpDown numericUpDowndeviceNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxProtocols;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.ComboBox comboBoxTypeCrc;
        private System.Windows.Forms.Label label3;
    }
}