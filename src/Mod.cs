using System;
using System.Reflection;
using CitiesHarmony.API;
using ColossalFramework.UI;
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
            var saveName = helper.AddTextfield("Save name", config.SaveName, value => config.SaveName = value) as UITextField;
            saveName.width += 300;
            var nameHelperGroup = helper.AddGroup("Tags to insert into the save name:") as UIHelper;

            var component = nameHelperGroup.self as UIPanel;
            component.autoLayoutDirection = LayoutDirection.Horizontal;
            component.autoLayoutPadding = new UnityEngine.RectOffset(5, 5, 0, 0);

            nameHelperGroup.AddButton("City Name", () => saveName.text += "{{CityName}}");
            nameHelperGroup.AddButton("Game Year", () => saveName.text += "{{Year}}");
            nameHelperGroup.AddButton("Game Month", () => saveName.text += "{{Month}}");
            nameHelperGroup.AddButton("Game Day", () => saveName.text += "{{Day}}");

            var throttleGroup = helper.AddGroup("Autosave Timing:\r\nThese settings do not affect and are not affected by the in-game autosaves.") as UIHelper;
            var limitFrequency = throttleGroup.AddCheckbox("Limit Autosave Frequency", config.LimitAutosaves, value => config.LimitAutosaves = value) as UICheckBox;
            var autosaveFrequency = throttleGroup.AddSlider("Cooldown", 1, 60, 1, config.AutosaveInterval, value => config.AutosaveInterval = (float)Math.Round(value)) as UISlider;
            autosaveFrequency.tooltip = $"{autosaveFrequency.value} minutes";
            autosaveFrequency.width += 300;

            var bottomPanel = throttleGroup.self as UIPanel;
            var time = bottomPanel.AddUIComponent<UILabel>();
            time.padding = new UnityEngine.RectOffset(0, 0, 15, 0);

            if (config.LimitAutosaves)
            {
                time.text = $"At least {config.AutosaveInterval} minutes must pass between autosaves.";
            }
            else
            {
                time.text = "The game will save everytime you pause.";
            }

            autosaveFrequency.eventValueChanged += (sender, value) =>
            {
                if (config.LimitAutosaves)
                {
                    time.text = $"At least {value} minutes must pass between autosaves.";
                }
                else
                {
                    time.text = "The game will save everytime you pause.";
                }
            };

            limitFrequency.eventCheckChanged += (sender, value) =>
            {
                if (value)
                {
                    time.text = $"At least {config.AutosaveInterval} minutes must pass between autosaves.";
                }
                else
                {
                    time.text = "The game will save everytime you pause.";
                }
            };

            var savePanel = bottomPanel.AddUIComponent<UIPanel>();
            savePanel.autoLayoutDirection = LayoutDirection.Horizontal;
            savePanel.height = 50;

            throttleGroup.AddButton("Save", () => Configuration<AutosaveOnPauseConfiguration>.Save());
            throttleGroup.AddButton("Cancel", () =>
            {
                config = Configuration<AutosaveOnPauseConfiguration>.Load();
                saveName.text = config.SaveName;
                limitFrequency.isChecked = config.LimitAutosaves;
                autosaveFrequency.value = config.AutosaveInterval;
                autosaveFrequency.enabled = config.LimitAutosaves;
            });
        }
    }
}
