﻿using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Utils.Logger;
using Utils.VRising.Entities;

namespace CoffinSleep;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
// [BepInDependency("gg.deca.Bloodstone")]
// [Bloodstone.API.Reloadable]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        if (World.IsServer) Server.Load(this.Config, this.Log);
        if (World.IsClient) Client.Load(this.Config, this.Log);
    }

    public override bool Unload()
    {
        if (World.IsServer) Server.Unload();
        if (World.IsClient) Client.Unload();

        return false;
    }
}

public static class Server
{
    public static Harmony harmony;
    internal static void Load(ConfigFile config, ManualLogSource logger)
    {
        Settings.Config.Load(config, logger, "Server");

        harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

        Log.Trace("Patching harmony");
        harmony.PatchAll();

        Log.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} server side is loaded!");
    }

    internal static bool Unload()
    {
        harmony.UnpatchSelf();

        Log.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} server side is unloaded!");
        return true;
    }
}

internal static class Client
{
    public static Harmony harmony;
    internal static void Load(ConfigFile config, ManualLogSource logger)
    {
        Settings.Config.Load(config, logger, "Client");

        harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

        Log.Trace("Patching harmony");
        harmony.PatchAll();

        Log.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} client side is loaded!");
    }

    internal static bool Unload()
    {
        harmony.UnpatchSelf();

        Log.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} client side is unloaded!");
        return true;
    }
}
