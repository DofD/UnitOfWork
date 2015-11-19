namespace DofD.UofW.DataAccess.Adapters.NH.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Common.Interface;

    using NHibernate;
    using NHibernate.Linq;

    /// <summary>
    ///     Репозиторий NHibernate
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора</typeparam>
    /// <typeparam name="TEntity">Тип репозитория</typeparam>
    public class RepositoryNh<TId, TEntity> : IRepository<TId, TEntity>
        where TEntity : class, IEntityIdentifier<TId>
    {
        /// <summary>
        ///     Фабрика единиц работы
        /// </summary>
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="RepositoryNh{TId, TEntity}" />
        /// </summary>
        /// <param name="unitOfWorkFactory">Фабрика единиц работы</param>
        public RepositoryNh(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this._unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        ///     Удалить объекты
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="ids">Идентификаторы</param>
        public void Delete(IUnitOfWork unitOfWork = null, params TId[] ids)
        {
            var unit = unitOfWork as UnitOfWorkNh;

            var innerUnitOfWork = unit ?? (UnitOfWorkNh)this._unitOfWorkFactory.Create();

            try
            {
                var entityIds = ids.ToList();
                var entities = this.GetAll(entity => entityIds.Contains(entity.Id), innerUnitOfWork).ToArray();

                this.Delete(innerUnitOfWork, entities);
            }
            finally
            {
                if (unit == null)
                {
                    innerUnitOfWork.Commit();
                }
            }
        }

        /// <summary>
        ///     Удалить объекты
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="entities">Удаляемые объекты</param>
        public void Delete(IUnitOfWork unitOfWork = null, params TEntity[] entities)
        {
            this.BaseAction((session, entity) => session.Delete(entity), unitOfWork, entities);
        }

        /// <summary>
        ///     Удалить объекты
        /// </summary>
        /// <param name="ids">Идентификаторы</param>
        public void Delete(params TId[] ids)
        {
            this.Delete(null, ids);
        }

        /// <summary>
        ///     Удалить объекты
        /// </summary>
        /// <param name="entities">Удаляемые объекты</param>
        public void Delete(params TEntity[] entities)
        {
            this.Delete(null, entities);
        }

        /// <summary>
        ///     Получить объект
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Объект из БД</returns>
        public TEntity Get(
            TId id,
            IUnitOfWork unitOfWork = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var predicateId = GetPredicateId(id);

            return this.BaseActionGet(query => query.SingleOrDefault(), predicateId, unitOfWork, includeProperties);
        }

        /// <summary>
        ///     Получить объект
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Объект из БД</returns>
        public TEntity Get(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Get(id, null, includeProperties);
        }

        /// <summary>
        ///     Получить выборку объектов
        /// </summary>
        /// <param name="where">Условие выбора</param>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Выборка объектов</returns>
        public IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> where = null,
            IUnitOfWork unitOfWork = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return this.BaseActionGet(query => query.ToList(), where, unitOfWork, includeProperties);
        }

        /// <summary>
        ///     Получить выборку объектов
        /// </summary>
        /// <param name="where">Условие выбора</param>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Выборка объектов</returns>
        public IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> @where,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return this.GetAll(where, null, includeProperties);
        }

        /// <summary>
        ///     Получить выборку объектов
        /// </summary>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Выборка объектов</returns>
        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return this.GetAll(null, null, includeProperties);
        }

        /// <summary>
        ///     Получить выборку объектов
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Выборка объектов</returns>
        public IEnumerable<TEntity> GetAll(
            IUnitOfWork unitOfWork,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return this.GetAll(null, unitOfWork, includeProperties);
        }

        /// <summary>
        ///     Сохранить объект
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="entities">Объекты для сохранения</param>
        public void Insert(IUnitOfWork unitOfWork = null, params TEntity[] entities)
        {
            this.BaseAction((session, entity) => session.Save(entity), unitOfWork, entities);
        }

        /// <summary>
        ///     Сохранить объект
        /// </summary>
        /// <param name="entities">Объекты для сохранения</param>
        public void Insert(params TEntity[] entities)
        {
            Insert(null, entities);
        }

        /// <summary>
        ///     Сохранить или обновить
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="entities">Объекты</param>
        public void InsertOrUpdate(IUnitOfWork unitOfWork = null, params TEntity[] entities)
        {
            this.BaseAction((session, entity) => session.SaveOrUpdate(entity), unitOfWork, entities);
        }

        /// <summary>
        ///     Сохранить или обновить
        /// </summary>
        /// <param name="entities">Объекты</param>
        public void InsertOrUpdate(params TEntity[] entities)
        {
            this.InsertOrUpdate(null, entities);
        }

        /// <summary>
        ///     Обновить объект
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="entities">Обновляемый объект</param>
        public void Update(IUnitOfWork unitOfWork = null, params TEntity[] entities)
        {
            this.BaseAction((session, entity) => session.Update(entity), unitOfWork, entities);
        }

        /// <summary>
        ///     Обновить объект
        /// </summary>
        /// <param name="entities">Обновляемый объект</param>
        public void Update(params TEntity[] entities)
        {
            this.Update(null, entities);
        }

        /// <summary>
        ///     Базовая функция
        /// </summary>
        /// <param name="action">Функция действия</param>
        /// <param name="unitOfWork">Единица работы</param>
        /// <param name="entities">Обрабатываемые объекты</param>
        protected virtual void BaseAction(
            Action<ISession, TEntity> action,
            IUnitOfWork unitOfWork = null,
            params TEntity[] entities)
        {
            if (entities == null)
            {
                return;
            }

            var unit = unitOfWork as UnitOfWorkNh;

            var unnerUnitOfWork = unit ?? (UnitOfWorkNh)this._unitOfWorkFactory.Create();

            foreach (var entity in entities)
            {
                action(unnerUnitOfWork.Session, entity);
            }

            // Комитим только если использовался внутренний UofW
            if (unit == null)
            {
                unnerUnitOfWork.Commit();
            }
        }

        /// <summary>
        ///     Базовый метод для получения сущностей
        /// </summary>
        /// <typeparam name="TResult">Тип результата</typeparam>
        /// <param name="action">Метод действия</param>
        /// <param name="where">Условие</param>
        /// <param name="unitOfWork">Единица работы</param>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Результат выполнения метода действия</returns>
        protected virtual TResult BaseActionGet<TResult>(
            Func<IQueryable<TEntity>, TResult> action,
            Expression<Func<TEntity, bool>> where,
            IUnitOfWork unitOfWork,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var unit = unitOfWork as UnitOfWorkNh;

            var innerUnitOfWork = unit ?? (UnitOfWorkNh)this._unitOfWorkFactory.Create();

            try
            {
                var query = AsQueryable(where, innerUnitOfWork);

                return action(query);
            }
            finally
            {
                if (unit == null)
                {
                    innerUnitOfWork.Rollback();
                }
            }
        }

        /// <summary>
        ///     В виде запроса
        /// </summary>
        /// <param name="where">Условие выбора</param>
        /// <param name="innerUnitOfWork">Единица работы</param>
        /// <returns>Выборка сущностей</returns>
        private static IQueryable<TEntity> AsQueryable(
            Expression<Func<TEntity, bool>> where,
            UnitOfWorkNh innerUnitOfWork)
        {
            return innerUnitOfWork.Session.Query<TEntity>().Where(where);
        }

        /// <summary>
        ///     Получить предикат выбора по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Предикат выбора</returns>
        private static Expression<Func<TEntity, bool>> GetPredicateId(TId id)
        {
            // Предикат: i => i.Id == id
            var arg = Expression.Parameter(typeof(TEntity), "i");
            var predicate =
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Equal(Expression.Property(arg, "Id"), Expression.Constant(id)),
                    arg);

            return predicate;
        }
    }
}