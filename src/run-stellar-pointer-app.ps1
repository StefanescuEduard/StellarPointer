$localIpAddress = (Get-NetIPAddress -AddressFamily IPv4 -InterfaceAlias WiFi).IPAddress
$initialLocation = Get-Location

$localIpAddress = (Get-NetIPAddress -AddressFamily IPv4 -InterfaceAlias WiFi).IPAddress

Start-Process -FilePath "powershell" -ArgumentList "-File .\run-web-api.ps1 -localIpAddress $($localIpAddress)"
Set-Location $initialLocation
Start-Process -FilePath "powershell" -ArgumentList "-File .\run-web-app.ps1 -localIpAddress $($localIpAddress)"

Write-Host "Used IP: $($localIpAddress)."

Set-Location $initialLocation