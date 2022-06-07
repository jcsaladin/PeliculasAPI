using PeliculasAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.ViewModels.GenresViewModels
{
    public class GenreEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} no debe tener mas de 50 caracteres.")]
        [CapitalCase]
        public string Name { get; set; }
    }
}