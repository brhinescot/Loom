#region Using Directives

using System.Web.Services.Description;

#endregion

namespace Loom.Web.Services
{
    /// <summary>
    ///     Called by the runtime during WSDL generation.
    /// </summary>
    /// <remarks>
    ///     Must be configured in the web.config or machine.config file.
    /// </remarks>
    public class ValidationExtensionReflector : SoapExtensionReflector
    {
        private int methodCount;

        /// <summary>
        ///     Called by frame work to allow this extension to take part
        ///     in the building of the wsdl file.
        /// </summary>
        public override void ReflectMethod()
        {
            ProtocolReflector reflector = ReflectionContext;
            object validationAttribute = reflector.Method.GetCustomAttribute(typeof(ValidationAttribute));

            // ValidationFormatExtension represents an extension element
            ValidationFormatExtension validationFormatExtension = new ValidationFormatExtension();
            if (validationAttribute != null && methodCount == 0)
            {
                methodCount++;

                // retrieve namespace bindings
                object[] namespaceAtts =
                    reflector.Method.DeclaringType.GetCustomAttributes(typeof(AssertNamespaceBindingAttribute), true);

                NamespaceFormatExtension namespaceFormatExtension = new NamespaceFormatExtension();
                namespaceFormatExtension.Namespaces = new AssertNamespaceBindingAttribute[namespaceAtts.Length];
                namespaceAtts.CopyTo(namespaceFormatExtension.Namespaces, 0);
                reflector.Binding.Extensions.Add(namespaceFormatExtension);
            }

            // populate with assertions and namespaces
            if (validationAttribute != null)
            {
                validationFormatExtension.SchemaValidation = ((ValidationAttribute) validationAttribute).SchemaValidation;

                // retrieve assertions
                object[] classRuleAtts = reflector.Method.DeclaringType.GetCustomAttributes(typeof(AssertAttribute), true);
                object[] methodRuleAtts = reflector.Method.GetCustomAttributes(typeof(AssertAttribute));

                // retrieve namespace bindings
                object[] namespaceAtts =
                    reflector.Method.DeclaringType.GetCustomAttributes(typeof(AssertNamespaceBindingAttribute), true);

                validationFormatExtension.Assertions = new AssertAttribute[methodRuleAtts.Length + classRuleAtts.Length];
                methodRuleAtts.CopyTo(validationFormatExtension.Assertions, 0);
                classRuleAtts.CopyTo(validationFormatExtension.Assertions, methodRuleAtts.Length);

                foreach (AssertNamespaceBindingAttribute bindingAttribute in namespaceAtts)
                    validationFormatExtension.SerializerNamespaces.Add(bindingAttribute.Prefix, bindingAttribute.Namespace);
            }
            // inject extension element in the binding/input element
            reflector.OperationBinding.Input.Extensions.Add(validationFormatExtension);
        }
    }
}