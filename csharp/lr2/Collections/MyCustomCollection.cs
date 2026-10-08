using System;
using System.Collections;
using System.Collections.Generic;
using GGGGGG_NNN_Lab2.Interfaces;

namespace GGGGGG_NNN_Lab2.Collections
{
    // Internal node for linked list
    internal class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class MyCustomCollection<T> : ICustomCollection<T>, IEnumerable<T>
    {
        private Node<T> _head;
        private Node<T> _currentNode;
        private int _count;

        public int Count => _count;

        public MyCustomCollection()
        {
            _head = null;
            _currentNode = null;
            _count = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException($"Index {index} is out of range.");

                Node<T> current = _head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                return current.Data;
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException($"Index {index} is out of range.");

                Node<T> current = _head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                current.Data = value;
            }
        }

        public void Add(T item)
        {
            Node<T> newNode = new Node<T>(item);
            if (_head == null)
            {
                _head = newNode;
                _currentNode = _head; // Reset cursor to head if it was null or just started
            }
            else
            {
                Node<T> current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            _count++;
        }

        public void Remove(T item)
        {
            if (_head == null) return;

            // Special case: removing head
            if (EqualityComparer<T>.Default.Equals(_head.Data, item))
            {
                _head = _head.Next;
                _count--;
                if (_count == 0) _currentNode = null;
                else _currentNode = _head;
                return;
            }

            Node<T> current = _head;
            while (current.Next != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Next.Data, item))
                {
                    current.Next = current.Next.Next;
                    _count--;
                    return;
                }
                current = current.Next;
            }

            // Item not found
            throw new Exception($"Item not found in collection.");
        }

        public T RemoveCurrent()
        {
            if (_currentNode == null) throw new Exception("Cursor is not positioned.");

            // Simplified removal of current node requires tracking previous or restarting from head
            // For simplicity in this lab context, we might just remove by value if unique, 
            // but strict linked list removal of 'current' usually needs prev pointer.
            // Let's assume we remove the item at current position by value search for robustness 
            // or implement a doubly linked list. 
            // Given constraints, let's stick to removing by value logic or simple head/tail handling.
            // To strictly follow "RemoveCurrent", we need to find the node before _currentNode.

            if (_currentNode == _head)
            {
                T data = _currentNode.Data;
                _head = _head.Next;
                _currentNode = _head;
                _count--;
                return data;
            }

            Node<T> prev = _head;
            while (prev.Next != _currentNode)
            {
                prev = prev.Next;
            }

            T removedData = _currentNode.Data;
            prev.Next = _currentNode.Next;
            _currentNode = prev.Next; // Move cursor to next
            _count--;
            return removedData;
        }

        public void Reset()
        {
            _currentNode = _head;
        }

        public void MoveNext()
        {
            if (_currentNode != null)
            {
                _currentNode = _currentNode.Next;
            }
        }

        public T Current()
        {
            if (_currentNode == null) throw new Exception("Cursor is null.");
            return _currentNode.Data;
        }

        // IEnumerable implementation
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}