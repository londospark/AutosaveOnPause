using System;
using ColossalFramework;

namespace AutosaveOnPause
{
    public class ASOPTimer : Singleton<ASOPTimer>
    {
        public DateTime LastSave { get; set; } = DateTime.MinValue;

        public bool EligibleToSave(AutosaveOnPauseConfiguration config)
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
