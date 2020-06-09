namespace AutosaveOnPause
{
    public static class NameFormatter
    {
        public static string FillTemplate(this string template, CityInformation cityInformation) =>
            template.Replace("{{CityName}}", cityInformation.Name)
                .Replace("{{Day}}", cityInformation.CurrentDate.Day.ToString("D2"))
                .Replace("{{Month}}", cityInformation.CurrentDate.Month.ToString("D2"))
                .Replace("{{Year}}", cityInformation.CurrentDate.Year.ToString());
    }
}
