namespace MultiThreadLab2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            button1 = new Button();
            checkBox1 = new CheckBox();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(-2, 58);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(802, 397);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.MouseClick += pictureBox1_MouseClick;
            // 
            // button1
            // 
            button1.Location = new Point(24, 14);
            button1.Name = "button1";
            button1.Size = new Size(137, 29);
            button1.TabIndex = 1;
            button1.Text = "GenerateRandom";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnGenerateRandom_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(315, 17);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(111, 24);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "MultiThread";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox_CheckedChanged;
            // 
            // button2
            // 
            button2.Location = new Point(191, 14);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 3;
            button2.Text = "Calculate";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Calculate_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(checkBox1);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button button1;
        private CheckBox checkBox1;
        private Button button2;
    }
}
