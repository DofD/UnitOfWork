namespace DofD.UofW.DataAccess.Adapters.NH.Interface
{
    using NHibernate;

    /// <summary>
    ///     Конфигуратор NHibernate
    /// </summary>
    public interface INHibernateConfigurer
    {
        /// <summary>
        ///     Метод создания фабрики сессий
        /// </summary>
        /// <returns>Фабрика сессий</returns>
        ISessionFactory CreateSessionFactory { get; }
    }
}