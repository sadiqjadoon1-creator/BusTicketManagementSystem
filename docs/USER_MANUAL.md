# Bus Ticket Management System - User Manual

## Table of Contents
1. [Getting Started](#getting-started)
2. [Login Guide](#login-guide)
3. [Dashboard Overview](#dashboard-overview)
4. [Booking Process](#booking-process)
5. [Payment Process](#payment-process)
6. [Reports Module](#reports-module)
7. [Chat Support](#chat-support)
8. [Admin Panel](#admin-panel)
9. [Troubleshooting](#troubleshooting)

---

## Getting Started

The Bus Ticket Management System is a comprehensive platform for booking bus tickets, managing schedules, and generating reports.

### Prerequisites
- Modern web browser (Chrome, Firefox, Safari, Edge)
- Internet connection
- Valid email address

### System Access
- **URL**: http://localhost:4200
- **API**: http://localhost:5000/api
- **Swagger Documentation**: http://localhost:5000/swagger

---

## Login Guide

### First Time Users

1. Click on "Register" button on the home page
2. Fill in the registration form:
   - First Name: [Your first name]
   - Last Name: [Your last name]
   - Email: [Your email address]
   - Username: [Create a unique username]
   - Password: [Create a strong password]
   - Confirm Password: [Re-enter password]
   - Phone Number: [Your 10-digit phone number]
3. Click "Register" button
4. You will receive a confirmation message
5. Navigate to login page

### Returning Users

1. Enter your email address
2. Enter your password
3. Click "Login" button
4. You will be redirected to the dashboard

### [SCREENSHOT-LOGIN]

---

## Dashboard Overview

The dashboard is your home page after login. It displays:

### Dashboard Components

1. **Navigation Bar**
   - Home link
   - Search Buses link
   - My Bookings link
   - Reports link
   - Profile dropdown
   - Logout button

2. **Dashboard Cards**
   - Quick Book: Direct access to booking
   - My Tickets: View your bookings
   - Support: Access live chat
   - Help: Frequently asked questions

3. **Featured Routes**
   - Popular routes with best prices
   - One-click booking option

4. **Recent Bookings**
   - Last 5 bookings summary
   - Quick access to booking details

### [SCREENSHOT-DASHBOARD]

---

## Booking Process

### Step 1: Search for Buses

1. Click "Search Buses" in navigation menu or "Quick Book" card
2. Fill in search criteria:
   - **From**: Select departure city
   - **To**: Select destination city
   - **Date**: Select travel date (click calendar icon)
   - **Passengers**: Select number of passengers
3. Click "Search" button
4. System displays available buses

### Step 2: Select Bus

The search results display:
- Bus number (e.g., BUS001)
- Bus type (AC/Non-AC)
- Departure and arrival times
- Available seats count
- Fare per passenger
- "Select" button

1. Review bus details
2. Click "Select" button to proceed

### Step 3: Choose Seats

1. Seat map is displayed
   - Green = Available
   - Red = Booked
   - Yellow = Selected

2. Click on desired seats to select them
3. Selected seats show booking cost
4. Click "Confirm Seat Selection"

### Step 4: Review Booking

Review page shows:
- Route details
- Passenger names fields
- Seat numbers
- Fare breakdown
  - Base fare
  - Taxes
  - Total amount

1. Enter passenger names
2. Review total cost
3. Click "Proceed to Payment"

### [SCREENSHOT-BOOKING]

---

## Payment Process

### Payment Methods

We accept:
1. **Credit Card**
2. **Debit Card**
3. **Bank Transfer**
4. **Mobile Wallet** (JazzCash, Easypaisa)
5. **Cash Payment** (at counter)

### Payment Steps

1. Select payment method
2. Enter payment details:
   - For cards: Card number, expiry, CVV
   - For wallets: Account number
3. Review total amount
4. Click "Pay Now" button
5. Complete payment verification
6. You will receive confirmation

### Payment Confirmation

After successful payment:
- Booking reference number is provided
- Confirmation email is sent
- Booking slip is displayed
- Download or print booking slip

### [SCREENSHOT-PAYMENT]

---

## Reports Module

### Accessing Reports

1. Click "Reports" in navigation menu (Admin users only)
2. Dashboard loads with available reports

### Available Reports

#### 1. Dashboard Report
- Total bookings count
- Total revenue
- Active schedules
- Occupancy rate
- Recent bookings table

#### 2. Revenue Reports
- Daily revenue
- Monthly revenue
- Annual revenue
- Trend analysis

#### 3. Occupancy Reports
- Bus utilization rate
- Seat occupancy percentage
- Peak hours analysis
- Route-wise occupancy

#### 4. Booking Reports
- Total bookings
- Bookings by route
- Bookings by bus
- Cancellation statistics

### Exporting Reports

1. Select desired report from dropdown
2. Click "Export PDF" or "Export Excel"
3. File will download automatically
4. Open with PDF reader or Excel

### [SCREENSHOT-REPORTS]

---

## Chat Support

### Accessing Chat

1. Look for chat icon in bottom-right corner
2. Click to open chat window
3. Chat window opens with conversation history

### Chat Features

**Instant Responses**
- Booking process questions
- Cancellation policy inquiries
- Price and schedule information
- Seat availability questions
- Payment method inquiries
- Refund policy questions

### Common Questions & Auto-Responses

#### Q: How do I book a ticket?
A: "To book a ticket: 1) Select your source and destination 2) Choose a date 3) Select a bus schedule 4) Pick your seat(s) 5) Review and confirm booking 6) Make payment."

#### Q: What is the cancellation policy?
A: "Ticket cancellation is allowed up to 48 hours before departure. Cancellations made within 48 hours will incur a 30% deduction from the ticket fare."

#### Q: How long for refunds?
A: "Refunds are typically processed within 5-7 business days to the original payment method."

### [SCREENSHOT-CHAT]

---

## Admin Panel

### Accessing Admin Panel

Admin users can access:
1. User Management
2. Bus Management
3. Route Management
4. Schedule Management
5. Reports & Analytics
6. Settings

### User Management

**View Users**
1. Click "Users" in admin menu
2. View all registered users
3. Search by name/email
4. Filter by role

**Add New User**
1. Click "Add User" button
2. Fill user details
3. Assign role (Admin, Manager, Customer)
4. Assign permissions
5. Click "Save"

**Edit User**
1. Click edit icon next to user
2. Modify details
3. Click "Update"

**Deactivate User**
1. Click deactivate icon
2. User account is disabled
3. User cannot login

### Bus Management

**Add Bus**
1. Click "Buses" in admin menu
2. Click "Add Bus" button
3. Fill bus details:
   - Bus Number
   - Bus Type (AC/Non-AC)
   - Total Capacity
   - Registration Number
   - Manufacturer Model
   - Year of Manufacture
4. Click "Save"

**Edit Bus**
1. Click edit icon
2. Modify details
3. Click "Update"

**View Bus Details**
1. Click bus from list
2. View full details
3. View maintenance schedule
4. View active schedules

### Route Management

**Create Route**
1. Click "Routes" menu
2. Click "Create Route" button
3. Fill route details:
   - Route Name
   - Source City
   - Destination City
   - Total Distance
   - Estimated Duration
   - Base Fare
4. Click "Save"

**Add Route Stops**
1. Select route
2. Click "Add Stops" button
3. Add city stops in sequence
4. Save stops

### Schedule Management

**Create Schedule**
1. Click "Schedules" menu
2. Click "Create Schedule"
3. Select Bus and Route
4. Set departure and arrival times
5. Select date
6. Assign driver and hostess
7. Click "Save"

**View Active Schedules**
1. View all upcoming schedules
2. Update schedule status
3. Manage seat allocation
4. View booking details

### [SCREENSHOT-ADMIN]

---

## Troubleshooting

### Common Issues

#### 1. Cannot Login
**Problem**: Invalid email or password
**Solution**:
- Check email spelling
- Verify password
- Use "Forgot Password" to reset
- Contact support

#### 2. Booking Not Processing
**Problem**: Booking stuck or not completing
**Solution**:
- Check internet connection
- Clear browser cache
- Try different browser
- Refresh and try again
- Contact support

#### 3. Payment Failed
**Problem**: Payment transaction rejected
**Solution**:
- Check payment method validity
- Verify card/account details
- Try different payment method
- Contact your bank
- Retry payment

#### 4. Cannot Download Reports
**Problem**: Export not working
**Solution**:
- Check browser settings
- Enable pop-ups if blocked
- Try different browser
- Check file permissions
- Contact support

#### 5. Chat Not Responding
**Problem**: Auto-response not working
**Solution**:
- Refresh chat window
- Clear browser cache
- Ensure JavaScript is enabled
- Try different browser
- Contact support

### Contact Support

**Email**: support@busticket.com
**Phone**: +92-XXX-XXXXXXX
**Live Chat**: Available 24/7 (via chat widget)
**Office Hours**: Monday-Friday, 9 AM - 6 PM

---

## FAQ

### General Questions

**Q: Is the system secure?**
A: Yes, we use HTTPS encryption, JWT tokens, and secure payment gateways.

**Q: What browsers are supported?**
A: Chrome, Firefox, Safari, Edge (latest versions).

**Q: Can I book multiple tickets at once?**
A: Yes, you can select multiple seats in one transaction.

**Q: Is there a mobile app?**
A: Yes, our responsive design works on mobile browsers.

---

## Appendix: System Architecture

### Technology Stack
- **Backend**: .NET 6 Web API
- **Database**: SQL Server
- **Frontend**: Angular 16
- **Real-time Chat**: SignalR
- **Authentication**: JWT Tokens
- **ORM**: ADO.NET (No Entity Framework)

### Key Features
- Clean Architecture (Domain, Application, Infrastructure, API)
- Repository Pattern with Unit of Work
- Role-Based Access Control (RBAC)
- Permission-Based Authorization
- Global Exception Handling
- Serilog Logging
- AutoMapper for DTOs
- Swagger/OpenAPI Documentation

---

**Last Updated**: June 2024
**Version**: 1.0.0
**Support**: support@busticket.com
