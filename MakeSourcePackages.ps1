# Patching all sources with internal attribute for use in source packages

$files=get-childitem . *.cs -rec
foreach ($file in $files)
{
(Get-Content $file.PSPath) | 
Foreach-Object {$_ -replace "public static class", "internal static class"} | 
Set-Content -Encoding UTF8 $file.PSPath
}

# Getting packages versions

$version = [System.Diagnostics.FileVersionInfo]::GetVersionInfo("$PSScriptRoot/src/Simplify.Xml/bin/Release/Simplify.Xml.dll").FileVersion

# Packing source packages

src\.nuget\NuGet.exe pack src/Simplify.Xml/Simplify.Xml.Sources.nuspec -Version $version

# Publishing to Appveyor artifacts

Get-ChildItem .\*.nupkg | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }