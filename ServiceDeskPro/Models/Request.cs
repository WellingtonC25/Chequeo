using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceDeskPro.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }

        [Display(Name ="Fecha Creacion")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string AdditionalComment { get; set; }
        public bool Resolved { get; set; }
        public string CommentResolve { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
    }
}