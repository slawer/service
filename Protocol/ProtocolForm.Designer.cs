namespace Protocol
{
    partial class ProtocolForm
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
            this.listBox = new System.Windows.Forms.ListBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.countPackets = new System.Windows.Forms.NumericUpDown();
            this.checkBoxShowProtocol = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.countPackets)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 15;
            this.listBox.Location = new System.Drawing.Point(12, 12);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(571, 469);
            this.listBox.TabIndex = 0;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 50;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // countPackets
            // 
            this.countPackets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.countPackets.Location = new System.Drawing.Point(270, 494);
            this.countPackets.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.countPackets.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countPackets.Name = "countPackets";
            this.countPackets.Size = new System.Drawing.Size(67, 20);
            this.countPackets.TabIndex = 8;
            this.countPackets.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.countPackets.ValueChanged += new System.EventHandler(this.countPackets_ValueChanged);
            // 
            // checkBoxShowProtocol
            // 
            this.checkBoxShowProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxShowProtocol.AutoSize = true;
            this.checkBoxShowProtocol.Checked = true;
            this.checkBoxShowProtocol.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowProtocol.Location = new System.Drawing.Point(39, 495);
            this.checkBoxShowProtocol.Name = "checkBoxShowProtocol";
            this.checkBoxShowProtocol.Size = new System.Drawing.Size(126, 17);
            this.checkBoxShowProtocol.TabIndex = 7;
            this.checkBoxShowProtocol.Text = "Выводить протокол";
            this.checkBoxShowProtocol.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 496);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Строк протокола";
            // 
            // ProtocolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 527);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.countPackets);
            this.Controls.Add(this.checkBoxShowProtocol);
            this.Controls.Add(this.listBox);
            this.Name = "ProtocolForm";
            this.Text = "Протокол обмена";
            ((System.ComponentModel.ISupportInitialize)(this.countPackets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.NumericUpDown countPackets;
        private System.Windows.Forms.CheckBox checkBoxShowProtocol;
        private System.Windows.Forms.Label label1;
    }
}