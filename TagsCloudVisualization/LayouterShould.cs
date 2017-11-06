using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization
{
    public class LayouterShould
    {
        private static CircularCloudLayouter _layouter;

        private static void SaveVisualizationOnFailure()
        {
            var testResult = TestContext.CurrentContext.Result.Outcome;
            var testName = TestContext.CurrentContext.Test.Name;

            if (Equals(testResult, ResultState.Failure) ||
                Equals(testResult, ResultState.Error))
            {
                Visualization.VisualizeLayouter(_layouter, testName);
            }
        }

        [TestFixture]
        public class InitializeWithGivenPoint
        {

            [TestCase(128, 128, TestName = "(128, 128) as Center")]
            [TestCase(220, 380, TestName = "(220, 380) as Center")]
            public void Initialize_WithGivenCenter(int x, int y)
            {
                var expectedCenter = new Point(x, y);
                _layouter = new CircularCloudLayouter(new Point(x, y));
                _layouter.Center.Should().Be(expectedCenter);
            }

            [TearDown]
            public void TearDown()
            {
                SaveVisualizationOnFailure();
            }
            
        }

        [TestFixture]
        public class PlaceFirstRectInCenter
        {

            [TestCase(123, 123, 120, 120,
                TestName = "with upper left (63, 63)," +
                           " 120x120 on Center (123, 123)")]
            [TestCase(64, 128, 30, 60,
                TestName = "with upper left (34, 68), " +
                           " 30x60 on Center (64, 128)")]

            public void PlaceFirstRect_InCenter(int centerX, int centerY, int width, int height)
            {
                _layouter = new CircularCloudLayouter(new Point(centerX, centerY));
                var nextRectangle = _layouter.PutNextRectangle(new Size(width, height));
                var expectedRectangle = new Rectangle(centerX - width / 2, centerY - height / 2, width, height);

                nextRectangle.Should().Be(expectedRectangle);
            }

            [TearDown]
            public void TearDown()
            {
                SaveVisualizationOnFailure();
            }
        }

        [TestFixture]
        public class PlaceTwoWithoutIntersections
        {
            [TestCase(128, 128, 120, 100, 60, 50,
                TestName = "when center is (128, 128), rectangles are 120x100 and 60x50")]
            [TestCase(120, 50, 100, 110, 50, 90,
                TestName = "when center is (120, 50), rectangles are 100x110 and 50x90")]
            public void PlaceTwoRects_WithoutIntersections(int centerX, int centerY, int w1, int h1, int w2, int h2)
            {
                _layouter = new CircularCloudLayouter(new Point(centerX, centerY));
                var firstRectangle = _layouter.PutNextRectangle(new Size(w1, h1));
                var secondRectangle = _layouter.PutNextRectangle(new Size(w2, h2));

                firstRectangle.IntersectsWith(secondRectangle).Should().BeFalse();
            }

            [TearDown]
            public void TearDown()
            {
                SaveVisualizationOnFailure();
            }
        }
    }
}
