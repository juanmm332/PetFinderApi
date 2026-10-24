using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    //internal class ApplicationDbContext
    //{
    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    //    {
    //    }

    //    protected ApplicationDbContext()
    //    {
    //    }
    //}

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }
    }
}
