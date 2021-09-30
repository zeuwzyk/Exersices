using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace you.Models
{
    public class EmployeeData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Car { get; set; }
    }
}
