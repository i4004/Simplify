@ECHO OFF

echo Uninstalling Simplify.WindowsServices.MultitaskExample service...
c:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u /LogFile= /LogToConsole=true Simplify.WindowsServices.MultitaskExample.exe
pause