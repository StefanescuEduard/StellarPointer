param($localIpAddress)
Set-Location .\stellar-pointer.web-app

$configPath = ".\src\assets\config\config.dev.json"
$configFile = Get-Content $configPath | ConvertFrom-Json
$configFile.apiUrl = "http://$($localIpAddress):8081"
$configFile | ConvertTo-Json | Set-Content $configPath

ng serve --host $localIpAddress
pause