
namespace Display
{
    partial class DisplayWindow
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
            this.Start_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Start_Button
            // 
            this.Start_Button.Location = new System.Drawing.Point(293, 148);
            this.Start_Button.Name = "Start_Button";
            this.Start_Button.Size = new System.Drawing.Size(203, 120);
            this.Start_Button.TabIndex = 0;
            this.Start_Button.Text = "start";
            this.Start_Button.UseVisualStyleBackColor = true;
            this.Start_Button.Click += new System.EventHandler(this.Start_Button_Click);
            // 
            // DisplayWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Start_Button);
            this.Name = "DisplayWindow";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Start_Button;
    }
}

