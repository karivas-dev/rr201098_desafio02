using System.ComponentModel.DataAnnotations;

namespace UsuarioAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        
        [Required]
        public string NombreDeUsuario { get; set; }
        
        [Required]
        public string Contrasena { get; set; }
    }
}
