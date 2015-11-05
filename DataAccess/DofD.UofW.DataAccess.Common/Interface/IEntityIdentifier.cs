namespace DofD.UofW.DataAccess.Common.Interface
{
    /// <summary>
    ///     Сущность с идентификатором
    /// </summary>
    /// <typeparam name="T">Тип идентификатора</typeparam>
    public interface IEntityIdentifier<out T>
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        T Id { get; }
    }
}