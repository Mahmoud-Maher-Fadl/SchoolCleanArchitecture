using MediatR;

namespace Application.Company.Notifications;

public record NewCompanyCreatedNotification(Domain.Tenant.School School) : INotification;