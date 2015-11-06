using System.Data.Entity;

namespace DofD.UofW.DataAccess.Adapters.EF.Interface
{
    /// <summary>
    ///     Фабрика контекста доступа к данным
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        ///     Создать контекст доступа к данным
        /// </summary>
        /// <typeparam name="TDbContext">Тип контекста</typeparam>
        /// <returns>Контекст доступа к данным</returns>
        TDbContext CreateDbContext<TDbContext>() where TDbContext : EntitiesContext;

        /// <summary>
        ///     Создать контекст доступа к данным
        /// </summary>
        /// <returns>Контекст доступа к данным</returns>
        DbContext CreateDbContext();
    }
}