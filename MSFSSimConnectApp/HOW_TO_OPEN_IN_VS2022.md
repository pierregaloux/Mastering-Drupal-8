# How to Open the Project in Visual Studio 2022

## Understanding the Project Structure

This C# MSFS SimConnect project is located in the `MSFSSimConnectApp` subdirectory of the Mastering-Drupal-8 repository. While this might seem unusual, it works perfectly fine with Visual Studio 2022.

## Option 1: Open the Solution File Directly (Recommended)

This is the easiest method and works regardless of where the project is located.

### Step-by-Step:

1. **Open Visual Studio 2022**

2. **Click "Open a project or solution"** on the start page
   - Or use: File → Open → Project/Solution

3. **Navigate to the MSFSSimConnectApp folder**:
   - Browse to where you cloned the repository
   - Example: `C:\Users\YourName\Documents\GitHub\Mastering-Drupal-8\MSFSSimConnectApp`

4. **Select `MSFSSimConnectWebAPI.sln`** and click **Open**

5. **Done!** Visual Studio opens the project, ignoring the Drupal files outside the folder.

### Why This Works:
Visual Studio only loads the files referenced in the `.sln` and `.csproj` files. It doesn't care about other files in parent directories.

## Option 2: Clone Just the Subfolder (Advanced)

If you prefer to have only the C# project without the Drupal files, you can use Git sparse-checkout:

### Step-by-Step:

1. **Open Git Bash or Command Prompt**

2. **Create a new directory for your project**:
   ```bash
   mkdir MSFSSimConnectWebAPI
   cd MSFSSimConnectWebAPI
   ```

3. **Initialize Git with sparse checkout**:
   ```bash
   git init
   git remote add origin https://github.com/pierregaloux/Mastering-Drupal-8.git
   git config core.sparseCheckout true
   ```

4. **Specify only the subfolder to checkout**:
   ```bash
   echo "MSFSSimConnectApp/*" >> .git/info/sparse-checkout
   ```

5. **Pull the specific branch**:
   ```bash
   git pull origin copilot/build-msfs-simconnect-app
   ```

6. **Open the solution**:
   - Navigate to `MSFSSimConnectApp` folder
   - Open `MSFSSimConnectWebAPI.sln` in Visual Studio 2022

### Why Use This:
- Cleaner workspace (only C# project files)
- Smaller download (doesn't include Drupal files)
- Still connected to Git for updates

## Option 3: Use "Open Folder" in Visual Studio

Visual Studio 2022 can open folders directly without a solution file.

### Step-by-Step:

1. **Open Visual Studio 2022**

2. **Click "Open a local folder"**
   - Or use: File → Open → Folder

3. **Navigate to and select the `MSFSSimConnectApp` folder**

4. **Visual Studio automatically detects the .NET project**

5. **Run by pressing F5**

### Note:
This method uses Visual Studio's folder mode, which works well for .NET projects.

## Troubleshooting

### "I see Drupal files when I open the folder"
This is normal if you cloned the entire repository. Just navigate to the `MSFSSimConnectApp` subfolder and open the `.sln` file directly.

### "Visual Studio says it can't find the project"
Make sure you're opening:
- **Correct file**: `MSFSSimConnectWebAPI.sln` (not a `.csproj`)
- **Correct location**: Inside the `MSFSSimConnectApp` folder

### "I want a clean project without Drupal files"
Use Option 2 (sparse checkout) or simply copy the `MSFSSimConnectApp` folder to a new location:
1. Copy `MSFSSimConnectApp` folder to wherever you want
2. Paste it in a new location (e.g., `C:\Projects\MSFSSimConnectWebAPI`)
3. Open the `.sln` file from the new location
4. Works perfectly! The project is self-contained.

### "The project won't build"
After opening, restore NuGet packages:
1. Right-click the solution in Solution Explorer
2. Select "Restore NuGet Packages"
3. Wait for completion
4. Build again (Ctrl+Shift+B)

## Quick Summary

**Easiest Method:**
1. Open Visual Studio 2022
2. File → Open → Project/Solution
3. Navigate to `MSFSSimConnectApp` folder
4. Open `MSFSSimConnectWebAPI.sln`
5. Press F5 to run

**That's it!** Visual Studio handles the rest.

## Why Is This Project in a Drupal Repository?

This appears to be an organizational choice. The C# project is completely independent and self-contained within its folder. It doesn't depend on or interact with the Drupal files.

**Good news:** 
- The project works perfectly as-is
- You can copy it anywhere and it still works
- Visual Studio only sees the C# project when you open the `.sln` file

## Next Steps

Once you have the project open in Visual Studio 2022:

1. **Build the project**: Press `Ctrl+Shift+B`
2. **Run the project**: Press `F5`
3. **Your browser opens automatically** with the dashboard
4. **Access from tablet**: Use `http://YOUR_COMPUTER_IP:5000`

See `GETTING_STARTED.md` for complete beginner instructions.

---

**Bottom Line:** Don't worry about the Drupal files. Just open the `.sln` file in the `MSFSSimConnectApp` folder and Visual Studio will handle everything correctly! ✅
