namespace DofD.UofW.DataAccess.Adapters.EF.Test
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Common.Interface;

    using EF.Impl;

    using Entities;

    using Impl;

    using Interface;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLog;

    [TestClass]
    public class ConcurrentRepositoryEfTest
    {
        private readonly IRepository<Guid, Department> _repositoryDepartment;

        public ConcurrentRepositoryEfTest()
        {
            ILogger logger = LogManager.GetCurrentClassLogger();

            IDbContextFactory contextFactory = new DbContextFactory(
                new DatabaseInitializer<EntitiesContext>(),
                new TestContextConfig(),
                logger);

            var uofWFactory = new UnitOfWorkFactoryEf(contextFactory);

            this._repositoryDepartment = new ConcurrentRepositoryEf<Guid, Department>(uofWFactory);
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

            this._repositoryDepartment.Insert(department1, department2);

            // Проверим что добавились
            var departments = this._repositoryDepartment.GetAll(d => d.Id == id1 || d.Id == id2).ToList();
            Assert.IsTrue(departments.Count == 2);

            // Удаляем
            this._repositoryDepartment.Delete(id1, id2);

            // Проверим что удалились
            departments = this._repositoryDepartment.GetAll(d => d.Id == id1 || d.Id == id2).ToList();
            Assert.IsTrue(departments.Count == 0);
        }

        /// <summary>
        ///     Тест получения
        /// </summary>
        [TestMethod]
        public void GetAllCascadeTest()
        {
            var departments = this._repositoryDepartment.GetAll(d => d.Courses).ToList();
            Assert.IsNotNull(departments);
            Assert.IsTrue(departments.Where(d => d.Courses != null).Any(d => d.Courses.Count > 0));
        }

        /// <summary>
        ///     Тест получения
        /// </summary>
        [TestMethod]
        public void GetAllTest()
        {
            var departments = this._repositoryDepartment.GetAll();
            Assert.IsNotNull(departments);
            Assert.IsTrue(departments.All(d => d.Courses == null));
        }

        /// <summary>
        ///     Тест получения каскада
        /// </summary>
        [TestMethod]
        public void GetCascadeTest()
        {
            var department = this._repositoryDepartment.Get(
                new Guid("5DE641CB-6D5F-4400-8F51-AEF4E4550955"),
                d => d.Courses);
            Assert.IsNotNull(department);
            Assert.IsNotNull(department.Courses);
            Assert.IsTrue(department.Courses.Count > 0);
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

        /// <summary>
        ///     Тест вставки каскада
        /// </summary>
        [TestMethod]
        public void InsertCascadeTest()
        {
            var department = new Department
                             {
                                 Id = new Guid("5DE641CB-6D5F-4400-8F51-AEF4E4550955"),
                                 Name = "Test " + DateTime.Now,
                                 Budget = 15,
                                 StartDate = DateTime.Now,
                                 Courses =
                                     new Collection<Course>
                                     {
                                         new Course
                                         {
                                             Id = Guid.NewGuid(),
                                             Title = "Courses " + DateTime.Now,
                                             Credits = 100
                                         },
                                         new Course
                                         {
                                             Id = Guid.NewGuid(),
                                             Title = "Courses2 " + DateTime.Now,
                                             Credits = 100
                                         },
                                         new Course
                                         {
                                             Id = Guid.NewGuid(),
                                             Title = "Courses3 " + DateTime.Now,
                                             Credits = 100
                                         }
                                     }
                             };

            this._repositoryDepartment.Insert(department);
        }

        /// <summary>
        ///     Тест вставки
        /// </summary>
        [TestMethod]
        public void InsertTest()
        {
            var department = new Department
                             {
                                 Id = new Guid("4D97F4C1-A4F9-421B-8DCC-8D9127B2D756"),
                                 Name = "Test " + DateTime.Now,
                                 Budget = 15,
                                 StartDate = DateTime.Now
                             };

            this._repositoryDepartment.Insert(department);
        }

        /// <summary>
        ///     Тест обновления
        /// </summary>
        [TestMethod]
        public void UpdateTest()
        {
            var department = this._repositoryDepartment.Get(new Guid("4D97F4C1-A4F9-421B-8DCC-8D9127B2D756"));
            Assert.IsNotNull(department);

            // Обновим
            var name = "Test " + DateTime.Now;
            department.Name = name;
            this._repositoryDepartment.Update(department);

            // Проверим что обновился
            department = this._repositoryDepartment.Get(new Guid("4D97F4C1-A4F9-421B-8DCC-8D9127B2D756"));
            Assert.AreEqual(department.Name, name);
        }

        /// <summary>
        ///     Тест добавить или обновить
        /// </summary>
        [TestMethod]
        public void InsertOrUpdateTest()
        {
            // Проверим что такого нет
            var department = this._repositoryDepartment.Get(new Guid("4D97F4C1-A4F9-421B-8DCC-8D9127B2D759"));
            Assert.IsNull(department);

            // Создадим
            department = new Department
            {
                Id = new Guid("4D97F4C1-A4F9-421B-8DCC-8D9127B2D759"),
                Name = "Test " + DateTime.Now,
                Budget = 15,
                StartDate = DateTime.Now
            };

            this._repositoryDepartment.InsertOrUpdate(department);

            // Проверим что добавился
            department = this._repositoryDepartment.Get(new Guid("4D97F4C1-A4F9-421B-8DCC-8D9127B2D759"));
            Assert.IsNotNull(department);

            // Обновим
            var name = "Test " + DateTime.Now;
            department.Name = name;
            this._repositoryDepartment.InsertOrUpdate(department);

            // Проверим что обновился
            department = this._repositoryDepartment.Get(new Guid("4D97F4C1-A4F9-421B-8DCC-8D9127B2D759"));
            Assert.AreEqual(department.Name, name);

            // Удалим
            this._repositoryDepartment.Delete(department.Id);
        }
    }
}