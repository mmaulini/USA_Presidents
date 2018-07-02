using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsaPresidents.DAL.Models
{
    public class President
    {
        public string PresidentName { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string DeathDate { get; set; }
        public string DeathPlace { get; set; }
    }
}