using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RevitUtils;
using System;
using Assert = NUnit.Framework.Assert;

namespace KdTree3DTest
{
    [TestClass]
    public class RectPrismTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void RectPrismInfinite()
        {
            Assert.AreEqual(double.NegativeInfinity, RectPrism.Infinite.MinX);
            Assert.AreEqual(double.NegativeInfinity, RectPrism.Infinite.MinY);
            Assert.AreEqual(double.NegativeInfinity, RectPrism.Infinite.MinZ);
            Assert.AreEqual(double.PositiveInfinity, RectPrism.Infinite.MaxX);
            Assert.AreEqual(double.PositiveInfinity, RectPrism.Infinite.MaxY);
            Assert.AreEqual(double.PositiveInfinity, RectPrism.Infinite.MaxZ);
        }

        [TestMethod]
        public void RectPrismPointConstructor()
        {
            RectPrism rect = new RectPrism(Point3D.Min, Point3D.Max);
            //Assert.AreEqual(double.ne)
        }

        [TestMethod]
        public void RectPrismDoubleConstructor()
        {
        }

        [TestMethod]
        public void RectPrismNullConstructor()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new RectPrism(null,null));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null."));
        }
    }
}
