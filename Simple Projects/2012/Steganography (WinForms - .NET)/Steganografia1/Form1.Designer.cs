namespace Steganografia
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.OryObraz = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ZakObraz = new System.Windows.Forms.PictureBox();
            this.zakoduj = new System.Windows.Forms.Button();
            this.odkoduj = new System.Windows.Forms.Button();
            this.ZaladujOrg = new System.Windows.Forms.Button();
            this.ZaladujZak = new System.Windows.Forms.Button();
            this.text1 = new System.Windows.Forms.TextBox();
            this.ZalText = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tekstInfo = new System.Windows.Forms.Label();
            this.obrazInfo = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.zakodowanyInfo = new System.Windows.Forms.Label();
            this.wyczyśćPolaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wyczyśćWszystkoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ustawieniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ustawSposóbKodowanieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oProgramieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oProgramieToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Wykres = new Triskelion.PieChart();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OryObraz)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZakObraz)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Wykres)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 251);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Oryginał";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.OryObraz);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 226);
            this.panel1.TabIndex = 0;
            // 
            // OryObraz
            // 
            this.OryObraz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OryObraz.Location = new System.Drawing.Point(0, 0);
            this.OryObraz.Name = "OryObraz";
            this.OryObraz.Size = new System.Drawing.Size(253, 226);
            this.OryObraz.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OryObraz.TabIndex = 0;
            this.OryObraz.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Location = new System.Drawing.Point(385, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 251);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Zakodowany";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ZakObraz);
            this.panel2.Location = new System.Drawing.Point(6, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(253, 226);
            this.panel2.TabIndex = 1;
            // 
            // ZakObraz
            // 
            this.ZakObraz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZakObraz.Location = new System.Drawing.Point(0, 0);
            this.ZakObraz.Name = "ZakObraz";
            this.ZakObraz.Size = new System.Drawing.Size(253, 226);
            this.ZakObraz.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ZakObraz.TabIndex = 1;
            this.ZakObraz.TabStop = false;
            // 
            // zakoduj
            // 
            this.zakoduj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.zakoduj.Location = new System.Drawing.Point(283, 95);
            this.zakoduj.Name = "zakoduj";
            this.zakoduj.Size = new System.Drawing.Size(96, 34);
            this.zakoduj.TabIndex = 2;
            this.zakoduj.Text = "Zakoduj >>";
            this.zakoduj.UseVisualStyleBackColor = true;
            this.zakoduj.Click += new System.EventHandler(this.zakoduj_Click);
            // 
            // odkoduj
            // 
            this.odkoduj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.odkoduj.ForeColor = System.Drawing.SystemColors.ControlText;
            this.odkoduj.Location = new System.Drawing.Point(283, 197);
            this.odkoduj.Name = "odkoduj";
            this.odkoduj.Size = new System.Drawing.Size(96, 35);
            this.odkoduj.TabIndex = 3;
            this.odkoduj.Text = "<< Odkoduj";
            this.odkoduj.UseVisualStyleBackColor = true;
            this.odkoduj.Click += new System.EventHandler(this.odkoduj_Click);
            // 
            // ZaladujOrg
            // 
            this.ZaladujOrg.Location = new System.Drawing.Point(12, 293);
            this.ZaladujOrg.Name = "ZaladujOrg";
            this.ZaladujOrg.Size = new System.Drawing.Size(265, 23);
            this.ZaladujOrg.TabIndex = 4;
            this.ZaladujOrg.Text = "Załaduj obraz";
            this.ZaladujOrg.UseVisualStyleBackColor = true;
            this.ZaladujOrg.Click += new System.EventHandler(this.ZaladujOrg_Click);
            // 
            // ZaladujZak
            // 
            this.ZaladujZak.Location = new System.Drawing.Point(385, 294);
            this.ZaladujZak.Name = "ZaladujZak";
            this.ZaladujZak.Size = new System.Drawing.Size(265, 23);
            this.ZaladujZak.TabIndex = 5;
            this.ZaladujZak.Text = "Załaduj obraz";
            this.ZaladujZak.UseVisualStyleBackColor = true;
            this.ZaladujZak.Click += new System.EventHandler(this.ZaladujZak_Click);
            // 
            // text1
            // 
            this.text1.Location = new System.Drawing.Point(12, 336);
            this.text1.Multiline = true;
            this.text1.Name = "text1";
            this.text1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text1.Size = new System.Drawing.Size(265, 74);
            this.text1.TabIndex = 6;
            this.text1.TextChanged += new System.EventHandler(this.text1_TextChanged);
            // 
            // ZalText
            // 
            this.ZalText.Location = new System.Drawing.Point(12, 416);
            this.ZalText.Name = "ZalText";
            this.ZalText.Size = new System.Drawing.Size(265, 23);
            this.ZalText.TabIndex = 7;
            this.ZalText.Text = "Załaduj tekst";
            this.ZalText.UseVisualStyleBackColor = true;
            this.ZalText.Click += new System.EventHandler(this.ZalText_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Bitmapy (*.bmp)|*.bmp|Wszystkie pliki (*.*)|*.*||";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "Pliki tekstowe (*.txt)|*.txt|PDF (*.pdf)|*.pdf|Wszystkie pliki (*.*)|*.*||";
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 485);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(662, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(366, 17);
            this.toolStripStatusLabel1.Text = "Witamy w programie: \"Steganografia\".  Uzupełnij odpowiednie pola.";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Bitmapy (*.bmp)|*.bmp";
            this.saveFileDialog1.Title = "Zapisz obraz";
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.progressBar1.Location = new System.Drawing.Point(12, 452);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(265, 22);
            this.progressBar1.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 457);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Pozostało: 0/0 [znaków]";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabControl1);
            this.groupBox3.Location = new System.Drawing.Point(292, 324);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(358, 150);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Informacje";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(346, 127);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tekstInfo);
            this.tabPage1.Controls.Add(this.obrazInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(338, 101);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Okno Oryginał";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(188, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tekst:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(23, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Obraz:";
            // 
            // tekstInfo
            // 
            this.tekstInfo.Location = new System.Drawing.Point(175, 21);
            this.tekstInfo.Name = "tekstInfo";
            this.tekstInfo.Size = new System.Drawing.Size(160, 74);
            this.tekstInfo.TabIndex = 3;
            // 
            // obrazInfo
            // 
            this.obrazInfo.Location = new System.Drawing.Point(3, 21);
            this.obrazInfo.Name = "obrazInfo";
            this.obrazInfo.Size = new System.Drawing.Size(158, 74);
            this.obrazInfo.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.zakodowanyInfo);
            this.tabPage2.Controls.Add(this.Wykres);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(338, 101);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Okno Zakodowany";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(23, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Obraz:";
            // 
            // zakodowanyInfo
            // 
            this.zakodowanyInfo.Location = new System.Drawing.Point(3, 21);
            this.zakodowanyInfo.Name = "zakodowanyInfo";
            this.zakodowanyInfo.Size = new System.Drawing.Size(158, 74);
            this.zakodowanyInfo.TabIndex = 3;
            // 
            // wyczyśćPolaToolStripMenuItem
            // 
            this.wyczyśćPolaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wyczyśćWszystkoToolStripMenuItem});
            this.wyczyśćPolaToolStripMenuItem.Name = "wyczyśćPolaToolStripMenuItem";
            this.wyczyśćPolaToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.wyczyśćPolaToolStripMenuItem.Text = "Wyczyść pola";
            // 
            // wyczyśćWszystkoToolStripMenuItem
            // 
            this.wyczyśćWszystkoToolStripMenuItem.Name = "wyczyśćWszystkoToolStripMenuItem";
            this.wyczyśćWszystkoToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.wyczyśćWszystkoToolStripMenuItem.Text = "Wyczyść wszystko";
            this.wyczyśćWszystkoToolStripMenuItem.Click += new System.EventHandler(this.wyczyśćWszystkoToolStripMenuItem_Click);
            // 
            // ustawieniaToolStripMenuItem
            // 
            this.ustawieniaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ustawSposóbKodowanieToolStripMenuItem});
            this.ustawieniaToolStripMenuItem.Name = "ustawieniaToolStripMenuItem";
            this.ustawieniaToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.ustawieniaToolStripMenuItem.Text = "Ustawienia";
            // 
            // ustawSposóbKodowanieToolStripMenuItem
            // 
            this.ustawSposóbKodowanieToolStripMenuItem.Name = "ustawSposóbKodowanieToolStripMenuItem";
            this.ustawSposóbKodowanieToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.ustawSposóbKodowanieToolStripMenuItem.Text = "Ustaw sposób kodowania";
            this.ustawSposóbKodowanieToolStripMenuItem.Click += new System.EventHandler(this.ustawSposóbKodowanieToolStripMenuItem_Click);
            // 
            // oProgramieToolStripMenuItem
            // 
            this.oProgramieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oProgramieToolStripMenuItem1});
            this.oProgramieToolStripMenuItem.Name = "oProgramieToolStripMenuItem";
            this.oProgramieToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.oProgramieToolStripMenuItem.Text = "Info";
            // 
            // oProgramieToolStripMenuItem1
            // 
            this.oProgramieToolStripMenuItem1.Name = "oProgramieToolStripMenuItem1";
            this.oProgramieToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.oProgramieToolStripMenuItem1.Text = "O programie";
            this.oProgramieToolStripMenuItem1.Click += new System.EventHandler(this.oProgramieToolStripMenuItem1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wyczyśćPolaToolStripMenuItem,
            this.ustawieniaToolStripMenuItem,
            this.oProgramieToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(662, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Wykres
            // 
            this.Wykres.Location = new System.Drawing.Point(167, 5);
            this.Wykres.Name = "Wykres";
            this.Wykres.Size = new System.Drawing.Size(155, 92);
            this.Wykres.TabIndex = 0;
            this.Wykres.TabStop = false;
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "Pliki tekstowe (*.txt)|*.txt|PDF (*.pdf)|*.pdf";
            this.saveFileDialog2.Title = "Zapisz plik ukryty";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(662, 507);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.ZalText);
            this.Controls.Add(this.text1);
            this.Controls.Add(this.ZaladujZak);
            this.Controls.Add(this.ZaladujOrg);
            this.Controls.Add(this.odkoduj);
            this.Controls.Add(this.zakoduj);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Steganografia";
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OryObraz)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ZakObraz)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Wykres)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox OryObraz;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox ZakObraz;
        private System.Windows.Forms.Button zakoduj;
        private System.Windows.Forms.Button odkoduj;
        private System.Windows.Forms.Button ZaladujOrg;
        private System.Windows.Forms.Button ZaladujZak;
        private System.Windows.Forms.TextBox text1;
        private System.Windows.Forms.Button ZalText;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem wyczyśćPolaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ustawieniaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oProgramieToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ustawSposóbKodowanieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wyczyśćWszystkoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oProgramieToolStripMenuItem1;
        private Triskelion.PieChart Wykres;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label tekstInfo;
        private System.Windows.Forms.Label obrazInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label zakodowanyInfo;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
    }
}

