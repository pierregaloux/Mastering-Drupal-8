# Quick Reference Card

## 🚀 Common Commands

### Build and Run
```bash
# Build the project
dotnet build

# Run the application
dotnet run

# Run on specific port
dotnet run --urls "http://localhost:5000"

# Run with hot reload (watches for changes)
dotnet watch run
```

### In Visual Studio 2022
- **Build**: `Ctrl+Shift+B`
- **Run**: `F5`
- **Run without debugging**: `Ctrl+F5`
- **Stop**: `Shift+F5`

## 🌐 Access URLs

### Local Computer
- **Dashboard**: http://localhost:5000
- **HTTPS**: https://localhost:5001
- **Swagger API Docs**: http://localhost:5000/swagger

### From Tablet
- **Dashboard**: http://YOUR_COMPUTER_IP:5000
- Example: http://192.168.1.100:5000

### Find Your IP Address
```bash
# Windows Command Prompt
ipconfig

# Look for "IPv4 Address" under your WiFi adapter
# Example: 192.168.1.100
```

## 📡 API Endpoints

| Method | Endpoint | Purpose |
|--------|----------|---------|
| GET | `/api/connection/status` | Check connection status |
| POST | `/api/connection/connect` | Connect to SimConnect |
| POST | `/api/connection/disconnect` | Disconnect |
| GET | `/api/flight/data` | Get current flight data |
| POST | `/api/flight/command` | Send command |

## 🎮 Common SimConnect Commands

### Autopilot
```json
// Turn ON Autopilot
{
  "variableName": "AUTOPILOT_MASTER",
  "value": 1,
  "unit": "Bool"
}

// Turn OFF Autopilot
{
  "variableName": "AUTOPILOT_MASTER",
  "value": 0,
  "unit": "Bool"
}

// Set Heading to 180° (South)
{
  "variableName": "HEADING_BUG_SET",
  "value": 180,
  "unit": "Degrees"
}

// Set Altitude to 10,000 feet
{
  "variableName": "AUTOPILOT_ALTITUDE_LOCK_VAR",
  "value": 10000,
  "unit": "Feet"
}
```

## 🔧 Troubleshooting

### Application Won't Start
```bash
# Check .NET version
dotnet --version

# Should show 8.0 or higher

# Restore packages
dotnet restore

# Clean and rebuild
dotnet clean
dotnet build
```

### Can't Access from Tablet
1. **Check firewall**:
   - Windows Firewall → Allow app
   - Port: 5000
   
2. **Verify same network**:
   - Both devices on same WiFi

3. **Use HTTP not HTTPS**:
   - ✅ `http://192.168.1.100:5000`
   - ❌ `https://192.168.1.100:5000`

### Port Already in Use
```bash
# Use different port
dotnet run --urls "http://localhost:5555"

# Or kill existing process
# Windows: Task Manager → End dotnet.exe
```

## 📁 Important Files

| File | Purpose |
|------|---------|
| `Program.cs` | Application entry point, configuration |
| `Services/SimConnectService.cs` | SimConnect logic |
| `Hubs/FlightDataHub.cs` | SignalR real-time hub |
| `wwwroot/index.html` | Web dashboard UI |
| `appsettings.json` | Configuration settings |

## 🎯 Quick Edits

### Change Update Interval
**File**: `appsettings.json`
```json
{
  "SimConnect": {
    "UpdateIntervalMs": 1000  // Change to 500 for faster updates
  }
}
```

**File**: `Services/SimConnectService.cs` (line ~155)
```csharp
_dataUpdateTimer = new System.Timers.Timer(1000); // Change interval here
```

### Change Dashboard Colors
**File**: `wwwroot/index.html`
```css
background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%);
/* Change to your preferred colors */
```

### Add New Flight Data Field
1. **Add to Model** (`Models/FlightData.cs`):
```csharp
public double FuelLevel { get; set; }
```

2. **Update Service** (`Services/SimConnectService.cs`):
```csharp
FuelLevel = _random.NextDouble() * 100,
```

3. **Update Dashboard** (`wwwroot/index.html`):
```html
<div class="card">
    <h3>Fuel Level</h3>
    <div class="value" id="fuelLevel">0</div>
    <div class="unit">percent</div>
</div>
```

```javascript
document.getElementById('fuelLevel').textContent = 
    Math.round(data.fuelLevel);
```

## 🔍 Debugging Tips

### View Application Logs
**In Visual Studio**: Output window (View → Output)

**Console**:
```bash
dotnet run
# Logs appear in console
```

### Browser Developer Tools
- Press `F12` in browser
- **Console**: See JavaScript errors
- **Network**: Monitor SignalR connections
- **Application**: View WebSocket frames

### Common Log Messages
```
✅ "Successfully connected to SimConnect (DEMO MODE)"
✅ "Now listening on: http://localhost:5000"
✅ "Client connected: [connection-id]"
❌ "Failed to bind to address" → Port in use
❌ "Connection closed" → SignalR disconnected
```

## 🎓 Learning Resources

### C# Basics
- Variables and types
- Classes and objects
- Async/await pattern
- Dependency injection

### ASP.NET Core
- Minimal APIs
- Middleware pipeline
- Dependency injection
- Configuration

### SignalR
- Hubs
- Client/server communication
- Real-time updates

### Files to Study (in order)
1. `Models/FlightData.cs` - Simple data model
2. `Services/ISimConnectService.cs` - Interface pattern
3. `Services/SimConnectService.cs` - Service implementation
4. `Hubs/FlightDataHub.cs` - SignalR hub
5. `Program.cs` - Application setup
6. `wwwroot/index.html` - Frontend

## ⌨️ Keyboard Shortcuts

### Visual Studio 2022
| Shortcut | Action |
|----------|--------|
| `Ctrl+K, Ctrl+D` | Format document |
| `Ctrl+.` | Quick actions (suggestions) |
| `F12` | Go to definition |
| `Ctrl+F` | Find |
| `Ctrl+H` | Replace |
| `Ctrl+/` | Comment/uncomment |
| `F9` | Set breakpoint |
| `F10` | Step over (debugging) |
| `F11` | Step into (debugging) |

### Browser
| Shortcut | Action |
|----------|--------|
| `F12` | Developer tools |
| `Ctrl+Shift+I` | Inspect element |
| `Ctrl+R` | Refresh page |
| `Ctrl+Shift+R` | Hard refresh |

## 📝 Configuration Checklist

Before first run:
- [ ] Visual Studio 2022 installed
- [ ] .NET 8.0 SDK installed (`dotnet --version`)
- [ ] Project opens without errors
- [ ] NuGet packages restored
- [ ] Build successful (0 errors)

Before tablet access:
- [ ] Application running on PC
- [ ] Computer IP address noted
- [ ] Firewall allows port 5000
- [ ] Tablet on same WiFi network
- [ ] Using `http://` not `https://`

For real MSFS connection:
- [ ] MSFS SDK installed
- [ ] SimConnect.dll referenced
- [ ] SimConnectService.cs updated
- [ ] MSFS running
- [ ] SimConnect enabled in MSFS settings

## 🎯 File Sizes Reference

| File | Lines | Size | Purpose |
|------|-------|------|---------|
| README.md | 387 | 9KB | Main documentation |
| GETTING_STARTED.md | 147 | 4KB | Beginner guide |
| SIMCONNECT_VARIABLES.md | 230 | 7KB | Variable reference |
| PROJECT_SUMMARY.md | 254 | 7KB | Overview |
| ARCHITECTURE.md | 571 | 16KB | System design |
| Program.cs | 101 | 3KB | App entry |
| SimConnectService.cs | 200 | 8KB | Core logic |
| index.html | 389 | 14KB | Dashboard |

## 🚨 Emergency Commands

### Stop All .NET Processes
```bash
# Windows
taskkill /F /IM dotnet.exe

# Alternative
# Task Manager → Details → End dotnet.exe
```

### Reset to Clean State
```bash
# Clean build artifacts
dotnet clean

# Delete obj and bin folders
rm -rf obj bin

# Restore packages
dotnet restore

# Rebuild
dotnet build
```

### Reset Git Changes (if needed)
```bash
# See what changed
git status

# Discard all changes
git reset --hard HEAD

# Or discard specific file
git checkout -- filename.cs
```

## 💡 Pro Tips

1. **Use snippets**: Type `prop` + Tab + Tab for property
2. **Multi-cursor**: Alt+Click for multiple cursors
3. **Save often**: Ctrl+S (auto-save enabled in VS)
4. **Test in browser**: Keep Dev Tools open (F12)
5. **Watch logs**: Keep Output window visible
6. **Use Swagger**: Easy API testing at `/swagger`
7. **Hot reload**: `dotnet watch run` for auto-refresh
8. **Breakpoints**: F9 to debug step-by-step

---

**Keep this file handy for quick reference!**

For detailed information, see:
- README.md - Full documentation
- GETTING_STARTED.md - First-time setup
- SIMCONNECT_VARIABLES.md - Variable details
