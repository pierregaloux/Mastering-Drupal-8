# MSFS SimConnect Web API - Beginner's Guide

## ⚠️ Opening in Visual Studio 2022

**Important:** This project is in a subdirectory. To open in Visual Studio 2022:

1. File → Open → Project/Solution
2. Navigate to the `MSFSSimConnectApp` folder
3. Open `MSFSSimConnectWebAPI.sln`

**See detailed instructions:** [HOW_TO_OPEN_IN_VS2022.md](HOW_TO_OPEN_IN_VS2022.md)

---

## 📋 Overview

This is a C# .NET 8.0 web application that connects to Microsoft Flight Simulator (MSFS) using SimConnect to read and write flight data. The data is displayed on a web page that you can access from your tablet or any device on your local network.

## 🎯 Features

- **Real-time Flight Data**: View live aircraft position, altitude, speed, heading, and more
- **Web Dashboard**: Responsive web interface optimized for tablets
- **SimConnect Integration**: Read and write data from/to MSFS
- **REST API**: RESTful endpoints for programmatic access
- **SignalR Real-time Updates**: Automatic data streaming to connected clients
- **Demo Mode**: Works without MSFS for testing and learning

## 📦 Prerequisites

### Required Software
1. **Visual Studio 2022** (Community Edition or higher)
   - Download from: https://visualstudio.microsoft.com/downloads/
   - During installation, select: ".NET desktop development" workload

2. **.NET 8.0 SDK** (included with Visual Studio 2022)
   - Verify installation by opening Command Prompt and typing: `dotnet --version`

3. **Microsoft Flight Simulator** (for real flight data)
   - Optional: App works in demo mode without MSFS

4. **MSFS SDK** (for production use with real MSFS)
   - Download from: https://docs.flightsimulator.com/
   - Note: Demo mode works without the SDK

## 🚀 Quick Start Guide (For Beginners)

### Step 1: Open the Project in Visual Studio 2022

1. Launch Visual Studio 2022
2. Click "Open a project or solution"
3. Navigate to `MSFSSimConnectApp` folder
4. Open `MSFSSimConnectWebAPI.csproj`

### Step 2: Restore NuGet Packages

Visual Studio will automatically restore packages. If not:
1. Right-click on the project in Solution Explorer
2. Select "Restore NuGet Packages"
3. Wait for packages to download

### Step 3: Build the Project

1. Click **Build** menu → **Build Solution** (or press `Ctrl+Shift+B`)
2. Wait for build to complete (check Output window)
3. Ensure there are no errors

### Step 4: Run the Application

1. Press **F5** or click the green "Start" button
2. Your default browser will open automatically
3. The application runs on `https://localhost:5001` by default

### Step 5: Access the Dashboard

#### On the Same Computer:
- Open browser and navigate to: `https://localhost:5001`

#### From Your Tablet (on the same WiFi network):
1. Find your computer's IP address:
   - Open Command Prompt
   - Type: `ipconfig`
   - Look for "IPv4 Address" (e.g., 192.168.1.100)

2. On your tablet's browser, navigate to:
   - `http://YOUR_COMPUTER_IP:5000` (replace YOUR_COMPUTER_IP with actual IP)
   - Example: `http://192.168.1.100:5000`

**Important**: Use `http` (not `https`) and port `5000` when accessing from other devices.

## 🎮 Using the Application

### Dashboard Features

The web dashboard displays:
- **Aircraft Name**: Current aircraft model
- **Altitude**: Current altitude in feet
- **Airspeed (IAS)**: Indicated airspeed in knots
- **Ground Speed**: Speed over ground in knots
- **Heading**: Current heading in degrees
- **Vertical Speed**: Climb/descent rate in feet per minute
- **Pitch & Bank**: Aircraft attitude
- **Position**: GPS coordinates (latitude/longitude)
- **Status**: On ground or in flight

### Sending Commands

You can control flight simulator parameters:
1. Enter the **Variable Name** (e.g., `HEADING_BUG_SET`, `AUTOPILOT_MASTER`)
2. Enter the **Value** (e.g., 180 for heading, 1 for ON, 0 for OFF)
3. Select the **Unit** (Number, Bool, Degrees, etc.)
4. Click **Send Command**

Quick buttons are provided for common commands like Autopilot ON/OFF.

## 🔧 Project Structure

```
MSFSSimConnectApp/
├── Models/
│   ├── FlightData.cs          # Flight data model
│   └── SimConnectCommand.cs   # Command model
├── Services/
│   ├── ISimConnectService.cs  # SimConnect service interface
│   ├── SimConnectService.cs   # SimConnect implementation (DEMO)
│   └── FlightDataBroadcastService.cs  # Background service
├── Hubs/
│   └── FlightDataHub.cs       # SignalR hub for real-time updates
├── wwwroot/
│   └── index.html             # Web dashboard UI
├── Program.cs                 # Application entry point
└── MSFSSimConnectWebAPI.csproj  # Project file
```

## 🔌 Connecting to Real MSFS

The application currently runs in **DEMO MODE** with simulated data. To connect to real Microsoft Flight Simulator:

### Step 1: Install MSFS SDK

1. Download MSFS SDK from: https://docs.flightsimulator.com/
2. Install the SimConnect SDK component
3. Locate `SimConnect.dll` (typically in `C:\MSFS SDK\SimConnect SDK\lib`)

### Step 2: Add SimConnect Reference

1. In Visual Studio, right-click **Dependencies** in Solution Explorer
2. Click **Add COM Reference** or **Add Reference**
3. Browse to `SimConnect.dll` and add it

### Step 3: Update SimConnectService.cs

Replace the demo implementation in `Services/SimConnectService.cs` with actual SimConnect API calls:

```csharp
using Microsoft.FlightSimulator.SimConnect;

// In ConnectAsync():
simConnect = new SimConnect("MSFSWebAPI", IntPtr.Zero, 0, null, 0);

// Set up data definitions and requests
// Register for data updates
// Handle SimConnect messages
```

Refer to SimConnect SDK documentation for detailed API usage.

### Step 4: Run MSFS

1. Start Microsoft Flight Simulator
2. Start your web application
3. The application will automatically connect

## 📡 API Endpoints

The application provides REST API endpoints:

- **GET** `/api/connection/status` - Check connection status
- **POST** `/api/connection/connect` - Connect to SimConnect
- **POST** `/api/connection/disconnect` - Disconnect from SimConnect
- **GET** `/api/flight/data` - Get current flight data
- **POST** `/api/flight/command` - Send a command

You can test these in your browser or use tools like Postman.

### Swagger UI

Access interactive API documentation at: `https://localhost:5001/swagger`

## 🐛 Troubleshooting

### Issue: Application won't start
**Solution**: Ensure .NET 8.0 SDK is installed. Run `dotnet --version` in Command Prompt.

### Issue: Can't access from tablet
**Solutions**:
- Ensure both devices are on the same WiFi network
- Check Windows Firewall - allow port 5000
- Use `http://` (not `https://`) with port 5000
- Verify computer's IP address is correct

### Issue: Build errors
**Solutions**:
- Restore NuGet packages: Right-click project → Restore NuGet Packages
- Clean and rebuild: Build → Clean Solution, then Build → Rebuild Solution

### Issue: "Connection closed" in dashboard
**Solutions**:
- Ensure the web application is running
- Check browser console (F12) for errors
- Refresh the page

## 🔒 Firewall Configuration

To access from your tablet, you may need to allow the application through Windows Firewall:

1. Open **Windows Defender Firewall**
2. Click **Advanced settings**
3. Click **Inbound Rules** → **New Rule**
4. Select **Port** → Next
5. Select **TCP** and enter port **5000**
6. Allow the connection
7. Name the rule "MSFS Web API"

## 📚 Learning Resources

### C# and .NET
- Microsoft C# Documentation: https://docs.microsoft.com/dotnet/csharp/
- ASP.NET Core Tutorial: https://docs.microsoft.com/aspnet/core/

### SimConnect
- SimConnect SDK Documentation: https://docs.flightsimulator.com/
- SimConnect Variables Reference: https://docs.flightsimulator.com/

### SignalR (Real-time Communication)
- SignalR Tutorial: https://docs.microsoft.com/aspnet/core/signalr/

## 🎓 Next Steps for Learning

1. **Explore the Code**: Read through each file with comments
2. **Modify the Dashboard**: Change colors, add new data fields in `index.html`
3. **Add New API Endpoints**: Practice creating new endpoints in `Program.cs`
4. **Study SimConnect**: Review official SimConnect documentation
5. **Experiment with Commands**: Try sending different commands to MSFS

## 📝 Common SimConnect Variables

Here are some useful SimConnect variables you can read/write:

**Autopilot**:
- `AUTOPILOT_MASTER` (Bool: 0=OFF, 1=ON)
- `AUTOPILOT_HEADING_LOCK` (Bool)
- `AUTOPILOT_ALTITUDE_LOCK` (Bool)
- `HEADING_BUG_SET` (Degrees)
- `AUTOPILOT_ALTITUDE_LOCK_VAR` (Feet)

**Flight Controls**:
- `ELEVATOR_POSITION` (Position -16384 to 16384)
- `AILERON_POSITION` (Position -16384 to 16384)
- `RUDDER_POSITION` (Position -16384 to 16384)

**Engine**:
- `GENERAL_ENG_THROTTLE_LEVER_POSITION:1` (Percent 0-100)
- `GENERAL_ENG_MIXTURE_LEVER_POSITION:1` (Percent 0-100)

## 💡 Tips for Beginners

1. **Start with Demo Mode**: Get familiar with the interface before connecting to MSFS
2. **Use Swagger UI**: Great for testing API endpoints
3. **Check the Console**: Press F12 in browser to see real-time logs
4. **Read Comments**: All code files have detailed comments explaining what they do
5. **Take Small Steps**: Make one change at a time and test

## 📄 License

This project is for educational purposes. Microsoft Flight Simulator and SimConnect are trademarks of Microsoft Corporation.

## 🤝 Support

For issues or questions:
1. Check the Troubleshooting section above
2. Review code comments for clarification
3. Consult official documentation links provided

---

**Happy Flying! ✈️**
