using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using DofD.UofW.DataAccess.Common.Helpers;
using DofD.UofW.DataAccess.Common.Interface;
using NLog;

namespace DofD.UofW.DataAccess.Adapters.EF
{
    /// <summary>
    ///     Контекст БД EF
    /// </summary>
    public class EntitiesContext : DbContext, IDbModelCacheKeyProvider
    {
        /// <summary>
        ///     Настройки контекста
        /// </summary>
        private readonly IContextConfig _contextConfig;

        /// <summary>
        ///     Логировщик
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="EntitiesContext" />
        /// </summary>
        /// <param name="databaseInitializer">Инициализатор</param>
        /// <param name="contextConfig">Настройки контекста</param>
        /// <param name="logger">Логировщик</param>
        protected internal EntitiesContext(
            IDatabaseInitializer<EntitiesContext> databaseInitializer,
            IContextConfig contextConfig, ILogger logger)
            : base(contextConfig.ConnectionString)
        {
            this._contextConfig = contextConfig;
            _logger = logger;

            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            
            if (this._contextConfig.LogSQL)
            {
                this.Database.Log = s => this._logger.Trace(s);
            }

            Database.SetInitializer(databaseInitializer);
        }

        /// <summary>
        ///     Ключ кеша для этого контекста
        /// </summary>
        public string CacheKey
        {
            get
            {
                return this._contextConfig.CacheKey;
            }
        }

        /// <summary>
        ///     Настраиваем модель
        /// </summary>
        /// <param name="modelBuilder">Построитель модели</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Динамически создаем модель
            if (!string.IsNullOrEmpty(this._contextConfig.DefaultSchema))
            {
                modelBuilder.HasDefaultSchema(this._contextConfig.DefaultSchema);
            }

            ModelCreatingByEntityTypeConfiguration(modelBuilder, this._logger, this._contextConfig.NamespaceMaps);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        ///     Проверить пространство имен
        /// </summary>
        /// <param name="ns">Пространство имен типа</param>
        /// <param name="namespaceMaps">Разрешенные пространства имен</param>
        /// <returns>true если пространство имен подходит</returns>
        private static bool CheckNamespace(string ns, params string[] namespaceMaps)
        {
            return !string.IsNullOrEmpty(ns)
                   && (namespaceMaps == null || namespaceMaps.Where(n => !string.IsNullOrEmpty(n)).Any(ns.StartsWith));
        }

        /// <summary>
        ///     Проверяет тип на наследование от EntityTypeConfiguration
        /// </summary>
        /// <param name="type">Тип</param>
        /// <returns>true если наследуется от EntityTypeConfiguration</returns>
        private static bool IsEntityTypeConfiguration(Type type)
        {
            return type.BaseType != null
                   && ((type.BaseType.IsGenericType
                        && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>))
                       || IsEntityTypeConfiguration(type.BaseType));
        }

        /// <summary>
        ///     Построить модель с помощью EntityTypeConfiguration
        /// </summary>
        /// <param name="modelBuilder">Построитель модели</param>
        /// <param name="logger">Логировщик</param>
        /// <param name="namespaceMap">Пространство имен для мапинга</param>
        private static void ModelCreatingByEntityTypeConfiguration(
            DbModelBuilder modelBuilder,
            ILogger logger,
            params string[] namespaceMap)
        {
            logger.Trace("Начинаем конфигурировать контекст БД");

            var typesToRegister =
                PathExtension.GetAssemblyCurrentDirectory()
                    .AsParallel()
                    .SelectMany(
                        a =>
                            a.GetTypes()
                                .Where(type => CheckNamespace(type.Namespace, namespaceMap))
                                .Where(type => !type.IsAbstract)
                                .Where(IsEntityTypeConfiguration))
                    .ToList();

            logger.Trace("Найдено: {0} мапинг объектов", typesToRegister.Count);

            foreach (var type in typesToRegister)
            {
                try
                {
                    dynamic configurationInstance = Activator.CreateInstance(type);
                    modelBuilder.Configurations.Add(configurationInstance);

                    logger.Trace("Тип {0} зарегистрирован", type.FullName);
                }
                catch (Exception exception)
                {
                    logger.Trace("Тип {0} ошибка", type.FullName);
                    logger.Error( exception);
                }
            }

            logger.Trace("Закончили конфигурировать контекст БД");
        }
    }
}
