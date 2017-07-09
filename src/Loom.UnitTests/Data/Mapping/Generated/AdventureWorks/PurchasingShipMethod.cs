#region Using Directives

using Loom;

#endregion

namespace AdventureWorks.Purchasing
{
    /// <summary>
    ///     This is an enum which wraps the Purchasing.ShipMethod table.
    /// </summary>
    public enum ShipMethod
    {
        /// <summary>
        ///     XRQ - TRUCK GROUND
        /// </summary>
        [EnumDescription("XRQ - TRUCK GROUND")] XRQTRUCKGROUND = 1,

        /// <summary>
        ///     ZY - EXPRESS
        /// </summary>
        [EnumDescription("ZY - EXPRESS")] ZYEXPRESS = 2,

        /// <summary>
        ///     OVERSEAS - DELUXE
        /// </summary>
        [EnumDescription("OVERSEAS - DELUXE")] OVERSEASDELUXE = 3,

        /// <summary>
        ///     OVERNIGHT J-FAST
        /// </summary>
        [EnumDescription("OVERNIGHT J-FAST")] OVERNIGHTJFAST = 4,

        /// <summary>
        ///     CARGO TRANSPORT 5
        /// </summary>
        [EnumDescription("CARGO TRANSPORT 5")] CARGOTRANSPORT5 = 5
    }
}