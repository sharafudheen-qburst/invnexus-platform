# InvNexus Web UI

A clean, professional Angular application for inventory management.

## Quick Start

### Prerequisites
- Node.js 18+
- npm or yarn

### Installation

```bash
npm install
```

### Development Server

```bash
npm start
```

Navigate to `http://localhost:4200/`. The application will automatically reload when you make code changes.

### Build

```bash
npm run build
```

The build artifacts will be stored in the `dist/` directory.

## Project Structure

```
src/
├── app/
│   ├── components/
│   │   ├── login/              # Login page
│   │   ├── dashboard/          # Dashboard landing page
│   │   ├── product/            # Product management page
│   │   ├── stock/              # Stock management page
│   │   └── shared/
│   │       └── navbar/         # Navigation bar
│   ├── services/
│   │   ├── auth.service.ts     # Authentication service
│   │   └── inventory.service.ts # Inventory API service
│   ├── guards/
│   │   ├── auth.guard.ts       # Route protection guard
│   │   └── auth.interceptor.ts # HTTP interceptor for JWT
│   ├── app.component.ts        # Root component
│   ├── app.config.ts           # Application configuration
│   └── app.routes.ts           # Route definitions
├── environments/               # Environment configurations
├── styles.css                  # Global styles
├── main.ts                     # Application entry point
└── index.html                  # HTML template
```

## Features

### Authentication
- Login form with email and password
- JWT token storage in localStorage
- Automatic token attachment to API requests
- Protected routes with auth guard
- Logout functionality

### Products
- Create new products with name, price, and active status
- View all products in a table
- Form validation

### Stock Management
- View all product stock levels
- Filter stock by product
- Color-coded stock status (In Stock, Low Stock, Out of Stock)

### Design
- Tailwind CSS for responsive styling
- Clean, minimal UI
- Professional appearance
- Mobile-friendly responsive layout

## API Integration

The application expects the following API endpoints:

### Authentication
- `POST /api/auth/login` - Login with email and password

### Inventory
- `GET /api/inventory/products` - Get all products
- `POST /api/inventory/products` - Create a new product
- `GET /api/inventory/products/:id` - Get product details
- `PUT /api/inventory/products/:id` - Update product
- `DELETE /api/inventory/products/:id` - Delete product

- `GET /api/inventory/stock` - Get all stock levels
- `GET /api/inventory/stock/:productId` - Get stock for a product
- `PUT /api/inventory/stock/:productId` - Update stock quantity

## Configuration

API base URL is configured in `src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiBaseUrl: 'http://localhost:5000/api'
};
```

Update this for your backend API endpoint.

## Login Demo

For testing purposes, use these credentials:
- Email: test@example.com
- Password: password123

## Technologies

- **Angular** 18 - Frontend framework
- **TypeScript** - Programming language
- **Tailwind CSS** - Utility-first CSS framework
- **RxJS** - Reactive programming library
- **Reactive Forms** - Form handling

## Notes

- All internal pages (Dashboard, Products, Stock) are protected by the AuthGuard
- The navbar only displays when authenticated
- JWT tokens are automatically included in all API requests via the HTTP interceptor
- Forms include validation and error handling
- Clean, modular component structure for easy extension

## Future Enhancements

- Add product update/delete functionality
- Implement stock update functionality
- Add pagination to tables
- Add sorting and filtering
- User profile page
- Audit logging
