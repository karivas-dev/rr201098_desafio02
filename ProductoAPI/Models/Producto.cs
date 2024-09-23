using System.ComponentModel.DataAnnotations;

namespace ProductoAPI.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string Categoria { get; set; }

        [Required]
        public string Distribuidor { get; set; }
    }
}
