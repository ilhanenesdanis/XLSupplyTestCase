using Manager.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Manager.Configurations
{
    public class MemberFilesConfiguration : IEntityTypeConfiguration<MemberFiles>
    {
        public void Configure(EntityTypeBuilder<MemberFiles> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Member).WithMany(x => x.MemberFiles).HasForeignKey(x => x.MemberId);
        }
    }
}
