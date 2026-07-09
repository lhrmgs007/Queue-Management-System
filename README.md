# Queue Management System

A comprehensive, modular queue management system with Windows server backend, Android tablets for counter displays, ESP32 calling terminals, and ticket dispensers.

## 🎯 Features

### Core Modules
- **Queue Management** - Create and manage queues
- **Service Management** - Define services and assign to queues
- **Counter Management** - Register and manage service counters
- **Ticket Management** - Generate tickets with QR codes
- **Calling System** - Call next, skip, recall, close operations
- **Display Management** - Real-time counter and waiting area displays
- **User Management** - Super Admin, Manager, Counter Staff, Ticket Operator
- **Reporting & Analytics** - Dashboard stats, reports, PDF/Excel/CSV export
- **Settings & Configuration** - Customize queues, tickets, working hours
- **Backup & Recovery** - Automated backups and restore
- **Device Management** - Register and monitor IoT devices

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────┐
│              Windows Server (Backend)               │
│  ┌──────────────────────────────────────────────┐  │
│  │  ASP.NET Core API (Port 5000)                │  │
│  │  - Queue Management                          │  │
│  │  - Ticket Generation                         │  │
│  │  - Real-time WebSocket (SignalR)             │  │
│  └──────────────────────────────────────────────┘  │
│  ┌──────────────────────────────────────────────┐  │
│  │  SQL Server Express                          │  │
│  │  - Queue Database                            │  │
│  │  - User Management                           │  │
│  │  - Reporting & Analytics                     │  │
│  └──────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────┘
                         │
         ┌───────────────┼───────────────┐
         │               │               │
    ┌────────────┐ ┌─────────────┐ ┌──────────────┐
    │   ESP32    │ │   Tablets   │ │  Dispenser   │
    │ Terminal   │ │  (Android)  │ │    (Win PC)  │
    │ WiFi/POE   │ │   WiFi      │ │  Printer     │
    └────────────┘ └─────────────┘ └──────────────┘

    Also connected via LAN/WiFi:
    ┌──────────────┐
    │ Waiting Area │
    │   Display    │
    │  (43-55")    │
    └──────────────┘
```

## 🛠️ Technology Stack

### Backend
- **.NET 6+** with ASP.NET Core
- **SQL Server Express** for database
- **Entity Framework Core** for ORM
- **SignalR** for real-time WebSocket communication
- **JWT** for authentication

### Frontend
- **React.js** with Hooks
- **Redux** for state management
- **Axios** for API calls
- **Socket.IO** for real-time updates
- **Tailwind CSS** for styling
- **Chart.js** for reporting

### Mobile
- **React-based responsive web** for Android tablets
- **Android WebView** for native app

### IoT
- **ESP32 DevKit**
- **Arduino IDE / PlatformIO**
- **SSD1306 OLED display**
- **4x4 Matrix Keypad**

## 📦 Project Structure

```
Queue-Management-System/
├── backend/
│   ├── QMS.API/                 # Main API
│   ├── QMS.Core/                # Domain models
│   ├── QMS.Data/                # Database & repositories
│   ├── QMS.Services/            # Business logic
│   └── QMS.Tests/               # Unit tests
├── frontend/
│   ├── src/
│   │   ├── components/          # React components
│   │   ├── pages/               # Dashboard pages
│   │   ├── services/            # API & WebSocket
│   │   ├── store/               # Redux state
│   │   └── App.js
│   └── package.json
├── mobile/
│   ├── counter-display/         # Counter display app
│   └── waiting-area/            # Waiting area display
├── iot/
│   ├── esp32-terminal/          # ESP32 firmware
│   └── libraries/               # Custom libraries
├── docs/
│   ├── DATABASE_SCHEMA.md
│   ├── API_DOCUMENTATION.md
│   ├── INSTALLATION_GUIDE.md
│   └── USER_MANUAL.md
└── README.md
```

## 🚀 Quick Start

### Prerequisites
- Windows Server 2019+ or Windows 10 Pro
- SQL Server Express (free)
- .NET 6+ SDK
- Node.js 16+
- Arduino IDE or PlatformIO

### Backend Setup

```bash
cd backend/QMS.API
dotnet restore
dotnet ef database update
dotnet run
```

API will be available at `http://localhost:5000`

### Frontend Setup

```bash
cd frontend
npm install
npm start
```

Dashboard will be available at `http://localhost:3000`

### ESP32 Terminal Setup

1. Open `iot/esp32-terminal/qms_terminal.ino` in Arduino IDE
2. Select Board: ESP32 Dev Module
3. Configure WiFi credentials
4. Upload to device

## 📚 Documentation

- [Database Schema](./docs/DATABASE_SCHEMA.md)
- [API Documentation](./docs/API_DOCUMENTATION.md)
- [Installation Guide](./docs/INSTALLATION_GUIDE.md)
- [User Manual](./docs/USER_MANUAL.md)
- [Development Guide](./docs/DEVELOPMENT_GUIDE.md)

## 📋 Project Phases

### Phase 1: MVP (2-3 months)
- ✅ Core API development
- ✅ Admin dashboard
- ✅ ESP32 terminal integration
- ✅ Android tablet display
- ✅ Ticket dispenser integration
- ✅ Basic reporting

### Phase 2: Enhanced (2-3 months)
- ⏳ Waiting area display with marketing carousel
- ⏳ Advanced reporting (PDF, Excel, CSV)
- ⏳ Voice announcements
- ⏳ Performance optimization

### Phase 3: Polish (1-2 months)
- ⏳ Load testing
- ⏳ Security hardening
- ⏳ Production deployment

## 🔑 Key Performance Indicators

| Metric | Target |
|--------|--------|
| API Response Time | < 200ms |
| System Uptime | > 99% |
| Real-time Sync Delay | < 1s |
| Concurrent Users | 100+ |
| Error Rate | < 0.1% |

## 🔒 Security

- JWT token-based authentication
- Role-based access control (RBAC)
- Session timeout for inactive users
- HTTPS/TLS encryption
- Password hashing (bcrypt)
- Audit logging of all actions
- Windows Firewall configuration
- VPN support for remote access

## 📝 License

MIT License - See LICENSE file for details

## 🤝 Support

For issues, questions, or contributions, please open an issue on GitHub.

## 👥 Team

Developed with ❤️ for efficient queue management
