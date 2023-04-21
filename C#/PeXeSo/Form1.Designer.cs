namespace PeXeSo
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
            components = new System.ComponentModel.Container();
            TStart = new Button();
            TRestart = new Button();
            LNavod = new Label();
            LSkore = new Label();
            LFinal = new Label();
            timerr = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // TStart
            // 
            TStart.BackColor = Color.Goldenrod;
            TStart.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            TStart.Location = new Point(261, 239);
            TStart.Name = "TStart";
            TStart.Size = new Size(238, 106);
            TStart.TabIndex = 0;
            TStart.Text = "Start";
            TStart.UseVisualStyleBackColor = false;
            TStart.Click += Start_Click;
            // 
            // TRestart
            // 
            TRestart.BackColor = Color.Goldenrod;
            TRestart.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            TRestart.Location = new Point(261, 239);
            TRestart.Name = "TRestart";
            TRestart.Size = new Size(238, 106);
            TRestart.TabIndex = 1;
            TRestart.Text = "Restart";
            TRestart.UseVisualStyleBackColor = false;
            TRestart.Click += Restart_Click;
            // 
            // LNavod
            // 
            LNavod.AutoSize = true;
            LNavod.Font = new Font("Source Serif Pro", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            LNavod.Location = new Point(12, 417);
            LNavod.Name = "LNavod";
            LNavod.Size = new Size(441, 24);
            LNavod.TabIndex = 2;
            LNavod.Text = "Návod: skóre se počítá jako počet chybných dvouotočení.";
            // 
            // LSkore
            // 
            LSkore.AutoSize = true;
            LSkore.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            LSkore.Location = new Point(784, 410);
            LSkore.Name = "LSkore";
            LSkore.Size = new Size(50, 31);
            LSkore.TabIndex = 3;
            LSkore.Text = "909";
            // 
            // LFinal
            // 
            LFinal.AutoSize = true;
            LFinal.Font = new Font("Stencil", 72F, FontStyle.Bold, GraphicsUnit.Point);
            LFinal.Location = new Point(282, 73);
            LFinal.Name = "LFinal";
            LFinal.Size = new Size(200, 142);
            LFinal.TabIndex = 4;
            LFinal.Text = "53";
            // 
            // timerr
            // 
            timerr.Enabled = true;
            timerr.Interval = 1000;
            timerr.Tag = "timer";
            timerr.Tick += timerr_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(846, 450);
            Controls.Add(LFinal);
            Controls.Add(LSkore);
            Controls.Add(LNavod);
            Controls.Add(TStart);
            Controls.Add(TRestart);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button TStart;
        private Button TRestart;
        private Label LNavod;
        private Label LSkore;
        private Label LFinal;
        private System.Windows.Forms.Timer timerr;
    }
}