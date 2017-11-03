using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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
            this.Center = center;
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
            var x = Math.Round(r * Math.Cos(phi)) + Center.X;
            var y = Math.Round(r * Math.Sin(phi)) + Center.Y;

            return new Point((int)x, (int)y);
        }

    }
}
