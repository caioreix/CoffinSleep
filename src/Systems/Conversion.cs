using Utils.VRising.Entities;

namespace CoffinSleep.Systems;

public static class Convertion {
    // IncreaseProgress of servant convertion.
    public static void IncreaseProgress(int modifier) {
        if (!Settings.ENV.SpeedServantConversions.Value) {
            return;
        }

        var modifierInSeconds = modifier * DayNightCycle.GetTimeScale().TimePerMinute;
        var coffinStations = ServantCoffinstation.GetAllComponentData();
        foreach (var coffinStation in coffinStations) {
            var station = coffinStation.Value;
            if (!ServantCoffinstation.IsConverting(station)) {
                continue;
            }

            var newConvertionProgress = ServantCoffinstation.GetConvertionProgress(station) + modifierInSeconds;
            ServantCoffinstation.SetConvertionProgress(ref station, newConvertionProgress);
            ServantCoffinstation.SetComponentData(coffinStation.Key, station);
        }
    }
}