namespace DofD.UofW.DataAccess.Adapters.EF.Test.Impl
{
    using Common.Interface;

    /// <summary>
    ///     Конфигурация тестового контекста
    /// </summary>
    public class TestContextConfig : IContextConfig
    {
        /// <summary>
        ///     Ключ контекста
        /// </summary>
        public string CacheKey
        {
            get
            {
                return "UofWTest";
            }
        }

        /// <summary>
        ///     Строка подключения
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return "TestUnitOfWork";
            }
        }

        /// <summary>
        ///     Схема по умолчанию
        /// </summary>
        public string DefaultSchema
        {
            get
            {
                return "Test";
            }
        }

        /// <summary>
        ///     Логировать SQL
        /// </summary>
        public bool LogSQL
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        ///     Пространство имен для мапинга
        /// </summary>
        public string[] NamespaceMaps
        {
            get
            {
                return new[] { "DofD.UofW.Entities" };
            }
        }
    }
}