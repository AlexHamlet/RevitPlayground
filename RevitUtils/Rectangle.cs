/*Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.*/

namespace RevitUtils
{
    public class Rectangle
    {
        public double MinX { get; }
        public double MinY { get; }
        public double MaxX { get; }
        public double MaxY { get; }


        public static Rectangle Infinite = new Rectangle(Point2D.Min, Point2D.Max);

        public Rectangle(Point2D min, Point2D max)
        {
            MinX = min.X;
            MinY = min.Y;
            MaxX = max.X;
            MaxY = max.Y;
        }

        public Rectangle(double minX, double minY, double maxX, double maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }

        public override bool Equals(object obj)
        {
            return obj is Rectangle rectangle &&
                   MinX == rectangle.MinX &&
                   MinY == rectangle.MinY &&
                   MaxX == rectangle.MaxX &&
                   MaxY == rectangle.MaxY;
        }

        public override int GetHashCode()
        {
            int hashCode = -1882349014;
            hashCode = hashCode * -1521134295 + MinX.GetHashCode();
            hashCode = hashCode * -1521134295 + MinY.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxX.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxY.GetHashCode();
            return hashCode;
        }
    }
}
