using System.Data.Entity;

namespace WpfApp2
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }

    }
}
