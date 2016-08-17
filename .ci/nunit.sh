mono --runtime=v4.0 src/.nuget/NuGet.exe install NUnit.ConsoleRunner -Version 3.4.1 -o packages

runTest()
{
    mono --runtime=v4.0 packages/NUnit.ConsoleRunner.3.4.1/tools/nunit3-console.exe -labels $@
   if [ $? -ne 0 ]
   then
     exit 1
   fi
}

runTest $1 -exclude:Windows

exit $?
