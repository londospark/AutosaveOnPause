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

        public static string SaveName {get; set;} = Constants.DefaultFileName; 

        public static void PatchAll()
        {
            if (patched) return;

            patched = true;
            var harmony = new Harmony(HarmonyId);
            var mOriginal = AccessTools.PropertySetter(typeof(SimulationManager), "SimulationPaused");
            var mPostfix = AccessTools.Method(typeof(Patcher), nameof(Autosave));

            //            FileLog.Log($"ASOP Original: {mOriginal.FullDescription()}");
            //            FileLog.Log($"ASOP Patched: {mPostfix.FullDescription()}");

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
                savePanel.AutoSave(SaveName);
            }
            //var panel = UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel");
            //panel.SetMessage("Autosave!", $"Paused is now {value}", false);
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
