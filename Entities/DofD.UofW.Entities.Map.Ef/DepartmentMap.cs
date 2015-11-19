namespace DofD.UofW.Entities.Map.Ef
{
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    ///     Мапинг департамента
    /// </summary>
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="DepartmentMap" />.
        /// </summary>
        public DepartmentMap()
        {
            this.ToTable("Departments");

            this.HasKey(t => t.Id);

            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Budget).HasColumnName("Budget");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
        }
    }
}