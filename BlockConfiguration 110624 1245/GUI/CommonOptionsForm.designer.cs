namespace BlockConfiguration.GUI
{
    partial class CommonOptionsForm
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
            this.Cancel = new System.Windows.Forms.Button();
            this.Accept = new System.Windows.Forms.Button();
            this.numericUpDownDevice = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownDeviceAnswerTimeout = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDataCheck = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.numericUpDownCountDo = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.numericUpDownTimeoutForAnswer = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDevice)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDeviceAnswerTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDataCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCountDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeoutForAnswer)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancel
            // 
            this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(265, 202);
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
            this.Accept.Location = new System.Drawing.Point(184, 202);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(75, 23);
            this.Accept.TabIndex = 5;
            this.Accept.Text = "Принять";
            this.Accept.UseVisualStyleBackColor = true;
            // 
            // numericUpDownDevice
            // 
            this.numericUpDownDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownDevice.Location = new System.Drawing.Point(236, 15);
            this.numericUpDownDevice.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.numericUpDownDevice.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDevice.Name = "numericUpDownDevice";
            this.numericUpDownDevice.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownDevice.TabIndex = 0;
            this.numericUpDownDevice.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numericUpDownDeviceAnswerTimeout);
            this.groupBox1.Controls.Add(this.numericUpDownDataCheck);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.numericUpDownCountDo);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.numericUpDownTimeoutForAnswer);
            this.groupBox1.Location = new System.Drawing.Point(28, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 153);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки чтени/записи";
            // 
            // numericUpDownDeviceAnswerTimeout
            // 
            this.numericUpDownDeviceAnswerTimeout.Location = new System.Drawing.Point(245, 36);
            this.numericUpDownDeviceAnswerTimeout.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownDeviceAnswerTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDeviceAnswerTimeout.Name = "numericUpDownDeviceAnswerTimeout";
            this.numericUpDownDeviceAnswerTimeout.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownDeviceAnswerTimeout.TabIndex = 1;
            this.numericUpDownDeviceAnswerTimeout.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDownDataCheck
            // 
            this.numericUpDownDataCheck.Location = new System.Drawing.Point(245, 114);
            this.numericUpDownDataCheck.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDownDataCheck.Name = "numericUpDownDataCheck";
            this.numericUpDownDataCheck.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownDataCheck.TabIndex = 4;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(15, 116);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(157, 13);
            this.label22.TabIndex = 49;
            this.label22.Text = "Количество проверок данных";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(15, 38);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(214, 13);
            this.label20.TabIndex = 45;
            this.label20.Text = "Таймаут ожидания ответа от устройства";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(15, 64);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(224, 13);
            this.label21.TabIndex = 47;
            this.label21.Text = "Таймаут между попытками чтения/записи";
            // 
            // numericUpDownCountDo
            // 
            this.numericUpDownCountDo.Location = new System.Drawing.Point(245, 88);
            this.numericUpDownCountDo.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownCountDo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCountDo.Name = "numericUpDownCountDo";
            this.numericUpDownCountDo.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownCountDo.TabIndex = 3;
            this.numericUpDownCountDo.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(15, 90);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(130, 13);
            this.label19.TabIndex = 43;
            this.label19.Text = "Попыток чтения/записи";
            // 
            // numericUpDownTimeoutForAnswer
            // 
            this.numericUpDownTimeoutForAnswer.Location = new System.Drawing.Point(245, 62);
            this.numericUpDownTimeoutForAnswer.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownTimeoutForAnswer.Name = "numericUpDownTimeoutForAnswer";
            this.numericUpDownTimeoutForAnswer.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownTimeoutForAnswer.TabIndex = 2;
            this.numericUpDownTimeoutForAnswer.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Линейный адрес блока отображения";
            // 
            // CommonOptionsForm
            // 
            this.AcceptButton = this.Accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(352, 237);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.numericUpDownDevice);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CommonOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки чтения/записи";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDevice)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDeviceAnswerTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDataCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCountDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeoutForAnswer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Accept;
        public System.Windows.Forms.NumericUpDown numericUpDownDevice;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.NumericUpDown numericUpDownDeviceAnswerTimeout;
        public System.Windows.Forms.NumericUpDown numericUpDownDataCheck;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        public System.Windows.Forms.NumericUpDown numericUpDownCountDo;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.NumericUpDown numericUpDownTimeoutForAnswer;
        private System.Windows.Forms.Label label1;
    }
}