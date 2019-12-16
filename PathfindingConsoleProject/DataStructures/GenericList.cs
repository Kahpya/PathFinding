using System;
using System.Collections;
using System.Collections.Generic;

namespace PathfindingConsoleProject.DataStructures
{
    public class GenericList<T> : ICollection<T>, ICollection
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
                    return;
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
            AddRange(new T[] { item } );
        }

        public void AddRange(T[] items)
        {
            if (Count + items.Length > size)
            {
                Resize(items.Length);
            }

            for (int i = 0; i < items.Length; i++)
            {
                list[Count++] = items[i];
            }
        }

        public void Clear()
        {
            list = new T[4];
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

        public void CopyTo(Array array, int index)
        {
            list.CopyTo(array, index);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            CopyTo(array as Array, arrayIndex);
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
                    if (Count > 1 && (j + 1) <= size)
                    {
                        list[j] = list[j + 1];
                    }
                    else
                    {
                        list[j] = default(T);
                    }
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


        private void Resize(int size)
        {
            int newSize = this.size;
            T[] temp = new T[this.size];

            CopyTo(temp, 0);

            while (Count + size > newSize)
            {
                newSize *= 2;
            }

            list = new T[newSize];

            temp.CopyTo(list, 0);
        }
    }
}
