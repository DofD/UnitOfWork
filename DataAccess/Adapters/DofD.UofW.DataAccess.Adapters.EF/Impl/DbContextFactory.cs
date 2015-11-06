using System.Data.Entity;
using DofD.UofW.DataAccess.Adapters.EF.Interface;
using DofD.UofW.DataAccess.Common.Interface;

namespace DofD.UofW.DataAccess.Adapters.EF.Impl
{
    /// <summary>
    ///     Фабрика контекста EF
    /// </summary>
    public class DbContextFactory : IDbContextFactory
    {
        /// <summary>
        ///     Конфигурация контекста
        /// </summary>
        private readonly IContextConfig _contextConfig;

        /// <summary>
        ///     Инициализатор БД
        /// </summary>
        private readonly IDatabaseInitializer<EntitiesContext> _databaseInitializer;

        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="DbContextFactory" />
        /// </summary>
        /// <param name="databaseInitializer">Инициализатор БД</param>
        /// <param name="contextConfig">Конфигурация контекста</param>
        public DbContextFactory(
            IDatabaseInitializer<EntitiesContext> databaseInitializer,
            IContextConfig contextConfig)
        {
            this._contextConfig = contextConfig;
            this._databaseInitializer = databaseInitializer;
        }

        /// <summary>
        ///     Создать контекст доступа к данным
        /// </summary>
        /// <typeparam name="TDbContext">Тип контекста</typeparam>
        /// <returns>Контекст доступа к данным</returns>
        public TDbContext CreateDbContext<TDbContext>() where TDbContext : EntitiesContext
        {
            return (TDbContext) this.CreateDbContext();
        }

        /// <summary>
        ///     Создать контекст доступа к данным
        /// </summary>
        /// <returns>Контекст доступа к данным</returns>
        public DbContext CreateDbContext()
        {
            return new EntitiesContext(this._databaseInitializer, this._contextConfig);
        }
    }
}
