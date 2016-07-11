using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manipulation
{
    public class LinkedList<T>
    {
        public Node<T> head;
        public Node<T> tail;

        public LinkedList(T key)
        {
            head = new Node<T>(null, key, null);
            tail = head;
        }

        public LinkedList()
        {
            head = null;
            tail = head;
        }

        public Node<T> find(T key)
        {
            Node<T> searchNode = head;
            if(searchNode != null)
            {
                while(searchNode.data != null & !searchNode.data.Equals(key)
                    && searchNode.next != null)
                {
                    searchNode = searchNode.next;
                }

                return searchNode.data.Equals(key) ? searchNode : null;
            }
            return null;
        }

        public void add(T key)
        {
            if(head == null)
            {
                head = new Node<T>(null, key, tail);
                tail = head;
                return;
            }
            Node<T> newNode = new Node<T>(tail, key, null);
            tail.next = newNode;
            tail = newNode;
        }

        public void remLast()
        {
            tail = tail.prev;
            tail.next = null;
        }

        public void insert(T prevKey, T key)
        {
            Node<T> prev = find(prevKey);
            Node<T> newNode = new Node<T>(prev, key, prev.next);
            prev.next = newNode;
        }

        public void remove(T key)
        {
            Node<T> removeNode = find(key);

            if (removeNode.prev != null)
                removeNode.prev.next = removeNode.next;
            if (removeNode.next != null)
                removeNode.next.prev = removeNode.prev;
            if (removeNode.Equals(head))
                this.head = removeNode.next;
        }

        public void printList()
        {
            Node<T> printNode = head;
            if(printNode != null)
            {
                while(printNode.next != null)
                {
                    Console.Out.Write("%s, ", printNode.data.ToString());
                }
                Console.Out.WriteLine("%s", printNode.data.ToString());
            }
        }

        public class Node<T>
        {
            public Node<T> prev;
            public Node<T> next;
            public T data;
                public Node(Node<T> prev, T data, Node<T> next)
            {
                this.prev = prev;
                this.data = data;
                this.next = next;
            }
        }
    }
}
