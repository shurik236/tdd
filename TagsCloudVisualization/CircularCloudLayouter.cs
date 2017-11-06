using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        public Point Center { get; }
        public Cloud Cloud { get; }
        private readonly SpiralWalker spiral;

        public CircularCloudLayouter(Point center)
        {
            Center = center;
            Cloud = new Cloud();
            spiral = new SpiralWalker(center, 0.1, 0, 1);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle newRect;

            if (!Cloud.Any())
            {
                newRect = new Rectangle(Center, rectangleSize);
                newRect.Offset((newRect.Left - newRect.Right) / 2, (newRect.Top - newRect.Bottom) / 2);
                Cloud.AppendRectangle(newRect);
                return newRect;
            }
            do
            {
                newRect = new Rectangle(spiral.GetNextPoint(), rectangleSize);
            } while (Cloud.IntersectsWith(newRect));

            Cloud.AppendRectangle(newRect);
            return newRect;
        }

    }
}
