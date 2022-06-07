using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entities
{
    public class Cinema: IModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 75, ErrorMessage = "El campo {0} no debe tener mas de 75 caracteres.")]
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}