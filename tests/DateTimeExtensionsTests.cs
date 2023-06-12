using System;
using NUnit.Framework;

namespace AutosaveOnPause.Tests;

[TestFixture]
public class DateTimeExtensionsTests
{
    [FsCheck.NUnit.Property(Arbitrary = new [] {typeof(OneHourFloatArb)})]
    public void SubtractMinutesWorksCorrectly(double minutes)
    {
        var expected = new DateTime(2023, 06, 12, 23, 32, 30);
        var actual = expected.AddMinutes(minutes).SubtractMinutes(minutes);
        
        Assert.That(actual, Is.EqualTo(expected));
    }
}