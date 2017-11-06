using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

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

        private static CircularCloudLayouter GenerateLayouter(Point center, IEnumerable<Size> rectangleSizes)
        {
            var layouter = new CircularCloudLayouter(center);
            foreach (var size in rectangleSizes)
                layouter.PutNextRectangle(size);

            return layouter;
        }

        public static void VisualizeLayouter(CircularCloudLayouter layouter, string fileName)
        {
            var center = layouter.Center;
            var img = new Bitmap(center.X * 2, center.Y * 2);
            var g = Graphics.FromImage(img);
            g.FillRectangle(new SolidBrush(Color.Blue), 0, 0, center.X * 2, center.Y * 2);
            g.DrawLine(Pens.Red, center.X, 0, center.X, center.Y * 2);
            g.DrawLine(Pens.Red, 0, center.Y, center.X * 2, center.Y);
            foreach (var rect in layouter.Cloud.Rectangles)
            {
                g.DrawRectangle(Pens.White, rect);
            }
            var path = Path.Combine(
                Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory),
                "..",
                "..",
                $"{fileName}.bmp"
                );
            img.Save(path);
        }
        //Можно было и относительными путями, но вот тогда во время выполнения тестов текущий путь
        //указывает на каталог с вижой. Немного небрежно, но хоть работает для своих примеров и для тестов.



        public static void Main()
        {
            VisualizeLayouter(GenerateLayouter(new Point(300, 300), SameRectangles()), "layout1");
            VisualizeLayouter(GenerateLayouter(new Point(300, 300), AllDifferent()), "layout2");
            VisualizeLayouter(GenerateLayouter(new Point(300, 300), SameSquares()), "layout3");
        }
    }
}
