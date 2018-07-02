using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsaPresidents.Models
{
    public class President
    {
        public string PresidentName { get; set; }
        public DateTime? BirdthDate { get; set; }
        public DateTime? DeathDate { get; set; }
    }
}