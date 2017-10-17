using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Globons_Test.Models
{
    public class PersonaViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Apellido")]
        public string Apellido { get; set; }

        [DisplayName("Numero de Documento")]
        [Required(ErrorMessage = "Debe ingresar un numero.")]
        public int NumeroDocumento { get; set; }

        [DisplayName("Fecha de Nacimiento")]
        [Required(ErrorMessage = "Debe ingresar una fecha.")]
        [DisplayFormat(DataFormatString = "{dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        public string FechaNacimientoFormateada { get; set; }

        [DisplayName("Dirección")]
        [Required(ErrorMessage = "Debe seleccionar una direccion.")]
        public int idDireccion { get; set; }

        public Direccion Direccion { get; set; }

        [DisplayName("Dirección")]
        public string DireccionCompleta { get ; set; }
    }
}