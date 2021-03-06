﻿namespace DofD.UofW.DataAccess.Adapters.EF.Impl
{
    using System.Data;
    using System.Data.Entity;

    using Common.Interface;

    using Interface;

    /// <summary>
    ///     Единица работы EF
    /// </summary>
    public class UnitOfWorkEf : IUnitOfWork
    {
        /// <summary>
        ///     Контекст доступа к данным
        /// </summary>
        private readonly EntitiesContext _context;

        /// <summary>
        ///     Транзакция
        /// </summary>
        private readonly DbContextTransaction _transaction;

        /// <summary>
        ///     Флаг очистки ресурсов
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="UnitOfWorkEf" />
        /// </summary>
        /// <param name="contextFactory">Фабрика контекста доступа к БД</param>
        /// <param name="isolationLevel">Уровень изоляции данных</param>
        public UnitOfWorkEf(
            IDbContextFactory contextFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            this._context = contextFactory.CreateDbContext<EntitiesContext>();

            // Если БД не была создана вызовет ошибку
            this._transaction = this._context.Database.BeginTransaction(isolationLevel);
        }

        /// <summary>
        ///     Уничтожает экземпляр класса <see cref="UnitOfWorkEf" />
        /// </summary>
        ~UnitOfWorkEf()
        {
            this.Dispose(false);
        }

        /// <summary>
        ///     Контекст БД
        /// </summary>
        public DbContext DbContext
        {
            get
            {
                return this._context;
            }
        }

        /// <summary>
        ///     Зафиксировать проделанную работу
        /// </summary>
        public void Commit()
        {
            this._context.SaveChanges();

            this._transaction.Commit();
        }

        /// <summary>
        ///     Выполнить определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        ///     Откатить проделанную работу
        /// </summary>
        public void Rollback()
        {
            this.Dispose();
        }

        /// <summary>
        ///     Выполнить определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов
        /// </summary>
        /// <param name="disposing">Освобождать</param>
        private void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                this._transaction.Dispose();
                this._context.Dispose();
            }

            this._disposed = true;
        }
    }
}