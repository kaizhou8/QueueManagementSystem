# Queue Management System

A modern queue management system built with Blazor, designed for hospitals, banks, and government service halls. The system includes a large display screen, staff console, and mobile client interface.

## Features

- **Multi-platform Support**
  - Large display screen for waiting areas
  - Staff operation console
  - Mobile web interface for customers
  - iPad/tablet self-service kiosk support

- **Real-time Updates**
  - Instant ticket status updates
  - Live counter status display
  - Real-time waiting time estimation
  - Voice announcements for ticket calls

- **Queue Management**
  - Multiple service types support
  - Priority queue handling
  - VIP customer support
  - Dynamic counter allocation

- **Mobile Integration**
  - QR code ticket generation
  - Mobile ticket status tracking
  - Push notifications
  - Remote queue joining

## Technical Architecture

### Server Components
- **Blazor Server**: Powers the display screen and staff console
- **SignalR**: Enables real-time communication
- **ASP.NET Core**: Provides REST API endpoints
- **Entity Framework Core**: Handles data persistence

### Client Components
- **Blazor WebAssembly**: Powers the mobile client interface
- **Progressive Web App (PWA)**: Enables mobile-first experience
- **Responsive Design**: Supports various screen sizes

### Communication
- **SignalR Hub**: Manages real-time updates
- **REST API**: Handles CRUD operations
- **WebSocket**: Ensures low-latency communication

## Project Structure

```
QueueSystem/
├── QueueSystem.Server/        # Blazor Server Application
│   ├── Pages/                 # Razor Pages
│   ├── Services/              # Business Logic
│   ├── Hubs/                 # SignalR Hubs
│   └── wwwroot/              # Static Files
│
├── QueueSystem.Mobile/        # Blazor WebAssembly Application
│   ├── Pages/                # Mobile Interface
│   ├── Services/             # Client Services
│   └── wwwroot/              # Static Files
│
└── QueueSystem.Shared/        # Shared Library
    ├── Models/               # Data Models
    └── Interfaces/           # Shared Interfaces
```

## Getting Started

1. **Prerequisites**
   ```bash
   - .NET 7.0 SDK
   - Visual Studio 2022 or VS Code
   - SQL Server (Optional)
   ```

2. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/QueueSystem.git
   cd QueueSystem
   ```

3. **Build and Run**
   ```bash
   dotnet restore
   dotnet build
   cd QueueSystem.Server
   dotnet run
   ```

4. **Access the Applications**
   - Display Screen: `https://localhost:5001/display`
   - Staff Console: `https://localhost:5001/console`
   - Mobile Interface: `https://localhost:5001`

## Configuration

### Display Screen Settings
```json
{
  "Display": {
    "RefreshInterval": 1000,
    "MaxDisplayItems": 10,
    "EnableVoice": true
  }
}
```

### Service Types
```json
{
  "ServiceTypes": [
    {
      "Id": "general",
      "Name": "General Service",
      "DefaultPriority": 1
    },
    {
      "Id": "express",
      "Name": "Express Service",
      "DefaultPriority": 2
    },
    {
      "Id": "vip",
      "Name": "VIP Service",
      "DefaultPriority": 3
    }
  ]
}
```

## Deployment

### Server Deployment
1. Publish the server application:
   ```bash
   dotnet publish -c Release
   ```

2. Deploy to IIS or Azure App Service

### Mobile Client Deployment
1. Build the WebAssembly application:
   ```bash
   dotnet publish -c Release
   ```

2. Deploy to a static web host or CDN

## Security Considerations

- Use HTTPS for all communications
- Implement authentication for staff console
- Secure SignalR connections
- Rate limit API endpoints
- Validate all user inputs
- Implement CORS policies

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Support

For support and questions, please create an issue in the GitHub repository or contact the development team.
