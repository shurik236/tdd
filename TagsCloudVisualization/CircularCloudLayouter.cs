using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using NUnit.Framework.Api;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        private readonly Point center;
        private readonly Cloud cloud;
        private readonly SpiralWalker spiral;
        
        public Point GetCenter()
        {
            return center;
        }

        public Cloud GetCloud()
        {
            return cloud;
        }

        public CircularCloudLayouter(Point center)
        {
            if (center.X < 0 || center.Y < 0)
                throw new ArgumentException("Center can't have negative components!");

            this.center = center;
            cloud = new Cloud();
            spiral = new SpiralWalker(center, 0.1, 0, 1);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle newRect;

            if (cloud.Count == 0)
            {
                newRect = new Rectangle(center, rectangleSize);
                newRect.Offset((newRect.Left - newRect.Right) / 2, (newRect.Top - newRect.Bottom) / 2);
                cloud.AppendRectangle(newRect);
                return newRect;
            }
            do
            {
                newRect = new Rectangle(spiral.GetNextPoint(), rectangleSize);
            } while (cloud.IntersectsWith(newRect));

            cloud.AppendRectangle(newRect);
            return newRect;
        }

    }
}
