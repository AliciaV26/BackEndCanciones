using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCanciones.Entidades
{
    public class Cancion
    {

        public int Id { get; set; }


        [Required] //(Error Message = "El campo {0} es requerido)]
        [StringLength(maximumLength: 5, ErrorMessage = "El campo {0} solo puede terner 5 caracteres")]
        public string Nombre { get; set; } //Las etiquetas van antes del atributo

        [Range(1,20, ErrorMessage = "El campo no se encuentra dentro del rango")]
        [NotMapped] //Para atributos que no estén en la base de datos
        public string Duracion { get; set; }

        [CreditCard]
        [NotMapped]
        public string Tarjeta { get; set;} //También puede ser decimal

        [Url]
        [NotMapped]
        public string Url { get; set; }

        public List<Album> albumes { get; set; }
    }

}
