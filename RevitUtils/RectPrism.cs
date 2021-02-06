namespace RevitUtils
{
    /// <summary>
    /// A class representing a Rectangular Prism
    /// </summary>
    public class RectPrism
    {
        /// <summary>
        /// Min X coordinate
        /// </summary>
        public double MinX { get; }
        /// <summary>
        /// Min Y coordinate
        /// </summary>
        public double MinY { get; }
        /// <summary>
        /// Min Z coordinate
        /// </summary>
        public double MinZ { get; }
        /// <summary>
        /// Max X coordinate
        /// </summary>
        public double MaxX { get; }
        /// <summary>
        /// Max Y coordinate
        /// </summary>
        public double MaxY { get; }
        /// <summary>
        /// Max Z coordinate
        /// </summary>
        public double MaxZ { get; }
        /// <summary>
        /// An infinitely large rectangular prism.
        /// </summary>
        public static RectPrism Infinite = new RectPrism(Point3D.Min, Point3D.Max);

        /// <summary>
        /// Constructor for Rectangular prism
        /// </summary>
        /// <param name="min">Min Point3D for the prism</param>
        /// <param name="max">Max Point3D for the prism</param>
        public RectPrism(Point3D min, Point3D max):this(Utils.CheckNotNull<Point3D>(min).X, Utils.CheckNotNull<Point3D>(min).Y, Utils.CheckNotNull<Point3D>(min).Z, Utils.CheckNotNull<Point3D>(max).X, Utils.CheckNotNull<Point3D>(max).Y, Utils.CheckNotNull<Point3D>(max).Z)
        {
        }

        /// <summary>
        /// Constructor for Rectangular prism
        /// </summary>
        /// <param name="minX">The minimum X bound</param>
        /// <param name="minY">The minimum Y bound</param>
        /// <param name="minZ">The minimum Z bound</param>
        /// <param name="maxX">The maximum X bound</param>
        /// <param name="maxY">The maximum Y bound</param>
        /// <param name="maxZ">The maximum Z bound</param>
        public RectPrism(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
        {
            MinX = minX;
            MinY = minY;
            MinZ = minZ;
            MaxX = maxX;
            MaxY = maxY;
            MaxZ = maxZ;
        }

        /// <summary>
        /// Equals method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is RectPrism prism &&
                   MinX == prism.MinX &&
                   MinY == prism.MinY &&
                   MinZ == prism.MinZ &&
                   MaxX == prism.MaxX &&
                   MaxY == prism.MaxY &&
                   MaxZ == prism.MaxZ;
        }

        /// <summary>
        /// Hashcode method
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hashCode = -1838259446;
            hashCode = hashCode * -1521134295 + MinX.GetHashCode();
            hashCode = hashCode * -1521134295 + MinY.GetHashCode();
            hashCode = hashCode * -1521134295 + MinZ.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxX.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxY.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxZ.GetHashCode();
            return hashCode;
        }
    }
}
