using Microsoft.EntityFrameworkCore;
using SkillNode_Backend.Models;

namespace SkillNode_Backend.Data
{
    public class SkillNodeDbContext : DbContext
    {
        public SkillNodeDbContext(DbContextOptions<SkillNodeDbContext> options) : base(options) { }

        // Define your database tables (DbSets)
        public DbSet<User> Users { get; set; } 
    }
}
