using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Manipulation
{
    class ArrayList<T>
    {
        protected T[] list;
        private int currentPos = 0;
        private int size = 2;

        public ArrayList()
        {
            this.list = new T[size];
        }

        public ArrayList(T[] list)
        {
            this.list = list;
            currentPos = list.Length;
            grow();
        }

        private void shrink()
        {
            int count = 0;
            int length = currentPos < list.Length ? currentPos : list.Length;
            for(int i = 0; i < length; i++)
            {
                if(list[i] == null)
                {
                    for(int j = i; j < length - 1; j++)
                    {
                        list[j] = list[j + 1];
                    }
                    count++;
                }
            }
            currentPos -= count;
        }

        private void grow()
        {
            T[] tempList = new T[(list.Length * 2)];
            int length = currentPos < list.Length ? currentPos : list.Length;
            for (int i = 0; i < length; i++)
            {
                tempList[i] = list[i];
            }
            list = tempList;
        }

        private void grow(int size)
        {
            T[] tempList = new T[(list.Length + size)];
            int length = currentPos < list.Length ? currentPos : list.Length;
            for(int i = 0; i < length; i++)
            {
                tempList[i] = list[i];
            }
            list = tempList;
        }

        public Boolean add(T data)
        {
            try
            {
                if(currentPos < list.Length)
                {
                    list[currentPos] = data;
                }else
                {
                    grow();
                    list[currentPos] = data;
                }
                currentPos++;
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public void add(int index, T data)
        {
            try
            {
                if (index >= list.Length || currentPos >= list.Length)
                    grow();
                T[] temp = new T[(currentPos - index)];
                for(int i = 0; i < temp.Length; i++)
                {
                    temp[i] = list[(index + i)];
                }
                list[index] = data;
                addAll(temp);
            }catch(IndexOutOfRangeException e)
            {
                Console.Error.Write("Index out of Bounds");
                e.StackTrace.ToString();
            }
        }

        public Boolean addAll(T[] listData)
        {
            try
            {
                grow(listData.Length);
                for(int i = 0; i < listData.Length; i++)
                {
                    list[(currentPos + i)] = listData[i];
                }
                currentPos += listData.Length;
                return true;
            }catch(NullReferenceException e)
            {
                Console.Error.Write("Provided Array references Null");
                e.StackTrace.ToString();
                return false;
            }
        }

        public void clear()
        {
            list = new T[this.size];
        }

        public T get(int pos)
        {
            try
            {
                return list[pos];
            }catch(IndexOutOfRangeException e)
            {
                e.StackTrace.ToString();
                return default(T); //need to verify syntax
            }
        }

        public int indexOf(T data)
        {
            int length = currentPos < list.Length ? currentPos : list.Length;
            for(int i = 0; i < length; i++)
            {
                if (list[i].Equals(data))
                    return i;
            }
            return -1;
        }

        public int lastIndexOf(T data)
        {
            int length = currentPos < list.Length ? currentPos : list.Length;
            int lastKnown = -1;
            for(int i = 0; i < length; i++)
            {
                if (list[i].Equals(data))
                    lastKnown = i;
            }
            return lastKnown;
        }

        public T remove(int index)
        {
            try
            {
                T output = list[index];
                list[index] = default(T); //again check syntax
                shrink();
                return output;
            }catch(IndexOutOfRangeException e)
            {
                Console.Error.Write("Index is out of range");
                e.StackTrace.ToString();
                return default(T); //syntax ?
            }
        }

        protected void removeRange(int startIndex, int lastindex)
        {
            try
            {
                for(int i = startIndex; i < lastindex; i++)
                {
                    list[i] = default(T); //syntax ?
                }
                shrink();
            }catch(IndexOutOfRangeException e)
            {
                Console.Error.Write("Range is out of Bounds");
                e.StackTrace.ToString();
            }
        }

        public T[] clone()
        {
            return list;
        }

        Boolean contains(T data)
        {
            if (indexOf(data) != -1)
                return true;
            return false;
        }

        public T set(int index, T data)
        {
            try
            {
                list[index] = data;
                return list[index];
            }catch(IndexOutOfRangeException e)
            {
                Console.Error.Write("Index out of Range");
                e.StackTrace.ToString();
                return default(T); // syntax ?
            }
        }

        public int _size()
        {
            return this.size = currentPos < list.Length ? currentPos : list.Length;
        }

        public T[] toArray()
        {
            try
            {
                T[] tempArray = new T[list.Length];
                for(int i = 0; i < list.Length; i++)
                {
                    tempArray[i] = list[i];
                }
                return tempArray;
            }catch(NullReferenceException e)
            {
                Console.Error.Write("Array referneces null");
                e.StackTrace.ToString();
                return default(T[]); //syntax ?
            }
        }

        public T[] toArray(T[] array)
        {
            try
            {
                T[] tempArray = new T[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    tempArray[i] = list[i];
                }
                return tempArray;
            }
            catch (NullReferenceException e)
            {
                Console.Error.Write("Array Reference is null");
                e.StackTrace.ToString();
                return default(T[]); //syntax ?
            }
        }

        public void trimToSize()
        {
            T[] tempArray = new T[size];
            for(int i = 0; i < _size(); i++)
            {
                tempArray[i] = list[i];
            }
            list = tempArray;
        }

    }
}
