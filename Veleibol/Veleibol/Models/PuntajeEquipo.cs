using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Veleibol.Models
{
    [Table("puntuacion")]
    public class Puntuacion
    {
        [Key]
        public int equipoId { set; get; }
        public int puntaje { set; get; }
    }
}
