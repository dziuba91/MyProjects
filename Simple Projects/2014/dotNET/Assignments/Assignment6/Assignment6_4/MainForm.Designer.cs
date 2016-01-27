namespace Assignment6_4
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.newEnterProductFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enterProductFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayProductFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newEnterProductFormToolStripMenuItem,
            this.windowToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(496, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // newEnterProductFormToolStripMenuItem
            // 
            this.newEnterProductFormToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enterProductFormToolStripMenuItem,
            this.displayProductFormToolStripMenuItem});
            this.newEnterProductFormToolStripMenuItem.Name = "newEnterProductFormToolStripMenuItem";
            this.newEnterProductFormToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.newEnterProductFormToolStripMenuItem.Text = "New";
            // 
            // enterProductFormToolStripMenuItem
            // 
            this.enterProductFormToolStripMenuItem.Name = "enterProductFormToolStripMenuItem";
            this.enterProductFormToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.enterProductFormToolStripMenuItem.Text = "Enter Product Form";
            this.enterProductFormToolStripMenuItem.Click += new System.EventHandler(this.enterProductFormToolStripMenuItem_Click);
            // 
            // displayProductFormToolStripMenuItem
            // 
            this.displayProductFormToolStripMenuItem.Name = "displayProductFormToolStripMenuItem";
            this.displayProductFormToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.displayProductFormToolStripMenuItem.Text = "Display Product Form";
            this.displayProductFormToolStripMenuItem.Click += new System.EventHandler(this.displayProductFormToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 319);
            this.Controls.Add(this.mainMenuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Assignment 6_4";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newEnterProductFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enterProductFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayProductFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
    }
}

