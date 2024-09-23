using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticationManager.Models
{
    public class AuthenticationResponse
    {
        public string NombreU { get; set; }
        public string JwtToken { get; set; }
        public string ExpiresIn { get; set; }
    }
}
