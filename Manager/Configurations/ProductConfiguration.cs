using Manager.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.MemberFiles).WithMany(x => x.Products).HasForeignKey(x => x.FileId);
        }
    }
}
