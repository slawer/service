namespace BlockConfiguration.GUI
{
    partial class OptionsOfBlockForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownSpeed = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.listViewCmds = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBoxTypeCRC = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxSpeed = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxNetAddress = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.Accept = new System.Windows.Forms.Button();
            this.checkBoxNeedSetTime = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownPerecl1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPerecl2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPerecl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPerecl2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numericUpDownSpeed);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.listViewCmds);
            this.groupBox1.Location = new System.Drawing.Point(22, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(577, 242);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Дополнительные функции(опрос)";
            // 
            // numericUpDownSpeed
            // 
            this.numericUpDownSpeed.DecimalPlaces = 4;
            this.numericUpDownSpeed.Increment = new decimal(new int[] {
            52,
            0,
            0,
            262144});
            this.numericUpDownSpeed.Location = new System.Drawing.Point(275, 202);
            this.numericUpDownSpeed.Maximum = new decimal(new int[] {
            1333,
            0,
            0,
            196608});
            this.numericUpDownSpeed.Minimum = new decimal(new int[] {
            52,
            0,
            0,
            262144});
            this.numericUpDownSpeed.Name = "numericUpDownSpeed";
            this.numericUpDownSpeed.Size = new System.Drawing.Size(67, 20);
            this.numericUpDownSpeed.TabIndex = 4;
            this.numericUpDownSpeed.Value = new decimal(new int[] {
            52,
            0,
            0,
            262144});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(233, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Временной интервал между запросами(сек)";
            // 
            // listViewCmds
            // 
            this.listViewCmds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewCmds.FullRowSelect = true;
            this.listViewCmds.GridLines = true;
            this.listViewCmds.Location = new System.Drawing.Point(19, 28);
            this.listViewCmds.Name = "listViewCmds";
            this.listViewCmds.Size = new System.Drawing.Size(552, 168);
            this.listViewCmds.TabIndex = 3;
            this.listViewCmds.UseCompatibleStateImageBehavior = false;
            this.listViewCmds.View = System.Windows.Forms.View.Details;
            this.listViewCmds.DoubleClick += new System.EventHandler(this.listViewCmds_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Сетевой адрес";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 101;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Размер буфера";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 117;
            // 
            // comboBoxTypeCRC
            // 
            this.comboBoxTypeCRC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTypeCRC.FormattingEnabled = true;
            this.comboBoxTypeCRC.Items.AddRange(new object[] {
            "CRC16(два байта)",
            "Однобайтовая по модулю 255"});
            this.comboBoxTypeCRC.Location = new System.Drawing.Point(167, 49);
            this.comboBoxTypeCRC.Name = "comboBoxTypeCRC";
            this.comboBoxTypeCRC.Size = new System.Drawing.Size(246, 21);
            this.comboBoxTypeCRC.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Тип контрольной суммы";
            // 
            // comboBoxSpeed
            // 
            this.comboBoxSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSpeed.FormattingEnabled = true;
            this.comboBoxSpeed.Items.AddRange(new object[] {
            "38400",
            "57600"});
            this.comboBoxSpeed.Location = new System.Drawing.Point(442, 22);
            this.comboBoxSpeed.Name = "comboBoxSpeed";
            this.comboBoxSpeed.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSpeed.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(294, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Скорость обмена(бод)";
            // 
            // comboBoxNetAddress
            // 
            this.comboBoxNetAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxNetAddress.FormattingEnabled = true;
            this.comboBoxNetAddress.Items.AddRange(new object[] {
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
            "1C",
            "1D",
            "1E",
            "1F"});
            this.comboBoxNetAddress.Location = new System.Drawing.Point(167, 22);
            this.comboBoxNetAddress.Name = "comboBoxNetAddress";
            this.comboBoxNetAddress.Size = new System.Drawing.Size(121, 21);
            this.comboBoxNetAddress.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Сетевой адрес устройства";
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(518, 330);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 6;
            this.Cancel.Text = "Отмена";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Accept
            // 
            this.Accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Accept.Location = new System.Drawing.Point(437, 330);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(75, 23);
            this.Accept.TabIndex = 5;
            this.Accept.Text = "Принять";
            this.Accept.UseVisualStyleBackColor = true;
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // checkBoxNeedSetTime
            // 
            this.checkBoxNeedSetTime.AutoSize = true;
            this.checkBoxNeedSetTime.Location = new System.Drawing.Point(437, 51);
            this.checkBoxNeedSetTime.Name = "checkBoxNeedSetTime";
            this.checkBoxNeedSetTime.Size = new System.Drawing.Size(121, 17);
            this.checkBoxNeedSetTime.TabIndex = 17;
            this.checkBoxNeedSetTime.Text = "Установить время";
            this.checkBoxNeedSetTime.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 330);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Номера переключаемых индикаторов";
            // 
            // numericUpDownPerecl1
            // 
            this.numericUpDownPerecl1.Location = new System.Drawing.Point(256, 328);
            this.numericUpDownPerecl1.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownPerecl1.Name = "numericUpDownPerecl1";
            this.numericUpDownPerecl1.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownPerecl1.TabIndex = 19;
            // 
            // numericUpDownPerecl2
            // 
            this.numericUpDownPerecl2.Location = new System.Drawing.Point(303, 328);
            this.numericUpDownPerecl2.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownPerecl2.Name = "numericUpDownPerecl2";
            this.numericUpDownPerecl2.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownPerecl2.TabIndex = 20;
            // 
            // OptionsOfBlockForm
            // 
            this.AcceptButton = this.Accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(605, 365);
            this.Controls.Add(this.numericUpDownPerecl2);
            this.Controls.Add(this.numericUpDownPerecl1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBoxNeedSetTime);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxTypeCRC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxSpeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxNetAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsOfBlockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OptionsOfBlockForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPerecl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPerecl2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownSpeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listViewCmds;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ComboBox comboBoxTypeCRC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Accept;
        public System.Windows.Forms.ComboBox comboBoxNetAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownPerecl2;
        private System.Windows.Forms.NumericUpDown numericUpDownPerecl1;
        public System.Windows.Forms.CheckBox checkBoxNeedSetTime;
    }
}