// include Fake lib
#r "paket:
storage: packages

nuget Fake.IO.FileSystem
nuget Fake.Core.Target
nuget Fake.DotNet.MSBuild
nuget Fake.DotNet.Testing.NUnit
nuget NUnit.ConsoleRunner //"

#if !FAKE
#load "./.fake/build.fsx/intellisense.fsx"
#endif

open Fake.Core
open Fake.DotNet
open Fake.DotNet.Testing
open Fake.IO.Globbing.Operators
open Fake.IO

let buildDir = "./Build/"

let installFolder = (Environment.environVar "LOCALAPPDATA") + "/Colossal Order/Cities_Skylines/Addons/Mods/AutosaveOnPause"
let nunitRunnerPath = "./.fake/build.fsx/packages/NUnit.ConsoleRunner/tools/nunit3-console.exe"

Target.create "Clean" (fun _ ->
    Shell.cleanDir buildDir
)

Target.create "Build" (fun _ -> 
    !! "./Src/AutosaveOnPause/*.csproj"
      |> MSBuild.runRelease id buildDir "Build"
      |> Trace.logItems "AppBuild-Output: "
)

Target.create "Test" (fun _ -> 
    !! "./Src/AutosaveOnPause.Tests/*.csproj"
      |> MSBuild.runRelease id buildDir "Build"
      |> Trace.logItems "TestBuild-Output: "
    !! (buildDir + "*.Tests.dll")
      |> NUnit3.run (fun p -> { p with ShadowCopy = false; ToolPath = nunitRunnerPath })
)

Target.create "Install" (fun _ -> 
    Shell.mkdir (installFolder)
    Shell.copyFile installFolder (buildDir + "AutosaveOnPause.dll")
    Shell.copyFile installFolder (buildDir + "AutosaveOnPause.pdb")
    Shell.copyFile installFolder (buildDir + "CitiesHarmony.API.dll")
)

open Fake.Core.TargetOperators

// Dependencies
"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Install"

// start build
Target.runOrDefault "Install"