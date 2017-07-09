#region Using Directives

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Loom.Annotations;
using Loom.Extensibility;

#endregion

namespace Loom.Ioc
{
    /// <summary>
    ///     Represents a class used to register and retrieve IOC dependencies.
    /// </summary>
    public static partial class IocContainer
    {
        private static readonly Dictionary<Type, ITypeCreator> Lookup = new Dictionary<Type, ITypeCreator>();

        /// <summary>
        ///     Registers a new dependency.
        /// </summary>
        /// <typeparam name="TBase">The base type of the registered dependency.</typeparam>
        /// <typeparam name="TConcrete">The concrete type to instantiate when for the base type.</typeparam>
        public static void Register<TBase, TConcrete>() where TConcrete : TBase
        {
            RegisterPrivate<TBase, TConcrete>(null, null);
        }

        /// <summary>
        ///     Registers a new dependency without compile time checks..
        /// </summary>
        /// <typeparam name="TBase">The base type of the registered dependency.</typeparam>
        /// <typeparam name="TConcrete">The concrete type to instantiate when for the base type.</typeparam>
        public static void RegisterExternal<TBase, TConcrete>()
        {
            RegisterPrivate<TBase, TConcrete>(null, null);
        }

        /// <summary>
        ///     Registers a new dependency.
        /// </summary>
        /// <typeparam name="TBase">The base type of the registered dependency.</typeparam>
        /// <typeparam name="TConcrete">The concrete type to instantiate when for the base type.</typeparam>
        /// <param name="initializer">
        ///     An <see cref="System.Action{TConcrete}" /> used to initialize the
        ///     properties of the concrete type.
        /// </param>
        public static void Register<TBase, TConcrete>(Action<TConcrete> initializer)
        {
            RegisterPrivate<TBase, TConcrete>(initializer, null);
        }

        /// <summary>
        ///     Registers a new dependency.
        /// </summary>
        /// <typeparam name="TBase">The base type of the registered dependency.</typeparam>
        /// <typeparam name="TConcrete">The concrete type to instantiate when for the base type.</typeparam>
        /// <param name="constructor">
        ///     A <see cref="System.Func{TResult}"></see> used to instantiate the
        ///     concrete class.
        /// </param>
        public static void Register<TBase, TConcrete>(Func<TConcrete> constructor)
        {
            RegisterPrivate<TBase, TConcrete>(null, constructor);
        }

        /// <summary>
        ///     Registers a new dependency.
        /// </summary>
        /// <typeparam name="TBase">The base type of the registered dependency.</typeparam>
        /// <typeparam name="TConcrete">The concrete type to instantiate when for the base type.</typeparam>
        /// <param name="constructor">
        ///     A <see cref="System.Func{TResult}"></see> used to instantiate the
        ///     concrete class.
        /// </param>
        /// <param name="initializer">
        ///     An <see cref="System.Action{TConcrete}" /> used to initialize the
        ///     properties of the concrete type.
        /// </param>
        public static void Register<TBase, TConcrete>(Func<TConcrete> constructor, Action<TConcrete> initializer)
        {
            RegisterPrivate<TBase, TConcrete>(initializer, constructor);
        }

        /// <summary>
        ///     Registers a new singleton dependency.
        /// </summary>
        /// <typeparam name="TBase">The base type of the registered dependency.</typeparam>
        /// <typeparam name="TConcrete">The concrete type to instantiate when for the base type.</typeparam>
        public static void RegisterSingleton<TBase, TConcrete>()
        {
            RegisterPrivate<TBase, TConcrete>(null, null, true);
        }

        /// <summary>
        ///     Registers a new dependency.
        /// </summary>
        /// <typeparam name="TBase">The base type of the registered dependency.</typeparam>
        /// <typeparam name="TConcrete">The concrete type to instantiate when for the base type.</typeparam>
        /// <param name="initializer">
        ///     An <see cref="System.Action{TConcrete}" /> used to initialize the
        ///     properties of the concrete type.
        /// </param>
        public static void RegisterSingleton<TBase, TConcrete>(Action<TConcrete> initializer)
        {
            RegisterPrivate<TBase, TConcrete>(initializer, null, true);
        }

        /// <summary>
        ///     Registers a new dependency.
        /// </summary>
        /// <typeparam name="TBase">The base type of the registered dependency.</typeparam>
        /// <typeparam name="TConcrete">The concrete type to instantiate when for the base type.</typeparam>
        /// <param name="constructor">
        ///     A <see cref="System.Func{TResult}"></see> used to instantiate the
        ///     concrete class.
        /// </param>
        public static void RegisterSingleton<TBase, TConcrete>(Func<TConcrete> constructor)
        {
            RegisterPrivate<TBase, TConcrete>(null, constructor, true);
        }

        /// <summary>
        ///     Registers a new dependency.
        /// </summary>
        /// <typeparam name="TBase">The base type of the registered dependency.</typeparam>
        /// <typeparam name="TConcrete">The concrete type to instantiate when for the base type.</typeparam>
        /// <param name="constructor">
        ///     A <see cref="System.Func{TResult}"></see> used to instantiate the
        ///     concrete class.
        /// </param>
        /// <param name="initializer">
        ///     An <see cref="System.Action{TConcrete}" /> used to initialize the
        ///     properties of the concrete type.
        /// </param>
        public static void RegisterSingleton<TBase, TConcrete>(Func<TConcrete> constructor, Action<TConcrete> initializer)
        {
            RegisterPrivate<TBase, TConcrete>(initializer, constructor, true);
        }

        /// <summary>
        ///     Re-registers a dependency.
        /// </summary>
        /// <typeparam name="TBase">The base type of the registered dependency.</typeparam>
        /// <typeparam name="TConcrete">The concrete type to instantiate when for the base type.</typeparam>
        /// <param name="initializationAction">
        ///     An <see cref="System.Action{TConcrete}" /> used to initialize
        ///     the properties of the concrete type.
        /// </param>
        /// <param name="constructor">
        ///     A <see cref="System.Func{TResult}"></see> used to instantiate the
        ///     concrete class.
        /// </param>
        public static void ReRegister<TBase, TConcrete>(Action<TConcrete> initializationAction = null, Func<TConcrete> constructor = null)
        {
            ReRegisterPrivate<TBase, TConcrete>(initializationAction, constructor);
        }

        /// <summary>
        ///     Retrieves a concrete instance for the supplied base type.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If no type is registered that implements the provided interface or base class, this method will search
        ///         for a default implementation. A default implementation includes types that inherit from an interface
        ///         with a name that is identical minus the I in the interface name or that inherit from a base class
        ///         with a name that is identical minus "Base" at the end of the name.
        ///     </para>
        ///     <para>
        ///         For example, a type named Logger that
        ///         inherits from ILogger would be the default implementation for ILogger. A type named Controller that inherits
        ///         from ControllerBase would be the default implementation for the type ControllerBase.
        ///     </para>
        /// </remarks>
        /// <example>
        ///     The following examples shows how to retrieve an instance from the IOC container.
        ///     <code>
        /// ILogger logger = IocContainer.Retrieve&lt;ILogger&gt;();
        /// </code>
        /// </example>
        /// <typeparam name="TBase">The base type for which to retrieve the concrete type.</typeparam>
        /// <returns>An instance implementation of the supplied base type.</returns>
        [NotNull]
        public static TBase Retrieve<TBase>(Action<TBase> initializationAction = null)
        {
            TBase instance = RetrievePrivate<TBase>();
            initializationAction?.Invoke(instance);

            return instance;
        }

        /// <summary>
        ///     Removes all registered dependencies.
        /// </summary>
        public static void Clear()
        {
            Lookup.Clear();
        }

        private static ITypeCreator GenerateTypeCreator<TConcrete>(Action<TConcrete> initializer, Func<TConcrete> constructor, bool singleton = false)
        {
            ITypeCreator creator = singleton
                ? (ITypeCreator) new SingletonCreator(typeof(TConcrete))
                : new InstanceCreator(typeof(TConcrete));

            if (initializer != null || constructor != null)
                creator = new TypeInitializer<TConcrete>(creator, initializer, constructor);
            return creator;
        }

        private static string GetConcreteTypeName(string baseTypeName, bool isInterface)
        {
            return isInterface && baseTypeName.StartsWith("I", false, CultureInfo.InvariantCulture)
                ? baseTypeName.Substring(1)
                : baseTypeName.EndsWith("Base", false, CultureInfo.InvariantCulture)
                    ? baseTypeName.Substring(0, baseTypeName.Length - 4)
                    : baseTypeName;
        }

        private static void RegisterPrivate<TBase, TConcrete>(Action<TConcrete> initializer, Func<TConcrete> constructor, bool singleton = false)
        {
            Type baseType = typeof(TBase);
            Type derivedType = typeof(TConcrete);
            if (!baseType.IsAssignableFrom(derivedType))
                throw new IocDependencyException("Unable to cast object of type '" + derivedType.Name + "' to type '" + baseType.Name + "'.");

            ITypeCreator value;
            if (Lookup.TryGetValue(baseType, out value))
            {
                ITypeCreator typeCreator = value;
                if (typeCreator.AutoRegistered)
                    Lookup[baseType] = GenerateTypeCreator(initializer, constructor, singleton);
                else
                    throw new IocDependencyException(SR.ExceptionTypeAlreadyRegistered(baseType.FullName));
            }
            else
            {
                Lookup.Add(baseType, GenerateTypeCreator(initializer, constructor, singleton));
            }
        }

        private static void ReRegisterPrivate<TBase, TConcrete>(Action<TConcrete> initializer, Func<TConcrete> constructor)
        {
            Type baseType = typeof(TBase);
            Type derivedType = typeof(TConcrete);
            if (!baseType.IsAssignableFrom(derivedType))
                throw new IocDependencyException("Unable to cast object of type '" + derivedType.Name + "' to type '" + baseType.Name + "'.");

            ITypeCreator creator = GenerateTypeCreator(initializer, constructor);
            if (Lookup.ContainsKey(baseType))
                Lookup[baseType] = creator;
            else
                Lookup.Add(baseType, creator);
        }

        private static ITypeCreator RegisterDefaultType(Type baseType)
        {
            string typeName = GetConcreteTypeName(baseType.Name, baseType.IsInterface);
            Type defaultType = TypeLoader.GetDerivedType(typeName, baseType);
            if (defaultType == null)
                throw new IocDependencyException(SR.ExceptionDefaultImplementationNotFound(baseType.FullName));

            InstanceCreator creator = new InstanceCreator(defaultType) {AutoRegistered = true};
            Lookup.Add(baseType, creator);

            return creator;
        }

        private static TBase RetrievePrivate<TBase>()
        {
            Type type = typeof(TBase);
            ITypeCreator value;
            if (Lookup.TryGetValue(type, out value))
                return (TBase) value.Create();

            ITypeCreator creator = RegisterDefaultType(type);
            return (TBase) creator.Create();
        }
    }

    #region Nested types

    public static partial class IocContainer
    {
        #region Nested type: InstanceCreator

        private class InstanceCreator : TypeCreator
        {
            private readonly ConstructorInfo constructor;
            private Type[] parameterTypes;

            public InstanceCreator(Type concreteType) : base(concreteType)
            {
                ConstructorInfo[] constructors = concreteType.GetConstructors();
                switch (constructors.Length)
                {
                    case 0:
                        throw new IocDependencyException(SR.ExceptionNoConstructorsDefined(concreteType.FullName));
                    case 1:
                        constructor = constructors[0];
                        break;
                    default: // TODO: Handle additional constructors.
                        constructor = constructors[0];
                        break;
                }
            }

            public override object Create()
            {
                if (parameterTypes == null)
                    parameterTypes = CacheConstructor(constructor);

                List<object> arguments = new List<object>(parameterTypes.Length);
                foreach (Type type in parameterTypes)
                    arguments.Add(Activator.CreateInstance(type));

                return constructor.Invoke(arguments.ToArray());
            }
        }

        #endregion

        #region Nested type: ITypeCreator

        private interface ITypeCreator
        {
            bool AutoRegistered { get; }
            Type ConcreteType { get; }

            object Create();
        }

        #endregion

        #region Nested type: SingletonCreator

        // TODO: Now that this works, simplify by reducing repeated code from InstanceCreator.
        private class SingletonCreator : TypeCreator
        {
            private readonly ConstructorInfo constructor;
            private Type[] parameterTypes;
            private object singleton;

            public SingletonCreator(Type concreteType)
                : base(concreteType)
            {
                ConstructorInfo[] constructors = concreteType.GetConstructors();
                switch (constructors.Length)
                {
                    case 0:
                        throw new IocDependencyException(SR.ExceptionNoConstructorsDefined(concreteType.FullName));
                    case 1:
                        constructor = constructors[0];
                        break;
                    default: // TODO: Handle additional constructors.
                        constructor = constructors[0];
                        break;
                }
            }

            public override object Create()
            {
                if (singleton != null)
                    return singleton;

                if (parameterTypes == null)
                    parameterTypes = CacheConstructor(constructor);

                List<object> arguments = new List<object>(parameterTypes.Length);
                arguments.AddRange(parameterTypes.Select(Activator.CreateInstance));

                singleton = constructor.Invoke(arguments.ToArray());
                return singleton;
            }
        }

        #endregion

        #region Nested type: TypeCreator

        private abstract class TypeCreator : ITypeCreator
        {
            protected TypeCreator(Type concreteType)
            {
                ConcreteType = concreteType;
            }

            #region ITypeCreator Members

            public abstract object Create();
            public Type ConcreteType { get; }

            public bool AutoRegistered { get; internal set; }

            #endregion

            protected static Type[] CacheConstructor(ConstructorInfo constructor)
            {
                ParameterInfo[] parameters = constructor.GetParameters();
                List<Type> args = new List<Type>(parameters.Length);
                for (int index = 0; index < parameters.Length; index++)
                {
                    Type parameterType = parameters[index].ParameterType;
                    ITypeCreator value;
                    args.Add(!Lookup.TryGetValue(parameterType, out value) ? RegisterDefaultType(parameterType).ConcreteType : value.ConcreteType);
                }
                return args.ToArray();
            }
        }

        #endregion

        #region Nested type: TypeInitializer

        private class TypeInitializer<T> : ITypeCreator
        {
            public TypeInitializer(ITypeCreator creator, Action<T> initializer, Func<T> constructor)
            {
                Creator = creator;
                Initializer = initializer;
                Constructor = constructor;
            }

            private Func<T> Constructor { get; }
            private ITypeCreator Creator { get; }
            private Action<T> Initializer { get; }

            #region ITypeCreator Members

            public object Create()
            {
                T instance = (T) (Constructor == null ? Creator.Create() : Constructor());
                Initializer?.Invoke(instance);
                return instance;
            }

            public Type ConcreteType => Creator.ConcreteType;

            public bool AutoRegistered => Creator.AutoRegistered;

            #endregion
        }

        #endregion
    }

    #endregion
}