﻿namespace DofD.UofW.DataAccess.Common.Interface
{
    using System;

    /// <summary>
    ///     Интерфейс единицы работы
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Зафиксировать проделанную работу
        /// </summary>
        void Commit();

        /// <summary>
        ///     Откатить проделанную работу
        /// </summary>
        void Rollback();
    }
}