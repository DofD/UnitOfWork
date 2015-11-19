namespace DofD.UofW.Entities.Map.Ef
{
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    ///     Мапинг инструктора
    /// </summary>
    public class InstructorMap : EntityTypeConfiguration<Instructor>
    {
        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="InstructorMap" />.
        /// </summary>
        public InstructorMap()
        {
            this.ToTable("Instructors");

            this.HasKey(t => t.Id);

            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.HireDate).HasColumnName("HireDate");

            this.HasMany(t => t.Courses).WithMany(c => c.Instructors);
        }
    }
}