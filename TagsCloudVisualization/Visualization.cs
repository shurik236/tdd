using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Visualization
    {
        public static IEnumerable<Size> SameRectangles()
        {
            for (var i=0; i<50; i++)
                yield return new Size(50, 30);
        }

        public static IEnumerable<Size> SameSquares()
        {
            for (var i=0; i<200; i++)
                yield return new Size(20, 20);
        }

        public static IEnumerable<Size> AllDifferent()
        {
            for (var i=0; i<30; i++)
                yield return new Size(50-i, 10+2*i);
        }

        private static CircularCloudLayouter GenerateLayout(Point center, IEnumerable<Size> rectangleSizes)
        {
            var layouter = new CircularCloudLayouter(center);
            foreach (var size in rectangleSizes)
                layouter.PutNextRectangle(size);

            return layouter;
        }

        public static void VisualizeLayout(CircularCloudLayouter layouter, string fileName)
        {
            var center = layouter.Center;
            var img = new Bitmap(center.X*2, center.Y*2);
            var g = Graphics.FromImage(img);
            g.FillRectangle(new SolidBrush(Color.Blue), 0, 0, center.X*2, center.Y*2);
            g.DrawLine(Pens.Red, center.X, 0, center.X, center.Y*2);
            g.DrawLine(Pens.Red, 0, center.Y, center.X*2, center.Y);
            foreach (var rect in layouter.Cloud.Rectangles)
            {
                g.DrawRectangle(Pens.White, rect);
            }
            img.Save($"./{fileName}.bmp");
        }

        public static void Main()
        {
            VisualizeLayout(GenerateLayout(new Point(300, 300), SameRectangles()), "layout1");
            VisualizeLayout(GenerateLayout(new Point(300, 300), AllDifferent()), "layout2");
            VisualizeLayout(GenerateLayout(new Point(300, 300), SameSquares()), "layout3");
        }
    }
}
