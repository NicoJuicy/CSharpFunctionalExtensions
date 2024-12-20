﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions
{
    public static partial class MaybeExtensions
    {
        public static async Task<TE> Match<TE, T>(
            this Maybe<T> maybe,
            Func<T, CancellationToken, Task<TE>> Some,
            Func<CancellationToken, Task<TE>> None,
            CancellationToken cancellationToken = default
        )
        {
            return maybe.HasValue
                ? await Some(maybe.GetValueOrThrow(), cancellationToken)
                : await None(cancellationToken);
        }

        public static async Task<TE> Match<TE, T, TContext>(
            this Maybe<T> maybe,
            Func<T, TContext, CancellationToken, Task<TE>> Some,
            Func<TContext, CancellationToken, Task<TE>> None,
            TContext context,
            CancellationToken cancellationToken = default
        )
        {
            return maybe.HasValue
                ? await Some(maybe.GetValueOrThrow(), context, cancellationToken)
                : await None(context, cancellationToken);
        }

        public static async Task Match<T>(
            this Maybe<T> maybe,
            Func<T, CancellationToken, Task> Some,
            Func<CancellationToken, Task> None,
            CancellationToken cancellationToken = default
        )
        {
            if (maybe.HasValue)
                await Some(maybe.GetValueOrThrow(), cancellationToken);
            else
                await None(cancellationToken);
        }

        public static async Task Match<T, TContext>(
            this Maybe<T> maybe,
            Func<T, TContext, CancellationToken, Task> Some,
            Func<TContext, CancellationToken, Task> None,
            TContext context,
            CancellationToken cancellationToken = default
        )
        {
            if (maybe.HasValue)
                await Some(maybe.GetValueOrThrow(), context, cancellationToken);
            else
                await None(context, cancellationToken);
        }

        public static async Task<TE> Match<TE, TKey, TValue>(
            this Maybe<KeyValuePair<TKey, TValue>> maybe,
            Func<TKey, TValue, CancellationToken, Task<TE>> Some,
            Func<CancellationToken, Task<TE>> None,
            CancellationToken cancellationToken = default
        )
        {
            return maybe.HasValue
                ? await Some.Invoke(
                    maybe.GetValueOrThrow().Key,
                    maybe.GetValueOrThrow().Value,
                    cancellationToken
                )
                : await None.Invoke(cancellationToken);
        }

        public static async Task<TE> Match<TE, TKey, TValue, TContext>(
            this Maybe<KeyValuePair<TKey, TValue>> maybe,
            Func<TKey, TValue, TContext, CancellationToken, Task<TE>> Some,
            Func<TContext, CancellationToken, Task<TE>> None,
            TContext context,
            CancellationToken cancellationToken = default
        )
        {
            return maybe.HasValue
                ? await Some.Invoke(
                    maybe.GetValueOrThrow().Key,
                    maybe.GetValueOrThrow().Value,
                    context,
                    cancellationToken
                )
                : await None.Invoke(context, cancellationToken);
        }

        public static async Task Match<TKey, TValue>(
            this Maybe<KeyValuePair<TKey, TValue>> maybe,
            Func<TKey, TValue, CancellationToken, Task> Some,
            Func<CancellationToken, Task> None,
            CancellationToken cancellationToken = default
        )
        {
            if (maybe.HasValue)
            {
                await Some.Invoke(
                    maybe.GetValueOrThrow().Key,
                    maybe.GetValueOrThrow().Value,
                    cancellationToken
                );
            }
            else
            {
                await None.Invoke(cancellationToken);
            }
        }

        public static async Task Match<TKey, TValue, TContext>(
            this Maybe<KeyValuePair<TKey, TValue>> maybe,
            Func<TKey, TValue, TContext, CancellationToken, Task> Some,
            Func<TContext, CancellationToken, Task> None,
            TContext context,
            CancellationToken cancellationToken = default
        )
        {
            if (maybe.HasValue)
            {
                await Some.Invoke(
                    maybe.GetValueOrThrow().Key,
                    maybe.GetValueOrThrow().Value,
                    context,
                    cancellationToken
                );
            }
            else
            {
                await None.Invoke(context, cancellationToken);
            }
        }
    }
}
