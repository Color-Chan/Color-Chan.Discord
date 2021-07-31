using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Color_Chan.Discord.Core.Extensions
{
    internal static class TaskExtensions
    {
        /// <summary>
        ///     Tries to run multiple <see cref="Task" />s at once.
        /// </summary>
        /// <param name="tasks">The <see cref="Task" />s that will be run.</param>
        /// <typeparam name="T">The <see cref="Task" /> types.</typeparam>
        /// <returns>
        ///     The tasks results.
        /// </returns>
        /// <exception cref="AggregateException">Thrown when one or more of the tasks resulted in an exception.</exception>
        /// <exception cref="NullReferenceException">Thrown when <see cref="AggregateException" /> was null.</exception>
        public static async Task<IEnumerable<T>> RunWhenAllAsync<T>(this Task<T[]> tasks)
        {
            try
            {
                return await tasks.ConfigureAwait(false);
            }
            catch (Exception)
            {
                // Ignore exception.
            }

            throw tasks.Exception ?? throw new NullReferenceException("AggregateException can not but null");
        }
    }
}