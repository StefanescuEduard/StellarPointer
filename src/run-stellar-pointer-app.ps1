$localIpAddress = (Get-NetIPAddress -AddressFamily IPv4 -InterfaceAlias WiFi).IPAddress
$initialLocation = Get-Location

$runWebApiBlock = {
  param($localIpAddress)
  Set-Location .\stellar-pointer.web-api
  dotnet run --urls "http://$($localIpAddress):8081"
}

$runWebAppBlock = {
  param($localIpAddress)
  Set-Location .\stellar-pointer.web-app

  $configPath = ".\src\assets\config\config.dev.json"
  $configFile = Get-Content $configPath | ConvertFrom-Json
  $configFile.apiUrl = "http://$($localIpAddress):8081"
  $configFile | ConvertTo-Json | Set-Content $configPath

  ng serve --host $localIpAddress
}

$webApiJob = Start-Job $runWebApiBlock -ArgumentList $localIpAddress -Name "WebAPI"
Set-Location $initialLocation
$webAppJob = Start-Job $runWebAppBlock -ArgumentList $localIpAddress -Name "WebApp"

$webApiJob | Wait-Job
$webApiJob | Receive-Job

$webAppJob | Wait-Job
$webAppJob | Receive-Job

Set-Location $initialLocation