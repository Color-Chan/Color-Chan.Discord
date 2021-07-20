﻿using System;

namespace Color_Chan.Discord.Core.Results
{
    public readonly struct Result : IResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="Result" />.
        /// </summary>
        /// <param name="errorResult">The <see cref="IErrorResult" /> containing the error details.</param>
        /// <param name="innerResult">The inner <see cref="errorResult" />.</param>
        private Result(IErrorResult? errorResult, IResult? innerResult)
        {
            InnerResult = innerResult;
            ErrorResult = errorResult;
        }

        /// <inheritdoc />
        public bool IsSuccessful => ErrorResult is null;

        /// <inheritdoc />
        public IErrorResult? ErrorResult { get; }

        /// <inheritdoc />
        public IResult? InnerResult { get; }

        /// <summary>
        ///     Initializes a new successful instance of <see cref="Result" />.
        /// </summary>
        /// <returns>
        ///     The successful instance of <see cref="Result" />.
        /// </returns>
        public static Result FromSuccess()
        {
            return new(null, null);
        }

        /// <summary>
        ///     Initializes a new unsuccessful instance of <see cref="Result" />.
        /// </summary>
        /// <param name="errorResult">The <see cref="IErrorResult" /> containing the error details.</param>
        /// <returns>
        ///     The unsuccessful instance of <see cref="Result" />.
        /// </returns>
        public static Result FromError(IErrorResult errorResult)
        {
            return new(errorResult, null);
        }

        /// <summary>
        ///     Initializes a new unsuccessful instance of <see cref="Result" />.
        /// </summary>
        /// <param name="errorResult">The <see cref="IErrorResult" /> containing the error details.</param>
        /// <param name="innerResult">The inner <see cref="IResult" />.</param>
        /// <returns>
        ///     The unsuccessful instance of <see cref="Result" />.
        /// </returns>
        public static Result FromError(IErrorResult errorResult, IResult innerResult)
        {
            return new(errorResult, innerResult);
        }

        /// <summary>
        ///     Creates a <see cref="Result" /> with an <see cref="Exception" />.
        /// </summary>
        /// <param name="exception">The <see cref="Exception" />.</param>
        /// <returns>
        ///     The unsuccessful instance of <see cref="Result" />.
        /// </returns>
        public static implicit operator Result(Exception exception)
        {
            return new(new ExceptionResult(exception), default);
        }

        /// <summary>
        ///     Creates a <see cref="Result" /> from an <see cref="ErrorResult" />.
        /// </summary>
        /// <param name="error">The <see cref="ErrorResult" />.</param>
        /// <returns>
        ///     The unsuccessful instance of <see cref="Result" />.
        /// </returns>
        public static implicit operator Result(ErrorResult error)
        {
            return new(error, default);
        }
    }


    public readonly struct Result<T> : IResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="Result" />.
        /// </summary>
        /// <param name="entity">The <see cref="T" /> containing the result data.</param>
        /// <param name="errorResult">The <see cref="IErrorResult" /> containing the error details.</param>
        /// <param name="innerResult">The inner <see cref="errorResult" />.</param>
        private Result(T? entity, IErrorResult? errorResult, IResult? innerResult)
        {
            InnerResult = innerResult;
            Entity = entity;
            ErrorResult = errorResult;
        }

        /// <inheritdoc />
        public bool IsSuccessful => ErrorResult is null;

        /// <summary>
        ///     The result entity.
        /// </summary>
        public T? Entity { get; }

        /// <inheritdoc />
        public IErrorResult? ErrorResult { get; }

        /// <inheritdoc />
        public IResult? InnerResult { get; }

        /// <summary>
        ///     Initializes a new successful instance of <see cref="Result" />.
        /// </summary>
        /// <param name="entity">The <see cref="T" /> containing the result data.</param>
        /// <returns>
        ///     The successful instance of <see cref="Result" />.
        /// </returns>
        public static Result<T> FromSuccess(T entity)
        {
            return new(entity, null, null);
        }

        /// <summary>
        ///     Initializes a new unsuccessful instance of <see cref="Result" />.
        /// </summary>
        /// <param name="entity">The <see cref="T" /> containing the result data.</param>
        /// <param name="errorResult">The <see cref="IErrorResult" /> containing the error details.</param>
        /// <returns>
        ///     The unsuccessful instance of <see cref="Result" />.
        /// </returns>
        public static Result<T> FromError(T? entity, IErrorResult errorResult)
        {
            return new(entity, errorResult, null);
        }

        /// <summary>
        ///     Initializes a new unsuccessful instance of <see cref="Result" />.
        /// </summary>
        /// <param name="entity">The <see cref="T" /> containing the result data.</param>
        /// <param name="errorResult">The <see cref="IErrorResult" /> containing the error details.</param>
        /// <param name="innerResult">The inner <see cref="IResult" />.</param>
        /// <returns>
        ///     The unsuccessful instance of <see cref="Result" />.
        /// </returns>
        public static Result<T> FromError(T? entity, IErrorResult errorResult, IResult innerResult)
        {
            return new(entity, errorResult, innerResult);
        }

        /// <summary>
        ///     Creates a <see cref="Result{T}" /> with an <see cref="Exception" />.
        /// </summary>
        /// <param name="exception">The <see cref="Exception" />.</param>
        /// <returns>
        ///     The unsuccessful instance of <see cref="Result" />.
        /// </returns>
        public static implicit operator Result<T>(Exception exception)
        {
            return new(default, new ExceptionResult(exception), default);
        }

        /// <summary>
        ///     Creates a <see cref="Result{T}" /> from an <see cref="ErrorResult" />.
        /// </summary>
        /// <param name="error">The <see cref="ErrorResult" />.</param>
        /// <returns>
        ///     The unsuccessful instance of <see cref="Result" />.
        /// </returns>
        public static implicit operator Result<T>(ErrorResult error)
        {
            return new(default, error, default);
        }
    }
}