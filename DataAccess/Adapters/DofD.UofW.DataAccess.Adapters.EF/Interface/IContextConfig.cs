namespace DofD.UofW.DataAccess.Common.Interface
{
    /// <summary>
    ///     Интерфейс настроек контекста EF
    /// </summary>
    public interface IContextConfig
    {
        /// <summary>
        ///     Ключ контекста
        /// </summary>
        string CacheKey { get; }

        /// <summary>
        ///     Строка подключения
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        ///     Схема по умолчанию
        /// </summary>
        string DefaultSchema { get; }

        /// <summary>
        ///     Логировать SQL
        /// </summary>
        bool LogSQL { get; }

        /// <summary>
        ///     Пространство имен для мапинга
        /// </summary>
        string[] NamespaceMaps { get; }
    }
}