using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [StringLength(1024)]
        public string Content { get; set; }
        public int KitapId { get; set; }
        public virtual Kitap Kitap { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
