using FluentValidation;
using Queues.Application.PersonQueue.DTOs;

namespace Queues.Application.PersonQueue.Validators;
public class PersonQueueAddDtoValidator : AbstractValidator<PersonQueueAddDto>
{
    public PersonQueueAddDtoValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty()
            .GreaterThan(1)
            .WithName("Invalid-PersonId")
            .WithMessage("Id de persona inválido");
    }
}
