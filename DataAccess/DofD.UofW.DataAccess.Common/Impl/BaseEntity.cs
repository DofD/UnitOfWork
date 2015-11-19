namespace DofD.UofW.DataAccess.Common.Impl
{
    using Interface;

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