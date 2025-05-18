# CoffinSleep

Coffin Sleep is a VRising mod that speeds up time while you sleep in your coffin.

## Installation (Manual)

* Install [BepInEx](https://docs.bepinex.dev/master/articles/user_guide/installation/index.html)
* Extract [CoffinSleep.dll](https://github.com/caioreix/CoffinSleep/releases) into (VRising client folder)/BepInEx/plugins
* [ServerLaunchFix](https://v-rising.thunderstore.io/package/Mythic/ServerLaunchFix/) recommended for in-game hosted
  games
* (Optional) If not using ServerLaunchFix, extract CoffinSleep.dll into (VRising server folder)/BepInEx/plugins

## How to use

* Just need to enter in your coffin and sleep.

* Actually Features:
  - Day night cycle speed.
  - Mission Progress speed.
  - Servant Conversion speed.
  - Servant Injuries speed.
  - Resources speed (trees, stones, ores, vegetation...)
  - Bosses respawn speed.
  - Npc respawn speed.
  - Chests respawn speed.
  - Stop speed during day/night transitions.
  - Stop speed during blood moon.

* Future possible features:
  - Blood recover while sleep.
  - De-buffs recover speed.
  - Clock UI while sleeping.
  - All creation/refining stations speed.
  - Config max consecutive sleeping time.

## Configuration

Values can be configured at `(VRising client/server folder)/VRising/BepInEx/config/CoffinSleep.cfg`

```
[🔧Server]

## Change the increased game time in hours per real time second
# Setting type: Int32
# Default value: 1
IncreasedTime = 1

## Enabled, sleep just speeds the time when it's daytime
# Setting type: Boolean
# Default value: true
OnlyDayTimeSleep = true

## Enabled, sleep just speeds the time if all players of the server are sleeping
# Setting type: Boolean
# Default value: true
OnlyAllPlayersSleeping = true

## Enabled, will stop time speed during the day and night transitions
# Setting type: Boolean
# Default value: true
PauseDuringTransitions = true

## Enabled, will stop time speed during blood moon nights
# Setting type: Boolean
# Default value: false
SleepBloodMoon = false
```
