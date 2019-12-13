using System;
using System.Collections;
using System.Collections.Generic;

namespace PathfindingConsoleProject.DataStructures
{
    public class GenericList<T> : ICollection<T>, ICollection where T :IComparable<T>
    {
        public T this[int index]
        {
            get
            {
                if (Count != 0 && 0 <= index && index < Count)
                {
                    return list[index];
                }

                throw new IndexOutOfRangeException();

            }
            set
            {
                if (0 <= index && index < Count)
                {
                    list[index] = value;
                }

                throw new IndexOutOfRangeException();
            }
        }

        public int Count 
        { 
            get; 
            private set; 
        } = 0;

        private T[] list;
        private int size;

        public bool IsReadOnly => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public GenericList() : this(4)
        { }

        public GenericList(int size)
        {
            list = new T[size];
            this.size = size;
        }

        public void Add(T item)
        {
            list[Count] = item;
            Count++;

            Resize();
        }

        public void Clear()
        {
            list = new T[size];
            Count = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(list[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            
        }

        public bool Remove(T item)
        {
            int removeIndex = -1;

            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(list[i]))
                {
                    removeIndex = i;
                    break;
                }
            }

            if (removeIndex != -1)
            {
                for (int j = removeIndex;  j < Count;  j++)
                {
                    list[j] = list[j + 1];
                }

                Count--;

                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return list[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Resize()
        {
            if (Count == size)
            {
                T[] temp = new T[size];
                list.CopyTo(temp, 0);

                int newSize = size * 2;

                list = new T[newSize];
                size = newSize;

                temp.CopyTo(list, 0);
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }
    }
}
