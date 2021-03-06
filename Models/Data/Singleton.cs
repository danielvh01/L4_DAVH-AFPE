using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using DataStructures;

namespace L4_DAVH_AFPE.Models.Data
{
    public sealed class Singleton
    {
        //DATA STORAGE//
        public string database;
        //
        public TaskModel edit;
        private readonly static Singleton _instance = new Singleton();
        public bool loginType;
        public string user;
        public int heapCapacity;
        public int hashCapacity;
        public HashTable<TaskModel,int> Tasks;
        public Heap<string>PriorityTask;
        public string txt;
        private Singleton()        
        {            
            heapCapacity = 15;
            hashCapacity = 15;
        }
        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }

       
        private string[] Abecedario = { "0","1","2","3","4","5","6","7","8","9","A", "B", 
                                        "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", 
                                        "M","N", "Ñ", "O", "P", "Q", "R", "S", "T", "U", 
                                        "V", "W", "X", "Y", "Z" };

        public int keyGen(string Title)
        {
            string l = Title.Substring(0,1).ToUpper();
            int key = -1;
            for (int i = 0; i < Abecedario.Length;i++)
            {
                if (l == Abecedario[i])
                {
                    key = i;
                    break;
                }
            }            
            return key % hashCapacity;
        }

        public string Save(string data)
        {
            database += data + '\n';
            return database;
        }

        public void BuildData()
        {
            database = "";
            database += "heapCapacity:" + heapCapacity + "\n";
            database += "hashCapacity:" + hashCapacity + "\n";
            string tasks = recorrido();
            if(tasks.Length > 0)
            {
                tasks = tasks.Remove(tasks.Length - 1);
            }
            database += "tasks:" + tasks;
        }

        public string recorrido()
        {
            //Write all the content of the HashTable.
            string result = "";
            for (int i = 0; i < PriorityTask.Length(); i++)
            {
                string taskname = PriorityTask.heapArray.Get(i).value;
                result += taskname + ",";
                var Task = Tasks.Get(x => x.title.CompareTo(taskname), keyGen(taskname));
                result += Task.description + ",";
                result += Task.project + ",";
                result += Task.priority + ",";
                result += Task.date + ",";
                result += Task.inCharge + ";";
            }
            return result;
        }
    }
}
