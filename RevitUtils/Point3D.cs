/*Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.*/

using System;

namespace RevitUtils
{
    /// <summary>
    /// A class used to represent a 3D point
    /// </summary>
    public class Point3D
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
        /// Z coordinate
        /// </summary>
        public double Z { get; }

        /// <summary>
        /// Point at (Negative Infinity, Negative Infinity, Negative Infinity)
        /// </summary>
        public static Point3D Min = new Point3D(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);
        /// <summary>
        /// Point at (Positive Infinity, Positive Infinity, Positive Infinity)
        /// </summary>
        public static Point3D Max = new Point3D(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);

        /// <summary>
        /// Constructor for Point3D
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Find the distance between points
        /// </summary>
        /// <param name="point">Point to find distance to.</param>
        /// <returns>The distance between this point and another point</returns>
        public double DistTo(Point3D point)
        {
            if (point == null)
                throw new ArgumentNullException("point");
            return Math.Sqrt(Math.Pow(point.X - X, 2) + Math.Pow(point.Y - Y, 2) + Math.Pow(point.Z - Z, 2));
        }

        /// <summary>
        /// Find the distance to a RectPrism
        /// </summary>
        /// <param name="rect">Rectangular prism to find distance to</param>
        /// <returns>The distance to the rectangular prism.</returns>
        public double DistTo(RectPrism rect)
        {
            if (rect == null)
                throw new ArgumentNullException("rect");
            double dx = Math.Max(rect.MinX - X, Math.Max(0, X - rect.MaxX));
            double dy = Math.Max(rect.MinY - Y, Math.Max(0, Y - rect.MaxY));
            double dz = Math.Max(rect.MinZ - Z, Math.Max(0, Z - rect.MaxZ));
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// Equals method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Point3D d &&
                   X == d.X &&
                   Y == d.Y &&
                   Z == d.Z;
        }

        /// <summary>
        /// Hashcode Method
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashCode = -307843816;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }
    }
}
