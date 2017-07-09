#region Using Directives

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

#endregion

namespace Loom.Web.Portal.Controllers
{
    internal static class ControllerInitializer
    {
        private const string ActionExecuterMethodPrefix = "controllerActionExecute_";

        public static Dictionary<string, ControllerActionData> CreateActionDataList(Type type)
        {
            Dictionary<string, ControllerActionData> actionDataList = new Dictionary<string, ControllerActionData>();

            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!method.ReturnType.IsAssignableFrom(typeof(ActionResult)))
                    continue;

                object[] actionNameAttributes = method.GetCustomAttributes(typeof(ActionNameAttribute), false);
                string name = actionNameAttributes.Length > 0
                    ? ((ActionNameAttribute) actionNameAttributes[0]).Name.ToUpper()
                    : method.Name.ToUpper();

                actionDataList.Add(name, CreateActionDataObject(method));
            }

            return actionDataList;
        }

        private static ControllerActionData CreateActionDataObject(MethodInfo method)
        {
            ControllerActionData actionData = new ControllerActionData();
            CheckSecureAction(method, actionData);
            CheckAntiForgeryAction(method, actionData);
            CheckActionFilters(method, actionData);

            DynamicMethod dm = new DynamicMethod(ActionExecuterMethodPrefix + method.Name,
                MethodAttributes.Static | MethodAttributes.Public,
                CallingConventions.Standard,
                typeof(ActionResult),
                new[] {typeof(object), typeof(List<object>)},
                method.DeclaringType, false);

            ParameterInfo[] parameters = method.GetParameters();
            MethodInfo getItem = typeof(List<object>).GetMethod("get_Item", new[] {typeof(int)});

            ILGenerator il = dm.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Castclass, method.DeclaringType);

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameter = parameters[i];

                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Ldc_I4, i);
                il.EmitCall(OpCodes.Callvirt, getItem, null);

                actionData.Parameters.Add(new ActionParameter(parameter.Name, parameter.ParameterType));
            }

            il.EmitCall(OpCodes.Callvirt, method, null);
            il.Emit(OpCodes.Ret);

            actionData.RegisterMethodDelegate((ActionExecuter) dm.CreateDelegate(typeof(ActionExecuter)));
            return actionData;
        }

        private static void CheckActionFilters(ICustomAttributeProvider method, ControllerActionData actionData)
        {
            object[] filterAttributes = method.GetCustomAttributes(typeof(FilterAttribute), false);
            if (filterAttributes.Length == 0)
                return;

            for (int index = 0; index < filterAttributes.Length; index++)
            {
                FilterAttribute attribute = (FilterAttribute) filterAttributes[index];
                actionData.Filters.Add(attribute);
            }
        }

        private static void CheckSecureAction(ICustomAttributeProvider method, ControllerActionData actionData)
        {
            object[] dataActionAttributes = method.GetCustomAttributes(typeof(SecureActionAttribute), false);
            if (dataActionAttributes.Length == 0)
                return;

            SecureActionAttribute attribute = (SecureActionAttribute) dataActionAttributes[0];
            actionData.AntiForgery = actionData.Secure = true;
            actionData.AllowRouteTokens = attribute.AllowRouteTokens;
            actionData.AntiForgerySalt = attribute.AntiForgerySalt;
        }

        private static void CheckAntiForgeryAction(ICustomAttributeProvider method, ControllerActionData actionData)
        {
            if (actionData.Secure)
                return;

            object[] dataActionAttributes = method.GetCustomAttributes(typeof(AntiForgeryAttribute), false);
            if (dataActionAttributes.Length == 0)
                return;

            actionData.AntiForgery = true;
            actionData.AntiForgerySalt = ((AntiForgeryAttribute) dataActionAttributes[0]).Salt;
        }
    }
}