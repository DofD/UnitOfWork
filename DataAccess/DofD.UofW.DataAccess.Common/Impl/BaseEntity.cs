using DofD.UofW.DataAccess.Common.Interface;

namespace DofD.UofW.DataAccess.Common.Impl
{
    /// <summary>
    ///     Базовый класс для всех сущностей
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора</typeparam>
    public abstract class BaseEntity<TId> : IEntityIdentifier<TId>
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public TId Id { get; set; }
    }
}