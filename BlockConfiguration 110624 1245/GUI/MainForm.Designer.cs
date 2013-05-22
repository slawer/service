namespace BlockConfiguration.GUI
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
            this.listViewIndicators = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.versionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.separatorLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.file = new System.Windows.Forms.ToolStripMenuItem();
            this.load = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.поУмолчаниюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.save = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.device = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.автоматическаяНастройкаБлокаОтображенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.опцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commonOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewIndicators
            // 
            this.listViewIndicators.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewIndicators.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewIndicators.FullRowSelect = true;
            this.listViewIndicators.GridLines = true;
            this.listViewIndicators.Location = new System.Drawing.Point(0, 24);
            this.listViewIndicators.Name = "listViewIndicators";
            this.listViewIndicators.Size = new System.Drawing.Size(683, 392);
            this.listViewIndicators.TabIndex = 10;
            this.listViewIndicators.UseCompatibleStateImageBehavior = false;
            this.listViewIndicators.View = System.Windows.Forms.View.Details;
            this.listViewIndicators.DoubleClick += new System.EventHandler(this.listViewIndicators_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Разъем";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Тип индикатора";
            this.columnHeader2.Width = 106;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Адрес источника";
            this.columnHeader3.Width = 103;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Смещение";
            this.columnHeader4.Width = 85;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Позиция точки";
            this.columnHeader5.Width = 96;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Смещение десятичной точки";
            this.columnHeader6.Width = 167;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionLabel,
            this.separatorLabel,
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 416);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(683, 22);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip";
            // 
            // versionLabel
            // 
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // separatorLabel
            // 
            this.separatorLabel.Name = "separatorLabel";
            this.separatorLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file,
            this.device,
            this.опцииToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(683, 24);
            this.menuStrip.TabIndex = 9;
            this.menuStrip.Text = "menuStrip";
            // 
            // file
            // 
            this.file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.load,
            this.save,
            this.toolStripMenuItem1,
            this.exit});
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(48, 20);
            this.file.Text = "Файл";
            // 
            // load
            // 
            this.load.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFromDevice,
            this.loadFromFile,
            this.поУмолчаниюToolStripMenuItem});
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(152, 22);
            this.load.Text = "Загрузить";
            // 
            // loadFromDevice
            // 
            this.loadFromDevice.Name = "loadFromDevice";
            this.loadFromDevice.Size = new System.Drawing.Size(159, 22);
            this.loadFromDevice.Text = "Из устройства";
            this.loadFromDevice.Click += new System.EventHandler(this.loadFromDevice_Click);
            // 
            // loadFromFile
            // 
            this.loadFromFile.Name = "loadFromFile";
            this.loadFromFile.Size = new System.Drawing.Size(159, 22);
            this.loadFromFile.Text = "Из файла";
            this.loadFromFile.Click += new System.EventHandler(this.loadFromFile_Click);
            // 
            // поУмолчаниюToolStripMenuItem
            // 
            this.поУмолчаниюToolStripMenuItem.Name = "поУмолчаниюToolStripMenuItem";
            this.поУмолчаниюToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.поУмолчаниюToolStripMenuItem.Text = "По умолчанию";
            this.поУмолчаниюToolStripMenuItem.Click += new System.EventHandler(this.поУмолчаниюToolStripMenuItem_Click);
            // 
            // save
            // 
            this.save.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToDevice,
            this.saveToFile});
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(152, 22);
            this.save.Text = "Сохранить";
            // 
            // saveToDevice
            // 
            this.saveToDevice.Name = "saveToDevice";
            this.saveToDevice.Size = new System.Drawing.Size(152, 22);
            this.saveToDevice.Text = "В устройсво";
            this.saveToDevice.Click += new System.EventHandler(this.saveToDevice_Click);
            // 
            // saveToFile
            // 
            this.saveToFile.Name = "saveToFile";
            this.saveToFile.Size = new System.Drawing.Size(152, 22);
            this.saveToFile.Text = "В файл";
            this.saveToFile.Click += new System.EventHandler(this.saveToFile_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(152, 22);
            this.exit.Text = "Выход";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // device
            // 
            this.device.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deviceOptions,
            this.автоматическаяНастройкаБлокаОтображенияToolStripMenuItem});
            this.device.Name = "device";
            this.device.Size = new System.Drawing.Size(82, 20);
            this.device.Text = "Устройство";
            // 
            // deviceOptions
            // 
            this.deviceOptions.Name = "deviceOptions";
            this.deviceOptions.Size = new System.Drawing.Size(247, 22);
            this.deviceOptions.Text = "Настройки блока отображения";
            this.deviceOptions.Click += new System.EventHandler(this.deviceOptions_Click);
            // 
            // автоматическаяНастройкаБлокаОтображенияToolStripMenuItem
            // 
            this.автоматическаяНастройкаБлокаОтображенияToolStripMenuItem.Name = "автоматическаяНастройкаБлокаОтображенияToolStripMenuItem";
            this.автоматическаяНастройкаБлокаОтображенияToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.автоматическаяНастройкаБлокаОтображенияToolStripMenuItem.Text = "Автоматическая настройка БО";
            this.автоматическаяНастройкаБлокаОтображенияToolStripMenuItem.Click += new System.EventHandler(this.автоматическаяНастройкаБлокаОтображенияToolStripMenuItem_Click);
            // 
            // опцииToolStripMenuItem
            // 
            this.опцииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commonOptions});
            this.опцииToolStripMenuItem.Name = "опцииToolStripMenuItem";
            this.опцииToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.опцииToolStripMenuItem.Text = "Опции";
            // 
            // commonOptions
            // 
            this.commonOptions.Name = "commonOptions";
            this.commonOptions.Size = new System.Drawing.Size(175, 22);
            this.commonOptions.Text = "Общие настройки";
            this.commonOptions.Click += new System.EventHandler(this.commonOptions_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Формат EF2XML|*.xml";
            this.saveFileDialog.Title = "Сохранить EPROM";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Формат EF1TXT|*.txt|Формат EF2XMLOLD|*.xml|Формат EF2XML|*.xml";
            this.openFileDialog.FilterIndex = 3;
            this.openFileDialog.Title = "Открыть EPROM";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 438);
            this.Controls.Add(this.listViewIndicators);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Name = "MainForm";
            this.Text = "Нкастройка блока отображения";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewIndicators;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem file;
        private System.Windows.Forms.ToolStripMenuItem load;
        private System.Windows.Forms.ToolStripMenuItem loadFromDevice;
        private System.Windows.Forms.ToolStripMenuItem loadFromFile;
        private System.Windows.Forms.ToolStripMenuItem save;
        private System.Windows.Forms.ToolStripMenuItem saveToDevice;
        private System.Windows.Forms.ToolStripMenuItem saveToFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exit;
        private System.Windows.Forms.ToolStripMenuItem device;
        private System.Windows.Forms.ToolStripMenuItem deviceOptions;
        private System.Windows.Forms.ToolStripMenuItem опцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commonOptions;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripStatusLabel versionLabel;
        private System.Windows.Forms.ToolStripStatusLabel separatorLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem поУмолчаниюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem автоматическаяНастройкаБлокаОтображенияToolStripMenuItem;
    }
}