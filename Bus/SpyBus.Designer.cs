namespace Bus
{
    partial class SpyBus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpyBus));
            this.label_gosNum = new System.Windows.Forms.Label();
            this.textBox_gosNum = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.listBox_info = new System.Windows.Forms.ListBox();
            this.label_results = new System.Windows.Forms.Label();
            this.button_showOnMap = new System.Windows.Forms.Button();
            this.listBox_sr = new System.Windows.Forms.ListBox();
            this.button_showBusInfo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_gosNum
            // 
            this.label_gosNum.AutoSize = true;
            this.label_gosNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_gosNum.Location = new System.Drawing.Point(9, 13);
            this.label_gosNum.Name = "label_gosNum";
            this.label_gosNum.Size = new System.Drawing.Size(203, 16);
            this.label_gosNum.TabIndex = 0;
            this.label_gosNum.Text = "Введите гос. номер автобуса:";
            // 
            // textBox_gosNum
            // 
            this.textBox_gosNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_gosNum.Location = new System.Drawing.Point(12, 32);
            this.textBox_gosNum.Name = "textBox_gosNum";
            this.textBox_gosNum.Size = new System.Drawing.Size(290, 22);
            this.textBox_gosNum.TabIndex = 1;
            // 
            // button_OK
            // 
            this.button_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_OK.Location = new System.Drawing.Point(308, 32);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // listBox_info
            // 
            this.listBox_info.Enabled = false;
            this.listBox_info.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox_info.FormattingEnabled = true;
            this.listBox_info.ItemHeight = 16;
            this.listBox_info.Location = new System.Drawing.Point(12, 296);
            this.listBox_info.Name = "listBox_info";
            this.listBox_info.Size = new System.Drawing.Size(371, 148);
            this.listBox_info.TabIndex = 3;
            // 
            // label_results
            // 
            this.label_results.AutoSize = true;
            this.label_results.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_results.Location = new System.Drawing.Point(9, 85);
            this.label_results.Name = "label_results";
            this.label_results.Size = new System.Drawing.Size(139, 16);
            this.label_results.TabIndex = 4;
            this.label_results.Text = "Результаты поиска:";
            // 
            // button_showOnMap
            // 
            this.button_showOnMap.Enabled = false;
            this.button_showOnMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_showOnMap.Location = new System.Drawing.Point(12, 450);
            this.button_showOnMap.Name = "button_showOnMap";
            this.button_showOnMap.Size = new System.Drawing.Size(371, 23);
            this.button_showOnMap.TabIndex = 5;
            this.button_showOnMap.Text = "Примерное местоположение на карте";
            this.button_showOnMap.UseVisualStyleBackColor = true;
            this.button_showOnMap.Click += new System.EventHandler(this.button_showOnMap_Click);
            // 
            // listBox_sr
            // 
            this.listBox_sr.Enabled = false;
            this.listBox_sr.FormattingEnabled = true;
            this.listBox_sr.Location = new System.Drawing.Point(12, 104);
            this.listBox_sr.Name = "listBox_sr";
            this.listBox_sr.Size = new System.Drawing.Size(371, 121);
            this.listBox_sr.TabIndex = 6;
            // 
            // button_showBusInfo
            // 
            this.button_showBusInfo.Enabled = false;
            this.button_showBusInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_showBusInfo.Location = new System.Drawing.Point(12, 231);
            this.button_showBusInfo.Name = "button_showBusInfo";
            this.button_showBusInfo.Size = new System.Drawing.Size(371, 23);
            this.button_showBusInfo.TabIndex = 7;
            this.button_showBusInfo.Text = "Показать подробную информацию об автобусе";
            this.button_showBusInfo.UseVisualStyleBackColor = true;
            this.button_showBusInfo.Click += new System.EventHandler(this.button_showBusInfo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 277);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Подробная информация об автобусе:";
            // 
            // SpyBus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 485);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_showBusInfo);
            this.Controls.Add(this.listBox_sr);
            this.Controls.Add(this.button_showOnMap);
            this.Controls.Add(this.label_results);
            this.Controls.Add(this.listBox_info);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.textBox_gosNum);
            this.Controls.Add(this.label_gosNum);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpyBus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpyBus";
            this.Load += new System.EventHandler(this.SpyBus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_gosNum;
        private System.Windows.Forms.TextBox textBox_gosNum;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.ListBox listBox_info;
        private System.Windows.Forms.Label label_results;
        private System.Windows.Forms.Button button_showOnMap;
        private System.Windows.Forms.ListBox listBox_sr;
        private System.Windows.Forms.Button button_showBusInfo;
        private System.Windows.Forms.Label label1;
    }
}