using CoffinSleep.Settings;
using Utils.VRising.Entities;

namespace CoffinSleep.Systems;

public static class Mission {
    // IncreaseProgress of servant missions.
    public static void IncreaseProgress(int modifier) {
        if (!ENV.SpeedServantMissions.Value) {
            return;
        }

        var modifierInSeconds = modifier * DayNightCycle.GetTimeScale().TimePerMinute;
        var missions = ActiveServantMission.GetAllBuffers();
        foreach (var missionBuffer in missions) {
            for (int i = 0; i < missionBuffer.Length; i++) {
                var mission = missionBuffer[i];

                var newMissionLength = ActiveServantMission.GetMissionLength(mission) - modifierInSeconds;
                if (newMissionLength <= 0) {
                    newMissionLength = 0;
                }

                ActiveServantMission.SetMissionLength(ref mission, newMissionLength);
                missionBuffer[i] = mission;
            }
        }
    }
}
