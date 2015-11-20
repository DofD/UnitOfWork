namespace DofD.UofW.DataAccess.Adapters.NH.Test
{
    using System;

    using Common.Interface;

    using Entities;

    using Impl;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NHibernate;

    [TestClass]
    public class RepositoryNhTest
    {
        private readonly IRepository<Guid, Department> _repositoryDepartment;

        public RepositoryNhTest()
        {
            ISessionFactory sessionFactory;
            IUnitOfWorkFactory uofWFactory = new UnitOfWorkFactoryNh(sessionFactory);
            this._repositoryDepartment = new RepositoryNh<Guid, Department>(uofWFactory);
        }

        /// <summary>
        ///     Тест получения
        /// </summary>
        [TestMethod]
        public void GetTest()
        {
            var department = this._repositoryDepartment.Get(new Guid("4D97F4C1-A4F9-421B-8DCC-8D9127B2D756"));
            Assert.IsNotNull(department);
            Assert.IsNull(department.Courses);
        }
    }
}