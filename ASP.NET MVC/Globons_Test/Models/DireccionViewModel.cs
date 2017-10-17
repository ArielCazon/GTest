using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Globons_Test.Models
{
    public class DireccionViewModel
    {
        [DisplayName("Id")]
        public int IdDireccion { get; set; }

        [DisplayName("Calle")]
        [Required(ErrorMessage = "Debe ingresar una calle.")]
        public string Calle { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage = "Debe ingresar un numero.")]
        public int Numero { get; set; }

        public string DireccionCompleta { get { return this.Calle + " " + this.Numero.ToString(); } }
    }
}