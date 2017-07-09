#region Using Directives

using Loom;

#endregion

namespace AdventureWorks.Production
{
    /// <summary>
    ///     This is an enum which wraps the Production.ScrapReason table.
    /// </summary>
    public enum ScrapReason
    {
        /// <summary>
        ///     Brake assembly not as ordered
        /// </summary>
        [EnumDescription("Brake assembly not as ordered")] BrakeAssemblyNotAsOrdered = 1,

        /// <summary>
        ///     Color incorrect
        /// </summary>
        [EnumDescription("Color incorrect")] ColorIncorrect = 2,

        /// <summary>
        ///     Gouge in metal
        /// </summary>
        [EnumDescription("Gouge in metal")] GougeInMetal = 3,

        /// <summary>
        ///     Drill pattern incorrect
        /// </summary>
        [EnumDescription("Drill pattern incorrect")] DrillPatternIncorrect = 4,

        /// <summary>
        ///     Drill size too large
        /// </summary>
        [EnumDescription("Drill size too large")] DrillSizeTooLarge = 5,

        /// <summary>
        ///     Drill size too small
        /// </summary>
        [EnumDescription("Drill size too small")] DrillSizeTooSmall = 6,

        /// <summary>
        ///     Handling damage
        /// </summary>
        [EnumDescription("Handling damage")] HandlingDamage = 7,

        /// <summary>
        ///     Paint process failed
        /// </summary>
        [EnumDescription("Paint process failed")] PaintProcessFailed = 8,

        /// <summary>
        ///     Primer process failed
        /// </summary>
        [EnumDescription("Primer process failed")] PrimerProcessFailed = 9,

        /// <summary>
        ///     Seat assembly not as ordered
        /// </summary>
        [EnumDescription("Seat assembly not as ordered")] SeatAssemblyNotAsOrdered = 10,

        /// <summary>
        ///     Stress test failed
        /// </summary>
        [EnumDescription("Stress test failed")] StressTestFailed = 11,

        /// <summary>
        ///     Thermoform temperature too high
        /// </summary>
        [EnumDescription("Thermoform temperature too high")] ThermoformTemperatureTooHigh = 12,

        /// <summary>
        ///     Thermoform temperature too low
        /// </summary>
        [EnumDescription("Thermoform temperature too low")] ThermoformTemperatureTooLow = 13,

        /// <summary>
        ///     Trim length too long
        /// </summary>
        [EnumDescription("Trim length too long")] TrimLengthTooLong = 14,

        /// <summary>
        ///     Trim length too short
        /// </summary>
        [EnumDescription("Trim length too short")] TrimLengthTooShort = 15,

        /// <summary>
        ///     Wheel misaligned
        /// </summary>
        [EnumDescription("Wheel misaligned")] WheelMisaligned = 16
    }
}