﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            this._unitOfWorkFactory = unitOfWorkFactory;
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
        ///     Удалить объект
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="ids">Идентификаторы</param>
        public void Delete(IUnitOfWork unitOfWork = null, params TId[] ids)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Удалить объект
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="entities">Удаляемые объекты</param>
        public void Delete(IUnitOfWork unitOfWork = null, params TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Удалить объект
        /// </summary>
        /// <param name="ids">Идентификаторы</param>
        public void Delete(params TId[] ids)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Удалить объект
        /// </summary>
        /// <param name="entities">Удаляемые объекты</param>
        public void Delete(params TEntity[] entities)
        {
            throw new NotImplementedException();
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
        public TEntity Get(TId id, IUnitOfWork unitOfWork = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Получить объект
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Объект из БД</returns>
        public TEntity Get(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
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
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> @where = null, IUnitOfWork unitOfWork = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Получить выборку объектов
        /// </summary>
        /// <param name="where">Условие выбора</param>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Выборка объектов</returns>
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> @where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Получить выборку объектов
        /// </summary>
        /// <param name="includeProperties">Подгружаемые свойства</param>
        /// <returns>Выборка объектов</returns>
        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
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
        public IEnumerable<TEntity> GetAll(IUnitOfWork unitOfWork, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Сохранить объект
        /// </summary>
        /// <param name="unitOfWork">
        ///     Единица работы
        ///     <remarks>Если репозиторий используется в рамках какой либо единицы иначе null</remarks>
        /// </param>
        /// <param name="entities">Объекты для сохранения</param>
        public void Save(IUnitOfWork unitOfWork = null, params TEntity[] entities)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Сохранить объект
        /// </summary>
        /// <param name="entities">Объекты для сохранения</param>
        public void Save(params TEntity[] entities)
        {
            throw new NotImplementedException();
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
    }
}