namespace AutosaveOnPause
{
    public static class NameFormatter
    {
        public static string FillTemplate(this string template, CityInformation cityInformation) {
            return template.Replace("{{CityName}}", cityInformation.Name);
        }
    }
}
