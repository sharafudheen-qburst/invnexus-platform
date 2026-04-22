# NotificationService Database

## Database type
- MongoDB

## Collections
- `Notifications`

## Important fields
### `Notifications`
- `Id` (Guid)
- `Type` (string)
- `Message` (string)
- `CreatedAt` (datetime)

## Relationships
- No relational joins in phase 1
- Each notification document is independent

## Notes for first version
- Store only system-generated notification logs
- Keep schema simple and append-only
- No delivery status tracking
