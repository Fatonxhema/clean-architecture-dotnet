using System.Net;

namespace Domain.Core.Primitives
{
    /// <summary>
    /// Represents a concrete domain error.
    /// </summary>
    public sealed class Error : ValueObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">The error message.</param>
        public Error(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public HttpStatusCode Code { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; }

        public static implicit operator HttpStatusCode(Error error) => error?.Code ?? HttpStatusCode.Processing;

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
            yield return Message;
        }

        /// <summary>
        /// Gets the empty error instance.
        /// </summary>
        internal static Error None => new Error(0, string.Empty);
    }
}
