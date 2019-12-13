using System;
using System.Collections;
using System.Collections.Generic;

namespace PathfindingConsoleProject.DataStructures
{
    public class GenericList<T> : ICollection<T>
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

        public bool IsReadOnly => throw new NotImplementedException();

        public GenericList()
        {
            list = new T[4];
        }

        public void Add(T item)
        {
            Count++;
        }

        public void Clear()
        {
            Count = 0;
        }

        public bool Contains(T item)
        {
            
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            
        }

        public bool Remove(T item)
        {
            Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            
        }
    }
}
