# InvNexus Platform

InvNexus Platform is a microservices-based inventory, procurement, 
and sales management system built using Clean Architecture, CQRS,
JWT authentication, event-driven communication, SQL, and MongoDB.

## Purpose
This project demonstrates how to build a scalable enterprise-style system with separate services for
authentication, inventory, purchasing, sales, notifications, and search.

## Tech Stack
- .NET 8 Web API
- Clean Architecture
- CQRS
- JWT Authentication
- SQL Server
- MongoDB
- Message Broker
- React or Angular UI
- Docker (later)

## High-Level Modules

### UI
- InvNexus.WebUI
- Purpose: user interface for login, product management, purchase, inventory, and sales operations

### Backend Services
- InvNexus.AuthService
- InvNexus.InventoryService
- InvNexus.PurchaseService
- InvNexus.SalesService
- InvNexus.NotificationService
- InvNexus.SearchService

## Service and Database Split

| Service | Responsibility | Database |
|--------|---------------|----------|
| InvNexus.AuthService | Authentication, JWT, roles, refresh token | SQL Server |
| InvNexus.InventoryService | Stock ledger, available stock, reservation, stock updates | SQL Server |
| InvNexus.PurchaseService | Purchase order, goods receipt, supplier purchase flow | SQL Server |
| InvNexus.SalesService | Sales order, sales completion, stock deduction trigger | SQL Server |
| InvNexus.NotificationService | Notification logs, email/push event processing | MongoDB |
| InvNexus.SearchService | Read models, dashboard search, aggregated query views | MongoDB |


## Architecture Style
- Microservices-based design
- Clean Architecture inside each service
- CQRS for command and query separation
- Event-driven communication between services
- Separate transactional and read-optimized data stores

## Example Business Flow
1. User logs in through AuthService
2. User creates a purchase order in PurchaseService
3. After goods receipt, PurchaseService publishes an event
4. InventoryService consumes the event and increases stock
5. SearchService updates read models for fast search
6. NotificationService sends purchase completion notification
7. When a sale is completed, SalesService publishes an event
8. InventoryService reduces stock accordingly

## Why SQL and MongoDB
### SQL Server
Used for transactional business data:
- authentication data
- purchase orders
- inventory stock records
- sales records

### MongoDB
Used for flexible and read-optimized data:
- notification logs
- search documents
- denormalized query models
- dashboard views

## Planned Features
- Product catalog
- Purchase order management
- Inventory tracking
- Sales order management
- JWT-based authentication
- Event-driven stock updates
- Search and dashboard read model
- Notification processing

## Future Enhancements
- API Gateway
- Docker and containerization
- Redis caching
- AI-based reorder suggestion
- low stock alert prediction
- audit logging dashboard

## Project Structure
/docs
/services
/ui

## Documentation
- architecture.md
- api-spec.md
- decisions.md
- flow diagrams
