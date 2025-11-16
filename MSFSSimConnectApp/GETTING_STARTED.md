# Getting Started - Quick Setup Guide

## For Complete Beginners to C# and Visual Studio

## ⚠️ Important: Project Location

**Note:** This C# project is located in the `MSFSSimConnectApp` folder within the Mastering-Drupal-8 repository. Don't worry - the project is completely self-contained and independent. Visual Studio will only load the C# project files when you open the solution.

**Having trouble opening in Visual Studio?** See the detailed guide: **[HOW_TO_OPEN_IN_VS2022.md](HOW_TO_OPEN_IN_VS2022.md)**

---

### Step 1: Install Visual Studio 2022

1. Go to https://visualstudio.microsoft.com/downloads/
2. Download **Visual Studio 2022 Community** (free)
3. Run the installer
4. When asked what to install, select:
   - ✅ **ASP.NET and web development**
   - ✅ **.NET desktop development**
5. Click Install and wait (can take 30-60 minutes)

### Step 2: Open the Project in Visual Studio 2022

**Important:** You need to open the solution file from the `MSFSSimConnectApp` subfolder.

1. Launch **Visual Studio 2022**
2. Click **"Open a project or solution"** (or File → Open → Project/Solution)
3. Navigate to where you cloned the repository
   - Example: `C:\Users\YourName\Documents\GitHub\Mastering-Drupal-8\`
4. **Open the `MSFSSimConnectApp` folder** (this is crucial!)
5. Select **MSFSSimConnectWebAPI.sln** (the solution file, not .csproj)
6. Click **Open**

**Visual Studio will now load only the C# project**, ignoring any Drupal files in the parent directory.

**Still having trouble?** See the detailed guide: [HOW_TO_OPEN_IN_VS2022.md](HOW_TO_OPEN_IN_VS2022.md) for alternative methods.

### Step 3: Build the Project

1. At the top menu, click **Build** → **Build Solution**
   - Or press `Ctrl+Shift+B` on your keyboard
2. Watch the **Output** window at the bottom
3. Wait for "Build succeeded" message (usually 10-30 seconds)

### Step 4: Run the Application

1. Press the green **▶ Start** button at the top (or press `F5`)
2. A browser window will open automatically
3. You should see the **MSFS Flight Data Dashboard**

**That's it!** The application is now running on your computer.

### Step 5: View on Your Tablet

1. **Find your computer's IP address:**
   - Press `Windows Key + R`
   - Type `cmd` and press Enter
   - Type `ipconfig` and press Enter
   - Look for **"IPv4 Address"** - it will look like `192.168.1.100`
   - Write down this number

2. **On your tablet:**
   - Make sure it's connected to the **same WiFi** as your computer
   - Open a web browser (Chrome, Safari, etc.)
   - Type in the address bar: `http://192.168.1.100:5000`
     (replace `192.168.1.100` with YOUR computer's IP)
   - Press Go/Enter

3. **You should see the dashboard!**
   - Data updates automatically every second
   - Try the controls at the bottom

### Troubleshooting

**Problem: Browser shows "Can't reach this page"**
- Make sure the application is running in Visual Studio (green button pressed)
- Check your IP address is correct
- Make sure both devices are on the same WiFi network

**Problem: Windows Firewall blocking**
1. Click **Allow access** when Windows asks
2. If you missed it:
   - Open **Windows Defender Firewall**
   - Click **"Allow an app through firewall"**
   - Find your app and check both Private and Public

**Problem: Build errors in Visual Studio**
1. Right-click on the project name in **Solution Explorer**
2. Click **"Restore NuGet Packages"**
3. Try building again

### What You're Seeing (Demo Mode)

The application generates **simulated flight data** for learning purposes. You'll see:
- Altitude, speed, heading changing realistically
- Position coordinates updating
- Aircraft attitude (pitch, bank) varying

This lets you learn how the system works without needing Microsoft Flight Simulator running.

### Next: Connect to Real MSFS

When you're ready to connect to real Microsoft Flight Simulator:
1. Read the full **README.md** in this folder
2. Install the MSFS SDK
3. Update the code as described in README.md

### Learning Tips

1. **Explore the code** - Each file has comments explaining what it does
2. **Try changing things** - Modify colors in `wwwroot/index.html`
3. **Use the debugger** - Click in the margin to set breakpoints, then press F5
4. **Read the comments** - Every file is documented for beginners

### Understanding the Files

- **Program.cs** - Where the application starts
- **Models/** - Data structures (what flight data looks like)
- **Services/** - Business logic (how we get/send data)
- **Hubs/** - Real-time communication (SignalR)
- **wwwroot/index.html** - The web page you see

### Getting Help

- Check **README.md** for detailed documentation
- Search for C# tutorials: https://docs.microsoft.com/dotnet/csharp/
- ASP.NET Core tutorial: https://docs.microsoft.com/aspnet/core/

---

**Welcome to C# development! You've successfully run your first web application! 🎉**
