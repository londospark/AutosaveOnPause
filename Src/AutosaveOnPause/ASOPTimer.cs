using System;
using ColossalFramework;

namespace AutosaveOnPause
{
    public class ASOPTimer : Singleton<ASOPTimer> {
        public DateTime LastSave { get; set; } = DateTime.MinValue;

        public bool EligibleToSave() {
            if (DateTime.Now.AddMinutes(-10) > LastSave)
            {
                LastSave = DateTime.Now;
                return true;
            }
            return false;
        }

    }
}
