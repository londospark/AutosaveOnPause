using System;
using NUnit.Framework;

namespace AutosaveOnPause.Tests;

[TestFixture]
public class NameFormatTests
{
    [Test]
    public void ATemplateWithNoPlaceholdersIsUnchanged()
    {
        var template = "AutosaveName";
        var cityInformation = new CityInformation { Name = "CityName" };
        var expected = "AutosaveName";

        Assert.That(template.FillTemplate(cityInformation), Is.EqualTo(expected));
    }

    [Test]
    public void ATemplateWithOnlyANamePaceholderIsUpdated()
    {
        var template = "{{CityName}}";
        var cityInformation = new CityInformation { Name = "Lakevalley" };
        var expected = "Lakevalley";

        Assert.That(template.FillTemplate(cityInformation), Is.EqualTo(expected));
    }

    [Test]
    public void ATemplateWithANamePaceholderAndTextIsUpdated()
    {
        var template = "Autosave: {{CityName}}";
        var cityInformation = new CityInformation { Name = "Lakevalley" };
        var expected = "Autosave: Lakevalley";

        Assert.That(template.FillTemplate(cityInformation), Is.EqualTo(expected));
    }

    [Test]
    public void AComplexTemplateIsUpdated()
    {
        var template = "Autosave: {{CityName}} - {{Year}}-{{Month}}-{{Day}}";
        var cityInformation = new CityInformation { Name = "Lakevalley", CurrentDate = new DateTime(2020, 6, 2) };
        var expected = "Autosave: Lakevalley - 2020-06-02";

        Assert.That(template.FillTemplate(cityInformation), Is.EqualTo(expected));
    }
}