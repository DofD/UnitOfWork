namespace DofD.UofW.DataAccess.Adapters.NH.Impl
{
    using Interface;

    using NHibernate;
    using NHibernate.Cfg;

    /// <summary>
    ///     Конфигуратор NHibernate
    /// </summary>
    public class NHibernateConfigurer : INHibernateConfigurer
    {
        private readonly Configuration _config;

        public NHibernateConfigurer()
        {
            this._config = new Configuration();
        }

        /// <summary>
        ///     Метод создания фабрики сессий
        /// </summary>
        /// <returns>Фабрика сессий</returns>
        ISessionFactory INHibernateConfigurer.BuildSessionFactory()
        {
            return this._config.BuildSessionFactory();
        }
    }
}