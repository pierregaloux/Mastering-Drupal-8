# Architecture Diagram

## System Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                         CLIENT DEVICES                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌───────────────┐         ┌───────────────┐                   │
│  │   Desktop PC  │         │    Tablet     │                   │
│  │   Browser     │         │   Browser     │                   │
│  │ (localhost)   │         │ (192.168.x.x) │                   │
│  └───────┬───────┘         └───────┬───────┘                   │
│          │                         │                           │
│          │  HTTP/HTTPS            │  HTTP                     │
│          │  SignalR (WebSocket)   │  SignalR (WebSocket)      │
└──────────┼─────────────────────────┼───────────────────────────┘
           │                         │
           ▼                         ▼
┌─────────────────────────────────────────────────────────────────┐
│                      WEB API SERVER                             │
│              (ASP.NET Core - .NET 8.0)                          │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │                    Program.cs                           │   │
│  │            Application Configuration Layer              │   │
│  └─────────────┬──────────────┬──────────────┬─────────────┘   │
│                │              │              │                 │
│    ┌───────────▼──────────┐   │   ┌──────────▼───────────┐    │
│    │  Static Files        │   │   │   CORS Policy        │    │
│    │  (wwwroot/)          │   │   │   (AllowAll)         │    │
│    │  • index.html        │   │   │   • AllowAnyOrigin   │    │
│    │  • CSS/JS            │   │   │   • AllowAnyMethod   │    │
│    └──────────────────────┘   │   └──────────────────────┘    │
│                                │                               │
│                    ┌───────────▼──────────┐                    │
│                    │   Swagger/OpenAPI    │                    │
│                    │   API Documentation  │                    │
│                    └──────────────────────┘                    │
│                                                                 │
├─────────────────────────────────────────────────────────────────┤
│                      API ENDPOINTS                              │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  • GET  /api/connection/status                                  │
│  • POST /api/connection/connect                                 │
│  • POST /api/connection/disconnect                              │
│  • GET  /api/flight/data                                        │
│  • POST /api/flight/command                                     │
│                                                                 │
├─────────────────────────────────────────────────────────────────┤
│                      SIGNALR HUB                                │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │               FlightDataHub.cs                           │  │
│  │         Real-time Communication Hub                      │  │
│  ├──────────────────────────────────────────────────────────┤  │
│  │  • OnConnectedAsync()    - Client connects              │  │
│  │  • OnDisconnectedAsync() - Client disconnects           │  │
│  │  • RequestFlightData()   - Client requests data         │  │
│  │  • SendCommand()         - Client sends command         │  │
│  │  • ReceiveFlightData     - Broadcast to clients         │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                 │
├─────────────────────────────────────────────────────────────────┤
│                      SERVICE LAYER                              │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │       FlightDataBroadcastService.cs                      │  │
│  │          Background Service (Singleton)                  │  │
│  ├──────────────────────────────────────────────────────────┤  │
│  │  • Starts on app startup                                │  │
│  │  • Subscribes to FlightDataUpdated events               │  │
│  │  • Broadcasts data to all SignalR clients               │  │
│  │  • Runs continuously in background                      │  │
│  └──────────────┬───────────────────────────────────────────┘  │
│                 │                                               │
│                 │ subscribes to                                 │
│                 ▼                                               │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │           SimConnectService.cs                           │  │
│  │    (Implements ISimConnectService - Singleton)           │  │
│  ├──────────────────────────────────────────────────────────┤  │
│  │  Public Methods:                                         │  │
│  │  • ConnectAsync()      - Connect to SimConnect          │  │
│  │  • DisconnectAsync()   - Disconnect                     │  │
│  │  • GetFlightDataAsync() - Get current data              │  │
│  │  • SendCommandAsync()  - Send command to sim            │  │
│  │  • IsConnected         - Connection status              │  │
│  │                                                          │  │
│  │  Events:                                                 │  │
│  │  • FlightDataUpdated   - Fires every 1 second           │  │
│  │                                                          │  │
│  │  Private:                                                │  │
│  │  • Timer (1000ms)      - Updates simulated data         │  │
│  │  • OnDataUpdateTimerElapsed() - Generates demo data     │  │
│  └──────────────┬───────────────────────────────────────────┘  │
│                 │                                               │
│                 │ (DEMO MODE - Simulated Data)                  │
│                 │                                               │
│                 │ [For Production: Replace with]                │
│                 ▼                                               │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │          Real SimConnect Integration                     │  │
│  │   Microsoft.FlightSimulator.SimConnect                   │  │
│  ├──────────────────────────────────────────────────────────┤  │
│  │  • SimConnect.dll from MSFS SDK                         │  │
│  │  • Connect to running MSFS instance                     │  │
│  │  • Request data definitions                             │  │
│  │  • Send/receive SimConnect messages                     │  │
│  │  • Handle events and data updates                       │  │
│  └──────────────┬───────────────────────────────────────────┘  │
│                 │                                               │
│                 ▼                                               │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │          Microsoft Flight Simulator                      │  │
│  │         (Running on same computer)                       │  │
│  └──────────────────────────────────────────────────────────┘  │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                        DATA MODELS                              │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌──────────────────────────┐  ┌──────────────────────────┐    │
│  │    FlightData.cs         │  │  SimConnectCommand.cs    │    │
│  ├──────────────────────────┤  ├──────────────────────────┤    │
│  │ • AircraftTitle          │  │ • VariableName           │    │
│  │ • Latitude/Longitude     │  │ • Value                  │    │
│  │ • Altitude               │  │ • Unit                   │    │
│  │ • IndicatedAirspeed      │  └──────────────────────────┘    │
│  │ • GroundSpeed            │                                  │
│  │ • Heading                │                                  │
│  │ • VerticalSpeed          │                                  │
│  │ • Pitch/Bank             │                                  │
│  │ • OnGround               │                                  │
│  │ • Timestamp              │                                  │
│  └──────────────────────────┘                                  │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Data Flow

### 1. Application Startup
```
Program.cs
  ↓
Configure Services (DI Container)
  • Add SimConnectService (Singleton)
  • Add FlightDataBroadcastService (Hosted)
  • Add SignalR
  • Add CORS
  ↓
App Starts
  ↓
FlightDataBroadcastService.ExecuteAsync()
  ↓
SimConnectService.ConnectAsync()
  ↓
Start Timer (1 second interval)
  ↓
Begin generating simulated data
```

### 2. Client Connection Flow
```
Browser → http://localhost:5000
  ↓
Load index.html (from wwwroot)
  ↓
Load SignalR JavaScript client
  ↓
Connect to /flightDataHub
  ↓
FlightDataHub.OnConnectedAsync()
  ↓
Send current FlightData to new client
  ↓
Client receives and displays data
```

### 3. Real-time Data Update Flow
```
Timer (every 1 second)
  ↓
SimConnectService.OnDataUpdateTimerElapsed()
  ↓
Generate new FlightData
  ↓
Fire FlightDataUpdated event
  ↓
FlightDataBroadcastService receives event
  ↓
Broadcast to all SignalR clients
  ↓
Clients receive via "ReceiveFlightData"
  ↓
Update dashboard UI
```

### 4. Command Flow (Client → Simulator)
```
User enters command on webpage
  ↓
Click "Send Command" button
  ↓
JavaScript: connection.invoke("SendCommand", command)
  ↓
SignalR → FlightDataHub.SendCommand()
  ↓
SimConnectService.SendCommandAsync()
  ↓
[DEMO: Update simulated values]
[PROD: Send to SimConnect → MSFS]
  ↓
Return success/failure
  ↓
Client receives "CommandResult"
  ↓
Display confirmation
```

## Technology Stack

```
┌─────────────────────────────────────────┐
│         PRESENTATION LAYER              │
├─────────────────────────────────────────┤
│ • HTML5                                 │
│ • CSS3 (Grid, Flexbox, Gradients)      │
│ • JavaScript (ES6+)                     │
│ • SignalR JavaScript Client Library     │
└─────────────────────────────────────────┘
              ▲ ▼
┌─────────────────────────────────────────┐
│         APPLICATION LAYER               │
├─────────────────────────────────────────┤
│ • ASP.NET Core 8.0                      │
│ • SignalR Hub                           │
│ • Minimal API (Endpoints)               │
│ • Swagger/OpenAPI                       │
│ • CORS Middleware                       │
└─────────────────────────────────────────┘
              ▲ ▼
┌─────────────────────────────────────────┐
│         BUSINESS LAYER                  │
├─────────────────────────────────────────┤
│ • ISimConnectService (Interface)        │
│ • SimConnectService (Implementation)    │
│ • FlightDataBroadcastService            │
│ • Dependency Injection                  │
└─────────────────────────────────────────┘
              ▲ ▼
┌─────────────────────────────────────────┐
│         DATA LAYER                      │
├─────────────────────────────────────────┤
│ • FlightData Model                      │
│ • SimConnectCommand Model               │
│ • (Future: SimConnect SDK)              │
└─────────────────────────────────────────┘
```

## Network Topology

```
                Internet
                   │
                   │ (Not Used)
                   │
          ┌────────┴────────┐
          │   Home Router   │
          │  192.168.1.1    │
          └────────┬────────┘
                   │
         WiFi Network (192.168.1.0/24)
                   │
        ┌──────────┴──────────┐
        │                     │
   ┌────▼────┐          ┌────▼────┐
   │ Desktop │          │ Tablet  │
   │  PC     │          │         │
   │         │          │         │
   │ Running │◄────────►│ Browser │
   │  Web    │          │ http:// │
   │  API    │  HTTP    │ 192.168 │
   │ :5000   │  SignalR │  .1.100 │
   │         │          │  :5000  │
   │ Running │          │         │
   │  MSFS   │          │         │
   │ (future)│          │         │
   └─────────┘          └─────────┘

Legend:
  ◄────────► Two-way communication
  ───────►   One-way communication
```

## Deployment Architecture

### Development (Current)
```
┌──────────────────────────────┐
│    Visual Studio 2022        │
│                              │
│  ┌────────────────────────┐  │
│  │   MSFSSimConnectApp    │  │
│  │                        │  │
│  │   Press F5 to Run      │  │
│  │         ↓              │  │
│  │   dotnet run           │  │
│  │         ↓              │  │
│  │   Kestrel Server       │  │
│  │   Port: 5000, 5001     │  │
│  └────────────────────────┘  │
│                              │
└──────────────────────────────┘
```

### Production (Future Options)
```
Option 1: Windows Service
┌──────────────────────────┐
│  Windows Service         │
│  • Auto-start on boot    │
│  • Runs in background    │
│  • SC.exe config         │
└──────────────────────────┘

Option 2: IIS
┌──────────────────────────┐
│  IIS (Internet Info Svr) │
│  • Full-featured server  │
│  • Windows Authentication│
│  • Advanced hosting      │
└──────────────────────────┘

Option 3: Docker (Advanced)
┌──────────────────────────┐
│  Docker Container        │
│  • Isolated environment  │
│  • Easy deployment       │
│  • Cross-platform        │
└──────────────────────────┘
```

## Security Architecture

```
┌─────────────────────────────────────────┐
│         SECURITY LAYERS                 │
├─────────────────────────────────────────┤
│                                         │
│  ┌───────────────────────────────────┐  │
│  │  Network Security                 │  │
│  │  • Local network only             │  │
│  │  • Windows Firewall rules         │  │
│  │  • Port 5000 allowed              │  │
│  └───────────────────────────────────┘  │
│                                         │
│  ┌───────────────────────────────────┐  │
│  │  Application Security             │  │
│  │  • CORS policy configured         │  │
│  │  • HTTPS available (port 5001)    │  │
│  │  • No authentication (local use)  │  │
│  └───────────────────────────────────┘  │
│                                         │
│  ┌───────────────────────────────────┐  │
│  │  Code Security                    │  │
│  │  • CodeQL scanned (0 issues)      │  │
│  │  • No hardcoded secrets           │  │
│  │  • Input validation on commands   │  │
│  │  • Error handling throughout      │  │
│  └───────────────────────────────────┘  │
│                                         │
└─────────────────────────────────────────┘
```

## Scalability Considerations

### Current Architecture (Single Instance)
- Supports: ~100 concurrent tablet connections
- Data updates: 1 per second to all clients
- Resource usage: Low (demo mode)

### Future Improvements
- Redis for SignalR backplane (multiple servers)
- Database for flight data history
- API rate limiting
- Load balancing for multiple instances

---

**Architecture Type**: N-Tier (3-tier) with Real-time Communication
**Pattern**: Service-Oriented Architecture (SOA)
**Communication**: RESTful API + SignalR WebSockets
