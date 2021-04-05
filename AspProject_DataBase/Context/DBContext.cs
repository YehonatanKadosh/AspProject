using AspProject_Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AspProject_DataBase.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
