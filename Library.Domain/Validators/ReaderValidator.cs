// Copyright (c) Buzenchi Andreea

using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    /// <summary>
    /// Validator responsible for validating <see cref="Reader"/> entities.
    /// </summary>
    /// <remarks>
    /// Ensures that a reader has a name and at least one contact method
    /// (email or phone number).
    /// </remarks>
    public class ReaderValidator : AbstractValidator<Reader>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderValidator"/> class
        /// and defines validation rules for <see cref="Reader"/>.
        /// </summary>
        public ReaderValidator()
        {
            this.RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Reader name is required.");

            this.RuleFor(r => r)
                .Must(r =>
                    !string.IsNullOrWhiteSpace(r.Email) ||
                    !string.IsNullOrWhiteSpace(r.Phone))
                .WithMessage("Reader must have at least an email or a phone number.");
        }
    }
}
