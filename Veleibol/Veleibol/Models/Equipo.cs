using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Veleibol.Models
{
    [Table("equipo")]
    public class Equipo
    {
       [Key]
       public  int equipoId { set; get; }
       public  string nombre { set; get; }
    }
}
