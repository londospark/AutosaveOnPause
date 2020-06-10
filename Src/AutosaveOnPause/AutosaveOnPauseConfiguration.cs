namespace AutosaveOnPause
{
    [ConfigurationPath("AutosaveOnPause.xml")]
    public class AutosaveOnPauseConfiguration
    {
        public string SaveName { get; set; } = "AutosavedOnPause";
        public bool LimitAutosaves { get; set; } = false;
        public double AutosaveInterval { get;  set; } = 10;
    }
}
