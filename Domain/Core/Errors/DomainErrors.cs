using Domain.Core.Primitives;
using System.Net;

namespace Domain.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static class DomainErrors
    {
        public static class Database
        {
            public static Error InternalServerError => new Error(HttpStatusCode.InternalServerError, "We have a problem with the database.");

        }
        public static class TaskError
        {
            public static Error NotFound => new Error(HttpStatusCode.NotFound, "The Task with the specified identifier was not found.");

        }
        /// <summary>
        /// Contains the user errors.
        /// </summary>
        public static class User
        {
            public static Error NotFound => new Error(HttpStatusCode.NotFound, "The user with the specified identifier was not found.");

            public static Error InvalidPermissions => new Error(HttpStatusCode.Forbidden,
                "The current user does not have the permissions to perform that operation.");

            public static Error DuplicateEmail => new Error(HttpStatusCode.Conflict, "The specified email is already in use.");

            public static Error CannotChangePassword => new Error(HttpStatusCode.Conflict,
                "The password cannot be changed to the specified password.");
        }

        /// <summary>
        /// Contains the name errors.
        /// </summary>
        public static class Name
        {
            public static Error NullOrEmpty => new Error(HttpStatusCode.BadRequest, "The name is required.");

            public static Error LongerThanAllowed => new Error(HttpStatusCode.BadRequest, "The name is longer than allowed.");
        }

        /// <summary>
        /// Contains the first name errors.
        /// </summary>
        public static class FirstName
        {
            public static Error NullOrEmpty => new Error(HttpStatusCode.BadRequest, "The first name is required.");

            public static Error LongerThanAllowed => new Error(HttpStatusCode.BadRequest, "The first name is longer than allowed.");
        }

        /// <summary>
        /// Contains the last name errors.
        /// </summary>
        public static class LastName
        {
            public static Error NullOrEmpty => new Error(HttpStatusCode.BadRequest, "The last name is required.");

            public static Error LongerThanAllowed => new Error(HttpStatusCode.BadRequest, "The last name is longer than allowed.");
        }

        /// <summary>
        /// Contains the email errors.
        /// </summary>
        public static class Email
        {
            public static Error NullOrEmpty => new Error(HttpStatusCode.BadRequest, "The email is required.");

            public static Error LongerThanAllowed => new Error(HttpStatusCode.BadRequest, "The email is longer than allowed.");

            public static Error InvalidFormat => new Error(HttpStatusCode.BadRequest, "The email format is invalid.");
        }

        /// <summary>
        /// Contains the password errors.
        /// </summary>
        public static class Password
        {
            public static Error NullOrEmpty => new Error(HttpStatusCode.BadRequest, "The password is required.");

            public static Error TooShort => new Error(HttpStatusCode.BadRequest, "The password is too short.");

            public static Error MissingUppercaseLetter => new Error(
                HttpStatusCode.BadRequest,
                "The password requires at least one uppercase letter.");

            public static Error MissingLowercaseLetter => new Error(
                HttpStatusCode.BadRequest,
                "The password requires at least one lowercase letter.");

            public static Error MissingDigit => new Error(
                HttpStatusCode.BadRequest,
                "The password requires at least one digit.");

            public static Error MissingNonAlphaNumeric => new Error(
                HttpStatusCode.BadRequest,
                "The password requires at least one non-alphanumeric.");
        }

        /// <summary>
        /// Contains general errors.
        /// </summary>
        public static class General
        {
            public static Error UnProcessableRequest => new Error(
                HttpStatusCode.UnprocessableContent,
                "The server could not process the request.");

            public static Error ServerError => new Error(HttpStatusCode.InternalServerError, "The server encountered an unrecoverable error.");
        }

        /// <summary>
        /// Contains the authentication errors.
        /// </summary>
        public static class Authentication
        {
            public static Error InvalidEmailOrPassword => new Error(HttpStatusCode.BadRequest, "The specified email or password are incorrect.");
        }
    }
}
