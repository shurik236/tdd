using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;

namespace TagsCloudVisualization
{
    public class Cloud
    {
        private readonly List<Rectangle> rectangles;
        public int Count { get; private set; }
        public Cloud()
        {
            rectangles = new List<Rectangle>();
        }

        public List<Rectangle> GetRectangles()
        {
            return rectangles;
        }

        public bool IntersectsWith(Rectangle rect)
        {
            return rectangles.Any(r => r.IntersectsWith(rect));
        }

        public void AppendRectangle(Rectangle rect)
        {
            rectangles.Add(rect);
            Count++;
        }
    }
}
