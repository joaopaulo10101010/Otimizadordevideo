namespace OtimizadorVideo
{
    partial class BaixarVideo
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
            otimizarbt = new Button();
            titulo = new Label();
            label1 = new Label();
            baixarbtn = new Button();
            youtubelink = new TextBox();
            Labelyoutubelink = new Label();
            labelsaidayoutube = new Label();
            saidavideoyoutube = new TextBox();
            youtubepainel = new Panel();
            baixaraudio = new Button();
            button3 = new Button();
            button2 = new Button();
            panel1 = new Panel();
            button1 = new Button();
            otimizarvalue = new Label();
            qualidadebar = new TrackBar();
            saidapathoti = new Label();
            saidatools = new TextBox();
            youtubepainel.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)qualidadebar).BeginInit();
            SuspendLayout();
            // 
            // carregarvideo
            // 
            carregarvideo.Location = new Point(18, 19);
            carregarvideo.Name = "carregarvideo";
            carregarvideo.Size = new Size(145, 23);
            carregarvideo.TabIndex = 0;
            carregarvideo.Text = "Carregar";
            carregarvideo.UseVisualStyleBackColor = true;
            carregarvideo.Click += carregarvideo_Click;
            // 
            // combovideo
            // 
            combovideo.DropDownStyle = ComboBoxStyle.DropDownList;
            combovideo.FormattingEnabled = true;
            combovideo.Location = new Point(18, 48);
            combovideo.Name = "combovideo";
            combovideo.Size = new Size(145, 23);
            combovideo.TabIndex = 2;
            combovideo.SelectedIndexChanged += combovideo_SelectedIndexChanged;
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
            btextrair.Location = new Point(263, 15);
            btextrair.Name = "btextrair";
            btextrair.Size = new Size(103, 23);
            btextrair.TabIndex = 6;
            btextrair.Text = "Extrair Frames";
            btextrair.UseVisualStyleBackColor = true;
            btextrair.Click += btextrair_Click;
            // 
            // otimizarbt
            // 
            otimizarbt.Location = new Point(608, 15);
            otimizarbt.Name = "otimizarbt";
            otimizarbt.Size = new Size(152, 23);
            otimizarbt.TabIndex = 7;
            otimizarbt.Text = "Otimizar";
            otimizarbt.UseVisualStyleBackColor = true;
            otimizarbt.Click += otimizarbt_Click;
            // 
            // titulo
            // 
            titulo.AutoSize = true;
            titulo.Location = new Point(12, 9);
            titulo.Name = "titulo";
            titulo.Size = new Size(507, 30);
            titulo.TabIndex = 8;
            titulo.Text = "Baixe vídeos do YouTube, otimize o espaço que eles ocupam e extraia seus frames.\r\nCole a URL do vídeo do YouTube para baixá-lo ou carregue localmente o vídeo para otimizá-lo.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Coral;
            label1.Location = new Point(12, 50);
            label1.Name = "label1";
            label1.Size = new Size(606, 15);
            label1.TabIndex = 9;
            label1.Text = "OBS: Esse programa foi feito para videos no formato '.mp4', outros formatos podem gerar resultados inesperados.";
            // 
            // baixarbtn
            // 
            baixarbtn.Location = new Point(622, 15);
            baixarbtn.Name = "baixarbtn";
            baixarbtn.Size = new Size(90, 23);
            baixarbtn.TabIndex = 10;
            baixarbtn.Text = "Baixar Video";
            baixarbtn.UseVisualStyleBackColor = true;
            baixarbtn.Click += button1_Click;
            // 
            // youtubelink
            // 
            youtubelink.Location = new Point(112, 15);
            youtubelink.Name = "youtubelink";
            youtubelink.Size = new Size(475, 23);
            youtubelink.TabIndex = 11;
            // 
            // Labelyoutubelink
            // 
            Labelyoutubelink.AutoSize = true;
            Labelyoutubelink.Location = new Point(27, 19);
            Labelyoutubelink.Name = "Labelyoutubelink";
            Labelyoutubelink.Size = new Size(79, 15);
            Labelyoutubelink.TabIndex = 12;
            Labelyoutubelink.Text = "Youtube Link:";
            Labelyoutubelink.Click += label2_Click;
            // 
            // labelsaidayoutube
            // 
            labelsaidayoutube.AutoSize = true;
            labelsaidayoutube.Location = new Point(41, 48);
            labelsaidayoutube.Name = "labelsaidayoutube";
            labelsaidayoutube.Size = new Size(65, 15);
            labelsaidayoutube.TabIndex = 13;
            labelsaidayoutube.Text = "Saida Path:";
            // 
            // saidavideoyoutube
            // 
            saidavideoyoutube.Location = new Point(112, 44);
            saidavideoyoutube.Name = "saidavideoyoutube";
            saidavideoyoutube.ReadOnly = true;
            saidavideoyoutube.Size = new Size(475, 23);
            saidavideoyoutube.TabIndex = 14;
            // 
            // youtubepainel
            // 
            youtubepainel.BorderStyle = BorderStyle.FixedSingle;
            youtubepainel.Controls.Add(baixaraudio);
            youtubepainel.Controls.Add(button3);
            youtubepainel.Controls.Add(button2);
            youtubepainel.Controls.Add(youtubelink);
            youtubepainel.Controls.Add(saidavideoyoutube);
            youtubepainel.Controls.Add(baixarbtn);
            youtubepainel.Controls.Add(labelsaidayoutube);
            youtubepainel.Controls.Add(Labelyoutubelink);
            youtubepainel.Location = new Point(12, 68);
            youtubepainel.Name = "youtubepainel";
            youtubepainel.Size = new Size(776, 86);
            youtubepainel.TabIndex = 15;
            // 
            // baixaraudio
            // 
            baixaraudio.Location = new Point(622, 44);
            baixaraudio.Name = "baixaraudio";
            baixaraudio.Size = new Size(90, 23);
            baixaraudio.TabIndex = 17;
            baixaraudio.Text = "Baixar Audio";
            baixaraudio.UseVisualStyleBackColor = true;
            baixaraudio.Click += baixaraudio_Click;
            // 
            // button3
            // 
            button3.Location = new Point(584, 15);
            button3.Name = "button3";
            button3.Size = new Size(23, 23);
            button3.TabIndex = 16;
            button3.Text = "📋";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(584, 44);
            button2.Name = "button2";
            button2.Size = new Size(23, 23);
            button2.TabIndex = 15;
            button2.Text = "🔗";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(otimizarvalue);
            panel1.Controls.Add(qualidadebar);
            panel1.Controls.Add(saidapathoti);
            panel1.Controls.Add(saidatools);
            panel1.Controls.Add(otimizarbt);
            panel1.Controls.Add(btextrair);
            panel1.Controls.Add(combovideo);
            panel1.Controls.Add(carregarvideo);
            panel1.Location = new Point(12, 160);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 95);
            panel1.TabIndex = 16;
            // 
            // button1
            // 
            button1.Location = new Point(584, 48);
            button1.Name = "button1";
            button1.Size = new Size(23, 23);
            button1.TabIndex = 12;
            button1.Text = "🔗";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // otimizarvalue
            // 
            otimizarvalue.AutoSize = true;
            otimizarvalue.Location = new Point(741, 56);
            otimizarvalue.Name = "otimizarvalue";
            otimizarvalue.Size = new Size(19, 15);
            otimizarvalue.TabIndex = 11;
            otimizarvalue.Text = "28";
            // 
            // qualidadebar
            // 
            qualidadebar.Location = new Point(608, 44);
            qualidadebar.Maximum = 35;
            qualidadebar.Name = "qualidadebar";
            qualidadebar.Size = new Size(127, 45);
            qualidadebar.TabIndex = 10;
            qualidadebar.Value = 28;
            qualidadebar.Scroll += qualidadebar_Scroll_1;
            // 
            // saidapathoti
            // 
            saidapathoti.AutoSize = true;
            saidapathoti.Location = new Point(192, 51);
            saidapathoti.Name = "saidapathoti";
            saidapathoti.Size = new Size(65, 15);
            saidapathoti.TabIndex = 9;
            saidapathoti.Text = "Saida Path:";
            // 
            // saidatools
            // 
            saidatools.Location = new Point(263, 48);
            saidatools.Name = "saidatools";
            saidatools.ReadOnly = true;
            saidatools.Size = new Size(324, 23);
            saidatools.TabIndex = 8;
            // 
            // BaixarVideo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(youtubepainel);
            Controls.Add(label1);
            Controls.Add(titulo);
            Controls.Add(processolabel);
            Controls.Add(processobar);
            Controls.Add(consoletext);
            Name = "BaixarVideo";
            Text = "BaixarVideo";
            youtubepainel.ResumeLayout(false);
            youtubepainel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)qualidadebar).EndInit();
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
        private Button otimizarbt;
        private Label titulo;
        private Label label1;
        private Button baixarbtn;
        private TextBox youtubelink;
        private Label Labelyoutubelink;
        private Label labelsaidayoutube;
        private TextBox saidavideoyoutube;
        private Panel youtubepainel;
        private Button button3;
        private Button button2;
        private Button baixaraudio;
        private Panel panel1;
        private Label saidapathoti;
        private TextBox saidatools;
        private TrackBar qualidadebar;
        private Label otimizarvalue;
        private Button button1;
    }
}
