using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitUtils
{
    /// <summary>
    /// A class used to find the closest point in a 2D Space
    /// 
    /// It is often useful to be able to find the closes element in 2D space.  This class is an optimized way to do that.
    /// 
    /// Note for beginners: If you look for an element contained in the tree, it will git you that element.  Nothing
    /// can be closer than itself.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KdTree2D<T>
    {

        private Node head;
        /// <summary>
        /// A count of the elements contained in the KdTree
        /// </summary>
        public int count { get; private set; }

        /// <summary>
        /// Constructor for KdTree2D
        /// </summary>
        public KdTree2D()
        {
            count = 0;
        }

        /// <summary>
        /// Place Data on the tree at the specified position.
        /// </summary>
        /// <param name="data">Data to store</param>
        /// <param name="position">Position associated with the data</param>
        public void Put(T data, Point2D position)
        {
            //Position is requried to place data
            if (position == null)
                throw new ArgumentNullException("Position cannot be null");
            head = Put(head, data, position, Rectangle.Infinite, true);
            count++;
        }

        /// <summary>
        /// Recursive Put method.  Traverses the tree to place Data in the position corresponding with the position.
        /// </summary>
        /// <param name="current">Current Tree node</param>
        /// <param name="Data">Data to store</param>
        /// <param name="position">Position associated with the data</param>
        /// <param name="rect">A rectangular prism used to prune and optimize searches</param>
        /// <param name="xyz">Argument checks against X(true), Y(false)</param>
        /// <returns></returns>
        private Node Put(Node current, T Data, Point2D position, Rectangle rect, bool xy)
        {
            //If node is null, the data goes here.
            if (current == null)
                return new Node(Data, position, rect);

            //Check X,Y, or Z
            if(xy)
            {
                if (position.X < current.Position.X)
                {
                    current.less = Put(current.less, Data, position,
                        new Rectangle(rect.MinX, rect.MinY, current.Position.X, rect.MaxY)
                        , !xy);
                }
                else
                {
                    current.more = Put(current.less, Data, position,
                        new Rectangle(current.Position.X, rect.MinY,  rect.MaxX, rect.MaxY)
                        , !xy);
                }
            }
            //Check Y
            else
            {
                if (position.Y < current.Position.Y)
                {
                    current.less = Put(current.less, Data, position,
                        new Rectangle(rect.MinX, rect.MinY, rect.MaxX, current.Position.Y)
                        , !xy);
                }
                else
                {
                    current.more = Put(current.less, Data, position,
                        new Rectangle(rect.MinX, current.Position.Y, rect.MaxX, rect.MaxY)
                        , !xy);
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
            return Get(head, point, true);
        }

        /// <summary>
        /// Recursive get method
        /// </summary>
        /// <param name="current">Current search node</param>
        /// <param name="position">Position to search for</param>
        /// <param name="xyz">Flag to check X(true), Y(false)</param>
        /// <returns>The Data at the point, returns the default value if not found</returns>
        private T Get(Node current, Point3D position, bool xy)
        {
            //Did not find
            if (current == null)
                return default(T);
            //Found
            if (current.Position.Equals(position))
                return current.Data;
            //Search
            //Check X,Y, or Z
            if (xy)
            {
                if (position.X < current.Position.X)
                {
                    return Get(current.less, position, !xy);
                }
                else
                {
                    return Get(current.less, position, !xy);
                }
            }
            //Check Y
            else
            {
                if (position.Y < current.Position.Y)
                {
                    return Get(current.less, position, !xy);
                }
                else
                {
                    return Get(current.less, position, !xy);
                }
            }
        }

        /// <summary>
        /// Returns the data Nearest to the given point
        /// </summary>
        /// <param name="point">Position to search for data near</param>
        /// <returns>The data closest to the point passed in.</returns>
        public T Nearest(Point2D point)
        {
            if (count == 0)
                throw new InvalidOperationException("Tree has no elements");
            Node champ = head;
            Nearest(head, point, true, ref champ);
            return champ.Data;
        }

        /// <summary>
        /// Recursive search for the nearest node
        /// </summary>
        /// <param name="current">Current Node to search from</param>
        /// <param name="position">Position to compare to (find nearest to this point)</param>
        /// <param name="xyz">Flag to check X(true), Y(false)</param>
        /// <param name="nearest">Current closest node</param>
        private void Nearest(Node current, Point2D position, bool xy, ref Node nearest)
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
            if (xy)
            {
                //Check in the correct direction first
                if (position.X < current.Position.X)
                {
                    Nearest(current.less, position, !xy, ref nearest);
                    Nearest(current.more, position, !xy, ref nearest);
                }
                else
                {
                    Nearest(current.more, position, !xy, ref nearest);
                    Nearest(current.less, position, !xy, ref nearest);
                }
            }
            //Check Y
            else
            {
                //Check in the correct direction first
                if (position.Y < current.Position.Y)
                {
                    Nearest(current.less, position, !xy, ref nearest);
                    Nearest(current.more, position, !xy, ref nearest);
                }
                else
                {
                    Nearest(current.more, position, !xy, ref nearest);
                    Nearest(current.less, position, !xy, ref nearest);
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
            public Rectangle RectPrism { get; }
            /// <summary>
            /// Position associated with the data.
            /// </summary>
            public Point2D Position { get; }
            /// <summary>
            /// Node that compares less than the parent node for the XY flag.
            /// </summary>
            public Node less { get; set; }
            /// <summary>
            /// Node that compares greater than the parent node for the XY flag.
            /// </summary>
            public Node more { get; set; }

            /// <summary>
            /// Constructor for the node class
            /// </summary>
            /// <param name="data">Data stored in the node</param>
            /// <param name="position">Position associated with the data</param>
            /// <param name="rectPrism">Rectangular prism used to prune and optimize nearest search</param>
            public Node(T data, Point2D position, Rectangle rectPrism)
            {
                Data = data;
                Position = position;
                RectPrism = rectPrism;
            }
        }
    }
}
