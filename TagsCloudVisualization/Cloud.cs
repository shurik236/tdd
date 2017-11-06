using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class Cloud : IEnumerable<Rectangle>

    {
        public List<Rectangle> Rectangles { get; }
        public int Count => Rectangles.Count;
        public Cloud()
        {
            Rectangles = new List<Rectangle>();
        }

        public bool IntersectsWith(Rectangle rect)
        {
            return Rectangles.Any(r => r.IntersectsWith(rect));
        }

        public void AppendRectangle(Rectangle rect)
        {
            Rectangles.Add(rect);
        }

        public IEnumerator<Rectangle> GetEnumerator()
        {
            return ((IEnumerable<Rectangle>)Rectangles).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Rectangle>)Rectangles).GetEnumerator();
        }
    }
}
