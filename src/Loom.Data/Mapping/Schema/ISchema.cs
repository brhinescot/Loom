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

namespace Loom.Data.Mapping.Schema
{
    public interface ISchema
    {
        string Owner { get; }
        string Name { get; }
        string Datasource { get; }
    }
}
