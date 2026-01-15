$ErrorActionPreference = 'Stop'

$root = Resolve-Path (Join-Path $PSScriptRoot '..')

$envFile = Join-Path $root 'scripts/docker/.env'
$saPassword = 'YourStrong!Passw0rd'

if (Test-Path $envFile) {
  $line = Get-Content $envFile | Where-Object { $_ -match '^SA_PASSWORD=' } | Select-Object -First 1
  if ($line) { $saPassword = $line -replace '^SA_PASSWORD=', '' }
}

$env:ConnectionStrings__DefaultConnection = "Server=localhost,1433;Database=BackendTuyaDb;User Id=sa;Password=$saPassword;TrustServerCertificate=True;Encrypt=True"
Write-Host "[INFO] ConnectionStrings__DefaultConnection configurada (SQL Server en localhost:1433)"

 dotnet run --project (Join-Path $root 'BackendTuya.csproj')
