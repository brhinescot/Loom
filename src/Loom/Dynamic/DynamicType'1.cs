#region Using Directives

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

#endregion

namespace Loom.Dynamic
{
    /// <summary>
    ///     Represents a class for creating dynamic method delegates for use in reflection scenarios.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The delegate that is created can be cached for working with multiple objects.
    ///         Caching and calling the delegate is orders of magnitude faster than using reflection.
    ///     </para>
    /// </remarks>
    /// <typeparam name="TObjectType"></typeparam>
    public static class DynamicType<TObjectType>
    {
        public static Func<object> CreateTypeConstructor()
        {
            Type type = typeof(TObjectType);

            ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
            if (ctor == null)
                throw new DynamicTypeInitializationException("The type '" + type.FullName + "' does not have an default parameterless constructor.");

            DynamicMethod method = new DynamicMethod("CreateIntance", type, null);

            ILGenerator il = method.GetILGenerator();
            il.Emit(OpCodes.Newobj, ctor);
            il.Emit(OpCodes.Ret);

            return (Func<object>) method.CreateDelegate(typeof(Func<object>));
        }

        public static DynamicProperty<TObjectType>[] CreateDynamicProperties(bool decoratedOnly = false)
        {
            Type type = typeof(TObjectType);
            PropertyInfo[] pi = type.GetProperties();
            List<DynamicProperty<TObjectType>> dynamicProperties = new List<DynamicProperty<TObjectType>>(pi.Length);

            for (int i = 0; i < pi.Length; i++)
            {
                string attributeName;
                DynamicPropertyAttribute attr = (DynamicPropertyAttribute) Attribute.GetCustomAttribute(pi[i], typeof(DynamicPropertyAttribute), true);
                if (attr == null && decoratedOnly)
                    continue;

                if (attr != null && !Compare.IsNullOrEmpty(attr.Name))
                    attributeName = attr.Name;
                else
                    attributeName = pi[i].Name;

                dynamicProperties.Add(new DynamicProperty<TObjectType>(
                    CreatePropertySetterAsObject(type, attributeName, pi[i]),
                    CreatePropertyGetterAsObject(type, attributeName, pi[i]),
                    pi[i].Name, attributeName, pi[i].PropertyType));
            }

            return dynamicProperties.ToArray();
        }

        public static DynamicProperty<TObjectType> CreateDynamicProperty(string propertyName)
        {
            Argument.Assert.IsNotNullOrEmpty(propertyName, nameof(propertyName));

            Type type = typeof(TObjectType);
            PropertyInfo pi = type.GetProperty(propertyName);

            string alternateName;
            DynamicPropertyAttribute attr = (DynamicPropertyAttribute) Attribute.GetCustomAttribute(pi, typeof(DynamicPropertyAttribute), true);
            if (attr != null && !Compare.IsNullOrEmpty(attr.Name))
                alternateName = attr.Name;
            else
                alternateName = pi.Name;

            DynamicProperty<TObjectType> property = new DynamicProperty<TObjectType>(
                CreatePropertySetterAsObject(type, alternateName, pi),
                CreatePropertyGetterAsObject(type, alternateName, pi),
                pi.Name, alternateName, pi.PropertyType);

            return property;
        }

        public static PropertySetter<TObjectType, TPropertyType> CreatePropertySetter<TPropertyType>(string propertyName)
        {
            Argument.Assert.IsNotNull(propertyName, nameof(propertyName));

            PropertySetter<TObjectType, TPropertyType> propertySetter;
            Type objectType = typeof(TObjectType);

            PropertyInfo pi;
            FieldInfo fi;

            if ((pi = objectType.GetProperty(propertyName)) != null)
                propertySetter = CreatePropertySetterDelegate<TPropertyType>(objectType, propertyName, pi);
            else if ((fi = objectType.GetField(propertyName)) != null)
                propertySetter = CreateDynamicFieldSetterDelegate<TPropertyType>(objectType, propertyName, fi);
            else
                throw new ArgumentException(string.Format("Member '{0}' is not a public property or field of type '{1}'", propertyName, objectType.Name));

            return propertySetter;
        }

        public static PropertySetter<TObjectType, object> CreatePropertySetterAsObject(string propertyName)
        {
            Argument.Assert.IsNotNull(propertyName, nameof(propertyName));

            PropertySetter<TObjectType, object> propertySetter;
            Type objectType = typeof(TObjectType);

            PropertyInfo pi;
            FieldInfo fi;

            if ((pi = objectType.GetProperty(propertyName)) != null)
                propertySetter = CreatePropertySetterAsObject(objectType, propertyName, pi);
            else if ((fi = objectType.GetField(propertyName)) != null)
                propertySetter = CreateFieldSetterAsObject(objectType, propertyName, fi);
            else
                throw new ArgumentException(string.Format("Member '{0}' is not a public property or field of type '{1}'", propertyName, objectType.Name));

            return propertySetter;
        }

        public static bool TryCreatePropertySetterAsObject(string propertyName, out PropertySetter<TObjectType, object> propertySetter)
        {
            return TryCreatePropertySetterAsObject(propertyName, out propertySetter, false);
        }

        public static bool TryCreatePropertySetterAsObject(string propertyName, out PropertySetter<TObjectType, object> propertySetter, bool ignoreCase)
        {
            Argument.Assert.IsNotNull(propertyName, nameof(propertyName));

            propertySetter = null;
            Type objectType = typeof(TObjectType);

            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            if (ignoreCase)
                flags |= BindingFlags.IgnoreCase;

            PropertyInfo pi;
            FieldInfo fi;

            if ((pi = objectType.GetProperty(propertyName, flags)) != null)
                propertySetter = CreatePropertySetterAsObject(objectType, propertyName, pi);
            else if ((fi = objectType.GetField(propertyName, flags)) != null)
                propertySetter = CreateFieldSetterAsObject(objectType, propertyName, fi);
            else
                return false;

            return true;
        }

        private static PropertySetter<TObjectType, TPropertyType> CreateDynamicFieldSetterDelegate<TPropertyType>(Type objectType, string fieldName, FieldInfo fi)
        {
            DynamicMethod dm = CreateSetterMethodSignature<TPropertyType>(fieldName, objectType);
            ILGenerator il = dm.GetILGenerator();
            EmitFieldSetter(il, fi);

            return (PropertySetter<TObjectType, TPropertyType>) dm.CreateDelegate(typeof(PropertySetter<TObjectType, TPropertyType>));
        }

        private static PropertySetter<TObjectType, TPropertyType> CreateDynamicPropertySetterDelegate<TPropertyType>(Type objectType, string propertyName, PropertyInfo pi)
        {
            DynamicMethod dm = CreateSetterMethodSignature<TPropertyType>(propertyName, objectType);
            ILGenerator il = dm.GetILGenerator();
            EmitPropertySetter(il, pi);

            return (PropertySetter<TObjectType, TPropertyType>) dm.CreateDelegate(typeof(PropertySetter<TObjectType, TPropertyType>));
        }

        private static PropertySetter<TObjectType, object> CreateFieldSetterAsObject(Type objectType, string fieldName, FieldInfo fi)
        {
            DynamicMethod dm = CreateSetterMethodSignature<object>(fieldName, objectType);
            ILGenerator il = dm.GetILGenerator();
            EmitFieldSetterAsObject(il, fi);

            return (PropertySetter<TObjectType, object>) dm.CreateDelegate(typeof(PropertySetter<TObjectType, object>));
        }

        private static PropertySetter<TObjectType, object> CreatePropertySetterAsObject(Type objectType, string propertyName, PropertyInfo pi)
        {
            if (pi.GetSetMethod() == null)
                return null;

            DynamicMethod dm = CreateSetterMethodSignature<object>(propertyName, objectType);
            ILGenerator il = dm.GetILGenerator();
            EmitPropertySetterAsObject(il, pi);

            return (PropertySetter<TObjectType, object>) dm.CreateDelegate(typeof(PropertySetter<TObjectType, object>));
        }

        private static PropertySetter<TObjectType, TPropertyType> CreatePropertySetterDelegate<TPropertyType>(Type objectType, string propertyName, PropertyInfo pi)
        {
            if (pi.ReflectedType != null && pi.ReflectedType.IsValueType)
                return CreateDynamicPropertySetterDelegate<TPropertyType>(objectType, propertyName, pi);

            MethodInfo mi = pi.GetSetMethod();
            if (mi == null)
                throw new ArgumentException(string.Format("Property '{0}' of type '{1}' does not have a public set accessor", propertyName, objectType.Name));

            return (PropertySetter<TObjectType, TPropertyType>) Delegate.CreateDelegate(typeof(PropertySetter<TObjectType, TPropertyType>), mi);
        }

        private static DynamicMethod CreateSetterMethodSignature<TPropertyType>(string name, Type objectType)
        {
            const MethodAttributes methodAttributes = MethodAttributes.Static | MethodAttributes.Public;
            return new DynamicMethod("set_" + name, methodAttributes, CallingConventions.Standard,
                null, new[] {objectType, typeof(TPropertyType)}, objectType, false);
        }

        public static PropertyGetter<TPropertyType> CreateStaticPropertyGetter<TPropertyType>(string propertyName)
        {
            Argument.Assert.IsNotNull(propertyName, nameof(propertyName));

            PropertyGetter<TPropertyType> propertyGetter;
            Type objectType = typeof(TObjectType);

            PropertyInfo pi;

            if ((pi = objectType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)) != null)
                propertyGetter = CreateStaticPropertyDelegate<TPropertyType>(objectType, propertyName, pi);
            else
                throw new ArgumentException(string.Format("Member '{0}' is not a public static property '{1}'", propertyName, objectType.Name));

            return propertyGetter;
        }

        /// <summary>
        ///     Creates a dynamic method delegate used to retrieve the value of a property or public field.
        /// </summary>
        /// <typeparam name="TPropertyType">The type of the property who's value is to be retrieved.</typeparam>
        /// <param name="propertyName">The name of the property who's value is to be retrieved.</param>
        /// <returns>
        ///     A <see cref="PropertyGetter{TObjectType,TPropertyType}" /> representing a dynamic method.
        /// </returns>
        public static PropertyGetter<TObjectType, TPropertyType> CreatePropertyGetter<TPropertyType>(string propertyName)
        {
            Argument.Assert.IsNotNull(propertyName, nameof(propertyName));

            PropertyGetter<TObjectType, TPropertyType> propertyGetter;
            Type objectType = typeof(TObjectType);

            PropertyInfo pi;
            FieldInfo fi;

            if ((pi = objectType.GetProperty(propertyName)) != null)
                propertyGetter = CreatePropertyDelegate<TPropertyType>(objectType, propertyName, pi);
            else if ((fi = objectType.GetField(propertyName)) != null)
                propertyGetter = CreateDynamicFieldDelegate<TPropertyType>(objectType, propertyName, fi);
            else
                throw new ArgumentException(string.Format("Member '{0}' is not a public property or field of type '{1}'", propertyName, objectType.Name));

            return propertyGetter;
        }

        /// <summary>
        ///     Creates a dynamic method delegate used to retrieve an object representation of the value of a property or public
        ///     field.
        /// </summary>
        /// <param name="propertyName">The name of the property who's value is to be retrieved.</param>
        /// <returns>
        ///     A <see cref="PropertyGetter{TObjectType,TPropertyType}" /> representing a dynamic method.
        /// </returns>
        public static PropertyGetter<TObjectType, object> CreatePropertyGetterAsObject(string propertyName)
        {
            Argument.Assert.IsNotNull(propertyName, nameof(propertyName));

            PropertyGetter<TObjectType, object> propertyGetter;
            Type objectType = typeof(TObjectType);

            PropertyInfo pi;
            FieldInfo fi;

            if ((pi = objectType.GetProperty(propertyName)) != null)
                propertyGetter = CreatePropertyGetterAsObject(objectType, propertyName, pi);
            else if ((fi = objectType.GetField(propertyName)) != null)
                propertyGetter = CreateDynamicFieldDelegateAsObject(objectType, propertyName, fi);
            else
                throw new ArgumentException(string.Format("Member '{0}' is not a public property or field of type '{1}'", propertyName, objectType.Name));

            return propertyGetter;
        }

        public static PropertyGetter<TObjectType, object>[] CreateAllPropertyGetters()
        {
            Type type = typeof(TObjectType);
            PropertyInfo[] pi = type.GetProperties();
            PropertyGetter<TObjectType, object>[] getters = new PropertyGetter<TObjectType, object>[pi.Length];

            for (int i = 0; i < pi.Length; i++)
                getters[i] = CreatePropertyGetterAsObject(type, pi[i].Name, pi[i]);

            return getters;
        }

        /// <summary>
        ///     Creates a dynamic method delegate used to retrieve the formatted string value of a property or public field.
        /// </summary>
        /// <param name="propertyName">The name of the property who's value is to be retrieved.</param>
        /// <returns>
        ///     A <see cref="FormattablePropertyGetter{TObjectType}" /> representing a dynamic method.
        /// </returns>
        public static FormattablePropertyGetter<TObjectType> CreateFormattablePropertyGetter(string propertyName)
        {
            Argument.Assert.IsNotNull(propertyName, nameof(propertyName));

            FormattablePropertyGetter<TObjectType> propertyGetter;
            Type objectType = typeof(TObjectType);

            PropertyInfo pi;
            FieldInfo fi;

            if ((pi = objectType.GetProperty(propertyName)) != null)
                propertyGetter = CreateDynamicPropertyDelegateAsString(objectType, propertyName, pi);
            else if ((fi = objectType.GetField(propertyName)) != null)
                propertyGetter = CreateDynamicFieldDelegateAsString(objectType, propertyName, fi);
            else
                throw new ArgumentException(string.Format("Member '{0}' is not a public property or field of type '{1}'", propertyName, objectType.Name));

            return propertyGetter;
        }

        /// <summary>
        ///     Creates a dynamic method delegate used to retrieve the hash code of a property or public field.
        /// </summary>
        /// <param name="propertyName">The name of the property who's value is to be retrieved.</param>
        /// <returns>
        ///     A <see cref="PropertyGetter{TObjectType,TPropertyType}" /> representing a dynamic method.
        /// </returns>
        public static PropertyGetter<TObjectType, int> CreatePropertyGetterGetHashCode(string propertyName)
        {
            Argument.Assert.IsNotNull(propertyName, nameof(propertyName));

            PropertyGetter<TObjectType, int> propertyGetter;
            Type objectType = typeof(TObjectType);

            PropertyInfo pi;
            FieldInfo fi;

            if ((pi = objectType.GetProperty(propertyName)) != null)
                propertyGetter = CreateDynamicPropertyDelegateGetHashCode(objectType, propertyName, pi);
            else if ((fi = objectType.GetField(propertyName)) != null)
                propertyGetter = CreateDynamicFieldDelegateGetHashCode(objectType, propertyName, fi);
            else
                throw new ArgumentException(string.Format("Member '{0}' is not a public property or field of type '{1}'", propertyName, objectType.Name));

            return propertyGetter;
        }

        private static PropertyGetter<TObjectType, TPropertyType> CreateDynamicFieldDelegate<TPropertyType>(Type objectType, string fieldName, FieldInfo fi)
        {
            DynamicMethod dm = CreateMethodSignature<TPropertyType>(fieldName, objectType);
            ILGenerator il = dm.GetILGenerator();
            EmitField(il, fi);

            return (PropertyGetter<TObjectType, TPropertyType>) dm.CreateDelegate(typeof(PropertyGetter<TObjectType, TPropertyType>));
        }

        private static PropertyGetter<TObjectType, TPropertyType> CreateDynamicPropertyDelegate<TPropertyType>(Type objectType, string propertyName, PropertyInfo pi)
        {
            DynamicMethod dm = CreateMethodSignature<TPropertyType>(propertyName, objectType);
            ILGenerator il = dm.GetILGenerator();
            EmitProperty(il, pi);

            return (PropertyGetter<TObjectType, TPropertyType>) dm.CreateDelegate(typeof(PropertyGetter<TObjectType, TPropertyType>));
        }

        private static PropertyGetter<TObjectType, TPropertyType> CreatePropertyDelegate<TPropertyType>(Type objectType, string propertyName, PropertyInfo pi)
        {
            if (pi.ReflectedType != null && pi.ReflectedType.IsValueType)
                return CreateDynamicPropertyDelegate<TPropertyType>(objectType, propertyName, pi);

            MethodInfo mi = pi.GetGetMethod();
            if (mi == null)
                throw new ArgumentException(string.Format("Property '{0}' of type '{1}' does not have a public get accessor", propertyName, objectType.Name));

            return (PropertyGetter<TObjectType, TPropertyType>) Delegate.CreateDelegate(typeof(PropertyGetter<TObjectType, TPropertyType>), mi);
        }

        private static PropertyGetter<TPropertyType> CreateStaticPropertyDelegate<TPropertyType>(Type objectType, string propertyName, PropertyInfo pi)
        {
            MethodInfo mi = pi.GetGetMethod();
            if (mi == null)
                throw new ArgumentException(string.Format("Property '{0}' of type '{1}' does not have a public get accessor", propertyName, objectType.Name));

            return (PropertyGetter<TPropertyType>) Delegate.CreateDelegate(typeof(PropertyGetter<TPropertyType>), mi);
        }

        private static PropertyGetter<TObjectType, object> CreateDynamicFieldDelegateAsObject(Type objectType, string fieldName, FieldInfo fi)
        {
            DynamicMethod dm = CreateMethodSignature<object>(fieldName, objectType);
            ILGenerator il = dm.GetILGenerator();
            EmitFieldAsObject(il, fi);

            return (PropertyGetter<TObjectType, object>) dm.CreateDelegate(typeof(PropertyGetter<TObjectType, object>));
        }

        private static PropertyGetter<TObjectType, object> CreatePropertyGetterAsObject(Type objectType, string propertyName, PropertyInfo pi)
        {
            DynamicMethod dm = CreateMethodSignature<object>(propertyName, objectType);
            ILGenerator il = dm.GetILGenerator();
            EmitPropertyAsObject(il, pi);

            return (PropertyGetter<TObjectType, object>) dm.CreateDelegate(typeof(PropertyGetter<TObjectType, object>));
        }

        private static FormattablePropertyGetter<TObjectType> CreateDynamicFieldDelegateAsString(Type objectType, string fieldName, FieldInfo fi)
        {
            DynamicMethod dm = CreateFormattableMethodSignature<string>(fieldName, objectType);
            ILGenerator il = dm.GetILGenerator();
            EmitFieldAsString(il, fi);

            return (FormattablePropertyGetter<TObjectType>) dm.CreateDelegate(typeof(FormattablePropertyGetter<TObjectType>));
        }

        private static FormattablePropertyGetter<TObjectType> CreateDynamicPropertyDelegateAsString(Type objectType, string propertyName, PropertyInfo pi)
        {
            DynamicMethod dm = CreateFormattableMethodSignature<string>(propertyName, objectType);
            ILGenerator il = dm.GetILGenerator();
            EmitPropertyAsString(il, pi);

            return (FormattablePropertyGetter<TObjectType>) dm.CreateDelegate(typeof(FormattablePropertyGetter<TObjectType>));
        }

        private static PropertyGetter<TObjectType, int> CreateDynamicFieldDelegateGetHashCode(Type objectType, string fieldName, FieldInfo fi)
        {
            DynamicMethod dm = CreateMethodSignature<int>(fieldName, objectType);
            ILGenerator il = dm.GetILGenerator();
            EmitFieldGetHashCode(il, fi);

            return (PropertyGetter<TObjectType, int>) dm.CreateDelegate(typeof(PropertyGetter<TObjectType, int>));
        }

        private static PropertyGetter<TObjectType, int> CreateDynamicPropertyDelegateGetHashCode(Type objectType, string propertyName, PropertyInfo pi)
        {
            DynamicMethod dm = CreateMethodSignature<int>(propertyName, objectType);
            ILGenerator il = dm.GetILGenerator();
            EmitPropertyGetHashCode(il, pi);

            return (PropertyGetter<TObjectType, int>) dm.CreateDelegate(typeof(PropertyGetter<TObjectType, int>));
        }

        private static DynamicMethod CreateFormattableMethodSignature<TPropertyType>(string name, Type objectType)
        {
            const MethodAttributes methodAttributes = MethodAttributes.Static | MethodAttributes.Public;
            return new DynamicMethod("get_" + name, methodAttributes, CallingConventions.Standard,
                typeof(TPropertyType), new[] {objectType, typeof(string)}, objectType, false);
        }

        private static DynamicMethod CreateMethodSignature<TPropertyType>(string name, Type objectType)
        {
            const MethodAttributes methodAttributes = MethodAttributes.Static | MethodAttributes.Public;
            return new DynamicMethod("get_" + name, methodAttributes, CallingConventions.Standard,
                typeof(TPropertyType), new[] {objectType}, objectType, false);
        }

        public static Comparison<TObjectType> CreateComparison(string propertyName)
        {
            Type objectType = typeof(TObjectType);
            MethodInfo propertyGetterMethodInfo = null;
            Type propertyGetterReturnType;

            FieldInfo fieldInfo;
            PropertyInfo propertyInfo = objectType.GetProperty(propertyName);
            if (propertyInfo != null)
            {
                propertyGetterMethodInfo = propertyInfo.GetGetMethod();
                if (propertyGetterMethodInfo != null)
                    propertyGetterReturnType = propertyInfo.PropertyType;
                else
                    throw new InvalidOperationException(string.Format("Property '{0}' of type '{1}' does not have a public get accessor",
                        propertyName, objectType.Name));
            }
            else if ((fieldInfo = objectType.GetField(propertyName)) != null)
            {
                propertyGetterReturnType = fieldInfo.FieldType;
            }
            else
            {
                throw new InvalidOperationException(string.Format("'{0}' is not a public field or property with a get accessor for type: '{1}' ",
                    propertyName, objectType.Name));
            }

            DynamicMethod dynamicMethod = !objectType.IsValueType
                ? GenerateReferenceTypeComparer(objectType, propertyName, propertyGetterReturnType, propertyGetterMethodInfo)
                : GenerateValueTypeComparer(objectType, propertyName, propertyGetterReturnType, propertyGetterMethodInfo);

            return (Comparison<TObjectType>) dynamicMethod.CreateDelegate(typeof(Comparison<TObjectType>));
        }

        private static DynamicMethod GenerateNullableTypeComparer(Type objectType, string propertyName, Type propertyGetterReturnType, MethodInfo propertyGetterMethodInfo)
        {
            MethodInfo compareMethod = typeof(Nullable).GetMethod("Compare", BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(propertyGetterReturnType.GetGenericArguments()[0]);
            Type[] parameterTypes = {objectType, objectType};

            DynamicMethod dynamicMethod = new DynamicMethod("Compare_" + propertyName, typeof(int), parameterTypes);
            ILGenerator il = dynamicMethod.GetILGenerator();

            il.Emit(OpCodes.Ldarga_S, 0);
            il.EmitCall(OpCodes.Call, propertyGetterMethodInfo, null);

            il.Emit(OpCodes.Ldarga_S, 1);
            il.EmitCall(OpCodes.Call, propertyGetterMethodInfo, null);

            il.EmitCall(OpCodes.Call, compareMethod, null);
            il.Emit(OpCodes.Ret);

            return dynamicMethod;
        }

        private static DynamicMethod GenerateReferenceTypeComparer(Type objectType, string propertyName, Type propertyGetterReturnType, MethodInfo propertyGetterMethodInfo)
        {
            Type comparerType = typeof(Comparer<>).MakeGenericType(propertyGetterReturnType);
            MethodInfo defaultProperty = comparerType.GetProperty("Default").GetGetMethod();
            MethodInfo compareMethod = defaultProperty.ReturnType.GetMethod("Compare");

            DynamicMethod dynamicMethod = new DynamicMethod("Compare_" + propertyName, typeof(int), new[] {objectType, objectType});
            ILGenerator generator = dynamicMethod.GetILGenerator();

            Label falseLabel = generator.DefineLabel();
            Label trueLabel = generator.DefineLabel();

            // Return -1 if either param is null.
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Brfalse_S, falseLabel);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Brtrue_S, trueLabel);
            generator.MarkLabel(falseLabel);
            generator.Emit(OpCodes.Ldc_I4_M1);
            generator.Emit(OpCodes.Ret);

            generator.MarkLabel(trueLabel);

            generator.EmitCall(OpCodes.Call, defaultProperty, null);
            generator.Emit(OpCodes.Ldarg_0);
            if (propertyGetterMethodInfo != null)
                generator.EmitCall(OpCodes.Callvirt, propertyGetterMethodInfo, null);
            else
                generator.Emit(OpCodes.Ldfld);

            generator.Emit(OpCodes.Ldarg_1);
            if (propertyGetterMethodInfo != null)
                generator.EmitCall(OpCodes.Callvirt, propertyGetterMethodInfo, null);
            else
                generator.Emit(OpCodes.Ldfld);

            generator.EmitCall(OpCodes.Callvirt, compareMethod, null);

            generator.Emit(OpCodes.Ret);
            return dynamicMethod;
        }

        private static DynamicMethod GenerateValueTypeComparer(Type objectType, string propertyName, Type propertyGetterReturnType, MethodInfo propertyGetterMethodInfo)
        {
            if (propertyGetterReturnType.IsGenericType && propertyGetterReturnType.GetGenericTypeDefinition() == typeof(Nullable<>))
                return GenerateNullableTypeComparer(objectType, propertyName, propertyGetterReturnType, propertyGetterMethodInfo);

            MethodInfo compareMethod = propertyGetterReturnType.GetMethod("CompareTo", new[] {propertyGetterReturnType});

            DynamicMethod dynamicMethod = new DynamicMethod("Compare_" + propertyName, typeof(int), new[] {objectType, objectType});
            ILGenerator il = dynamicMethod.GetILGenerator();

            if (propertyGetterReturnType.IsValueType)
            {
                LocalBuilder local = il.DeclareLocal(propertyGetterReturnType);
                il.Emit(OpCodes.Ldarga_S, 0);
                il.EmitCall(OpCodes.Call, propertyGetterMethodInfo, null);
                il.Emit(OpCodes.Stloc_0);
                il.Emit(OpCodes.Ldloca_S, local);
                il.Emit(OpCodes.Ldarga_S, 1);
                il.EmitCall(OpCodes.Call, propertyGetterMethodInfo, null);
                il.EmitCall(OpCodes.Call, compareMethod, null);
                il.Emit(OpCodes.Ret);
            }
            else
            {
                Label bothNotNull = il.DefineLabel();
                Label firstNotNull = il.DefineLabel();

                il.Emit(OpCodes.Ldarga_S, 0);
                il.EmitCall(OpCodes.Call, propertyGetterMethodInfo, null);
                il.Emit(OpCodes.Brtrue_S, bothNotNull);
                il.Emit(OpCodes.Ldarga_S, 1);
                il.EmitCall(OpCodes.Call, propertyGetterMethodInfo, null);
                il.Emit(OpCodes.Brtrue_S, bothNotNull);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ret);

                il.MarkLabel(bothNotNull);
                il.Emit(OpCodes.Ldarga_S, 0);
                il.EmitCall(OpCodes.Call, propertyGetterMethodInfo, null);
                il.Emit(OpCodes.Brtrue_S, firstNotNull);
                il.Emit(OpCodes.Ldc_I4_M1);
                il.Emit(OpCodes.Ret);

                il.MarkLabel(firstNotNull);
                il.Emit(OpCodes.Ldarga_S, 0);
                il.EmitCall(OpCodes.Call, propertyGetterMethodInfo, null);
                il.Emit(OpCodes.Ldarga_S, 1);
                il.EmitCall(OpCodes.Call, propertyGetterMethodInfo, null);
                il.EmitCall(OpCodes.Callvirt, compareMethod, null);
                il.Emit(OpCodes.Ret);
            }
            return dynamicMethod;
        }

        private static void EmitField(ILGenerator il, FieldInfo fi)
        {
            Label exitLabel = il.DefineLabel();
            il.DeclareLocal(fi.FieldType);

            if (fi.ReflectedType != null && fi.ReflectedType.IsValueType)
                il.Emit(OpCodes.Ldarga_S, 0);
            else
                il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Ldfld, fi);

            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Br_S, exitLabel);
            il.MarkLabel(exitLabel);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
        }

        private static void EmitFieldAsObject(ILGenerator il, FieldInfo fi)
        {
            Label exitLabel = il.DefineLabel();
            il.DeclareLocal(typeof(object));

            if (fi.ReflectedType != null && fi.ReflectedType.IsValueType)
                il.Emit(OpCodes.Ldarga_S, 0);
            else
                il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Ldfld, fi);
            if (fi.FieldType.IsValueType)
                il.Emit(OpCodes.Box, fi.FieldType);

            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Br_S, exitLabel);
            il.MarkLabel(exitLabel);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
        }

        private static void EmitFieldAsString(ILGenerator il, FieldInfo fi)
        {
            Label exitLabel = il.DefineLabel();
            Label notNullLabel = il.DefineLabel();
            il.DeclareLocal(typeof(string));

            #region Test For Null

            if (!fi.FieldType.IsValueType)
            {
                il.DeclareLocal(typeof(bool));
                if (fi.ReflectedType != null && fi.ReflectedType.IsValueType)
                    il.Emit(OpCodes.Ldarga_S, 0);
                else
                    il.Emit(OpCodes.Ldarg_0);
                il.Emit(fi.FieldType.IsValueType ? OpCodes.Ldflda : OpCodes.Ldfld, fi);

                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Stloc_1);
                il.Emit(OpCodes.Ldloc_1);
                il.Emit(OpCodes.Brtrue_S, notNullLabel);
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Stloc_0);
                il.Emit(OpCodes.Br_S, exitLabel);
                il.MarkLabel(notNullLabel);
            }

            #endregion

            if (fi.ReflectedType != null && fi.ReflectedType.IsValueType)
                il.Emit(OpCodes.Ldarga_S, 0);
            else
                il.Emit(OpCodes.Ldarg_0);

            if (fi.FieldType != typeof(string))
            {
                bool useFormat = true;
                MethodInfo toStringMethod = fi.FieldType.GetMethod("ToString", new[] {typeof(string)});
                if (toStringMethod == null)
                {
                    useFormat = false;
                    toStringMethod = fi.FieldType.GetMethod("ToString", new Type[0]);
                }

                if (fi.FieldType.IsValueType)
                {
                    il.Emit(OpCodes.Ldflda, fi);
                    if (useFormat)
                        il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Call, toStringMethod);
                }
                else
                {
                    il.Emit(OpCodes.Ldfld, fi);
                    if (useFormat)
                        il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Callvirt, toStringMethod);
                }
            }
            else
            {
                il.Emit(OpCodes.Ldfld, fi);
            }
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Br_S, exitLabel);
            il.MarkLabel(exitLabel);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
        }

        private static void EmitFieldGetHashCode(ILGenerator il, FieldInfo fi)
        {
            Label exitLabel = il.DefineLabel();
            Label notNullLabel = il.DefineLabel();
            il.DeclareLocal(typeof(int));

            #region Test For Null

            if (!fi.FieldType.IsValueType)
            {
                il.DeclareLocal(typeof(bool));
                if (fi.ReflectedType != null && fi.ReflectedType.IsValueType)
                    il.Emit(OpCodes.Ldarga_S, 0);
                else
                    il.Emit(OpCodes.Ldarg_0);
                il.Emit(fi.FieldType.IsValueType ? OpCodes.Ldflda : OpCodes.Ldfld, fi);

                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Stloc_1);
                il.Emit(OpCodes.Ldloc_1);
                il.Emit(OpCodes.Brtrue_S, notNullLabel);
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Stloc_0);
                il.Emit(OpCodes.Br_S, exitLabel);
                il.MarkLabel(notNullLabel);
            }

            #endregion

            if (fi.ReflectedType != null && fi.ReflectedType.IsValueType)
                il.Emit(OpCodes.Ldarga_S, 0);
            else
                il.Emit(OpCodes.Ldarg_0);

            MethodInfo getHashCodeMethod = fi.FieldType.GetMethod("GetHashCode", new Type[0]);

            if (fi.FieldType.IsValueType)
            {
                il.Emit(OpCodes.Ldflda, fi);
                il.Emit(OpCodes.Call, getHashCodeMethod);
            }
            else
            {
                il.Emit(OpCodes.Ldfld, fi);
                il.Emit(OpCodes.Callvirt, getHashCodeMethod);
            }

            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Br_S, exitLabel);
            il.MarkLabel(exitLabel);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
        }

        private static void EmitFieldSetter(ILGenerator il, FieldInfo fi)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, fi);
            il.Emit(OpCodes.Ret);
        }

        private static void EmitFieldSetterAsObject(ILGenerator il, FieldInfo fi)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Unbox_Any, fi.FieldType);
            il.Emit(OpCodes.Stfld, fi);
            il.Emit(OpCodes.Ret);
        }

        private static void EmitProperty(ILGenerator il, PropertyInfo pi)
        {
            Label exitLabel = il.DefineLabel();
            il.DeclareLocal(pi.PropertyType);

            if (pi.ReflectedType != null && pi.ReflectedType.IsValueType)
                il.Emit(OpCodes.Ldarga_S, 0);
            else
                il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Callvirt, pi.GetGetMethod());
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Br_S, exitLabel);
            il.MarkLabel(exitLabel);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
        }

        private static void EmitPropertyAsObject(ILGenerator il, PropertyInfo pi)
        {
            Label exitLabel = il.DefineLabel();
            il.DeclareLocal(typeof(object));

            if (pi.ReflectedType != null && pi.ReflectedType.IsValueType)
                il.Emit(OpCodes.Ldarga_S, 0);
            else
                il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Callvirt, pi.GetGetMethod());
            if (pi.PropertyType.IsValueType)
                il.Emit(OpCodes.Box, pi.PropertyType);

            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Br_S, exitLabel);
            il.MarkLabel(exitLabel);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
        }

        private static void EmitPropertyAsString(ILGenerator il, PropertyInfo pi)
        {
            Label exitLabel = il.DefineLabel();
            Label notNullLabel = il.DefineLabel();
            il.DeclareLocal(typeof(string));
            il.DeclareLocal(pi.PropertyType);

            #region Test For Null

            if (!pi.PropertyType.IsValueType)
            {
                il.DeclareLocal(typeof(bool));
                if (pi.ReflectedType != null && pi.ReflectedType.IsValueType)
                    il.Emit(OpCodes.Ldarga_S, 0);
                else
                    il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Callvirt, pi.GetGetMethod());
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Stloc_2);
                il.Emit(OpCodes.Ldloc_2);
                il.Emit(OpCodes.Brtrue_S, notNullLabel);
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Stloc_0);
                il.Emit(OpCodes.Br_S, exitLabel);
                il.MarkLabel(notNullLabel);
            }

            #endregion

            if (pi.ReflectedType != null && pi.ReflectedType.IsValueType)
                il.Emit(OpCodes.Ldarga_S, 0);
            else
                il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Callvirt, pi.GetGetMethod());

            if (pi.PropertyType != typeof(string))
            {
                bool useFormat = true;
                MethodInfo toStringMethod = pi.PropertyType.GetMethod("ToString", new[] {typeof(string)});
                if (toStringMethod == null)
                {
                    useFormat = false;
                    toStringMethod = pi.PropertyType.GetMethod("ToString", new Type[0]);
                }

                if (pi.PropertyType.IsValueType)
                {
                    il.Emit(OpCodes.Stloc_1);
                    il.Emit(OpCodes.Ldloca_S, 1);
                    if (useFormat)
                        il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Call, toStringMethod);
                }
                else
                {
                    if (useFormat)
                        il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Callvirt, toStringMethod);
                }
            }

            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Br_S, exitLabel);
            il.MarkLabel(exitLabel);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
        }

        private static void EmitPropertyGetHashCode(ILGenerator il, PropertyInfo pi)
        {
            Label exitLabel = il.DefineLabel();
            Label notNullLabel = il.DefineLabel();
            il.DeclareLocal(typeof(int));
            il.DeclareLocal(pi.PropertyType);

            #region Test For Null

            if (!pi.PropertyType.IsValueType)
            {
                il.DeclareLocal(typeof(bool));
                if (pi.ReflectedType != null && pi.ReflectedType.IsValueType)
                    il.Emit(OpCodes.Ldarga_S, 0);
                else
                    il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Callvirt, pi.GetGetMethod());
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Stloc_2);
                il.Emit(OpCodes.Ldloc_2);
                il.Emit(OpCodes.Brtrue_S, notNullLabel);
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Stloc_0);
                il.Emit(OpCodes.Br_S, exitLabel);
                il.MarkLabel(notNullLabel);
            }

            #endregion

            if (pi.ReflectedType != null && pi.ReflectedType.IsValueType)
                il.Emit(OpCodes.Ldarga_S, 0);
            else
                il.Emit(OpCodes.Ldarg_0);

            il.Emit(OpCodes.Callvirt, pi.GetGetMethod());

            MethodInfo getHashCodeMethod = pi.PropertyType.GetMethod("GetHashCode", new Type[0]);
            if (pi.PropertyType.IsValueType)
            {
                il.Emit(OpCodes.Stloc_1);
                il.Emit(OpCodes.Ldloca_S, 1);
                il.Emit(OpCodes.Call, getHashCodeMethod);
            }
            else
            {
                il.Emit(OpCodes.Callvirt, getHashCodeMethod);
            }

            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Br_S, exitLabel);
            il.MarkLabel(exitLabel);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
        }

        private static void EmitPropertySetter(ILGenerator il, PropertyInfo pi)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Callvirt, pi.GetSetMethod());
            il.Emit(OpCodes.Ret);
        }

        private static void EmitPropertySetterAsObject(ILGenerator il, PropertyInfo pi)
        {
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            if (pi.PropertyType.IsEnum)
            {
                il.Emit(OpCodes.Ldtoken, pi.PropertyType);

                MethodInfo getTypeFromHandle = typeof(Type).GetMethod("GetTypeFromHandle", BindingFlags.Public | BindingFlags.Static, null, new[] {typeof(RuntimeTypeHandle)}, new ParameterModifier[0]);
                MethodInfo getUnderlyingType = typeof(Enum).GetMethod("GetUnderlyingType", BindingFlags.Public | BindingFlags.Static);
                MethodInfo changeType = typeof(Convert).GetMethod("ChangeType", BindingFlags.Public | BindingFlags.Static, null, new[] {typeof(object), typeof(Type)}, new ParameterModifier[0]);

                il.Emit(OpCodes.Call, getTypeFromHandle);
                il.Emit(OpCodes.Call, getUnderlyingType);
                il.Emit(OpCodes.Call, changeType);
            }
            il.Emit(OpCodes.Unbox_Any, pi.PropertyType);
            il.Emit(OpCodes.Callvirt, pi.GetSetMethod());
            il.Emit(OpCodes.Ret);
        }
    }
}