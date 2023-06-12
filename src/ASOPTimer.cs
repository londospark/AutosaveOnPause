using System;

namespace AutosaveOnPause;

public static class ASOPTimer
{
    private static DateTime LastSave { get; set; } = DateTime.MinValue;

    public static bool EligibleToSave(AutosaveOnPauseConfiguration config)
    {
        if (!config.LimitAutosaves) return true;
        if (DateTime.Now.SubtractMinutes(config.AutosaveInterval) <= LastSave) return false;
        
        LastSave = DateTime.Now;
        return true;
    }
}