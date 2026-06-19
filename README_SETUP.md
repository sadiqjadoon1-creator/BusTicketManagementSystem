# Bus Ticket Management System - Setup Instructions

## Prerequisites
- .NET 6 SDK or later
- SQL Server 2019 or later
- Node.js 16+ (for Angular)
- Visual Studio 2022 or VS Code

## Backend Setup (.NET 6)

### 1. Database Setup
1. Open SQL Server Management Studio
2. Run the script from `database/01_InitialSchema.sql`
3. Verify tables are created successfully

### 2. API Setup
1. Navigate to `src/BusTicketManagement.API`
2. Update connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=BusTicketManagementDB;Trusted_Connection=true;TrustServerCertificate=true;"
   }
   ```
3. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
4. Build the solution:
   ```bash
   dotnet build
   ```
5. Run the API:
   ```bash
   dotnet run
   ```
6. Swagger will be available at: `https://localhost:5001/swagger`

## Frontend Setup (Angular 16)

### 1. Install Dependencies
```bash
cd frontend
npm install
```

### 2. Configure API URL
Update `src/app/services/api.service.ts` with your API URL:
```typescript
private apiUrl = 'http://localhost:5000/api'; // Update port if needed
```

### 3. Run Development Server
```bash
npm start
```
The application will be available at: `http://localhost:4200`

## Features Implemented

✅ Complete Domain Entities
✅ Application Layer with DTOs
✅ Infrastructure with ADO.NET Repositories
✅ API Controllers with Swagger
✅ JWT Authentication
✅ Angular 16 UI with Routing
✅ Responsive Design with Bootstrap
✅ Chat Module Foundation
✅ Database Schema

## Project Structure

```
BusTicketManagementSystem/
├── src/
│   ├── BusTicketManagement.Domain/
│   ├── BusTicketManagement.Application/
│   ├── BusTicketManagement.Infrastructure/
│   └── BusTicketManagement.API/
├── frontend/
│   ├── src/
│   │   ├── app/
│   │   │   ├── pages/
│   │   │   ├── services/
│   │   │   └── interceptors/
│   │   └── assets/
│   ├── angular.json
│   └── package.json
├── database/
│   └── 01_InitialSchema.sql
├── docs/
│   └── API_DOCUMENTATION.md
└── README.md
```

## Next Steps

1. Implement all service methods
2. Add unit tests
3. Implement chat module with SignalR
4. Add reporting module
5. Complete Angular UI pages
6. Add PDF export functionality
7. Implement payment processing

## Support

For issues and questions, please create an issue in the repository.
