using System.Drawing;
using static System.Math;

namespace TagsCloudVisualization
{
    public class SpiralWalker
    {
        public Point Center { get; }
        private readonly double angularStep;
        private readonly double initialRadius;
        private readonly double sparcity;
        private double angle;

        public SpiralWalker(Point center, double step, double initialRadius, double sparcity)
        {
            Center = center;
            angularStep = step;
            this.initialRadius = initialRadius;
            this.sparcity = sparcity;
        }

        public Point GetNextPoint()
        {
            angle += angularStep;
            var radius = initialRadius + angle * sparcity;
            return ToCartesianPoint(radius, angle);
        }

        private Point ToCartesianPoint(double r, double phi)
        {
            var x = Round(r * Cos(phi)) + Center.X;
            var y = Round(r * Sin(phi)) + Center.Y;

            return new Point((int)x, (int)y);
        }

    }
}
