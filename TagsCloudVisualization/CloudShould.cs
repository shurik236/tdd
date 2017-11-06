using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    public class CloudShould
    {
        [Test]
        public void HaveNothingOnInit()
        {
            var cloud = new Cloud();
            cloud.Should().BeEmpty();
        }

        [Test]
        public void HaveRectangleAfterAppend()
        {
            var cloud = new Cloud();
            cloud.AppendRectangle(new Rectangle(100, 50, 20, 10));
            cloud.Should().HaveCount(1);
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
