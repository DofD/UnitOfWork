using System.Data;
using System.Data.Entity;
using DofD.UofW.DataAccess.Adapters.EF.Interface;
using DofD.UofW.DataAccess.Common.Interface;

namespace DofD.UofW.DataAccess.Adapters.EF.Impl
{
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
        ///     Фабрика контекста доступа к БД
        /// </summary>
        private IDbContextFactory _contextFactory;

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
            _contextFactory = contextFactory;

            _context = _contextFactory.CreateDbContext<EntitiesContext>();

            // Если БД не была создана вызовет ошибку
            _transaction = _context.Database.BeginTransaction(isolationLevel);
        }

        /// <summary>
        ///     Контекст БД
        /// </summary>
        public DbContext DbContext
        {
            get { return _context; }
        }

        /// <summary>
        ///     Зафиксировать проделанную работу
        /// </summary>
        public void Commit()
        {
            _context.SaveChanges();

            _transaction.Commit();
        }

        /// <summary>
        ///     Выполнить определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        ///     Откатить проделанную работу
        /// </summary>
        public void Rollback()
        {
            Dispose();
        }

        /// <summary>
        ///     Уничтожает экземпляр класса <see cref="UnitOfWorkEf" />
        /// </summary>
        ~UnitOfWorkEf()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Выполнить определяемые приложением задачи, связанные с высвобождением или сбросом неуправляемых ресурсов
        /// </summary>
        /// <param name="disposing">Освобождать</param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _transaction.Dispose();
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}