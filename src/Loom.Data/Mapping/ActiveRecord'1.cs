#region File Header

// *************************************************************************
// Copyright © 2008 Colossus Interactive, LLC
// All Rights Reserved
//    
// Unauthorized reproduction or distribution in source or compiled
// form is strictly prohibited.
//  
// http://www.colossusinteractive.com
// licensing@colossusinteractive.com
//  
// *************************************************************************

#endregion

namespace Loom.Data.Mapping
{
    public abstract class ActiveRecord<TActiveRecord> : DataRecord<TActiveRecord>
        where TActiveRecord : DataRecord<TActiveRecord>, new() {}
}
