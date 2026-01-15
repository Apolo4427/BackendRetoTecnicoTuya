$ErrorActionPreference = 'Stop'
$root = Resolve-Path (Join-Path $PSScriptRoot '..')

dotnet test (Join-Path $root 'tests/BackendTuya.Application.Tests/BackendTuya.Application.Tests.csproj') -c Release
