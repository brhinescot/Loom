﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Colossus.Framework.Data {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SqlScript {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SqlScript() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Loom.Data.Mapping.Resources.SqlScript", typeof(SqlScript).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT
        ///	USAGE.TABLE_SCHEMA AS TableOwner,
        ///	USAGE.TABLE_NAME AS TableName,
        ///	USAGE.COLUMN_NAME AS ColumnName, 	
        ///	CON.CONSTRAINT_TYPE AS ConstraintType	
        ///FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE USAGE
        ///JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS CON
        ///ON USAGE.CONSTRAINT_NAME = CON.CONSTRAINT_NAME
        ///ORDER BY USAGE.TABLE_SCHEMA, USAGE.TABLE_NAME.
        /// </summary>
        internal static string SqlConstraints {
            get {
                return ResourceManager.GetString("SqlConstraints", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///	COL.TABLE_SCHEMA AS TableOwner,
        ///	COL.TABLE_NAME AS TableName,
        ///	COL.COLUMN_NAME AS ColumnName, 	
        ///	COLUSE.TABLE_SCHEMA AS ForeignTableOwner,
        ///	COLUSE.TABLE_NAME AS ForeignTableName,
        ///	COLUSE.COLUMN_NAME AS ForeignColumnName
        ///FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE COL
        ///JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS CON
        ///ON COL.CONSTRAINT_NAME = CON.CONSTRAINT_NAME
        ///JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS REF
        ///ON REF.CONSTRAINT_NAME = CON.CONSTRAINT_NAME
        ///JOIN INFORMATION_SCHEMA.CONSTRAINT_COL [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SqlForeignKeys {
            get {
                return ResourceManager.GetString("SqlForeignKeys", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT
        ///	ISP.SPECIFIC_SCHEMA AS Owner, 
        ///	ISP.SPECIFIC_NAME AS ProcedureName, 
        ///	REPLACE(ISP.PARAMETER_NAME, &apos;@&apos;, &apos;&apos;) AS ParameterName,
        ///	ISP.ORDINAL_POSITION AS OrdinalPosition, 
        ///	ISP.PARAMETER_MODE AS ParamType, 
        ///	ISP.IS_RESULT AS IsResult, 
        ///	ISP.DATA_TYPE AS DataType,  
        ///	ISP.CHARACTER_MAXIMUM_LENGTH AS MaxLength
        ///FROM INFORMATION_SCHEMA.PARAMETERS ISP
        ///JOIN INFORMATION_SCHEMA.ROUTINES ISR ON 
        ///	ISP.SPECIFIC_SCHEMA = ISR.SPECIFIC_SCHEMA AND
        ///	ISP.SPECIFIC_NAME = ISR.SPECIFIC_NAME
        ///WHERE ISR.ROUTINE_TY [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SqlSPParameters {
            get {
                return ResourceManager.GetString("SqlSPParameters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT 
        ///	COL.TABLE_SCHEMA AS TableOwner,
        ///	COL.TABLE_NAME AS TableName,
        ///	TBL.TABLE_TYPE AS TableType,
        ///	COL.COLUMN_NAME AS ColumnName, 
        ///	COL.IS_NULLABLE AS IsNullable, 
        ///	COL.DATA_TYPE AS DataType, 
        ///	COL.CHARACTER_MAXIMUM_LENGTH AS MaxLength, 
        ///	COLUMNPROPERTY(object_id(COL.TABLE_SCHEMA + &apos;.&apos; + COL.TABLE_NAME), COL.COLUMN_NAME, &apos;IsComputed&apos;) as IsComputed,
        ///	COLUMNPROPERTY(object_id(COL.TABLE_SCHEMA + &apos;.&apos; + COL.TABLE_NAME), COL.COLUMN_NAME, &apos;IsIdentity&apos;) as IsIdentity,
        ///	COL.ORDINAL_POSITION AS Ordinal, [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SqlTableColumns {
            get {
                return ResourceManager.GetString("SqlTableColumns", resourceCulture);
            }
        }
    }
}
