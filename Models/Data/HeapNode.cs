using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L4_DAVH_AFPE.Models.Data
{
    public class HeapNode<T> : IComparable where T : IComparable
    {
        public int priority;
        public T value { get; set; }

        public HeapNode<T> parent { get; set; }
        public HeapNode<T> left { get; set; }
        public HeapNode<T> right { get; set; }

        public int height;

        public HeapNode(T newvalue, int p)
        {
            value = newvalue;
            left = null;
            right = null;
            parent = null;
            height = 1;
            priority = p;
        }

        public int CompareTo(Object obj)
        {
            var comparer = ((HeapNode<T>)obj).priority;
            return comparer.CompareTo(priority);
        }

    }
}
