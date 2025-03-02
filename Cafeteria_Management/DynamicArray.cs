namespace DynamicArray
{
    public class DArray<T>
    {
        private T[] data;
        private int count;
        private int capacity;

        public DArray()
        {
            capacity = 4;
            data = new T[capacity];
            count = 0;
        }

        public void Print()
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(data[i]);
            }
        }

        public void Add(T item)
        {
            if (count == capacity)
            {
                Expand();
            }
            data[count] = item;
            count++;
        }

        public void RemoveAt(int index)
        {
            if (count < capacity / 4)
            {
                Shrink();
            }
            if (index < 0 || index >= count)
            {
                return;
            }

            for (int i = index; i < count - 1; i++)
            {
                data[i] = data[i + 1];
            }
            count--;
        }

        private void Expand()
        {
            capacity = capacity * 2;
            T[] newData = new T[capacity];
            Array.Copy(data, newData, count);
            data = newData;
        }

        private void Shrink()
        {
            capacity = capacity / 2;
            T[] newData = new T[capacity];
            Array.Copy(data, newData, count);
            data = newData;
        }

        public T Get(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Index out of bounds.");
            }
            return data[index];
        }

        public int Count => count;
    }
}
