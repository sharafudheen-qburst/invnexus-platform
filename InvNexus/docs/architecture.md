# InvNexus Architecture Overview

## Core Principles
- Microservices architecture
- Clean Architecture (layered design)
- CQRS (Command Query Responsibility Segregation)
- Custom mediator pattern (MediatR-like)

## Data & Persistence
- EF Core Code First
- SQL Server for transactional services
- MongoDB for Search and Notification (read models / logs)
- Unit of Work via DbContext
- Lightweight repository pattern
- Migration-based database evolution

## Communication
- HTTP/REST for synchronous service-to-service communication
- Event-driven communication (Pub/Sub style) for asynchronous updates

## Security
- JWT-based authentication
- JWT validation across services
- Role-based authorization

## Frontend
- Angular UI
- Tailwind CSS for styling

## Development Approach
- API-first design using markdown documentation
- Monorepo structure with multiple services
- Consistent patterns across services (Inventory as reference)

## Design Highlights
- Separation of read and write models (CQRS)
- Denormalized read models (SearchService)
- Scalable and extensible service structure