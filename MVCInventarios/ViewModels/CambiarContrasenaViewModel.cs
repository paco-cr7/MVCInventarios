using System.ComponentModel.DataAnnotations;

namespace MVCInventarios.ViewModels
{
    public class CambiarContrasenaViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Cuenta del usuario")]
        public string Username { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        [MinLength(5, ErrorMessage = "La contraseña debe ser mayor o igual a 5 caracteres"),
           MaxLength(20, ErrorMessage = "La contraseña no debe exceder los 20 caracteres")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }
        [Required(ErrorMessage = "La confirmación de la contraseña es requerida")]
        [MinLength(5, ErrorMessage = "La confirmación de la contraseña debe ser mayor o igual a 5 caracteres"),
           MaxLength(20, ErrorMessage = "La confirmación de la contraseña no debe exceder los 20 caracteres")]
        [DataType(DataType.Password)]
        [Compare("Contrasena", ErrorMessage = "La contraseña y su confrimación no coinciden")]
        [Display(Name = "Confirmar contraseña")]
        public string ConfirmarContrasena { get; set; }
    }
}
