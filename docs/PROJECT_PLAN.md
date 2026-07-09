# Queue Management System - Complete Project Plan

## Overview
A modular, multi-platform queue management system for regular queues without priority, VIP, or appointment-based handling.

## Architecture Stack

### Backend
- .NET 6+ / ASP.NET Core
- SQL Server Express
- Entity Framework Core
- SignalR for WebSocket
- JWT Authentication (Username/Password)

### Frontend
- React.js with Hooks
- Redux for state management
- Tailwind CSS for styling
- Chart.js for reporting
- Socket.IO for real-time updates

### Mobile
- Browser-based (Responsive Web)
- Android WebView (optional native)

### IoT
- ESP32 DevKit
- Arduino IDE / PlatformIO
- SSD1306 OLED display
- 4x4 Matrix Keypad

## Core Modules (11 Total)

1. **Queue Management** - Create, update, delete queues, numbering config
2. **Service Management** - Define services, assign to queues, track metrics
3. **Counter Management** - Register counters, assign services, track status
4. **Ticket Management** - Generate tickets, QR codes, status tracking
5. **Calling System** - Call next, skip, recall, close operations
6. **Display Management** - Counter displays, waiting area, real-time updates, voice announcements
7. **User Management** - Super Admin, Manager, Counter Staff, Ticket Operator + username/password auth
8. **Reporting & Analytics** - Dashboard stats, performance reports, PDF/Excel/CSV export
9. **Settings & Configuration** - Queue customization, ticket templates, working hours, holidays
10. **Backup & Recovery** - Automated backups, restore, data export
11. **Device Management** - Register IoT devices, monitor status, firmware updates

## Development Timeline

### Phase 1: MVP (2-3 months)
- Backend API core
- Admin dashboard
- ESP32 terminal
- Android displays
- Ticket dispenser integration
- User authentication & basic reporting

### Phase 2: Enhanced (2-3 months)
- Waiting area display with marketing carousel
- Advanced reporting with exports
- Voice announcement system
- Performance optimization
- Documentation & training

### Phase 3: Polish (1-2 months)
- Load testing & optimization
- Security hardening
- Production deployment
- Post-launch support

## Database Tables

- Users, Roles, RolePermissions
- Queues, Services, Counters
- Tickets, TicketHistory, TicketCalls
- Devices, SystemSettings, BackupHistory
- AuditLog

## Hardware Specifications

| Component | Specs |
|-----------|-------|
| **Server PC** | Windows Server 2019+, i5+, 8-16GB RAM, 256GB SSD |
| **Ticket Dispenser** | i3+, 4GB RAM, 80mm thermal printer (ESC/POS) |
| **Counter Display** | 10" Android tablet, WiFi, Wall mount |
| **Calling Terminal** | ESP32, 0.96" OLED, 4x4 Keypad, WiFi/POE |
| **Waiting Area** | 43-55" LED/LCD, 1920x1080, WiFi/LAN |

## API Endpoints (Summary)

- **Queue Management** - GET/POST /api/queues, GET/PUT/DELETE /api/queues/{id}
- **Ticket Management** - POST /api/tickets/generate, GET /api/tickets/queue/{queueId}
- **Calling System** - POST /api/calls/next, skip, recall
- **Counter Management** - GET/POST /api/counters
- **User Management** - POST /api/auth/login, GET/POST/PUT /api/users
- **Reporting** - GET /api/reports/dashboard, /export
- **Devices** - GET/POST /api/devices

## Real-time Events (WebSocket)

**Server → Clients**
- ticket:called
- ticket:skipped
- ticket:recalled
- counter:status-change
- device:connected/disconnected

**Client → Server**
- call:next
- call:skip
- call:recall
- counter:close
- display:subscribe
- device:heartbeat

## Key Performance Indicators

- Average wait time per queue
- Service completion rate
- Tickets served per hour
- Counter utilization percentage
- API response time (target < 200ms)
- System uptime (target > 99%)
- Real-time sync delay (target < 1s)

## Success Criteria

**Phase 1 (MVP)**
- All core features operational
- System uptime > 95%
- Real-time sync reliable
- All devices communicating

**Phase 2 (Enhanced)**
- Waiting area display operational
- All reports exportable
- System uptime > 99%
- User training completed

**Phase 3 (Production)**
- Zero critical bugs
- System uptime > 99.5%
- Full documentation
- Performance optimized

## Risk Management

| Risk | Impact | Mitigation |
|------|--------|----------|
| Hardware failure (ESP32, printer) | High | Spare parts, device redundancy |
| Network loss | High | Offline queue storage, auto-reconnect |
| Database corruption | Critical | Automated daily backups, test restore |
| Real-time sync delays | Medium | WebSocket optimization, load testing |
| Scope creep | Medium | Strict change control, MVP lock |

## Next Steps

1. Create database schema
2. Develop backend API
3. Build frontend dashboard
4. Integrate mobile displays
5. Develop ESP32 firmware
6. Implement ticket dispenser integration
7. Testing & deployment
