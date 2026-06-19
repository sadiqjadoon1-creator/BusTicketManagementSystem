# Bus Ticket Management System

A production-grade, enterprise-level Bus Ticket Management System built with .NET 6, ASP.NET Core, ADO.NET, and SQL Server.

## Project Overview

This system provides a complete solution for managing bus tickets, schedules, routes, bookings, and payments with advanced features like real-time chat support, segment-based seat allocation, and comprehensive reporting.

## Technology Stack

- **Backend**: .NET 6 Web API
- **Database**: SQL Server (ADO.NET - No Entity Framework)
- **Authentication**: ASP.NET Core Identity + JWT
- **Authorization**: Role-Based Access Control (RBAC) + Permission-Based
- **Architecture**: Clean Architecture (Domain, Application, Infrastructure, API)
- **Data Access**: Repository Pattern + Unit of Work with ADO.NET
- **Real-Time**: SignalR for Live Chat
- **Logging**: Serilog (Database & File)
- **API Documentation**: Swagger/OpenAPI
- **Mapping**: AutoMapper
- **Frontend**: Angular 16

## Core Modules

### 1. Authentication & Authorization
- User Registration/Login
- JWT Token Generation with Refresh Tokens
- Role-Based Access Control
- Permission-Based Authorization
- User Management (Admin)

### 2. Bus Management
- Create, Update, Delete Buses
- Bus Types (AC/Non-AC)
- Capacity Management
- Bus Scheduling

### 3. Route Management
- Multiple Stops Support
- Distance & Duration Calculation
- Segment-Based Routes
- Stop Management

### 4. Advanced Seat Management
- Segment-Based Seat Allocation
- Prevent Overlapping Bookings
- Partial Route Booking Support
- Real-Time Availability Checking

### 5. Booking System
- Book Tickets
- Cancel Tickets (with 48-hour refund rules)
- Prevent Double Booking (with Transactions)
- Booking Slip Generation

### 6. Payment Module
- Multiple Payment Statuses (Paid, Pending, Failed, Partial)
- Payment Processing
- Refund Management

### 7. Live Chat System (Real-Time)
- SignalR Integration
- Auto-Response Engine
- FAQ-Based Responses
- Chat History Storage
- Chat Analytics

### 8. Reporting Module
- 13+ Comprehensive Reports
- PDF & Excel Export
- Date-Wise Filtering
- Dashboard Analytics

### 9. User Manual
- Complete PDF Documentation
- Step-by-Step Guides
- UI Instructions

## Project Structure

```
BusTicketManagementSystem/
├── src/
│   ├── BusTicketManagement.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   └── Common/
│   ├── BusTicketManagement.Application/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Services/
│   │   └── Mappings/
│   ├── BusTicketManagement.Infrastructure/
│   │   ├── Data/
│   │   ├── Repository/
│   │   └── Persistence/
│   └── BusTicketManagement.API/
│       ├── Controllers/
│       ├── Middleware/
│       ├── Extensions/
│       └── Program.cs
├── database/
│   └── Scripts/
├── docs/
│   └── UserManual.pdf
├── frontend/
│   └── BusTicketUI/ (Angular 16)
└── tests/
```

## Key Features

### Authentication
- JWT Token with Refresh Token
- Token Expiration & Renewal
- Secure Password Hashing
- Account Lockout Policies

### Authorization
- Admin
- Staff (Manager, HR, Account)
- Customer

### Permissions
- CreateBus, UpdateBus, DeleteBus
- BookTicket, CancelTicket
- ViewReports, ViewForms
- User Management (Activate/Deactivate)

### Advanced Seat Management
- Segment-based allocation
- Prevent overlapping bookings
- Real-time availability
- Transaction-based booking

### Reporting (13+ Reports)
1. Reports Dashboard
2. Bookings Daily Report
3. Bookings Monthly Report
4. Bookings Route-Wise Report
5. Bookings Bus-Wise Report
6. Revenue Daily Report
7. Revenue Monthly Report
8. Revenue Annual Report
9. Driver Assignment History
10. Driver Performance Report
11. Hostess Assignment History
12. Seat Occupancy Rate
13. Bus Utilization Report

### Live Chat System
- Real-time messaging with SignalR
- Auto-response engine
- FAQ-based responses
- Context-aware replies
- Chat history & analytics
- PDF export of chat transcripts

## Setup Instructions

### Prerequisites
- .NET 6 SDK
- SQL Server 2019+
- Node.js 16+ (for Angular frontend)
- Visual Studio 2022 or VS Code

### Database Setup
1. Create a new SQL Server database
2. Run the database script: `database/Scripts/01_InitialSchema.sql`
3. Run stored procedures: `database/Scripts/02_StoredProcedures.sql`
4. Execute seed data: `database/Scripts/03_SeedData.sql`

### API Setup
1. Clone the repository
2. Update connection string in `appsettings.json`
3. Restore NuGet packages: `dotnet restore`
4. Build solution: `dotnet build`
5. Run migrations (if any)
6. Start API: `dotnet run`

### Frontend Setup
1. Navigate to frontend directory
2. Install dependencies: `npm install`
3. Start development server: `ng serve`
4. Access UI at `http://localhost:4200`

## API Endpoints

### Authentication
```
POST   /api/auth/register
POST   /api/auth/login
POST   /api/auth/refresh-token
```

### Admin
```
GET    /api/admin/users
POST   /api/admin/users
PUT    /api/admin/users/{id}
DELETE /api/admin/users/{id}
GET    /api/admin/roles
POST   /api/admin/permissions
```

### Buses
```
GET    /api/buses
POST   /api/buses
PUT    /api/buses/{id}
DELETE /api/buses/{id}
```

### Routes
```
GET    /api/routes
POST   /api/routes
GET    /api/routes/{id}/stops
```

### Schedules
```
GET    /api/schedules
POST   /api/schedules
GET    /api/schedules/{id}/seats
```

### Bookings
```
POST   /api/bookings
GET    /api/bookings/user/{userId}
GET    /api/bookings/{id}
POST   /api/bookings/{id}/cancel
GET    /api/bookings/{id}/slip
```

### Payments
```
POST   /api/payments
GET    /api/payments/{bookingId}
```

### Chat
```
POST   /api/chat/message
GET    /api/chat/history/{sessionId}
GET    /api/chat/faq
```

### Reports
```
GET    /api/reports/dashboard
GET    /api/reports/bookings/daily
GET    /api/reports/bookings/monthly
GET    /api/reports/revenue/daily
GET    /api/reports/occupancy
GET    /api/reports/export/pdf
GET    /api/reports/export/excel
```

## Database Design

### Core Tables
- AspNetUsers (extended identity)
- AspNetRoles
- AspNetUserRoles
- UserPermissions
- RolePermissions
- Buses
- Routes
- RouteStops
- Schedules
- Seats
- SeatSegmentBookings
- Bookings
- BookingDetails
- Payments
- ChatSessions
- ChatMessages
- ChatBotResponses
- Reports
- AuditLog

## Security Features

- Password hashing with bcrypt
- JWT token-based authentication
- Role & permission-based authorization
- Encrypted sensitive data
- SQL injection prevention (parameterized queries)
- Transaction management for critical operations
- Audit logging for all operations
- CORS configuration
- Rate limiting

## Error Handling

- Global exception middleware
- Centralized error response model
- Proper HTTP status codes
- Detailed error logging
- User-friendly error messages

## Logging

- Serilog integration
- Database logging
- File logging (daily rotation)
- Request/Response logging
- Error tracking
- Performance monitoring

## Response Model

All API responses follow a consistent format:

```json
{
  "success": true,
  "message": "Operation completed successfully",
  "data": {},
  "errors": [],
  "timestamp": "2024-01-01T00:00:00Z"
}
```

## Contributing

1. Create a feature branch
2. Commit your changes
3. Push to branch
4. Create a Pull Request

## License

This project is licensed under the MIT License.

## Support

For issues and questions, please create an issue in the repository.

---

**Built with ❤️ for Enterprise Solutions**
