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
    public interface ICallable : ISchema
    {
        ICallableParameterCollection Parameters { get; }
    }
}
