using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Kitap
    {
        [Key]
        public int KitapId { get; set; }

        [StringLength(1024)] 
        public string KitapName { get; set; }
        public int AlanId { get; set; }
        public virtual Alan Alan { get; set; }

        public int YazarId { get; set; }
        public virtual Yazar Yazar { get; set; }

        public bool KitapStatus { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [StringLength(1024)]
        public string KitapYolu { get; set; }
    }
}
