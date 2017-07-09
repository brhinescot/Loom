#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace Loom
{
    public abstract class ContextBase<T>
    {
        [ThreadStatic] private static Stack<T> stack;

        /// <summary>
        ///     Gets the current active {T}.
        /// </summary>
        public static T Current
        {
            get
            {
                if (stack == null || stack.Count == 0)
                    return default(T);

                return stack.Peek();
            }
        }

        /// <summary>
        ///     Adds the specified {T} to the current context stack.
        /// </summary>
        /// <param name="context"></param>
        protected static void PushObject(T context)
        {
            if (stack == null)
                stack = new Stack<T>();
            stack.Push(context);
        }

        /// <summary>
        ///     Removes the current item from the context stack.
        /// </summary>
        /// <remarks>
        ///     If a derived class is using the Dispose pattern to control a resource, consider
        ///     calling this method from it's <see cref="IDisposable.Dispose" /> method.
        /// </remarks>
        /// <returns></returns>
        protected static T PopObject()
        {
            if (stack == null)
                throw new InvalidOperationException("An attempt was made to decrement a ContextBase<T> Stack that is not initialized.");
            if (stack.Count == 0)
                throw new InvalidOperationException("An attempt was made to decrement an empty ContextBase<T> Stack.");

            return stack.Pop();
        }
    }
}