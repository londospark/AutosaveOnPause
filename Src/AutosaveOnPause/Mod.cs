using System.Reflection;
using CitiesHarmony.API;
using HarmonyLib;
using ICities;

[assembly: AssemblyVersion("1.0.*")]

namespace AutosaveOnPause
{

    public static class Constants
    {
        public const string DefaultFileName = "AutoSavedOnPause";
    }
    public class Mod : LoadingExtensionBase, IUserMod
    {
        public string Name => "Autosave on Pause";
        public string Description => "Save your game everytime that you pause it.";

        public void OnEnabled()
        {
            Harmony.DEBUG = true;
            HarmonyHelper.EnsureHarmonyInstalled();
        }

        public override void OnCreated(ILoading loading)
        {
            FileLog.Log("AutoSavedOnPause Loading");
            if (HarmonyHelper.IsHarmonyInstalled) Patcher.PatchAll();
        }

        public override void OnReleased()
        {
            if (HarmonyHelper.IsHarmonyInstalled) Patcher.UnpatchAll();
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            var group = helper.AddGroup("Save options:");
            group.AddTextfield("Save name", Constants.DefaultFileName, value => Patcher.SaveName = value);
        }
    }
}
