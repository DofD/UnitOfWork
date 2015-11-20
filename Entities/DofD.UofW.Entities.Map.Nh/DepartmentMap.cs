namespace DofD.UofW.Entities.Map.Nh
{
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    ///     Мапинг департамента
    /// </summary>
    public class DepartmentMap : ClassMapping<Department>
    {
        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="DepartmentMap" />.
        /// </summary>
        public DepartmentMap()
        {
            this.Table("Departments");

            this.Id(t => t.Id);

            this.Property(t => t.Name, mapper => mapper.Column("Name"));
            this.Property(t => t.Budget, mapper => mapper.Column("Budget"));
            this.Property(t => t.StartDate, mapper => mapper.Column("StartDate"));
        }
    }
}