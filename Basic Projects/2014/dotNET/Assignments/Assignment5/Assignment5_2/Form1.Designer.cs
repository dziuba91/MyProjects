namespace Assignment5_2
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
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.min = new System.Windows.Forms.TextBox();
            this.hour = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.year = new System.Windows.Forms.TextBox();
            this.month = new System.Windows.Forms.TextBox();
            this.day = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.destination = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.origin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flightID = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.min);
            this.groupBox1.Controls.Add(this.hour);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.year);
            this.groupBox1.Controls.Add(this.month);
            this.groupBox1.Controls.Add(this.day);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.destination);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.origin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.flightID);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 277);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FLIGHT";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(60, 244);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "ADD";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(48, 218);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = ":";
            // 
            // min
            // 
            this.min.Location = new System.Drawing.Point(62, 214);
            this.min.Name = "min";
            this.min.Size = new System.Drawing.Size(26, 20);
            this.min.TabIndex = 14;
            this.min.Text = "mm";
            // 
            // hour
            // 
            this.hour.Location = new System.Drawing.Point(18, 214);
            this.hour.Name = "hour";
            this.hour.Size = new System.Drawing.Size(26, 20);
            this.hour.TabIndex = 13;
            this.hour.Text = "hh";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 198);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "time (hour:min)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "/";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "/";
            // 
            // year
            // 
            this.year.Location = new System.Drawing.Point(102, 170);
            this.year.Name = "year";
            this.year.Size = new System.Drawing.Size(53, 20);
            this.year.TabIndex = 9;
            this.year.Text = "yyyy";
            // 
            // month
            // 
            this.month.Location = new System.Drawing.Point(60, 170);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(26, 20);
            this.month.TabIndex = 8;
            this.month.Text = "mm";
            // 
            // day
            // 
            this.day.Location = new System.Drawing.Point(18, 170);
            this.day.Name = "day";
            this.day.Size = new System.Drawing.Size(26, 20);
            this.day.TabIndex = 7;
            this.day.Text = "dd";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "date (day/month/year)";
            // 
            // destination
            // 
            this.destination.Location = new System.Drawing.Point(18, 125);
            this.destination.Name = "destination";
            this.destination.Size = new System.Drawing.Size(153, 20);
            this.destination.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "destination";
            // 
            // origin
            // 
            this.origin.Location = new System.Drawing.Point(18, 81);
            this.origin.Name = "origin";
            this.origin.Size = new System.Drawing.Size(153, 20);
            this.origin.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "origin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "flight ID";
            // 
            // flightID
            // 
            this.flightID.Location = new System.Drawing.Point(18, 38);
            this.flightID.Name = "flightID";
            this.flightID.Size = new System.Drawing.Size(153, 20);
            this.flightID.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 301);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "FLIGHT";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox min;
        private System.Windows.Forms.TextBox hour;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox year;
        private System.Windows.Forms.TextBox month;
        private System.Windows.Forms.TextBox day;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox destination;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox origin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox flightID;
        private System.Windows.Forms.Button button1;
    }
}

