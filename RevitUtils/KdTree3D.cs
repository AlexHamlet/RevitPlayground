using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitUtils
{
    public class KdTree3D<T>
    {
        private Node head;
        public int count { get; private set; }

        public KdTree3D()
        {
            count = 0;
        }

        public void Put(T data, Point3D position)
        {
            if (position == null)
                throw new ArgumentNullException("Position cannot be null");
            head = Put(head, data, position, RectPrism.Infinite, 0);
            count++;
        }

        private Node Put(Node current, T Data, Point3D position, RectPrism rect, int xyz)
        {
            if (current == null)
                return new Node(Data, position, rect);

            //Check X,Y, or Z
            if (xyz == 0)
            {
                if (position.X < current.Position.X)
                {
                    current.less = Put(current.less, Data, position,
                        new RectPrism(rect.MinX, rect.MinY, rect.MinZ, current.Position.X, rect.MaxY, rect.MaxZ)
                        , 1);
                }
                else
                {
                    current.more = Put(current.less, Data, position,
                        new RectPrism(current.Position.X, rect.MinY, rect.MinZ, rect.MaxX, rect.MaxY, rect.MaxZ)
                        , 1);
                }
            }
            //Check Y
            else if (xyz == 1)
            {
                if (position.Y < current.Position.Y)
                {
                    current.less = Put(current.less, Data, position,
                        new RectPrism(rect.MinX, rect.MinY, rect.MinZ, rect.MaxX, current.Position.Y, rect.MaxZ)
                        , 2);
                }
                else
                {
                    current.more = Put(current.less, Data, position,
                        new RectPrism(rect.MinX, current.Position.Y, rect.MinZ, rect.MaxX, rect.MaxY, rect.MaxZ)
                        , 2);
                }
            }
            //Check Z
            else
            {
                if (position.Z < current.Position.Z)
                {
                    current.less = Put(current.less, Data, position,
                        new RectPrism(rect.MinX, rect.MinY, rect.MinZ, rect.MaxX, rect.MaxY, current.Position.Z)
                        , 0);
                }
                else
                {
                    current.more = Put(current.less, Data, position,
                        new RectPrism(rect.MinX, rect.MinY, current.Position.Z, rect.MaxX, rect.MaxY, rect.MaxZ)
                        , 0);
                }
            }

            return current;
        }

        public T Nearest(Point3D point)
        {
            if (count == 0)
                throw new InvalidOperationException("Tree has no elements");
            Node champ = head;
            Nearest(head, point, 0, ref champ);
            return champ.Data; 
        }

        private void Nearest(Node current, Point3D position, int xyz, ref Node nearest)
        {
            //Done checking branch
            if (current == null)
                return;
            //Branch cannot get closer
            if (nearest.Position.DistTo(position) <= position.DistTo(current.RectPrism))
                return;
            //Assign new nearest
            if (current.Position.DistTo(position) < nearest.Position.DistTo(position))
                nearest = current;
            //Compare and recurse
            if (xyz == 0)
            {
                if (position.X < current.Position.X)
                {
                    Nearest(current.less, position, 1, ref nearest);
                    Nearest(current.more, position, 1, ref nearest);
                }
                else
                {
                    Nearest(current.more, position, 1, ref nearest); 
                    Nearest(current.less, position, 1, ref nearest);
                }
            }
            //Check Y
            else if (xyz == 1)
            {
                if (position.Y < current.Position.Y)
                {
                    Nearest(current.less, position, 2, ref nearest);
                    Nearest(current.more, position, 2, ref nearest);
                }
                else
                {
                    Nearest(current.more, position, 2, ref nearest);
                    Nearest(current.less, position, 2, ref nearest);
                }
            }
            //Check Z
            else
            {
                if (position.Z < current.Position.Z)
                {
                    Nearest(current.less, position, 0, ref nearest);
                    Nearest(current.more, position, 0, ref nearest);
                }
                else
                {
                    Nearest(current.more, position, 0, ref nearest);
                    Nearest(current.less, position, 0, ref nearest);
                }
            }
        }

        private class Node
        {
            public T Data { get; }
            public RectPrism RectPrism { get; }
            public Point3D Position { get; }
            public Node less { get; set; }
            public Node more { get; set; }

            public Node(T data, Point3D position, RectPrism rectPrism)
            {
                Data = data;
                Position = position;
                RectPrism = rectPrism;
            }
        }
    }
}
