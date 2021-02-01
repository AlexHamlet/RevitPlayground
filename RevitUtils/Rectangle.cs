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
