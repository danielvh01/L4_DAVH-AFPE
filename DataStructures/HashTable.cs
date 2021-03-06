using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataStructures
{
    public class HashTable<T, K> where T : IComparable where K : IComparable
    {
        #region Variables
        int Length;
        int maxKeys;
        HashNode<T, K> start;
        HashNode<T, K> end;
        #endregion

        #region Methods
        public HashTable(int L)
        {
            start = null;
            end = null;
            Length = 0;
            maxKeys = L;
        }

        public bool existsKey(K key)
        {
            if(Length > 0)
            {
                HashNode<T, K> temp = start;
                
                while (temp != null)
                {
                    if (temp.key.CompareTo(key) == 0)
                    {
                        return true;
                    }
                    else
                    {
                        temp = temp.next;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public void Add(T value, K key)
        {
            HashNode<T, K> newnode = new HashNode<T, K>();
            if (Length == 0)
            {
                newnode.key = key;
                newnode.value.InsertAtEnd(value);
                start = newnode;
                end = newnode; 
            }
            else {
                if(!existsKey(key))
                {
                    newnode.key = key;
                    newnode.value.InsertAtEnd(value);
                    end.next = newnode;
                    newnode.prev = end;
                    end = end.next;
                }
                else
                {
                    HashNode<T, K> temp = start;
                    while (temp.key.CompareTo(key) != 0)
                    {
                        temp = temp.next;
                    }
                    temp.value.InsertAtEnd(value);
                }
            }
            Length++;
        }

        public T Get(T value, K key)
        {
            if (existsKey(key))
            {
                HashNode<T, K> temp = start;
                while (temp.key.CompareTo(key) != 0)
                {
                    temp = temp.next;
                }
                return temp.value.Find(value);
            }
            else
            {
                return default;
            }
        }
        public T Get( Func<T,int> comparer, K key)
        {
            if (existsKey(key))
            {
                HashNode<T, K> temp = start;
                while (temp.key.CompareTo(key) != 0)
                {
                    temp = temp.next;
                }
                return temp.value.Find(comparer);
            }
            else
            {
                return default;
            }
        }



        public T Delete(T value, K key)
        {
            if (existsKey(key))
            {
                HashNode<T, K> temp = start;
                while (temp.key.CompareTo(key) != 0)
                {
                    temp = temp.next;
                }
                int idx = temp.value.GetPositionOf(value);
                T val = temp.value.Find(value);
                if (idx >= 0)
                {
                    temp.value.Delete(idx);
                }
                if(temp.value.Length == 0)
                {
                    DeleteKey(key);
                }
                Length--;
                return val;
            }
            else
            {
                return default;
            }
        }

        private void DeleteKey (K key)
        {
            if (Length > 0 && existsKey(key))
            {
                if (Length == 1)
                {
                    start = null;
                    end = null;
                }
                else
                {
                    if (start.key.CompareTo(key) == 0)
                    {
                        start = start.next;
                        start.prev = null;
                    }
                    else if (end.key.CompareTo(key) == 0)
                    {
                        end = end.prev;
                        end.next = null;
                    }
                    else
                    {
                        HashNode<T, K> pretemp = start;
                        HashNode<T, K> temp = start.next;
                        while (temp.key.CompareTo(key) != 0)
                        {
                            pretemp = temp;
                            temp = temp.next;
                        }
                        pretemp.next = temp.next;
                        if (temp.next != null)
                        {
                            temp.next.prev = pretemp;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
