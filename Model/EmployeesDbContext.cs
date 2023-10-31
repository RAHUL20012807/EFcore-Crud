using Microsoft.EntityFrameworkCore;

namespace crud.Model
{

    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext(DbContextOptions<EmployeesDbContext> opt) : base(opt)
        {

        }
        public DbSet<Employees> Employees { get; set; }
    }

}
