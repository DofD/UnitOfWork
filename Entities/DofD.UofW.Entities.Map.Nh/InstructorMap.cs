namespace DofD.UofW.Entities.Map.Nh
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    ///     Мапинг инструктора
    /// </summary>
    public class InstructorMap : ClassMapping<Instructor>
    {
        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="InstructorMap" />.
        /// </summary>
        public InstructorMap()
        {
            this.Table("Instructors");

            this.Id(t => t.Id);

            this.Property(t => t.LastName, mapper => mapper.Column("LastName"));
            this.Property(t => t.FirstName, mapper => mapper.Column("FirstName"));
            this.Property(t => t.HireDate, mapper => mapper.Column("HireDate"));

            this.Set(t => t.Courses, mapper => mapper.Lazy(CollectionLazy.Lazy), relation => relation.ManyToMany());
        }
    }
}
