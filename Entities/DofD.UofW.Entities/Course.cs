namespace DofD.UofW.Entities
{
    using System;
    using System.Collections.Generic;

    using DataAccess.Common.Impl;

    /// <summary>
    ///     Курс
    /// </summary>
    public class Course : BaseEntity<Guid>
    {
        /// <summary>
        ///     Кредиты
        /// </summary>
        public int Credits { get; set; }

        /// <summary>
        ///     Отдел
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        ///     Идентификатор отдела
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        ///     Инструкторы
        /// </summary>
        public virtual ICollection<Instructor> Instructors { get; set; }

        /// <summary>
        ///     Название
        /// </summary>
        public string Title { get; set; }
    }
}