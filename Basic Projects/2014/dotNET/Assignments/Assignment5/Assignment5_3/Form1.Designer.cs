namespace Assignment5_3
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
            this.components = new System.ComponentModel.Container();
            this.myButton = new System.Windows.Forms.Button();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeBackgroundCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSizeBackgroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myTextBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.duplicateFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // myButton
            // 
            this.myButton.ContextMenuStrip = this.contextMenuStrip2;
            this.myButton.Location = new System.Drawing.Point(101, 85);
            this.myButton.Name = "myButton";
            this.myButton.Size = new System.Drawing.Size(75, 23);
            this.myButton.TabIndex = 1;
            this.myButton.Text = "CLICK";
            this.myButton.UseVisualStyleBackColor = true;
            this.myButton.Click += new System.EventHandler(this.myButton_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeSizeToolStripMenuItem,
            this.changeBackgroundCToolStripMenuItem,
            this.setSizeBackgroundColorToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(273, 70);
            // 
            // changeSizeToolStripMenuItem
            // 
            this.changeSizeToolStripMenuItem.Name = "changeSizeToolStripMenuItem";
            this.changeSizeToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.changeSizeToolStripMenuItem.Text = "change size (randomly)";
            this.changeSizeToolStripMenuItem.Click += new System.EventHandler(this.changeSizeToolStripMenuItem_Click);
            // 
            // changeBackgroundCToolStripMenuItem
            // 
            this.changeBackgroundCToolStripMenuItem.Name = "changeBackgroundCToolStripMenuItem";
            this.changeBackgroundCToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.changeBackgroundCToolStripMenuItem.Text = "change background color (randomly)";
            this.changeBackgroundCToolStripMenuItem.Click += new System.EventHandler(this.changeBackgroundCToolStripMenuItem_Click);
            // 
            // setSizeBackgroundColorToolStripMenuItem
            // 
            this.setSizeBackgroundColorToolStripMenuItem.Name = "setSizeBackgroundColorToolStripMenuItem";
            this.setSizeBackgroundColorToolStripMenuItem.Size = new System.Drawing.Size(272, 22);
            this.setSizeBackgroundColorToolStripMenuItem.Text = "set size/background color";
            this.setSizeBackgroundColorToolStripMenuItem.Click += new System.EventHandler(this.setSizeBackgroundColorToolStripMenuItem_Click);
            // 
            // myTextBox
            // 
            this.myTextBox.Location = new System.Drawing.Point(90, 33);
            this.myTextBox.Name = "myTextBox";
            this.myTextBox.Size = new System.Drawing.Size(100, 20);
            this.myTextBox.TabIndex = 4;
            this.myTextBox.Text = "Click the button!";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.duplicateFormToolStripMenuItem,
            this.addControlToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // duplicateFormToolStripMenuItem
            // 
            this.duplicateFormToolStripMenuItem.Name = "duplicateFormToolStripMenuItem";
            this.duplicateFormToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.duplicateFormToolStripMenuItem.Text = "duplicate form";
            this.duplicateFormToolStripMenuItem.Click += new System.EventHandler(this.duplicateFormToolStripMenuItem_Click);
            // 
            // addControlToolStripMenuItem
            // 
            this.addControlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonToolStripMenuItem,
            this.textBoxToolStripMenuItem});
            this.addControlToolStripMenuItem.Name = "addControlToolStripMenuItem";
            this.addControlToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addControlToolStripMenuItem.Text = "add control";
            // 
            // buttonToolStripMenuItem
            // 
            this.buttonToolStripMenuItem.Name = "buttonToolStripMenuItem";
            this.buttonToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.buttonToolStripMenuItem.Text = "button";
            this.buttonToolStripMenuItem.Click += new System.EventHandler(this.buttonToolStripMenuItem_Click);
            // 
            // textBoxToolStripMenuItem
            // 
            this.textBoxToolStripMenuItem.Name = "textBoxToolStripMenuItem";
            this.textBoxToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.textBoxToolStripMenuItem.Text = "text box";
            this.textBoxToolStripMenuItem.Click += new System.EventHandler(this.textBoxToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 136);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.myTextBox);
            this.Controls.Add(this.myButton);
            this.Name = "Form1";
            this.Text = "TASK 5_3";
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button myButton;
        private System.Windows.Forms.TextBox myTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem duplicateFormToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem changeSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeBackgroundCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setSizeBackgroundColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textBoxToolStripMenuItem;

    }
}

