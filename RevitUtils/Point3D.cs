using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitUtils
{
    public class Point3D
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public static Point3D Min = new Point3D(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);
        public static Point3D Max = new Point3D(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double DistTo(Point3D p)
        {
            return Math.Sqrt(Math.Pow(p.X - X, 2) + Math.Pow(p.Y - Y, 2) + Math.Pow(p.Z - Z, 2));
        }

        public double DistTo(RectPrism rect)
        {
            double dx = Math.Max(rect.MinX - X, Math.Max(0, X - rect.MaxX));
            double dy = Math.Max(rect.MinY - Y, Math.Max(0, Y - rect.MaxY));
            double dz = Math.Max(rect.MinZ - Z, Math.Max(0, Z - rect.MaxZ));
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public override bool Equals(object obj)
        {
            return obj is Point3D d &&
                   X == d.X &&
                   Y == d.Y &&
                   Z == d.Z;
        }

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
