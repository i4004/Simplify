$config = "release"
$excludeProjects = @('Simplify.WindowsServices.IntegrationTests', 'Simplify.Mail.IntegrationTests')

$excludeProjectsRegex = [string]::Join('|', $excludeProjects)

# Find each test project and run tests and upload results to AppVeyor
Get-ChildItem .\**\*.csproj -Recurse | 
	Where-Object { ($_.Name -match ".*Test(s)?.csproj$") -and ($_.Name -notmatch $excludeProjectsRegex) } | 
	ForEach-Object { 

	# Run dotnet test on the project and output the results in mstest format (also works for other frameworks like nunit)
	& dotnet test $_.FullName --configuration $config --no-build --no-restore --logger "trx;LogFileName=..\..\..\test-result.trx" 

	# if on build server upload results to AppVeyor
	if ("${ENV:APPVEYOR_JOB_ID}" -ne "")
	{
		$wc = New-Object 'System.Net.WebClient'
		$wc.UploadFile("https://ci.appveyor.com/api/testresults/mstest/$($env:APPVEYOR_JOB_ID)", (Resolve-Path .\test-result.trx)) 
	}

	# don't leave the test results lying around
	Remove-Item .\test-result.trx -ErrorAction SilentlyContinue
}