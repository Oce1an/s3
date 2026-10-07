using System;
using System.Collections.Generic;
using Interfaces;

namespace Collections
{
    public class MyCustomCollection<T> : ICustomCollection<T>
    {
        private class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }
            public Node(T data) { Data = data; Next = null; }
        }

        private Node head;
        private Node cursor;
        private int count;

        public MyCustomCollection()
        {
            head = null;
            cursor = null;
            count = 0;
        }

        public int Count => count;

        public void Add(T item)
        {
            Node newNode = new Node(item);
            if (head == null)
            {
                head = newNode;
                cursor = head;
            }
            else
            {
                Node temp = head;
                while (temp.Next != null) temp = temp.Next;
                temp.Next = newNode;
            }
            count++;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count) throw new IndexOutOfRangeException("Индекс за пределами диапазона.");
                Node temp = head;
                for (int i = 0; i < index; i++) temp = temp.Next;
                return temp.Data;
            }
            set
            {
                if (index < 0 || index >= count) throw new IndexOutOfRangeException("Индекс за пределами диапазона.");
                Node temp = head;
                for (int i = 0; i < index; i++) temp = temp.Next;
                temp.Data = value;
            }
        }

        public void Reset() => cursor = head;

        public void MoveNext()
        {
            if (cursor != null) cursor = cursor.Next;
        }

        public T Current()
        {
            if (cursor == null) throw new InvalidOperationException("Курсор не указывает на элемент.");
            return cursor.Data;
        }

        public void Remove(T item)
        {
            if (head == null) return;

            if (EqualityComparer<T>.Default.Equals(head.Data, item))
            {
                if (cursor == head) cursor = head.Next;
                head = head.Next;
                count--;
                return;
            }

            Node current = head;
            while (current.Next != null && !EqualityComparer<T>.Default.Equals(current.Next.Data, item))
            {
                current = current.Next;
            }

            if (current.Next != null)
            {
                if (cursor == current.Next) cursor = current.Next.Next;
                current.Next = current.Next.Next;
                count--;
            }
        }

        public T RemoveCurrent()
        {
            if (cursor == null) throw new InvalidOperationException("Курсор не указывает на элемент.");
            T data = cursor.Data;
            Remove(data);
            return data;
        }
    }
}