using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Alan
    {
        [Key]
        public int AlanId { get; set; }

        [StringLength(512)]
        //Required(Message="Bu alan boş geçilemez.")
        public string AlanName { get; set; }
        public ICollection<Kitap> Kitaps { get; set; }
    }
}
