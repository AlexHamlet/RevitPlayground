using System;

namespace RevitUtils
{
    /// <summary>
    /// A class used to represent a 2D point
    /// </summary>
    public class Point2D
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public double X { get; }
        /// <summary>
        /// Y coordinate
        /// </summary>
        public double Y { get; }
        /// <summary>
        /// Point at (Negative Infinity, Negative Infinity)
        /// </summary>
        public static Point2D Min = new Point2D(double.NegativeInfinity, double.NegativeInfinity);
        /// <summary>
        /// Point at (Positive Infinity, Positive Infinity)
        /// </summary>
        public static Point2D Max = new Point2D(double.PositiveInfinity, double.PositiveInfinity);

        /// <summary>
        /// Constructor for Point2D
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Find the distance between points
        /// </summary>
        /// <param name="point">Point to find distance to.</param>
        /// <returns>The distance between this point and another point</returns>
        public double DistTo(Point2D point)
        {
            if (point == null)
                throw new ArgumentNullException("point");
            return Math.Sqrt(Math.Pow(point.X - X, 2) + Math.Pow(point.Y - Y, 2));
        }

        /// <summary>
        /// Find the distance to a Rectangle
        /// </summary>
        /// <param name="rect">Rectangle to find distance to</param>
        /// <returns>The distance to the rectangle.</returns>
        public double DistTo(Rectangle rect)
        {
            if (rect == null)
                throw new ArgumentNullException("rect");
            double dx = Math.Max(rect.MinX - X, Math.Max(0, X - rect.MaxX));
            double dy = Math.Max(rect.MinY - Y, Math.Max(0, Y - rect.MaxY));
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// Equals method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Point2D d &&
                   X == d.X &&
                   Y == d.Y;
        }

        /// <summary>
        /// Hashcode method
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}
