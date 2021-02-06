using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RevitUtils;
using System;
using Assert = NUnit.Framework.Assert;

namespace KdTree2DTest
{
    [TestClass]
    public class Point2DTest
    {
        [TestMethod]
        public void Template()
        {
        }

        [TestMethod]
        public void PointMin()
        {
            double expected = double.NegativeInfinity;
            Point2D p = Point2D.Min;
            Assert.AreEqual(expected, p.X);
            Assert.AreEqual(expected, p.Y);
        }

        [TestMethod]
        public void PointMax()
        {
            double expected = double.PositiveInfinity;
            Point2D p = Point2D.Max;
            Assert.AreEqual(expected, p.X);
            Assert.AreEqual(expected, p.Y);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            double X = 4;
            double Y = 8;
            Point2D p = new Point2D(X, Y);
            Assert.AreEqual(X, p.X);
            Assert.AreEqual(Y, p.Y);
        }

        [TestMethod]
        public void DistToHoriz()
        {
            double expected = 8;
            Point2D p1 = new Point2D(-4, 0);
            Point2D p2 = new Point2D(4, 0);
            Assert.AreEqual(expected, p1.DistTo(p2));
        }

        [TestMethod]
        public void DistToVert()
        {
            double expected = 8;
            Point2D p1 = new Point2D(0, -4);
            Point2D p2 = new Point2D(0, 4);
            Assert.AreEqual(expected, p1.DistTo(p2));
        }

        [TestMethod]
        public void DistToDiag()
        {
            double expected = 5;
            Point2D p1 = new Point2D(0, 3);
            Point2D p2 = new Point2D(4, 0);
            Assert.AreEqual(expected, p1.DistTo(p2));
        }

        [TestMethod]
        public void DistToSame()
        {
            double expected = 0;
            Point2D p1 = new Point2D(0, 0);
            Assert.AreEqual(expected, p1.DistTo(p1));
        }

        [TestMethod]
        public void DistToOverlap()
        {
            double expected = 0;
            Point2D p1 = new Point2D(1, 1);
            Point2D p2 = new Point2D(1, 1);
            Assert.AreEqual(expected, p1.DistTo(p2));
        }

        [TestMethod]
        public void DistToNull()
        {
            Point2D p1 = new Point2D(1, 1);
            var ex = Assert.Throws<ArgumentNullException>(() => p1.DistTo((Point2D)null));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: point"));
        }

        [TestMethod]
        public void DistToRectHoriz()
        {
            double expected = 1;
            Point2D p1 = new Point2D(2, 0);
            Rectangle rect = new Rectangle(-1,-1,1,1);
            Assert.AreEqual(expected, p1.DistTo(rect));
        }

        [TestMethod]
        public void DistToRectVert()
        {
            double expected = 1;
            Point2D p1 = new Point2D(0,2);
            Rectangle rect = new Rectangle(-1, -1, 1, 1);
            Assert.AreEqual(expected, p1.DistTo(rect));
        }

        [TestMethod]
        public void DistToRectDiag()
        {
            double expected = 5;
            Point2D p1 = new Point2D(4, 5);
            Rectangle rect = new Rectangle(-1, -1, 1, 1);
            Assert.AreEqual(expected, p1.DistTo(rect));
        }

        [TestMethod]
        public void DistToRectOverlap()
        {
            double expected = 0;
            Point2D p1 = new Point2D(1, 1);
            Rectangle rect = new Rectangle(-1, -1, 1, 1);
            Assert.AreEqual(expected, p1.DistTo(rect));
        }

        [TestMethod]
        public void DistToRectNull()
        {
            Point2D p1 = new Point2D(1, 1);
            var ex = Assert.Throws<ArgumentNullException>(() => p1.DistTo((Rectangle)null));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: rect"));
        }
    }
}
