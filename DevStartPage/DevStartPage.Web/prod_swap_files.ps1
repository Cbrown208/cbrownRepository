Set-Location $PSScriptRoot
[Environment]::CurrentDirectory = (Get-Location -PSProvider FileSystem).ProviderPath
Write-Host "Current directory: $(Get-Location)"

$content = [System.IO.File]::ReadAllText(".\package.json").Replace("gulp build:debug","gulp build:prod")
[System.IO.File]::WriteAllText(".\package.json", $content)

 $content = [System.IO.File]::ReadAllText(".\app\environment.ts").Replace("production: false","production: true")
 [System.IO.File]::WriteAllText(".\app\environment.ts", $content)

$file = '.\Views\Shared\_Layout.cshtml'

$prodFIleName =   $file + "_prod";

If (Test-Path $prodFIleName)
{
	$fp = Get-Item -Path $prodFIleName
	$originalName = $fp.FullName.Replace("_prod","")
		
	Copy-item -Path  $fp.FullName -Destination $originalName -Force
	Write-Host "Renamed file from "   $fp.FullName " to " $originalName
}