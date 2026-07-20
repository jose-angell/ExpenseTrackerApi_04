using ExpenseTrackerApi_04.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTrackerApi_04.Infrastructure
{
    public class ExpenseConfiguration: IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("expenses");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(2000);
            builder.Property(e => e.Amount).HasColumnType("numeric(18,2").IsRequired();
            builder.Property(e => e.ExpenseDate).IsRequired();
            builder.Property(e => e.CategoryId).IsRequired();
            builder.HasOne(e => e.Category)
                    .WithMany(e => e.Expenses)
                    .HasForeignKey(e => e.CategoryId);
        }
    }
}
