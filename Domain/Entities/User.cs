﻿using Domain.Core.Abstractions;
using Domain.Core.Primitives;
using Domain.Core.Utility;
using Domain.ValueObjects;

namespace Domain.Entities
{
    /// <summary>
    /// Represents the user entity.
    /// </summary>
    public sealed class User : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="firstName">The user first name.</param>
        /// <param name="lastName">The user last name.</param>
        /// <param name="email">The user email instance.</param>
        /// <param name="passwordHash">The user password hash.</param>
        private User(FirstName firstName, LastName lastName, Email email, string passwordHash)
            : base(Guid.NewGuid())
        {
            Ensure.NotEmpty(firstName, "The first name is required.", nameof(firstName));
            Ensure.NotEmpty(lastName, "The last name is required.", nameof(lastName));
            Ensure.NotEmpty(email, "The email is required.", nameof(email));
            Ensure.NotEmpty(passwordHash, "The password hash is required", nameof(passwordHash));

            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        private User()
        {
        }

        /// <summary>
        /// Gets the user first name.
        /// </summary>
        public FirstName FirstName { get; private set; }

        /// <summary>
        /// Gets the user last name.
        /// </summary>
        public LastName LastName { get; private set; }

        /// <summary>
        /// Gets the user full name.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Gets the user email.
        /// </summary>
        public Email Email { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; }

        /// <inheritdoc />
        public bool Deleted { get; }

    }
}
