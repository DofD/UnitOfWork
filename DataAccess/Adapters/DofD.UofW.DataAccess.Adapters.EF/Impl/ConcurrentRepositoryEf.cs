using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DofD.UofW.DataAccess.Adapters.EF.Extensions;
using DofD.UofW.DataAccess.Common.Interface;

namespace DofD.UofW.DataAccess.Adapters.EF.Impl
{
    /// <summary>
    ///     Потокобезопасный репозиторий EF
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора</typeparam>
    /// <typeparam name="TEntity">Тип репозитория</typeparam>
    public class ConcurrentRepositoryEf<TId, TEntity> : IRepository<TId, TEntity>
        where TEntity : class, IEntityIdentifier<TId>
    {
        /// <summary>
        ///     Фабрика единиц работы
        /// </summary>
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        /// <summary>
        ///     Инициализирует новый экземпляр класса <see cref="ConcurrentRepositoryEf{TId, TEntity}" />
        /// </summary>
        /// <param name="unitOfWorkFactory">Фабрика единиц работы</param>
        public ConcurrentRepositoryEf(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
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
            var unit = unitOfWork as UnitOfWorkEf;

            UnitOfWorkEf innerUnitOfWork = unit ?? (UnitOfWorkEf)_unitOfWorkFactory.Create();

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
            this.BaseAction((dbSet, entity) => dbSet.Remove(entity), unitOfWork, entities);
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
        ///     Удалить объект
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
        public TEntity Get(TId id, IUnitOfWork unitOfWork = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var predicateId = GetPredicateId(id);

            return BaseActionGet(
                query => query.SingleOrDefault(),
                predicateId,
                unitOfWork,
                includeProperties);
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
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where = null, IUnitOfWork unitOfWork = null,
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
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where,
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
        public IEnumerable<TEntity> GetAll(IUnitOfWork unitOfWork,
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
            BaseAction((dbSet, entity) => dbSet.Add(entity), unitOfWork, entities);
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
        public void SaveOrUpdate(IUnitOfWork unitOfWork = null, params TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Сохранить или обновить
        /// </summary>
        /// <param name="entities">Объекты</param>
        public void SaveOrUpdate(params TEntity[] entities)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Обновить объект
        /// </summary>
        /// <param name="entities">Обновляемый объект</param>
        public void Update(params TEntity[] entities)
        {
            throw new NotImplementedException();
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
            var unit = unitOfWork as UnitOfWorkEf;

            UnitOfWorkEf innerUnitOfWork = unit ?? (UnitOfWorkEf)_unitOfWorkFactory.Create();

            try
            {
                IQueryable<TEntity> query = AsQueryable(where, innerUnitOfWork, includeProperties);

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
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Выборка сущностей</returns>
        private static IQueryable<TEntity> AsQueryable(
            Expression<Func<TEntity, bool>> where,
            UnitOfWorkEf innerUnitOfWork,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            where = where ?? (entity => true);

            return innerUnitOfWork.DbContext.Set<TEntity>().Where(where).PerformInclusions(includeProperties);
        }

        /// <summary>
        ///     Получить предикат выбора по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Предикат выбора</returns>
        private static Expression<Func<TEntity, bool>> GetPredicateId(TId id)
        {
            // Предикат: i => i.Id == id, строим вручную чтобы избежать ошибки компиляции "Operator '==' cannot be applied to operands of type 'TId' and 'TId'"
            ParameterExpression arg = Expression.Parameter(typeof(TEntity), "i");
            Expression<Func<TEntity, bool>> predicate =
                Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Equal(Expression.Property(arg, "Id"), Expression.Constant(id)),
                    arg);

            return predicate;
        }

        /// <summary>
        ///     Сохранить изменения
        /// </summary>
        /// <param name="unitOfWork">Единица работы</param>
        internal virtual void CommitChanges(UnitOfWorkEf unitOfWork)
        {
            unitOfWork.Commit();
            unitOfWork.Dispose();
        }

        /// <summary>
        ///     Базовая функция
        /// </summary>
        /// <param name="action">Функция действия</param>
        /// <param name="unitOfWork">Единица работы</param>
        /// <param name="entities">Обрабатываемые объекты</param>
        protected virtual void BaseAction(Action<DbSet<TEntity>, TEntity> action, IUnitOfWork unitOfWork = null,
            params TEntity[] entities)
        {
            if (entities == null)
            {
                return;
            }

            var unit = unitOfWork as UnitOfWorkEf;

            UnitOfWorkEf unnerUnitOfWork = unit ?? (UnitOfWorkEf)_unitOfWorkFactory.Create();

            foreach (TEntity entity in entities)
            {
                action(unnerUnitOfWork.DbContext.Set<TEntity>(), entity);
            }

            // Комитим только если использовался внутренний UofW
            if (unit == null)
            {
                CommitChanges(unnerUnitOfWork);
            }
        }
    }
}