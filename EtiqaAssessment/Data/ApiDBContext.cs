using EtiqaAssessment.Models;
using Microsoft.EntityFrameworkCore;

namespace EtiqaAssessment.Data
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<User> Users { get; set; }
    }
}
