using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace L4_DAVH_AFPE.Models.Data
{
    public sealed class Singleton
    {
        
        private readonly static Singleton _instance = new Singleton();
        public bool loginType;
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

        public string keyGen(string date)
        {
            return "";
        }
    }
}
