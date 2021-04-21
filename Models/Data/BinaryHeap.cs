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
        int capacity;


        #endregion

        #region Methods
        public BinaryHeap(int L)
        {
            capacity = L;
            Root = null;
            heapArray = new DoubleLinkedList<HeapNode<T>>();
        }

        public int Length()
        {
            return heapArray.Length;
        }

        public void Swap(int a, int b)
        {
            HeapNode<T> temp = heapArray.Get(a);
            heapArray.Delete(a);
            heapArray.Insert(heapArray.Get(b), a);
            heapArray.Delete(b);
            heapArray.Insert(temp, b);
        }

        public int Parent(int index)
        {
            return (index - 1) / 2;
        }

        public int Left(int index)
        {
            return 2 * index + 1;
        }

        public int Right(int index)
        {
            return 2 * index + 2;
        }

        public bool insertKey(T value, int p)
        {
            if(Length() == capacity)
            {
                return false;
            }
            int i = Length();
            heapArray.Insert(new HeapNode<T>(value, p), i);

            while (i != 0 && heapArray.Get(i).CompareTo(heapArray.Get(Parent(i))) < 0)
            {
                Swap(i, Parent(i));
                i = Parent(i);
            }
            return true;
        }

        public HeapNode<T> getMin()
        {
            return heapArray.Get(0);
        }

        public HeapNode<T> extractMin()
        {
            if (Length() <= 0)
            {
                return default;
            }
            else
            {
                HeapNode<T> result = heapArray.Get(0);
                heapArray.Delete(0);
                Swap(0, Length() - 1);
                Sort(0);
                return result;
            }
        }

        public void Sort(int position)
        {
            int lchild = Left(position);
            int rchild = Right(position);
            int largest = 0;
            if ((lchild < Length()) && (heapArray.Get(lchild).CompareTo(heapArray.Get(position)) < 0))
            {
                largest = lchild;
            }
            else
            {
                largest = position;
            }
            if ((rchild < Length()) && (heapArray.Get(rchild).CompareTo(heapArray.Get(largest)) < 0))
            {
                largest = rchild;
            }
            if (largest != position)
            {
                Swap(position, largest);
                Sort(largest);
            }
        }
        #endregion
    }
}
