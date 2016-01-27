namespace Assignment8
{
    partial class MainForm
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.ascendingSortRadioButton = new System.Windows.Forms.RadioButton();
            this.descendingSortRadioButton = new System.Windows.Forms.RadioButton();
            this.noSortingRadioButton = new System.Windows.Forms.RadioButton();
            this.showNameCheckBox = new System.Windows.Forms.CheckBox();
            this.showIdCheckBox = new System.Windows.Forms.CheckBox();
            this.showPriceCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 21);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(305, 108);
            this.listBox1.TabIndex = 0;
            this.listBox1.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.listBox1_Format);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(331, 21);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(158, 108);
            this.listBox2.TabIndex = 1;
            // 
            // ascendingSortRadioButton
            // 
            this.ascendingSortRadioButton.AutoSize = true;
            this.ascendingSortRadioButton.Location = new System.Drawing.Point(65, 149);
            this.ascendingSortRadioButton.Name = "ascendingSortRadioButton";
            this.ascendingSortRadioButton.Size = new System.Drawing.Size(137, 17);
            this.ascendingSortRadioButton.TabIndex = 2;
            this.ascendingSortRadioButton.TabStop = true;
            this.ascendingSortRadioButton.Text = "Sort in ascending order ";
            this.ascendingSortRadioButton.UseVisualStyleBackColor = true;
            // 
            // descendingSortRadioButton
            // 
            this.descendingSortRadioButton.AutoSize = true;
            this.descendingSortRadioButton.Location = new System.Drawing.Point(65, 173);
            this.descendingSortRadioButton.Name = "descendingSortRadioButton";
            this.descendingSortRadioButton.Size = new System.Drawing.Size(140, 17);
            this.descendingSortRadioButton.TabIndex = 3;
            this.descendingSortRadioButton.TabStop = true;
            this.descendingSortRadioButton.Text = "Sort in descending order";
            this.descendingSortRadioButton.UseVisualStyleBackColor = true;
            // 
            // noSortingRadioButton
            // 
            this.noSortingRadioButton.AutoSize = true;
            this.noSortingRadioButton.Location = new System.Drawing.Point(65, 197);
            this.noSortingRadioButton.Name = "noSortingRadioButton";
            this.noSortingRadioButton.Size = new System.Drawing.Size(73, 17);
            this.noSortingRadioButton.TabIndex = 4;
            this.noSortingRadioButton.TabStop = true;
            this.noSortingRadioButton.Text = "No sorting";
            this.noSortingRadioButton.UseVisualStyleBackColor = true;
            // 
            // showNameCheckBox
            // 
            this.showNameCheckBox.AutoSize = true;
            this.showNameCheckBox.Location = new System.Drawing.Point(301, 150);
            this.showNameCheckBox.Name = "showNameCheckBox";
            this.showNameCheckBox.Size = new System.Drawing.Size(116, 17);
            this.showNameCheckBox.TabIndex = 5;
            this.showNameCheckBox.Text = "Show Article Name";
            this.showNameCheckBox.UseVisualStyleBackColor = true;
            this.showNameCheckBox.CheckedChanged += new System.EventHandler(this.showNameCheckBox_CheckedChanged);
            // 
            // showIdCheckBox
            // 
            this.showIdCheckBox.AutoSize = true;
            this.showIdCheckBox.Location = new System.Drawing.Point(301, 174);
            this.showIdCheckBox.Name = "showIdCheckBox";
            this.showIdCheckBox.Size = new System.Drawing.Size(99, 17);
            this.showIdCheckBox.TabIndex = 6;
            this.showIdCheckBox.Text = "Show Article ID";
            this.showIdCheckBox.UseVisualStyleBackColor = true;
            this.showIdCheckBox.CheckedChanged += new System.EventHandler(this.showIdCheckBox_CheckedChanged);
            // 
            // showPriceCheckBox
            // 
            this.showPriceCheckBox.AutoSize = true;
            this.showPriceCheckBox.Location = new System.Drawing.Point(301, 198);
            this.showPriceCheckBox.Name = "showPriceCheckBox";
            this.showPriceCheckBox.Size = new System.Drawing.Size(112, 17);
            this.showPriceCheckBox.TabIndex = 7;
            this.showPriceCheckBox.Text = "Show Article Price";
            this.showPriceCheckBox.UseVisualStyleBackColor = true;
            this.showPriceCheckBox.CheckedChanged += new System.EventHandler(this.showPriceCheckBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 235);
            this.Controls.Add(this.showPriceCheckBox);
            this.Controls.Add(this.showIdCheckBox);
            this.Controls.Add(this.showNameCheckBox);
            this.Controls.Add(this.noSortingRadioButton);
            this.Controls.Add(this.descendingSortRadioButton);
            this.Controls.Add(this.ascendingSortRadioButton);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Name = "MainForm";
            this.Text = "Assignment 8";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.RadioButton ascendingSortRadioButton;
        private System.Windows.Forms.RadioButton descendingSortRadioButton;
        private System.Windows.Forms.RadioButton noSortingRadioButton;
        private System.Windows.Forms.CheckBox showNameCheckBox;
        private System.Windows.Forms.CheckBox showIdCheckBox;
        private System.Windows.Forms.CheckBox showPriceCheckBox;
    }
}

