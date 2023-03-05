using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace MVCInventarios.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        [Display(Name = "Nombre")]
        [MinLength(5, ErrorMessage = "El nombre del producto debe ser mayor o igual a 5 caracteres")]
        [MaxLength(50, ErrorMessage = "El nombre del producto no debe ser mayor a 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Descripción")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "La descripcion del producto debe cotener entre 5 y 200 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "La marca del producto es obligatoria.")]
        public int MarcaId { get; set; }
        public virtual Marca Marca { get; set; }

        [Required(ErrorMessage = "El costo es obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Costo { get; set; }

        [Display(Name = "Estatus")]
        [Required(ErrorMessage = "El estatus del producto es obligatorio.")]
        public EstatusProducto Estatus { get; set; } = EstatusProducto.Activo;

        public string Imagen { get; set; }
    }
}