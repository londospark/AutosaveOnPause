using System.Reflection;
using CitiesHarmony.API;
using ICities;

[assembly: AssemblyVersion("1.0.*")]

namespace AutosaveOnPause
{
    public class Mod : LoadingExtensionBase, IUserMod
    {
        public string Name => "Autosave on Pause";
        public string Description => "Save your game everytime that you pause it.";

        public void OnEnabled()
        {
            HarmonyHelper.EnsureHarmonyInstalled();
        }

        public override void OnCreated(ILoading loading)
        {
            if (HarmonyHelper.IsHarmonyInstalled) Patcher.PatchAll();
        }

        public override void OnReleased()
        {
            if (HarmonyHelper.IsHarmonyInstalled) Patcher.UnpatchAll();
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            var config = Configuration<AutosaveOnPauseConfiguration>.Load();
            var group = helper.AddGroup("Save options:");
            group.AddTextfield("Save name", config.SaveName, value => {
                config.SaveName = value;
                Configuration<AutosaveOnPauseConfiguration>.Save();
            });
        }
    }
}
