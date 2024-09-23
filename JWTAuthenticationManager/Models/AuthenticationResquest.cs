using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticationManager.Models
{
    internal class AuthenticationResquest
    {
        public string NombreDeUsuario { get; set; }
        public string Contrasena { get; set; }
    }
}
