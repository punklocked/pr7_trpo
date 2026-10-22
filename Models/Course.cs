using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr7_trpo_nab.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";

        public string Teacher { get; set; } = "";

        public int DurationHours { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }
    }
}
