namespace BlockConfiguration.GUI
{
    partial class EditForm
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
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.textBoxMax = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxBlink = new System.Windows.Forms.CheckBox();
            this.checkBoxDWORD = new System.Windows.Forms.CheckBox();
            this.checkBoxHide = new System.Windows.Forms.CheckBox();
            this.textBoxOffset = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxFact = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.comboBoxPntPos = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxOffPpBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxOffPpByte = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxOffThrBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxOffThrByte = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxOffDat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxNetAddress = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.Accept = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxMin
            // 
            this.textBoxMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMin.Location = new System.Drawing.Point(462, 266);
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(56, 20);
            this.textBoxMin.TabIndex = 13;
            this.textBoxMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxMax
            // 
            this.textBoxMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMax.Location = new System.Drawing.Point(462, 292);
            this.textBoxMax.Name = "textBoxMax";
            this.textBoxMax.Size = new System.Drawing.Size(56, 20);
            this.textBoxMax.TabIndex = 14;
            this.textBoxMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(314, 295);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(142, 13);
            this.label12.TabIndex = 123;
            this.label12.Text = "Значение верхнего порога";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(314, 269);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(139, 13);
            this.label11.TabIndex = 122;
            this.label11.Text = "Значение нижнего порога";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxBlink);
            this.groupBox1.Controls.Add(this.checkBoxDWORD);
            this.groupBox1.Controls.Add(this.checkBoxHide);
            this.groupBox1.Location = new System.Drawing.Point(317, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 112);
            this.groupBox1.TabIndex = 107;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Управление работой индикатора";
            // 
            // checkBoxBlink
            // 
            this.checkBoxBlink.AutoSize = true;
            this.checkBoxBlink.Location = new System.Drawing.Point(15, 29);
            this.checkBoxBlink.Name = "checkBoxBlink";
            this.checkBoxBlink.Size = new System.Drawing.Size(167, 17);
            this.checkBoxBlink.TabIndex = 8;
            this.checkBoxBlink.Text = "Индикатор должен моргать";
            this.checkBoxBlink.UseVisualStyleBackColor = true;
            this.checkBoxBlink.CheckedChanged += new System.EventHandler(this.checkBoxBlink_CheckedChanged);
            // 
            // checkBoxDWORD
            // 
            this.checkBoxDWORD.AutoSize = true;
            this.checkBoxDWORD.Location = new System.Drawing.Point(15, 75);
            this.checkBoxDWORD.Name = "checkBoxDWORD";
            this.checkBoxDWORD.Size = new System.Drawing.Size(263, 17);
            this.checkBoxDWORD.TabIndex = 10;
            this.checkBoxDWORD.Text = "Данные для индикации имеют размер 4 байта";
            this.checkBoxDWORD.UseVisualStyleBackColor = true;
            this.checkBoxDWORD.CheckedChanged += new System.EventHandler(this.checkBoxDWORD_CheckedChanged);
            // 
            // checkBoxHide
            // 
            this.checkBoxHide.AutoSize = true;
            this.checkBoxHide.Location = new System.Drawing.Point(15, 52);
            this.checkBoxHide.Name = "checkBoxHide";
            this.checkBoxHide.Size = new System.Drawing.Size(242, 17);
            this.checkBoxHide.TabIndex = 9;
            this.checkBoxHide.Text = "Индикатор должен быть всегда выключен";
            this.checkBoxHide.UseVisualStyleBackColor = true;
            this.checkBoxHide.CheckedChanged += new System.EventHandler(this.checkBoxHide_CheckedChanged);
            // 
            // textBoxOffset
            // 
            this.textBoxOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOffset.Location = new System.Drawing.Point(252, 292);
            this.textBoxOffset.Name = "textBoxOffset";
            this.textBoxOffset.Size = new System.Drawing.Size(56, 20);
            this.textBoxOffset.TabIndex = 12;
            this.textBoxOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 295);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(203, 13);
            this.label10.TabIndex = 120;
            this.label10.Text = "Коррекция смещения входных данных";
            // 
            // textBoxFact
            // 
            this.textBoxFact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFact.Location = new System.Drawing.Point(252, 266);
            this.textBoxFact.Name = "textBoxFact";
            this.textBoxFact.Size = new System.Drawing.Size(56, 20);
            this.textBoxFact.TabIndex = 11;
            this.textBoxFact.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 269);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(219, 13);
            this.label9.TabIndex = 118;
            this.label9.Text = "Коэффициент коррекции входных данных";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 117;
            this.label8.Text = "Тип индикатора";
            // 
            // comboBoxType
            // 
            this.comboBoxType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Столбик, 32 деления",
            "Столбик, 32 деления биполярный",
            "3-х значный",
            "4-х значный",
            "5-и значный",
            "Часы",
            "По умолчанию"});
            this.comboBoxType.Location = new System.Drawing.Point(108, 23);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(200, 21);
            this.comboBoxType.TabIndex = 0;
            // 
            // comboBoxPntPos
            // 
            this.comboBoxPntPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPntPos.FormattingEnabled = true;
            this.comboBoxPntPos.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxPntPos.Location = new System.Drawing.Point(258, 104);
            this.comboBoxPntPos.Name = "comboBoxPntPos";
            this.comboBoxPntPos.Size = new System.Drawing.Size(50, 21);
            this.comboBoxPntPos.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 13);
            this.label7.TabIndex = 114;
            this.label7.Text = "Положение десятичной точки";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.comboBoxOffPpBits);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.comboBoxOffPpByte);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(314, 131);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(293, 129);
            this.groupBox3.TabIndex = 113;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Смещения положения десятичной точки на индикаторе";
            // 
            // comboBoxOffPpBits
            // 
            this.comboBoxOffPpBits.FormattingEnabled = true;
            this.comboBoxOffPpBits.Items.AddRange(new object[] {
            "0",
            "2",
            "4",
            "6"});
            this.comboBoxOffPpBits.Location = new System.Drawing.Point(111, 72);
            this.comboBoxOffPpBits.Name = "comboBoxOffPpBits";
            this.comboBoxOffPpBits.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOffPpBits.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Смещение битов";
            // 
            // comboBoxOffPpByte
            // 
            this.comboBoxOffPpByte.FormattingEnabled = true;
            this.comboBoxOffPpByte.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.comboBoxOffPpByte.Location = new System.Drawing.Point(111, 45);
            this.comboBoxOffPpByte.Name = "comboBoxOffPpByte";
            this.comboBoxOffPpByte.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOffPpByte.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Смещение байта";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.comboBoxOffThrBits);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.comboBoxOffThrByte);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(15, 131);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(293, 129);
            this.groupBox2.TabIndex = 112;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Смещение превышения порогов индуцируемым значением";
            // 
            // comboBoxOffThrBits
            // 
            this.comboBoxOffThrBits.FormattingEnabled = true;
            this.comboBoxOffThrBits.Items.AddRange(new object[] {
            "0",
            "2",
            "4",
            "6"});
            this.comboBoxOffThrBits.Location = new System.Drawing.Point(111, 72);
            this.comboBoxOffThrBits.Name = "comboBoxOffThrBits";
            this.comboBoxOffThrBits.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOffThrBits.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Смещение битов";
            // 
            // comboBoxOffThrByte
            // 
            this.comboBoxOffThrByte.FormattingEnabled = true;
            this.comboBoxOffThrByte.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.comboBoxOffThrByte.Location = new System.Drawing.Point(111, 45);
            this.comboBoxOffThrByte.Name = "comboBoxOffThrByte";
            this.comboBoxOffThrByte.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOffThrByte.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Смещение байта";
            // 
            // comboBoxOffDat
            // 
            this.comboBoxOffDat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOffDat.FormattingEnabled = true;
            this.comboBoxOffDat.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "0A",
            "0B",
            "0C",
            "0D",
            "0E",
            "0F",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "1A",
            "1B",
            "1C"});
            this.comboBoxOffDat.Location = new System.Drawing.Point(258, 77);
            this.comboBoxOffDat.Name = "comboBoxOffDat";
            this.comboBoxOffDat.Size = new System.Drawing.Size(50, 21);
            this.comboBoxOffDat.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 13);
            this.label2.TabIndex = 110;
            this.label2.Text = "Смещение индуцируемого значения в пакете";
            // 
            // comboBoxNetAddress
            // 
            this.comboBoxNetAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxNetAddress.FormattingEnabled = true;
            this.comboBoxNetAddress.Items.AddRange(new object[] {
            "81",
            "82",
            "83",
            "84",
            "85",
            "86",
            "87",
            "88",
            "89",
            "8A",
            "8B",
            "8C",
            "8D",
            "8E",
            "8F",
            "90",
            "91",
            "92",
            "93",
            "94",
            "95",
            "96",
            "97",
            "98",
            "99",
            "9A",
            "9B",
            "9C",
            "9D",
            "9E",
            "9F",
            "A0",
            "A1",
            "A2",
            "A3",
            "A4",
            "A5",
            "A6",
            "A7",
            "A8",
            "A9",
            "AA",
            "AB",
            "AC",
            "AD",
            "AE",
            "AF",
            "B0",
            "B1",
            "B2",
            "B3",
            "B4",
            "B5",
            "B6",
            "B7",
            "B8",
            "B9",
            "BA",
            "BB",
            "BC",
            "BD",
            "BE"});
            this.comboBoxNetAddress.Location = new System.Drawing.Point(258, 50);
            this.comboBoxNetAddress.Name = "comboBoxNetAddress";
            this.comboBoxNetAddress.Size = new System.Drawing.Size(50, 21);
            this.comboBoxNetAddress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 13);
            this.label1.TabIndex = 108;
            this.label1.Text = "Сетевой адрес в обрабатываемых пакетах";
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(534, 331);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 16;
            this.Cancel.Text = "Отмена";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Accept
            // 
            this.Accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Accept.Location = new System.Drawing.Point(453, 331);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(75, 23);
            this.Accept.TabIndex = 15;
            this.Accept.Text = "Принять";
            this.Accept.UseVisualStyleBackColor = true;
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // EditForm
            // 
            this.AcceptButton = this.Accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(625, 367);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxMin);
            this.Controls.Add(this.textBoxMax);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxOffset);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxFact);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.comboBoxPntPos);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.comboBoxOffDat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxNetAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование индикатора";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMin;
        private System.Windows.Forms.TextBox textBoxMax;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxBlink;
        private System.Windows.Forms.CheckBox checkBoxDWORD;
        private System.Windows.Forms.CheckBox checkBoxHide;
        private System.Windows.Forms.TextBox textBoxOffset;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxFact;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.ComboBox comboBoxPntPos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxOffPpBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxOffPpByte;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxOffThrBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxOffThrByte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxOffDat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxNetAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Accept;
    }
}