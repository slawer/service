namespace Platform
{
    partial class Options
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.checkBoxAutostart = new System.Windows.Forms.CheckBox();
            this.Accept = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.errorProviderPort = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderHost = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownWaitTimeForWorking = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownMaxPacketsCount = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownDownTime = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownSpeedSyrvey = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderHost)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitTimeForWorking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPacketsCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDownTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeedSyrvey)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Хост";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Порт";
            // 
            // textBoxHost
            // 
            this.textBoxHost.Location = new System.Drawing.Point(53, 27);
            this.textBoxHost.Name = "textBoxHost";
            this.textBoxHost.Size = new System.Drawing.Size(386, 20);
            this.textBoxHost.TabIndex = 2;
            this.textBoxHost.TextChanged += new System.EventHandler(this.textBoxHost_TextChanged);
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(53, 53);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 20);
            this.textBoxPort.TabIndex = 3;
            this.textBoxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxPort.TextChanged += new System.EventHandler(this.textBoxPort_TextChanged);
            // 
            // checkBoxAutostart
            // 
            this.checkBoxAutostart.AutoSize = true;
            this.checkBoxAutostart.Location = new System.Drawing.Point(196, 56);
            this.checkBoxAutostart.Name = "checkBoxAutostart";
            this.checkBoxAutostart.Size = new System.Drawing.Size(158, 17);
            this.checkBoxAutostart.TabIndex = 4;
            this.checkBoxAutostart.Text = "Подключаться при старте";
            this.checkBoxAutostart.UseVisualStyleBackColor = true;
            // 
            // Accept
            // 
            this.Accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Accept.Location = new System.Drawing.Point(305, 282);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(75, 23);
            this.Accept.TabIndex = 7;
            this.Accept.Text = "Принять";
            this.Accept.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(386, 282);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 8;
            this.Cancel.Text = "Отмена";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // errorProviderPort
            // 
            this.errorProviderPort.ContainerControl = this;
            // 
            // errorProviderHost
            // 
            this.errorProviderHost.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numericUpDownWaitTimeForWorking);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numericUpDownMaxPacketsCount);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numericUpDownDownTime);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDownSpeedSyrvey);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(15, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 197);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки платформы";
            // 
            // numericUpDownWaitTimeForWorking
            // 
            this.numericUpDownWaitTimeForWorking.Location = new System.Drawing.Point(266, 87);
            this.numericUpDownWaitTimeForWorking.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownWaitTimeForWorking.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownWaitTimeForWorking.Name = "numericUpDownWaitTimeForWorking";
            this.numericUpDownWaitTimeForWorking.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownWaitTimeForWorking.TabIndex = 8;
            this.numericUpDownWaitTimeForWorking.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(221, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Минимальное время на обработку пакета";
            // 
            // numericUpDownMaxPacketsCount
            // 
            this.numericUpDownMaxPacketsCount.Location = new System.Drawing.Point(266, 113);
            this.numericUpDownMaxPacketsCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownMaxPacketsCount.Minimum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericUpDownMaxPacketsCount.Name = "numericUpDownMaxPacketsCount";
            this.numericUpDownMaxPacketsCount.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownMaxPacketsCount.TabIndex = 6;
            this.numericUpDownMaxPacketsCount.Value = new decimal(new int[] {
            7000,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(189, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Максимальное количество пакетов";
            // 
            // numericUpDownDownTime
            // 
            this.numericUpDownDownTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownDownTime.Location = new System.Drawing.Point(266, 61);
            this.numericUpDownDownTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownDownTime.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDownTime.Name = "numericUpDownDownTime";
            this.numericUpDownDownTime.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownDownTime.TabIndex = 4;
            this.numericUpDownDownTime.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(227, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Максимальное время на обработку пакета";
            // 
            // numericUpDownSpeedSyrvey
            // 
            this.numericUpDownSpeedSyrvey.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownSpeedSyrvey.Location = new System.Drawing.Point(266, 35);
            this.numericUpDownSpeedSyrvey.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownSpeedSyrvey.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownSpeedSyrvey.Name = "numericUpDownSpeedSyrvey";
            this.numericUpDownSpeedSyrvey.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownSpeedSyrvey.TabIndex = 2;
            this.numericUpDownSpeedSyrvey.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Частота рассылки пакетов плагинам";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(412, 40);
            this.label3.TabIndex = 0;
            this.label3.Text = "Для вступления данных настроек в силу необходимо перезапустить приложение";
            // 
            // Options
            // 
            this.AcceptButton = this.Accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(473, 317);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.checkBoxAutostart);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxHost);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Options_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderHost)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitTimeForWorking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxPacketsCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDownTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeedSyrvey)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxHost;
        public System.Windows.Forms.TextBox textBoxPort;
        public System.Windows.Forms.CheckBox checkBoxAutostart;
        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ErrorProvider errorProviderPort;
        private System.Windows.Forms.ErrorProvider errorProviderHost;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown numericUpDownSpeedSyrvey;
        public System.Windows.Forms.NumericUpDown numericUpDownDownTime;
        public System.Windows.Forms.NumericUpDown numericUpDownMaxPacketsCount;
        public System.Windows.Forms.NumericUpDown numericUpDownWaitTimeForWorking;
    }
}