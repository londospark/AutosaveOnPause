using ICities;
using UnityEngine;

namespace AutosaveOnPause
{

    public static class Constants
    {
        public const string DefaultFileName = "AutoSavedOnPause";
    }
    public class AutosaveOnPause : IUserMod
    {
        public string Name => "Autosave on Pause";
        public string Description => "Save your game everytime that you pause it.";

        public void OnSettingsUI(UIHelperBase helper)
        {
            var group = helper.AddGroup("Save options:");
            group.AddTextfield("Save name", Constants.DefaultFileName, value => Mod.SaveName = value);
        }
    }

    public class Mod : ThreadingExtensionBase
    {
        public static string SaveName { get; set; } = Constants.DefaultFileName;
        private bool savedThisPause = false;
        private ISerializableData serializableData;

        public override void OnCreated(IThreading threading)
        {
            serializableData = threading.managers.serializableData;
        }
        public override void OnAfterSimulationTick()
        {
            if (threadingManager.simulationPaused != true && !savedThisPause)
            {
                savedThisPause = true;
                serializableData.SaveGame(SaveName);
            }

            if (!threadingManager.simulationPaused == true)
                savedThisPause = false;
        }
    }
}
