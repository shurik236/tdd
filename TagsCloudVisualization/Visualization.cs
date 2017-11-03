using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class Visualization
    {
        public static void GenerateAndVisualize(int canvasW, int canvasH, Point center, string fileName)
        {
            var img = new Bitmap(canvasW, canvasH);
            var layouter = new CircularCloudLayouter(center);
            var g = Graphics.FromImage(img);
            g.FillRectangle(new SolidBrush(Color.Blue), 0, 0, canvasW, canvasH);
            g.DrawLine(Pens.Red, center.X, 0, center.X, canvasH);
            g.DrawLine(Pens.Red, 0, center.Y, canvasW, center.Y);
            for (var i = 0; i < 200; i++)
            {
                var nextRectangle = layouter.PutNextRectangle(new Size(20, 20));
                g.DrawRectangle(Pens.White, nextRectangle);                
            }
            img.Save(@".\"+fileName+".bmp");
        }

        public static void VisualizeLayout(CircularCloudLayouter layout, string fileName)
        {
            var center = layout.GetCenter();
            var img = new Bitmap(center.X*2, center.Y*2);
            var g = Graphics.FromImage(img);
            g.FillRectangle(new SolidBrush(Color.Blue), 0, 0, center.X*2, center.Y*2);
            g.DrawLine(Pens.Red, center.X, 0, center.X, center.Y*2);
            g.DrawLine(Pens.Red, 0, center.Y, center.X*2, center.Y);
            foreach (var rect in layout.GetCloud().GetRectangles())
            {
                g.DrawRectangle(Pens.White, rect);
            }
            img.Save(@".\" + fileName + ".bmp");
        }

        public static void Main()
        {
            GenerateAndVisualize(600, 600, new Point(300, 300), "kek");
        }
    }
}
