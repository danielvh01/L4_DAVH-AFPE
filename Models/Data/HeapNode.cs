using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L4_DAVH_AFPE.Models.Data
{
    public class HeapNode<T> : AVLTreeNode<T> where T : IComparable
    {
        int priority;

        public HeapNode(T newvalue, int p) : base(newvalue)
        {
            priority = p;
        }

    }
}
