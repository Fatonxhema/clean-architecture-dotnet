using Domain.Core.Primitives;
using System.Net;

namespace Application.Core.Errors
{
    /// <summary>
    /// Contains the validation errors.
    /// </summary>
    internal static class ValidationErrors
    {
        /// <summary>
        /// Contains the login errors.
        /// </summary>
        internal static class Login
        {
            internal static Error EmailIsRequired => new Error(HttpStatusCode.BadRequest, "The email is required.");

            internal static Error PasswordIsRequired => new Error(HttpStatusCode.BadRequest, "The password is required.");
        }

        /// <summary>
        /// Contains the reject friendship request errors.
        /// </summary>
        internal static class RejectFriendshipRequest
        {
            internal static Error FriendshipRequestIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The invitation identifier is required.");
        }

        /// <summary>
        /// Contains the accept friendship request errors.
        /// </summary>
        internal static class AcceptFriendshipRequest
        {
            internal static Error FriendshipRequestIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The invitation identifier is required.");
        }

        /// <summary>
        /// Contains the send remove friendship errors.
        /// </summary>
        internal static class RemoveFriendship
        {
            internal static Error UserIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The user identifier is required.");

            internal static Error FriendIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The friend identifier is required.");
        }

        /// <summary>
        /// Contains the create group event errors.
        /// </summary>
        internal static class CreateGroupEvent
        {
            internal static Error UserIdIsRequired => new Error(HttpStatusCode.BadRequest, "The user identifier is required.");

            internal static Error NameIsRequired => new Error(HttpStatusCode.BadRequest, "The event name is required.");

            internal static Error CategoryIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The category identifier is required.");

            internal static Error DateAndTimeIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The date and time of the event is required.");
        }

        /// <summary>
        /// Contains the update group event errors.
        /// </summary>
        internal static class UpdateGroupEvent
        {
            internal static Error GroupEventIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The group event identifier is required.");

            internal static Error NameIsRequired => new Error(HttpStatusCode.BadRequest, "The event name is required.");

            internal static Error DateAndTimeIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The date and time of the event is required.");
        }

        /// <summary>
        /// Contains the cancel group event errors.
        /// </summary>
        internal static class CancelGroupEvent
        {
            internal static Error GroupEventIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The group event identifier is required.");
        }

        /// <summary>
        /// Contains the invite friend to group event errors.
        /// </summary>
        internal static class InviteFriendToGroupEvent
        {
            internal static Error GroupEventIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The group event identifier is required.");

            internal static Error FriendIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The friend identifier is required.");
        }

        /// <summary>
        /// Contains the accept invitation errors.
        /// </summary>
        internal static class AcceptInvitation
        {
            internal static Error InvitationIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The invitation identifier is required.");
        }

        /// <summary>
        /// Contains the reject invitation errors.
        /// </summary>
        internal static class RejectInvitation
        {
            internal static Error InvitationIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The invitation identifier is required.");
        }

        /// <summary>
        /// Contains the create personal event errors.
        /// </summary>
        internal static class CreatePersonalEvent
        {
            internal static Error UserIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The user identifier is required.");

            internal static Error NameIsRequired => new Error(HttpStatusCode.BadRequest, "The event name is required.");

            internal static Error CategoryIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The category identifier is required.");

            internal static Error DateAndTimeIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The date and time of the event is required.");
        }

        /// <summary>
        /// Contains the update personal event errors.
        /// </summary>
        internal static class UpdatePersonalEvent
        {
            internal static Error GroupEventIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The group event identifier is required.");

            internal static Error NameIsRequired => new Error(HttpStatusCode.BadRequest, "The event name is required.");

            internal static Error DateAndTimeIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The date and time of the event is required.");
        }

        /// <summary>
        /// Contains the cancel personal event errors.
        /// </summary>
        internal static class CancelPersonalEvent
        {
            internal static Error PersonalEventIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The group event identifier is required.");
        }

        /// <summary>
        /// Contains the change password errors.
        /// </summary>
        internal static class ChangePassword
        {
            internal static Error UserIdIsRequired => new Error(HttpStatusCode.BadRequest, "The user identifier is required.");

            internal static Error PasswordIsRequired => new Error(HttpStatusCode.BadRequest, "The password is required.");
        }

        /// <summary>
        /// Contains the create user errors.
        /// </summary>
        internal static class CreateUser
        {
            internal static Error FirstNameIsRequired => new Error(HttpStatusCode.BadRequest, "The first name is required.");

            internal static Error LastNameIsRequired => new Error(HttpStatusCode.BadRequest, "The last name is required.");

            internal static Error EmailIsRequired => new Error(HttpStatusCode.BadRequest, "The email is required.");

            internal static Error PasswordIsRequired => new Error(HttpStatusCode.BadRequest, "The password is required.");
        }

        /// <summary>
        /// Contains the send friendship request errors.
        /// </summary>
        internal static class SendFriendshipRequest
        {
            internal static Error UserIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The user identifier is required.");

            internal static Error FriendIdIsRequired => new Error(
                HttpStatusCode.BadRequest,
                "The friend identifier is required.");
        }

        /// <summary>
        /// Contains the update user errors.
        /// </summary>
        internal static class UpdateUser
        {
            internal static Error UserIdIsRequired => new Error(HttpStatusCode.BadRequest, "The user identifier is required.");

            internal static Error FirstNameIsRequired => new Error(HttpStatusCode.BadRequest, "The first name is required.");

            internal static Error LastNameIsRequired => new Error(HttpStatusCode.BadRequest, "The last name is required.");
        }
    }
}
