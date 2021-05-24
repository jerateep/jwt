using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace testrm.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
        public string name { get; set; }
        public string mail { get; set; }
    }
}
