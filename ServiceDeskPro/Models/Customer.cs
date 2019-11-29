using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceDeskPro.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [StringLength(13)]
        public string Phone { get; set; }
        [StringLength(15)]
        public string Cedula { get; set; }
        public string Email { get; set; }
        public DateTime DateAdmission { get; set; }
    }
}