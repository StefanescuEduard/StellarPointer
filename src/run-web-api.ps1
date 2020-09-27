param($localIpAddress)
Set-Location .\stellar-pointer.web-api
dotnet run --urls "http://$($localIpAddress):8081"
pause