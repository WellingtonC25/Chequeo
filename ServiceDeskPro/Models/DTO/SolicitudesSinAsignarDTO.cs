using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceDeskPro.Models.DTO
{
    public class SolicitudesSinAsignarDTO
    {
        public List<ApplicationUser> Usuarios { get; set; }
        public Request Solicitudes { get; set; }
        public string UsuarioId { get; set; }
    }
}