# InvNexus Angular UI - Project Setup Guide

## Project Structure Created

```
invnexus-webui/
├── src/
│   ├── app/
│   │   ├── components/
│   │   │   ├── login/
│   │   │   │   └── login.component.ts          # Login page with form
│   │   │   ├── dashboard/
│   │   │   │   └── dashboard.component.ts      # Dashboard with navigation cards
│   │   │   ├── product/
│   │   │   │   └── product.component.ts        # Product create/list page
│   │   │   ├── stock/
│   │   │   │   └── stock.component.ts          # Stock management page
│   │   │   └── shared/
│   │   │       └── navbar/
│   │   │           └── navbar.component.ts     # Navigation bar (shown when authenticated)
│   │   ├── services/
│   │   │   ├── auth.service.ts                 # Authentication service
│   │   │   └── inventory.service.ts            # Inventory/Products API service
│   │   ├── guards/
│   │   │   ├── auth.guard.ts                   # Route protection guard
│   │   │   └── auth.interceptor.ts             # HTTP interceptor for JWT token
│   │   ├── app.component.ts                    # Root component
│   │   ├── app.config.ts                       # App configuration & providers
│   │   └── app.routes.ts                       # Route definitions
│   ├── environments/
│   │   ├── environment.ts                      # Development config
│   │   └── environment.prod.ts                 # Production config
│   ├── styles.css                              # Global Tailwind styles
│   ├── main.ts                                 # Bootstrap entry point
│   └── index.html                              # HTML template
├── angular.json                                # Angular CLI configuration
├── tsconfig.json                               # TypeScript configuration
├── tsconfig.app.json                           # App-specific TypeScript config
├── package.json                                # Dependencies
├── tailwind.config.js                          # Tailwind configuration
├── postcss.config.js                           # PostCSS configuration
├── .gitignore                                  # Git ignore rules
├── .browserslistrc                             # Browser support list
├── README.md                                   # Project documentation
└── SETUP.md                                    # This file
```

## Installation & Setup

### Step 1: Install Dependencies

```bash
cd invnexus-webui
npm install
```

This installs:
- Angular 18 framework
- TypeScript compiler
- Tailwind CSS for styling
- Autoprefixer for CSS vendor prefixes
- All peer dependencies

### Step 2: Configure API Base URL (if needed)

Edit `src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiBaseUrl: 'http://localhost:5000/api'  // Update this to your API URL
};
```

### Step 3: Start Development Server

```bash
npm start
```

The application will be available at: **http://localhost:4200**

The dev server automatically reloads when you save changes.

## Application Architecture

### Authentication Flow

1. **Login Page** (`/login`)
   - User enters email and password
   - AuthService calls `POST /api/auth/login`
   - JWT token received and stored in localStorage
   - User redirected to dashboard

2. **Route Guard** (AuthGuard)
   - All internal routes check authentication status
   - Unauthenticated users redirected to login page
   - Protected routes: `/dashboard`, `/products`, `/stock`

3. **HTTP Interceptor** (AuthInterceptor)
   - Automatically adds `Authorization: Bearer <token>` header to all API requests
   - No manual token management needed in components

4. **Logout**
   - Token removed from localStorage
   - User redirected to login page

### Page Components

#### Login Page (`/login`)
- Email input (with validation)
- Password input (with validation)
- Login button
- Error message display
- Success flow stores JWT token

#### Dashboard (`/dashboard`)
- Welcome/landing page after login
- Navigation cards for:
  - Products page
  - Stock page
- Simple, clean layout

#### Products Page (`/products`)
- **Create Product Form:**
  - Product name (required, min 2 chars)
  - Price (required, min 0)
  - Active checkbox
  - Form validation
  - Success/error messages
  - Calls `POST /api/inventory/products`

- **Products Table:**
  - Lists all products
  - Columns: Name, Price, Active Status
  - Color-coded status badges
  - Calls `GET /api/inventory/products`

#### Stock Page (`/stock`)
- **Product Filter:**
  - Dropdown to select product
  - Shows current stock for selected product
  - Calls `GET /api/inventory/stock/:productId`

- **Stock Table:**
  - All products with stock levels
  - Columns: Product Name, Quantity, Status
  - Color-coded status:
    - Green: In Stock (>10 units)
    - Yellow: Low Stock (1-10 units)
    - Red: Out of Stock (0 units)
  - Calls `GET /api/inventory/stock`

### Services

#### AuthService
Methods:
- `login(credentials)` - Call login API
- `setToken(token)` - Store JWT token
- `getToken()` - Retrieve stored token
- `hasToken()` - Check if token exists
- `removeToken()` - Clear token
- `isAuthenticated()` - Check auth status
- `logout()` - Clear auth state

Observable:
- `isAuthenticated$` - Observable of auth state changes

#### InventoryService
Product Methods:
- `getProducts()` - GET /api/inventory/products
- `getProduct(id)` - GET /api/inventory/products/:id
- `createProduct(product)` - POST /api/inventory/products
- `updateProduct(id, product)` - PUT /api/inventory/products/:id
- `deleteProduct(id)` - DELETE /api/inventory/products/:id

Stock Methods:
- `getStock()` - GET /api/inventory/stock
- `getStockByProduct(productId)` - GET /api/inventory/stock/:productId
- `updateStock(productId, quantity)` - PUT /api/inventory/stock/:productId

## Expected API Endpoints

Your backend API should implement these endpoints:

### Authentication
```
POST /api/auth/login
Request: { email: string, password: string }
Response: { token: string, user?: any }
```

### Products
```
GET /api/inventory/products
Response: Product[]

POST /api/inventory/products
Request: { name: string, price: number, isActive: boolean }
Response: Product (with id)

GET /api/inventory/products/:id
Response: Product

PUT /api/inventory/products/:id
Request: { name: string, price: number, isActive: boolean }
Response: Product

DELETE /api/inventory/products/:id
Response: void
```

### Stock
```
GET /api/inventory/stock
Response: Stock[]

GET /api/inventory/stock/:productId
Response: { productId: string, productName: string, quantity: number }

PUT /api/inventory/stock/:productId
Request: { quantity: number }
Response: Stock
```

## Build for Production

```bash
npm run build:prod
```

This creates optimized build in `dist/invnexus-webui/` ready for deployment.

## Key Features Implemented

✅ **Authentication**
- Login form with email/password validation
- JWT token management
- Protected routes with AuthGuard
- HTTP interceptor for automatic token attachment
- Logout functionality

✅ **Product Management**
- Create products with validation
- View all products in table format
- Form validation (name, price)
- Success/error messages
- API error handling

✅ **Stock Management**
- View all stock levels
- Filter by product
- Color-coded status indicators
- Table display of all products

✅ **UI/UX**
- Tailwind CSS for clean, professional styling
- Responsive design (desktop & mobile)
- Consistent color scheme
- Error and success notifications
- Loading states
- Form validation with user feedback

✅ **Architecture**
- Standalone components (Angular 18)
- Service-based architecture
- Route guards for security
- HTTP interceptors for auth
- Environment-based configuration
- Modular, extensible structure

## Testing the Application

1. **Start Development Server**
   ```bash
   npm start
   ```

2. **Navigate to http://localhost:4200**

3. **Test Login Flow**
   - You'll be redirected to `/login`
   - Try invalid credentials (should show error)
   - Use credentials from your API

4. **Test Dashboard**
   - After login, you'll see dashboard with navigation cards
   - Navbar appears at top with logout button

5. **Test Products Page**
   - Create a new product
   - Check that it appears in the table
   - Verify API calls in browser DevTools Network tab

6. **Test Stock Page**
   - View all stock levels
   - Select a product from dropdown
   - Verify stock details display

7. **Test Authentication**
   - Click Logout in navbar
   - Verify redirect to login
   - Try accessing /products directly (should redirect to login)

## Troubleshooting

### Port 4200 already in use
```bash
ng serve --port 4300
```

### CORS Issues
Add CORS headers to your backend API or configure in proxy

### API Connection Failed
- Check `apiBaseUrl` in `src/environments/environment.ts`
- Ensure backend is running
- Check browser console for error details

### Styles Not Applied
- Clear browser cache
- Restart dev server
- Check Tailwind configuration

## Next Steps

To extend this application:
1. Add more components as needed
2. Implement product update/delete functionality
3. Add pagination and sorting to tables
4. Implement stock update functionality
5. Add user profile page
6. Implement role-based access control
7. Add audit logging
8. Implement more sophisticated error handling

## Support

For issues or questions, check:
1. Browser DevTools Console for errors
2. Network tab for API request/response
3. Angular documentation: https://angular.io
4. Tailwind CSS documentation: https://tailwindcss.com
