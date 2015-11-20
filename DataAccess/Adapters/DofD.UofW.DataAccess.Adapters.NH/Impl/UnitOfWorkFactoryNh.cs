namespace DofD.UofW.DataAccess.Adapters.NH.Impl
{
    using System.Data;

    using Common.Interface;

    using NHibernate;

    /// <summary>
    ///     Фабрика единицы работы NH
    /// </summary>
    public class UnitOfWorkFactoryNh : IUnitOfWorkFactory
    {
        /// <summary>
        ///     Фабрика сессий
        /// </summary>
        private readonly ISessionFactory _sessionFactory;

        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="UnitOfWorkFactoryNh" />
        /// </summary>
        /// <param name="sessionFactory">Фабрика сессий</param>
        public UnitOfWorkFactoryNh(ISessionFactory sessionFactory)
        {
            this._sessionFactory = sessionFactory;
        }

        /// <summary>
        ///     Создает единицу работы, если не будет вызван метод <see cref="IUnitOfWork.Commit" />, то автоматически будет
        ///     выполнен
        ///     <see cref="IUnitOfWork.Rollback" />
        /// </summary>
        /// <param name="isolationLevel">Уровень изоляции</param>
        /// <returns>Единица работы</returns>
        public IUnitOfWork Create(IsolationLevel isolationLevel)
        {
            return new UnitOfWorkNh(this._sessionFactory, isolationLevel);
        }

        /// <summary>
        ///     Создает единицу работы, если не будет вызван метод <see cref="IUnitOfWork.Commit" />, то автоматически будет
        ///     выполнен
        ///     <see cref="IUnitOfWork.Rollback" />
        /// </summary>
        /// <returns>Единица работы</returns>
        public IUnitOfWork Create()
        {
            return this.Create(IsolationLevel.ReadCommitted);
        }
    }
}