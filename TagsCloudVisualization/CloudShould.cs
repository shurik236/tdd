using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization
{
    public class CloudShould
    {
        [TestCase(0, TestName = "be empty before any appending")]
        [TestCase(1, TestName = "have 1 after appending 1")]
        [TestCase(20, TestName = "have 20 after appending 20")]
        [TestCase(5, TestName = "have 5 after appending 5")]
        public void HaveExactAmount_AfterAppend(int amount)
        {
            var cloud = new Cloud();
            for (int i=0; i<amount; i++)
                cloud.AppendRectangle(new Rectangle(120-i, 120-i, 2+i, 2+i));
            cloud.Count.Should().Be(amount);
        }

        [TestCase(0, 0, 25, 25, false, 
            TestName = "doesn't intersect with ((0, 0), (25, 25))" +
                       "having ((0, 50),(50, 150)), ((150, 50),(250, 150))")]
        [TestCase(25, 50, 100, 50, true, 
            TestName = "intersects with ((25, 50), (125, 100)) " +
                       "having ((0, 50),(50, 150)), ((150, 50),(250, 150))")]

        public void ShowIfIntersects(int left, int top, int width, int height, bool expectedResult)
        {
            var cloud = new Cloud();
            cloud.AppendRectangle(new Rectangle(0, 50, 50, 100));
            cloud.AppendRectangle(new Rectangle(150, 50, 100, 100));

            var givenRectangle = new Rectangle(left, top, width, height);

            cloud.IntersectsWith(givenRectangle).Should().Be(expectedResult);
        }
    }
}
