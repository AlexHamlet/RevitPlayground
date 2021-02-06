using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RevitUtils;
using System;
using Assert = NUnit.Framework.Assert;

namespace KdTree2DTest
{
    [TestClass]
    public class RectangleTest
    {
        [TestMethod]
        public void Template()
        {

        }

        [TestMethod]
        public void RectInfinite()
        {
            Assert.AreEqual(double.NegativeInfinity, Rectangle.Infinite.MinX);
            Assert.AreEqual(double.NegativeInfinity, Rectangle.Infinite.MinY);
            Assert.AreEqual(double.PositiveInfinity, Rectangle.Infinite.MaxX);
            Assert.AreEqual(double.PositiveInfinity, Rectangle.Infinite.MaxY);
        }

        [TestMethod]
        public void RectPointConstructor()
        {
            Rectangle rect = new Rectangle(Point2D.Min, Point2D.Max);
            Assert.AreEqual(double.NegativeInfinity, rect.MinX);
            Assert.AreEqual(double.NegativeInfinity, rect.MinY);
            Assert.AreEqual(double.PositiveInfinity, rect.MaxX);
            Assert.AreEqual(double.PositiveInfinity, rect.MaxY);
        }

        [TestMethod]
        public void Rectdoubleconstructor()
        {
            Rectangle rect = new Rectangle(-1, -1, 1, 1);
            Assert.AreEqual(-1, rect.MinX);
            Assert.AreEqual(-1, rect.MinY);
            Assert.AreEqual(1, rect.MaxX);
            Assert.AreEqual(1, rect.MaxY);
        }

        [TestMethod]
        public void RectPointConstructorNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Rectangle(null,null));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null."));
        }
    }
}
