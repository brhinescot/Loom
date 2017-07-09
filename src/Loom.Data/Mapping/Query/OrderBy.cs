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

using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping.Query
{
    public sealed class OrderBy
    {
        #region Member Fields

        private readonly IQueryableColumn column;
        private OrderByDirection direction;

        #endregion

        #region Internal Property Accessors

        internal IQueryableColumn Column
        {
            get { return column; }
        }

        internal OrderByDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        #endregion

        #region .ctor

        internal OrderBy(IQueryableColumn column, OrderByDirection direction)
        {
            Argument.Assert.IsNotNull(column, Argument.Names.column);
            
            this.column = column;
            this.direction = direction;
        }

        #endregion
    }
}
