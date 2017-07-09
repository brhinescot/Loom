#region Using Directives

using System;
using NUnit.Framework;

#endregion

namespace Loom.Ioc
{
    [TestFixture]
    public class IocContainerInstanceTests
    {
        [TearDown]
        public void TearDown()
        {
            IocContainer.Clear();
        }

        [Test]
        public void RegisterInstanceType()
        {
            IocContainer.Register<ILogger, Logger>();
            ILogger logger1 = IocContainer.Retrieve<ILogger>();
            ILogger logger2 = IocContainer.Retrieve<ILogger>();

            Assert.IsNotNull(logger1);
            Assert.IsNotNull(logger2);
            Assert.IsInstanceOf<ILogger>(logger1);
            Assert.IsInstanceOf<ILogger>(logger2);

            Assert.AreNotSame(logger1, logger2);
        }

        [Test]
        public void RegisterTypeWithNonRegisteredDependency()
        {
            IocContainer.Register<IDataContext, DataContext>();
            IDataContext context = IocContainer.Retrieve<IDataContext>();

            Assert.IsNotNull(context);
            Assert.IsInstanceOf<DataContext>(context);

            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<Logger>(context.Log);
        }

        [Test]
        public void RegisterTypeForUseAsConstructorDependency()
        {
            IocContainer.Register<ILogger, Logger>();
            IocContainer.Register<IDataContext, DataContext>();
            IDataContext context = IocContainer.Retrieve<IDataContext>();

            Assert.IsNotNull(context);
            Assert.IsInstanceOf<DataContext>(context);

            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<Logger>(context.Log);
        }

        [Test]
        public void RegisterTypeForUseAsConstructorDependencyAfterUsage()
        {
            IocContainer.Register<IDataContext, DataContext>();
            IocContainer.Register<ILogger, Logger>();

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

            IocContainer.Register<IDataContext, DataContext>(dc => dc.DbName = dbName);
            IocContainer.Register<ILogger, DatabaseLogger>();

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

            IocContainer.Register<ILogger, Logger>(() => new Logger(loggerName));
            ILogger logger = IocContainer.Retrieve<ILogger>();

            Assert.IsNotNull(logger);
            Assert.AreEqual(loggerName, logger.Name);
        }

        [Test]
        public void RegisterTypeWithExplicitConstructionAndDependency()
        {
            Func<DataContext> constructor = () => new DataContext(IocContainer.Retrieve<ILogger>());
            IocContainer.Register<IDataContext, DataContext>(constructor, null);
            // Can be written as:
            // IocContainer.Register<IDataContext, DataContext>(null, () => new DataContext(IocContainer.Retrieve<ILogger>()));

            IDataContext context = IocContainer.Retrieve<IDataContext>();

            Assert.IsNotNull(context);
            Assert.IsInstanceOf<DataContext>(context);

            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<Logger>(context.Log);
        }

        [Test]
        public void ReRegisterBaseType()
        {
            IocContainer.Register<ILogger, Logger>(l => l.Name = "First");
            ILogger logger = IocContainer.Retrieve<ILogger>();
            Assert.IsNotNull(logger);
            Assert.AreEqual("First", logger.Name);

            IocContainer.ReRegister<ILogger, DatabaseLogger>(l => l.Name = "Second");
            logger = IocContainer.Retrieve<ILogger>();
            Assert.IsNotNull(logger);
            Assert.AreEqual("Second", logger.Name);
        }

        [Test]
        public void RetrieveNonRegisteredDefaultTypeFromInterface()
        {
            ILogger logger = IocContainer.Retrieve<ILogger>();

            Assert.IsNotNull(logger);
            Assert.IsInstanceOf<Logger>(logger);
        }

        [Test]
        public void RetrieveNonRegisteredDefaultTypeFromBaseClass()
        {
            LoggerBase logger = IocContainer.Retrieve<LoggerBase>();

            Assert.IsNotNull(logger);
            Assert.IsInstanceOf<Logger>(logger);
        }

        [Test]
        public void RegisterTypeWithRegisteredDefaultDependency()
        {
            IocContainer.Register<ILogger, Logger>();
            IocContainer.Register<IDataContext, DataContext>();

            DataContext context = IocContainer.Retrieve<IDataContext>() as DataContext;

            Assert.IsNotNull(context);
            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<Logger>(context.Log);
        }

        [Test]
        public void RegisterTypeWithRegisteredNonDefaultDependency()
        {
            IocContainer.Register<ILogger, DatabaseLogger>();
            IocContainer.Register<IDataContext, DataContext>();

            DataContext context = IocContainer.Retrieve<IDataContext>() as DataContext;

            Assert.IsNotNull(context);
            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<DatabaseLogger>(context.Log);
        }

        [Test]
        public void RegisterTypeWithLateRegisteredDefaultDependency()
        {
            IocContainer.Register<IDataContext, DataContext>();
            IocContainer.Register<ILogger, Logger>();

            DataContext context = IocContainer.Retrieve<IDataContext>() as DataContext;

            Assert.IsNotNull(context);
            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<Logger>(context.Log);
        }

        [Test]
        public void RegisterTypeWithLateRegisteredNonDefaultDependency()
        {
            IocContainer.Register<IDataContext, DataContext>();
            IocContainer.Register<ILogger, DatabaseLogger>();

            DataContext context = IocContainer.Retrieve<IDataContext>() as DataContext;

            Assert.IsNotNull(context);
            Assert.IsNotNull(context.Log);
            Assert.IsInstanceOf<DatabaseLogger>(context.Log);
        }

        [Test]
        public void RetrieveWithInitializationAction()
        {
            const string expectedValue = "InitializedAtRetrieve";

            IocContainer.Register<ILogger, DatabaseLogger>();
            ILogger logger = IocContainer.Retrieve<ILogger>(l => l.Name = expectedValue);

            Assert.IsNotNull(logger);
            Assert.AreEqual(expectedValue, logger.Name);
        }

        [Test]
        [ExpectedException(typeof(IocDependencyException))]
        public void ExceptionRegisterBaseAndConcreteTypeTwice()
        {
            IocContainer.Register<IDataContext, DataContext>();
            IocContainer.Register<IDataContext, DataContext>();
        }

        [Test]
        [ExpectedException(typeof(IocDependencyException))]
        public void ExceptionRegisterBaseTypeTwice()
        {
            IocContainer.Register<ILogger, Logger>();
            IocContainer.Register<ILogger, DatabaseLogger>();
        }

        [Test]
        [ExpectedException(typeof(IocDependencyException))]
        public void ExceptionRetrieveNonRegisteredTypeWithoutDefault()
        {
            IocContainer.Retrieve<INoImplemntations>();
        }

        [Test]
        [ExpectedException(typeof(IocDependencyException))]
        public void ExceptionRegisterNonBaseAndConcreteType()
        {
            IocContainer.RegisterExternal<IDataContext, Logger>();
        }

        [Test]
        public void ExceptionRegisterNonBaseAndConcreteTypeProperMessage()
        {
            try
            {
                IocContainer.RegisterExternal<IDataContext, Logger>();
            }
            catch (IocDependencyException e)
            {
                Assert.AreEqual("Unable to cast object of type 'Logger' to type 'IDataContext'.", e.Message);
            }
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