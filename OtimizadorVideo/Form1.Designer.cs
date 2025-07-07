namespace OtimizadorVideo
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
            carregarvideo = new Button();
            combovideo = new ComboBox();
            consoletext = new TextBox();
            processobar = new ProgressBar();
            processolabel = new Label();
            btextrair = new Button();
            SuspendLayout();
            // 
            // carregarvideo
            // 
            carregarvideo.Location = new Point(40, 112);
            carregarvideo.Name = "carregarvideo";
            carregarvideo.Size = new Size(124, 23);
            carregarvideo.TabIndex = 0;
            carregarvideo.Text = "Carregar";
            carregarvideo.UseVisualStyleBackColor = true;
            carregarvideo.Click += carregarvideo_Click;
            // 
            // combovideo
            // 
            combovideo.DropDownStyle = ComboBoxStyle.DropDownList;
            combovideo.FormattingEnabled = true;
            combovideo.Location = new Point(236, 112);
            combovideo.Name = "combovideo";
            combovideo.Size = new Size(124, 23);
            combovideo.TabIndex = 2;
            // 
            // consoletext
            // 
            consoletext.Location = new Point(12, 274);
            consoletext.Multiline = true;
            consoletext.Name = "consoletext";
            consoletext.ReadOnly = true;
            consoletext.ScrollBars = ScrollBars.Vertical;
            consoletext.Size = new Size(776, 120);
            consoletext.TabIndex = 3;
            // 
            // processobar
            // 
            processobar.Location = new Point(12, 419);
            processobar.Name = "processobar";
            processobar.Size = new Size(776, 23);
            processobar.TabIndex = 4;
            // 
            // processolabel
            // 
            processolabel.AutoSize = true;
            processolabel.Location = new Point(12, 401);
            processolabel.Name = "processolabel";
            processolabel.Size = new Size(60, 15);
            processolabel.TabIndex = 5;
            processolabel.Text = "Processo: ";
            // 
            // btextrair
            // 
            btextrair.Location = new Point(236, 141);
            btextrair.Name = "btextrair";
            btextrair.Size = new Size(124, 23);
            btextrair.TabIndex = 6;
            btextrair.Text = "Extrair Frames";
            btextrair.UseVisualStyleBackColor = true;
            btextrair.Click += btextrair_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btextrair);
            Controls.Add(processolabel);
            Controls.Add(processobar);
            Controls.Add(consoletext);
            Controls.Add(combovideo);
            Controls.Add(carregarvideo);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button carregarvideo;
        private ComboBox combovideo;
        private TextBox consoletext;
        private ProgressBar processobar;
        private Label processolabel;
        private Button btextrair;
    }
}
