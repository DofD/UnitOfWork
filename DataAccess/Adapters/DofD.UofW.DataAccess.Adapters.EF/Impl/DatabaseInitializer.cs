using System.Data.Entity;

namespace DofD.UofW.DataAccess.Adapters.EF.Impl
{
    /// <summary>
    ///     Инициализатор БД
    /// </summary>
    /// <typeparam name="TContext">Тип контекста доступа к БД</typeparam>
    public class DatabaseInitializer<TContext> : IDatabaseInitializer<TContext>
        where TContext : DbContext
    {
        /// <summary>
        ///     Выполняет стратегию для инициализации базы данных для данного контекста
        /// </summary>
        /// <param name="context">Контекст</param>
        public void InitializeDatabase(TContext context)
        {
            if (context.Database.Exists())
            {
                return;
            }

            context.Database.Create();
            this.Seed(context);
            context.SaveChanges();
        }

        /// <summary>
        ///     Заполнить таблицы
        /// </summary>
        /// <param name="context">Контекст</param>
        protected virtual void Seed(TContext context)
        {
            // TODO: Добавление инициализации таблиц
        }
    }
}
