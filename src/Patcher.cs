using Cities.DemoMode;
using ColossalFramework.UI;
using HarmonyLib;

namespace AutosaveOnPause;

public static class Patcher
{
    private const string HarmonyId = "com.garethhubball.AutosaveOnPause";
    private static bool patched = false;
    public static void PatchAll()
    {
        if (patched) return;

        patched = true;
        var harmony = new Harmony(HarmonyId);
        var originalMethod = AccessTools.PropertySetter(typeof(SimulationManager), "SimulationPaused");
        var postfixMethod = AccessTools.Method(typeof(Patcher), nameof(Autosave));
        harmony.Patch(originalMethod, postfix: new HarmonyMethod(postfixMethod));
    }

    static void Autosave(int value)
    {
        var config = Configuration<AutosaveOnPauseConfiguration>.Load();
        if (value == 0 || !ASOPTimer.EligibleToSave(config)) return;
        
        if (DemoModeLoader.instance is not null)
            return;
        var savePanel = UIView.library.Get<SavePanel>("SavePanel");
        if (savePanel is null)
            return;
        var metaData = SimulationManager.instance.m_metaData;
        var cityInformation = new CityInformation
        {
            Name = metaData.m_CityName,
            CurrentDate = metaData.m_currentDateTime
        };

        var saveName = config.SaveName.FillTemplate(cityInformation);
        savePanel.AutoSave(saveName);
    }

    public static void UnpatchAll()
    {
        if (!patched) return;

        var harmony = new Harmony(HarmonyId);
        harmony.UnpatchAll(HarmonyId);
        patched = false;
    }
}