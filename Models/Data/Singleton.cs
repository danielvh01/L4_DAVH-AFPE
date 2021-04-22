using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace L4_DAVH_AFPE.Models.Data
{
    public sealed class Singleton
    {
        //DATA STORAGE
        public string database;
        //
        private readonly static Singleton _instance = new Singleton();
        public bool loginType;
        public string user;
        public int heapCapacity;
        public int hashCapacity;
        public HashTable<TaskModel,int> Tasks;
        public BinaryHeap<string>PriorityTask;
        private Singleton()
        {
            loginType = false;
        }
        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }

        public int keyGen(string date)
        {
            return 1;
        }

        public string Save(string data)
        {
            database += data + '\n';
            return database;
        }
    }
}
