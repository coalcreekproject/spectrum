using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class MessageConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Message");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Subject).HasColumnName("Subject").IsOptional().HasMaxLength(256);
            Property(x => x.Urgent).HasColumnName("Urgent").IsOptional();
            Property(x => x.To).HasColumnName("To").IsOptional().HasMaxLength(128);
            Property(x => x.From).HasColumnName("From").IsOptional().HasMaxLength(256);
            Property(x => x.Body).HasColumnName("Body").IsOptional();
            Property(x => x.EncodingType).HasColumnName("EncodingType").IsOptional().HasMaxLength(128);
            Property(x => x.EmailRelay).HasColumnName("EmailRelay").IsOptional().HasMaxLength(256);
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();
        }
    }
}