using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        //Database tablolari
        public DbSet<Alan> Alans { get; set; }
        public DbSet<Kitap> Kitaps { get; set; }
        public DbSet<Yazar> Yazars { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        //public DbSet<Class1> Class1s { get; set; }
    }
}
