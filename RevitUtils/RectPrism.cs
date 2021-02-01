using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitUtils
{
    public class RectPrism
    {
        public double MinX { get; }
        public double MinY { get; }
        public double MinZ { get; }
        public double MaxX { get; }
        public double MaxY { get; }
        public double MaxZ { get; }

        public static RectPrism Infinite = new RectPrism(Point3D.Min, Point3D.Max);

        public RectPrism(Point3D min, Point3D max)
        {
            MinX = min.X;
            MinY = min.Y;
            MinZ = min.Z;
            MaxX = max.X;
            MaxY = max.Y;
            MaxZ = max.Z;
        }

        public RectPrism(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
        {
            MinX = minX;
            MinY = minY;
            MinZ = minZ;
            MaxX = maxX;
            MaxY = maxY;
            MaxZ = maxZ;
        }
    }
}
