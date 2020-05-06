using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStructures
{
    public class BinaryTree<T> where T : IComparable
    {
        private TreeNode<T> root;
        int count;

        /// <summary>
        /// Initializes and sets the root of tree with value
        /// </summary>
        /// <param name="value"></param>
        public BinaryTree(T value)
        {
            root = new TreeNode<T>(value);
            count = 1;
        }
        //index override
        public T this[int index]
        {
            get => Bfs(index).value;
            set
            {

                //Using breadth-first search
                Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>(1);
                if (root == null)
                    throw new IndexOutOfRangeException();
                else
                {
                    TreeNode<T> tempRoot = root;
                    while (index != 0)
                    {
                        queue.Capacity++;
                        if (tempRoot.leftSib != null)
                            queue.Enqueue(tempRoot.leftSib);
                        if (tempRoot.rightSib != null)
                            queue.Enqueue(tempRoot.rightSib);
                        tempRoot = queue.Dequeue();
                        index--;
                    }
                    tempRoot.value = value;

                }
            }
        }


        private TreeNode<T> Bfs(int index)
        {
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>(1);
            if (root == null)
                throw new IndexOutOfRangeException();
            else
            {
                TreeNode<T> tempRoot = root;
                while (index != 0)
                {
                    queue.Capacity++;
                    if (tempRoot.leftSib != null)
                        queue.Enqueue(tempRoot.leftSib);
                    if (tempRoot.rightSib != null)
                        queue.Enqueue(tempRoot.rightSib);
                    tempRoot = queue.Dequeue();
                    index--;
                }
                return tempRoot;

            }

        }

        /// <summary>
        /// Gets right number of nodes in tree
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Adds node to tree
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            count++;
            if (root == null)
                root = new TreeNode<T>(value);
            else
            {
                TreeNode<T> temp = new TreeNode<T>(value);
                //Adds node using recursion
                RecursiveAdd(root, temp);
            }
        }

        private void RecursiveAdd(TreeNode<T> current, TreeNode<T> temp)
        {
            //right sib
            if (current < temp)
            {
                if (current.rightSib == null)
                {
                    current.rightSib = temp;
                    temp.parent = current;
                }
                else
                    RecursiveAdd(current.rightSib, temp);
            }
            //left sib
            else
            {
                if (current.leftSib == null)
                {
                    current.leftSib = temp;
                    temp.parent = current;
                }
                else
                    RecursiveAdd(current.leftSib, temp);
            }
        }

        /// <summary>
        /// Return index of value. If tree doesn't contain such value, return -1
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Search(T value)
        {
            for (int i = 0; i < this.count; i++)
                if (this[i].CompareTo(value) == 0)
                    return i;
            return -1;
        }

        /// <summary>
        /// Return string with all leaves int the tree
        /// </summary>
        /// <returns></returns>
        public string Leaves()
        {
            string res = "";
            if (root != null)
            {
                Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>(1);
                TreeNode<T> tempRoot = root;
                do
                {
                    queue.Capacity++;
                    if (tempRoot.leftSib == null && tempRoot.rightSib == null)
                        res += $"{tempRoot.value} ";

                    if (tempRoot.leftSib != null)
                        queue.Enqueue(tempRoot.leftSib);
                    if (tempRoot.rightSib != null)
                        queue.Enqueue(tempRoot.rightSib);
                    tempRoot = queue.Dequeue();
                } while (queue.Count != 0);
                if (tempRoot.leftSib == null && tempRoot.rightSib == null)
                    res += $"{tempRoot.value} ";
                return res;

            }
            return res;
        }

        /// <summary>
        /// Deletes value at index
        /// </summary>
        /// <param name="index"></param>
        public void DeleteAt(int index)
        {
            if (index < count && index >= 0)
            {
                Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>(1);
                TreeNode<T> tempRoot = root;
                if (index != 0)
                {
                    do
                    {
                        queue.Capacity++;
                        if (tempRoot.leftSib != null)
                            queue.Enqueue(tempRoot.leftSib);
                        if (tempRoot.rightSib != null)
                            queue.Enqueue(tempRoot.rightSib);
                        tempRoot = queue.Dequeue();
                        index--;
                    } while (index != 0);
                }
                if (tempRoot.parent > tempRoot)
                    tempRoot.parent.leftSib = null;
                else
                    tempRoot.parent.rightSib = null;
                tempRoot.parent = null;
            }
            else throw new IndexOutOfRangeException();
        }
        
        /// <summary>
        /// Deletes node with needed value. If deletion is completed, retrun true othervise return false 
        /// </summary>
        /// <param name="value"></param>
        public bool DeleteValue(T value)
        {
            //Index of needed value
            int index = this.Search(value);
            if (index != -1)
            {
                this.DeleteAt(index);
                return true;
            }
            else
                return false;
        }

        //String override
        public override string ToString()
        {
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>(1);
            string res = "";
            if (root == null)
                return res;
            else
            {
                TreeNode<T> tempRoot = root;
                do
                {
                    res += $"{tempRoot.value} ";
                    queue.Capacity++;
                    if (tempRoot.leftSib != null)
                        queue.Enqueue(tempRoot.leftSib);
                    if (tempRoot.rightSib != null)
                        queue.Enqueue(tempRoot.rightSib);
                    tempRoot = queue.Dequeue();
                } while (queue.Count != 0);
                res += $"{tempRoot.value} ";
                return res;

            }
        }


    }

}
