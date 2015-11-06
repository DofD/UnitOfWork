using System;
using System.Collections.Generic;
using DofD.UofW.DataAccess.Common.Impl;

namespace DofD.UofW.Entities
{
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
