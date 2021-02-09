/*Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.*/

using System;

namespace RevitUtils
{
    /// <summary>
    /// A class used to find the closest point in a 3D Space
    /// 
    /// It is often useful to be able to find the closes element in 3D space.  This class is an optimized way to do that.
    /// 
    /// Note for beginners: If you look for an element contained in the tree, it will git you that element.  Nothing
    /// can be closer than itself.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KdTree3D<T>
    {
        private Node head;
        /// <summary>
        /// A count of the elements contained in the KdTree
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Constructor for KdTree3D
        /// </summary>
        public KdTree3D()
        {
            Count = 0;
        }

        /// <summary>
        /// Place Data on the tree at the specified position.
        /// </summary>
        /// <param name="data">Data to store</param>
        /// <param name="position">Position associated with the data</param>
        public void Put(T data, Point3D position)
        {
            //Position is requried to place data
            if (position == null)
                throw new ArgumentNullException("Position cannot be null");
            head = Put(head, data, position, RectPrism.Infinite, 0);
            Count++;
        }

        /// <summary>
        /// Recursive Put method.  Traverses the tree to place "Data" in the position corresponding with the position.
        /// </summary>
        /// <param name="current">Current Tree node</param>
        /// <param name="Data">Data to store</param>
        /// <param name="position">Position associated with the data</param>
        /// <param name="rect">A rectangular prism used to prune and optimize searches</param>
        /// <param name="xyz">Argument checks against X(0), Y(1), or Z(2)</param>
        /// <returns></returns>
        private Node Put(Node current, T Data, Point3D position, RectPrism rect, int xyz)
        {
            //If node is null, the data goes here.
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
                    current.more = Put(current.more, Data, position,
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
                    current.more = Put(current.more, Data, position,
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
                    current.more = Put(current.more, Data, position,
                        new RectPrism(rect.MinX, rect.MinY, current.Position.Z, rect.MaxX, rect.MaxY, rect.MaxZ)
                        , 0);
                }
            }

            return current;
        }

        /// <summary>
        /// Get the Data at the specified point
        /// </summary>
        /// <param name="point">Point to find data at</param>
        /// <returns>The Data at the point, returns the default value if not found</returns>
        public T Get(Point3D point)
        {
            return Get(head, point, 0);
        }

        /// <summary>
        /// Recursive get method
        /// </summary>
        /// <param name="current">Current search node</param>
        /// <param name="position">Position to search for</param>
        /// <param name="xyz">Flag to check X(0), Y(1), or Z(2)</param>
        /// <returns>The Data at the point, returns the default value if not found</returns>
        private T Get(Node current, Point3D position, int xyz)
        {
            //Did not find
            if (current == null)
                return default(T);
            //Found
            if (current.Position.Equals(position))
                return current.Data;
            //Search
            //Check X,Y, or Z
            if (xyz == 0)
            {
                if (position.X < current.Position.X)
                {
                    return Get(current.less, position, 1);
                }
                else
                {
                    return Get(current.less, position, 1);
                }
            }
            //Check Y
            else if (xyz == 1)
            {
                if (position.Y < current.Position.Y)
                {
                    return Get(current.less, position, 2);
                }
                else
                {
                    return Get(current.less, position, 2);
                }
            }
            //Check Z
            else
            {
                if (position.Z < current.Position.Z)
                {
                    return Get(current.less, position, 0);
                }
                else
                {
                    return Get(current.less, position, 0);
                }
            }
        }

        /// <summary>
        /// Returns the data Nearest to the given point
        /// </summary>
        /// <param name="point">Position to search for data near</param>
        /// <returns>The data closest to the point passed in.</returns>
        public T Nearest(Point3D point)
        {
            if (Count == 0)
                throw new InvalidOperationException("Tree has no elements");
            Node champ = head;
            Nearest(head, point, 0, ref champ);
            return champ.Data;
        }

        /// <summary>
        /// Recursive search for the nearest node
        /// </summary>
        /// <param name="current">Current Node to search from</param>
        /// <param name="position">Position to compare to (find nearest to this point)</param>
        /// <param name="xyz">Flag to check X(0), Y(1), Z(2)</param>
        /// <param name="nearest">Current closest node</param>
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
                //Check in the correct direction first
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
                //Check in the correct direction first
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
                //Check in the correct direction first
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

        /// <summary>
        /// Private Node class.  Used to store and traverse KdTree.
        /// </summary>
        private class Node
        {
            /// <summary>
            /// Data stored in this node
            /// </summary>
            public T Data { get; }
            /// <summary>
            /// Rectangular Prism used to prune and optimize nearest search
            /// </summary>
            public RectPrism RectPrism { get; }
            /// <summary>
            /// Position associated with the data.
            /// </summary>
            public Point3D Position { get; }
            /// <summary>
            /// Node that compares less than the parent node for the XYZ flag.
            /// </summary>
            public Node less { get; set; }
            /// <summary>
            /// Node that compares greater than the parent node for the XYZ flag.
            /// </summary>
            public Node more { get; set; }

            /// <summary>
            /// Constructor for the node class
            /// </summary>
            /// <param name="data">Data stored in the node</param>
            /// <param name="position">Position associated with the data</param>
            /// <param name="rectPrism">Rectangular prism used to prune and optimize nearest search</param>
            public Node(T data, Point3D position, RectPrism rectPrism)
            {
                Data = data;
                Position = position;
                RectPrism = rectPrism;
            }
        }
    }
}
