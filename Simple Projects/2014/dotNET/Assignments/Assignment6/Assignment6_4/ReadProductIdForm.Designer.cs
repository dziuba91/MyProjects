namespace Assignment6_4
{
    partial class ReadProductIdForm
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
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.getIdButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(38, 20);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 0;
            // 
            // getIdButton
            // 
            this.getIdButton.Location = new System.Drawing.Point(167, 18);
            this.getIdButton.Name = "getIdButton";
            this.getIdButton.Size = new System.Drawing.Size(75, 23);
            this.getIdButton.TabIndex = 1;
            this.getIdButton.Text = "Get ID";
            this.getIdButton.UseVisualStyleBackColor = true;
            this.getIdButton.Click += new System.EventHandler(this.getIdButton_Click);
            // 
            // ReadProductIdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 62);
            this.Controls.Add(this.getIdButton);
            this.Controls.Add(this.idTextBox);
            this.Name = "ReadProductIdForm";
            this.Text = "Read Product ID Form";
            this.Load += new System.EventHandler(this.ReadProductIdForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Button getIdButton;
    }
}