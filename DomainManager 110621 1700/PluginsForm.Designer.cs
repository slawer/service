namespace Platform
{
    partial class PluginsForm
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
            this.listView = new System.Windows.Forms.ListView();
            this.number = new System.Windows.Forms.ColumnHeader();
            this.name = new System.Windows.Forms.ColumnHeader();
            this.Author = new System.Windows.Forms.ColumnHeader();
            this.description = new System.Windows.Forms.ColumnHeader();
            this.version = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.number,
            this.name,
            this.Author,
            this.description,
            this.version});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(777, 447);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // number
            // 
            this.number.Text = "#";
            this.number.Width = 32;
            // 
            // name
            // 
            this.name.Text = "Плагин";
            this.name.Width = 171;
            // 
            // Author
            // 
            this.Author.Text = "Автор";
            this.Author.Width = 176;
            // 
            // description
            // 
            this.description.Text = "Описание";
            this.description.Width = 264;
            // 
            // version
            // 
            this.version.Text = "Версия";
            this.version.Width = 103;
            // 
            // PluginsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 447);
            this.Controls.Add(this.listView);
            this.Name = "PluginsForm";
            this.Text = "Установленные компоненты";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader number;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.ColumnHeader Author;
        private System.Windows.Forms.ColumnHeader description;
        private System.Windows.Forms.ColumnHeader version;
        public System.Windows.Forms.ListView listView;
    }
}