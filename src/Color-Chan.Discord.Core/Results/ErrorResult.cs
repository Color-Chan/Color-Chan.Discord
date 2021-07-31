namespace Color_Chan.Discord.Core.Results
{
    /// <inheritdoc cref="IErrorResult" />
    public record ErrorResult : IErrorResult
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="ErrorResult" />.
        /// </summary>
        /// <param name="errorMessage">The message of the error.</param>
        public ErrorResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        /// <inheritdoc />
        public string ErrorMessage { get; set; }
    }
}