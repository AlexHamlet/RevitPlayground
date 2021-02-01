/*Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.*/

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
        /// <param name="p">Point to find distance to.</param>
        /// <returns>The distance between this point and another point</returns>
        public double DistTo(Point2D p)
        {
            return Math.Sqrt(Math.Pow(p.X - X, 2) + Math.Pow(p.Y - Y, 2));
        }

        /// <summary>
        /// Find the distance to a Rectangle
        /// </summary>
        /// <param name="rect">Rectangle to find distance to</param>
        /// <returns>The distance to the rectangle.</returns>
        public double DistTo(Rectangle rect)
        {
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
