using Utils.Settings;

namespace CoffinSleep.Settings;

public class ENV
{
    // Server
    public static ConfigElement<int> IncreasedTime;
    public static ConfigElement<bool> OnlyDayTimeSleep;
    public static ConfigElement<bool> OnlyAllPlayersSleeping;
    public static ConfigElement<bool> PauseDuringTransitions;
    public static ConfigElement<bool> SleepBloodMoon;


    public class Server
    {
        private static string serverSection = "🔧Server";

        public static void Setup()
        {
            Utils.Settings.Config.AddConfigActions(load);
        }

        // Load the plugin config variables.
        public static void load()
        {
            // Server
            IncreasedTime = Utils.Settings.Config.Bind(
                serverSection,
                nameof(IncreasedTime),
                1,
                "Change the increased game time in hours per real time second"
            );

            OnlyDayTimeSleep = Utils.Settings.Config.Bind(
                serverSection,
                nameof(OnlyDayTimeSleep),
                true,
                "Enabled, sleep just speeds the time when it's daytime"
            );

            OnlyAllPlayersSleeping = Utils.Settings.Config.Bind(
                serverSection,
                nameof(OnlyAllPlayersSleeping),
                true,
                "Enabled, sleep just speeds the time if all players of the server are sleeping"
            );

            PauseDuringTransitions = Utils.Settings.Config.Bind(
                serverSection,
                nameof(PauseDuringTransitions),
                true,
                "Enabled, will stop time speed during the day and night transitions"
            );

            SleepBloodMoon = Utils.Settings.Config.Bind(
                serverSection,
                nameof(SleepBloodMoon),
                false,
                "Enabled, will stop time speed during blood moon nights"
            );
        }
    }
}
