# Bus Ticket Management System - API Documentation

## Authentication Endpoints

### Register
```
POST /api/auth/register
Content-Type: application/json

{
  "userName": "john_doe",
  "email": "john@example.com",
  "password": "Password123!",
  "firstName": "John",
  "lastName": "Doe",
  "phoneNumber": "1234567890"
}

Response:
{
  "success": true,
  "message": "User registered successfully",
  "data": {
    "userId": "user-123",
    "email": "john@example.com"
  },
  "timestamp": "2024-01-01T00:00:00Z"
}
```

### Login
```
POST /api/auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "Password123!"
}

Response:
{
  "success": true,
  "message": "Login successful",
  "data": {
    "accessToken": "eyJhbGciOiJIUzI1NiIs...",
    "refreshToken": "eyJhbGciOiJIUzI1NiIs...",
    "userId": "user-123",
    "roles": ["Customer"]
  },
  "timestamp": "2024-01-01T00:00:00Z"
}
```

## Bus Endpoints

### Get All Buses
```
GET /api/buses
Authorization: Bearer {token}

Response:
{
  "success": true,
  "message": "Buses retrieved successfully",
  "data": [
    {
      "busId": 1,
      "busNo": "BUS001",
      "totalCapacity": 50,
      "currentOccupancy": 25
    }
  ]
}
```

### Create Bus
```
POST /api/buses
Authorization: Bearer {admin-token}
Content-Type: application/json

{
  "busNo": "BUS001",
  "busTypeId": 1,
  "totalCapacity": 50,
  "registrationNumber": "XYZ-123"
}
```

## Booking Endpoints

### Create Booking
```
POST /api/bookings
Authorization: Bearer {token}
Content-Type: application/json

{
  "scheduleId": 1,
  "selectedSeatIds": [1, 2, 3]
}
```

### Get User Bookings
```
GET /api/bookings/user/{userId}
Authorization: Bearer {token}
```

### Cancel Booking
```
POST /api/bookings/{bookingId}/cancel
Authorization: Bearer {token}
Content-Type: application/json

{
  "cancellationReason": "Change of plans"
}
```

## Payment Endpoints

### Process Payment
```
POST /api/payments
Authorization: Bearer {token}
Content-Type: application/json

{
  "bookingId": 1,
  "paymentMethodId": 1,
  "amount": 5000
}
```

## Report Endpoints

### Get Dashboard Report
```
GET /api/reports/dashboard
Authorization: Bearer {admin-token}
```

### Export Report
```
GET /api/reports/export/pdf?reportType=bookings&dateFrom=2024-01-01&dateTo=2024-01-31
Authorization: Bearer {admin-token}
```
