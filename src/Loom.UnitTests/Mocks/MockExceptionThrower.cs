#region Using Directives

using System;

#endregion

namespace Loom.Mocks
{
    internal class MockExceptionThrower
    {
        internal Exception ExceptionToThrow { get; set; }

        internal int Depth { get; set; }

        internal void Throw()
        {
            if (Depth == 0)
                throw ExceptionToThrow;

            InnerClass inner = new InnerClass();
            InnerClass firstInner = inner;
            for (int i = 1; i < Depth - 1; i++)
            {
                inner.Inner = new InnerClass();
                inner = inner.Inner;
            }

            try
            {
                firstInner.Throw(ExceptionToThrow);
            }
            catch (Exception ex)
            {
                throw new Exception("Wrapped a caught mock exception", ex);
            }
        }

        #region Nested type: InnerClass

        internal class InnerClass
        {
            public InnerClass Inner { get; set; }

            public void Throw(Exception ex)
            {
                if (Inner != null)
                    Inner.Throw(ex);
                else
                    throw ex;
            }
        }

        #endregion
    }
}