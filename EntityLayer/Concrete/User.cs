using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User
    {
        [Key]
        public int UserId {get; set;}

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string UserPassword { get; set; }

        [StringLength(1)]
        public string UserRole { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
