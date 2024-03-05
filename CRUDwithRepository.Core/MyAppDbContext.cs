using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDwithRepository.Core
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public static string configSql = /*"Data Source=MSI;Initial Catalog=CRUDwithRepository;Integrated Security=True;Trust Server Certificate=True"*/
            "Server=DESKTOP-T8COVLQ\\SQLEXPRESS;Database=CRUDwithRepository;Integrated Security=True;Trust Server Certificate=True";
        // sửa chỗ này thành cái database connection string của anh nha
        // từ giờ kệ cái file appsettings.json đi nghe, từ giờ mình sẽ config ở đây
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(configSql);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
