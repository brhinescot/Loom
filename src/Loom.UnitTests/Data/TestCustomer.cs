#region Using Directives

using System;

#endregion

namespace Loom.Data
{
    public class TestCustomer
    {
        public int CustomerId { get; set; }
        public int TerritoryId { get; set; }
        public string AccountNumber { get; set; }
        public string CustomerType { get; set; }
        public Guid Rowguid { get; set; }
    }
}