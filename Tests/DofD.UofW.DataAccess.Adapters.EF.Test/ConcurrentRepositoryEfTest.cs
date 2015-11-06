using System;
using DofD.UofW.DataAccess.Adapters.EF.Impl;
using DofD.UofW.DataAccess.Adapters.EF.Interface;
using DofD.UofW.DataAccess.Adapters.EF.Test.Impl;
using DofD.UofW.DataAccess.Common.Interface;
using DofD.UofW.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DofD.UofW.DataAccess.Adapters.EF.Test
{
    [TestClass]
    public class ConcurrentRepositoryEfTest
    {
        private readonly IRepository<Guid, Department> _repositoryDepartment;

        public ConcurrentRepositoryEfTest()
        {
            IDbContextFactory contextFactory = new DbContextFactory(new DatabaseInitializer<EntitiesContext>(), new TestContextConfig());
            var uofWFactory = new UnitOfWorkFactoryEf(contextFactory);

            _repositoryDepartment = new ConcurrentRepositoryEf<Guid, Department>(uofWFactory);
        }

        [TestMethod]
        public void TestInsert()
        {
            var department = new Department
            {
                Id = Guid.NewGuid(),
                Name = "Test " + DateTime.Now,
                Budget = 15,
                StartDate = DateTime.Now
            };

            _repositoryDepartment.Insert(department);
        }
    }
}
