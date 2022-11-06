using System;
using System.Collections.Generic;

#nullable disable

namespace GreenLeaves.Models
{
    public partial class Contacto
    {
        public int IdContact { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Fecha { get; set; }
        public string Ciudad { get; set; }
    }
}
