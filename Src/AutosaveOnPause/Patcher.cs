using System;
using System.Reflection;
using Cities.DemoMode;
using ColossalFramework.UI;
using HarmonyLib;

namespace AutosaveOnPause
{
    public static class Patcher
    {
        private const string HarmonyId = "com.garethhubball.AutosaveOnPause";
        private static bool patched = false;
        public static void PatchAll()
        {
            if (patched) return;

            patched = true;
            var harmony = new Harmony(HarmonyId);
            var mOriginal = AccessTools.PropertySetter(typeof(SimulationManager), "SimulationPaused");
            var mPostfix = AccessTools.Method(typeof(Patcher), nameof(Autosave));
            harmony.Patch(mOriginal, postfix: new HarmonyMethod(mPostfix));
        }

        static void Autosave(int value)
        {
            if (value != 0)
            {
                if ((Object)DemoModeLoader.instance != (Object)null)
                    return;
                SavePanel savePanel = UIView.library.Get<SavePanel>("SavePanel");
                if (!((Object)savePanel != (Object)null))
                    return;
                var saveName = Configuration<AutosaveOnPauseConfiguration>.Load().SaveName.Parse(new CityInformation());
                savePanel.AutoSave(saveName);
            }
        }

        public static void UnpatchAll()
        {
            if (!patched) return;

            var harmony = new Harmony(HarmonyId);
            harmony.UnpatchAll(HarmonyId);
            patched = false;
        }
    }
}
