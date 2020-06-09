namespace AutosaveOnPause
{
    public static class NameFormatter
    {
        public static string Parse(this string template, CityInformation cityInformation) {
            return template.Replace("{{CityName}}", cityInformation.Name);
        }
    }
}
