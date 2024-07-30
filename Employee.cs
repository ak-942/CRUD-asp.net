using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        
        [DisplayName("Employee Name") ]
        public string Name { get; set; }
        public int Age { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
    }
}