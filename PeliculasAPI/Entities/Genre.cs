using PeliculasAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities
{
    public class Genre: IModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50,ErrorMessage = "El campo {0} no debe tener mas de 50 caracteres.")]
        [CapitalCase]
        public string Name { get; set; }
    }
}