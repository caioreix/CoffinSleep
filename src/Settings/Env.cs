using BepInEx.Configuration;

namespace CoffinSleep.Settings;

public class ENV {
    // Server
    public static ConfigEntry<int> IncreasedTime;
    public static ConfigEntry<bool> OnlyDayTimeSleep;
    public static ConfigEntry<bool> OnlyAllPlayersSleeping;
    public static ConfigEntry<bool> PauseDuringTransitions;
    public static ConfigEntry<bool> SleepBloodMoon;
    public static ConfigEntry<bool> SpeedServantConversions;
    public static ConfigEntry<bool> SpeedServantMissions;

    public class Server {
        private static string serverSection = "🔧Server";

        public static void Setup() {
            Utils.Settings.Config.AddConfigActions(load);
        }

        // Load the plugin config variables.
        public static void load() {
            // Server
            IncreasedTime = Utils.Settings.Config.cfg.Bind(
                serverSection,
                nameof(IncreasedTime),
                30,
                "Change the increased game time in minutes per real time second"
            );

            OnlyDayTimeSleep = Utils.Settings.Config.cfg.Bind(
                serverSection,
                nameof(OnlyDayTimeSleep),
                true,
                "Enabled, sleep just speeds the time when it's daytime"
            );

            OnlyAllPlayersSleeping = Utils.Settings.Config.cfg.Bind(
                serverSection,
                nameof(OnlyAllPlayersSleeping),
                true,
                "Enabled, sleep just speeds the time if all players of the server are sleeping"
            );

            PauseDuringTransitions = Utils.Settings.Config.cfg.Bind(
                serverSection,
                nameof(PauseDuringTransitions),
                true,
                "Enabled, will stop time speed during the day and night transitions"
            );

            SleepBloodMoon = Utils.Settings.Config.cfg.Bind(
                serverSection,
                nameof(SleepBloodMoon),
                false,
                "Enabled, will stop time speed during blood moon nights"
            );

            SpeedServantConversions = Utils.Settings.Config.cfg.Bind(
                serverSection,
                nameof(SpeedServantConversions),
                true,
                "Enabled, sleep speeds servant conversion progress"
            );

            SpeedServantMissions = Utils.Settings.Config.cfg.Bind(
                serverSection,
                nameof(SpeedServantMissions),
                true,
                "Enabled, sleep speeds servant mission progress"
            );
        }
    }
}
