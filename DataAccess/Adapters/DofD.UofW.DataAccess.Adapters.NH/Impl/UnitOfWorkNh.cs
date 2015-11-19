namespace DofD.UofW.DataAccess.Adapters.NH.Impl
{
    using System.Data;

    using Common.Interface;

    using NHibernate;

    /// <summary>
    ///     Единица работы NHibernate
    /// </summary>
    public class UnitOfWorkNh : IUnitOfWork
    {
        private readonly ISession _session;

        /// <summary>
        ///     Транзакция
        /// </summary>
        private readonly ITransaction _transaction;

        /// <summary>
        ///     Флаг очистки ресурсов
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="UnitOfWorkNh" />
        /// </summary>
        /// <param name="sessionFactory">Фабрика сессий</param>
        /// <param name="isolationLevel">Уровень изоляции данных</param>
        public UnitOfWorkNh(
            ISessionFactory sessionFactory,
            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            this._session = sessionFactory.OpenSession();
            this._transaction = this._session.BeginTransaction(isolationLevel);
        }

        /// <summary>
        ///     Сессия
        /// </summary>
        public ISession Session { get; set; }

        /// <summary>
        ///     Зафиксировать проделанную работу
        /// </summary>
        public void Commit()
        {
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
                this._session.Dispose();
            }

            this._disposed = true;
        }
    }
}