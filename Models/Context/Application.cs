using newproject.Models.Context;
using  newproject.Models.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace newproject.Models.Context
{
    public class Application : DbContext
    {
        public Application(DbContextOptions<Application> options) : base(options)
        {
        }
        //تنظیمات معرفی جدول
          public DbSet<Tbl_Header> tbl_Headers { get; set; }
          public DbSet<Tbl_Company> tbl_Companies { get; set; }
          public DbSet<Tbl_AboutUS> tbl_AboutUS { get; set; }
          public DbSet<Tbl_AboutUSItem> tbl_AboutUSItems { get; set; }
    }

    public class ToDoContextFactory : IDesignTimeDbContextFactory<Application>
    {
        public Application CreateDbContext(string[] args)
        {
            //نام جدول
            var builder = new DbContextOptionsBuilder<Application>();
            builder.UseSqlServer("Data Source=.;initial Catalog=newproject;integrated Security=SSPI;MultipleActiveResultSets=true");
            return new Application(builder.Options);
        }
    }
}