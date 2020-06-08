// include Fake lib
#r "paket:
nuget Fake.IO.FileSystem
nuget Fake.Core.Target
nuget Fake.DotNet.MSBuild //"

#if !FAKE
#load "./.fake/build.fsx/intellisense.fsx"
#endif

open Fake.Core
open Fake.DotNet
open Fake.IO.Globbing.Operators
open Fake.IO

let buildDir = "./Build/"

let installFolder = (Environment.environVar "LOCALAPPDATA") + "/Colossal Order/Cities_Skylines/Addons/Mods/AutosaveOnPause"

Target.create "Clean" (fun _ ->
    Shell.cleanDir buildDir
)

Target.create "Build" (fun _ -> 
    !! "./Src/**/*.csproj"
      |> MSBuild.runRelease id buildDir "Build"
      |> Trace.logItems "AppBuild-Output: "
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

"Build"
  ==> "Install"

// start build
Target.runOrDefault "Install"