using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
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
            Property(x => x.To).HasColumnName("To").IsOptional();
            Property(x => x.ToEmail).HasColumnName("ToEmail").IsOptional();
            Property(x => x.From).HasColumnName("From").IsOptional();
            Property(x => x.FromEmail).HasColumnName("FromEmail").IsOptional();
            Property(x => x.Cc).HasColumnName("Cc").IsOptional();
            Property(x => x.Bcc).HasColumnName("Bcc").IsOptional();
            Property(x => x.Body).HasColumnName("Body").IsOptional();
            Property(x => x.Encoding).HasColumnName("Encoding").IsOptional().HasMaxLength(128);
            Property(x => x.DisplayEncoding).HasColumnName("DisplayEncoding").IsOptional().HasMaxLength(128);
            Property(x => x.Relay).HasColumnName("Relay").IsOptional().HasMaxLength(256);
            Property(x => x.RelayEmail).HasColumnName("RelayEmail").IsOptional().HasMaxLength(256);
        }
    }
}