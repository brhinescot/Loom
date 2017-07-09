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

namespace Loom.Data.Mapping.CodeGeneration
{
    internal sealed class ReadWriteProcessorState : IClassProcessorState
    {
        public string ClassTemplate
        {
            get { return Templates.ActiveRecordClass; }
        }

        public string PropertyTemplate
        {
            get { return Templates.Property; }
        }

        public string EnumPropertyTemplate
        {
            get { return Templates.EnumProperty; }
        }
    }
}
