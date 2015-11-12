using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DofD.UofW.DataAccess.Common.Interface
{
    /// <summary>
    ///     Репозиторий
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора</typeparam>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public interface IRepository<in TId, TEntity>
    {
        /// <summary>
        ///     Удалить объекты
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="ids">Идентификаторы</param>
        void Delete(IUnitOfWork unitOfWork = null, params TId[] ids);

        /// <summary>
        ///     Удалить объекты
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="entities">Удаляемые объекты</param>
        void Delete(IUnitOfWork unitOfWork = null, params TEntity[] entities);

        /// <summary>
        ///     Удалить объекты
        /// </summary>
        /// <param name="ids">Идентификаторы</param>
        void Delete(params TId[] ids);

        /// <summary>
        ///     Удалить объекты
        /// </summary>
        /// <param name="entities">Удаляемые объекты</param>
        void Delete(params TEntity[] entities);

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
        TEntity Get(TId id, IUnitOfWork unitOfWork = null, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        ///     Получить объект
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Объект из БД</returns>
        TEntity Get(TId id, params Expression<Func<TEntity, object>>[] includeProperties);

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
        IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> where = null,
            IUnitOfWork unitOfWork = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        ///     Получить выборку объектов
        /// </summary>
        /// <param name="where">Условие выбора</param>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Выборка объектов</returns>
        IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        ///     Получить выборку объектов
        /// </summary>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Выборка объектов</returns>
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        ///     Получить выборку объектов
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Выборка объектов</returns>
        IEnumerable<TEntity> GetAll(
            IUnitOfWork unitOfWork,
            params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        ///     Сохранить объект
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="entities">Объекты для сохранения</param>
        void Insert(IUnitOfWork unitOfWork = null, params TEntity[] entities);

        /// <summary>
        ///     Сохранить объект
        /// </summary>
        /// <param name="entities">Объекты для сохранения</param>
        void Insert(params TEntity[] entities);

        /// <summary>
        ///     Сохранить или обновить
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="entities">Объекты</param>
        void SaveOrUpdate(IUnitOfWork unitOfWork = null, params TEntity[] entities);

        /// <summary>
        ///     Сохранить или обновить
        /// </summary>
        /// <param name="entities">Объекты</param>
        void SaveOrUpdate(params TEntity[] entities);

        /// <summary>
        ///     Обновить объект
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="entities">Обновляемый объект</param>
        void Update(IUnitOfWork unitOfWork = null, params TEntity[] entities);

        /// <summary>
        ///     Обновить объект
        /// </summary>
        /// <param name="entities">Обновляемый объект</param>
        void Update(params TEntity[] entities);
    }
}