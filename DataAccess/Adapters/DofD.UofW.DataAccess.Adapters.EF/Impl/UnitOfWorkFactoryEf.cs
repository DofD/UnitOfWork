namespace DofD.UofW.DataAccess.Adapters.EF.Impl
{
    using System.Data;

    using Common.Interface;

    using Interface;

    /// <summary>
    ///     Фабрика единицы работы EF
    /// </summary>
    public class UnitOfWorkFactoryEf : IUnitOfWorkFactory
    {
        /// <summary>
        ///     Фабрика контекста БД
        /// </summary>
        private readonly IDbContextFactory _contextFactory;

        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="UnitOfWorkFactoryEf" />.
        /// </summary>
        /// <param name="contextFactory">Фабрика контекста доступа к БД</param>
        public UnitOfWorkFactoryEf(IDbContextFactory contextFactory)
        {
            this._contextFactory = contextFactory;
        }

        /// <summary>
        ///     Создать единицу работы
        /// </summary>
        /// <param name="isolationLevel">Уровень изоляции</param>
        /// <returns>Единица работы</returns>
        public IUnitOfWork Create(IsolationLevel isolationLevel)
        {
            return new UnitOfWorkEf(this._contextFactory, isolationLevel);
        }

        /// <summary>
        ///     Создать единицу работы
        /// </summary>
        /// <returns>Единица работы</returns>
        public IUnitOfWork Create()
        {
            return this.Create(IsolationLevel.ReadCommitted);
        }
    }
}