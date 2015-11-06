using System.Data.Entity.ModelConfiguration;

namespace DofD.UofW.Entities.Map.Ef
{

    /// <summary>
    ///     Мапинг курса
    /// </summary>
    public class CourseMap : EntityTypeConfiguration<Course>
    {
        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="CourseMap" />.
        /// </summary>
        public CourseMap()
        {
            this.ToTable("Courses");

            this.Property(p => p.Title).HasColumnName("Title");
            this.Property(p => p.Credits).HasColumnName("Credits");

            this.HasKey(p => p.Id);

            this.HasRequired(c => c.Department).WithMany(t => t.Courses).HasForeignKey(p => p.DepartmentId);
        }
    }
}