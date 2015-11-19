namespace DofD.UofW.Entities
{
    using System;
    using System.Collections.Generic;

    using DataAccess.Common.Impl;

    /// <summary>
    ///     Инструктор
    /// </summary>
    public class Instructor : BaseEntity<Guid>
    {
        /// <summary>
        ///     Курсы
        /// </summary>
        public virtual ICollection<Course> Courses { get; private set; }

        /// <summary>
        ///     Фамилия
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Дата найма
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        ///     Наименование
        /// </summary>
        public string LastName { get; set; }
    }
}