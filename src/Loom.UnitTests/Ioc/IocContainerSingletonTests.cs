#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom.Ioc
{
    [TestFixture]
    public class IocContainerSingletonTests
    {
        [TearDown]
        public void TearDown()
        {
            IocContainer.Clear();
        }

        [Test]
        public void RegisterType()
        {
            IocContainer.RegisterSingleton<ILogger, Logger>();
            ILogger logger1 = IocContainer.Retrieve<ILogger>();
            ILogger logger2 = IocContainer.Retrieve<ILogger>();
            ILogger logger3 = IocContainer.Retrieve<ILogger>();

            Assert.IsNotNull(logger1);
            Assert.IsNotNull(logger2);
            Assert.IsNotNull(logger3);
            Assert.IsInstanceOf<ILogger>(logger1);
            Assert.IsInstanceOf<ILogger>(logger2);
            Assert.IsInstanceOf<ILogger>(logger3);

            Assert.AreSame(logger1, logger2);
            Assert.AreSame(logger1, logger3);
            Assert.AreSame(logger2, logger3);
        }

        [Test]
        public void RegisterTypeWithNonRegisteredDependency()
        {
            IocContainer.RegisterSingleton<IDataContext, DataContext>();
            IDataContext context = IocContainer.Retrieve<IDataContext>();

            Assert.IsNotNull(context);
            Assert.IsInstanceOf<DataContext>(context);

            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<Logger>(context.Log);
        }

        [Test]
        public void RegisterTypeForUseAsConstructorDependency()
        {
            IocContainer.RegisterSingleton<ILogger, Logger>();
            IocContainer.RegisterSingleton<IDataContext, DataContext>();
            IDataContext context = IocContainer.Retrieve<IDataContext>();

            Assert.IsNotNull(context);
            Assert.IsInstanceOf<DataContext>(context);

            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<Logger>(context.Log);
        }

        [Test]
        public void RegisterTypeForUseAsConstructorDependencyAfterUsage()
        {
            IocContainer.RegisterSingleton<IDataContext, DataContext>();
            IocContainer.RegisterSingleton<ILogger, Logger>();

            IDataContext context = IocContainer.Retrieve<IDataContext>();

            Assert.IsNotNull(context);
            Assert.IsInstanceOf<DataContext>(context);

            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<Logger>(context.Log);
        }

        [Test]
        public void RegisterTypeWithPropertyInitializer()
        {
            const string dbName = "TestDb";

            IocContainer.RegisterSingleton<IDataContext, DataContext>(dc => dc.DbName = dbName);
            IocContainer.RegisterSingleton<ILogger, DatabaseLogger>();

            IDataContext context = IocContainer.Retrieve<IDataContext>();

            Assert.IsNotNull(context);
            Assert.IsInstanceOf<DataContext>(context);

            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<DatabaseLogger>(context.Log);
            Assert.AreEqual(dbName, context.DbName);
        }

        [Test]
        public void RegisterTypeWithExplicitConstruction()
        {
            const string loggerName = "ExplicitConstruction";

            IocContainer.RegisterSingleton<ILogger, Logger>(() => new Logger(loggerName));
            ILogger logger = IocContainer.Retrieve<ILogger>();

            Assert.IsNotNull(logger);
            Assert.AreEqual(loggerName, logger.Name);
        }

        [Test]
        public void RegisterTypeWithExplicitConstructionAndDependency()
        {
            Func<DataContext> constructor = () => new DataContext(IocContainer.Retrieve<ILogger>());
            IocContainer.RegisterSingleton<IDataContext, DataContext>(constructor, null);
            // Can be written as:
            // IocContainer.Register<IDataContext, DataContext>(null, () => new DataContext(IocContainer.Retrieve<ILogger>()));

            IDataContext context = IocContainer.Retrieve<IDataContext>();

            Assert.IsNotNull(context);
            Assert.IsInstanceOf<DataContext>(context);

            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<Logger>(context.Log);
        }

//        [Test]
//        public void ReRegisterBaseType() 
//        {
//            IocContainer.Register<ILogger, Logger>(l => l.Name = "First");
//            ILogger logger = IocContainer.Retrieve<ILogger>();
//            Assert.IsNotNull(logger);
//            Assert.AreEqual("First", logger.Name);
//
//            IocContainer.ReRegister<ILogger, DatabaseLogger>(l => l.Name = "Second");
//            logger = IocContainer.Retrieve<ILogger>();
//            Assert.IsNotNull(logger);
//            Assert.AreEqual("Second", logger.Name);
//        }

        [Test]
        public void RegisterTypeWithRegisteredDefaultDependency()
        {
            IocContainer.RegisterSingleton<ILogger, Logger>();
            IocContainer.RegisterSingleton<IDataContext, DataContext>();

            DataContext context = IocContainer.Retrieve<IDataContext>() as DataContext;

            Assert.IsNotNull(context);
            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<Logger>(context.Log);
        }

        [Test]
        public void RegisterTypeWithRegisteredNonDefaultDependency()
        {
            IocContainer.RegisterSingleton<ILogger, DatabaseLogger>();
            IocContainer.RegisterSingleton<IDataContext, DataContext>();

            DataContext context = IocContainer.Retrieve<IDataContext>() as DataContext;

            Assert.IsNotNull(context);
            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<DatabaseLogger>(context.Log);
        }

        [Test]
        public void RegisterTypeWithLateRegisteredDefaultDependency()
        {
            IocContainer.RegisterSingleton<IDataContext, DataContext>();
            IocContainer.RegisterSingleton<ILogger, Logger>();

            DataContext context = IocContainer.Retrieve<IDataContext>() as DataContext;

            Assert.IsNotNull(context);
            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<Logger>(context.Log);
        }

        [Test]
        public void RegisterTypeWithLateRegisteredNonDefaultDependency()
        {
            IocContainer.RegisterSingleton<IDataContext, DataContext>();
            IocContainer.RegisterSingleton<ILogger, DatabaseLogger>();

            DataContext context = IocContainer.Retrieve<IDataContext>() as DataContext;

            Assert.IsNotNull(context);
            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<DatabaseLogger>(context.Log);
        }

        [Test]
        public void RetrieveWithInitializationAction()
        {
            const string expectedValue = "InitializedAtRetrieve";

            IocContainer.RegisterSingleton<ILogger, DatabaseLogger>();
            ILogger logger = IocContainer.Retrieve<ILogger>(l => l.Name = expectedValue);

            Assert.IsNotNull(logger);
            Assert.AreEqual(expectedValue, logger.Name);
        }

        [Test]
        [ExpectedException(typeof(IocDependencyException))]
        public void ExceptionRegisterBaseAndConcreteTypeTwice()
        {
            IocContainer.RegisterSingleton<IDataContext, DataContext>();
            IocContainer.RegisterSingleton<IDataContext, DataContext>();
        }

        [Test]
        [ExpectedException(typeof(IocDependencyException))]
        public void ExceptionRegisterBaseTypeTwice()
        {
            IocContainer.RegisterSingleton<ILogger, Logger>();
            IocContainer.RegisterSingleton<ILogger, DatabaseLogger>();
        }

        #region Nested type: DatabaseLogger

        public class DatabaseLogger : ILogger
        {
            #region ILogger Members

            public string Name { get; set; }

            public void LogError() { }

            #endregion
        }

        #endregion

        #region Nested type: DataContext

        public class DataContext : IDataContext
        {
            public DataContext(ILogger logger)
            {
                Log = logger;
            }

            #region IDataContext Members

            public ILogger Log { get; set; }
            public string DbName { get; set; }

            #endregion
        }

        #endregion

        #region Nested type: IDataContext

        public interface IDataContext
        {
            ILogger Log { get; set; }
            string DbName { get; set; }
        }

        #endregion

        #region Nested type: ILogger

        public interface ILogger
        {
            string Name { get; set; }
            void LogError();
        }

        #endregion

        #region Nested type: INoImplemntations

        public interface INoImplemntations { }

        #endregion

        #region Nested type: Logger

        public class Logger : LoggerBase
        {
            public Logger() { }

            public Logger(string name)
            {
                Name = name;
            }

            public override string Name { get; set; }

            public override void LogError() { }
        }

        #endregion

        #region Nested type: LoggerBase

        public abstract class LoggerBase : ILogger
        {
            #region ILogger Members

            public abstract string Name { get; set; }
            public abstract void LogError();

            #endregion
        }

        #endregion
    }
}