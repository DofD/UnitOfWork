using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DofD.UofW.DataAccess.Adapters.EF.Impl;
using DofD.UofW.DataAccess.Adapters.EF.Interface;
using DofD.UofW.DataAccess.Adapters.EF.Test.Impl;
using DofD.UofW.DataAccess.Common.Interface;
using DofD.UofW.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace DofD.UofW.DataAccess.Adapters.EF.Test
{
    [TestClass]
    public class ConcurrentRepositoryEfTest
    {
        private readonly IRepository<Guid, Department> _repositoryDepartment;

        public ConcurrentRepositoryEfTest()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            IDbContextFactory contextFactory = new DbContextFactory(new DatabaseInitializer<EntitiesContext>(),
                new TestContextConfig(), logger);

            var uofWFactory = new UnitOfWorkFactoryEf(contextFactory);

            _repositoryDepartment = new ConcurrentRepositoryEf<Guid, Department>(uofWFactory);
        }

        /// <summary>
        ///     Тест вставки
        /// </summary>
        [TestMethod]
        public void InsertTest()
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

        /// <summary>
        ///     Тест вставки каскада
        /// </summary>
        [TestMethod]
        public void InsertCascadeTest()
        {
            var department = new Department
            {
                Id = Guid.NewGuid(),
                Name = "Test " + DateTime.Now,
                Budget = 15,
                StartDate = DateTime.Now,
                Courses =
                    new Collection<Course>
                    {
                        new Course {Id = Guid.NewGuid(), Title = "Courses " + DateTime.Now, Credits = 100},
                        new Course {Id = Guid.NewGuid(), Title = "Courses2 " + DateTime.Now, Credits = 100},
                        new Course {Id = Guid.NewGuid(), Title = "Courses3 " + DateTime.Now, Credits = 100}
                    }
            };

            _repositoryDepartment.Insert(department);
        }

        /// <summary>
        ///     Тест получения
        /// </summary>
        [TestMethod]
        public void GetTest()
        {
            Department department = _repositoryDepartment.Get(new Guid("4D97F4C1-A4F9-421B-8DCC-8D9127B2D756"));
            Assert.IsNotNull(department);
            Assert.IsNull(department.Courses);
        }

        /// <summary>
        ///     Тест получения каскада
        /// </summary>
        [TestMethod]
        public void GetCascadeTest()
        {
            Department department = _repositoryDepartment.Get(new Guid("5DE641CB-6D5F-4400-8F51-AEF4E4550955"),
                d => d.Courses);
            Assert.IsNotNull(department);
            Assert.IsNotNull(department.Courses);
            Assert.IsTrue(department.Courses.Count > 0);
        }

        /// <summary>
        ///     Тест получения
        /// </summary>
        [TestMethod]
        public void GetAllTest()
        {
            IEnumerable<Department> departments = _repositoryDepartment.GetAll();
            Assert.IsNotNull(departments);
            Assert.IsTrue(departments.All(d => d.Courses == null));
        }


        /// <summary>
        ///     Тест получения
        /// </summary>
        [TestMethod]
        public void GetAllCascadeTest()
        {
            List<Department> departments = _repositoryDepartment.GetAll(d => d.Courses).ToList();
            Assert.IsNotNull(departments);
            Assert.IsTrue(departments.Where(d => d.Courses != null).Any(d => d.Courses.Count > 0));
        }

        /// <summary>
        ///     Тест удаления по идентификатору
        /// </summary>
        [TestMethod]
        public void DeleteByIdTest()
        {
            // Добавим 2 сущности
            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();

            var department1 = new Department
            {
                Id = id1,
                Name = "Для удаления 1",
                Budget = 15,
                StartDate = DateTime.Now
            };

            var department2 = new Department
            {
                Id = id2,
                Name = "Для удаления 2",
                Budget = 15,
                StartDate = DateTime.Now
            };

            _repositoryDepartment.Insert(department1, department2);

            // Проверим что добавились
            List<Department> departments = _repositoryDepartment.GetAll(d => d.Id == id1 || d.Id == id2).ToList();
            Assert.IsTrue(departments.Count == 2);

            // Удаляем
            _repositoryDepartment.Delete(id1, id2);

            // Проверим что удалились
            departments = _repositoryDepartment.GetAll(d => d.Id == id1 || d.Id == id2).ToList();
            Assert.IsTrue(departments.Count == 0);
        }
    }
}