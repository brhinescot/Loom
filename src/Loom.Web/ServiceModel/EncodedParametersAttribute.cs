#region Using Directives

using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Security.Application;

#endregion

namespace Loom.Web.ServiceModel
{
    /// <summary>
    ///     Indicates that a method's <see cref="string" /> parameters should be HTML encoded.
    /// </summary>
    public sealed class EncodedParametersAttribute : Attribute, IOperationBehavior, IParameterInspector
    {
        private readonly int[] parameterIndexes;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EncodedParametersAttribute" /> class.
        /// </summary>
        /// <param name="parameterIndexes">The zero based indexes of parameters to encode.</param>
        public EncodedParametersAttribute(params int[] parameterIndexes)
        {
            this.parameterIndexes = parameterIndexes;
        }

        #region IOperationBehavior Members

        void IOperationBehavior.Validate(OperationDescription operationDescription) { }

        void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(this);
        }

        void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation) { }

        void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters) { }

        #endregion

        #region IParameterInspector Members

        object IParameterInspector.BeforeCall(string operationName, object[] inputs)
        {
            if (inputs == null)
                return null;

            bool noParameterIndexesSpecified = parameterIndexes == null || parameterIndexes.Length == 0;

            if (noParameterIndexesSpecified && inputs.Length == 1)
            {
                EncodeParameter(0, inputs);
                return null;
            }
            if (noParameterIndexesSpecified)
            {
                for (int i = 0; i < inputs.Length; i++)
                    EncodeParameter(i, inputs);

                return null;
            }

            for (int i = 0; i < parameterIndexes.Length; i++)
                EncodeParameter(parameterIndexes[i], inputs);

            // No state data needs to be returned.
            return null;
        }

        void IParameterInspector.AfterCall(string operationName, object[] outputs, object returnValue, object correlationState) { }

        #endregion

        private static void EncodeParameter(int i, IList<object> inputs)
        {
            if (i < 0 || i >= inputs.Count)
                throw new IndexOutOfRangeException("EncodedParameters attribute index of " + i + " is out of range.");

            string s = inputs[i] as string;
            if (s == null)
                return;

            inputs[i] = AntiXss.HtmlEncode(s);
        }
    }
}