# SimConnect Variables Reference

## Common SimConnect Variables for Beginners

This guide shows you the most common variables you can read from and write to Microsoft Flight Simulator using SimConnect.

### 📍 Position & Navigation

| Variable Name | Type | Unit | Description | Example Value |
|---------------|------|------|-------------|---------------|
| `PLANE_LATITUDE` | Read | Degrees | Aircraft latitude | 47.6062 |
| `PLANE_LONGITUDE` | Read | Degrees | Aircraft longitude | -122.3321 |
| `PLANE_ALTITUDE` | Read | Feet | Altitude above sea level | 5000 |
| `HEADING_INDICATOR` | Read | Degrees | Current heading | 270 |
| `GPS_GROUND_SPEED` | Read | Knots | Speed over ground | 150 |

### ✈️ Flight Controls

| Variable Name | Type | Unit | Description | Example Value |
|---------------|------|------|-------------|---------------|
| `AIRSPEED_INDICATED` | Read | Knots | Indicated airspeed | 145 |
| `VERTICAL_SPEED` | Read | Feet/min | Rate of climb/descent | 500 |
| `PLANE_PITCH_DEGREES` | Read | Degrees | Pitch angle | 5.0 |
| `PLANE_BANK_DEGREES` | Read | Degrees | Bank angle | -10.0 |
| `SIM_ON_GROUND` | Read | Bool | Is aircraft on ground | 0 or 1 |

### 🤖 Autopilot

| Variable Name | Type | Unit | Read/Write | Description |
|---------------|------|------|------------|-------------|
| `AUTOPILOT_MASTER` | Bool | Bool | Both | Autopilot on/off (0=OFF, 1=ON) |
| `AUTOPILOT_HEADING_LOCK` | Bool | Bool | Both | Heading hold on/off |
| `AUTOPILOT_ALTITUDE_LOCK` | Bool | Bool | Both | Altitude hold on/off |
| `AUTOPILOT_NAV1_LOCK` | Bool | Bool | Both | NAV1 tracking on/off |
| `AUTOPILOT_APPROACH_HOLD` | Bool | Bool | Both | Approach mode on/off |
| `HEADING_BUG_SET` | Number | Degrees | Write | Set heading bug (0-360) |
| `AUTOPILOT_ALTITUDE_LOCK_VAR` | Number | Feet | Both | Altitude target |
| `AUTOPILOT_AIRSPEED_HOLD_VAR` | Number | Knots | Both | Airspeed target |
| `AUTOPILOT_VERTICAL_HOLD_VAR` | Number | Feet/min | Both | Vertical speed target |

### 🎛️ Engine & Instruments

| Variable Name | Type | Unit | Read/Write | Description |
|---------------|------|------|------------|-------------|
| `GENERAL_ENG_RPM:1` | Number | RPM | Read | Engine 1 RPM |
| `GENERAL_ENG_THROTTLE_LEVER_POSITION:1` | Number | Percent | Both | Throttle position (0-100) |
| `GENERAL_ENG_MIXTURE_LEVER_POSITION:1` | Number | Percent | Both | Mixture position (0-100) |
| `GENERAL_ENG_PROPELLER_LEVER_POSITION:1` | Number | Percent | Both | Prop lever (0-100) |

### 🛬 Landing Gear & Flaps

| Variable Name | Type | Unit | Read/Write | Description |
|---------------|------|------|------------|-------------|
| `GEAR_POSITION` | Number | Percent | Read | Gear position (0=up, 1=down) |
| `FLAPS_HANDLE_PERCENT` | Number | Percent | Both | Flaps position (0-100) |
| `SPOILERS_HANDLE_POSITION` | Number | Percent | Both | Spoilers position (0-100) |
| `BRAKE_PARKING_POSITION` | Bool | Bool | Both | Parking brake (0=OFF, 1=ON) |

### 📻 Communication & Nav

| Variable Name | Type | Unit | Read/Write | Description |
|---------------|------|------|------------|-------------|
| `COM_ACTIVE_FREQUENCY:1` | Number | MHz | Both | COM1 active frequency |
| `COM_STANDBY_FREQUENCY:1` | Number | MHz | Both | COM1 standby frequency |
| `NAV_ACTIVE_FREQUENCY:1` | Number | MHz | Both | NAV1 active frequency |
| `NAV_STANDBY_FREQUENCY:1` | Number | MHz | Both | NAV1 standby frequency |
| `TRANSPONDER_CODE:1` | Number | Number | Both | Transponder code (squawk) |

## 💡 Usage Examples

### Example 1: Turn on Autopilot and Set Heading

```json
// Step 1: Turn on autopilot
{
  "variableName": "AUTOPILOT_MASTER",
  "value": 1,
  "unit": "Bool"
}

// Step 2: Set heading bug to 270 degrees (West)
{
  "variableName": "HEADING_BUG_SET",
  "value": 270,
  "unit": "Degrees"
}

// Step 3: Enable heading hold
{
  "variableName": "AUTOPILOT_HEADING_LOCK",
  "value": 1,
  "unit": "Bool"
}
```

### Example 2: Set Altitude Hold

```json
// Step 1: Set altitude to 10,000 feet
{
  "variableName": "AUTOPILOT_ALTITUDE_LOCK_VAR",
  "value": 10000,
  "unit": "Feet"
}

// Step 2: Enable altitude hold
{
  "variableName": "AUTOPILOT_ALTITUDE_LOCK",
  "value": 1,
  "unit": "Bool"
}
```

### Example 3: Set Throttle to 75%

```json
{
  "variableName": "GENERAL_ENG_THROTTLE_LEVER_POSITION:1",
  "value": 75,
  "unit": "Percent"
}
```

### Example 4: Lower Landing Gear

```json
{
  "variableName": "GEAR_HANDLE_POSITION",
  "value": 1,
  "unit": "Bool"
}
```

### Example 5: Set Flaps to 50%

```json
{
  "variableName": "FLAPS_HANDLE_PERCENT",
  "value": 50,
  "unit": "Percent"
}
```

## 🎓 Understanding Units

| Unit Type | Description | Example Values |
|-----------|-------------|----------------|
| `Bool` | Boolean (True/False) | 0 = OFF/False, 1 = ON/True |
| `Number` | Generic number | Any integer or decimal |
| `Degrees` | Angle in degrees | 0-360 for heading, -90 to 90 for pitch |
| `Feet` | Altitude/distance in feet | 0-50000 for altitude |
| `Knots` | Speed in knots | 0-300 typical for GA aircraft |
| `Percent` | Percentage (0-100) | 0 = 0%, 100 = 100% |
| `Feet/min` | Vertical speed | -1000 to +1000 typical |
| `RPM` | Revolutions per minute | 0-3000 typical |
| `MHz` | Megahertz (radio freq) | 118.0-137.0 for COM |

## 📝 Important Notes

1. **Read vs Write**: Some variables are read-only (you can only get their values), while others can be written to (you can set their values).

2. **Engine Number**: Variables with `:1`, `:2`, etc. refer to engine number. For single-engine aircraft, use `:1`.

3. **Boolean Values**: 
   - `0` = OFF/False/Disabled
   - `1` = ON/True/Enabled

4. **Heading Range**: Heading values are 0-360 degrees:
   - 0° = North
   - 90° = East
   - 180° = South
   - 270° = West

5. **Testing**: In demo mode, only heading and altitude commands have visible effects. Full functionality requires real MSFS connection.

## 🔗 Full Documentation

For a complete list of all SimConnect variables, see:
- Official SimConnect Documentation: https://docs.flightsimulator.com/
- SimConnect Variables Reference: https://docs.flightsimulator.com/html/Programming_Tools/SimVars/Simulation_Variables.htm

## 🎯 Quick Test Commands

Copy these into the web dashboard to test:

**Test 1 - Autopilot ON:**
- Variable: `AUTOPILOT_MASTER`
- Value: `1`
- Unit: `Bool`

**Test 2 - Set Heading to North:**
- Variable: `HEADING_BUG_SET`
- Value: `0`
- Unit: `Degrees`

**Test 3 - Climb to 8000 feet:**
- Variable: `AUTOPILOT_ALTITUDE_LOCK_VAR`
- Value: `8000`
- Unit: `Feet`

---

**Tip:** Start with simple commands and gradually try more complex autopilot sequences!
