namespace SwitcherProtocol
{
    partial class PortOptions
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxPortNames = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxBufferRead = new System.Windows.Forms.ComboBox();
            this.comboBoxBufferWrite = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.comboBoxStopBits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.comboBoxDataBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.accept = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Номер COM порта:";
            // 
            // comboBoxPortNames
            // 
            this.comboBoxPortNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPortNames.FormattingEnabled = true;
            this.comboBoxPortNames.Location = new System.Drawing.Point(131, 19);
            this.comboBoxPortNames.Name = "comboBoxPortNames";
            this.comboBoxPortNames.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPortNames.TabIndex = 49;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxBufferRead);
            this.groupBox2.Controls.Add(this.comboBoxBufferWrite);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(15, 46);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(519, 154);
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Размер пакета";
            // 
            // comboBoxBufferRead
            // 
            this.comboBoxBufferRead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBufferRead.FormattingEnabled = true;
            this.comboBoxBufferRead.Items.AddRange(new object[] {
            "64",
            "128",
            "192",
            "256",
            "320",
            "384",
            "448",
            "512",
            "576",
            "640",
            "704",
            "768",
            "832",
            "896",
            "960",
            "1024",
            "1088",
            "1152",
            "1216",
            "1280",
            "1344",
            "1408",
            "1472",
            "1536",
            "1600",
            "1664",
            "1728",
            "1792",
            "1856",
            "1920",
            "1984",
            "2048",
            "2112",
            "2176",
            "2240",
            "2304",
            "2368",
            "2432",
            "2496",
            "2560",
            "2624",
            "2688",
            "2752",
            "2816",
            "2880",
            "2944",
            "3008",
            "3072",
            "3136",
            "3200",
            "3264",
            "3328",
            "3392",
            "3456",
            "3520",
            "3584",
            "3648",
            "3712",
            "3776",
            "3840",
            "3904",
            "3968",
            "4032",
            "4096"});
            this.comboBoxBufferRead.Location = new System.Drawing.Point(263, 83);
            this.comboBoxBufferRead.Name = "comboBoxBufferRead";
            this.comboBoxBufferRead.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBufferRead.TabIndex = 4;
            // 
            // comboBoxBufferWrite
            // 
            this.comboBoxBufferWrite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBufferWrite.FormattingEnabled = true;
            this.comboBoxBufferWrite.Items.AddRange(new object[] {
            "64",
            "128",
            "192",
            "256",
            "320",
            "384",
            "448",
            "512",
            "576",
            "640",
            "704",
            "768",
            "832",
            "896",
            "960",
            "1024",
            "1088",
            "1152",
            "1216",
            "1280",
            "1344",
            "1408",
            "1472",
            "1536",
            "1600",
            "1664",
            "1728",
            "1792",
            "1856",
            "1920",
            "1984",
            "2048",
            "2112",
            "2176",
            "2240",
            "2304",
            "2368",
            "2432",
            "2496",
            "2560",
            "2624",
            "2688",
            "2752",
            "2816",
            "2880",
            "2944",
            "3008",
            "3072",
            "3136",
            "3200",
            "3264",
            "3328",
            "3392",
            "3456",
            "3520",
            "3584",
            "3648",
            "3712",
            "3776",
            "3840",
            "3904",
            "3968",
            "4032",
            "4096"});
            this.comboBoxBufferWrite.Location = new System.Drawing.Point(263, 110);
            this.comboBoxBufferWrite.Name = "comboBoxBufferWrite";
            this.comboBoxBufferWrite.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBufferWrite.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(55, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Буфер передачи (Байты):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(55, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Буфер приема (Байты):";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(6, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(504, 54);
            this.label6.TabIndex = 0;
            this.label6.Text = "Чтобы устранить проблемы с производительностью на низких скоростях передачи, попр" +
                "обуйте уменьшить значение.\r\nЧто бы увеличить производительность, попробуйте увел" +
                "ичить значение.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxBaudRate);
            this.groupBox1.Controls.Add(this.comboBoxStopBits);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxParity);
            this.groupBox1.Controls.Add(this.comboBoxDataBits);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(15, 206);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 159);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Скорость (бит/с):";
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBaudRate.FormattingEnabled = true;
            this.comboBoxBaudRate.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "1800",
            "2400",
            "4800",
            "7200",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.comboBoxBaudRate.Location = new System.Drawing.Point(126, 29);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(132, 21);
            this.comboBoxBaudRate.TabIndex = 5;
            // 
            // comboBoxStopBits
            // 
            this.comboBoxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStopBits.FormattingEnabled = true;
            this.comboBoxStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBoxStopBits.Location = new System.Drawing.Point(126, 110);
            this.comboBoxStopBits.Name = "comboBoxStopBits";
            this.comboBoxStopBits.Size = new System.Drawing.Size(132, 21);
            this.comboBoxStopBits.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Биты данных:";
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxParity.FormattingEnabled = true;
            this.comboBoxParity.Items.AddRange(new object[] {
            "Чет",
            "Нечет",
            "Нет",
            "Маркер",
            "Пробел"});
            this.comboBoxParity.Location = new System.Drawing.Point(126, 83);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(132, 21);
            this.comboBoxParity.TabIndex = 10;
            // 
            // comboBoxDataBits
            // 
            this.comboBoxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataBits.FormattingEnabled = true;
            this.comboBoxDataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.comboBoxDataBits.Location = new System.Drawing.Point(126, 56);
            this.comboBoxDataBits.Name = "comboBoxDataBits";
            this.comboBoxDataBits.Size = new System.Drawing.Size(132, 21);
            this.comboBoxDataBits.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Стоповые биты:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Четность:";
            // 
            // accept
            // 
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(380, 339);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 51;
            this.accept.Text = "Принять";
            this.accept.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(461, 339);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 52;
            this.cancel.Text = "Отмена";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // PortOptions
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(548, 374);
            this.ControlBox = false;
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxPortNames);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PortOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки порта";
            this.Load += new System.EventHandler(this.ComOptions_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPortNames;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxBufferRead;
        private System.Windows.Forms.ComboBox comboBoxBufferWrite;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.ComboBox comboBoxStopBits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.ComboBox comboBoxDataBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button cancel;
    }
}

