$files=get-childitem . *.cs -rec
foreach ($file in $files)
{
(Get-Content $file.PSPath) | 
Foreach-Object {$_ -replace "public static class", "internal static class"} | 
Set-Content -Encoding UTF8 $file.PSPath
}