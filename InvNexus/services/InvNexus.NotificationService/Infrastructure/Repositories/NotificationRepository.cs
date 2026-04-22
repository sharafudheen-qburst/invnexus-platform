using InvNexus.NotificationService.Application.Interfaces;
using InvNexus.NotificationService.Domain.Entities;
using InvNexus.NotificationService.Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace InvNexus.NotificationService.Infrastructure.Repositories;

public class NotificationRepository(
    IMongoClient mongoClient,
    IOptions<MongoDbSettings> mongoDbSettings) : INotificationRepository
{
    private readonly IMongoCollection<Notification> _notificationsCollection = mongoClient
        .GetDatabase(mongoDbSettings.Value.DatabaseName)
        .GetCollection<Notification>(mongoDbSettings.Value.NotificationsCollectionName);

    public async Task AddAsync(Notification notification, CancellationToken cancellationToken)
    {
        await _notificationsCollection.InsertOneAsync(notification, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<Notification>> GetAllAsync(CancellationToken cancellationToken)
    {
        var notifications = await _notificationsCollection
            .Find(FilterDefinition<Notification>.Empty)
            .SortByDescending(notification => notification.CreatedAt)
            .ToListAsync(cancellationToken);

        return notifications;
    }

    public async Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _notificationsCollection
            .Find(notification => notification.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
