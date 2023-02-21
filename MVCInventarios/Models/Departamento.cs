using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MVCInventarios.Models
{
    public class Departamento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del departamento es requerido.")]
        [Display(Name = "Nombre")]
        [MinLength(4, ErrorMessage = "El nombre del departamento debe ser mayor o igual a 4 caracteres")]
        [MaxLength(100, ErrorMessage = "El nombre del departamento no debe ser mayor a 100 caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion")]
        [MinLength(4, ErrorMessage = "La descripción del departamento debe ser mayor o igual a 4 caracteres")]
        [MaxLength(200, ErrorMessage = "La descripción del departamento no debe ser mayor a 200 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de creación es requerida.")]
        //[BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime FechaCreacion { get; set; }

    }
}
