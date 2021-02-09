using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RevitUtils;
using System;
using Assert = NUnit.Framework.Assert;

namespace KdTree3DTest
{
    [TestClass]
    public class Point3DTest
    {
        [TestMethod]
        public void Template()
        {
        }

        [TestMethod]
        public void Point3DMin()
        {
            Assert.AreEqual(double.NegativeInfinity, Point3D.Min.X);
            Assert.AreEqual(double.NegativeInfinity, Point3D.Min.Y);
            Assert.AreEqual(double.NegativeInfinity, Point3D.Min.Z);
        }

        [TestMethod]
        public void Point3DMax()
        {
            Assert.AreEqual(double.PositiveInfinity, Point3D.Max.X);
            Assert.AreEqual(double.PositiveInfinity, Point3D.Max.Y);
            Assert.AreEqual(double.PositiveInfinity, Point3D.Max.Z);
        }

        [TestMethod]
        public void Point3DDistToNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Point3D.Min.DistTo((Point3D)null));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: point"));
        }

        [TestMethod]
        public void Point3DDistToRectNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Point3D.Min.DistTo((RectPrism)null));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: rect"));
        }

        [TestMethod]
        public void Point3DDistToX()
        {
            double expected = 1;
            Point3D p1 = new Point3D(0, 0, 0);
            Point3D p2 = new Point3D(1, 0, 0);
            Assert.AreEqual(expected, p1.DistTo(p2));
        }

        [TestMethod]
        public void Point3DDistToY()
        {
            double expected = 2;
            Point3D p1 = new Point3D(0, 0, 0);
            Point3D p2 = new Point3D(0, -2, 0);
            Assert.AreEqual(expected, p1.DistTo(p2));
        }

        [TestMethod]
        public void Point3DDistToZ()
        {
            double expected = 3;
            Point3D p1 = new Point3D(0, 0, 0);
            Point3D p2 = new Point3D(0, 0, 3);
            Assert.AreEqual(expected, p1.DistTo(p2));
        }

        [TestMethod]
        public void Point3DDistToXY()
        {
            double expected = 5;
            Point3D p1 = new Point3D(3, 0, 0);
            Point3D p2 = new Point3D(0, 4, 0);
            Assert.AreEqual(expected, p1.DistTo(p2));
        }

        [TestMethod]
        public void Point3DDistToXZ()
        {
            double expected = 5;
            Point3D p1 = new Point3D(0, 0, 4);
            Point3D p2 = new Point3D(3, 0, 0);
            Assert.AreEqual(expected, p1.DistTo(p2));
        }

        [TestMethod]
        public void Point3DDistToYZ()
        {
            double expected = 5;
            Point3D p1 = new Point3D(0, -3, 0);
            Point3D p2 = new Point3D(0, 0, -4);
            Assert.AreEqual(expected, p1.DistTo(p2));
        }

        [TestMethod]
        public void Point3DDistToXYZ()
        {
            double expected = 7;
            Point3D p1 = new Point3D(0, 0, 0);
            Point3D p2 = new Point3D(2, 3, 6);
            Assert.AreEqual(expected, p1.DistTo(p2));
        }

        [TestMethod]
        public void Point3DDistToRectX()
        {
            double expected = 1;
            Point3D p1 = new Point3D(-2, 0, 0);
            RectPrism rect = new RectPrism(-1, -1, -1, 1, 1, 1);
            Assert.AreEqual(expected, p1.DistTo(rect));
        }

        [TestMethod]
        public void Point3DDistToRectY()
        {
            double expected =1;
            Point3D p1 = new Point3D(0, 2, 0);
            RectPrism rect = new RectPrism(-1, -1, -1, 1, 1, 1);
            Assert.AreEqual(expected, p1.DistTo(rect));
        }

        [TestMethod]
        public void Point3DDistToRectZ()
        {
            double expected = 3;
            Point3D p1 = new Point3D(0, 0, 4);
            RectPrism rect = new RectPrism(-1, -1, -1, 1, 1, 1);
            Assert.AreEqual(expected, p1.DistTo(rect));
        }

        [TestMethod]
        public void Point3DDistToRectXY()
        {
            double expected = 5;
            Point3D p1 = new Point3D(4, 5, 0);
            RectPrism rect = new RectPrism(-1, -1, -1, 1, 1, 1);
            Assert.AreEqual(expected, p1.DistTo(rect));
        }

        [TestMethod]
        public void Point3DDistToRectXZ()
        {
            double expected = 5;
            Point3D p1 = new Point3D(4, 0, 5);
            RectPrism rect = new RectPrism(-1, -1, -1, 1, 1, 1);
            Assert.AreEqual(expected, p1.DistTo(rect));
        }

        [TestMethod]
        public void Point3DDistToRectYZ()
        {
            double expected = 5;
            Point3D p1 = new Point3D(0, -4, -5);
            RectPrism rect = new RectPrism(-1, -1, -1, 1, 1, 1);
            Assert.AreEqual(expected, p1.DistTo(rect));
        }

        [TestMethod]
        public void Point3DDistToRectXYZ()
        {
            double expected = 7;
            Point3D p1 = new Point3D(3, 4, 7);
            RectPrism rect = new RectPrism(-1, -1, -1, 1, 1, 1);
            Assert.AreEqual(expected, p1.DistTo(rect));
        }
    }
}
