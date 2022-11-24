using BepInEx.Configuration;
using BepInEx.Logging;

namespace CoffinSleep.Settings;

public class Config {
    public static void Load(ConfigFile configFile, ManualLogSource logger, string worldType) {
        // Settings setup
        Settings.ENV.Server.Setup();
        Utils.Settings.Config.Setup(PluginInfo.PLUGIN_GUID, configFile);
        Utils.Settings.Config.Load(); // just load this after setup all actions.

        // Logger setup
        Utils.Logger.Config.Setup(logger, worldType);
    }
}