using Manager.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberFiles> MemberFiles { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if(item.Entity is BaseEntity baseEntity){
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                baseEntity.CreateDate = DateTime.Now;
                                baseEntity.Status = true;
                            }
                            break;
                       
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
