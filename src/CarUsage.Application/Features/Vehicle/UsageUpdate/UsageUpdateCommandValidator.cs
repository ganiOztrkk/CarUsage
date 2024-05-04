using FluentValidation;

namespace CarUsage.Application.Features.Vehicle.UsageUpdate;

public class UsageUpdateCommandValidator : AbstractValidator<UsageUpdateCommandRequest>
{
    public UsageUpdateCommandValidator()
    {
        RuleFor(x => x.ActiveUsageTime)
            .LessThanOrEqualTo(168).WithMessage("Çalışma süresi en çok 168 saat olabilir.")
            .Must(x => decimal.TryParse(x.ToString(), out _)).WithMessage("Çalışma süresi bir sayı olmalıdır.");
        
        RuleFor(x => x.RepairTime)
            .LessThanOrEqualTo(168).WithMessage("Çalışma süresi en çok 168 saat olabilir.")
            .Must(x => decimal.TryParse(x.ToString(), out _)).WithMessage("Çalışma süresi bir sayı olmalıdır.");
        
        RuleFor(x => x).Custom((request, context) =>
        {
            var totalUsage = request.ActiveUsageTime + request.RepairTime;
            if (totalUsage > 168)
            {
                context.AddFailure("Aktif kullanım süresi ve onarım süresi toplamı 168 saati geçemez.");
            }
        });

    }
}