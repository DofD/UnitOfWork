namespace DofD.UofW.Entities
{
    using System;
    using System.Collections.Generic;

    using DataAccess.Common.Impl;

    /// <summary>
    ///     Департамент
    /// </summary>
    public class Department : BaseEntity<Guid>
    {
        /// <summary>
        ///     Бюджет
        /// </summary>
        public decimal Budget { get; set; }

        /// <summary>
        ///     Курсы
        /// </summary>
        public virtual ICollection<Course> Courses { get; set; }

        /// <summary>
        ///     Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Дата начала
        /// </summary>
        public DateTime StartDate { get; set; }
    }
}