namespace Assignment7
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
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.handlingTextFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawingShapesFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.windowToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(595, 24);
            this.mainMenuStrip.TabIndex = 1;
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.handlingTextFormToolStripMenuItem,
            this.drawingShapesFormToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.newToolStripMenuItem.Text = "New";
            // 
            // handlingTextFormToolStripMenuItem
            // 
            this.handlingTextFormToolStripMenuItem.Name = "handlingTextFormToolStripMenuItem";
            this.handlingTextFormToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.handlingTextFormToolStripMenuItem.Text = "Handling Text Form";
            this.handlingTextFormToolStripMenuItem.Click += new System.EventHandler(this.handlingTextFormToolStripMenuItem_Click);
            // 
            // drawingShapesFormToolStripMenuItem
            // 
            this.drawingShapesFormToolStripMenuItem.Name = "drawingShapesFormToolStripMenuItem";
            this.drawingShapesFormToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.drawingShapesFormToolStripMenuItem.Text = "Drawing Shapes Form";
            this.drawingShapesFormToolStripMenuItem.Click += new System.EventHandler(this.drawingShapesFormToolStripMenuItem_Click);
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
            this.ClientSize = new System.Drawing.Size(595, 464);
            this.Controls.Add(this.mainMenuStrip);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "Assignment 7";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem handlingTextFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawingShapesFormToolStripMenuItem;
    }
}

