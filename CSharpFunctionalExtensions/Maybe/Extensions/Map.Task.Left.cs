﻿using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions
{
    public static partial class MaybeExtensions
    {
        public static async Task<Maybe<K>> Map<T, K>(
            this Task<Maybe<T>> maybeTask,
            Func<T, K> selector
        )
        {
            var maybe = await maybeTask.DefaultAwait();
            return maybe.Map(selector);
        }

        public static async Task<Maybe<K>> Map<T, K, TContext>(
            this Task<Maybe<T>> maybeTask,
            Func<T, TContext, K> selector,
            TContext context
        )
        {
            var maybe = await maybeTask.DefaultAwait();
            return maybe.Map(selector, context);
        }
    }
}
