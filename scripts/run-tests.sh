#!/usr/bin/env bash
set -euo pipefail

ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

dotnet test "$ROOT/tests/BackendTuya.Application.Tests/BackendTuya.Application.Tests.csproj" --configuration Release
