$config = "release"
$excludeProjects = @('Simplify.WindowsServices.IntegrationTests', 'Simplify.Mail.IntegrationTests')

$excludeProjectsRegex = [string]::Join('|', $excludeProjects)

# Find each test project and run tests and upload results to AppVeyor
Get-ChildItem .\**\*.csproj -Recurse | 
	Where-Object { ($_.Name -match ".*Test(s)?.csproj$") -and (($_.Name -notmatch $excludeProjectsRegex) -or ($excludeProjects.length -eq 0)) } | 
	ForEach-Object { 

	$testResultPath = (Get-Item -Path ".\").FullName + "\test-result.trx"
	$loggerParameters = "trx;LogFileName=" + $testResultPath

	# Run dotnet test on the project and output the results in mstest format (also works for other frameworks like nunit)
	& dotnet test $_.FullName --configuration $config --no-build --no-restore --logger $loggerParameters

	# if on build server upload results to AppVeyor
	if ("${ENV:APPVEYOR_JOB_ID}" -ne "")
	{
		"Uploading test results to AppVeyor..."

		$wc = New-Object 'System.Net.WebClient'
		$wc.UploadFile("https://ci.appveyor.com/api/testresults/mstest/$($env:APPVEYOR_JOB_ID)", $testResultPath) 
	}

	# don't leave the test results lying around
	Remove-Item $testResultPath -ErrorAction SilentlyContinue
}