#region Using Directives

using System;

#endregion

namespace Loom.Web.Portal.Controllers
{
    /// <summary>
    ///     Represents a parameter to an <see cref="IController" /> action method.
    /// </summary>
    internal sealed class ActionParameter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ActionParameter" /> class.
        /// </summary>
        /// <param name="name">A <see cref="string" /> representing the name of the parameter.</param>
        /// <param name="assembly">The parameter's <see cref="Type" />.</param>
        public ActionParameter(string name, Type assembly)
        {
            Argument.Assert.IsNotNullOrEmpty(name, nameof(name));
            Argument.Assert.IsNotNull(assembly, nameof(assembly));

            Name = name;
            TypeName = assembly.AssemblyQualifiedName;
        }

        /// <summary>
        ///     Gets the assembly-qualified name of the <see cref="ActionParameter" /> type, which
        ///     includes the name of the assembly from which the <see cref="ActionParameter" /> type was loaded.
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        ///     Gets the name of the <see cref="ActionParameter" />.
        /// </summary>
        public string Name { get; }
    }
}