# InvNexus Frontend (Angular UI)

## Overview
The frontend of InvNexus is built using Angular.  
It provides a simple, clean UI to interact with backend microservices.

The UI communicates with services using HTTP APIs and JWT-based authentication.

---

## Tech Stack
- Angular
- Tailwind CSS
- TypeScript
- Angular Router
- HTTP Client

---

## Purpose (Phase 1)
- User login
- Basic navigation
- Product management
- Stock viewing

---

## Pages

### 1. Login Page
- Email and password input
- Calls AuthService:
  - `POST /api/auth/login`
- Stores JWT token after successful login

---

### 2. Dashboard
- Landing page after login
- Simple navigation cards/links:
  - Products
  - Stock

---

### 3. Product Page
- Create product form:
  - Name
  - Price
  - IsActive
- List products
- APIs used:
  - `POST /api/products`
  - `GET /api/products`

---

### 4. Stock Page
- Select product or enter productId
- View stock quantity
- API used:
  - `GET /api/stock/{productId}`

---

## Authentication Flow

1. User logs in via Login page
2. AuthService returns JWT token
3. Token is stored in browser (memory/local storage)
4. HTTP interceptor attaches token to all API requests:


1. 
## Project Structure


ui/invnexus-webui/
src/app/
auth/
dashboard/
products/
stock/
core/
services/
guards/
interceptors/


---

## Communication

Frontend communicates with backend using HTTP:

- AuthService
- InventoryService
- PurchaseService (later)
- SalesService (later)

---

## Design Guidelines

- Simple, clean UI
- Minimal styling using Tailwind
- No complex UI components
- Focus on functionality, not design

---

## Out of Scope (Phase 1)

- Signup
- Advanced dashboards
- Charts/analytics
- State management libraries
- Complex UI components
- Notifications UI
- Real-time updates

---

## Future Enhancements

- Purchase and Sales UI
- Notifications view
- Search page
- Better UI components
- Role-based UI visibility