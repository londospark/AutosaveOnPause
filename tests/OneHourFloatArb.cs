using System;
using FsCheck;

namespace AutosaveOnPause.Tests;

internal class OneHourFloatArb
{
    // ReSharper disable once UnusedMember.Global
    public static Arbitrary<double> Float() =>
        Arb.Default.Float().Filter(x => Math.Abs(x) < 70);
}