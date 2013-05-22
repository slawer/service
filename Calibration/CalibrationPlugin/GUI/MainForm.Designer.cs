namespace Calibration.CalibrationPlugin.GUI
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.dataGridViewCalibrationTable = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.insertPoint = new System.Windows.Forms.Button();
            this.fromTo = new System.Windows.Forms.Button();
            this.checkBoxCaculateSide = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxToTableCalibrated = new System.Windows.Forms.TextBox();
            this.textBoxTotablePhysic = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxDoScale = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxFromDeviceCalibrated = new System.Windows.Forms.TextBox();
            this.textBoxFromDevicePhysic = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deletePoint = new System.Windows.Forms.Button();
            this.calibrationWrite = new System.Windows.Forms.Button();
            this.calibrationLast = new System.Windows.Forms.Button();
            this.calibrationReset = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.file = new System.Windows.Forms.ToolStripMenuItem();
            this.load = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вУстройсвоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.close = new System.Windows.Forms.ToolStripMenuItem();
            this.channels = new System.Windows.Forms.ToolStripMenuItem();
            this.listOfChannels = new System.Windows.Forms.ToolStripMenuItem();
            this.options = new System.Windows.Forms.ToolStripMenuItem();
            this.commonOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCalibrationTable)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 456);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(654, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // dataGridViewCalibrationTable
            // 
            this.dataGridViewCalibrationTable.AllowUserToAddRows = false;
            this.dataGridViewCalibrationTable.AllowUserToResizeColumns = false;
            this.dataGridViewCalibrationTable.AllowUserToResizeRows = false;
            this.dataGridViewCalibrationTable.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridViewCalibrationTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewCalibrationTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewCalibrationTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewCalibrationTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3});
            this.dataGridViewCalibrationTable.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridViewCalibrationTable.Location = new System.Drawing.Point(439, 36);
            this.dataGridViewCalibrationTable.Name = "dataGridViewCalibrationTable";
            this.dataGridViewCalibrationTable.RowHeadersVisible = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewCalibrationTable.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewCalibrationTable.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridViewCalibrationTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewCalibrationTable.Size = new System.Drawing.Size(203, 267);
            this.dataGridViewCalibrationTable.TabIndex = 20;
            this.dataGridViewCalibrationTable.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewCalibrationTable_CellBeginEdit);
            this.dataGridViewCalibrationTable.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dataGridViewCalibrationTable_CellParsing);
            this.dataGridViewCalibrationTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCalibrationTable_CellEndEdit);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Сигнал";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Значение";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // insertPoint
            // 
            this.insertPoint.Location = new System.Drawing.Point(331, 404);
            this.insertPoint.Name = "insertPoint";
            this.insertPoint.Size = new System.Drawing.Size(102, 23);
            this.insertPoint.TabIndex = 32;
            this.insertPoint.Text = "Добавить точку";
            this.insertPoint.UseVisualStyleBackColor = true;
            this.insertPoint.Click += new System.EventHandler(this.insertPoint_Click);
            // 
            // fromTo
            // 
            this.fromTo.Location = new System.Drawing.Point(197, 349);
            this.fromTo.Name = "fromTo";
            this.fromTo.Size = new System.Drawing.Size(30, 23);
            this.fromTo.TabIndex = 31;
            this.fromTo.Text = "->";
            this.fromTo.UseVisualStyleBackColor = true;
            this.fromTo.Click += new System.EventHandler(this.fromTo_Click);
            // 
            // checkBoxCaculateSide
            // 
            this.checkBoxCaculateSide.AutoSize = true;
            this.checkBoxCaculateSide.Location = new System.Drawing.Point(34, 427);
            this.checkBoxCaculateSide.Name = "checkBoxCaculateSide";
            this.checkBoxCaculateSide.Size = new System.Drawing.Size(136, 17);
            this.checkBoxCaculateSide.TabIndex = 27;
            this.checkBoxCaculateSide.Text = "Расчет крайних точек";
            this.checkBoxCaculateSide.UseVisualStyleBackColor = true;
            this.checkBoxCaculateSide.CheckedChanged += new System.EventHandler(this.checkBoxCaculateSide_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxToTableCalibrated);
            this.groupBox3.Controls.Add(this.textBoxTotablePhysic);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(233, 309);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 89);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Данные для таблицы";
            // 
            // textBoxToTableCalibrated
            // 
            this.textBoxToTableCalibrated.Location = new System.Drawing.Point(81, 47);
            this.textBoxToTableCalibrated.Name = "textBoxToTableCalibrated";
            this.textBoxToTableCalibrated.Size = new System.Drawing.Size(100, 20);
            this.textBoxToTableCalibrated.TabIndex = 7;
            this.textBoxToTableCalibrated.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxTotablePhysic
            // 
            this.textBoxTotablePhysic.Location = new System.Drawing.Point(81, 21);
            this.textBoxTotablePhysic.Name = "textBoxTotablePhysic";
            this.textBoxTotablePhysic.Size = new System.Drawing.Size(100, 20);
            this.textBoxTotablePhysic.TabIndex = 6;
            this.textBoxTotablePhysic.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Значение";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Сигнал";
            // 
            // checkBoxDoScale
            // 
            this.checkBoxDoScale.AutoSize = true;
            this.checkBoxDoScale.Location = new System.Drawing.Point(34, 404);
            this.checkBoxDoScale.Name = "checkBoxDoScale";
            this.checkBoxDoScale.Size = new System.Drawing.Size(222, 17);
            this.checkBoxDoScale.TabIndex = 26;
            this.checkBoxDoScale.Text = "Масштабировать без последней точки";
            this.checkBoxDoScale.UseVisualStyleBackColor = true;
            this.checkBoxDoScale.CheckedChanged += new System.EventHandler(this.checkBoxDoScale_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxFromDeviceCalibrated);
            this.groupBox2.Controls.Add(this.textBoxFromDevicePhysic);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(11, 309);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 89);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Текущие данные";
            // 
            // textBoxFromDeviceCalibrated
            // 
            this.textBoxFromDeviceCalibrated.Location = new System.Drawing.Point(67, 52);
            this.textBoxFromDeviceCalibrated.Name = "textBoxFromDeviceCalibrated";
            this.textBoxFromDeviceCalibrated.Size = new System.Drawing.Size(100, 20);
            this.textBoxFromDeviceCalibrated.TabIndex = 3;
            this.textBoxFromDeviceCalibrated.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxFromDevicePhysic
            // 
            this.textBoxFromDevicePhysic.Location = new System.Drawing.Point(67, 26);
            this.textBoxFromDevicePhysic.Name = "textBoxFromDevicePhysic";
            this.textBoxFromDevicePhysic.ReadOnly = true;
            this.textBoxFromDevicePhysic.Size = new System.Drawing.Size(100, 20);
            this.textBoxFromDevicePhysic.TabIndex = 2;
            this.textBoxFromDevicePhysic.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Значение";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сигнал";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.deletePoint);
            this.groupBox1.Controls.Add(this.calibrationWrite);
            this.groupBox1.Controls.Add(this.calibrationLast);
            this.groupBox1.Controls.Add(this.calibrationReset);
            this.groupBox1.Location = new System.Drawing.Point(439, 309);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 144);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Работа с таблицей";
            // 
            // deletePoint
            // 
            this.deletePoint.Location = new System.Drawing.Point(6, 21);
            this.deletePoint.Name = "deletePoint";
            this.deletePoint.Size = new System.Drawing.Size(191, 23);
            this.deletePoint.TabIndex = 3;
            this.deletePoint.Text = "Удалить точку";
            this.deletePoint.UseVisualStyleBackColor = true;
            this.deletePoint.Click += new System.EventHandler(this.deletePoint_Click);
            // 
            // calibrationWrite
            // 
            this.calibrationWrite.Location = new System.Drawing.Point(6, 108);
            this.calibrationWrite.Name = "calibrationWrite";
            this.calibrationWrite.Size = new System.Drawing.Size(191, 23);
            this.calibrationWrite.TabIndex = 2;
            this.calibrationWrite.Text = "Записать в устройство";
            this.calibrationWrite.UseVisualStyleBackColor = true;
            this.calibrationWrite.Click += new System.EventHandler(this.calibrationWrite_Click);
            // 
            // calibrationLast
            // 
            this.calibrationLast.Location = new System.Drawing.Point(6, 79);
            this.calibrationLast.Name = "calibrationLast";
            this.calibrationLast.Size = new System.Drawing.Size(191, 23);
            this.calibrationLast.TabIndex = 1;
            this.calibrationLast.Text = "Вернуться к исходной таблице";
            this.calibrationLast.UseVisualStyleBackColor = true;
            this.calibrationLast.Click += new System.EventHandler(this.calibrationLast_Click);
            // 
            // calibrationReset
            // 
            this.calibrationReset.Location = new System.Drawing.Point(6, 50);
            this.calibrationReset.Name = "calibrationReset";
            this.calibrationReset.Size = new System.Drawing.Size(191, 23);
            this.calibrationReset.TabIndex = 0;
            this.calibrationReset.Text = "Сброс таблицы";
            this.calibrationReset.UseVisualStyleBackColor = true;
            this.calibrationReset.Click += new System.EventHandler(this.calibrationReset_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file,
            this.channels,
            this.options});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(654, 24);
            this.menuStrip.TabIndex = 33;
            this.menuStrip.Text = "menuStrip1";
            // 
            // file
            // 
            this.file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.load,
            this.сохранитьToolStripMenuItem,
            this.toolStripMenuItem1,
            this.close});
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(48, 20);
            this.file.Text = "Файл";
            // 
            // load
            // 
            this.load.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFromDevice,
            this.loadFromFile});
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(152, 22);
            this.load.Text = "Загрузить";
            // 
            // loadFromDevice
            // 
            this.loadFromDevice.Name = "loadFromDevice";
            this.loadFromDevice.Size = new System.Drawing.Size(152, 22);
            this.loadFromDevice.Text = "Из устройства";
            this.loadFromDevice.Click += new System.EventHandler(this.loadFromDevice_Click);
            // 
            // loadFromFile
            // 
            this.loadFromFile.Name = "loadFromFile";
            this.loadFromFile.Size = new System.Drawing.Size(152, 22);
            this.loadFromFile.Text = "Из файла";
            this.loadFromFile.Click += new System.EventHandler(this.loadFromFile_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вУстройсвоToolStripMenuItem,
            this.вФайлToolStripMenuItem});
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            // 
            // вУстройсвоToolStripMenuItem
            // 
            this.вУстройсвоToolStripMenuItem.Name = "вУстройсвоToolStripMenuItem";
            this.вУстройсвоToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.вУстройсвоToolStripMenuItem.Text = "В устройсво";
            this.вУстройсвоToolStripMenuItem.Click += new System.EventHandler(this.вУстройсвоToolStripMenuItem_Click);
            // 
            // вФайлToolStripMenuItem
            // 
            this.вФайлToolStripMenuItem.Name = "вФайлToolStripMenuItem";
            this.вФайлToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.вФайлToolStripMenuItem.Text = "В файл";
            this.вФайлToolStripMenuItem.Click += new System.EventHandler(this.вФайлToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(129, 6);
            // 
            // close
            // 
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(132, 22);
            this.close.Text = "Закрыть";
            // 
            // channels
            // 
            this.channels.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listOfChannels});
            this.channels.Name = "channels";
            this.channels.Size = new System.Drawing.Size(61, 20);
            this.channels.Text = "Каналы";
            // 
            // listOfChannels
            // 
            this.listOfChannels.Name = "listOfChannels";
            this.listOfChannels.Size = new System.Drawing.Size(163, 22);
            this.listOfChannels.Text = "Список каналов";
            this.listOfChannels.Click += new System.EventHandler(this.listOfChannels_Click);
            // 
            // options
            // 
            this.options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commonOptions});
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(79, 20);
            this.options.Text = "Настройки";
            // 
            // commonOptions
            // 
            this.commonOptions.Name = "commonOptions";
            this.commonOptions.Size = new System.Drawing.Size(175, 22);
            this.commonOptions.Text = "Общие настройки";
            this.commonOptions.Click += new System.EventHandler(this.commonOptions_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Формат EF1TXT|*.txt|Формат EF2XMLOLD|*.xml|Формат EF2XML|*.xml";
            this.openFileDialog.FilterIndex = 3;
            this.openFileDialog.Title = "Открыть EPROM";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Формат EF2XML|*.xml";
            this.saveFileDialog.FilterIndex = 0;
            this.saveFileDialog.Title = "Сохранить EPROM";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 478);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.insertPoint);
            this.Controls.Add(this.fromTo);
            this.Controls.Add(this.checkBoxCaculateSide);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.checkBoxDoScale);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewCalibrationTable);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Калибровка";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.onPaint);
            this.Shown += new System.EventHandler(this.onShown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCalibrationTable)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.DataGridView dataGridViewCalibrationTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button insertPoint;
        private System.Windows.Forms.Button fromTo;
        private System.Windows.Forms.CheckBox checkBoxCaculateSide;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxToTableCalibrated;
        private System.Windows.Forms.TextBox textBoxTotablePhysic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxDoScale;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxFromDeviceCalibrated;
        private System.Windows.Forms.TextBox textBoxFromDevicePhysic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button deletePoint;
        private System.Windows.Forms.Button calibrationWrite;
        private System.Windows.Forms.Button calibrationLast;
        private System.Windows.Forms.Button calibrationReset;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem file;
        private System.Windows.Forms.ToolStripMenuItem load;
        private System.Windows.Forms.ToolStripMenuItem loadFromDevice;
        private System.Windows.Forms.ToolStripMenuItem loadFromFile;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вУстройсвоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem close;
        private System.Windows.Forms.ToolStripMenuItem channels;
        private System.Windows.Forms.ToolStripMenuItem listOfChannels;
        private System.Windows.Forms.ToolStripMenuItem options;
        private System.Windows.Forms.ToolStripMenuItem commonOptions;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}