#region Using Directives

using System.Diagnostics;

#endregion

namespace Loom.Data
{
    [DebuggerDisplay("ConstraintType={ConstraintType}, Count={Count}, Start={Start}")]
    public struct Constraint
    {
        public ConstraintType ConstraintType { get; private set; }
        public int Count { get; private set; }
        public int Start { get; private set; }

        public static Constraint TopPercent(int percent)
        {
            return new Constraint {ConstraintType = ConstraintType.TopPercent, Start = 0, Count = percent};
        }

        public static Constraint TopCount(int count)
        {
            return new Constraint {ConstraintType = ConstraintType.Top, Start = 0, Count = count};
        }

        public static Constraint Page(int startIndex, int pageSize)
        {
            return new Constraint {ConstraintType = ConstraintType.Page, Start = startIndex, Count = pageSize};
        }

        public static Constraint Random(int count)
        {
            return new Constraint {ConstraintType = ConstraintType.Random, Start = 0, Count = count};
        }
    }
}