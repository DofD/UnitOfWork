namespace DofD.UofW.DataAccess.Adapters.EF.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    ///     Расширение выборки
    /// </summary>
    public static class QueryableExtension
    {
        /// <summary>
        ///     Включить свойства
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="query">Выборка</param>
        /// <param name="includeProperties">Включаемые свойства</param>
        /// <returns>Выборка с включенными свойствами</returns>
        public static IQueryable<TEntity> PerformInclusions<TEntity>(
            this IQueryable<TEntity> query,
            IEnumerable<Expression<Func<TEntity, object>>> includeProperties) where TEntity : class
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}