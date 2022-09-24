using Manager.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace Manager
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VORNP2E;Initial Catalog=XlSupplyTestCase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            
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
                if (item.Entity is BaseEntity baseEntity)
                {
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
