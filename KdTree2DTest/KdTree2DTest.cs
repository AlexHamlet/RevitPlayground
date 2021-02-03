using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using RevitUtils;
using System;
using Assert = NUnit.Framework.Assert;

namespace KdTree2DTest
{
    [TestClass]
    public class KdTree2DTest
    {
        private KdTree2D<int> harness;

        [TestMethod]
        public void TemplateTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void InitCount()
        {
            int expected = 0;
            harness = new KdTree2D<int>();
            Assert.AreEqual(expected, harness.Count);
        }

        [TestMethod]
        public void CorrectCount()
        {
            int expected = 5;
            harness = new KdTree2D<int>();
            harness.Put(1, new Point2D(1, 1));
            harness.Put(2, new Point2D(2, 2));
            harness.Put(3, new Point2D(3, 3));
            harness.Put(4, new Point2D(4, 4));
            harness.Put(5, new Point2D(5, 5));
            Assert.AreEqual(expected, harness.Count);
        }

        [TestMethod]
        public void Nearest1()
        {
            int expected = 1;
            harness = new KdTree2D<int>();
            harness.Put(1, new Point2D(1, 1));
            harness.Put(2, new Point2D(2, 2));
            harness.Put(3, new Point2D(3, 3));
            harness.Put(4, new Point2D(4, 4));
            harness.Put(5, new Point2D(5, 5));
            Assert.AreEqual(expected, harness.Nearest(new Point2D(0,0)));
        }

        [TestMethod]
        public void Nearest2()
        {
            int expected = 5;
            harness = new KdTree2D<int>();
            harness.Put(1, new Point2D(1, 1));
            harness.Put(2, new Point2D(2, 2));
            harness.Put(3, new Point2D(3, 3));
            harness.Put(4, new Point2D(4, 4));
            harness.Put(5, new Point2D(5, 5));
            Assert.AreEqual(expected, harness.Nearest(new Point2D(4.6,4.6)));
        }

        [TestMethod]
        public void Nearest3()
        {
            int expected = 3;
            harness = new KdTree2D<int>();
            harness.Put(1, new Point2D(1, 1));
            harness.Put(2, new Point2D(2, 2));
            harness.Put(3, new Point2D(3, 3));
            harness.Put(4, new Point2D(4, 4));
            harness.Put(5, new Point2D(5, 5));
            Assert.AreEqual(expected, harness.Nearest(new Point2D(3,3)));
        }

        [TestMethod]
        public void NearestNullPoint()
        {
            harness = new KdTree2D<int>();
            harness.Put(1, new Point2D(1, 1));
            var ex = Assert.Throws<ArgumentNullException>(() => harness.Nearest(null));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: point"));
        }

        [TestMethod]
        public void NearestEmpty()
        {
            harness = new KdTree2D<int>();
            var ex = Assert.Throws<InvalidOperationException>(() => harness.Nearest(Point2D.Min));
            Assert.That(ex.Message, Is.EqualTo("Tree has no elements"));
        }
    }
}
