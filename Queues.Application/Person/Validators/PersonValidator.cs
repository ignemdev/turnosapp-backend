using FluentValidation;
using Queues.Application.Person.DTOs;

namespace Queues.Application.Person.Validators
{
    public class PersonAddDtoValidator : AbstractValidator<PersonAddDto>
    {
        public PersonAddDtoValidator()
        {
            RuleFor(x => x.Pregnant)
                .NotEmpty();

            RuleFor(x => x.HealthIssues)
                .NotEmpty();

            RuleFor(x => x.Height)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Weight)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);
        }
    }
}
