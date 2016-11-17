namespace Assignment6_4
{
    partial class DisplayProductForm
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
            this.readProductIdButton = new System.Windows.Forms.Button();
            this.displayRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // readProductIdButton
            // 
            this.readProductIdButton.Location = new System.Drawing.Point(13, 15);
            this.readProductIdButton.Name = "readProductIdButton";
            this.readProductIdButton.Size = new System.Drawing.Size(132, 23);
            this.readProductIdButton.TabIndex = 0;
            this.readProductIdButton.Text = "Read Product ID";
            this.readProductIdButton.UseVisualStyleBackColor = true;
            this.readProductIdButton.Click += new System.EventHandler(this.readProductIdButton_Click);
            // 
            // displayRichTextBox
            // 
            this.displayRichTextBox.Location = new System.Drawing.Point(13, 51);
            this.displayRichTextBox.Name = "displayRichTextBox";
            this.displayRichTextBox.Size = new System.Drawing.Size(270, 169);
            this.displayRichTextBox.TabIndex = 1;
            this.displayRichTextBox.Text = "";
            // 
            // DisplayProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 237);
            this.Controls.Add(this.displayRichTextBox);
            this.Controls.Add(this.readProductIdButton);
            this.Name = "DisplayProductForm";
            this.Text = "Display Product Information";
            this.Load += new System.EventHandler(this.DisplayProductForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button readProductIdButton;
        private System.Windows.Forms.RichTextBox displayRichTextBox;
    }
}