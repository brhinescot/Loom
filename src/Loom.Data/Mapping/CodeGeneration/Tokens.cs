#region License information

/******************************************************************
 * Copyright © 2004 Brian Scott (DevInterop)
 * All Rights Reserved
 * 
 * Unauthorized reproduction or distribution in source or compiled
 * form is strictly prohibited.
 * 
 * http://www.devinterop.com
 * 
 * ****************************************************************/

#endregion

#region Using Directives

using System.Runtime.InteropServices;

#endregion

namespace Loom.Data.Mapping.CodeGeneration
{
    [StructLayout(LayoutKind.Auto)]
    internal struct Tokens
    {
        internal const string ClassName = "#CLASSNAME#";
        internal const string Namespace = "#NAMESPACE#";
        internal const string NamespaceList = "#NAMESPACELIST#";
        internal const string ProcedureName = "#PROCEDURENAME#";
        internal const string SchemaName = "#SCHEMA#";
        internal const string Properties = "#PROPERTIES#";
        internal const string XmlAttribute = "#XMLATTRIBUTE#";
        internal const string EnumType = "#ENUMTYPE#";
        internal const string TableName = "#TABLE#";
        internal const string PropertyName = "#PROPERTYNAME#";
        internal const string ColumnName = "#COLUMN#";
        internal const string DataTypeShort = "#DATATYPESHORT#";
        internal const string DataTypeLong = "#DATATYPELONG#";
        internal const string EnumValues = "#ENUMVALUES#";
        internal const string Summary = "#SUMMARY#";
        internal const string ParameterList = "#PARAMETERLIST#";
        internal const string PrimaryKeyColumn = "#PRIMARYKEYCOLUMN#";
        internal const string SchemaAttributes = "#SCHEMAATTRIBUTES#";
        internal const string ColumnList = "#COLUMNLIST#";
        internal const string AdditionalAttributes = "#ADDITIONALATTRIBUTES#";
        internal const string TargetNamespace = "#TARGETNAMESPACE#";
        internal const string MemberFields = "#MEMBERFIELDS#";
        internal const string AllowNillable = "#ALLOWNILLABLE#";
        internal const string FieldName = "#FIELDNAME#";
        internal const string ProviderName = "#PROVIDERNAME#";
    }
}
