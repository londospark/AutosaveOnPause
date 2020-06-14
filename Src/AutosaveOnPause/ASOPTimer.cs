using System;
using ColossalFramework;

namespace AutosaveOnPause
{
    public static class ASOPTimer
    {
        public static DateTime LastSave { get; set; } = DateTime.MinValue;

        public static bool EligibleToSave(AutosaveOnPauseConfiguration config)
        {
            if (!config.LimitAutosaves) return true;

            if (DateTime.Now.AddMinutes(-1 * config.AutosaveInterval) > LastSave)
            {
                LastSave = DateTime.Now;
                return true;
            }
            return false;
        }

    }
}
