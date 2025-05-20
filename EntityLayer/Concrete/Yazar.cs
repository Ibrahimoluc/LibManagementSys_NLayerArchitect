using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Yazar
    {
        [Key]
        public int YazarId { get; set; }
        
        [StringLength(512)]
        public string YazarName { get; set; }
        public DateTime YazarDTarih { get; set; }
        public ICollection<Kitap> Kitaps { get; set; }
    }
}
