@ECHO OFF

echo Uninstalling Simplify.AutomatedWindowsServices.Example service...
c:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u /LogFile= /LogToConsole=true Simplify.AutomatedWindowsServices.Example.exe
pause