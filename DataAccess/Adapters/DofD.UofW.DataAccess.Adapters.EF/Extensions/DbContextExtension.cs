namespace DofD.UofW.DataAccess.Adapters.EF.Extensions
{
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;

    /// <summary>
    ///     Расширение контекста EF
    /// </summary>
    public static class DbContextExtension
    {
        /// <summary>
        ///     Установить состояние сущности
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="context">Контекст EF</param>
        /// <param name="entity">Сущность</param>
        /// <param name="state">Состояние</param>
        public static void ChangeObjectState<TEntity>(this DbContext context, TEntity entity, EntityState state)
            where TEntity : class
        {
            context.GetObjectContext().ObjectStateManager.ChangeObjectState(entity, state);
        }

        /// <summary>
        ///     Получить объект контекста
        /// </summary>
        /// <param name="context">Контекст EF</param>
        /// <returns>Объект контекста</returns>
        public static ObjectContext GetObjectContext(this DbContext context)
        {
            return ((IObjectContextAdapter)context).ObjectContext;
        }

        /// <summary>
        ///     Сохранить или обновить
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="context">Объект контекста</param>
        /// <param name="entity">Сущность</param>
        public static void SaveOrUpdate<TEntity>(this ObjectContext context, TEntity entity) where TEntity : class
        {
            ObjectStateEntry stateEntry = null;
            context.ObjectStateManager.TryGetObjectStateEntry(entity, out stateEntry);

            var objectSet = context.CreateObjectSet<TEntity>();
            if (stateEntry == null || stateEntry.EntityKey.IsTemporary)
            {
                objectSet.AddObject(entity);
            }
            else
            {
                if (stateEntry.State == EntityState.Detached)
                {
                    objectSet.Attach(entity);
                    context.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                }
            }
        }

        /// <summary>
        ///     Сохранить или обновить
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="context">Контекст EF</param>
        /// <param name="entity">Сущность</param>
        public static void SaveOrUpdate<TEntity>(this DbContext context, TEntity entity) where TEntity : class
        {
            context.GetObjectContext().SaveOrUpdate(entity);
        }
    }
}