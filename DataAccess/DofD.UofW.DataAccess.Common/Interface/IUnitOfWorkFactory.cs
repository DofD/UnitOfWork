using System.Data;

namespace DofD.UofW.DataAccess.Common.Interface
{
    /// <summary>
    ///     Интерфейс фабрики единицы работы
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        ///     Создает единицу работы, если не будет вызван метод <see cref="IUnitOfWork.Commit" />, то автоматически будет
        ///     выполнен
        ///     <see cref="IUnitOfWork.Rollback" />
        /// </summary>
        /// <param name="isolationLevel">Уровень изоляции</param>
        /// <returns>Единица работы</returns>
        IUnitOfWork Create(IsolationLevel isolationLevel);

        /// <summary>
        ///     Создает единицу работы, если не будет вызван метод <see cref="IUnitOfWork.Commit" />, то автоматически будет
        ///     выполнен
        ///     <see cref="IUnitOfWork.Rollback" />
        /// </summary>
        /// <returns>Единица работы</returns>
        IUnitOfWork Create();
    }
}