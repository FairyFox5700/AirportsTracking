using System;

namespace Structures
{
    class List<T>
    {
        T[] array;
        int _size;
        int count = 0;

        public List(int size = 4)
        {
            this._size = size;
            array = new T[_size];
        }

        public void Add(T data)
        {
            if (count >= _size)
                Expand();
            array[count] = data;
            count++;
        }

        public void Remove(int index)
        {
            if (index < count && index >= 0)
            {
                for(int i=index;i<count-1; i++)
                    array[i] = array[i + 1];
                
                if(_size/count > 2)
                    Expand();
                count--;
            }
            else throw new Exception("IndexOutofBoundsException");
        }

        public int Count()
        {
            return count;
        }

        public T this[int index]
        {
            get
            {
                if (index < count && index >= 0)
                    return array[index];
                else
                    throw new IndexOutOfRangeException("Index is out of range!");
            }
            set
            {
                if (index < count && index >= 0)
                    array[index] = value;
                else
                    throw new IndexOutOfRangeException("Index is out of range!");
            }
        }

        void Expand()
        {
            _size = count * 2;
            T[] newarray = new T[_size];
            array.CopyTo(newarray, 0);
            array = newarray;
        }
    }
}
