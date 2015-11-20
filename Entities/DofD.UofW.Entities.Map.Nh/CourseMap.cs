namespace DofD.UofW.Entities.Map.Nh
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    ///     Мапинг курса
    /// </summary>
    public class CourseMap : ClassMapping<Course>
    {
        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="CourseMap" />.
        /// </summary>
        public CourseMap()
        {
            this.Id(course => course.Id);

            this.Table("Courses");

            this.Property(p => p.Title, mapper => mapper.Column("Title"));
            this.Property(p => p.Credits, mapper => mapper.Column("Credits"));

            this.OneToOne(course => course.Department, mapper => mapper.Lazy(LazyRelation.Proxy));
            this.Set(course => course.Instructors, mapper => mapper.Lazy(CollectionLazy.Lazy));
        }
    }
}