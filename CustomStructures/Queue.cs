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
        private Node<T> last; //link to last node
        private Node<T> first;//ling to first node
        private int capacity; // Total capacity

        /// <summary>
        /// Constructor which sets capacity
        /// </summary>
        /// <param name="capacity"></param>
        public Queue(int capacity) 
        {
            last = null;
            first = null;
            if (capacity > 0)
                this.capacity = capacity;
            else
                this.capacity = 0;
        }

        /// <summary>
        /// Constructor sets capacity and fills it with value
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="value"></param>
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

        //Index override
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

        /// <summary>
        /// Property gets current amount of nodes in queue
        /// </summary>
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


        /// <summary>
        /// Gets first value in queue and NOT removing it
        /// </summary>
        public T Peak => first.value;

        /// <summary>
        /// Adds value to queue
        /// </summary>
        /// <param name="value"></param>
        public void Enqueue(T value)
        {
            if (Count != capacity)//if there are available space for adding
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

        /// <summary>
        /// Deletes value from top of queue and returns it
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            T value = first.value;
            first = first.next;
            return value;
            
        }
        /// <summary>
        /// Gets of sets capacity. If it is less than current size of queue, throws an exeption
        /// </summary>
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

        //To sting override
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

        /// <summary>
        /// Merges two queues
        /// </summary>
        /// <param name="firstQ"></param>
        /// <param name="secondQ"></param>
        /// <returns></returns>
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
