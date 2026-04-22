# NotificationService Specification

## Purpose
Log notifications triggered by system business events in phase 1.

## Scope
- Consume integration events from other services
- Store notification logs in MongoDB
- Provide APIs to read notification logs

## Main entities
### Notification
- `Id` (Guid)
- `Type` (string)
- `Message` (string)
- `CreatedAt` (datetime)

## CQRS summary
- Query side only for API reads in phase 1
- Event handlers create notification records

## Out of scope
- Email integration
- SMS integration
- Push notifications
- User preference management
- Retry and escalation workflows
