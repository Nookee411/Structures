using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace CustomStructures
{
    public class Queue<T>
    {
        private Node<T> last;
        private Node<T> first;
        private int capacity;

        public Queue(int capacity)
        {
            last = null;
            first = null;
            this.capacity = capacity;
        }
        public Queue(int capacity, T value)
        {
            this.capacity = capacity;
            if (capacity >= 0)
            {
                first = new Node<T>(value);
                Node<T> temp = this.first;
                if (capacity > 1)
                {
                    for (int i = 0; i < capacity - 1; i++)
                    {
                        temp.next = new Node<T>(value);
                        temp = temp.next;
                    }
                }
                last = temp;
            }
            else
                throw new ArgumentException("Capacity can't be less than zero.");
        }

        public T this[int index]
        {
            get 
            {
                if (index < 0||index>capacity)
                    throw new IndexOutOfRangeException();
                Node<T> temp = first;
                for(int i=0;i<index;i++)
                {
                    temp = temp.next;
                    if (temp == null)
                        throw new IndexOutOfRangeException();
                }
                return temp.value;
            }
            set
            {
                if (index < 0||index>capacity)
                    throw new IndexOutOfRangeException();
                Node<T> temp = first;
                for (int i = 0; i < index; i++)
                {
                    if (temp == null)
                        throw new IndexOutOfRangeException();
                    temp = temp.next;
                }
                temp.value= value;
            }
        }


        public int Count
        {
            get
            {
                Node<T> temp = first;
                int counter=0;
                while(temp!=null)
                {
                    counter++;
                    temp = temp.next;
                }
                return counter;
            }
        }

        public T Peak => first.value;


        public void Enqueue(T value)
        {
            if (Count != capacity)
            {
                Node<T> temp = new Node<T>(value);
                if (last == null)
                {
                    first = temp;
                    last = temp;
                }
                else
                {
                    last.next = temp;
                    last = temp;
                }
            }
            else
                throw new StackOverflowException();
        }

        public T Dequeue()
        {
            T value = first.value;
            first = first.next;
            return value;
            
        }

        public int Capacity
        {
            get { return this.capacity; }
            set
            {
                if (capacity < Count)
                    throw new ArgumentOutOfRangeException();
                this.capacity = value; 
            }    
        }

        public override string ToString()
        {
            string res = "";
            Node<T> temp = first;
            while (temp != null)
            {
                res += $"{temp.value} ";
                temp = temp.next;
            }
            return res;

        }

        public static Queue<T> Merge(Queue<T> firstQ, Queue<T> secondQ)
        {
            if (firstQ.Count>0 && secondQ.Count > 0)
            {
                firstQ.last.next = secondQ.first;
                firstQ.last = secondQ.last;

            }
            else
                throw new ArgumentException();
            return firstQ;
        }
    }


    internal class Node<T>
    {
        public T value { get; set; }
        public Node<T> next { get; set; }

        public Node(T value)
        {
            this.value = value;
        }
        public Node()
        {
            
        }
    }


    public class BinaryTree<T> where T : IComparable
    {
        private TreeNode<T> root;
        int count;
         
        public BinaryTree(T value)
        {
            root = new TreeNode<T>(value);
            count = 1;
        }

        public T this[int index]
        {
            get => bfs(index).value;
            set
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
                    tempRoot.value = value;

                }
            }
        }
        

        private TreeNode<T> bfs(int index) 
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

        public int Count => count;
        public void Add(T value)
        {
            count++;
            if(root == null)
                root = new TreeNode<T>(value);
            else
            {
                TreeNode<T> temp = new TreeNode<T>(value);
                RecursiveAdd(root, temp);
            }
        }

        private void RecursiveAdd(TreeNode<T> current,TreeNode<T> temp)
        {
            //right sib
            if (current<temp)
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

        public int Search(T value)
        {
            for (int i = 0; i < this.count; i++)
                if (this[i].CompareTo(value)==0)
                    return i;
            return -1;
        }

        public string Leaves()
        {
            string res = "";
            if(root!=null)
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

        public void DeleteAt(int index)
        {
            if (index < count && index >= 0)
            {
                Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>(1);
                TreeNode<T> tempRoot = root;
                do
                {
                    queue.Capacity++;
                    if (tempRoot.leftSib != null)
                        queue.Enqueue(tempRoot.leftSib);
                    if (tempRoot.rightSib != null)
                        queue.Enqueue(tempRoot.rightSib);
                    tempRoot = queue.Dequeue();
                    index--;
                } while (index !=0);
                tempRoot.parent = null;
            }
            else throw new IndexOutOfRangeException();
        }

        public override string ToString()
        {
            Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>(1);
            string res="";
            if (root == null)
                return res;
            else
            {
                TreeNode<T> tempRoot = root;
                do
                {
                    res += $"{tempRoot.value} ";
                    queue.Capacity++;
                    if(tempRoot.leftSib!=null)
                        queue.Enqueue(tempRoot.leftSib);
                    if(tempRoot.rightSib!=null)
                        queue.Enqueue(tempRoot.rightSib);
                    tempRoot = queue.Dequeue();
                } while (queue.Count != 0);
                res += $"{tempRoot.value} ";
                return res;

            }
        }


    }

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
            if (second != null && first.value.CompareTo(second.value)==1)
                return true;
            else return false;
        }

        public static bool operator <(TreeNode<T> first, TreeNode<T> second)
        {
            if (second != null && first.value.CompareTo(second.value)==-1)
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
            
            return (((TreeNode<T>)obj).value.CompareTo(this.value)==0);
        }

        public override int GetHashCode()
        {
            return ((leftSib!=null)?1:0) + ((rightSib != null) ? 1 : 0);
        }
    }

}
