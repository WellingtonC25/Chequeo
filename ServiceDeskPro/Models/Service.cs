using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ServiceDeskPro.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string ServiceType { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
    }
}