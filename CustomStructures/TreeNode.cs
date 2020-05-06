using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    internal class TreeNode<T> where T : IComparable
    {
        public T value { get; set; }
        public TreeNode<T> leftSib { get; set; }
        public TreeNode<T> rightSib { get; set; }
        public TreeNode<T> parent { get; set; }

        public TreeNode(T value)
        {
            this.value = value;
            leftSib = null;
            rightSib = null;
        }

        //public static bool operator ==(TreeNode<T> first, TreeNode<T> second)
        //{
        //    if (first.CompareTo(second) == 0)
        //        return true;
        //    else return false;
        //}

        //public static bool operator !=(TreeNode<T> first, TreeNode<T> second)
        //{
        //    if (second != null && first.CompareTo(second) != 0)
        //        return true;
        //    else return false;
        //}

        public static bool operator >(TreeNode<T> first, TreeNode<T> second)
        {
            if (second != null && first.value.CompareTo(second.value) == 1)
                return true;
            else return false;
        }

        public static bool operator <(TreeNode<T> first, TreeNode<T> second)
        {
            if (second != null && first.value.CompareTo(second.value) == -1)
                return true;
            else return false;
        }

        //public static bool operator >=(TreeNode<T> first, TreeNode<T> second)
        //{
        //    if (second != null && first.CompareTo(second) >= 0)
        //        return true;
        //    else return false;
        //}

        //public static bool operator <=(TreeNode<T> first, TreeNode<T> second)
        //{
        //    if (second != null && first.CompareTo(second) <= 0)
        //        return true;
        //    else return false;
        //}

        public override bool Equals(object obj)
        {

            return (((TreeNode<T>)obj).value.CompareTo(this.value) == 0);
        }

        public override int GetHashCode()
        {
            return ((leftSib != null) ? 1 : 0) + ((rightSib != null) ? 1 : 0);
        }
    }

}
