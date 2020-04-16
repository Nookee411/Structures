using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Classes
{
    public class Node<T>
    {
        public T val { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }
        public Node(T val)
        {
            this.val = val;
        }
    }

    public class TreeNode<T>
    {
        public T Data { get; set; }
        public Node<T> LeftSibling { get; set; }
        public Node<T> RightSibling { get; set; }

        public TreeNode(T Data)
        {
            this.Data = Data;
        }
    }

    public class DoubleOrderedList<T> : IEnumerable<T>
    {
        Node<T> head;
        Node<T> tail;
        int count;

        //Constructors
        public DoubleOrderedList() //Empty consrtuctor
        {
            
            this.head = null;
            tail = null;
            count = 0;
        }
        public DoubleOrderedList(T []Data)
        {
            foreach(T el in Data)
            {
                this.Add(el);
            }
        }
        public int Count => count;
        public bool IsEmpty => (count == 0);

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
            List<int> a = new List<int>();
        }
        
        public void AddRange(IEnumerable<T>collection)
        {
            foreach (T el in collection)
                this.Add(el);
        }

        public void Erase(int index)
        {
            if (index >= count)
                throw new Exception("Index out of range");            
            if(index < count/2)
            {
                if (count == 1)
                    this.Clear();
                else if (index == 0)
                    this.PopFront();
                else if (index == count - 1)
                    this.PopBack();
                else
                {
                    Node<T> todelete = head;
                    for (int i = 0; i < index; i++)
                    {
                        todelete = todelete.Next;
                    }
                    todelete.Prev.Next = todelete.Next;
                    todelete.Next.Prev = todelete.Prev;
                }
            }
            else
            {
                Node<T> todelete = tail;
                for(int i=0;i!=(count-1)-index;i++)
                {
                    todelete = todelete.Prev;
                    i++;
                }
                if(index!=0)
                    todelete.Prev.Next = todelete.Next;
                if(index!= count-1)
                    todelete.Next.Prev = todelete.Prev;
            }
            count--;

        }

        public void PopFront()
        {
            head = head.Next;
            head.Prev = null;
        }
        public void PopBack()
        {
            tail = tail.Prev;
            tail.Next = null;
        }
        
        public void Add(T Data)
        {
            Node<T> temp = new Node<T>(Data);
            if (head == null)
                head = temp;
            else
            {
                tail.Next = temp;
                temp.Prev = tail;
            }
            tail = temp;
            count++;
        }
        public void AddFront(T Data)
        {
            Node<T> temp = new Node<T>(Data);
            if (head == null)
                head = temp;
            else
            {
                temp.Next = head;
                head.Prev = temp;
                head = temp;
            }
            count++;
        }
        //Override

        //Output
        public override string ToString()
        {
            Node<T> temp = head;
            string res = "";
            for (int i = 0; i < count&&temp!=null; i++, temp = temp.Next)
                res += temp.val.ToString()+" ";
            return res;
        }

        //equals
        public override bool Equals(object obj)
        {
            DoubleOrderedList<T> b = (DoubleOrderedList<T>)obj;
            if (this.count != b.count)
                return false;
            Node<T> temp1 = this.head;
            Node<T> temp2 = b.head;
            for (int i = 0;i<this.count;i++)
            {
                if(!temp1.val.Equals(temp2.val))
                {
                    return false;
                }
                temp1 = temp1.Next;
                temp2 = temp2.Next;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return count;
        }

        //Indexing
        public T this[int index]
        {
            get 
            {
                if (index < 0 && index >= this.count)
                    throw new Exception("Index out of range.");
                //Prev and next allows us to get and set index at n/2
                int i = 0;
                if (index < count / 2)
                {
                    Node<T> temp = head;
                    while (index !=i)
                    {
                        temp = temp.Next;
                        i++;
                    }
                    return temp.val;
                }
                else
                {
                    Node<T> temp = tail;
                    while ((count-1)-index != i)
                    {
                        temp = temp.Prev;
                        i++;
                    }
                    return temp.val;
                }
            }

            set
            {
                if (index < 0 && index >= this.count)
                    throw new Exception("Index out range.");
                int i = 0;
                if (index < count / 2)
                {
                    Node<T> temp = head;
                    while (i != index)
                    {
                        temp = temp.Next;
                        i++;
                    }
                    temp.val = value;
                }
                else
                {
                    Node<T> temp = tail;
                    while ((count - 1) - index != i)
                    {
                        temp = temp.Prev;
                        i++;
                    }
                    temp.val = value;
                }
                return;
            }
        }

        //IEnumerable realization
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.val;
                current = current.Next;
            }
        }

        public IEnumerable<T> BackEnumerator()
        {
            Node<T> current = tail;
            while (current != null)
            {
                yield return current.val;
                current = current.Prev;
            }
        }
    }
}


//как перегрузить индексатор чтобы возвращались разные значения
//как сделать перегрузки GetHashCode()