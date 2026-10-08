using System;
using System.Collections;
using System.Collections.Generic;
using Exceptions;
using Interfaces;

namespace Collections
{
    public class MyCustomCollection<T> : ICustomCollection<T>, IEnumerable<T>
    {
        private class Node
        {
            public T Data { get; set; }
            public Node? Next { get; set; }
            public Node(T data) { Data = data; Next = null; }
        }

        private Node? head;
        private Node? cursor;
        private int count;

        public MyCustomCollection()
        {
            head = null;
            cursor = null;
            count = 0;
        }

        public int Count => count;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException(
                        $"Индекс {index} за пределами диапазона [0, {count - 1}].");
                Node temp = head!;
                for (int i = 0; i < index; i++) temp = temp.Next!;
                return temp.Data;
            }
            set
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException(
                        $"Индекс {index} за пределами диапазона [0, {count - 1}].");
                Node temp = head!;
                for (int i = 0; i < index; i++) temp = temp.Next!;
                temp.Data = value;
            }
        }

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
                Node temp = head; // head уже проверен на null
                while (temp.Next != null) temp = temp.Next;
                temp.Next = newNode;
            }
            count++;
        }

        public void Reset() => cursor = head;

        public void MoveNext()
        {
            if (cursor != null) cursor = cursor.Next;
        }

        public T Current()
        {
            if (cursor == null)
                throw new InvalidOperationException("Курсор не указывает на элемент.");
            return cursor.Data;
        }

        public void Remove(T item)
        {
            if (head == null)
                throw new ItemNotFoundException(
                    $"Коллекция пуста. Элемент '{item}' не найден.");

            if (EqualityComparer<T>.Default.Equals(head.Data, item))
            {
                if (cursor == head) cursor = head.Next;
                head = head.Next;
                count--;
                return;
            }

            Node current = head;
            while (current.Next != null &&
                   !EqualityComparer<T>.Default.Equals(current.Next.Data, item))
            {
                current = current.Next;
            }

            if (current.Next == null)
                throw new ItemNotFoundException(
                    $"Элемент '{item}' не найден в коллекции.");

            if (cursor == current.Next) cursor = current.Next.Next;
            current.Next = current.Next.Next;
            count--;
        }

        public T RemoveCurrent()
        {
            if (cursor == null)
                throw new InvalidOperationException("Курсор не указывает на элемент.");
            T data = cursor.Data;
            Remove(data);
            return data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node? current = head;
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