namespace Slingshot
{
    partial class ConfigurationWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.numMutationRate = new System.Windows.Forms.NumericUpDown();
            this.numCrossoverRate = new System.Windows.Forms.NumericUpDown();
            this.numSelectionPressure = new System.Windows.Forms.NumericUpDown();
            this.numPopulationSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMutationRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCrossoverRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionPressure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPopulationSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mutation Rate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Crossover Rate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Selection Pressure";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(189, 206);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(67, 29);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // numMutationRate
            // 
            this.numMutationRate.Location = new System.Drawing.Point(152, 103);
            this.numMutationRate.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMutationRate.Name = "numMutationRate";
            this.numMutationRate.Size = new System.Drawing.Size(104, 20);
            this.numMutationRate.TabIndex = 7;
            // 
            // numCrossoverRate
            // 
            this.numCrossoverRate.Location = new System.Drawing.Point(152, 129);
            this.numCrossoverRate.Name = "numCrossoverRate";
            this.numCrossoverRate.Size = new System.Drawing.Size(104, 20);
            this.numCrossoverRate.TabIndex = 8;
            // 
            // numSelectionPressure
            // 
            this.numSelectionPressure.Location = new System.Drawing.Point(152, 155);
            this.numSelectionPressure.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numSelectionPressure.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSelectionPressure.Name = "numSelectionPressure";
            this.numSelectionPressure.Size = new System.Drawing.Size(104, 20);
            this.numSelectionPressure.TabIndex = 9;
            this.numSelectionPressure.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numPopulationSize
            // 
            this.numPopulationSize.Location = new System.Drawing.Point(152, 33);
            this.numPopulationSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numPopulationSize.Name = "numPopulationSize";
            this.numPopulationSize.Size = new System.Drawing.Size(104, 20);
            this.numPopulationSize.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Population Size";
            // 
            // numDuration
            // 
            this.numDuration.Location = new System.Drawing.Point(152, 59);
            this.numDuration.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numDuration.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(104, 20);
            this.numDuration.TabIndex = 13;
            this.numDuration.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Duration";
            // 
            // ConfigurationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.numDuration);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numPopulationSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numSelectionPressure);
            this.Controls.Add(this.numCrossoverRate);
            this.Controls.Add(this.numMutationRate);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ConfigurationWindow";
            this.Text = "ConfigurationWindow";
            ((System.ComponentModel.ISupportInitialize)(this.numMutationRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCrossoverRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionPressure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPopulationSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.NumericUpDown numMutationRate;
        private System.Windows.Forms.NumericUpDown numCrossoverRate;
        private System.Windows.Forms.NumericUpDown numSelectionPressure;
        private System.Windows.Forms.NumericUpDown numPopulationSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numDuration;
        private System.Windows.Forms.Label label5;
    }
}