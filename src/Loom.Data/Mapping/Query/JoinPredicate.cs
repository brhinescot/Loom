#region License Information

// ******************************************************************
// Devinterop Framework 
// 
// Copyright � 2004, 2008 by Brian Scott (DevInterop)
// All Rights Reserved
//  
// Unauthorized reproduction or distribution in source or compiled
// form is strictly prohibited.
//  
// http://www.devinterop.com
// http://blogs.geekdojo.net/brian
//  
// ******************************************************************

#endregion

#region Using Directives

using System;
using System.Diagnostics;
using Loom.Data.Mapping.Schema;

#endregion

namespace Loom.Data.Mapping.Query
{
    [DebuggerDisplay("{FromColumn.Table.Name, nq}.{FromColumn.Name, nq} {Comparison} {ToColumn.Table.Name, nq}.{ToColumn.Name, nq}, OrNextPredicate={OrNextPredicate}")]
    public class JoinPredicate : IPredicate<JoinPredicate>, IBindablePredicate, IEquatable<JoinPredicate>
    {
        #region Property Accessors

        public IQueryableColumn FromColumn { get; private set; }

        public Comparison Comparison { get; set; }

        public IQueryableColumn ToColumn { get; private set; }

        public JoinPredicate NextInGroup { get; set; }

        public bool OrNextPredicate { get; set; }

        public bool OrToPreviousGroup { get; set; }

        public bool IsGroup
        {
            get { return NextInGroup != null; }
        }

        #endregion

        #region .ctor

        public JoinPredicate(IQueryableColumn fromColumn, Comparison comparison, IQueryableColumn toColumn)
        {
//            if (toColumn != null && fromColumn != null && fromColumn.Table == toColumn.Table)
//                throw new NotSupportedException("Self referential table joins are not supported in this version.");

            FromColumn = fromColumn;
            Comparison = comparison;
            ToColumn = toColumn;
        }

        #endregion

        #region IPredicate<JoinPredicate> Members

        /// <exception cref="ArgumentException">Only one instance of each <see cref="JoinPredicate"/>
        /// can exist in a predicate group.</exception>
        public JoinPredicate Or(JoinPredicate predicate)
        {
            if (this == predicate)
                throw new ArgumentException("Only one instance of each JoinPredicate can exist in a predicate group.");

            JoinPredicate last = this;
            while (last.NextInGroup != null)
            {
                if (last.NextInGroup == predicate)
                    throw new ArgumentException("Only one instance of each JoinPredicate can exist in a predicate group.");
                last = last.NextInGroup;
            }

            last.NextInGroup = predicate;
            last.OrNextPredicate = true;
            return this;
        }

        /// <exception cref="ArgumentException">Only one instance of each <see cref="JoinPredicate"/>
        /// can exist in a predicate group.</exception>
        public JoinPredicate And(JoinPredicate predicate)
        {
            if (this == predicate)
                throw new ArgumentException("Only one instance of each JoinPredicate can exist in a predicate group.");

            JoinPredicate last = this;
            while (last.NextInGroup != null)
            {
                if (last.NextInGroup == predicate)
                    throw new ArgumentException("Only one instance of each JoinPredicate can exist in a predicate group.");
                last = last.NextInGroup;
            }

            last.NextInGroup = predicate;
            last.OrNextPredicate = false;
            return this;
        }

        #endregion

        #region IBindablePredicate Members

        void IBindablePredicate.BindWhere(ICommandBuilder builder)
        {
            if(!builder.JoinPredicates.Contains(this))
                builder.JoinPredicates.Add(this);
        }

        /// <exception cref="NotImplementedException"></exception>
        void IBindablePredicate.BindHaving(ICommandBuilder builder)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Operator Overloads

        public static JoinPredicate operator |(JoinPredicate comparison1, JoinPredicate comparison2)
        {
            if (comparison1 == null)
                return comparison2;

            return comparison1.Or(comparison2);
        }

        public static JoinPredicate operator &(JoinPredicate comparison1, JoinPredicate comparison2)
        {
            if (comparison1 == null)
                return comparison2;

            return comparison1.And(comparison2);
        }

        #endregion

        public bool Equals(JoinPredicate other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Equals(other.FromColumn, FromColumn) && Equals(other.Comparison, Comparison) && Equals(other.ToColumn, ToColumn);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof (JoinPredicate))
                return false;
            return Equals((JoinPredicate) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = FromColumn.GetHashCode();
                result = (result*397) ^ Comparison.GetHashCode();
                result = (result*397) ^ ToColumn.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(JoinPredicate left, JoinPredicate right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(JoinPredicate left, JoinPredicate right)
        {
            return !Equals(left, right);
        }
    }
}
