tools\fart.exe -r "*.cs" "public static class" "internal static class"

for /F "tokens=4" %%F in ('tools\filever.exe /B /A /D src\Simplify.Xml\bin\Release\Simplify.Xml.dll') do (
  set VERSION=%%F
)

src\.nuget\NuGet.exe pack src/Simplify.Xml/Simplify.Xml.Sources.nuspec -Version %VERSION%
Get-ChildItem .\*.nupkg | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }
