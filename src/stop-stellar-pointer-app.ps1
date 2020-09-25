Get-Job -Name "WebAPI" | Stop-Job
Get-Job -Name "WebAPI" | Remove-Job
Get-Job -Name "WebApp" | Stop-Job
Get-Job -Name "WebApp" | Remove-Job