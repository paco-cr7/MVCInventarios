using Microsoft.AspNetCore.Mvc.Rendering;
using MVCInventarios.Models;

namespace MVCInventarios.ViewModels
{
    public class AgregarEditarProductoViewModel
    {
        public SelectList ListadoMarcas { get; set; }
        public Producto Producto { get; set; } = new Producto();
    }
}
