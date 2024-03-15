namespace zoomir1
{
    partial class talon
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btntalon = new System.Windows.Forms.Button();
            this.comboBoxPickupPoints = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(652, 379);
            this.dataGridView1.TabIndex = 1;
            // 
            // btntalon
            // 
            this.btntalon.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btntalon.Location = new System.Drawing.Point(723, 355);
            this.btntalon.Name = "btntalon";
            this.btntalon.Size = new System.Drawing.Size(153, 36);
            this.btntalon.TabIndex = 7;
            this.btntalon.Text = "Талон";
            this.btntalon.UseVisualStyleBackColor = true;
            this.btntalon.Click += new System.EventHandler(this.btntalon_Click);
            // 
            // comboBoxPickupPoints
            // 
            this.comboBoxPickupPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxPickupPoints.FormattingEnabled = true;
            this.comboBoxPickupPoints.Items.AddRange(new object[] {
            "Центральный пункт выдачи",
            "Коминтерновский пункт выдачи",
            "Левобережный пункт выдачи"});
            this.comboBoxPickupPoints.Location = new System.Drawing.Point(675, 12);
            this.comboBoxPickupPoints.Name = "comboBoxPickupPoints";
            this.comboBoxPickupPoints.Size = new System.Drawing.Size(234, 28);
            this.comboBoxPickupPoints.TabIndex = 9;
            this.comboBoxPickupPoints.SelectedIndexChanged += new System.EventHandler(this.comboBoxPickupPoints_SelectedIndexChanged);
            // 
            // talon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(921, 405);
            this.Controls.Add(this.comboBoxPickupPoints);
            this.Controls.Add(this.btntalon);
            this.Controls.Add(this.dataGridView1);
            this.Name = "talon";
            this.Text = "talon";
            this.Load += new System.EventHandler(this.talon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btntalon;
        private System.Windows.Forms.ComboBox comboBoxPickupPoints;
    }
}