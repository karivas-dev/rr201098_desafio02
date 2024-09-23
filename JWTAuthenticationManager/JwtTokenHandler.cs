using JWTAuthenticationManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsuarioAPI.Models;

namespace JWTAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "superKey";
        private const int JWT_TOKEN_VALIDITY_MINS = 10;
        private readonly UsuarioContext _context;

        private readonly List<Usuario> usuarios;

        public JwtTokenHandler(UsuarioContext context)
        {
            _context = context;
            usuarios = GetUsuarios();
        }

        public AuthenticationResponse? GenerateJwtToken(AuthenticationResquest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.NombreDeUsuario) || string.IsNullOrWhiteSpace(authenticationRequest.Contrasena))
            {
                return null;
            }

            var usuario = usuarios.Where(x => x.NombreDeUsuario == authenticationRequest.NombreDeUsuario && x.Contrasena == authenticationRequest.Contrasena).FirstOrDefault();
            if (usuario == null)
            {
                return null;
            }

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.NombreDeUsuario),
            });

            var signingCredential = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredential
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                NombreU = usuario.NombreDeUsuario,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token,
            };
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}
