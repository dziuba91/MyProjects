using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace Triskelion
{
    public class PieChart:PictureBox
    {
        public float InitalAngle=0;
        public CompositingQuality CompositingQuality;
        public SmoothingMode SmoothingMode;
        public InterpolationMode InterpolationMode;
        public Point StartLabels = new Point(0, 0);
        Color[,] ColorSets = new Color[,]{{Color.Red, Color.DarkRed,Color.LightPink},{Color.Blue, Color.DarkBlue, Color.SkyBlue}, {Color.Green, Color.DarkGreen, Color.LimeGreen}};
        public void Chart(int[] Values)
        {
            float[] FloatValues = new float[Values.Length];
            for (int i = 0; i < Values.Length; ++i)
                FloatValues[i] = Values[i];
            Chart(FloatValues);
        }
        public void Chart(float[] Values)
        {
            this.Image = new Bitmap(this.Width-1, this.Height-1);
            Graphics g = Graphics.FromImage(this.Image);
            g.CompositingQuality = this.CompositingQuality;
            g.InterpolationMode = this.InterpolationMode;
            g.SmoothingMode = this.SmoothingMode;
            Rectangle DrawingArea=new Rectangle(new Point(0,0),new Size(this.Image.Size.Width-1,this.Image.Height-1));
            g.FillRectangle(new SolidBrush(this.BackColor),DrawingArea);
            float Sum = 0, CurrentAngle = InitalAngle, ArcAngle;
            foreach (float Value in Values)
                Sum += Value;
            for (int i=0; i<Values.Length; ++i)
            {
                ArcAngle=360 * Values[i] / Sum;
                g.FillPie(new SolidBrush(ColorSets[i, 0]),DrawingArea, CurrentAngle, ArcAngle);
                g.DrawPie(new Pen(ColorSets[i, 1]),DrawingArea, CurrentAngle, ArcAngle);
                CurrentAngle += ArcAngle;
            }
            for (int i=0; i<Values.Length; ++i)
                g.DrawString(Math.Round(Values[i] / Sum * 100, 2) + "%", new Font(FontFamily.GenericMonospace, 8, FontStyle.Bold), new SolidBrush(ColorSets[i, 2]), 5, 25 + (i * 10));
            g.DrawImage(this.Image, 0, 0);
        }
    }
}
