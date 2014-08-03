param($installPath, $toolsPath, $package, $project)

$project.Object.References.Add("$installPath\lib\net40\Simplify.Resources.dll")