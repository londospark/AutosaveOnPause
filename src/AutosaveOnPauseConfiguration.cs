namespace AutosaveOnPause;

[ConfigurationPath("AutosaveOnPause.xml")]
public class AutosaveOnPauseConfiguration
{
    public string SaveName { get; set; } = "Autosave {{CityName}}: {{Year}}-{{Month}}-{{Day}}";
    public bool LimitAutosaves { get; set; } = false;
    public float AutosaveInterval { get;  set; } = 10.0f;
}