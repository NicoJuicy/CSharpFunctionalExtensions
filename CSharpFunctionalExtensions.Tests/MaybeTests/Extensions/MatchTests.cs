﻿using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions.Tests.ResultTests.Extensions;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.Tests.MaybeTests.Extensions;

public class MatchTests : MatchTestsBase
{
    [Fact]
    public void Match_follows_some_branch_where_there_is_a_value()
    {
        Maybe<T> maybe = T.Value;

        maybe.Match(
            Some: value => value.Should().Be(T.Value),
            None: () => throw new FieldAccessException("Accessed None path while maybe has value")
        );
    }

    [Fact]
    public void Match_provides_context_to_some_selector()
    {
        Maybe<T> maybe = T.Value;
        var context = 5;
        var contextAsserted = false;

        maybe.Match(
            Some: (value, ctx) =>
            {
                ctx.Should().Be(context);
                value.Should().Be(T.Value);
                contextAsserted = true;
            },
            None: (_) => throw new FieldAccessException("Accessed None path while maybe has value"),
            context: context
        );

        contextAsserted.Should().BeTrue();
    }

    [Fact]
    public void Match_follows_none_branch_where_is_no_value()
    {
        Maybe<T> maybe = null;

        maybe.Match(
            Some: _ =>
                throw new FieldAccessException("Accessed Some path while maybe has no value"),
            None: () => Assert.True(true)
        );
    }

    [Fact]
    public void Match_provides_context_to_none_selector()
    {
        Maybe<T> maybe = null;
        var context = 5;
        var contextAsserted = false;

        maybe.Match(
            Some: (_, _) =>
                throw new FieldAccessException("Accessed Some path while maybe has no value"),
            None: (ctx) =>
            {
                ctx.Should().Be(context);
                contextAsserted = true;
                Assert.True(true);
            },
            context: context
        );

        contextAsserted.Should().BeTrue();
    }

    [Fact]
    public void Match_for_deconstruct_kv_follows_some_branch_where_is_no_value()
    {
        Maybe<KeyValuePair<int, string>> maybe = Maybe<KeyValuePair<int, string>>.From(
            new KeyValuePair<int, string>(42, "Matrix")
        );

        maybe.Match(
            Some: (intValue, stringValue) =>
            {
                intValue.Should().Be(42);
                stringValue.Should().Be("Matrix");
            },
            None: () => throw new FieldAccessException("Accessed None path while maybe has value")
        );
    }

    [Fact]
    public void Match_for_deconstruct_kv_provides_context_to_some_selector()
    {
        Maybe<KeyValuePair<int, string>> maybe = Maybe<KeyValuePair<int, string>>.From(
            new KeyValuePair<int, string>(42, "Matrix")
        );
        var context = 5;
        var contextAsserted = false;

        maybe.Match(
            Some: (intValue, stringValue, ctx) =>
            {
                intValue.Should().Be(42);
                stringValue.Should().Be("Matrix");
                ctx.Should().Be(context);
                contextAsserted = true;
            },
            None: (_) => throw new FieldAccessException("Accessed None path while maybe has value"),
            context: context
        );

        contextAsserted.Should().BeTrue();
    }

    [Fact]
    public void Match_for_deconstruct_kv_follows_none_branch_where_is_no_value()
    {
        Maybe<KeyValuePair<int, string>> maybe = Maybe<KeyValuePair<int, string>>.None;

        maybe.Match(
            Some: (intValue, stringValue) =>
                throw new FieldAccessException("Accessed Some path while maybe has no value"),
            None: () => Assert.True(true)
        );
    }

    [Fact]
    public void Match_for_deconstruct_kv_provides_context_to_none_selector()
    {
        Maybe<KeyValuePair<int, string>> maybe = Maybe<KeyValuePair<int, string>>.None;
        var context = 5;
        var contextAsserted = false;

        maybe.Match(
            Some: (intValue, stringValue, ctx) =>
                throw new FieldAccessException("Accessed Some path while maybe has no value"),
            None: (ctx) =>
            {
                ctx.Should().Be(context);
                contextAsserted = true;
            },
            context: context
        );

        contextAsserted.Should().BeTrue();
    }

    [Fact]
    public void Match_for_deconstruct_kv_follows_some_branch_where_is_a_return_value()
    {
        Maybe<KeyValuePair<int, string>> maybe = Maybe<KeyValuePair<int, string>>.From(
            new KeyValuePair<int, string>(42, "Matrix")
        );

        var returnValue = maybe.Match(
            Some: (intValue, stringValue) => intValue,
            None: () => throw new FieldAccessException("Accessed None path while maybe has value")
        );

        42.Should().Be(returnValue);
    }

    [Fact]
    public void Match_for_deconstruct_kv_with_return_value_provides_context_to_some_selector()
    {
        Maybe<KeyValuePair<int, string>> maybe = Maybe<KeyValuePair<int, string>>.From(
            new KeyValuePair<int, string>(42, "Matrix")
        );
        var context = 5;
        var contextAsserted = false;

        var returnValue = maybe.Match(
            Some: (intValue, stringValue, ctx) =>
            {
                ctx.Should().Be(context);
                contextAsserted = true;

                return intValue;
            },
            None: (_) => throw new FieldAccessException("Accessed None path while maybe has value"),
            context: context
        );

        42.Should().Be(returnValue);
        contextAsserted.Should().BeTrue();
    }

    [Fact]
    public void Match_for_deconstruct_kv_follows_none_branch_where_is_a_return_value()
    {
        Maybe<KeyValuePair<int, string>> maybe = Maybe<KeyValuePair<int, string>>.None;

        var returnValue = maybe.Match(
            Some: (intValue, stringValue) =>
                throw new FieldAccessException("Accessed Some path while maybe has no value"),
            None: () => -1
        );

        (-1).Should().Be(returnValue);
    }

    [Fact]
    public void Match_for_deconstruct_kv_with_return_value_provides_context_to_none_selector()
    {
        Maybe<KeyValuePair<int, string>> maybe = Maybe<KeyValuePair<int, string>>.None;
        var context = 5;
        var contextAsserted = false;

        var returnValue = maybe.Match(
            Some: (intValue, stringValue, ctx) =>
                throw new FieldAccessException("Accessed Some path while maybe has no value"),
            None: (ctx) =>
            {
                ctx.Should().Be(context);
                contextAsserted = true;
                return -1;
            },
            context: context
        );

        (-1).Should().Be(returnValue);
        contextAsserted.Should().BeTrue();
    }
}
