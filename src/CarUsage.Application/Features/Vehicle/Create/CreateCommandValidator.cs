using FluentValidation;

namespace CarUsage.Application.Features.Vehicle.Create;

public class CreateCommandValidator : AbstractValidator<CreateCommandRequest>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Araba ismi boş olamaz.")
            .NotNull().WithMessage("Araba ismi boş olamaz.")
            .MinimumLength(3).WithMessage("Araba ismi en az 3 karakter olmalı.");
        
        RuleFor(x => x.PlateId)
            .NotEmpty().WithMessage("Plaka boş olamaz.")
            .NotNull().WithMessage("Plaka boş olamaz.")
            .MinimumLength(6).WithMessage("Plaka en az 6 karakter olmalı.")
            .MaximumLength(8).WithMessage("Plaka en fazla 8 karakter olmalı.")
            .Must(ContainAtLeastTwoLetter).WithMessage("Plaka en az 2 harf içermeli.")
            .Must(ContainAtLeastFourDigits).WithMessage("Plaka en az 4 sayısal değer içermeli.");
    }
    private bool ContainAtLeastTwoLetter(string plateId)
    {
        int letterCount = plateId.Count(char.IsLetter);
        return letterCount >= 2;
    }
    
    private bool ContainAtLeastFourDigits(string plateId)
    {
        int digitCount = plateId.Count(char.IsDigit);
        return digitCount >= 4;
    }
}