param($installPath, $toolsPath, $package, $project)

$project.Object.References | where { $_.Name -eq 'Simplify.Resources' } | foreach { $_.Remove() }