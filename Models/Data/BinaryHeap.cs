using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L4_DAVH_AFPE.Models.Data
{
    public class BinaryHeap<T> where T : IComparable
    {
        #region Variables
        public HeapNode<T> Root { get; set; }
        public DoubleLinkedList<HeapNode<T>> heapArray;
        public int capacity { get; set; }

        public int lenght = 0;
        public BinaryHeap()
        {
            lenght = 0; 
            Root = null;
            heapArray = new DoubleLinkedList<HeapNode<T>>();
        }
        #endregion

        #region Methods
        public void Swap(int a, int b)
        {
            HeapNode<T> temp = heapArray.Get(a);
            heapArray.Delete(a);
            heapArray.Insert(heapArray.Get(b), a);
            heapArray.Delete(b);
            heapArray.Insert(temp, b);
        }

        public int Parent(int key)
        {
            return (key - 1) / 2;
        }

        public int Left(int key)
        {
            return 2 * key + 1;
        }

        public int Right(int key)
        {
            return 2 * key + 2;
        }

        public bool insertKey(T value, int p)
        {
            int i = lenght;
            heapArray.Insert(new HeapNode<T>(value, p), i);
            lenght++;

            // Fix the min heap property if it is violated 
            while (i != 0 && heapArray.Get(i).CompareTo(heapArray.Get(Parent(i))) < 0)
            {
                Swap(i, Parent(i));
                i = Parent(i);
            }
            return true;
        }

        public void decreaseKey(int key, HeapNode<T> newval)
        {
            heapArray.Insert(newval, key);

            while (key != 0 && heapArray.Get(key).CompareTo(heapArray.Get(Parent(key))) < 0)
            {
                Swap(key, Parent(key));
                key = Parent(key);
            }
        }

        public HeapNode<T> getMin()
        {
            return heapArray.Get(0);
        }

        public HeapNode<T> extractMin()
        {
            if (lenght <= 0)
            {
                return default;
            }

            if (lenght == 1)
            {
                lenght--;
                return heapArray.Get(0);
            }

            
            Root = heapArray.Get(0);

            heapArray[0] = heapArray[current_heap_size - 1];
            current_heap_size--;
            MinHeapify(0);

            return root;
        }
        #endregion
    }
}
