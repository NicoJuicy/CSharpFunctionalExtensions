﻿#if NET5_0_OR_GREATER
using System;
using System.Threading.Tasks;

namespace CSharpFunctionalExtensions.ValueTasks
{
    public static partial class ResultExtensions
    {
        public static async ValueTask<Result> MapError(
            this Result result,
            Func<string, ValueTask<string>> errorFactory
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success();
            }

            var error = await errorFactory(result.Error);
            return Result.Failure(error);
        }

        public static async ValueTask<Result> MapError<TContext>(
            this Result result,
            Func<string, TContext, ValueTask<string>> errorFactory,
            TContext context
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success();
            }

            var error = await errorFactory(result.Error, context);
            return Result.Failure(error);
        }

        public static async ValueTask<UnitResult<E>> MapError<E>(
            this Result result,
            Func<string, ValueTask<E>> errorFactory
        )
        {
            if (result.IsSuccess)
            {
                return UnitResult.Success<E>();
            }

            var error = await errorFactory(result.Error);
            return UnitResult.Failure(error);
        }

        public static async ValueTask<UnitResult<E>> MapError<E, TContext>(
            this Result result,
            Func<string, TContext, ValueTask<E>> errorFactory,
            TContext context
        )
        {
            if (result.IsSuccess)
            {
                return UnitResult.Success<E>();
            }

            var error = await errorFactory(result.Error, context);
            return UnitResult.Failure(error);
        }

        public static async ValueTask<Result<T>> MapError<T>(
            this Result<T> result,
            Func<string, ValueTask<string>> errorFactory
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success(result.Value);
            }

            var error = await errorFactory(result.Error);
            return Result.Failure<T>(error);
        }

        public static async ValueTask<Result<T>> MapError<T, TContext>(
            this Result<T> result,
            Func<string, TContext, ValueTask<string>> errorFactory,
            TContext context
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success(result.Value);
            }

            var error = await errorFactory(result.Error, context);
            return Result.Failure<T>(error);
        }

        public static async ValueTask<Result<T, E>> MapError<T, E>(
            this Result<T> result,
            Func<string, ValueTask<E>> errorFactory
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success<T, E>(result.Value);
            }

            var error = await errorFactory(result.Error);
            return Result.Failure<T, E>(error);
        }

        public static async ValueTask<Result<T, E>> MapError<T, E, TContext>(
            this Result<T> result,
            Func<string, TContext, ValueTask<E>> errorFactory,
            TContext context
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success<T, E>(result.Value);
            }

            var error = await errorFactory(result.Error, context);
            return Result.Failure<T, E>(error);
        }

        public static async ValueTask<Result> MapError<E>(
            this UnitResult<E> result,
            Func<E, ValueTask<string>> errorFactory
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success();
            }

            var error = await errorFactory(result.Error);
            return Result.Failure(error);
        }

        public static async ValueTask<Result> MapError<E, TContext>(
            this UnitResult<E> result,
            Func<E, TContext, ValueTask<string>> errorFactory,
            TContext context
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success();
            }

            var error = await errorFactory(result.Error, context);
            return Result.Failure(error);
        }

        public static async ValueTask<UnitResult<E2>> MapError<E, E2>(
            this UnitResult<E> result,
            Func<E, ValueTask<E2>> errorFactory
        )
        {
            if (result.IsSuccess)
            {
                return UnitResult.Success<E2>();
            }

            var error = await errorFactory(result.Error);
            return UnitResult.Failure(error);
        }

        public static async ValueTask<UnitResult<E2>> MapError<E, E2, TContext>(
            this UnitResult<E> result,
            Func<E, TContext, ValueTask<E2>> errorFactory,
            TContext context
        )
        {
            if (result.IsSuccess)
            {
                return UnitResult.Success<E2>();
            }

            var error = await errorFactory(result.Error, context);
            return UnitResult.Failure(error);
        }

        public static async ValueTask<Result<T>> MapError<T, E>(
            this Result<T, E> result,
            Func<E, ValueTask<string>> errorFactory
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success(result.Value);
            }

            var error = await errorFactory(result.Error);
            return Result.Failure<T>(error);
        }

        public static async ValueTask<Result<T>> MapError<T, E, TContext>(
            this Result<T, E> result,
            Func<E, TContext, ValueTask<string>> errorFactory,
            TContext context
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success(result.Value);
            }

            var error = await errorFactory(result.Error, context);
            return Result.Failure<T>(error);
        }

        public static async ValueTask<Result<T, E2>> MapError<T, E, E2>(
            this Result<T, E> result,
            Func<E, ValueTask<E2>> errorFactory
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success<T, E2>(result.Value);
            }

            var error = await errorFactory(result.Error);
            return Result.Failure<T, E2>(error);
        }

        public static async ValueTask<Result<T, E2>> MapError<T, E, E2, TContext>(
            this Result<T, E> result,
            Func<E, TContext, ValueTask<E2>> errorFactory,
            TContext context
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success<T, E2>(result.Value);
            }

            var error = await errorFactory(result.Error, context);
            return Result.Failure<T, E2>(error);
        }
    }
}
#endif
