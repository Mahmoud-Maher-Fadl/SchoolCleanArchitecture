using Application.Common;
using Domain.common;
using MediatR;

namespace Application.Company.Notifications;

public class Handler : INotificationHandler<NewCompanyCreatedNotification>
{
    private readonly IMigrationService _migrationService;

    public Handler(IMigrationService migrationService)
    {
        _migrationService = migrationService;
    }

    public async Task Handle(NewCompanyCreatedNotification notification, CancellationToken cancellationToken)
    {
        await _migrationService.CreateAndMigrateDatabaseAsync(notification.School.Id, cancellationToken);
    }
}