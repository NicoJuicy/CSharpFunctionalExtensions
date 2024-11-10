﻿using System.Threading.Tasks;
using CSharpFunctionalExtensions.ValueTasks;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.Tests.MaybeTests.Extensions
{
    public class BindTests_ValueTask : MaybeTestBase
    {
        [Fact]
        public async Task Bind_ValueTask_returns_no_value_if_initial_maybe_is_null()
        {
            Maybe<T> maybe = null;

            var maybe2 = await maybe.AsValueTask().Bind(ExpectAndReturnMaybe_ValueTask(null, T.Value));

            maybe2.HasValue.Should().BeFalse();
        }

        [Fact]
        public async Task Bind_ValueTask_returns_no_value_if_selector_returns_null()
        {
            Maybe<T> maybe = T.Value;

            var maybe2 = await maybe.AsValueTask().Bind(ExpectAndReturn_ValueTask(T.Value, Maybe<T>.None));

            maybe2.HasValue.Should().BeFalse();
        }

        [Fact]
        public async Task Bind_ValueTask_returns_value_if_selector_returns_value()
        {
            Maybe<T> maybe = T.Value;

            var maybe2 = await maybe.AsValueTask().Bind(ExpectAndReturnMaybe_ValueTask<T>(T.Value, T.Value));

            maybe2.HasValue.Should().BeTrue();
            maybe2.Value.Should().Be(T.Value);
        }

        [Fact]
        public async Task Bind_ValueTask_provides_context_to_selector()
        {
            Maybe<T> maybe = T.Value;
            var context = 5;

            var maybe2 = await maybe.AsValueTask().Bind(
                (value, ctx) =>
                {
                    ctx.Should().Be(context);
                    return Maybe.From(value).AsValueTask();
                },
                context
            );

            maybe2.HasValue.Should().BeTrue();
        }

        [Fact]
        public async Task Bind_ValueTask_with_context_returns_no_value_if_initial_maybe_is_null()
        {
            Maybe<T> maybe = null;

            var maybe2 = await maybe.AsValueTask().Bind(
                (value, _) => ExpectAndReturnMaybe_ValueTask(null, T.Value)(value),
                context: 5
            );

            maybe2.HasValue.Should().BeFalse();
        }

        [Fact]
        public async Task Bind_ValueTask_with_context_returns_no_value_if_selector_returns_null()
        {
            Maybe<T> maybe = T.Value;

            var maybe2 = await maybe.AsValueTask().Bind(
                (value, _) => ExpectAndReturn_ValueTask(T.Value, Maybe<T>.None)(value),
                context: 5
            );

            maybe2.HasValue.Should().BeFalse();
        }

        [Fact]
        public async Task Bind_ValueTask_with_context_returns_value_if_selector_returns_value()
        {
            Maybe<T> maybe = T.Value;

            var maybe2 = await maybe.AsValueTask().Bind(
                (value, _) => ExpectAndReturnMaybe_ValueTask<T>(T.Value, T.Value)(value),
                5
            );

            maybe2.HasValue.Should().BeTrue();
            maybe2.Value.Should().Be(T.Value);
        }
    }
}
