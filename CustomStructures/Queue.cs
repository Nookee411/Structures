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






}
