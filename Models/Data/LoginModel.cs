using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace L4_DAVH_AFPE.Models.Data
{
    public class LoginModel
    {
        [Required]
        public string user { get; set; }
        [Required]
        public bool admin { get; set; }
    }
}
