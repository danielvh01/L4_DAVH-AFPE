using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace L4_DAVH_AFPE.Models
{
    public class TaskModel : IComparable
    {
        #region Variables
        //xd
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string project { get; set; }

        [Required]
        public int priority { get; set; }

        [Required]
        public string date { get; set; }

        public string inCharge { get; set; }
        #endregion

        public int CompareTo(object obj)
        {
            var comparer = ((TaskModel)obj).title;
            return comparer.CompareTo(title);
        }

        public TaskModel()
        {
            title          = "";
            description    = "";
            project        = "";
            priority       =  0;
            date           = "";
            inCharge       = "";
        }
        public TaskModel(string t)
        {
            title = t;
            description = "";
            project = "";
            priority = 0;
            date = "";
            inCharge = "";
        }
    }
}
