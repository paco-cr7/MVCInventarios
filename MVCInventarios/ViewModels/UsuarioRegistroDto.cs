﻿using Microsoft.AspNetCore.Components.Forms;
using MVCInventarios.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCInventarios.ViewModels
{
    public class UsuarioRegistroDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del usuario es obligatorio")]
        [MinLength(5, ErrorMessage = "El nombre del perfil debe ser mayor o igual a 5 caracteres"),
           MaxLength(25, ErrorMessage = "El nombre del perfil no debe exceder los 25 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Los apelldios del usuario son obligatorios")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "El nombre de la cuenta del usuario es obligatorio")]
        [MinLength(5, ErrorMessage = "El nombre de la ceunta del usuario debe ser mayor o igual a 5 caracteres"),
           MaxLength(20, ErrorMessage = "El nombre de la ceunta del usuario no debe exceder los 20 caracteres")]
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
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [DataType(DataType.EmailAddress)]
        public string CorreoElectronico { get; set; }
        public string Celular { get; set; }
        [Required(ErrorMessage = "El perfil del usuario es obligatorio")]
        [Display(Name = "Perfil")]
        public int PerfilId { get; set; }
        [Display(Name = "Foto")]
        public byte[] Foto { get; set; }
    }
}
