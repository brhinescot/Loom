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

using System;

namespace Loom.Data.Mapping.CodeGeneration
{
    [Flags]
    public enum CodeFormatOptions
    {
        None = 0,
        RemoveFKPrefix = 1,
        RemoveTblPrefix = 2,
        AddIdSuffixForKeys = 4
    }
}
