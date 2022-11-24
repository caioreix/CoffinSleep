using System;
using CoffinSleep.Settings;
using Utils.Database;
using Utils.VRising.Entities;

using DNC = Utils.VRising.Entities.DayNightCycle;

namespace CoffinSleep.Systems;

public static class RotationCycle {
    private static int transitionControl = 3;
    public static void IncreaseTime(params Action<int>[] actions) {
        var dnc = DNC.GetSingleton();
        if (ENV.OnlyDayTimeSleep.Value && !DNC.IsDay(dnc)) {
            return; // Only day time sleep.
        }

        if (ENV.SleepBloodMoon.Value && DNC.IsBloodMoonDay(dnc) && !DNC.IsDay(dnc)) {
            return; // Do not sleep during blood moons.
        }

        if (User.IsAllOffline()) {
            return; // To not pass time without online players.
        }

        if (ENV.OnlyAllPlayersSleeping.Value && !PlayerCharacter.IsAllOnlinePlayersSleeping()) {
            return; // Only when all player are sleeping.
        }

        if (Cache.IsBlocked("speed", 1000)) {
            return; // To control update per second.
        }

        var modifier = ENV.IncreasedTime.Value;
        if (ENV.PauseDuringTransitions.Value && !ENV.OnlyDayTimeSleep.Value) {
            handleModifier(ref modifier, dnc); // Will stop time speed in all transitions.
        }

        if (modifier == 0) {
            return; // Ignore time speed.
        }

        foreach (var action in actions) {
            action(modifier); // run all increased time actions.
        }

        SetTimeOfDayEvent.Add(minute: modifier); // increase the time in game minutes.
    }

    private static void handleModifier(ref int modifier, ProjectM.DayNightCycle dnc) {
        var timeSinceDayStart = DNC.GetTimeSinceDayStart(dnc);
        if (inTransition(timeSinceDayStart, dnc)) { modifier = 0; return; } // stop time speed in transitions

        var safeTransitionControl = transitionControl * 2;
        var dayDurationInSeconds = DNC.GetDayDurationInSeconds(dnc); // all day
        var dayTimeDurationInSeconds = DNC.GetDayTimeDurationInSeconds(dnc); // day time

        var modifierInGameSeconds = modifier * DNC.GetTimeScale(dnc).TimePerMinute;
        if (modifierInGameSeconds > dayDurationInSeconds) {
            modifierInGameSeconds = dayDurationInSeconds;
        }

        var nextTimeSinceDayStart = timeSinceDayStart + modifierInGameSeconds;
        if (nextTimeSinceDayStart > dayDurationInSeconds && !DNC.IsDay(dnc)) { // if is night and passes the day
            modifierInGameSeconds = dayDurationInSeconds - timeSinceDayStart - safeTransitionControl; // (seconds to day transition) - transition control
        } else if (nextTimeSinceDayStart > dayDurationInSeconds && DNC.IsDay(dnc)) { // if is day and passes the night transition
            modifierInGameSeconds = dayTimeDurationInSeconds - timeSinceDayStart - safeTransitionControl; // (seconds to night transition) - transition control
        } else if (nextTimeSinceDayStart > dayTimeDurationInSeconds && DNC.IsDay(dnc)) { // if is day and passes the night transition
            modifierInGameSeconds = dayTimeDurationInSeconds - timeSinceDayStart - safeTransitionControl; // (seconds to night transition) - transition control
        }

        modifier = (int)(modifierInGameSeconds / DNC.GetTimeScale(dnc).TimePerMinute);

        if (modifier < 0) { modifier = 0; }
    }

    private static bool inTransition(float gameDayTime, ProjectM.DayNightCycle dnc) {
        if (inNightToDayTransition(gameDayTime, dnc)) { return true; }
        if (inDayToNightTransition(gameDayTime, dnc)) { return true; }
        return false;
    }

    private static bool inNightToDayTransition(float gameDayTime, ProjectM.DayNightCycle dnc) {
        var DayDurationInSeconds = DNC.GetDayDurationInSeconds(dnc);
        if (gameDayTime >= (DayDurationInSeconds - transitionControl) &&
            gameDayTime <= transitionControl) {
            return true;
        }
        return false;
    }

    private static bool inDayToNightTransition(float gameDayTime, ProjectM.DayNightCycle dnc) {
        var DayTimeDurationInSeconds = DNC.GetDayTimeDurationInSeconds(dnc);
        if (gameDayTime >= (DayTimeDurationInSeconds - transitionControl) &&
            gameDayTime <= (DayTimeDurationInSeconds + transitionControl)) {
            return true;
        }
        return false;
    }
}
