using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektPin.Models
{
    public class Izleti
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Mjesto { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Vrijeme")]
        public DateTime Date { get; set; }

        
    }
}
